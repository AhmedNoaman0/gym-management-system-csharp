using GymManagementSystem.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystem.Models
{
    public class Membership
    {
        private DateTime _endDate;
        public MembershipType MembershipType { get; set; }
        public decimal Price { get; private set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate 
        {
            get => _endDate;
            set 
            {
                if (value < StartDate)
                    throw new ArgumentException("End date cannot be before start date.");
                _endDate = value;
            }
        }
        public bool IsExpired => DateTime.Now.Date > EndDate; // use computed Property
        public Membership(MembershipType type) : this(type,DateTime.Now.Date)
        {
            // in case if we put start date automatically
        }
        public Membership(MembershipType type, DateTime start_date)
        {
            // in case if we put start date manually
            MembershipType = type;
            StartDate = start_date;
            int months = (int)MembershipType;
            _endDate = StartDate.AddMonths(months);
            Price = CalcPrice(MembershipType);

        }
        private decimal CalcPrice(MembershipType type) 
        {
            decimal price = (int)type * 400;
            return price;
        }

    }
}
