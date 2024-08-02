using System.Collections.Generic;

namespace UdemyAspNetCore.Models
{
    public static class CustomerContext
    {
        public static List<Customer> Customer = new List<Customer>()
        {
            new Customer{Id=1,FirstName = "Taha", LastName = "Adıyaman", Age = 26},
            new Customer{Id=2,FirstName = "Yasemen", LastName = "Zambakcı", Age = 24},

        };
    }
}
