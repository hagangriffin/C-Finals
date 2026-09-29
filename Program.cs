using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinalsProject
{
    public class BranchCombineTest
    {
        public void TestMethod()
        {
            Console.WriteLine("This is a test method for branch combine.");
        }
    }
    public class User
    {
        public virtual void Login()
        {
            String username = "";
            String password = "";

            Console.WriteLine("Enter username: ");
            String inputUsername = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            String inputPassword = Console.ReadLine();

            if (inputUsername == username && inputPassword == password)
            {
                Console.WriteLine("Login successful");
            }
            else
            {
                Console.WriteLine("Login failed");
            }
        }
    }

    public abstract class Admin : User
    {
        public abstract void AddUser();
        public abstract void RemoveUser();

        // Other methods and properties specific to Admin can be added here

    }

    public sealed class AddAsset
    {
        // Implementation of AddAsset class
    }
    public sealed class RemoveAsset
    {
        // Implementation of RemoveAsset class
    }

    public class UserInterface
    {
        public void DisplayMenu()
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Add Asset");
            Console.WriteLine("3. Remove Asset");
            // Add more menu options as needed
            Console.WriteLine("4. Exit");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    User user = new User();
                    user.Login();
                    break;
                case 2:
                    // Call method to add asset
                    break;
                case 3:
                    // Call method to remove asset
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
