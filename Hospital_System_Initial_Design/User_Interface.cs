using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Hospital
{
    public interface IUser
    {

        private static string? userType;
        public static string UserType
        {
            get
            {
                return userType ?? "";
            }
            set
            {
                userType = value;
            }
        }

        private static int? staffID;
        public static int StaffID
        {
            get
            {
                return staffID ?? -1;
            }
            set
            {
                if (value >= 100 && value <= 999 && (User.staffIDsActive.Contains(value) == false))
                {
                    staffID = value;
                }
                else if (value >= 100 && value <= 999 && (User.staffIDsActive.Contains(value) == true))
                {
                    staffID = -2;
                }
                else
                {
                    staffID = -1;
                }
            }
        }
        private static string? name;
        public static string Name
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
                    name = "";
                }
            }
        }
        private static int age;
        public static int Age
        {
            get
            {
                return age;
            }
            set
            {
                
                // creates if statements that retrict the users age based on their User Type
                if (value >= 0 && value <= 100 && IUser.UserType == "Patient")
                {
                    age = value;
                }
                else if (value >= 21 && value <= 70 && IUser.UserType == "Floor manager")
                {
                    age = value;
                }
                else if (value >= 30 && value <= 75 && IUser.UserType == "Surgeon")
                {
                    age = value;
                }
                else
                {
                    age = -1;
                }
            }
        }
        private static string? emailAddress;
        public static string EmailAddress
        {
            get
            {
                return emailAddress ?? "";
            }
            set
            {
                // create an if statement that checks the @ is in valid position
                if (value.Contains("@") == true && (value.EndsWith("@") == false && value.StartsWith("@") == false) && User.EmailsActive.Contains(value) == false)
                {
                    emailAddress = value;
                }
                else if (value.Contains("@") == true && (value.EndsWith("@") == false && value.StartsWith("@") == false) && User.EmailsActive.Contains(value) == true)
                {
                    emailAddress = "-1";
                }
                else
                {
                    emailAddress = "";
                }
            }
        }
        // mobile number stored as string instead of int because int wont let you start with a 0
        private static string? mobileNumber;
        public static string MobileNumber
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
                    mobileNumber = "";
                }
            }
        }
        private static string? password;
        public static string Password
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
                    password = "";
                }
            }
        }
        private static int roomNumber;
        public static int RoomNumber
        {
            get
            {
                return roomNumber;
            }
            set
            {
                // create an if statement that restricts the number of rooms to 10 with a minimum room number of 1
                if (value <= 10 && value >= 1)
                {
                    roomNumber = value;
                }
                else
                {
                    roomNumber = -1;
                }
            }
        }
        
        private static int floorNumber;
        public static int FloorNumber
        {
            get
            {
                return floorNumber;
            }
            set
            {
                //create an if statement that restricts the number of floors to 6
                if (value <= 6 && value >= 1 && User.FloorsManaged.Contains(value) == false)
                {
                    floorNumber = value;
                }
                else if (value <= 6 && value >= 1 && User.FloorsManaged.Contains(value) == true)
                {
                    floorNumber = -2;
                }
                else
                {
                    floorNumber = -1;
                }
            }
        }
        public static string[] possibleSpecialities = ["General Surgeon", "Orthopaedic Surgeon", "Cardiothoracic Surgeon", "Neurosurgeon", ""];
        private static string? speciality;
        public static string Speciality
        {
            get
            {
                return speciality ?? "";
            }
            set
            {
                if (possibleSpecialities.Contains(value))
                {
                    speciality = value;
                }
                else
                {
                    speciality = "";
                }
            }
        }


        //set IsLoggedIn to false because at the start the user shouldn't be logged in
        public static bool IsLoggedIn = false;
        public static bool IsAuthenticated = false;
        public static bool EmailIsAuthenticated = false;
        public static bool PasswordIsAuthenticated = false;

        //set IsCheckedIn to false because at the start the patient shouldn't be checked in
        public static bool IsCheckedIn = false;

        

    }
}
