using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class Product
{
    // Private fields
    private int productId;
    private string name;
    private string description;
    private double price;
    private int stockQuantity;
    private int categoryId;

    // Public properties
    public int ProductId
    {
        get { return productId; }
        set { productId = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public double Price
    {
        get { return price; }
        set
        {
            if (value >= 0)
                price = value;
            else
                throw new ArgumentException("Price cannot be negative.");
        }
    }

    public int StockQuantity
    {
        get { return stockQuantity; }
        set
        {
            if (value >= 0)
                stockQuantity = value;
            else
                throw new ArgumentException("Stock quantity cannot be negative.");
        }
    }

    public int CategoryId
    {
        get { return categoryId; }
        set { categoryId = value; }
    }

    // Default constructor
    public Product()
    {
        ProductId = 0;
        Name = "Unknown";
        Description = "No description available";
        Price = 0.0;
        StockQuantity = 0;
        CategoryId = 0;
    }

    // Parameterised constructor
    public Product(int productId, string name, string description, double price, int stockQuantity, int categoryId)
    {
        this.ProductId = productId;
        this.Name = name;
        this.Description = description;
        this.Price = price;
        this.StockQuantity = stockQuantity;
        this.CategoryId = categoryId;
    }

    // Method to display product details
    public void DisplayProduct()
    {
        Console.WriteLine(string.Format("{0,-5} {1,-20} {2,-40} £{3,-10:F2} {4,-10} {5,-10}",
                                             ProductId, Name, Description, Price, StockQuantity, CategoryId));
    }

    // Static method to load products from CSV
    public static List<Product> LoadProductsFromFile(string filePath)
    {
        var products = new List<Product>();

        if (File.Exists(filePath))
        {
            try
            {
                var lines = File.ReadAllLines(filePath);

                // Skip the header row and process each product row
                for (int i = 1; i < lines.Length; i++)
                {
                    var values = lines[i].Split(',');

                    if (values.Length >= 6 &&
                        int.TryParse(values[0], out int id) &&
                        double.TryParse(values[3].Replace("£", "").Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, out double price) &&
                        int.TryParse(values[4], out int stock) &&
                        int.TryParse(values[5], out int category))
                    {
                        string name = values[1].Trim();
                        string description = values[2].Trim();

                        products.Add(new Product(id, name, description, price, stock, category));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the file: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
        }

        return products;
    }

    // Test method to load and display products
    public static void TestProductLoading(string filePath)
    {
        var products = LoadProductsFromFile(filePath);

        if (products.Count > 0)
        {
            Console.WriteLine(" Product List ");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"ID",-5} {"Name",-20} {"Description",-40} {"Price",-10} {"Stock",-10} {"Category",-10}");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");

            foreach (var product in products)
            {
                product.DisplayProduct();
            }
        }
        else
        {
            Console.WriteLine("No products were loaded. Check your CSV file.");
        }
    }
}


