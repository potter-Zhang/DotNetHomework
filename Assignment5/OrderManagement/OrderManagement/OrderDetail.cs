using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{
    public class OrderDetail
    {
        public string Name { get; set; }
        public int Number { get; set; }

        public OrderDetail(string name, int number)
        {
            Name = name; 
            Number = number;
        }

        public override string ToString()
        {
            return $"{Name, -11}\t{Number}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as OrderDetail;
            if (other == null)
                return false;
            return Name == other.Name;
        }
    }
}
