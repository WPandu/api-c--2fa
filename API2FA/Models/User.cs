using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using System.Data;

namespace API2FA.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id", TypeName = "VarChar(20)")]
        public Guid ID { get; set; }

        [Column("email", TypeName = "VarChar(255)")]
        public string Email { get; set; }

        [Column("password", TypeName = "VarChar(255)")]
        public string Password { get; set; }

        [Column("name", TypeName = "VarChar(255)")]
        public string Name { get; set; }

        [Column("google_2fa_secret", TypeName = "VarChar(255)")]
        public string? Google2faSecret { get; set; }
    }
}
