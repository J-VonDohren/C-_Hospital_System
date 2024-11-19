using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Hospital
{
    class MenuChildClass : IUser
    {

        public MenuChildClass()
        {
        }

        public static void InitialiseMenu()
        {
            Console.WriteLine("Please choose from the menu below:");
            Console.WriteLine("1. Login as a registered user");
            Console.WriteLine("2. Register as a new user");
            Console.WriteLine("3. Exit");
            Console.WriteLine("Please enter a choice between 1 and 3.");
        }
        public static void RegisterUserMenu()
        {
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
                    User.ConstructPatient();
                    //construct patient
                    Program.IsOn2ndMenu = false;
                    break;
                // this case is for registering staff members
                case "2":
                    MenuChildClass.RegisterStaffMenu();
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
        public static void RegisterStaffMenu()
        {
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
                    if (User.RegisteredFloorManagerList.Count >= 6)
                    {
                        Console.WriteLine("#####");
                        Console.WriteLine("#Error - All floors are assigned.");
                        Console.WriteLine("#####");
                        Program.IsOn2ndMenu = false;
                        break;
                    }
                    else
                    {
                        User.ConstructFloorManager();
                        Program.IsOn2ndMenu = false;
                        break;
                    }
                // this case is for registering Surgeons
                case "2":
                    User.ConstructSurgeon();
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
        public static void RegisterSpeciality()
        {
            Console.WriteLine("Please choose your speciality:");
            Console.WriteLine("1. General Surgeon");
            Console.WriteLine("2. Orthopaedic Surgeon");
            Console.WriteLine("3. Cardiothoracic Surgeon");
            Console.WriteLine("4. Neurosurgeon");
            Console.WriteLine("Please enter a choice between 1 and 4.");
            switch (Console.ReadLine())
            {
                case "1":
                    IUser.Speciality = "General Surgeon";
                    break;
                case "2":
                    IUser.Speciality = "Orthopaedic Surgeon";
                    break;
                case "3":
                    IUser.Speciality = "Cardiothoracic Surgeon";
                    break;
                case "4":
                    IUser.Speciality = "Neurosurgeon";
                    break;
                default:
                    Console.WriteLine("#####");
                    Console.WriteLine("#Error - Non-valid speciality type, please try again.");
                    Console.WriteLine("#####");
                    MenuChildClass.RegisterSpeciality();
                    break;
            }
        }
        public static void UserOptionMenu()
        {
            if (User.ActiveUser.UserType == "Patient")
            {
                Console.WriteLine();
                Console.WriteLine("Patient Menu.");
                Console.WriteLine("Please choose from the menu below:");
                Console.WriteLine("1. Display my details");
                Console.WriteLine("2. Change password");
                if (User.ActiveUser.IsCheckedIn == true)
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
                        User.DisplayInfo();
                        break;
                    case "2":
                        Console.WriteLine("Enter new password:");
                        string NewPassword = Console.ReadLine() ?? "";
                        User.ChangePassword(NewPassword);
                        break;
                    case "3":
                        User.CheckInorOut();
                        break;
                    case "4":
                        User.SeeRoomAssigned();
                        break;
                    case "5":
                        User.SeeAllocatedSurgeon();
                        break;
                    case "6":
                        User.SeeSurgeryDateTime();
                        break;
                    case "7":
                        User.LogOut();
                        break;
                    default:
                        Console.WriteLine("#####");
                        Console.WriteLine("#Error - Supplied Menu Input is invalid, please try again.");
                        Console.WriteLine("#####");
                        break;
                }
            }
            if (User.ActiveUser.UserType == "Floor manager")
            {
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
                            User.DisplayInfo();
                            break;
                        case "2":
                            Console.WriteLine("Enter new password:");
                            string NewPassword = Console.ReadLine() ?? "";
                            User.ChangePassword(NewPassword);
                            break;
                        case "3":
                            User.FillPatientsList("CheckedInPatientsListNoRoom");
                            if (User.RegisteredPatientList.Count == 0)
                            {
                                Console.WriteLine("There are no registered patients.");
                                break;
                            }
                            else if (User.CheckedInPatientsList.Count == 0)
                            {
                                Console.WriteLine("There are no checked in patients.");
                                break;
                            }
                            else if (User.ActiveUser.RoomsLeftToAssign == 0)
                            {
                            Console.WriteLine("#####");
                            Console.WriteLine("#Error - All rooms on this floor are assigned.");
                            Console.WriteLine("#####");
                            break;
                            }
                            else
                            {
                                User.AssignRoom();
                                break;
                            }
                        case "4":
                            User.FillPatientsList("PatientsAssignedRooms");
                            if (User.RegisteredPatientList.Count == 0)
                            {
                                Console.WriteLine("There are no registered patients.");
                                break;
                            }
                            else if (User.PatientsAssignedRooms.Count == 0)
                            {
                                Console.WriteLine("There are no patients ready for surgery.");
                                break;
                            }
                            User.AssignSurgeon();
                                break;
                        case "5":
                            if (User.PatientHadSurgeryList.Count == 0)
                            {
                            Console.WriteLine("There are no patients ready to have their rooms unassigned.");
                            break;
                            }
                            User.UnassignRoom();
                            break;
                        case "6":
                            User.LogOut();
                            break;
                        default:
                            Console.WriteLine("#####");
                            Console.WriteLine("#Error - Supplied Menu Input is invalid, please try again.");
                            Console.WriteLine("#####");
                            break;
                    }
            }
            if (User.ActiveUser.UserType == "Surgeon")
            {
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
                        User.DisplayInfo();
                        break;
                    case "2":
                        Console.WriteLine("Enter new password:");
                        string NewPassword = Console.ReadLine() ?? "";
                        User.ChangePassword(NewPassword);
                        break;
                    case "3":
                        Console.WriteLine("Your Patients.");
                        if (User.ActiveUser.PatientsAssignedToSurgeon.Count == 0)
                        {
                            Console.WriteLine("You do not have any patients assigned.");
                            break;
                        }
                        User.SeePatientList();
                        break;
                    case "4":
                        Console.WriteLine("Your schedule.");
                        User.FillPatientsList("CheckedInPatientsList");
                        if (User.CheckedInPatientsList.Count() == 0)
                        {
                            Console.WriteLine("You do not have any patients assigned.");
                            break;
                        }
                        else
                        {
                            User.SeeSchedule();
                        }
                        break;
                    case "5":
                        User.PerformSurgery();
                        break;
                    case "6":
                        User.LogOut();
                        break;
                    default:
                        Console.WriteLine("#####");
                        Console.WriteLine("#Error - Supplied Menu Input is invalid, please try again.");
                        Console.WriteLine("#####");
                        break;
                }
            }
        }
    }
}
