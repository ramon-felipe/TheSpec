using System.Linq.Expressions;
namespace TheSpec;

public class Spec<T> : ISpec<T>
{
    public Spec(Expression<Func<T, bool>> expression)
    {
        this.Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        this.Predicate = this.Expression.Compile();
    }

    public Expression<Func<T, bool>> Expression { get; }
    public Func<T, bool> Predicate { get; }

    public bool IsSatisfiedBy(T entity) => this.Predicate.Invoke(entity);
}
