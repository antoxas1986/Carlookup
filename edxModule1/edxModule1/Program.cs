using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edxModule1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Student Only
            string firstName = "John";
            string lastName = "Johnson";
            string birthdate = "12/05/1980";
            string address1 = "123 Main street";
            string address2 = "Apt B";
            string city = "Chicago";
            string state = "IL";
            int zip = 12345;
            string country = "USA";
            Console.WriteLine("Hello my name is {0} {1} \n\nI was born on {2} \n\nI live at: \n\n{3}, {4} \n{5}, {6} {7} {8} ",
                firstName, lastName, birthdate, address1, address2, city, state, zip, country);
            Console.ReadLine();
        }
    }
}
