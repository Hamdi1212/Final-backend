using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace app.models
{
    public class Projet
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        public virtual ICollection<Ligne> Lignes { get; set; }
    }
}