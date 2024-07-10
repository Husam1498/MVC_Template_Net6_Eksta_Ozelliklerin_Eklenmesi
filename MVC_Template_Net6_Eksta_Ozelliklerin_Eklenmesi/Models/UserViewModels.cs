using System.ComponentModel.DataAnnotations;

namespace MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Models
{
    public class UserModel
    {

        public Guid Id { get; set; }

        public String Fullname { get; set; }

        public String UserName { get; set; }

        public bool Locked { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Role { get; set; } = "user";

    }
}
