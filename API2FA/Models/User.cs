using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

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

        [JsonIgnore]
        [Column("password", TypeName = "VarChar(255)")]
        public string Password { get; set; }

        [Column("name", TypeName = "VarChar(255)")]
        public string Name { get; set; }


        [JsonIgnore]
        [Column("google_2fa_secret", TypeName = "VarChar(255)")]
        public string? Google2faSecret { get; set; }
    }
}
