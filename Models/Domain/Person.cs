using System;
namespace FlowerChainAPI.Models.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public String firstName { get; set; }
        
        public String lastName { get; set; }
        public String streetName { get; set; }
        public String postalCode { get; set; }
        public String city { get; set; }
        public String phoneNumber { get; set; }
        public String email { get; set; }
        public DateTime dateOfBirth { get; set; }
        public Boolean isEmployee { get; set; }
    }
}