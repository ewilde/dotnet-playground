using System.Collections.Generic;

namespace Edward.Wilde.CSharp.Features.Model
{
    public class Product
    {
        readonly string name;
        public string Name { get { return name; } }
        readonly decimal price;
        public decimal Price { get { return price; } }
        public int SupplierId { get; set; }

        public int Id { get; set; }

        public Product(string name, decimal price)
        {
            this.name = name;
            this.price = price;
        }
        public static List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product(name: "West Side Story", price: 9.99m) {Id=1, SupplierId = 2},
                new Product(name: "Assassins", price: 14.99m) {Id=2, SupplierId = 4},
                new Product(name: "Frogs", price: 13.99m) {Id=3, SupplierId = 3},
                new Product(name: "Sweeney Todd", price: 10.99m) {Id=4, SupplierId = 1}
            };
        }
        public override string ToString()
        {
            return string.Format("{0}: {1}", name, price);
        }
    }
}
