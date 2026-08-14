namespace HotelFlow.Domain.Enums
{
    public enum CategoriaQuarto
    {
        Standard, 
        Luxo, 
        Suite
    }

    public static class CategoriaQuartoExtensions
    {
        public static string Descricao(this CategoriaQuarto categoria)
        {
            return categoria switch
            {
                CategoriaQuarto.Standard => "Standard",
                CategoriaQuarto.Luxo => "Luxo",
                CategoriaQuarto.Suite => "Suíte"
            };
        }
    }
}
