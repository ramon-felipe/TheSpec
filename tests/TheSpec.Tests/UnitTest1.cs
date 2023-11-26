using AutoFixture;
using FluentAssertions;
using TheSpecTests.Models;

namespace TheSpec.Tests;

public class SpecTests
{
	private readonly Fixture fixture;
    public SpecTests()
    {
		this.fixture = new Fixture();
        fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
	public void Should_Filter_For_IEnumerable_Successfully()
	{
		// Arrange
		var movie1 = this.fixture.Build<Movie>().With(_ => _.Id, 1).Create();
		var movie2 = this.fixture.Build<Movie>().With(_ => _.Id, 2).Create();
		var movies = new HashSet<Movie> { movie1, movie2 };
		var spec = new Spec<Movie>(_ => _.Id > 1);

		// Act
		var result = movies.Where(spec.Predicate);

		// Assert
		result.Should().HaveCount(1);

		result.Should().Contain(movie2);
	}

    [Fact]
    public void Should_Filter_For_IQueryable_Successfully()
    {
        // Arrange
        var movie1 = this.fixture.Build<Movie>().With(_ => _.Id, 1).Create();
        var movie2 = this.fixture.Build<Movie>().With(_ => _.Id, 2).Create();
        var movies = new HashSet<Movie> { movie1, movie2 }.AsQueryable();
        var spec = new Spec<Movie>(_ => _.Id > 1);

        // Act
        var result = movies.Where(spec.Expression);

        // Assert
        result.Should().HaveCount(1);

        result.Should().Contain(movie2);
    }
}