namespace ServiceLayer.Models.Exceptions
{
    public class ClientSideException : Exception
    {
        public ClientSideException(string message) : base(message) { }
    }
}
