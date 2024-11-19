using System.Globalization;

namespace Assignment2FinalDesign
{
    public class FloorManager : Staff, IUser
    {
        public static List<FloorManager> RegisteredFloorManagerList = new List<FloorManager>() ?? [];
        public static List<User> PatientsHadSurgeryAssignedToManager = new List<User>() ?? [];
        public string ManagersPatientChoice = "";
        public string ManagersSurgeonChoice = "";
        private int age;
        public override int Age
        {
            get
            {
                return age;
            }
            set
            {
                // creates if statements that retrict the Floor managers age 21-70 inclusively
                if (value >= 21 && value <= 70)
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
        public FloorManager()
        {
        }
        public FloorManager(string Name, int Age, string EmailAddress, string MobileNumber, string Password, int StaffID, int? FloorNumber) : base(Name, EmailAddress, MobileNumber, Password, StaffID)
        {
            this.FloorNumber = FloorNumber;
            this.FloorRoomsAssigned = [];
            RegisteredFloorManagerList.Add(this);
            FloorsManaged.Add(this.FloorNumber);
        }
        /// <summary>
        /// this method is used to display an objects properties such as name, age, mobile number , email, staff ID and floor number
        /// </summary>
        public void DisplayInfo()
        {
            FloorManager ActiveFloorManager = (FloorManager)UsersList[ActiveUserIndex];
            Console.WriteLine("Your details.");
            Console.WriteLine($"Name: {ActiveFloorManager.Name}");
            Console.WriteLine($"Age: {ActiveFloorManager.Age}");
            Console.WriteLine($"Mobile phone: {ActiveFloorManager.MobileNumber}");
            Console.WriteLine($"Email: {ActiveFloorManager.EmailAddress}");
            Console.WriteLine($"Staff ID: {ActiveFloorManager.StaffID}");
            Console.WriteLine($"Floor: {ActiveFloorManager.FloorNumber}.");
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
            Console.WriteLine($"Floor manager {UsersList[ActiveUserIndex].Name} has logged out.");
        }
        /// <summary>
        /// this method is used to allow the floor manager to select a room number for a patient
        /// </summary>
        /// <returns>an int room number</returns>
        public int? SelectRoomNumber()
        {
            FloorManager ActiveFloorManager = (FloorManager)UsersList[ActiveUserIndex];
            do
            {
                Console.WriteLine("Please enter your room (1-10):");
                string? selectedRoomNumber = Console.ReadLine() ?? "";
                try
                {
                    int.Parse(selectedRoomNumber);
                }
                catch (Exception) 
                {
                    Console.WriteLine($"#####{Environment.NewLine}#Error - Supplied value is not an integer, please try again.{Environment.NewLine}#####");
                    continue;
                }
                if (ActiveFloorManager.FloorRoomsAssigned.Contains(int.Parse(selectedRoomNumber)) == true)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Room has been assigned to another patient, please try again.");
                    Console.WriteLine("#####");
                    ActiveFloorManager.RoomNumber = null;
                    continue;
                }
                else if (int.Parse(selectedRoomNumber) > 10 || int.Parse(selectedRoomNumber) < 1)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied value is out of range, please try again.");
                    Console.WriteLine("#####");
                    selectedRoomNumber = null;
                    continue;
                }
                else
                {
                    ActiveFloorManager.RoomNumber = int.Parse(selectedRoomNumber);
                    if (ActiveFloorManager.RoomNumber == null)
                    {
                        continue;
                    }
                    return ActiveFloorManager.RoomNumber;
                }
            }
            while (ActiveFloorManager.RoomNumber == null);
            return ActiveFloorManager.RoomNumber = null;
        }
        /// <summary>
        /// this method is used for a manager to select a patient
        /// </summary>
        /// <param name="MethodAssociatedList"> this is a list that is specific to the method being used in</param>
        public void SelectPatient(List<User> MethodAssociatedList)
        {
            Console.WriteLine("Please select your patient:");
            GiveStaffMemberOptions(MethodAssociatedList);
            do
            {
                Console.WriteLine($"Please enter a choice between 1 and {OptionsList.Count}.");
                ManagersPatientChoice = Console.ReadLine() ?? "";
                try
                {
                    int.Parse(ManagersPatientChoice);
                }
                catch (Exception)
                {
                    Console.WriteLine($"#####{Environment.NewLine}#Error - Supplied value is not an integer, please try again.{Environment.NewLine}#####");
                    ManagersPatientChoice = "";
                    continue;
                }
                if (int.Parse(ManagersPatientChoice) > OptionsList.Count || int.Parse(ManagersPatientChoice) < 1)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied value is out of range, please try again.");
                    Console.WriteLine("#####");
                    ManagersPatientChoice = "";
                    continue;
                }
                else
                {
                    ManagersPatientChoice = ReformatOption(ManagersPatientChoice) ?? "";
                }
            }
            while (ManagersPatientChoice == "");
        }
        /// <summary>
        /// this method is used to assign a patient to a room 
        /// </summary>
        public void AssignRoom()
        {
            FloorManager ActiveFloorManager = (FloorManager)UsersList[ActiveUserIndex];
            ActiveFloorManager.SelectPatient(Patient.CheckedInPatientsList);
            int? SelectedRoomNumber = SelectRoomNumber();
            RemoveUserChosen(Patient.CheckedInPatientsList, ManagersPatientChoice);
            ActiveFloorManager.FloorRoomsAssigned.Add(SelectedRoomNumber);

            string OptionsName = ManagersPatientChoice;

            foreach (var CurrentUser in UsersList)
            {
                if (CurrentUser.Name == OptionsName)
                {
                    int CurrentPatientIndex = FindUserIndex(CurrentUser.Name);
                    UsersList[CurrentPatientIndex].RoomNumber = SelectedRoomNumber;
                    UsersList[CurrentPatientIndex].FloorNumber = ActiveFloorManager.FloorNumber;
                    PatientsAssignedRooms.Add(UsersList[CurrentPatientIndex]);
                    Console.WriteLine($"Patient {UsersList[CurrentPatientIndex].Name} has been assigned to room number {UsersList[CurrentPatientIndex].RoomNumber} on floor {UsersList[CurrentPatientIndex].FloorNumber}.");
                    break;
                }
                else
                {
                    continue;
                }
            }
            OptionsList = new List<string>();
        }
        /// <summary>
        /// this method is used for the floor manager to select a date and time for surgery and formats correctly
        /// </summary>
        /// <returns> a datetime variable in the correct format</returns>
        public DateTime GetDateTime()
        {
            DateTime Dateresult = Convert.ToDateTime("00:00 01 / 01 / 0001");
            DateTime Input;
            do
            {
                Console.WriteLine("Please enter a date and time (e.g. 14:30 31/01/2024).");


                string input = Console.ReadLine() ?? "";
                bool dtWorked = DateTime.TryParseExact(input, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out Dateresult);
                bool dtAbove24 = DateTime.TryParseExact(input, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out Input);
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
        /// <summary>
        /// this method is used for a manager to select a surgeon from the staffed surgeons collection
        /// </summary>
        public void SelectSurgeon()
        {
            Console.WriteLine("Please select your surgeon:");
            GiveStaffMemberOptions(StaffedSurgeons);
            do
            {
                Console.WriteLine($"Please enter a choice between 1 and {OptionsList.Count}.");
                ManagersSurgeonChoice = Console.ReadLine() ?? "";
                try
                {
                    int.Parse(ManagersSurgeonChoice);
                }
                catch (Exception)
                {
                    Console.WriteLine($"#####{Environment.NewLine}#Error - Supplied value is not an integer, please try again.{Environment.NewLine}#####");
                    ManagersSurgeonChoice = "";
                    continue;
                }
                if (int.Parse(ManagersSurgeonChoice) > OptionsList.Count || int.Parse(ManagersSurgeonChoice) < 1)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied value is out of range, please try again.");
                    Console.WriteLine("#####");
                    ManagersSurgeonChoice = "";
                    continue;
                }
            }
            while (ManagersSurgeonChoice == "");
        }
        /// <summary>
        /// this method is used for the floor manager to assign a surgeon to a patient
        /// </summary>
        public void AssignSurgeon()
        {
            FloorManager ActiveFloorManager = (FloorManager)UsersList[ActiveUserIndex];
            ActiveFloorManager.SelectPatient(PatientsAssignedRooms);
            //managersPatientChoice = ReformatOption(managersPatientChoice) ?? "";
            int PatientChosenIndex = FindUserIndex(ManagersPatientChoice);

            OptionsList = new List<string>();
            PatientsAssignedRooms.Remove(UsersList[PatientChosenIndex]);

            ActiveFloorManager.SelectSurgeon();
            ManagersSurgeonChoice = ReformatOption(ManagersSurgeonChoice) ?? "";
            int SurgeonChosenIndex = FindUserIndex(ManagersSurgeonChoice);
            OptionsList = new List<string>();
            DateTime ManagersDateChoice = GetDateTime();

            //declaring variables to be updated for patients
            ((Patient)UsersList[PatientChosenIndex]).CurrentSurgeon = UsersList[SurgeonChosenIndex].Name;
            ((Patient)UsersList[PatientChosenIndex]).SurgeryTime = ManagersDateChoice.ToString(DateFormat);
            //declaring variables to be updated for surgeons
            ((Surgeon)UsersList[SurgeonChosenIndex]).SurgerySchedule.Add(ManagersDateChoice, ManagersPatientChoice);
            ((Surgeon)UsersList[SurgeonChosenIndex]).PatientsAssignedToSurgeon.Add((Patient)UsersList[PatientChosenIndex]);

            Console.WriteLine($"Surgeon {ManagersSurgeonChoice} has been assigned to patient {ManagersPatientChoice}.");
            Console.WriteLine($"Surgery will take place on {ManagersDateChoice.ToString(DateFormat)}.");
        }
        /// <summary>
        /// this method is used to find the patients that have had surgery and are assigned the the manager logged in
        /// </summary>
        /// <param name="ManagerFloorNumber"> the name of the patient searching for</param>
        /// <returns>the updated list of patients that have had surgery assigned to the manager</returns>
        public static List<User> FindPatientsHadSurgeryAssignedToManager(int? ManagerFloorNumber)
        {
            foreach (Patient CurrentPatient in Patient.PatientHadSurgeryList.OrderBy(x => x.Name))
            {
                if (CurrentPatient.FloorNumber == ManagerFloorNumber)
                {
                    PatientsHadSurgeryAssignedToManager.Add(CurrentPatient);
                    continue;
                }
                else
                {
                    continue;
                }
            }
            return PatientsHadSurgeryAssignedToManager;
        }
        /// <summary>
        /// this method is used to unassign a room from a patient once the patient has been checked out and had surgery
        /// </summary>
        public void UnassignRoom()
        {
            FloorManager ActiveFloorManager = (FloorManager)UsersList[ActiveUserIndex];
            Console.WriteLine("Please select your patient:");
            string managersChoice;
            FindPatientsHadSurgeryAssignedToManager(ActiveFloorManager.FloorNumber);
            GiveStaffMemberOptions(PatientsHadSurgeryAssignedToManager);
            do
            {
                Console.WriteLine($"Please enter a choice between 1 and {OptionsList.Count}.");
                managersChoice = Console.ReadLine() ?? "";
                try
                {
                    int.Parse(managersChoice);
                }
                catch (Exception)
                {
                    Console.WriteLine($"#####{Environment.NewLine}#Error - Supplied value is not an integer, please try again.{Environment.NewLine}#####");
                    continue;
                }
                if (int.Parse(managersChoice) > OptionsList.Count || int.Parse(managersChoice) < 1)
                {
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied value is out of range, please try again.");
                    Console.WriteLine("#####");
                    managersChoice = "";
                    continue;
                }
                else
                {
                    managersChoice = ReformatOption(managersChoice) ?? "";
                }
            }
            while (managersChoice == "");

            int PatientChosenIndex = FindUserIndex(managersChoice);
            Console.WriteLine($"Room number {UsersList[PatientChosenIndex].RoomNumber} on floor {UsersList[PatientChosenIndex].FloorNumber} has been unassigned.");
            ActiveFloorManager.FloorRoomsAssigned.Remove(UsersList[PatientChosenIndex].RoomNumber);
            UsersList[PatientChosenIndex].RoomNumber = null;
            UsersList[PatientChosenIndex].FloorNumber = null;
            PatientsHadSurgeryAssignedToManager = new();
        }
    }
}
