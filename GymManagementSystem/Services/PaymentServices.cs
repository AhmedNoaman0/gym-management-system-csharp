using GymManagementSystem.Enums;
using GymManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystem.Services
{
    public class PaymentServices
    {
        private int last_id = 0;
        public List<Payment> PaymentHistory { get; set; } = new List<Payment>();

        public Payment CreatePayment(decimal price, PaymentMethod payment_method)
        {
            Payment payment = new Payment(++last_id, price, payment_method);
            PaymentHistory.Add(payment);
            return payment;
        }
        public List<Payment> GetPaymentHistory() 
        {
            return PaymentHistory;
        }
        public List<Payment> GetTodayPayments() 
        {
            //foreach (var payment in PaymentHistory)
            //{
            //    if (payment.Date.Date == DateTime.Now.Date)
            //    {
            //        payments.Add(payment);
            //    }
            //}


            return PaymentHistory.Where(p => p.Date.Day == DateTime.Now.Day).ToList();
        }
    }
}