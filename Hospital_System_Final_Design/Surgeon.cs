namespace Assignment2FinalDesign
{
    public class Surgeon : Staff, IUser
    {
        private int age;
        public override int Age
        {
            get
            {
                return age;
            }
            set
            {
                // creates if statements that retrict the Surgeons age 30-75 inclusively
                if (value >= 30 && value <= 75)
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
        public Surgeon()
        {
        }
        public Surgeon(string Name, int Age, string EmailAddress, string MobileNumber, string Password, int StaffID, string Speciality) : base(Name, EmailAddress, MobileNumber, Password, StaffID)
        {
            this.Speciality = Speciality;
            this.Age = Age;
            this.IsAuthenticated = false;
            this.PatientsAssignedToSurgeon = [];
            this.SurgerySchedule = [];
            StaffedSurgeons.Add(this); // adds this object instance to the Staffed Surgeons collection
        }
        /// <summary>
        /// this method is used to show a surgeon their surgeries date and for what patient ordered by when the surgery is
        /// </summary>
        public void SeeSchedule()
        {
            Surgeon ActiveSurgeon = (Surgeon)UsersList[ActiveUserIndex];
            foreach (KeyValuePair<DateTime, string> DictionaryEntry in ActiveSurgeon.SurgerySchedule.OrderBy(ManagersDateChoice => ManagersDateChoice.Key))
            {
                Console.WriteLine($"Performing surgery on patient {DictionaryEntry.Value} on {DictionaryEntry.Key.ToString(DateFormat)}");
            }
        }
        /// <summary>
        /// this method is used to show the surgeon how many patients they have
        /// </summary>
        public void SeePatientList()
        {
            Surgeon ActiveSurgeon = (Surgeon)UsersList[ActiveUserIndex];

            for (int i = 0; i < ActiveSurgeon.PatientsAssignedToSurgeon.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ActiveSurgeon.PatientsAssignedToSurgeon[i].Name}");
            }
        }
        /// <summary>
        /// this method is used to modify relevant Patient and Surgeon properties to the surgery process
        /// </summary>
        public void PerformSurgery()
        {
            Surgeon ActiveSurgeon = (Surgeon)UsersList[ActiveUserIndex];
            Console.WriteLine("Please select your patient:");
            string SurgeonsChoice;
            GiveStaffMemberOptions(ActiveSurgeon.PatientsAssignedToSurgeon);
            do
            {
                Console.WriteLine($"Please enter a choice between 1 and {OptionsList.Count}.");
                SurgeonsChoice = Console.ReadLine() ?? "";
                try
                {
                    int.Parse(SurgeonsChoice);
                }
                catch (Exception)
                {
                    Console.WriteLine($"#####{Environment.NewLine}#Error - Supplied value is not an integer, please try again.{Environment.NewLine}#####");
                    SurgeonsChoice = "";
                    continue;
                }

                if (int.Parse(SurgeonsChoice) > OptionsList.Count || int.Parse(SurgeonsChoice) < 1)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied value is out of range, please try again.");
                    Console.WriteLine("#####");
                    SurgeonsChoice = "";
                    continue;
                }
            }
            while (SurgeonsChoice == "");
            SurgeonsChoice = ReformatOption(SurgeonsChoice) ?? "";
            Patient PatientHavingSurgery = (Patient)FindUserChosen(ActiveSurgeon.PatientsAssignedToSurgeon, SurgeonsChoice);

            PatientHavingSurgery.HadSurgery = true;
            PatientHavingSurgery.CurrentSurgeon = null;//set Current Surgeon to the null because no surgeon will be assigned after this method
            Patient.PatientHadSurgeryList.Add(PatientHavingSurgery);

            ActiveSurgeon.PatientsAssignedToSurgeon.Remove(PatientHavingSurgery);
            ActiveSurgeon.RemovePatientFromSchedule(PatientHavingSurgery);

            Console.WriteLine($"Surgery performed on {PatientHavingSurgery.Name} by {ActiveSurgeon.Name}.");
        }
        /// <summary>
        /// this method is to remove a Patient from a surgeons Surgery schedule once surgery has been performed
        /// </summary>
        /// <param name="PatientToRemove"> this is the name of the patient that has to be removed</param>
        public void RemovePatientFromSchedule(User PatientToRemove)
        {
            Surgeon ActiveSurgeon = (Surgeon)UsersList[ActiveUserIndex];
            foreach (KeyValuePair<DateTime, string> DictEntry in ActiveSurgeon.SurgerySchedule)
            {
                if (DictEntry.Value == PatientToRemove.Name)
                {
                    ActiveSurgeon.SurgerySchedule.Remove(DictEntry.Key);
                    break;
                }
                continue;
            }
        }
        /// <summary>
        /// this method is used to display an objects properties such as name, age, mobile number, email, staff ID and Speciality
        /// </summary>
        public void DisplayInfo()
        {
            Surgeon ActiveSurgeon = (Surgeon)UsersList[ActiveUserIndex];
            Console.WriteLine("Your details.");
            Console.WriteLine($"Name: {ActiveSurgeon.Name}");
            Console.WriteLine($"Age: {ActiveSurgeon.Age}");
            Console.WriteLine($"Mobile phone: {ActiveSurgeon.MobileNumber}");
            Console.WriteLine($"Email: {ActiveSurgeon.EmailAddress}");
            Console.WriteLine($"Staff ID: {ActiveSurgeon.StaffID}");
            Console.WriteLine($"Speciality: {ActiveSurgeon.Speciality}");
        }
        /// <summary>
        /// this method  is used to modify whether the patient is checked in or out at any given time throughout their time registered
        /// </summary>
        public void LogOut()
        {
            IsAuthenticated = false;
            IsLoggedIn = false;
            UsersList[ActiveUserIndex].IsAuthenticated = false;
            UsersList[ActiveUserIndex].IsLoggedIn = false;
            Console.WriteLine($"Surgeon {UsersList[ActiveUserIndex].Name} has logged out.");
        }
    }
}
