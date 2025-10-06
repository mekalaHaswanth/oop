using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop
{
    public class ShoppingBasket
    {
        private List<Product> basketItems;
        private double totalAmount;

        public ShoppingBasket()
        {
            basketItems = new List<Product>();
            totalAmount = 0.0;
        }

        public void AddItem(Product product)
        {
            basketItems.Add(product);
            totalAmount += product.Price;
            Console.WriteLine($"Product '{product.Name}' added to the basket. Current total: {totalAmount:C}");
        }

        public void RemoveItem(int productId)
        {
            Product productToRemove = basketItems.Find(p => p.ProductId == productId);
            if (productToRemove != null)
            {
                basketItems.Remove(productToRemove);
                totalAmount -= productToRemove.Price;
                Console.WriteLine($"Product '{productToRemove.Name}' removed from the basket. Current total: {totalAmount:C}");
            }
            else
            {
                Console.WriteLine($"Product with ID {productId} not found in the basket.");
            }
        }

        public void ViewBasket()
        {
            Console.WriteLine("\n--- Shopping Basket ---");
            if (basketItems.Count == 0)
            {
                Console.WriteLine("Your basket is empty.");
            }
            else
            {
                foreach (var product in basketItems)
                {
                    Console.WriteLine($"Product ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price:C}");
                }
                Console.WriteLine($"Total Amount: {totalAmount:C}");
            }
        }

    }
}
