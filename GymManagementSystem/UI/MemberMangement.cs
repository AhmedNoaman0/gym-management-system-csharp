using GymManagementSystem.Enums;
using GymManagementSystem.Models;
using GymManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystem.UI
{
    internal class MemberMangement
    {
        private readonly Menu menu;
        private readonly InputHelper inputHelper;
        public MemberServices member_services;
        public PaymentServices paymentServices;

        private readonly string title;
        private readonly List<string> options;

        public MemberMangement(MemberServices memberservices, PaymentServices paymentServices)
        {
            title = "Member Mangement";
            options = new()
            {
                "Add New Member (Personal Info + Initial Plan)",
                "View All Members",
                "Search Member (By ID or Phone)",
                "Renew / Upgrade Membership",
                "Delete Member",
                "Back to Main Menu"
            };
            menu = new Menu(title, options);
            inputHelper = new InputHelper();
            member_services = memberservices;
            this.paymentServices = paymentServices;
        }

        private void AddNewMember() 
        {
            inputHelper.YellowMessage("-- Flow: Add new Member --");

            // Member data
            string name = inputHelper.ReadString("Name");
            string phone = inputHelper.ReadString("Phone");
            DateTime birth_date = inputHelper.ReadDate();

            MembershipType membershipType = inputHelper.ReadMemberShipType();
            Membership membership = new Membership(membershipType);
            // Payment
            PaymentMethod paymentMethod = inputHelper.ReadPaymentMethod();
            Payment payment = paymentServices.CreatePayment(membership.Price, paymentMethod);
            inputHelper.CyanMessage($"Amount to Pay: {payment.Price}");
            payment.PaymentStatus = inputHelper.ReadPaymentStatus();
            if (payment.PaymentStatus == PaymentStatus.Completed)
            {
                Member member = member_services.CreateMember(name, phone, birth_date, membership, payment);
                payment.Member_id = member.Id;
                inputHelper.GreenMessage("[SUCCESS] Member added successfully!");
            }
            //TODO: Handle the pending/failed Code
            inputHelper.MagentaMessage(payment.ToString());
        }
        private void ViewAllMembers() 
        {
            var Members = member_services.GetAllMembers();
            foreach(var mebmer in Members) 
            {
                Console.WriteLine(mebmer.ToString());
            }
        }
        private int ViewAllMembersReadId(string prompt) 
        {
            inputHelper.CyanMessage($"Which member's membership do you want to {prompt}?");
            ViewAllMembers();
            return inputHelper.ReadInt("Id");
        }
        private void SearchMember() 
        {
            string input = inputHelper.ReadString("Member name or id or phone");
            var member = member_services.SearchMember(input);
            if (member == null) 
            {
                inputHelper.WarningMessage("No member found matching your search.");
                SearchMember();
            }
            inputHelper.FoundMessage("MEMEBER");
            Console.WriteLine(member.ToString());
        }
        private void UpgradeMembership() 
        {
            int id = ViewAllMembersReadId("upgrade");

            MembershipType membershipType = inputHelper.ReadMemberShipType();
            Membership membership = new Membership(membershipType);
            member_services.UpgradeMembership(membership,id);
        }
        private void DeleteMember()
        {
            int id = ViewAllMembersReadId("delete");
            member_services.DeleteMember(id);
        }

        public void Run() 
        {
            while (true)
            {
                int choice = menu.ShowAndGetChoice();
                switch (choice) 
                {
                    case 1:
                        AddNewMember();
                        break;
                    case 2:
                        ViewAllMembers();
                        break;
                    case 3:
                        SearchMember();
                        break;
                    case 4:
                        UpgradeMembership();
                        break;
                    case 5:
                        DeleteMember();
                        break;
                    case 0:
                        return;
                }
            }
        }



    }
}
