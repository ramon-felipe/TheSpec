namespace TheSpec;

public interface ISpec<T>
{
    bool IsSatisfiedBy(T entity);
}