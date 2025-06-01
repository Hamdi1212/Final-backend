using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.models
{
    public class Ligne
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }

        [ForeignKey("Projet")]
        public int Project_Id { get; set; }
        public virtual Projet Projet { get; set; }

        public virtual ICollection<Poste> Postes { get; set; }
    }
}