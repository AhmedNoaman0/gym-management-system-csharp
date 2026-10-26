using GymManagementSystem.Enums;
using GymManagementSystem.Models;
using GymManagementSystem.Services;

namespace GymManagementSystem.Test
{
    public static class SeedData
    {
        public static void Seed(
            MemberServices memberServices,
            TrainersServices trainersServices,
            PaymentServices paymentServices)
        {
            // ==================== TRAINERS ====================

            trainersServices.AddTrainer(
                "Ahmed Hassan",
                "01012345678",
                new DateTime(1990, 5, 12),
                TrainerSpecialization.Bodybuilding);

            trainersServices.AddTrainer(
                "Omar Mohamed",
                "01123456789",
                new DateTime(1988, 8, 20),
                TrainerSpecialization.Crossfit);

            trainersServices.AddTrainer(
                "Youssef Ali",
                "01234567890",
                new DateTime(1993, 2, 15),
                TrainerSpecialization.Cardio);

            trainersServices.AddTrainer(
                "Mahmoud Samir",
                "01567890123",
                new DateTime(1987, 11, 3),
                TrainerSpecialization.Bodybuilding);

            // ==================== MEMBERS ====================

            var payment1 = paymentServices.CreatePayment(400, PaymentMethod.Cash);
            payment1.PaymentStatus = PaymentStatus.Completed;
            memberServices.CreateMember(
                "Mohamed Ahmed", "01011112222",
                new DateTime(1998, 4, 10),
                new Membership(MembershipType.Monthly), payment1);

            var payment2 = paymentServices.CreatePayment(1200, PaymentMethod.CreditCard);
            payment2.PaymentStatus = PaymentStatus.Completed;
            memberServices.CreateMember(
                "Ali Hassan", "01122223333",
                new DateTime(2000, 7, 21),
                new Membership(MembershipType.Monthly, new DateTime(2026, 7, 20)), payment2);

            var payment3 = paymentServices.CreatePayment(2400, PaymentMethod.Cash);
            payment3.PaymentStatus = PaymentStatus.Completed;
            memberServices.CreateMember(
                "Omar Khaled", "01233334444",
                new DateTime(1995, 1, 5),
                new Membership(MembershipType.SixMonths), payment3);

            var payment4 = paymentServices.CreatePayment(4800, PaymentMethod.CreditCard);
            payment4.PaymentStatus = PaymentStatus.Completed;
            memberServices.CreateMember(
                "Mostafa Adel", "01544445555",
                new DateTime(1997, 9, 18),
                new Membership(MembershipType.Yearly), payment4);

            var payment5 = paymentServices.CreatePayment(400, PaymentMethod.Cash);
            payment5.PaymentStatus = PaymentStatus.Completed;
            memberServices.CreateMember(
                "Youssef Mahmoud", "01055556666",
                new DateTime(2001, 12, 2),
                new Membership(MembershipType.Monthly, new DateTime(2015, 7, 20)), payment5);

            var payment6 = paymentServices.CreatePayment(1200, PaymentMethod.CreditCard);
            payment6.PaymentStatus = PaymentStatus.Completed;
            memberServices.CreateMember(
                "Karim Tarek", "01166667777",
                new DateTime(1996, 6, 14),
                new Membership(MembershipType.ThreeMonths), payment6);

            var payment7 = paymentServices.CreatePayment(2400, PaymentMethod.Cash);
            payment7.PaymentStatus = PaymentStatus.Completed;
            memberServices.CreateMember(
                "Hassan Ibrahim", "01277778888",
                new DateTime(1999, 3, 27),
                new Membership(MembershipType.SixMonths), payment7);

            var payment8 = paymentServices.CreatePayment(4800, PaymentMethod.CreditCard);
            payment8.PaymentStatus = PaymentStatus.Completed;
            memberServices.CreateMember(
                "Amr Nabil", "01588889999",
                new DateTime(1992, 4, 27),
                new Membership(MembershipType.Yearly), payment8);

            // ==================== ASSIGN MEMBERS ====================

            var members = memberServices.GetAllMembers();

            for (int i = 0; i < members.Count; i++)
            {
                members[i].Payments_History[0].Member_id = members[i].Id;
            }

            trainersServices.AssignMemberToTrainer(1, members[0]);
            trainersServices.AssignMemberToTrainer(1, members[1]);

            trainersServices.AssignMemberToTrainer(2, members[2]);
            trainersServices.AssignMemberToTrainer(2, members[3]);

            trainersServices.AssignMemberToTrainer(3, members[4]);
            trainersServices.AssignMemberToTrainer(3, members[5]);

            trainersServices.AssignMemberToTrainer(4, members[6]);
            trainersServices.AssignMemberToTrainer(4, members[7]);
        }
    }
}