namespace Assignment2FinalDesign
{
    public class Patient : User, IUser
    {
        public static List<User> CheckedInPatientsList = new List<User>() ?? []; // this collection is used to keep track and store checked in patients
        public static List<Patient> RegisteredPatientsList = new List<Patient>() ?? []; // this collection is used to keep track and store registered patients
        public static List<User> PatientHadSurgeryList = new List<User>() ?? []; // this collection is used to keep track and store patients that have had surgery
        public string? CurrentSurgeon = null;
        public string? SurgeryTime = null;
        public bool IsCheckedIn = false;
        public bool HadSurgery = false;

        private int age;
        public override int Age
        {
            get
            {
                return age;
            }
            set
            {
                // creates if statements that retrict the users age from 0-100 inclusivly
                if (value >= 0 && value <= 100)
                {
                    age = value;
                }

                else
                {
                    // if anything else is given an argument exception is thrown and caught when creating instances of a patient
                    throw new ArgumentException(String.Format($"#####{Environment.NewLine}#Error - Supplied age is invalid, please try again.{Environment.NewLine}#####", Age));
                }
            }
        }
        public Patient()
        {
            RegisteredPatientsList.Add(this); // adds this instance of the object to the registered patients list
        }
        public Patient(string Name, int Age, string EmailAddress, string MobileNumber, string Password) : base(Name, EmailAddress, MobileNumber, Password)
        {
            this.Age = Age;
            this.IsAuthenticated = false;
            this.IsCheckedIn = false;
            this.RoomNumber = null;
            this.FloorNumber = null;
            this.CurrentSurgeon = null;
            RegisteredPatientsList.Add(this);
        }
        /// <summary>
        /// this method is used to display an objects properties such as name, age, mobile number and email
        /// </summary>
        public void DisplayInfo()
        {
            Console.WriteLine("Your details.");
            Console.WriteLine($"Name: {UsersList[ActiveUserIndex].Name}");
            Console.WriteLine($"Age: {UsersList[ActiveUserIndex].Age}");
            Console.WriteLine($"Mobile phone: {UsersList[ActiveUserIndex].MobileNumber}");
            Console.WriteLine($"Email: {UsersList[ActiveUserIndex].EmailAddress}");
        }
        /// <summary>
        /// this method is used to reset the log in data to default in order to tell the system the user has logged out and a new user can log in
        /// </summary>
        public void LogOut()
        {
            IsAuthenticated = false;
            IsLoggedIn = false;
            UsersList[ActiveUserIndex].IsAuthenticated = false;
            UsersList[ActiveUserIndex].IsLoggedIn = false;
            Console.WriteLine($"Patient {UsersList[ActiveUserIndex].Name} has logged out.");
        }
        /// <summary>
        /// this method  is used to modify whether the patient is checked in or out at any given time throughout their time registered
        /// </summary>
        public void CheckInorOut()
        {
            Patient ActivePatient = (Patient)UsersList[ActiveUserIndex];

            if (ActivePatient.IsCheckedIn == false && ActivePatient.HadSurgery == true)
            {
                Console.WriteLine("You are unable to check in at this time.");
            }
            else if (ActivePatient.IsCheckedIn == false && ActivePatient.HadSurgery == false)
            {
                ActivePatient.IsCheckedIn = true;
                CheckedInPatientsList.Add(ActivePatient);
                Console.WriteLine($"Patient {ActivePatient.Name} has been checked in.");
            }
            else if (ActivePatient.IsCheckedIn == true && ActivePatient.HadSurgery == false)
            {
                Console.WriteLine("You are unable to check out at this time.");
            }
            else
            {
                ActivePatient.IsCheckedIn = false;
                Console.WriteLine($"Patient {ActivePatient.Name} has been checked out.");
            }
        }
        /// <summary>
        /// this method allows a patient to see their allocated room and floor numbers
        /// </summary>
        public void SeeRoomAssigned()
        {
            Patient ActivePatient = (Patient)UsersList[ActiveUserIndex];
            if (ActivePatient.RoomNumber == null)
            {
                Console.WriteLine("You do not have an assigned room.");
            }
            else
            {
                Console.WriteLine($"Your room is number {ActivePatient.RoomNumber} on floor {ActivePatient.FloorNumber}.");
            }
        }
        /// <summary>
        /// this method allows a patient to see their allocated Surgeons Name
        /// </summary>
        public void SeeAllocatedSurgeon()
        {
            Patient ActivePatient = (Patient)UsersList[ActiveUserIndex];
            if (ActivePatient.CurrentSurgeon == null)
            {
                Console.WriteLine("You do not have an assigned surgeon.");
            }
            else
            {
                Console.WriteLine($"Your surgeon is {ActivePatient.CurrentSurgeon}.");
            }
        }
        /// <summary>
        /// this method allows a patient to see their allocated Surgery date and time.
        /// </summary>
        public void SeeSurgeryDateTime()
        {
            Patient ActivePatient = (Patient)UsersList[ActiveUserIndex];
            if (ActivePatient.SurgeryTime == null)
            {
                Console.WriteLine("You do not have assigned surgery.");
            }
            else
            {
                Console.WriteLine($"Your surgery time is {ActivePatient.SurgeryTime}.");
            }
        }
    }
}
