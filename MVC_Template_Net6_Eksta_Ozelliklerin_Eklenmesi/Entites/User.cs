using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace MVC_Template_Net6_Eksta_Ozelliklerin_Eklenmesi.Entites
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(30)]
        public String Fullname { get; set; }

        [Required]
        [StringLength(50)]
        public String UserName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(8)]
        public String Password { get; set; }

        public bool Locked { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [StringLength(255)]
        public string? ProfilImageFileName { get; set; } = "no_image.jpeg";

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = "user";


    }

}
