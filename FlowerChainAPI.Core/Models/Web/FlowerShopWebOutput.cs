namespace FlowerChainAPI.Models.Web
{
    
    public class FlowerShopWebOutput
    {
        public FlowerShopWebOutput(int Id, string ShopName, string StreetName, string HouseNumber,string City, string PostalCode, string PhoneNumber, string Email)
        {
            id = Id;
            shopName = ShopName;
            streetName = StreetName;
            houseNumber = HouseNumber;
            city = City;
            postalCode = PostalCode;
            phoneNumber = PhoneNumber;
            email = Email;
            
        }

        public int id { get; set; }
        public string shopName { get; set; }
        public string streetName { get; set; }
        public string houseNumber { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        
    }
}
