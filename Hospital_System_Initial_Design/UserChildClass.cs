using System;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assignment2Hospital
{
    class User : IUser
    {
        public const string DateFormat = "HH:mm dd/MM/yyyy";
        public static List<int> FloorsManaged = new List<int>() ?? [];
        public static User ActiveUser = new();
        public static User SurgeonChosen = new();
        public static User PatientChosen = new();
        public static string managersChoice = "";
        public bool IsCheckedIn;
        public string? UserType;
        public string? EmailAddress;
        public string? Password;
        public string? Name;
        public int? Age;
        public int? StaffID;
        public string? Speciality;
        public int FloorNumber;
        public int RoomNumber;
        public string? MobileNumber;
        public string? CurrentSurgeon;
        public string? SurgeryTime;
        public string[]? bookedSurgeryTimes;
        public bool? HadSurgery;
        public int? RoomsLeftToAssign;
        public bool IsAuthenticated = false;
        public static List<User> UsersList = new List<User>() ?? [];
        public static List<int> staffIDsActive = new List<int>() ?? [];
        public static List<string> EmailsActive = new List<string>() ?? [];
        public static List<User> CheckedInPatientsList = new List<User>() ?? [];
        public static List<User> StaffedSurgeons = new List<User>() ?? [];
        public static List<string> OptionsList = new List<string>() ?? [];
        public static List<User> PatientsAssignedRooms = new List<User>() ?? [];
        public static List<User> RegisteredPatientList = new List<User>() ?? [];
        public static List<User> RegisteredFloorManagerList = new List<User>() ?? [];
        public List<int> FloorRoomsAssigned = [];
        public Dictionary<DateTime, string> SurgerySchedule = [];
        public static List<User> PatientHadSurgeryList = new List<User>() ?? [];
        public static List<User> PatientsHadSurgeryAssignedToManager = new List<User>() ?? [];
        public List<User> PatientsAssignedToSurgeon = [];


        // create an overloaded Constructor that can construct a Patient, a surgeon, or a manager

        public User()
        {

        }
        public User(string Name, int Age, string EmailAddress, string MobileNumber, string Password, string UserType, bool IsCheckedIn, int RoomNumber, int FloorNumber, string CurrentSurgeon, string SurgeryTime, bool HadSurgery)
        {
            this.UserType = UserType;
            IUser.UserType = UserType;
            this.Name = Name;
            IUser.Name = Name;
            this.Age = Age;
            IUser.Age = Age;
            this.EmailAddress = EmailAddress;
            IUser.EmailAddress = EmailAddress;
            this.MobileNumber = MobileNumber;
            IUser.MobileNumber = MobileNumber;
            this.Password = Password;
            IUser.Password = Password;
            IsAuthenticated = IUser.IsAuthenticated;
            this.IsCheckedIn = IsCheckedIn;
            this.RoomNumber = RoomNumber;
            this.FloorNumber = FloorNumber;
            this.CurrentSurgeon = CurrentSurgeon;
            this.SurgeryTime = SurgeryTime;
            this.HadSurgery = HadSurgery;



        }
        public User(string name, int age, string emailAddress, string mobileNumber, string password, string UserType, int staffID, string speciality, string[] bookedSurgeryTimes, Dictionary<DateTime, string> SurgerySchedule, List<User> PatientsAssignedToSurgeon)
        {
            this.UserType = UserType;
            IUser.UserType = UserType;
            this.Name = name;
            IUser.Name = name;
            this.Age = age;
            IUser.Age = age;
            this.EmailAddress = emailAddress;
            IUser.EmailAddress = emailAddress;
            this.MobileNumber = mobileNumber;
            IUser.MobileNumber = mobileNumber;
            this.Password = password;
            IUser.Password = password;
            this.StaffID = staffID;
            IUser.StaffID = staffID;
            this.Speciality = speciality;
            IUser.Speciality = speciality;
            this.bookedSurgeryTimes = bookedSurgeryTimes;
            this.SurgerySchedule = SurgerySchedule;
            this.PatientsAssignedToSurgeon = PatientsAssignedToSurgeon;
            IsAuthenticated = IUser.IsAuthenticated;
        }
        public User(string name, int age, string emailAddress, string mobileNumber, string password, string UserType, int staffID, int floorNumber, int RoomsLeftToAssign, List<int> FloorRoomsAssigned)
        {
            this.UserType = UserType;
            IUser.UserType = UserType;
            this.Name = name;
            IUser.Name = name;
            this.Age = age;
            IUser.Age = age;
            this.EmailAddress = emailAddress;
            IUser.EmailAddress = emailAddress;
            this.MobileNumber = mobileNumber;
            IUser.MobileNumber = mobileNumber;
            this.Password = password;
            IUser.Password = password;
            this.StaffID = staffID;
            IUser.StaffID = staffID;
            this.FloorNumber = floorNumber;
            IUser.FloorNumber = floorNumber;
            this.RoomsLeftToAssign = RoomsLeftToAssign;
            this.FloorRoomsAssigned = FloorRoomsAssigned;
            IsAuthenticated = IUser.IsAuthenticated;

        }
        public static void FindActiveUser()
        {
            foreach (User CurrentUser in UsersList)
            {
                if (CurrentUser.IsAuthenticated == true)
                {
                    User.ActiveUser = CurrentUser;
                }
                else
                {
                    continue;
                }
            }
        }
        public static void ConstructPatient()
        {
            IUser.UserType = "Patient";
            Console.WriteLine("Registering as a patient.");
            do
            {
                Console.WriteLine("Please enter in your name:");
                IUser.Name = Console.ReadLine() ?? "";
                ErrorDetection.DetectStringError(IUser.Name, "name");
            }
            while (IUser.Name == "");

            do
            {
                Console.WriteLine("Please enter in your age:");
                string age = Console.ReadLine() ?? "";
                if (age.Any(Char.IsDigit) == false && age.StartsWith("-") == false || age.Any(char.IsLetter) == true)
                {
                    IUser.Age = -1;
                    Console.WriteLine("#####");
                    Console.WriteLine($"#Error - Supplied value is not an integer, please try again.");
                    Console.WriteLine("#####");
                    continue;
                }

                IUser.Age = int.Parse(age);

                ErrorDetection.DetectIntError(IUser.Age, "age");
            }
            while (IUser.Age == -1);

            do
            {
                Console.WriteLine("Please enter in your mobile number:");
                IUser.MobileNumber = Console.ReadLine() ?? "";
                ErrorDetection.DetectStringError(IUser.MobileNumber, "mobile number");
            }
            while (IUser.MobileNumber == "");

            do
            {
                Console.WriteLine("Please enter in your email:");
                IUser.EmailAddress = Console.ReadLine() ?? "";
                ErrorDetection.DetectStringError(IUser.EmailAddress, "email");
            }
            while (IUser.EmailAddress == "" || IUser.EmailAddress == "-1");


            do
            {
                Console.WriteLine("Please enter in your password:");
                IUser.Password = Console.ReadLine() ?? "";
                ErrorDetection.DetectStringError(IUser.Password, "password");
            }
            while (IUser.Password == "");
            // sets a new patient to not checked in
            bool IsCheckedIn = false;
            int RoomNumber = -1;
            int FloorNumber = -1;
            string CurrentSurgeon = "";
            string SurgeryTime = "";
            bool HadSurgery = false;
            User User = new(IUser.Name, IUser.Age, IUser.EmailAddress, IUser.MobileNumber, IUser.Password, IUser.UserType, IsCheckedIn, RoomNumber, FloorNumber, CurrentSurgeon, SurgeryTime, HadSurgery);
            UsersList.Add(User);
            RegisteredPatientList.Add(User);
            Console.WriteLine($"{IUser.Name} is registered as a patient.");
            EmailsActive.Add(IUser.EmailAddress);
        }
        public static void ConstructFloorManager()
        {
            Console.WriteLine("Registering as a floor manager.");
            IUser.UserType = "Floor manager";

            do
            {
                Console.WriteLine("Please enter in your name:");
                IUser.Name = Console.ReadLine() ?? "";
                ErrorDetection.DetectStringError(IUser.Name, "name");
            }
            while (IUser.Name == "");

            do
            {

                Console.WriteLine("Please enter in your age:");
                string age = Console.ReadLine() ?? "-1";
                if (age.Any(Char.IsDigit) == false && age.StartsWith("-") == false || age.Any(char.IsLetter) == true)
                {
                    IUser.Age = -1;
                    Console.WriteLine("#####");
                    Console.WriteLine($"#Error - Supplied value is not an integer, please try again.");
                    Console.WriteLine("#####");
                    continue;
                }
                IUser.Age = int.Parse(age);
                ErrorDetection.DetectIntError(IUser.Age, "age");
            }
            while (IUser.Age == -1);
            do
            {
                Console.WriteLine("Please enter in your mobile number:");
                IUser.MobileNumber = Console.ReadLine() ?? "";
                ErrorDetection.DetectStringError(IUser.MobileNumber, "mobile number");
            }
            while (IUser.MobileNumber == "");

            do
            {
                Console.WriteLine("Please enter in your email:");
                IUser.EmailAddress = Console.ReadLine() ?? "";
                ErrorDetection.DetectStringError(IUser.EmailAddress, "email");
            }
            while (IUser.EmailAddress == "" || IUser.EmailAddress == "-1");


            do
            {
                Console.WriteLine("Please enter in your password:");
                IUser.Password = Console.ReadLine() ?? "";
                ErrorDetection.DetectStringError(IUser.Password, "password");
            }
            while (IUser.Password == "");
            do
            {
                Console.WriteLine("Please enter in your staff ID:");
                string staffID = Console.ReadLine() ?? "-1";
                if ((staffID.All(char.IsDigit) == false) || (staffID == ""))
                {
                    IUser.StaffID = -1;
                    Console.WriteLine("#####");
                    Console.WriteLine($"#Error - Supplied value is not an integer, please try again.");
                    Console.WriteLine("#####");
                    continue;
                }
                IUser.StaffID = int.Parse(staffID);
                ErrorDetection.DetectIntError(IUser.StaffID, "staff identification number");
            }
            while (IUser.StaffID == -1 || IUser.StaffID == -2);


            do
            {
                Console.WriteLine("Please enter in your floor number:");
                string floorNumber = Console.ReadLine() ?? "-1";
                if ((floorNumber.All(char.IsDigit) == false) || (floorNumber == ""))
                {
                    IUser.FloorNumber = -1;
                    Console.WriteLine("#####");
                    Console.WriteLine($"#Error - Supplied value is not an integer, please try again.");
                    Console.WriteLine("#####");
                    continue;
                }
                IUser.FloorNumber = int.Parse(floorNumber);
                ErrorDetection.DetectIntError(IUser.FloorNumber, "floor number");
            }
            while (IUser.FloorNumber == -1 || IUser.FloorNumber == -2);
            // rooms left to assign is set to 10 initially because there are no  rooms taken yet
            int RoomsLeftToAssign = 10;
            List<int> FloorRoomsAssigned = [];
            User User = new User(IUser.Name, IUser.Age, IUser.EmailAddress, IUser.MobileNumber, IUser.Password, IUser.UserType, IUser.StaffID, IUser.FloorNumber, RoomsLeftToAssign, FloorRoomsAssigned);
            FloorsManaged.Add(IUser.FloorNumber);
            UsersList.Add(User);
            //RegisteredFloorManagerList.Add(User);
            RegisteredFloorManagerList = UsersList.Where(user => user.UserType == "Floor manager").ToList();
            EmailsActive.Add(IUser.EmailAddress);
            staffIDsActive.Add(IUser.StaffID);
            Console.WriteLine($"{IUser.Name} is registered as a floor manager.");
        }
        public static void ConstructSurgeon()
        {
            Console.WriteLine("Registering as a surgeon.");
            IUser.UserType = "Surgeon";
            do
            {
                Console.WriteLine("Please enter in your name:");
                IUser.Name = Console.ReadLine() ?? "";
                ErrorDetection.DetectStringError(IUser.Name, "name");
            }
            while (IUser.Name == "");

            do
            {

                Console.WriteLine("Please enter in your age:");
                string age = Console.ReadLine() ?? "-1";
                if (age.Any(Char.IsDigit) == false && age.StartsWith("-") == false || age.Any(char.IsLetter) == true)
                {
                    IUser.Age = -1;
                    Console.WriteLine("#####");
                    Console.WriteLine($"#Error - Supplied value is not an integer, please try again.");
                    Console.WriteLine("#####");
                    continue;
                }
                IUser.Age = int.Parse(age);
                ErrorDetection.DetectIntError(IUser.Age, "age");
            }
            while (IUser.Age == -1);
            do
            {
                Console.WriteLine("Please enter in your mobile number:");
                IUser.MobileNumber = Console.ReadLine() ?? "";
                ErrorDetection.DetectStringError(IUser.MobileNumber, "mobile number");
            }
            while (IUser.MobileNumber == "");

            do
            {
                Console.WriteLine("Please enter in your email:");
                IUser.EmailAddress = Console.ReadLine() ?? "";
                ErrorDetection.DetectStringError(IUser.EmailAddress, "email");
            }
            while (IUser.EmailAddress == "" || IUser.EmailAddress == "-1");


            do
            {
                Console.WriteLine("Please enter in your password:");
                IUser.Password = Console.ReadLine() ?? "";
                ErrorDetection.DetectStringError(IUser.Password, "password");
            }
            while (IUser.Password == "");
            do
            {
                Console.WriteLine("Please enter in your staff ID:");
                string staffID = Console.ReadLine() ?? "-1";
                if ((staffID.All(char.IsDigit) == false) || (staffID == ""))
                {
                    IUser.StaffID = -1;
                    Console.WriteLine("#####");
                    Console.WriteLine($"#Error - Supplied value is not an integer, please try again.");
                    Console.WriteLine("#####");
                    continue;
                }
                IUser.StaffID = int.Parse(staffID);
                ErrorDetection.DetectIntError(IUser.StaffID, "staff identification number");
            }
            while (IUser.StaffID == -1 || IUser.StaffID == -2);


            MenuChildClass.RegisterSpeciality();
            string[] SurgeryTime = [];
            Dictionary<DateTime, string> SurgerySchedule = [];
            List<User> PatientsAssignedToSurgeon = [];
            User User = new User(IUser.Name, IUser.Age, IUser.EmailAddress, IUser.MobileNumber, IUser.Password, IUser.UserType, IUser.StaffID, IUser.Speciality, SurgeryTime, SurgerySchedule, PatientsAssignedToSurgeon);
            UsersList.Add(User);
            StaffedSurgeons.Add(User);
            EmailsActive.Add(IUser.EmailAddress);
            staffIDsActive.Add(IUser.StaffID);
            Console.WriteLine($"{IUser.Name} is registered as a surgeon.");
        }

        public static void CheckInorOut()
        {

            //set IsCheckedIn to true because patient is now checked in
            if (User.ActiveUser.IsCheckedIn == false && User.ActiveUser.HadSurgery == true)
            {
                Console.WriteLine("You are unable to check in at this time.");
            }
            else if (User.ActiveUser.IsCheckedIn == false && User.ActiveUser.HadSurgery == false)
            {
                User.ActiveUser.IsCheckedIn = true;
                Console.WriteLine($"Patient {User.ActiveUser.Name} has been checked in.");
            }
            else if (User.ActiveUser.IsCheckedIn == true && User.ActiveUser.HadSurgery == false)
            {
                Console.WriteLine("You are unable to check out at this time.");
            }
            else
            {
                User.ActiveUser.IsCheckedIn = false;
                Console.WriteLine($"Patient {User.ActiveUser.Name} has been checked out.");
            }
            User.UpdateUserInfo(User.ActiveUser);
        }


        public static void DisplayInfo()
        {
            //display all information for patients
            if (User.ActiveUser.UserType == "Patient")
            {

                Console.WriteLine("Your details.");
                Console.WriteLine($"Name: {User.ActiveUser.Name}");
                Console.WriteLine($"Age: {User.ActiveUser.Age}");
                Console.WriteLine($"Mobile phone: {User.ActiveUser.MobileNumber}");
                Console.WriteLine($"Email: {User.ActiveUser.EmailAddress}");
            }
            if (User.ActiveUser.UserType == "Floor manager")
            {
                Console.WriteLine("Your details.");
                Console.WriteLine($"Name: {User.ActiveUser.Name}");
                Console.WriteLine($"Age: {User.ActiveUser.Age}");
                Console.WriteLine($"Mobile phone: {User.ActiveUser.MobileNumber}");
                Console.WriteLine($"Email: {User.ActiveUser.EmailAddress}");
                Console.WriteLine($"Staff ID: {User.ActiveUser.StaffID}");
                Console.WriteLine($"Floor: {User.ActiveUser.FloorNumber}.");
            }
            if (User.ActiveUser.UserType == "Surgeon")
            {
                Console.WriteLine("Your details.");
                Console.WriteLine($"Name: {User.ActiveUser.Name}");
                Console.WriteLine($"Age: {User.ActiveUser.Age}");
                Console.WriteLine($"Mobile phone: {User.ActiveUser.MobileNumber}");
                Console.WriteLine($"Email: {User.ActiveUser.EmailAddress}");
                Console.WriteLine($"Staff ID: {User.ActiveUser.StaffID}");
                Console.WriteLine($"Speciality: {User.ActiveUser.Speciality}");
            }
        }


        public static void Authenticate()
        {
            do
            {
                if (IUser.EmailIsAuthenticated == true && IUser.PasswordIsAuthenticated == false)
                {
                    break;
                }
                Console.WriteLine("Please enter in your email:");
                string AuthenticatedEmail = Console.ReadLine() ?? "";
                if (EmailsActive.Contains(AuthenticatedEmail) == false)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine($"#Error - Email is not registered.");
                    Console.WriteLine("#####");
                    break;
                }
                foreach (User CurrentUser in UsersList)
                {
                    int i = 0;
                    if (CurrentUser.EmailAddress == AuthenticatedEmail)
                    {
                        IUser.EmailIsAuthenticated = true;
                        if (IUser.EmailIsAuthenticated == true)
                        {
                            Console.WriteLine("Please enter in your password:");
                            string AuthenticatedPassword = Console.ReadLine() ?? "";
                            if (CurrentUser.Password == AuthenticatedPassword)
                            {
                                CurrentUser.IsAuthenticated = true;
                                IUser.IsAuthenticated = true;
                                IUser.IsLoggedIn = true;
                                Console.WriteLine($"Hello {CurrentUser.Name} welcome back.");
                                break;
                            }
                            else
                            {
                                IUser.PasswordIsAuthenticated = false;
                                IUser.IsAuthenticated = false;
                                Console.WriteLine("#####");
                                Console.WriteLine($"#Error - Wrong Password.");
                                Console.WriteLine("#####");
                                break;
                            }
                        }
                    }
                    else
                    {
                        i++;
                        IUser.EmailIsAuthenticated = false;
                        IUser.IsAuthenticated = false;
                        if (IUser.EmailIsAuthenticated == false && i == UsersList.Count)
                        {
                            Console.WriteLine("#####");
                            Console.WriteLine($"#Error - Supplied email is invalid, please try again.");
                            Console.WriteLine("#####");
                        }
                        continue;
                    }
                }
            }
            while (IUser.IsAuthenticated == false);
        }
        public static void LogOut()
        {
            //replace Ative user and the actual users thing
            foreach (User CurrentUser in UsersList)
            {
                if (CurrentUser.EmailAddress == User.ActiveUser.EmailAddress && CurrentUser.IsAuthenticated == true)
                {
                    CurrentUser.IsAuthenticated = false;
                    User.ActiveUser.IsAuthenticated = false;
                    Console.WriteLine($"{User.ActiveUser.UserType} {User.ActiveUser.Name} has logged out.");
                    User.ActiveUser = new();
                    break;
                }
                else
                {
                    continue;
                }
            }
            IUser.IsAuthenticated = false;
            IUser.EmailIsAuthenticated = false;
            IUser.IsLoggedIn = false;
        }
        public static void ChangePassword(string password)
        {
            User.ActiveUser.Password = password;
            Console.WriteLine($"Password has been changed.");
        }


        public static void FillPatientsList(string CurrentList)
        {
            if (CurrentList == "CheckedInPatientsListNoRoom")
            {
                foreach (User CurrentUser in UsersList)
                {
                    // since only patients have property check in no need to check user type
                    if (CurrentUser.IsCheckedIn == true && CurrentUser.RoomNumber == -1)
                    {
                        User.CheckedInPatientsList.Add(CurrentUser);
                    }
                    continue;
                }
            }
            if (CurrentList == "CheckedInPatientsList")
            {
                foreach (User CurrentUser in UsersList)
                {
                    // since only patients have property check in no need to check user type
                    if (CurrentUser.IsCheckedIn == true)
                    {
                        User.CheckedInPatientsList.Add(CurrentUser);
                    }
                    continue;
                }
            }
            if (CurrentList == "PatientsAssignedRooms")
            {
                foreach (User CurrentUser in User.CheckedInPatientsList)
                {
                    // since only patients have property check in no need to check user type
                    if (CurrentUser.RoomNumber != -1 && CurrentUser.CurrentSurgeon == "")
                    {
                        User.PatientsAssignedRooms.Add(CurrentUser);
                    }
                    continue;
                }
            }
        }
        public static int SelectRoomNumber()
        {
            do
            {
                ErrorDetection.HasError = false;
                Console.WriteLine("Please enter your room (1-10):");
                string SelectedRoomNumber = Console.ReadLine() ?? "";
                ErrorDetection.DetectStringError(SelectedRoomNumber, "Integer check");
                
                if (ErrorDetection.HasError == false)
                {
                    if (ActiveUser.FloorRoomsAssigned.Contains(int.Parse(SelectedRoomNumber)) == true)
                    {
                        Console.WriteLine("#####");
                        Console.WriteLine("#Error - Room has been assigned to another patient, please try again.");
                        Console.WriteLine("#####");
                        IUser.RoomNumber = -1;
                        continue;
                    }
                    else 
                    {
                        IUser.RoomNumber = int.Parse(SelectedRoomNumber);
                        ErrorDetection.DetectIntError(IUser.RoomNumber, "room number");
                        if (IUser.RoomNumber == -1)
                        {
                            continue;
                        }
                        return IUser.RoomNumber;
                    }
                }
            }
            while (IUser.RoomNumber == -1 || IUser.RoomNumber == -2);
            return IUser.RoomNumber = -1;
        }
        public static void UpdateUserInfo(User UserUpdating)
        {
            foreach (User CurrentUser in UsersList)
            {
                if (CurrentUser.UserType == "Patient" && CurrentUser.EmailAddress == UserUpdating.EmailAddress && CurrentUser.Password == UserUpdating.Password)
                {
                    CurrentUser.Password = UserUpdating.Password;
                    CurrentUser.EmailAddress = UserUpdating.EmailAddress;
                    CurrentUser.RoomNumber = UserUpdating.RoomNumber;
                    CurrentUser.IsCheckedIn = UserUpdating.IsCheckedIn;
                    CurrentUser.SurgeryTime = UserUpdating.SurgeryTime;
                    CurrentUser.CurrentSurgeon = UserUpdating.CurrentSurgeon;
                    CurrentUser.HadSurgery = UserUpdating.HadSurgery;

                    break;
                }
                else if (CurrentUser.UserType == "Floor Manager" && CurrentUser.EmailAddress == UserUpdating.EmailAddress && CurrentUser.Password == UserUpdating.Password)
                {
                    CurrentUser.RoomsLeftToAssign = UserUpdating.RoomsLeftToAssign;
                    CurrentUser.FloorRoomsAssigned = UserUpdating.FloorRoomsAssigned;
                }
                else if (CurrentUser.UserType == "Surgeon" && CurrentUser.EmailAddress == UserUpdating.EmailAddress && CurrentUser.Password == UserUpdating.Password)
                {
                    CurrentUser.Password = UserUpdating.Password;
                    CurrentUser.EmailAddress = UserUpdating.EmailAddress;
                    CurrentUser.bookedSurgeryTimes = UserUpdating.bookedSurgeryTimes;
                    CurrentUser.SurgerySchedule = UserUpdating.SurgerySchedule;
                    CurrentUser.PatientsAssignedToSurgeon = UserUpdating.PatientsAssignedToSurgeon;
                }
                else
                {
                    continue;
                }
            }
        }
        public static void GiveManagerOptions(List<User> ActiveList)
        {
            for (int i = 0; i < ActiveList.Count; i++)
            {
                string Option = $"{i + 1}. {ActiveList[i].Name}";
                OptionsList.Add(Option);
                if (i == ActiveList.Count - 1)
                {
                    foreach (string option in OptionsList)
                    {
                        Console.WriteLine(option);
                    }
                }
            }
        }
        public static string? ReformatOption(string ManagersChoice)
        {
            foreach (string option in OptionsList)
            {
                if (option.StartsWith(ManagersChoice))
                {
                    string OptionsName = option.Trim(ManagersChoice.ToCharArray());
                    // due to formatting issues will need to remove the first character which will be a string
                    OptionsName = OptionsName.TrimStart('.');
                    OptionsName = OptionsName.TrimStart(' ');
                    OptionsList = new List<string>();
                    return OptionsName;
                }
                continue;
            }
            return null;
        }
        public static void AssignRoom()
        {
            Console.WriteLine("Please select your patient:");

            User.GiveManagerOptions(User.CheckedInPatientsList);
            do
            {
                Console.WriteLine($"Please enter a choice between 1 and {OptionsList.Count}.");
                managersChoice = Console.ReadLine() ?? "";
                if (managersChoice.Any(Char.IsDigit) == false && managersChoice.StartsWith("-") == false || managersChoice.Any(char.IsLetter) == true)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied value is not an integer, please try again.");
                    Console.WriteLine("#####");
                    managersChoice = "";
                    
                }
                else if (int.Parse(managersChoice) > OptionsList.Count || int.Parse(managersChoice) < 1)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied value is out of range, please try again.");
                    Console.WriteLine("#####");
                    managersChoice = "";
                    
                }
                else 
                {
                    managersChoice = User.ReformatOption(managersChoice) ?? "";
                }
            }
            while (User.managersChoice == "");
            int SelectedRoomNumber = User.SelectRoomNumber();
            ActiveUser.FloorRoomsAssigned.Add(SelectedRoomNumber);

            string OptionsName = managersChoice;

            foreach (User CurrentUser in CheckedInPatientsList)
            {
                if (CurrentUser.Name == OptionsName)
                {
                    CurrentUser.RoomNumber = SelectedRoomNumber;
                    CurrentUser.FloorNumber = User.ActiveUser.FloorNumber;
                    User.UpdateUserInfo(CurrentUser);
                    PatientsAssignedRooms.Add(CurrentUser);
                    ActiveUser.RoomsLeftToAssign -= 1;
                    User.UpdateUserInfo(ActiveUser);
                    Console.WriteLine($"Patient {CurrentUser.Name} has been assigned to room number {CurrentUser.RoomNumber} on floor {CurrentUser.FloorNumber}.");
                    break;
                }
                else
                {
                    continue;
                }
            }

            User.OptionsList = new List<string>();
            User.CheckedInPatientsList = new List<User>();
        }
        public static void SeeRoomAssigned()
        {
            if (User.ActiveUser.RoomNumber == -1 || User.ActiveUser.FloorNumber == -1)
            {
                Console.WriteLine("You do not have an assigned room.");
            }
            else
            {
                Console.WriteLine($"Your room is number {User.ActiveUser.RoomNumber} on floor {User.ActiveUser.FloorNumber}.");
            }
        }
        public static DateTime GetDateTime()
        {
            DateTime Dateresult = Convert.ToDateTime("00:00 01 / 01 / 0001");
            DateTime Input;
            do
            {
                Console.WriteLine("Please enter a date and time (e.g. 14:30 31/01/2024).");

                
                string input = Console.ReadLine() ?? "";
                bool dtWorked = DateTime.TryParseExact(input, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out Dateresult);
                bool dtAbove24 = DateTime.TryParseExact(input, DateFormat,CultureInfo.InvariantCulture, DateTimeStyles.None, out Input);
                    if (!dtAbove24)
                    {
                        Console.WriteLine("#####");
                        Console.WriteLine("#Error - Supplied value is not a valid DateTime.");
                        Console.WriteLine("#####");
                        Dateresult = Convert.ToDateTime("00:00 01 / 01 / 0001");
                    }
                    else if (!dtWorked)
                    {
                        Console.WriteLine("#####");
                        Console.WriteLine("#Error - Supplied Date is provided in the wrong format. please try again.");
                        Console.WriteLine("#####");
                        Dateresult = Convert.ToDateTime("00:00 01 / 01 / 0001");
                    }
                else
                {
                    return Dateresult;
                }
                
            }
            while (Dateresult == Convert.ToDateTime("00:00 01 / 01 / 0001"));
            return Dateresult;
        }
        public static User? FindUserChosen(List<User> CurrentList, string ManagersChoice)
        {
            foreach (User CurrentUser in CurrentList)
            {
                if (CurrentUser.Name == ManagersChoice)
                {
                    return CurrentUser;
                }
                else
                {
                    continue;
                }
            }
            return null;
        }
        public static void AssignSurgeon()
        {
            Console.WriteLine("Please select your patient:");
            User.GiveManagerOptions(User.PatientsAssignedRooms);
            do
            {
                Console.WriteLine($"Please enter a choice between 1 and {OptionsList.Count}.");
                managersChoice = Console.ReadLine() ?? "";
                if (managersChoice.Any(Char.IsDigit) == false && managersChoice.StartsWith("-") == false || managersChoice.Any(char.IsLetter) == true)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied value is not an integer, please try again.");
                    Console.WriteLine("#####");
                    managersChoice = "";
                }
                else if (int.Parse(managersChoice) > OptionsList.Count || int.Parse(managersChoice) < 1)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied value is out of range, please try again.");
                    Console.WriteLine("#####");
                    managersChoice = "";
                }
            }
            while (managersChoice == "");
            string managersPatientChoice = managersChoice;
            managersPatientChoice = User.ReformatOption(managersPatientChoice) ?? "";
            PatientChosen = User.FindUserChosen(PatientsAssignedRooms, managersPatientChoice) ?? new();

            User.OptionsList = new List<string>();
            User.PatientsAssignedRooms.Remove(PatientChosen);

            Console.WriteLine("Please select your surgeon:");
            User.GiveManagerOptions(User.StaffedSurgeons);
            do
            {
                Console.WriteLine($"Please enter a choice between 1 and {OptionsList.Count}.");
                managersChoice = Console.ReadLine() ?? "";
                if (managersChoice.Any(Char.IsDigit) == false && managersChoice.StartsWith("-") == false || managersChoice.Any(char.IsLetter) == true)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied value is not an integer, please try again.");
                    Console.WriteLine("#####");
                    managersChoice = "";
                }
                else if (int.Parse(managersChoice) > OptionsList.Count || int.Parse(managersChoice) < 1)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied value is out of range, please try again.");
                    Console.WriteLine("#####");
                    managersChoice = "";
                }
            }
            while (managersChoice == "");
            string managersSurgeonChoice = managersChoice;
            managersSurgeonChoice = User.ReformatOption(managersSurgeonChoice) ?? "";
            SurgeonChosen = User.FindUserChosen(StaffedSurgeons, managersSurgeonChoice) ?? new();

            User.OptionsList = new List<string>();

            
            DateTime ManagersDateChoice = User.GetDateTime();

            //declaring variables to be updated for patients
            PatientChosen.CurrentSurgeon = SurgeonChosen.Name;
            PatientChosen.SurgeryTime = ManagersDateChoice.ToString(DateFormat);
            User.UpdateUserInfo(PatientChosen);
            //declaring variables to be updated for surgeons
            SurgeonChosen.SurgerySchedule.Add(ManagersDateChoice, managersPatientChoice);
            SurgeonChosen.PatientsAssignedToSurgeon.Add(PatientChosen);
            User.UpdateUserInfo(SurgeonChosen);

            Console.WriteLine($"Surgeon {managersSurgeonChoice} has been assigned to patient {managersPatientChoice}.");
            Console.WriteLine($"Surgery will take place on {ManagersDateChoice.ToString(DateFormat)}.");

        }
        public static void SeeAllocatedSurgeon()
        {
            if (User.ActiveUser.CurrentSurgeon == "")
            {
                Console.WriteLine("You do not have an assigned surgeon.");
            }
            else 
            {
                Console.WriteLine($"Your surgeon is {User.ActiveUser.CurrentSurgeon}.");
            }
        }
        public static void SeeSurgeryDateTime()
        {
            if (ActiveUser.SurgeryTime == "")
            {
                Console.WriteLine("You do not have assigned surgery.");
            }
            else
            {
                Console.WriteLine($"Your surgery time is {ActiveUser.SurgeryTime}.");
            }
        }
        public static void SeePatientList()
        {
            for(int i = 0; i < ActiveUser.PatientsAssignedToSurgeon.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ActiveUser.PatientsAssignedToSurgeon[i].Name}");
            }
        }
        public static void SeeSchedule()
        {
            foreach (KeyValuePair<DateTime, string> DictionaryEntry in User.ActiveUser.SurgerySchedule.OrderBy(ManagersDateChoice => ManagersDateChoice.Key))
            {
                Console.WriteLine($"Performing surgery on patient {DictionaryEntry.Value} on {DictionaryEntry.Key.ToString(DateFormat)}");
            }
        }
        public static void RemovePatientFromSchedule(User PatientHavingSurgery)
        {
            foreach (KeyValuePair<DateTime, string> DictEntry in ActiveUser.SurgerySchedule)
            {
                if (DictEntry.Value == PatientHavingSurgery.Name)
                {
                    ActiveUser.SurgerySchedule.Remove(DictEntry.Key);
                    break;
                }
                continue;
            }
        }
        public static void PerformSurgery()
        {
            Console.WriteLine("Please select your patient:");
            User.GiveManagerOptions(User.ActiveUser.PatientsAssignedToSurgeon);
            do
            {
                Console.WriteLine($"Please enter a choice between 1 and {OptionsList.Count}.");
                managersChoice = Console.ReadLine() ?? "";
                if (managersChoice.Any(Char.IsDigit) == false && managersChoice.StartsWith("-") == false || managersChoice.Any(char.IsLetter) == true)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied value is not an integer, please try again.");
                    Console.WriteLine("#####");
                    managersChoice = "";
                }
                else if (int.Parse(managersChoice) > OptionsList.Count || int.Parse(managersChoice) < 1)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied value is out of range, please try again.");
                    Console.WriteLine("#####");
                    managersChoice = "";
                }
            }
            while (managersChoice == "");
            string SurgeonsChoice = managersChoice;
            SurgeonsChoice = User.ReformatOption(SurgeonsChoice) ?? "";
            User  PatientHavingSurgery = User.FindUserChosen(User.ActiveUser.PatientsAssignedToSurgeon, SurgeonsChoice) ?? new();

            PatientHavingSurgery.HadSurgery = true;
            PatientHavingSurgery.CurrentSurgeon = "";
            User.UpdateUserInfo(PatientHavingSurgery);
            User.PatientHadSurgeryList.Add(PatientHavingSurgery);

            User.ActiveUser.PatientsAssignedToSurgeon.Remove(PatientHavingSurgery);
            User.RemovePatientFromSchedule(PatientHavingSurgery);
            User.UpdateUserInfo(User.ActiveUser);

            Console.WriteLine($"Surgery performed on {PatientHavingSurgery.Name} by {ActiveUser.Name}.");
        }
        public static List<User> FindPatientsHadSurgeryAssignedToManager(List<User> GivenList, int ManagerFloorNumber)
        {
            foreach (User CurrentPatient in GivenList.Where(s => s.HadSurgery == true))
            {
                if (CurrentPatient.FloorNumber == ManagerFloorNumber)
                {
                    PatientsHadSurgeryAssignedToManager.Add(CurrentPatient);
                }
                else 
                {
                    continue;
                }
            }
            return PatientsHadSurgeryAssignedToManager;
        }
        public static void UnassignRoom()
        {
            Console.WriteLine("Please select your patient:");
            User.FindPatientsHadSurgeryAssignedToManager(UsersList, ActiveUser.FloorNumber);
            User.GiveManagerOptions(PatientsHadSurgeryAssignedToManager);
            do
            {
                Console.WriteLine($"Please enter a choice between 1 and {OptionsList.Count}.");
                managersChoice = Console.ReadLine() ?? "";
                if (managersChoice.Any(Char.IsDigit) == false && managersChoice.StartsWith("-") == false || managersChoice.Any(char.IsLetter) == true)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied value is not an integer, please try again.");
                    Console.WriteLine("#####");
                    managersChoice = "";

                }
                else if (int.Parse(managersChoice) > OptionsList.Count || int.Parse(managersChoice) < 1)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied value is out of range, please try again.");
                    Console.WriteLine("#####");
                    managersChoice = "";

                }
                else
                {
                    managersChoice = User.ReformatOption(managersChoice) ?? "";
                }
            }
            while (User.managersChoice == "");

            PatientChosen = User.FindUserChosen(User.PatientHadSurgeryList, managersChoice) ?? new();
            Console.WriteLine($"Room number {PatientChosen.RoomNumber} on floor {PatientChosen.FloorNumber} has been unassigned.");
            ActiveUser.FloorRoomsAssigned.Remove(PatientChosen.RoomNumber);
            PatientChosen.RoomNumber = -1;
            PatientChosen.FloorNumber = -1;
            User.UpdateUserInfo(PatientChosen);
            PatientsHadSurgeryAssignedToManager = new();
        }
    }
}
