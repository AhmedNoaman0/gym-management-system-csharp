using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystem.Models
{
    public class PersonalSession
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int TrainerId { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime EndDate => StartDate.Add(Duration);
    }
}
