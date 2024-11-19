using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;


namespace Assignment2Hospital
{
    internal class Program
    {
        public static bool IsOnMenu = true;
        public static bool IsOn2ndMenu = true;

        public static void Main(string[] args)
        {
            Console.WriteLine("=================================");
            Console.WriteLine("Welcome to Gardens Point Hospital");
            Console.WriteLine("=================================");

            // create the initial starting menu and when IsOnMenu becomes false exits the loop which stops new iterations of the menu
            while (IsOnMenu == true)
            {
                IsOn2ndMenu = true;
                Console.WriteLine();
                MenuChildClass.InitialiseMenu();
                switch (Console.ReadLine())
                {
                    // this case is for Users that are already registered to Log In and use their dedicated methods
                    case "1":
                        Console.WriteLine();
                        Console.WriteLine("Login Menu.");
                        if (User.UsersList.Count == 0)
                        {
                            Console.WriteLine("#####");
                            Console.WriteLine("#Error - There are no people registered.");
                            Console.WriteLine("#####");
                            break;
                        }
                        User.Authenticate();
                        if (IUser.IsAuthenticated == false)
                        {
                            break;
                        }
                        User.FindActiveUser();
                        while (IUser.IsLoggedIn == true)
                        {
                            MenuChildClass.UserOptionMenu();
                        }
                        break;
                    // this case is for registering users
                    case "2":
                        while (IsOn2ndMenu == true)
                        {
                            MenuChildClass.RegisterUserMenu();
                        }
                        break;
                    // this case sets the IsOnMenu to false to exit the loop thus exiting the menu
                    case "3":
                        Console.WriteLine("Goodbye. Please stay safe.");
                        IsOnMenu = false;
                        break;
                    // this default case makes any other input invalid and will restart the loop after providing an error message
                    default:
                        Console.WriteLine("#####");
                        Console.WriteLine("#Error - Invalid Menu Option, please try again.");
                        Console.WriteLine("#####");
                        break;
                }
            }



        }



    }
}

