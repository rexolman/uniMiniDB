using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public interface IPerson
    {
         string CardNumber { get; set; }
         string Name { get; set;  }
         DateTime Birthday { get; set; }
         int calcAge(DateTime date);
    }
    public class Person : IPerson
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public int calcAge(DateTime date)
        {
            int years = DateTime.Today.Year - date.Year - 1;
            date = date.AddYears(DateTime.Today.Year - date.Year);
            years += date <= DateTime.Today ? 1 : 0;
            return years;
        }
    }
}
