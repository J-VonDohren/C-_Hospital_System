namespace Assignment2FinalDesign
{
    public abstract class Staff : User
    {

        public static List<int?> FloorsManaged = new List<int?>() ?? [];
        public static List<int> staffIDsActive = new List<int>() ?? [];
        public static string[] possibleSpecialities = ["General Surgeon", "Orthopaedic Surgeon", "Cardiothoracic Surgeon", "Neurosurgeon", ""];
        public List<User> PatientsAssignedToSurgeon = new List<User>() ?? [];
        public Dictionary<DateTime, string> SurgerySchedule = [];
        public static List<User> StaffedSurgeons = new List<User>() ?? [];
        public static List<string> OptionsList = new List<string>() ?? [];
        public List<int?> FloorRoomsAssigned = [];
        public List<User> PatientsAssignedRooms = new List<User>() ?? [];
        public Staff() : base()
        {
            staffIDsActive.Add(this.StaffID);
        }
        public Staff(string Name, string EmailAddress, string MobileNumber, string Password, int StaffID) : base(Name, EmailAddress, MobileNumber, Password)
        {
            this.StaffID = StaffID;
            staffIDsActive.Add(this.StaffID);
        }
        private int? staffID;
        public int StaffID
        {
            get
            {
                return staffID ?? -1;
            }
            set
            {
                if (value >= 100 && value <= 999 && (staffIDsActive.Contains(value) == false))
                {
                    staffID = value;
                }
                else if (value >= 100 && value <= 999 && (staffIDsActive.Contains(value) == true))
                {
                    throw new ArgumentException(String.Format($"#####{Environment.NewLine}#Error - StaffID is already registered, please try again.{Environment.NewLine}#####", StaffID));
                }
                else
                {
                    throw new ArgumentException(String.Format($"#####{Environment.NewLine}#Error - Supplied staff identification number is invalid, please try again.{Environment.NewLine}#####", StaffID));
                }
            }
        }
        private string? speciality;
        public string Speciality
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
                    throw new ArgumentException(String.Format($"#####{Environment.NewLine}#Error - Supplied speciality is invalid, please try again.{Environment.NewLine}#####", Speciality));
                }
            }
        }
        /// <summary>
        /// this method is used to give staff members a list to choice from when making decisions
        /// </summary>
        /// <param name="ActiveList"> The List that the relevant Users are contained</param>
        public void GiveStaffMemberOptions(List<User> ActiveList)
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
        /// <summary>
        /// this method is used to reformat the staff members choice which is an int to the corresponding name
        /// </summary>
        /// <param name="StaffMembersChoice"> this is an int used to represent the staff members choice</param>
        /// <returns></returns>
        public string? ReformatOption(string StaffMembersChoice)
        {
            foreach (string option in OptionsList)
            {
                if (option.StartsWith(StaffMembersChoice))
                {
                    string OptionsName = option.Trim(StaffMembersChoice.ToCharArray());
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
        /// <summary>
        /// this method is used to remove an already selected User from a relevant list
        /// </summary>
        /// <param name="ContainingList"> This is the List the User is located within</param>
        /// <param name="UserRemoving"> This is the Users Name to remove</param>
        public void RemoveUserChosen(List<User> ContainingList, string UserRemoving)
        {
            foreach (User user in ContainingList)
            {
                if (user.Name == UserRemoving)
                {
                    ContainingList.Remove(user);
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
        /// <summary>
        /// this method is used to find the User chosen when given a staff members choice
        /// </summary>
        /// <param name="CurrentList"> This is the list containing the chosen User</param>
        /// <param name="StaffMembersChoice"> This is the name of the User chosen</param>
        /// <returns> The object relevant to the staff members choice</returns>
        public User FindUserChosen(List<User> CurrentList, string StaffMembersChoice)
        {
            foreach (var CurrentUser in CurrentList)
            {
                if (CurrentUser.Name == StaffMembersChoice)
                {
                    return CurrentUser;
                }
                else
                {
                    continue;
                }
            }
            throw new Exception("No User Found");
        }
        /// <summary>
        /// this method is used to find the chosen users index within the UsersList in order to update any data modified
        /// </summary>
        /// <param name="StaffMembersChoice"> this is the name of the User chosen</param>
        /// <returns> the index number of the selected User</returns>
        public int FindUserIndex(string StaffMembersChoice)
        {
            foreach (var CurrentUser in UsersList)
            {
                if (CurrentUser.Name == StaffMembersChoice)
                {
                    return UsersList.IndexOf(CurrentUser);
                }
                else
                {
                    continue;
                }
            }
            return 0;
        }
    }
}
