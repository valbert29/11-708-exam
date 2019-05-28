using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_2.Models.OthModels
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public List<Promo> Promos { get; set; }
        public int Bucket { get; set; }
    }

    public class Bucket
    {
        public int Id { get; set; }
        public List<Dish> Dishes { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int UserId { get; set; }
        public DateTime Time { get; set; }
        public List<int> Dishes { get; set; }
    }

    public class Restoran
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Dish> Dishes { get; set; }
        public Restoran()
        {
            Dishes = new List<Dish>();
        }
    }

    public class Dish
    {
        public string Avatar { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desription { get; set; }
        public int Price { get; set; }

        public int? RestId { get; set; }
        public Restoran Restoran { get; set; }
    }

    public class Promo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Usage { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
