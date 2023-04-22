namespace CsUtility.Pool
{
    public interface IObjectPoolFactory<T> where T : class
    {
        T Create();

        void ActionOnGet(T obj);

        void ActionOnRelease(T obj);

        void ActionOnDispose(T obj);
    }
}