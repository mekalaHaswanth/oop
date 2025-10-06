
using Online_Shop;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main()
    {
        Console.WriteLine("WELCOME TO THE ONLINE SHOP");
        Console.WriteLine("--------------------------");
        
        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. Customer");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    List<Admin> admins = Admin.CreateSampleAdmin();
                    AdminLoginMenu(admins);
                    break;
                case "2":
                    //Customer.ManageShoppingBasket();
                    break;
                case "3":
                    Console.WriteLine("Exiting application...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

        // Create a new user
        User user = new User(1, "JohnDoe", "password123", "johndoe@example.com", "9876543210", "123 Main St", "Metropolis");

        // Display user details
        user.DisplayUserDetails();

        // Update user profile
        user.UpdateProfile("JaneDoe", "janedoe@example.com", "1234567890", "456 Elm St", "Gotham");

        // Display updated user details
        user.DisplayUserDetails();

        // Log out
        user.LogOut();
    }
    static void AdminLoginMenu(List<Admin> admins)
    {
        Console.WriteLine("\n--- Admin Login ---");
        Console.Write("Enter Username: ");
        string inputUserName = Console.ReadLine();

        Console.Write("Enter Password: ");
        string inputPassword = Console.ReadLine();

        // Validate credentials
        var matchingAdmin = admins.FirstOrDefault(admin =>
            admin.UserName.Equals(inputUserName, StringComparison.OrdinalIgnoreCase) &&
            admin.Password == inputPassword);

        if (matchingAdmin != null)
        {
            Console.WriteLine($"Login successful! Welcome, {matchingAdmin.UserName}.");
            // Proceed to admin functionalities
            ShowAdminDashboard(matchingAdmin);
        }
        else
        {
            Console.WriteLine("Invalid username or password. Please try again.");
        }
    }
     static void ShowAdminDashboard(Admin matchingAdmin)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Login successful! Welcome to ADMIN PAGE");
            Console.WriteLine("\n1. Add New Products");
            Console.WriteLine("2. Remove Products");
            Console.WriteLine("3. Update Products");
            Console.WriteLine("4. View Products");
            Console.WriteLine("5. Add Product Category");
            Console.WriteLine("6. Update Product Category");
            Console.WriteLine("7. View Product Categories");
            Console.WriteLine("8. Log Out");
            Console.Write("\nChoose an option: ");
            Console.Write("");
            string select = Console.ReadLine();
            string filePath = @"C:\Users\M  Haswanth Reddy\Desktop\new\New folder\Online_Shop\data\Products.csv";
            // Path to your CSV file for categories
            string filePath1 = @"C:\Users\M  Haswanth Reddy\Desktop\new\New folder\Online_Shop\data\Category.csv";

            switch (select)
            {
                case "1":
                    AddNewProduct(matchingAdmin, filePath);
                    break;
                case "2":
                    RemoveProduct(matchingAdmin, filePath);
                    break;
                case "3":
                    UpdateProduct(matchingAdmin, filePath);
                    break;
                case "4":
                    Product.TestProductLoading(filePath);
                    break;
                case "5":
                    AddProductCategory(matchingAdmin, filePath1);
                    break;
                case "6":
                    UpdateProductCategory(matchingAdmin, filePath1);
                break;
                case "7":
                    Category.TestCategoryLoading(filePath1);
                    break;
                case "8":
                    Console.WriteLine("Logging Out...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            Console.Clear(); // Clear console for a clean UI on next action
        }

    }
    static void AddNewProduct(Admin admin, string productFilePath)
    {
        Console.WriteLine("\n--- Add New Product ---");

        try
        {
            // Gather product details from the user
            Console.Write("Enter Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Enter Product Name: ");
            string productName = Console.ReadLine();

            Console.Write("Enter Product Description: ");
            string productDescription = Console.ReadLine();

            Console.Write("Enter Product Price: ");
            double productPrice = double.Parse(Console.ReadLine());

            Console.Write("Enter Stock Quantity: ");
            int stockQuantity = int.Parse(Console.ReadLine());

            Console.Write("Enter Category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            // Call the Admin method to save the product directly to the file
            admin.AddProductToFile(productId, productName, productDescription, productPrice, stockQuantity, categoryId, productFilePath);

            Console.WriteLine("New product added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding product: {ex.Message}");
        }
    }


    static void RemoveProduct(Admin admin, string productFilePath)
    {
        Console.WriteLine("\n--- Remove Product ---");

        try
        {
            // Prompt the admin for the product ID to remove
            Console.Write("Enter Product ID to remove: ");
            int productId = int.Parse(Console.ReadLine());

            // Call the Admin method to remove the product
            admin.RemoveProductFromFile(productId, productFilePath);
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a valid numeric Product ID.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error removing product: {ex.Message}");
        }
    }

    static void UpdateProduct(Admin admin, string productFilePath)
    {
        Console.WriteLine("\n--- Update Product ---");

        try
        {
            // Prompt the admin for the product ID to update
            Console.Write("Enter Product ID to update: ");
            int productId = int.Parse(Console.ReadLine());

            // Gather updated product details
            Console.Write("Enter New Product Name (or press Enter to keep current): ");
            string name = Console.ReadLine();

            Console.Write("Enter New Description (or press Enter to keep current): ");
            string description = Console.ReadLine();

            Console.Write("Enter New Price (or press Enter to keep current): ");
            string priceInput = Console.ReadLine();
            double price = string.IsNullOrEmpty(priceInput) ? -1 : double.Parse(priceInput);

            Console.Write("Enter New Stock Quantity (or press Enter to keep current): ");
            string stockInput = Console.ReadLine();
            int stock = string.IsNullOrEmpty(stockInput) ? -1 : int.Parse(stockInput);

            Console.Write("Enter New Category ID (or press Enter to keep current): ");
            string categoryInput = Console.ReadLine();
            int category = string.IsNullOrEmpty(categoryInput) ? -1 : int.Parse(categoryInput);

            // Load the current list of products
            var products = Product.LoadProductsFromFile(productFilePath);
            var productToUpdate = products.FirstOrDefault(p => p.ProductId == productId);

            if (productToUpdate != null)
            {
                // Update product details
                productToUpdate.Name = string.IsNullOrEmpty(name) ? productToUpdate.Name : name;
                productToUpdate.Description = string.IsNullOrEmpty(description) ? productToUpdate.Description : description;
                productToUpdate.Price = price < 0 ? productToUpdate.Price : price;
                productToUpdate.StockQuantity = stock < 0 ? productToUpdate.StockQuantity : stock;
                productToUpdate.CategoryId = category < 0 ? productToUpdate.CategoryId : category;

                // Call admin method to save the changes
                admin.UpdateProductInFile(productId, productFilePath, productToUpdate);
            }
            else
            {
                Console.WriteLine($"Product with ID {productId} not found.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter valid data.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating product: {ex.Message}");
        }
    }

    static void AddProductCategory(Admin admin, string categoryFilePath)
    {
        Console.WriteLine("\n--- Add Product Category ---");

        Console.Write("Enter Category ID: ");
        int categoryId = int.Parse(Console.ReadLine());

        Console.Write("Enter Category Name: ");
        string categoryName = Console.ReadLine();

        Console.Write("Enter Category Description: ");
        string categoryDescription = Console.ReadLine();

        // Call Admin's method to save the category directly to the file
        admin.AddCategoryToFile(categoryId, categoryName, categoryDescription, categoryFilePath);
    }



    // Method to save the categories to the CSV file
    static void SaveCategoriesToFile(List<Category> categories, string filePath)
    {
        try
        {
            // Open the file in append mode to add new categories
            using (var writer = new StreamWriter(filePath, append: true))
            {
                foreach (var category in categories)
                {
                    writer.WriteLine($"{category.CategoryId},{category.Name},{category.Description}");
                }
            }
            Console.WriteLine("Categories saved to file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving categories: {ex.Message}");
        }
    }

    static void UpdateProductCategory(Admin admin, string categoryFilePath)
    {
        Console.WriteLine("\n--- Update Product Category ---");

        // Get the ID of the category to update
        Console.Write("Enter Category ID to Update: ");
        if (!int.TryParse(Console.ReadLine(), out int categoryId))
        {
            Console.WriteLine("Invalid Category ID. Please enter a number.");
            return;
        }

        // Get new details for the category
        Console.Write("Enter New Category Name (leave blank to keep current): ");
        string newName = Console.ReadLine();

        Console.Write("Enter New Category Description (leave blank to keep current): ");
        string newDescription = Console.ReadLine();

        // Call the Admin method to update the category in the file
        admin.UpdateCategoryInFile(categoryId, newName, newDescription, categoryFilePath);
    }





}
