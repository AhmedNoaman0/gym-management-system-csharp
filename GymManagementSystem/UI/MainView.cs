using GymManagementSystem.Services;
using GymManagementSystem.Test;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace GymManagementSystem.UI
{
    public class MainView
    {
        private readonly Menu menu;
        private readonly MemberServices memberServices;
        private readonly TrainersServices trainersServices;
        private readonly PaymentServices paymentServices;
        private readonly MemberMangement memberMangement;

        private readonly TrainersMangement trainersMangement;
        private readonly GateMangement gateMangement;
        private readonly BillingMangement billingMangement;


        private string title = "GYM MANAGEMENT SYSTEM (v1.0)";
        private List<string> options = new()
        {
            "Members Management",
            "Trainers Management",
            "Reception & Gate (Check-In / Check-Out)",
            "Personal Training (Private Sessions)",
            "Billing & Payments",
            "Exit System"
        };

        public MainView()
        {

            menu = new Menu(title, options,true);
            memberServices = new MemberServices();
            trainersServices = new TrainersServices();
            paymentServices = new PaymentServices();
            // we here use single instance, insted of create seperation/multi data
            memberMangement = new MemberMangement(memberServices,paymentServices);
            trainersMangement = new TrainersMangement(trainersServices, memberServices);
            gateMangement = new GateMangement(memberServices);
            billingMangement = new BillingMangement(memberServices, paymentServices);
            SeedData.Seed(memberServices, trainersServices,paymentServices);
        }

        public void Run() 
        {
            while (true)
            {
                int choice = menu.ShowAndGetChoice();
                switch (choice)
                {
                    case 1:
                        memberMangement.Run();
                        break;
                    case 2:
                        trainersMangement.Run();
                        break;
                    case 3:
                        gateMangement.Run();
                        break;
                    case 5:
                        billingMangement.Run();
                        break;
                    case 0:
                        return;
                }
            }

        }
    }
}
