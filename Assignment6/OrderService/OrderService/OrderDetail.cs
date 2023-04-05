using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement
{
    public class OrderDetail
    {
        public Good Goods { get; set; }
        //public string _GoodsName;
        public string GoodsName
        {
            get
            {
                return Goods.Name;
            }
            set
            {
                Goods.Name = value;
            }
        }

        public double GoodsPrice { 
            get
            {
                return Goods.Price;
            }
            set
            {
                Goods.Price = value;
            }
        }
        public int Number { get; set; }
        public double TotalPrice { 
            get
            {
                return Number * Goods.Price;
            }
        
        }

       public OrderDetail(Good goods, int number)
        {
            Goods = goods;
            Number = number;
            
        }

        public override string ToString()
        {
            return $"{Goods.Name, -17}{Goods.Price, -7}{Number,-8}{TotalPrice, -5}";
        }

        public override bool Equals(object obj)
        {
            var other = obj as OrderDetail;
            if (other == null)
                return false;
            return Goods.Name == other.Goods.Name;
        }
    }
}
