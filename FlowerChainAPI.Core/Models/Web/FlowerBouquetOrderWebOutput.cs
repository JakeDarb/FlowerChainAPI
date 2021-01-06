namespace FlowerChainAPI.Models.Web
{
    
    public class FlowerBouquetOrderWebOutput
    {
        public FlowerBouquetOrderWebOutput(int Id, int OrderId, int FlowerBouquetId, int Amount)
        {
           
            id = Id;
            orderId = OrderId;
            flowerBouquetId = FlowerBouquetId;
            amount = Amount;
        }

        public int id { get; set; }

        public int orderId { get; set; }

        public int flowerBouquetId { get; set; }

        public int amount { get; set; }
        
    }
}