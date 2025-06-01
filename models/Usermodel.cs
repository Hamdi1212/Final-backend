using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace app.models
{
    public class Usermodel
    {
        [Key]public virtual string matricule { get; set;}
        [Required]
        [NotNull]
        public virtual string Name { get; set; }
        [NotNull]
        public virtual string lastName { get; set; } 

        public virtual string Email { get; set; }
        [NotNull]
        [Required]
        public virtual string Password { get; set; }
        public virtual string Role { get;set; }
        public virtual string Token { get; set; }
        public virtual string phonenumber { get; set; }
        public virtual string UserName { get; set; }

        public virtual DateTime BirthDate { get;set; }

        public virtual DateTime CreationDate { get;set; }

    }

}
