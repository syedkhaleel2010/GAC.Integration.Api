using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace GAC.Integration.Domain.Entities
{
    [Table("Users", Schema = "dbo")]
    public class User : EntityBase
    {
        public User()
        {
            CreatedBy = string.Empty;
            UpdatedBy = string.Empty;
        }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}