namespace MinimalAPIs.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? Picture { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Stock {  get; set; }

        public int GeneroId { get; set; }
        public Genero? Genero { get; set; }
    }
}
