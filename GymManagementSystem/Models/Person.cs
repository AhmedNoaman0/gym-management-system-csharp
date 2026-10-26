using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystem.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age => DateTime.Now.Year - BirthDate.Year - (DateTime.Now.DayOfYear > BirthDate.DayOfYear ? 1 : 0);
        protected Person()
        {
            
        }
        protected Person(int id, string name, string phone, DateTime birh_date)
        {
            Id = id;
            Name = name;
            Phone = phone;
            BirthDate = birh_date;
        }
        public virtual string ToString()
        {
            return $"Member ID: {Id} | Name: {Name} | Phone: {Phone} | Age: {Age} ";
        }
    }
}
