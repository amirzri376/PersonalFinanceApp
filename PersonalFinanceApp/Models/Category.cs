using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceApp.Models
{
    public class Category
    {
        public string Name { get; }

        public Category(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
