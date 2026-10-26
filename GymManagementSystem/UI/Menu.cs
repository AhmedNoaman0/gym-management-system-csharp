using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystem.UI
{
    public class Menu
    {
        private readonly string title;
        private readonly bool IsMainView;

        private readonly List<string> options = new();
        public Menu(string _title, List<string> _options, bool is_main_view = false)
        {
            title = _title;
            options = _options;
            IsMainView = is_main_view;
        }
        public int ShowAndGetChoice() 
        {
            while (true)
            {
                if(IsMainView)
                    Console.ForegroundColor = ConsoleColor.Blue;
                else
                    Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("==================================================");
                Console.WriteLine($"\t\t{title}");
                Console.WriteLine("==================================================");
                Console.ResetColor();
                for (int i = 0; i < options.Count - 1; i++)
                {
                    Console.WriteLine($"[{i + 1}] {options[i]}");
                }
                Console.WriteLine($"[0] {options[options.Count - 1]}");
                Console.Write("Enter choice: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 0 && choice < options.Count)
                    return choice;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Invalid input! Please enter a number betweeen 0 - {options.Count - 1}");
                Console.ResetColor();
            }
        }
    
    }
}
