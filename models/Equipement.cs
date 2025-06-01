using System.ComponentModel.DataAnnotations;
using System;

namespace app.models
{
    public class Equipement
    {
        [Key] public Guid EquipementId { get; set; }
        public virtual string Description { get; set; }
        public virtual string SerialNumber { get; set; }
        public virtual DateTime Input { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual string Etat { get; set; }
        public virtual string Marque { get; set; }
        public virtual string category { get; set; }
        public virtual int Qté { get; set; }

        // Emplacement initial et courant
        public Guid? StoreId { get; set; }
        public Store Store { get; set; }
        public int? ProjetId { get; set; }
        public Projet Projet { get; set; }
        public int? LigneId { get; set; }
        public Ligne Ligne { get; set; }
        public int? PosteId { get; set; }
        public Poste Poste { get; set; }

        // Pour affichage rapide de l’emplacement courant
        public string CurrentEmplacement { get; set; }
    }
}
