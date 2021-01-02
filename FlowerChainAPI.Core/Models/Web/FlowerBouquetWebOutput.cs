namespace FlowerChainAPI.Models.Web
{
    
    public class FlowerBouquetWebOutput
    {
        public FlowerBouquetWebOutput(int Id, string BouquetName, double Price, int AmountSold,string Description)
        {
            id = Id;
            bouquetName = BouquetName;
            price = Price;
            amountSold = AmountSold;
            description = Description;
        }

        public int id { get; set; }
        public string bouquetName{get;set;}
        public double price { get; set; }
        public int  amountSold { get; set; }
        public string description {get;set;}
    }
}

