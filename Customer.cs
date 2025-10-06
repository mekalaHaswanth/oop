using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop
{
    public class Customer : User
    {
        // Private Fields
        private string role;
        private string status;

        // ShoppingBasket instance for this customer
        private ShoppingBasket shoppingBasket;

        // Public Properties
        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        // Constructor for Customer class, chaining to base User constructor
        public Customer(int userId, string userName, string password, string email, string phoneNumber,
                        string street, string city, string role, string status)
            : base(userId, userName, password, email, phoneNumber, street, city) // Call base constructor
        {
            this.Role = role;
            this.Status = status;
            this.shoppingBasket = new ShoppingBasket(); // Initialize shopping basket
        }

        // Method to display customer-specific details
        public void DisplayCustomerDetails()
        {
            // Display base class details
            DisplayUserDetails();

            // Display Customer-specific details
            Console.WriteLine($"Role: {Role}");
            Console.WriteLine($"Status: {Status}");
        }

        // Method to update customer-specific details
        public void UpdateCustomerDetails(string newRole, string newStatus)
        {
            this.Role = newRole;
            this.Status = newStatus;
            Console.WriteLine("Customer-specific details updated successfully.");
        }

        // Shopping Basket Operations
        public void AddItemToBasket(Product product)
        {
            shoppingBasket.AddItem(product);
        }

        public void RemoveItemFromBasket(int productId)
        {
            shoppingBasket.RemoveItem(productId);
        }

        public void ViewShoppingBasket()
        {
            shoppingBasket.ViewBasket();
        }

        // Method to interact with shopping basket
        public void ManageShoppingBasket()
        {
            // Sample products in the store
            //var product1 = new Product(1, "Laptop", 1000.00);
            //var product2 = new Product(2, "Smartphone", 699.99);
            //var product3 = new Product(3, "Tablet", 499.99);

            while (true)
            {
                // Display menu for basket management
                Console.Clear();
                Console.WriteLine("---- Customer Shopping Basket ----");
                Console.WriteLine("1. Add item to basket");
                Console.WriteLine("2. Remove item from basket");
                Console.WriteLine("3. View items in basket");
                Console.WriteLine("4. Exit");
                Console.Write("Please select an option: ");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    Console.WriteLine("\nAvailable Products:");
                    Console.WriteLine("1. Laptop - $1000");
                    Console.WriteLine("2. Smartphone - $699.99");
                    Console.WriteLine("3. Tablet - $499.99");
                    Console.Write("Enter the product ID to add: ");
                    int productId = int.Parse(Console.ReadLine());

                    //if (productId == 1) AddItemToBasket(product1);
                    //else if (productId == 2) AddItemToBasket(product2);
                    //else if (productId == 3) AddItemToBasket(product3);
                    //else
                        Console.WriteLine("Invalid product ID. Please try again.");
                }
                else if (option == "2")
                {
                    Console.Write("\nEnter the product ID to remove: ");
                    int productIdToRemove = int.Parse(Console.ReadLine());
                    RemoveItemFromBasket(productIdToRemove);
                }
                else if (option == "3")
                {
                    ViewShoppingBasket();
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                }
                else if (option == "4")
                {
                    break; // Exit the shopping basket management
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
        }
    }
}



