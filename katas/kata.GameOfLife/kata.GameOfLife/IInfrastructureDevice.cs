namespace kata.GameOfLife
{
    public interface IInfrastructureDevice<T>
    {
        void Output(T state);
        T Input();
    }
}