using GymManagementSystem.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystem.Models
{
    public class Trainer : Person
    {
        public TrainerSpecialization Specialization { get; set; }
        public List<Member> Trainees { get; set; } = new();

        public Trainer()
        {
            
        }
        public Trainer(int id, string name, string phone, DateTime birth_date,
            TrainerSpecialization specialization) : base(id, name, phone, birth_date)
        {
            Specialization = specialization;
        }
        public void AssignMember(Member member) 
        {
            Trainees.Add(member);
        }
        public List<Member> GetAllTrainees() 
        {
            return Trainees;
        }
        public override string ToString()
        {
            return base.ToString() + $" | Specialization: {Specialization} | Number of trainees [{Trainees.Count}] ";
        }
    }
}
