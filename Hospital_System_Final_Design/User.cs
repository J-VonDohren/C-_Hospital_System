namespace Assignment2FinalDesign
{
    public abstract class User
    {
        public static int ActiveUserIndex;
        public const string DateFormat = "HH:mm dd/MM/yyyy";
        public static List<User> UsersList = new List<User>();
        public string? AuthenticatedEmail;
        public string? AuthenticatedPassword;
        public User()
        {
            UsersList.Add(this);
        }
        public User(string Name, string EmailAddress, string MobileNumber, string Password)
        {
            this.Name = Name;
            this.EmailAddress = EmailAddress;
            this.MobileNumber = MobileNumber;
            this.Password = Password;
            EmailsActive.Add(this.EmailAddress);
        }
        /// <summary>
        /// The following Code Blocks are used to control the User input when creating an object
        /// </summary>
        private string? name;
        public string Name
        {
            get
            {
                return name ?? "";
            }
            set
            {
                // create an if statement to restrict the users name to containing at least 1 letter and having at least 1 space
                if ((value.Any(char.IsLetter) == true && value.Contains(" ")) || value.Any(char.IsLetter))
                {
                    name = value;
                }
                else
                {
                    throw new ArgumentException(String.Format($"#####{Environment.NewLine}#Error - Supplied name is invalid, please try again.{Environment.NewLine}#####", name));
                }
            }
        }
        private int age;
        public virtual int Age // the public Age property is set to a virtual property as each User Type should have their own Age limit which overrides the blank setter
        {
            get
            {
                return age;
            }
            set
            {
            }
        }
        private string? emailAddress;
        public string EmailAddress
        {
            get
            {
                return emailAddress ?? "";
            }
            set
            {
                // create an if statement that checks the @ is in valid position
                if (value.Contains("@") == true && (value.EndsWith("@") == false && value.StartsWith("@") == false && EmailsActive.Contains(value) == false))
                {
                    emailAddress = value;
                }
                else if (value.Contains("@") == true && (value.EndsWith("@") == false && value.StartsWith("@") == false && EmailsActive.Contains(value) == true))
                {
                    throw new ArgumentException(String.Format($"#####{Environment.NewLine}#Error - Email is already registered, please try again.{Environment.NewLine}#####", EmailAddress));
                }
                else
                {
                    throw new ArgumentException(String.Format($"#####{Environment.NewLine}#Error - Supplied email is invalid, please try again.{Environment.NewLine}#####", EmailAddress));
                }
            }
        }
        // mobile number stored as string instead of int because int wont let you start with a 0
        private string? mobileNumber;
        public string MobileNumber
        {
            get
            {
                return mobileNumber ?? "";
            }
            set
            {
                // create an if statement that checks the phone number is a digit and contains 10 digits
                if (value.All(char.IsDigit) && value.Length == 10 && value.StartsWith('0'))
                {
                    mobileNumber = value;
                }
                else
                {
                    throw new ArgumentException(String.Format($"#####{Environment.NewLine}#Error - Supplied mobile number is invalid, please try again.{Environment.NewLine}#####", MobileNumber));
                }
            }
        }
        private string? password;
        public string Password
        {
            get
            {
                return password ?? "";
            }
            set
            {
                // create an if statement that makes sure the password is only comprised of letter and digits and has a minimum length of 8 characters
                if (value.All(char.IsLetterOrDigit) && value.Any(char.IsDigit) && value.Length >= 8 && (value.Any(char.IsUpper) && value.Any(char.IsLower)))
                {
                    password = value;
                }
                else
                {
                    throw new ArgumentException(String.Format($"#####{Environment.NewLine}#Error - Supplied password is invalid, please try again.{Environment.NewLine}#####", Password));
                }
            }
        }
        private int? roomNumber;
        public int? RoomNumber
        {
            get
            {
                return roomNumber;
            }
            set
            {
                // create an if statement that restricts the number of rooms to 10 with a minimum room number of 1
                if (value <= 10 && value >= 1 || value == null)
                {
                    roomNumber = value;
                }
                else
                {
                    throw new ArgumentException(String.Format($"#####{Environment.NewLine}#Error - Supplied room number is invalid, please try again.{Environment.NewLine}#####", RoomNumber));
                }
            }
        }
        private int? floorNumber;
        public int? FloorNumber
        {
            get
            {
                return floorNumber;
            }
            set
            {
                //create an if statement that restricts the number of floors to 6
                if (value <= 6 && value >= 1 || value == null)
                {
                    floorNumber = value;
                }
                else
                {
                    throw new ArgumentException(String.Format($"#####{Environment.NewLine}#Error - Supplied floor is invalid, please try again.{Environment.NewLine}#####", FloorNumber));
                }
            }
        }

        // declaring all variables necessary for thee login and authentication process
        public bool IsLoggedIn = false;
        public bool IsAuthenticated = false;
        public static bool EmailIsAuthenticated = false;
        public static bool PasswordIsAuthenticated = false;

        /// <summary>
        /// the following Code blocks are used to store relevant data for all Users
        /// </summary>

        // this list is to keep track of the emails currently being used to ensure an email is not used again
        public static List<string?> EmailsActive = [];
        public void AuthenticateEmail()
        {
            do
            {
                Console.WriteLine("Please enter in your email:");
                AuthenticatedEmail = Console.ReadLine() ?? "";
                if (EmailsActive.Contains(AuthenticatedEmail) == false)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine($"#Error - Email is not registered.");
                    Console.WriteLine("#####");
                    break;
                }
                else
                {
                    EmailIsAuthenticated = true;
                    break;
                }
            }
            while (EmailIsAuthenticated == false);
        }
        public void AuthenticatePassword() 
        {
            foreach (var CurrentUser in UsersList)
            {
                int i = 1;
                if (CurrentUser.EmailAddress == AuthenticatedEmail)
                {
                    Console.WriteLine("Please enter in your password:");
                    AuthenticatedPassword = Console.ReadLine() ?? "";
                    if (CurrentUser.Password == AuthenticatedPassword)
                    {
                        PasswordIsAuthenticated = true;
                        CurrentUser.IsAuthenticated = true;
                        CurrentUser.IsLoggedIn = true;
                        Console.WriteLine($"Hello {CurrentUser.Name} welcome back.");
                        ActiveUserIndex = UsersList.IndexOf(CurrentUser);
                        break;
                    }
                    else if (i == UsersList.Count && CurrentUser.Password != AuthenticatedPassword)
                    {
                        PasswordIsAuthenticated = false;
                        EmailIsAuthenticated = false;
                        Console.WriteLine("#####");
                        Console.WriteLine($"#Error - Wrong Password.");
                        Console.WriteLine("#####");
                        break;
                    }
                }
                else 
                {
                    i++;
                }
            }
        }
        public void ChangePassword()
        {
            Console.WriteLine("Enter new password:");
            string password = Console.ReadLine() ?? "";
            UsersList[ActiveUserIndex].Password = password;
            Console.WriteLine($"Password has been changed.");
        }
    }
}

