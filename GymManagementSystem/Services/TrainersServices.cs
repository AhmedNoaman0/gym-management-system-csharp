using GymManagementSystem.Enums;
using GymManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GymManagementSystem.Services
{
    public class TrainersServices
    {
        private int last_id;
        public List<Trainer> Trainers { get; set; }
        public TrainersServices()
        {
            Trainers = new List<Trainer>();
            last_id = 0;
        }

        public void AddTrainer(string name, string phone, DateTime birth_date, TrainerSpecialization trainerSpecialization) 
        {
            Trainer trainer = new Trainer(++last_id, name, phone, birth_date, trainerSpecialization);
            Trainers.Add(trainer);
        }
        public List<Trainer> GetAllTrainers() 
        {
            return Trainers;
        }
        public Trainer FindTrainer(string input) 
        {
            input = input.ToLower();
            int.TryParse(input, out int input_int);
            var match = Trainers.FirstOrDefault(m => m.Id == input_int ||
            m.Name.ToLower().Contains(input) || m.Phone == input);
            return match;
        }
        public void AssignMemberToTrainer(int trainer_id, Member member) 
        {
            var match = Trainers.FirstOrDefault(t => t.Id == trainer_id);
            match.AssignMember(member);
        }
        public List<Member> GetAllTrainees(int trainer_id, ref Trainer trainer) 
        {
            var match = Trainers.FirstOrDefault(t => t.Id == trainer_id);
            trainer = match;
            return match.GetAllTrainees();
        }

    }
}
