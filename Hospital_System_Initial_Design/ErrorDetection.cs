using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Hospital
{
    class ErrorDetection
    {
        public static bool HasError  = false;
        public ErrorDetection() 
        {
        }
        public static void DetectStringError(string Input, string TypeofError)
        {
            if (TypeofError == "Integer check" && (Input.Any(Char.IsDigit) == false && Input.StartsWith("-") == false || Input.Any(char.IsLetter) == true))
            {
                IUser.RoomNumber = -1;
                HasError = true;
                Console.WriteLine("#####");
                Console.WriteLine($"#Error - Supplied value is not an integer, please try again.");
                Console.WriteLine("#####");
            }
            else if (TypeofError == "email" && Input == "-1")
            {
                Console.WriteLine("#####");
                Console.WriteLine($"#Error - Email is already registered, please try again.");
                Console.WriteLine("#####");
            }
            else if (Input == "")
                {
                Console.WriteLine("#####");
                Console.WriteLine($"#Error - Supplied {TypeofError} is invalid, please try again.");
                Console.WriteLine("#####");
                }
        }
        public static void DetectIntError(int input, string TypeofError)
        {
            if (TypeofError == "staff identification number" && input == -2)
            {
                Console.WriteLine("#####");
                Console.WriteLine($"#Error - StaffID is already registered, please try again.");
                Console.WriteLine("#####");
            }

            else if (TypeofError == "floor number" && input == -2)
            {
                Console.WriteLine("#####");
                Console.WriteLine($"#Error - Floor has been assigned to another floor manager, please try again.");
                Console.WriteLine("#####");
            }
            else if (TypeofError == "floor number" && input == -1)
            {
                Console.WriteLine("#####");
                Console.WriteLine($"#Error - Supplied floor is invalid, please try again.");
                Console.WriteLine("#####");
            }
            else if (TypeofError == "room number" && input == -1)
            {
                Console.WriteLine("#####");
                Console.WriteLine($"#Error - Supplied value is out of range, please try again.");
                Console.WriteLine("#####");
            }
            else if (input == -1 && TypeofError != "floor number")
            {
                Console.WriteLine("#####");
                Console.WriteLine($"#Error - Supplied {TypeofError} is invalid, please try again.");
                Console.WriteLine("#####");

            }           
            
        }
    }
}
