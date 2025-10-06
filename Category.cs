using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop
{
    public class Category
    {
        // Private fields
        private int categoryId;
        private string name;
        private string description;

        // Public properties
        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
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

        // Default constructor
        public Category()
        {
            CategoryId = 0;
            Name = "Unknown";
            Description = "No description available";
        }

        // Parameterized constructor
        public Category(int categoryId, string name, string description)
        {
            this.CategoryId = categoryId;
            this.Name = name;
            this.Description = description;
        }

        // Method to display category details
        public void DisplayCategory()
        {
            Console.WriteLine(string.Format("{0,-10} {1,-20} {2,-50}",
                                                 CategoryId, Name, Description));
        }

        // Static method to load categories from CSV
        public static List<Category> LoadCategoriesFromFile(string filePath)
        {
            var categories = new List<Category>();

            if (File.Exists(filePath))
            {
                try
                {
                    var lines = File.ReadAllLines(filePath);

                    // Skip the header row and process each category row
                    for (int i = 1; i < lines.Length; i++)
                    {
                        var values = lines[i].Split(',');

                        if (values.Length >= 3 &&
                            int.TryParse(values[0], out int id))
                        {
                            string name = values[1].Trim();
                            string description = values[2].Trim();

                            categories.Add(new Category(id, name, description));
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

            return categories;
        }

        // Test method to load and display categories
        public static void TestCategoryLoading(string filePath)
        {
            var categories = LoadCategoriesFromFile(filePath);

            if (categories.Count > 0)
            {
                Console.WriteLine(" Category List ");
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine($"{"ID",-10} {"Name",-20} {"Description",-50}");
                Console.WriteLine("-------------------------------------------------------------");

                foreach (var category in categories)
                {
                    category.DisplayCategory();
                }
            }
            else
            {
                Console.WriteLine("No categories were loaded. Check your CSV file.");
            }
        }
    }
}
