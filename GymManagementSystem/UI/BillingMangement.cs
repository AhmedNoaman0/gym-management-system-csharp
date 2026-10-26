using GymManagementSystem.Enums;
using GymManagementSystem.Models;
using GymManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Text;

/*
 
*/

namespace GymManagementSystem.UI
{
    public class BillingMangement
    {
        private readonly Menu menu;
        private readonly InputHelper inputHelper;
        private readonly MemberServices memberServices;
        private readonly PaymentServices paymentServices;


        public BillingMangement(MemberServices _memberServices,PaymentServices paymentServices)
        {
            string title = "BILLING & PAYMENTS";

            List<string> options = new List<string>();
            options.Add("View All Payment Transactions");
            options.Add("View Member Payment History");
            options.Add("Generate Today's Revenue Report");
            options.Add("Back to Main Menu");

            menu = new Menu(title,options);
            inputHelper = new InputHelper();
            memberServices = _memberServices;
            this.paymentServices = paymentServices;
        }

        private void PrintPayments(List<Payment> payments) 
        {
            decimal total = 0;
            Console.WriteLine("========================================================================================");
            foreach (var payment in payments)
            {
                Console.WriteLine(payment.ToString());
                total += payment.Price;
            }
            Console.WriteLine("========================================================================================");
            Console.WriteLine($"Total Value: {total:C} EGP");
            Console.WriteLine("----------------------------------------------------------------------------------------");
        }
        private void ViewAllPaymentTransactions() 
        {
            //var members = memberServices.ViewAllPaymentTransactions();
            var payments_history = paymentServices.GetPaymentHistory();
            PrintPayments(payments_history);
        }

        private void MemberPaymentHistory() 
        {
            var input = inputHelper.ReadString("Enter Member Name or Id");
            var paymentOfUser = memberServices.GetPaymentsHistoryOfUser(input);

            Console.WriteLine("----------------------------------------------------------------------------------------");
            Console.WriteLine($"Member ID   : {paymentOfUser.member_id}");
            Console.WriteLine($"Name        : {paymentOfUser.member_name}");
            Console.WriteLine($"Phone       : {paymentOfUser.memmber_phone}");
            Console.WriteLine("----------------------------------------------------------------------------------------");

            PrintPayments(paymentOfUser.Payments);
        }

        private void TodayReport() 
        {
            inputHelper.CyanMessage($"\t\t\tDAILY REVENUE REPORT ({DateTime.Now.ToString("yyyy-MM-dd")})");
            var payments = paymentServices.GetTodayPayments();
            var payments_cash = payments.Where(p => p.PaymentMethod == PaymentMethod.Cash);
            decimal cash_total = 0;
            foreach (var item in payments_cash)
            {
                cash_total += item.Price;
            }
            var payments_credit = payments.Where(p => p.PaymentMethod == PaymentMethod.CreditCard);
            decimal credit_total = 0;
            foreach (var item in payments_credit)
            {
                credit_total += item.Price;
            }
            Console.WriteLine($"- Cash Payments        : {cash_total:C}EGP ({payments_cash.Count()} Transaction)");
            Console.WriteLine($"- Credit Payments        : {credit_total:C}EGP ({payments_credit.Count()} Transaction)");

        }


        public void Run() 
        {
            while (true)
            {
                int choice = menu.ShowAndGetChoice();
                switch (choice)
                {
                    case 1:
                        ViewAllPaymentTransactions();
                        break;
                    case 2:
                        MemberPaymentHistory();
                        break;
                    case 3:
                        TodayReport();
                        break;
                    case 0:
                        return;
                }
            }
        }
    }
}
