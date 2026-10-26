using GymManagementSystem.Enums;
using GymManagementSystem.Models;
using GymManagementSystem.Records;
using GymManagementSystem.UI;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace GymManagementSystem.Services
{
    public class MemberServices
    {
        private int payment_id = 0;
        private int last_id = 0;
        private readonly List<Member> members = new();
        public Member CreateMember(string name, string phone, DateTime birth_date, Membership member_ship,Payment payment) 
        {
            Member new_member = new Member(++last_id,name, phone, birth_date, member_ship,payment);
            members.Add(new_member);
            return new_member;
        }
        public void DeleteMember(int id)
        {
            Member match = members.FirstOrDefault(m => m.Id == id);
            members.Remove(match);
        }
        public List<Member> GetAllMembers() 
        {
            return members;
        }
        public Member SearchMember(string input) 
        {
            input = input.ToLower();
            int.TryParse(input, out int input_int);
            // we here search by phone, name, id 
            var match = members.FirstOrDefault(m => m.Phone == input || m.Name.ToLower().Contains(input)
            || m.Id == input_int);
            return match;
        }
        public void UpgradeMembership(Membership membership,int member_id) 
        {
            var match = members.FirstOrDefault(m => m.Id == member_id);
            match.Membership = membership;
        }
        public GateRecord CheakIn(int member_id) 
        {
            var match = members.FirstOrDefault(m => m.Id == member_id);
            string member_name = match.Name;
            if (!match.IsInside)
            {
                if (match.Membership.IsExpired)
                {
                    return new GateRecord(false, member_name, match.Membership, DateTime.Now);
                }
                CheakInRecord cheak_in = new CheakInRecord() { CheakIn = DateTime.Now };
                match.CheakInRecords.Add(cheak_in);
                return new GateRecord(true, member_name, match.Membership,cheak_in.CheakIn);
            }
            var cheak_in_time = match.CheakInRecords.LastOrDefault().CheakIn;
            return new GateRecord(false, member_name, null, cheak_in_time);
        }
        public GateRecord CheakOut(int member_id) 
        {
            var match = members.FirstOrDefault(m => m.Id == member_id);
            if (!match.IsInside)
            {
                // this member dosent cheak in yet
                return new GateRecord(false, match.Name, null, null);
            }
            match.CheakInRecords.LastOrDefault()?.CheakOut = DateTime.Now;
            var cheak_in_time = match.CheakInRecords.LastOrDefault()?.CheakIn;

            return new GateRecord(true, match.Name, match.Membership, cheak_in_time);
        }
        public List<Member> Members_Currently_Inside() 
        {
            List<Member> members_inside = new List<Member>();
            foreach (var member in members)
            {
                if (member.IsInside)
                {
                    members_inside.Add(member);
                }
            }
            return members_inside;
        }
        public List<Member> ViewAllPaymentTransactions() 
        {
            List<Member> new_members = new List<Member>();
            foreach (var member in members)
            {
                if (member.IsHasPayment)
                {
                    new_members.Add(member);
                }
            }
            return new_members;
        }
        public Member? MemberById(int member_id) 
        {
            return members.FirstOrDefault(m => m.Id == member_id);
        }
        public PaymentOfUser GetPaymentsHistoryOfUser(string input) 
        {
            Member member = SearchMember(input);
            return new PaymentOfUser(member.Payments_History, member.Id, member.Name, member.Phone);
        }

    }
}
