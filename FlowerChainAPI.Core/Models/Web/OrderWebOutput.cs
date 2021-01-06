namespace FlowerChainAPI.Models.Web
{
    
    public class OrderWebOutput
    {
        public OrderWebOutput(int Id, string DateTimeOrder ,string PersonId)
        {
           
            id = Id;
            dateTimeOrder = DateTimeOrder;
            personId = PersonId;
        }

        public int id { get; set; }
        public string dateTimeOrder { get; set; }
        public string personId{ get; set; }
    }
}