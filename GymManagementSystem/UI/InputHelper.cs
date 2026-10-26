using GymManagementSystem.Enums;
using GymManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystem.UI
{
    public class InputHelper
    {
        #region
        public void WarningMessage(string message) 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public void YellowMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public void GreenMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public void CyanMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public void MagentaMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void FoundMessage(string str) 
        {
            str = str.ToUpper();
            GreenMessage("--------------------------------------------------\n" +
                        $">> {str} DETAILS FOUND\n" +
                        "--------------------------------------------------");
        }
        #endregion
        
        
        
        
        
        public string ReadString(string type)
        {
            while (true)
            {
                Console.Write($"Enter {type} ");
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();
                WarningMessage("Invalid input! Please try again");
            }
        }

        public int ReadInt(string type) 
        {
            while (true)
            {
                Console.Write($"Enter {type} ");
                string? input = Console.ReadLine();
                int.TryParse(input, out int value);
                //TODO: logic before return
                return value;
            }
        }

        public DateTime ReadDate()
        {
            while (true)
            {
                Console.Write("Enter Birth Date (yyyy-MM-dd): ");
                string input = Console.ReadLine();

                if (DateTime.TryParse(input, out DateTime date))
                    return date;
                WarningMessage("Invalid date format entered.");
            }
        }
        public MembershipType ReadMemberShipType()
        {
            while (true)
            {
                Console.WriteLine("Enter Number from the following:");
                foreach (var type in Enum.GetValues<MembershipType>())
                {
                    Console.WriteLine($"[{(int)type}]. {type}");
                }
                Console.Write("Enter choice: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= 0 && Enum.IsDefined(typeof(MembershipType), choice))
                {
                    return (MembershipType)choice;
                }

                WarningMessage("Invalid input! Please enter a valid number");
            }
        }
        public PaymentMethod ReadPaymentMethod()
        {
            while (true)
            {
                Console.WriteLine("Enter Number from the following:");
                foreach (var type in Enum.GetValues<PaymentMethod>())
                {
                    Console.WriteLine($"[{(int)type}]. {type}");
                }

                Console.Write("Selection: ");
                string? input = Console.ReadLine();
                
                if (int.TryParse(input, out int choice) && choice >= 0 && Enum.IsDefined(typeof(PaymentMethod), choice))
                {
                    return (PaymentMethod)choice;
                }
    
                WarningMessage("Invalid input! Please enter a valid number");
            }
        }
        public PaymentStatus ReadPaymentStatus()
        {
            while (true)
            {
                Console.WriteLine("Enter Number from the following:");
                foreach (var status in Enum.GetValues<PaymentStatus>())
                {
                    Console.WriteLine($"[{(int)status}]. {status}");
                }
                Console.Write("Selection: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 0 && Enum.IsDefined(typeof(PaymentStatus), choice))
                {
                    return (PaymentStatus)choice;
                }
                WarningMessage("Invalid input! Please enter a valid number");
            }
        }
        // Trainer
        public TrainerSpecialization ReadTrainerSpecialization() 
        {
            Console.WriteLine("Enter Number from the following:");
            foreach (var item in Enum.GetValues<TrainerSpecialization>())
            {
                Console.WriteLine($"[{(int)item}]. {item}");
            }
            while (true)
            {
                Console.Write("Selection: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && Enum.IsDefined(typeof(TrainerSpecialization), choice))
                {
                    return (TrainerSpecialization)choice;
                }
                WarningMessage("Invalid input! Please enter a valid number");
            }
        }

    }
}
