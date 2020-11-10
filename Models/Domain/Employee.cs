using System.Runtime.ExceptionServices;
using System;
namespace FlowerChainAPI.Models.Domain 
{
    public class Employee : Person
    {
        public int id { get; set; }

        public DateTime workStartDate{get; set;}

        public int personId { get; set; }





    }
}