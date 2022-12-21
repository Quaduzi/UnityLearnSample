public interface IChainable<in T>
{
    public void AddNext(T obj);
}
