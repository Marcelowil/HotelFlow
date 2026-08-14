namespace HotelFlow.Domain.Enums
{
    public enum StatusReserva
    {
        Pendente,
        Confirmada, 
        Cancelada, 
        Concluida
    }

    public static class StatusReservaExtensions
    {
        public static string Descricao(this StatusReserva status)
        {
            return status switch
            {
                StatusReserva.Pendente => "Pendente",
                StatusReserva.Confirmada => "Confirmada",
                StatusReserva.Cancelada => "Cancelada",
                StatusReserva.Concluida => "Concluída"
            };
        }
    }
}
