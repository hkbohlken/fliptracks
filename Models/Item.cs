namespace FlipTracks.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";
        public string Category { get; set; } = "";

        public string PurchaseSource { get; set; } = "";
        public DateOnly PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }

        public string? SaleSource { get; set; }
        public DateOnly? SaleDate { get; set; }
        public decimal? SalePrice { get; set; }

        public decimal Profit
        {
            get
            {
                if (SalePrice == null)
                {
                    return 0;
                }

                return SalePrice.Value - PurchasePrice;
            }
        }
    }
}
