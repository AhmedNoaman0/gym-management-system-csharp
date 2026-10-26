using GymManagementSystem.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystem.Models
{
    public class Payment
    {
        //private static int last_id = 0;
        private int id { get; set; }
        public int? Member_id { get; set; }
        public decimal Price { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime Date { get; set; }

        public Payment(int id,decimal price, PaymentMethod payment_method)
        {
            this.id = id;
            Price = price;
            PaymentMethod = payment_method;
            Date = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Payment #{id} | Amount: {Price:C} | " +
                $"| Date: {Date:d} | Status: {PaymentStatus} - {PaymentMethod} ";
        }
    }
}
