using GymManagementSystem.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystem.Models
{
    public class Member : Person
    {
        public Membership? Membership { get; set; }
        public int? TrainerId { get; set; }
        public List<Payment> Payments_History { get; set; } = new List<Payment>();
        public bool IsHasPayment => Payments_History.Count > 0;
        public List<CheakInRecord> CheakInRecords { get; set; } = new List<CheakInRecord>();
        public bool IsInside => CheakInRecords.LastOrDefault()?.IsCurrentlyInside ?? false;
        public Member()
        {
            
        }
        public Member(int id,string name, string phone, DateTime birth_date,
            Membership member_ship,Payment payment) :base(id,name,phone,birth_date)
        {
            Membership = member_ship;
            Payments_History.Add(payment);
        }
        public override string ToString()
        {
            return $"{base.ToString()} | Membership: {Membership.MembershipType} " +
                $"| Subscription ends on: {Membership.EndDate.ToShortDateString()}";
        }
    }
}
