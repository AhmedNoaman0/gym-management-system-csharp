using GymManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystem.Records
{
    public record GateRecord(bool cheak, string member_name,Membership? member_ship,
        DateTime? cheak_in_time);

    public record PaymentOfUser(List<Payment> Payments,int member_id, string member_name,string memmber_phone);
}
