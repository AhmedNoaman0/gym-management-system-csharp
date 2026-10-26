using GymManagementSystem.Enums;
using GymManagementSystem.Models;
using GymManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GymManagementSystem.UI
{
    internal class TrainersMangement
    {
        readonly Menu menu;
        readonly TrainersServices trainers_services;
        readonly MemberServices memberServices;


        readonly InputHelper inputHelper;
        public TrainersMangement(TrainersServices _trainersservices, MemberServices _memberservices)
        {
            string title = "TRAINERS MANAGEMENT";
            List<string> options = new List<string>();
            options.Add("Add New Trainer");
            options.Add("View All Trainers");
            options.Add("Find Trainer by ID");
            options.Add("Assign Member to Trainer");
            options.Add("View Trainer's Trainees List");
            options.Add("Back to Main Menu");
            menu = new Menu(title, options);

            inputHelper = new InputHelper();
            trainers_services = _trainersservices;
            memberServices = _memberservices;
        }



        private void AddNewTrainer() 
        {
            string name = inputHelper.ReadString("Name");
            string phone = inputHelper.ReadString("Phone");
            DateTime birth_date = inputHelper.ReadDate();
            TrainerSpecialization trainerSpecialization = inputHelper.ReadTrainerSpecialization();
            
            trainers_services.AddTrainer(name, phone, birth_date, trainerSpecialization);
            inputHelper.GreenMessage("[SUCCESS] Trainer added successfully!");

        }
        private void ViewAllTrainers() 
        {
            var trainers = trainers_services.GetAllTrainers();
            foreach (var trainer in trainers)
            {
                Console.WriteLine(trainer.ToString());
            }
        }
        private void FindTrainer() 
        {
            while (true)
            {
                string input = inputHelper.ReadString("Trainer Id or Name or Phone");
                if (input == "0") return;

                var trainer = trainers_services.FindTrainer(input);
                if (trainer != null)
                {
                    inputHelper.FoundMessage("TRAINER");
                    Console.WriteLine(trainer.ToString());
                    break;
                }
                inputHelper.WarningMessage("No trainer found matching your search.");

            }
        }
        private void AssignMemberToTrainer(int trails = 3) 
        {
            Member member = new Member();
            Trainer trainer = new Trainer();

            string input;
            while ( trails-- > 0)
            {
                input = inputHelper.ReadString("Member Id or Name or Phone");
                member = memberServices.SearchMember(input);

                if (member != null)
                    break;

                inputHelper.WarningMessage("No member found matching your search.");
            }

            if (member == null)
            {
                Console.WriteLine("Sorry, you've entered it incorrectly 3 times. Please try again later.");
                return;
            }

            inputHelper.FoundMessage($"Verifying Member... [FOUND: {member.Name}]");
            trails = 3;

            while (trails-- > 0)
            {
                input = inputHelper.ReadString("Trainer Id or Name or Phone");
                trainer = trainers_services.FindTrainer(input);

                if (trainer != null)
                    break;

                    inputHelper.WarningMessage("No trainer found matching your search.");
            }
            if (trainer == null)
            {
                Console.WriteLine("Sorry, you've entered it incorrectly 3 times. Please try again later.");
                return;
            }
            inputHelper.FoundMessage($"Verifying Trainer... [FOUND: Captain {trainer.Name}]");
            Console.WriteLine("--------------------------------------------------");
            inputHelper.CyanMessage($"Confirm Assignment of Member [{member.Name}] to Trainer [Captain {trainer.Name}]? (Y/N)");
            Console.Write("Selection: ");
            input = Console.ReadLine();
            if (input.ToUpper() == "Y")
            {
                trainers_services.AssignMemberToTrainer(trainer.Id, member);

                inputHelper.GreenMessage($">> [SUCCESS] Member {member.Name} has been assigned to Captain {trainer.Name}.");
                Console.WriteLine($"Current Trainees Count for Captain {trainer.Name}: {trainer.Trainees.Count}");

                return;
            }
            Console.WriteLine("Operation not saved.");

        }

        private void ViewAllTrainees() 
        {
            int input = inputHelper.ReadInt("ID");
            Console.WriteLine();
            Trainer trainer = new Trainer();
            var trainees = trainers_services.GetAllTrainees(input, ref trainer);
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Trainer: Captain {trainer.Name} ({trainer.Specialization}) ");
            Console.WriteLine($"Total Trainees: {trainer.Trainees.Count}");
            inputHelper.MagentaMessage
                ("--------------------------------------------------------------------\n"
                + "ID | Member Name | Phone | Membership Expire date\n"
                + "--------------------------------------------------------------------");
            foreach (var item in trainees)
            {
                Console.WriteLine($"{item.Id}    | {item.Name}          | {item.Phone}        | ({item.Membership.EndDate:d})");
            }
            Console.WriteLine("==================================================");
        }


        public void Run() 
        {
            while (true)
            {
                int choice = menu.ShowAndGetChoice();
                switch (choice) 
                {
                    case 1:
                        AddNewTrainer();
                        break;
                    case 2:
                        ViewAllTrainers();
                        break;
                    case 3:
                        FindTrainer();
                        break;
                    case 4:
                        AssignMemberToTrainer();
                        break;
                    case 5:
                        ViewAllTrainees();
                        break;
                    case 0:
                        return;

                }
            }

        }
    
    }
}
