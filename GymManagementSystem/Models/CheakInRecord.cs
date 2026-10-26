using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystem.Models
{
    public class CheakInRecord
    {
        public int MemberId { get; set; }
        public DateTime CheakIn { get; set; }
        public DateTime? CheakOut { get; set; }
        public bool IsCurrentlyInside => CheakOut == null; // member is inside if no cheakout untill now
    }
}
