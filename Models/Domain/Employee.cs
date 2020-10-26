using System.Runtime.ExceptionServices;
using System;
namespace FlowerChainAPI.Models.Domain 
{
    public class Employee : Person
    {
        public DateTime workStartDate{get; set;}

        public String employeeId { get; set; }



    }
}