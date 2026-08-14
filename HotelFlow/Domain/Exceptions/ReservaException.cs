namespace HotelFlow.Domain.Exceptions
{
    public class ReservaException : Exception
    {
        public ReservaException(string mensagem) : base(mensagem) { }
    }
}
