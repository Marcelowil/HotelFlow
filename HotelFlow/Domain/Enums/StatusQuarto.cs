namespace HotelFlow.Domain.Enums
{
    public enum StatusQuarto
    {
        Disponivel,
        Ocupado,
        EmManutencao
    }

    public static class StatusQuartoExtensions{
        public static string Descricao(this StatusQuarto statusQuarto)
        {
            return statusQuarto switch
            {
                StatusQuarto.Disponivel => "Disponível",
                StatusQuarto.Ocupado => "Ocupado",
                StatusQuarto.EmManutencao => "Em Manutenção"
            };
        }
    }
}
