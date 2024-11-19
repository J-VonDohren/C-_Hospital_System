namespace Assignment2FinalDesign
{
    internal class Program
    {
        public static bool IsOnMenu = true;
        public static bool IsOn2ndMenu = false;
        public static Menu CurrentMenu = new(); // still has some static menus so remove the statics
        static void Main(string[] args)
        {
            Console.WriteLine("=================================");
            Console.WriteLine("Welcome to Gardens Point Hospital");
            Console.WriteLine("=================================");

            // this is the initial menu
            while (IsOnMenu == true)
            {
                Menu.InitialiseMenu();

                switch (Console.ReadLine())
                {
                    // this case lets the user go through the log in process
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
                        User.UsersList[User.ActiveUserIndex].AuthenticateEmail();
                        if (User.EmailIsAuthenticated == false)
                        {
                            break;
                        }
                        if (User.EmailIsAuthenticated == true)
                        {
                            User.UsersList[User.ActiveUserIndex].AuthenticatePassword();
                        }
                        if (User.PasswordIsAuthenticated == false)
                        {
                            break;
                        }
                        while (User.UsersList[User.ActiveUserIndex].IsLoggedIn == true)
                        {
                            CurrentMenu.UserOptionMenu();
                        }
                        break;
                    // this case brings the user to the User registration menu
                    case "2":
                        IsOn2ndMenu = true;
                        while (IsOn2ndMenu == true)
                        {
                            Menu.RegisterUserMenu();
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
