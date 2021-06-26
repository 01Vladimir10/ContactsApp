namespace Domain.Common
{
    public interface ILogger<T>
    {
        public void I(string message);
        public void E(string message);
        public void W(string message);
    }
}