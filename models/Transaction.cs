using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System;

namespace app.models
{
    public class Transaction
    {
        public virtual Guid TransactionId { get; set; }
        public virtual DateTime TransactionDate { get; set; }
        public virtual String SerialNumber { get; set; }
        public virtual String Status { get; set; }
        public virtual string Description { get; set; }
        public virtual string Deplacement { get; set; } // Ajouté : emplacement de destination
    }
}


