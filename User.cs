//Dylan Lees B00958334

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop
{
    public class User
    {
        // Private fields
        private int userId;
        private string userName;
        private string password;
        private string email;
        private string phoneNumber;
        private string street;
        private string city;

        // Public properties
        public int UserId
        {
            get { return userId; }
            private set { userId = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length >= 6)
                    password = value;
                else
                    throw new ArgumentException("Password must be at least 6 characters long.");
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (value.Contains("@") && value.Contains("."))
                    email = value;
                else
                    throw new ArgumentException("Invalid email format.");
            }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set {phoneNumber = value;}
        }

        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        // Parameterized constructor
        public User(int userId, string userName, string password, string email, string phoneNumber, string street, string city)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Street = street;
            this.City = city;
        }

        // Method to update profile
        public void UpdateProfile(string newUserName, string newEmail, string newPhoneNumber, string newStreet, string newCity)
        {
            this.UserName = newUserName;
            this.Email = newEmail;
            this.PhoneNumber = newPhoneNumber;
            this.Street = newStreet;
            this.City = newCity;

            Console.WriteLine("Profile updated successfully.");
        }

        // Method to log out
        public void LogOut()
        {
            Console.WriteLine($"{UserName} has logged out successfully.");
        }

        // Method to display user details
        public void DisplayUserDetails()
        {
            Console.WriteLine("User Profile:");
            Console.WriteLine($"User ID: {UserId}");
            Console.WriteLine($"Name: {UserName}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Phone Number: {PhoneNumber}");
            Console.WriteLine($"Street: {Street}");
            Console.WriteLine($"City: {City}");
        }
    }
       
}

