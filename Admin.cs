//Dylan Lees B00958334

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop
{
    public class Admin : User
    {    // Private list to store products and users (assuming you are managing them)
        private List<Product> products;
        private List<User> users;
        private List<Category> categories;

        // Constructor that initializes the admin user and the products/users list
        public Admin(int userId, string userName, string password, string email, string phoneNumber, string street, string city)
            : base(userId, userName, password, email, phoneNumber, street, city)
        {
            products = new List<Product>();
            users = new List<User>();
            categories = new List<Category>();
        }

        // Method to add Admin
        public static List<Admin> CreateSampleAdmin()
        {
            List<Admin> admins = new List<Admin>
            {
                new Admin(1, "Admin1", "Pass123", "admin1@example.com", "111-111-1111", "123 Admin St", "CityOne"),
                new Admin(2, "Admin2", "Pass456", "admin2@example.com", "222-222-2222", "456 Admin Blvd", "CityTwo"),
                new Admin(3, "SuperAdmin", "SuperPass", "superadmin@example.com", "333-333-3333", "789 Admin Ave", "CityThree")
            };

            Console.WriteLine("Sample Admins Created:");
            foreach (var admin in admins)
            {
                Console.WriteLine($"Username: {admin.UserName}, Email: {admin.Email}");
            }

            return admins;
        }
        public void AddCategory(Category category)
        {
            categories.Add(category);
        }

        // Helper method to append the product data to the CSV file
        public void AddProductToFile(int productId, string name, string description, double price, int stock, int categoryId, string filePath)
        {
            try
            {
                // Append the new product to the CSV file
                using (var writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine($"{productId},{name},{description},{price},{stock},{categoryId}");
                }
                Console.WriteLine("Product added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving product: {ex.Message}");
            }
        }


        // Method to remove a product from the list of products by product ID
        public void RemoveProductFromFile(int productId, string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath).ToList();

                    // Skip the header and remove the line with the matching product ID
                    var header = lines[0];
                    var remainingLines = lines.Skip(1)
                                              .Where(line => !line.StartsWith($"{productId},"))
                                              .ToList();

                    if (remainingLines.Count == lines.Count - 1)
                    {
                        // Product was removed
                        remainingLines.Insert(0, header); // Add the header back
                        File.WriteAllLines(filePath, remainingLines);
                        Console.WriteLine("Product removed successfully!");
                    }
                    else
                    {
                        Console.WriteLine($"Product with ID {productId} not found.");
                    }
                }
                else
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing product: {ex.Message}");
            }
        }

        // Method to update a product in the list of products by product ID
        public void UpdateProductInFile(int productId, string filePath, Product updatedProduct)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath).ToList();
                    var header = lines[0];
                    var updated = false;

                    // Process each line except the header
                    for (int i = 1; i < lines.Count; i++)
                    {
                        var columns = lines[i].Split(',');
                        if (int.TryParse(columns[0], out int currentId) && currentId == productId)
                        {
                            // Update the product details
                            lines[i] = $"{updatedProduct.ProductId},{updatedProduct.Name},{updatedProduct.Description}," +
                                       $"{updatedProduct.Price:F2},{updatedProduct.StockQuantity},{updatedProduct.CategoryId}";
                            updated = true;
                            break;
                        }
                    }

                    if (updated)
                    {
                        lines.Insert(0, header); // Ensure header is at the top
                        File.WriteAllLines(filePath, lines);
                        Console.WriteLine("Product updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine($"Product with ID {productId} not found.");
                    }
                }
                else
                {
                    Console.WriteLine($"File not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product: {ex.Message}");
            }
        }

        // Method to manage users (in this case, add them to the list)
        public void ManageUsers(User user)
        {
            users.Add(user);
            Console.WriteLine($"User {user.UserName} managed by admin.");
        }

        // Method to display the products added by the admin
        public void DisplayProducts()
        {
            Console.WriteLine("Products managed by Admin:");
            foreach (var product in products)
            {
                product.DisplayProduct();
            }
        }

        // Method to display users managed by the admin
        public void DisplayUsers()
        {
            Console.WriteLine("Users managed by Admin:");
            foreach (var user in users)
            {
                user.DisplayUserDetails();
            }
        }
        public static void SaveProductsToFile(List<Product> products, string filePath)
        {
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    // Write the header row
                    writer.WriteLine("ProductId,Name,Description,Price,StockQuantity,CategoryId");

                    // Write each product
                    foreach (var product in products)
                    {
                        writer.WriteLine($"{product.ProductId},{product.Name},{product.Description},{product.Price},{product.StockQuantity},{product.CategoryId}");
                    }
                }
                Console.WriteLine("Product file updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
            }
        }
        // Method to add a category directly to the file
        public void AddCategoryToFile(int categoryId, string name, string description, string filePath)
        {
            try
            {
                // Append the new category to the CSV file
                using (var writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine($"{categoryId},{name},{description}");
                }
                Console.WriteLine("Category added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving category: {ex.Message}");
            }
        }

        // Method to update a category in the file
        public void UpdateCategoryInFile(int categoryId, string newName, string newDescription, string filePath)
        {
            try
            {
                // Read all categories from the file
                var categories = new List<Category>();

                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath);

                    foreach (var line in lines)
                    {
                        var values = line.Split(',');
                        if (values.Length >= 3 && int.TryParse(values[0], out int id))
                        {
                            var category = new Category
                            {
                                CategoryId = id,
                                Name = values[1].Trim(),
                                Description = values[2].Trim()
                            };

                            // Update the matching category
                            if (id == categoryId)
                            {
                                category.Name = newName;
                                category.Description = newDescription;
                            }

                            categories.Add(category);
                        }
                    }
                }

                // Write updated categories back to the file
                using (var writer = new StreamWriter(filePath, append: false))
                {
                    foreach (var category in categories)
                    {
                        writer.WriteLine($"{category.CategoryId},{category.Name},{category.Description}");
                    }
                }

                Console.WriteLine("Category updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating category: {ex.Message}");
            }
        }


    }
}
