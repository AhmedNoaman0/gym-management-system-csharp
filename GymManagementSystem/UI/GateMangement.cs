using GymManagementSystem.Models;
using GymManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystem.UI
{
    internal class GateMangement
    {
        private readonly Menu? menu;
        private readonly MemberServices memberServices;
        private readonly InputHelper inputHelper;

        public GateMangement(MemberServices memberServices)
        {
            string title = "GATE CHECK-IN / OUT";
            List<string> options = new List<string>();
            options.Add("Member Check-In");
            options.Add("Member Check-Out");
            options.Add("View Who Is Currently Inside");
            options.Add("Back to Main Menu");
            menu = new Menu(title, options);
            inputHelper = new InputHelper();
            this.memberServices = memberServices;
        }

        private void Member_CheakIn() 
        {
            int member_id = inputHelper.ReadInt("Member ID");
            CheakInRecord cheak_in = new CheakInRecord() { CheakIn = DateTime.Now };

            var Gate_Record = memberServices.CheakIn(member_id);

            Console.WriteLine("Verifying Member..."); // :)
            if (Gate_Record.cheak)
            {
                inputHelper.GreenMessage("<< [ACCESS GRANTED]");
                Console.WriteLine($"\tWelcome, {Gate_Record.member_name}");
                Console.WriteLine($"\tMembership: ACTIVE (EXP: {Gate_Record.member_ship.EndDate.ToString("yyyy-MM-dd")})");
                Console.WriteLine($"\tCheak-In Time: {Gate_Record.cheak_in_time?.ToString("hh:mm tt")}");
                return;
            }
            inputHelper.WarningMessage("<< [ACCESS DENIED]");
            if (Gate_Record.member_ship == null)
            {
                Console.WriteLine("\t\tDuplicate Entry Detected!");
                Console.WriteLine($"\tMember {Gate_Record.member_name} is already inside (Checked-in at {Gate_Record.cheak_in_time?.ToString("hh:mm tt")}).");
                Console.WriteLine("\tCannot re-enter without checking out first!");
                return;
            }
            Console.WriteLine("\t\tMembership Expired!");
            Console.WriteLine($"\tExpired On: ({Gate_Record.member_ship.EndDate.ToString("yyyy-MM-dd")}).");
            Console.WriteLine("Please redirect member to billing to renew.");

        }
        private void Member_CheakOut() 
        {
            int member_id = inputHelper.ReadInt("Member ID");
            var Gate_Record = memberServices.CheakOut(member_id);
            if (!Gate_Record.cheak)
            {
                inputHelper.WarningMessage("<< [CHECK-OUT FAILED]");
                Console.WriteLine("\t\tThis Member hasn't Cheaked in!");
                return;
            }
            inputHelper.GreenMessage("<< [CHECK-OUT CONFIRMED]");
            Console.WriteLine($"\t\tGoodbye {Gate_Record.member_name}");
            TimeSpan duration = DateTime.Now - Gate_Record.cheak_in_time.Value;

            Console.WriteLine($"\t\tDuration Inside: {(int)duration.TotalHours} Hour {duration.Minutes} Minutes.");
        }
        private void Members_Inside() 
        {
            var members = memberServices.Members_Currently_Inside();
            if (members.Count == 0)
            {
                inputHelper.WarningMessage("<< [NO MEMBERS INSIDE]");
            }
            foreach (var member in members)
            {
                Console.WriteLine(member.Name);
            }
        }

        public void Run() 
        {
            while (true)
            {
                int choice = menu.ShowAndGetChoice();
                switch (choice) 
                {
                    case 1:
                        Member_CheakIn();
                        break;
                    case 2:
                        Member_CheakOut();
                        break;
                    case 3:
                        Members_Inside();
                        break;
                    case 0:
                        break;
                }
            }
        }
    }
}
