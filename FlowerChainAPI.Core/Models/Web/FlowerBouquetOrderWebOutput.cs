namespace FlowerChainAPI.Models.Web
{
    
    public class FlowerBouquetOrderWebOutput
    {
        public FlowerBouquetOrderWebOutput(string Id, int OrderId, int FlowerBouquetId, int Amount)
        {
            
            id = Id;
            orderId = OrderId;
            flowerBouquetId = FlowerBouquetId;
            amount = Amount;

        }

        public string id { get; set; }

        public int orderId { get; set; }

        public int flowerBouquetId { get; set; }

        public int amount { get; set; }

        
        
    }
}