namespace Assignment2FinalDesign
{
    // this class is relevant to handling the Menu UI
    class Menu
    {
        // this method is to display the initialised menu
        public static void InitialiseMenu()
        {
            Console.WriteLine("Please choose from the menu below:");
            Console.WriteLine("1. Login as a registered user");
            Console.WriteLine("2. Register as a new user");
            Console.WriteLine("3. Exit");
            Console.WriteLine("Please enter a choice between 1 and 3.");
        }
        // this menu is used to see what type of User the ActiveUser is and will then display the options that user has access to
        public void UserOptionMenu()
        {
            Menu CurrentMenu = new Menu();
            if (User.UsersList[User.ActiveUserIndex].GetType() == typeof(Patient))
            {
                CurrentMenu.PatientOptionMenu();
            }
            else if (User.UsersList[User.ActiveUserIndex].GetType() == typeof(Surgeon))
            {
                CurrentMenu.SurgeonOptionMenu();
            }
            else if (User.UsersList[User.ActiveUserIndex].GetType() == typeof(FloorManager))
            {
                CurrentMenu.FloorManagerOptionMenu();
            }
        }
        /// <summary>
        /// the following methods are going to be used for User registration related menu UI
        /// the methods contained are the base Register User Option menu and the Register Staff member Option menu
        /// </summary>
        //this method is used to show the User the User Registration menu
        public static void RegisterUserMenu()
        {
            Menu CurrentMenu = new Menu();
            Console.WriteLine();
            Console.WriteLine("Register as which type of user:");
            Console.WriteLine("1. Patient");
            Console.WriteLine("2. Staff");
            Console.WriteLine("3. Return to the first menu");
            Console.WriteLine("Please enter a choice between 1 and 3.");

            switch (Console.ReadLine())
            {
                // this case is for registering patients
                case "1":
                    CurrentMenu.ConstructPatient();
                    Program.IsOn2ndMenu = false;
                    break;
                // this case is for registering staff members
                case "2":
                    Menu.RegisterStaffMenu();
                    break;
                case "3":
                    Program.IsOn2ndMenu = false;
                    break;
                default:
                    Program.IsOn2ndMenu = false;
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Invalid Menu Option, please try again.");
                    Console.WriteLine("#####");
                    break;
            }
        }
        // this method is used to show the User the Register Staff member Option menu
        public static void RegisterStaffMenu()
        {
            Menu CurrentMenu = new Menu();
            Console.WriteLine();
            Console.WriteLine("Register as which type of staff:");
            Console.WriteLine("1. Floor manager");
            Console.WriteLine("2. Surgeon");
            Console.WriteLine("3. Return to the first menu");
            Console.WriteLine("Please enter a choice between 1 and 3.");

            switch (Console.ReadLine())
            {
                // this case is for registering Floor Managers
                case "1":
                    if (FloorManager.RegisteredFloorManagerList.Count >= 6)
                    {
                        Console.WriteLine("#####");
                        Console.WriteLine("#Error - All floors are assigned.");
                        Console.WriteLine("#####");
                        Program.IsOn2ndMenu = false;
                        break;
                    }
                    else
                    {
                        CurrentMenu.ConstructFloorManager();
                        Program.IsOn2ndMenu = false;
                        break;
                    }
                // this case is for registering Surgeons
                case "2":
                    CurrentMenu.ConstructSurgeon();
                    Program.IsOn2ndMenu = false;
                    break;
                // this case sends user back to initial menu
                case "3":
                    Program.IsOn2ndMenu = false;
                    break;

                default:
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Invalid Menu Option, please try again.");
                    Console.WriteLine("#####");
                    break;
            }
        }
        /// <summary>
        /// the following methods are going to be used for Patient related menu UI
        /// the methods contained are the Patient Option menu and the construct a new patient menu
        /// </summary>
        // this method is used to display the options a Patient can select and allows the patient to select the options shown
        public void PatientOptionMenu()
        {
            Patient ActivePatient = (Patient)User.UsersList[User.ActiveUserIndex];
            Console.WriteLine();
            Console.WriteLine("Patient Menu.");
            Console.WriteLine("Please choose from the menu below:");
            Console.WriteLine("1. Display my details");
            Console.WriteLine("2. Change password");
            if (ActivePatient.IsCheckedIn == true)
            {
                Console.WriteLine("3. Check out");
            }
            else
            {
                Console.WriteLine("3. Check in");
            }
            Console.WriteLine("4. See room");
            Console.WriteLine("5. See surgeon");
            Console.WriteLine("6. See surgery date and time");
            Console.WriteLine("7. Log out");
            Console.WriteLine("Please enter a choice between 1 and 7.");

            switch (Console.ReadLine())
            {

                case "1":
                    ActivePatient.DisplayInfo();
                    break;
                case "2":
                    ActivePatient.ChangePassword();
                    break;
                case "3":
                    ActivePatient.CheckInorOut();
                    break;
                case "4":
                    ActivePatient.SeeRoomAssigned();
                    break;
                case "5":
                    ActivePatient.SeeAllocatedSurgeon();
                    break;
                case "6":
                    ActivePatient.SeeSurgeryDateTime();
                    break;
                case "7":
                    ActivePatient.LogOut();

                    break;
                default:
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied Menu Input is invalid, please try again.");
                    Console.WriteLine("#####");
                    break;
            }
        }
        // this method is used to accept user input and constructs a patient object based off that input
        public void ConstructPatient()
        {
            Patient patient = new();
            Console.WriteLine("Registering as a patient.");
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your name:");
                    patient.Name = Console.ReadLine() ?? "";
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your age:");
                    string age = Console.ReadLine() ?? "";
                    patient.Age = int.Parse(age);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (Exception)
                {
                    Console.WriteLine($"#####{Environment.NewLine}#Error - Supplied value is not an integer, please try again.{Environment.NewLine}#####");
                    continue;
                }
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your mobile number:");
                    patient.MobileNumber = Console.ReadLine() ?? "";
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your email:");
                    patient.EmailAddress = Console.ReadLine() ?? "";
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your password:");
                    patient.Password = Console.ReadLine() ?? "";
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                break;
            }
            Patient Patient = new Patient(patient.Name, patient.Age, patient.EmailAddress, patient.MobileNumber, patient.Password);
            Console.WriteLine($"{Patient.Name} is registered as a patient.");
        }
        /// <summary>
        ///the following methods are going to be used for Floor manager related menu UI
        /// the methods contained are the Floor manager Option menu and the construct a new Surgeon menu
        /// </summary>
        // this method is used to display the options a Floor manager can select and allows the Floor manager to select the options shown
        public void FloorManagerOptionMenu()
        {
            FloorManager ActiveFloorManager = (FloorManager)User.UsersList[User.ActiveUserIndex];
            Console.WriteLine();
            Console.WriteLine("Floor Manager Menu.");
            Console.WriteLine("Please choose from the menu below:");
            Console.WriteLine("1. Display my details");
            Console.WriteLine("2. Change password");
            Console.WriteLine("3. Assign room to patient");
            Console.WriteLine("4. Assign surgery");
            Console.WriteLine("5. Unassign room");
            Console.WriteLine("6. Log out");
            Console.WriteLine("Please enter a choice between 1 and 6.");

            switch (Console.ReadLine())
            {
                case "1":
                    ActiveFloorManager.DisplayInfo();
                    break;
                case "2":
                    ActiveFloorManager.ChangePassword();
                    break;
                case "3":
                    if (Patient.RegisteredPatientsList.Count == 0)
                    {
                        Console.WriteLine("There are no registered patients.");
                        break;
                    }
                    else if (Patient.CheckedInPatientsList.Count == 0)
                    {
                        Console.WriteLine("There are no checked in patients.");
                        break;
                    }
                    else if (ActiveFloorManager.FloorRoomsAssigned.Count == 10) // if 10 rooms are assigned then the floor has no more rooms available to assign
                    {
                        Console.WriteLine("#####");
                        Console.WriteLine("#Error - All rooms on this floor are assigned.");
                        Console.WriteLine("#####");
                        break;
                    }
                    else
                    {
                        ActiveFloorManager.AssignRoom();
                        break;
                    }
                case "4":
                    if (Patient.RegisteredPatientsList.Count == 0)
                    {
                        Console.WriteLine("There are no registered patients.");
                        break;
                    }
                    else if (ActiveFloorManager.FloorRoomsAssigned.Count == 0)
                    {
                        Console.WriteLine("There are no patients ready for surgery.");
                        break;
                    }
                    else
                    {
                        ActiveFloorManager.AssignSurgeon();
                        break;
                    }
                case "5":
                    if (Patient.PatientHadSurgeryList.Count == 0)
                    {
                        Console.WriteLine("There are no patients ready to have their rooms unassigned.");
                        break;
                    }
                    ActiveFloorManager.UnassignRoom();
                    break;
                case "6":
                    ActiveFloorManager.LogOut();
                    break;
                default:
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied Menu Input is invalid, please try again.");
                    Console.WriteLine("#####");
                    break;
            }
        }
        // this method is used to accept user input and constructs a Floor manager object based off that input
        public void ConstructFloorManager()
        {
            FloorManager floorManager = new FloorManager();
            Console.WriteLine("Registering as a floor manager.");
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your name:");
                    floorManager.Name = Console.ReadLine() ?? "";
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your age:");
                    string age = Console.ReadLine() ?? "";
                    floorManager.Age = int.Parse(age);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (Exception)
                {
                    Console.WriteLine($"#####{Environment.NewLine}#Error - Supplied value is not an integer, please try again.{Environment.NewLine}#####");
                    continue;
                }
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your mobile number:");
                    floorManager.MobileNumber = Console.ReadLine() ?? "";
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your email:");
                    floorManager.EmailAddress = Console.ReadLine() ?? "";
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your password:");
                    floorManager.Password = Console.ReadLine() ?? "";
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your staff ID:");
                    string staffID = Console.ReadLine() ?? "-1";
                    floorManager.StaffID = int.Parse(staffID);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (Exception)
                {
                    Console.WriteLine($"#####{Environment.NewLine}#Error - Supplied value is not an integer, please try again.{Environment.NewLine}#####");
                    continue;
                }
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your floor number:");
                    string floorNumber = Console.ReadLine() ?? "-1";
                    floorManager.FloorNumber = int.Parse(floorNumber);
                    if (Staff.FloorsManaged.Contains(floorManager.FloorNumber) == true)
                    {
                        floorManager.FloorNumber = null;
                        Console.WriteLine("#####");
                        Console.WriteLine($"#Error - Floor has been assigned to another floor manager, please try again.");
                        Console.WriteLine("#####");
                        continue;
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (Exception)
                {
                    Console.WriteLine($"#####{Environment.NewLine}#Error - Supplied value is not an integer, please try again.{Environment.NewLine}#####");
                    continue;
                }
                break;
            }
            // rooms left to assign is set to 10 initially because there are no  rooms taken yet
            List<int?> FloorRoomsAssigned = [];
            FloorManager NEWfloorManager = new FloorManager(floorManager.Name, floorManager.Age, floorManager.EmailAddress, floorManager.MobileNumber, floorManager.Password, floorManager.StaffID, floorManager.FloorNumber);
            Console.WriteLine($"{floorManager.Name} is registered as a floor manager.");
        }
        /// <summary>
        /// the following methods are going to be used for Surgeon related menu UI
        /// the methods contained are the Surgeon Option menu and the construct a new Surgeon menu
        /// </summary>
        // this method is used to display the options a Surgeon can select and allows the Surgeon to select the options shown
        public void SurgeonOptionMenu()
        {
            Surgeon ActiveSurgeon = (Surgeon)User.UsersList[User.ActiveUserIndex];

            Console.WriteLine();
            Console.WriteLine("Surgeon Menu.");
            Console.WriteLine("Please choose from the menu below:");
            Console.WriteLine("1. Display my details");
            Console.WriteLine("2. Change password");
            Console.WriteLine("3. See your list of patients");
            Console.WriteLine("4. See your schedule");
            Console.WriteLine("5. Perform surgery");
            Console.WriteLine("6. Log out");
            Console.WriteLine("Please enter a choice between 1 and 6.");

            switch (Console.ReadLine())
            {
                case "1":
                    ActiveSurgeon.DisplayInfo();
                    break;
                case "2":
                    ActiveSurgeon.ChangePassword();
                    break;
                case "3":
                    Console.WriteLine("Your Patients.");
                    if (ActiveSurgeon.PatientsAssignedToSurgeon.Count == 0)
                    {
                        Console.WriteLine("You do not have any patients assigned.");
                        break;
                    }
                    ActiveSurgeon.SeePatientList();
                    break;
                case "4":
                    Console.WriteLine("Your schedule.");
                    if (ActiveSurgeon.PatientsAssignedToSurgeon.Count() == 0)
                    {
                        Console.WriteLine("You do not have any patients assigned.");
                        break;
                    }
                    else
                    {
                        ActiveSurgeon.SeeSchedule();
                    }
                    break;
                case "5":
                    ActiveSurgeon.PerformSurgery();
                    break;
                case "6":
                    ActiveSurgeon.LogOut();

                    break;
                default:
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Supplied Menu Input is invalid, please try again.");
                    Console.WriteLine("#####");
                    break;
            }
        }
        // this method is used to display the options a surgeon can select to specialise in
        public static void RegisterSpeciality()
        {
            Console.WriteLine("Please choose your speciality:");
            Console.WriteLine("1. General Surgeon");
            Console.WriteLine("2. Orthopaedic Surgeon");
            Console.WriteLine("3. Cardiothoracic Surgeon");
            Console.WriteLine("4. Neurosurgeon");
            Console.WriteLine("Please enter a choice between 1 and 4.");
        }
        // this method is used to accept user input and constructs a Surgeon object based off that input
        public void ConstructSurgeon()
        {
            Surgeon surgeon = new Surgeon();
            Console.WriteLine("Registering as a surgeon.");
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your name:");
                    surgeon.Name = Console.ReadLine() ?? "";
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your age:");
                    string age = Console.ReadLine() ?? "";
                    surgeon.Age = int.Parse(age);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (Exception)
                {
                    Console.WriteLine($"#####{Environment.NewLine}#Error - Supplied value is not an integer, please try again.{Environment.NewLine}#####");
                    continue;
                }
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your mobile number:");
                    surgeon.MobileNumber = Console.ReadLine() ?? "";
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your email:");
                    surgeon.EmailAddress = Console.ReadLine() ?? "";
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your password:");
                    surgeon.Password = Console.ReadLine() ?? "";
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                break;
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter in your staff ID:");
                    string staffID = Console.ReadLine() ?? "-1";
                    surgeon.StaffID = int.Parse(staffID);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (Exception)
                {
                    Console.WriteLine($"#####{Environment.NewLine}#Error - Supplied value is not an integer, please try again.{Environment.NewLine}#####");
                    continue;
                }
                break;
            }

            Menu.RegisterSpeciality();
            switch (Console.ReadLine())
            {
                case "1":
                    surgeon.Speciality = "General Surgeon";
                    break;
                case "2":
                    surgeon.Speciality = "Orthopaedic Surgeon";
                    break;
                case "3":
                    surgeon.Speciality = "Cardiothoracic Surgeon";
                    break;
                case "4":
                    surgeon.Speciality = "Neurosurgeon";
                    break;
                default:
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Non-valid speciality type, please try again.");
                    Console.WriteLine("#####");
                    Menu.RegisterSpeciality();
                    break;
            }
            Surgeon Surgeon = new Surgeon(surgeon.Name, surgeon.Age, surgeon.EmailAddress, surgeon.MobileNumber, surgeon.Password, surgeon.StaffID, surgeon.Speciality);
            Console.WriteLine($"{surgeon.Name} is registered as a surgeon.");
        }
    }
}