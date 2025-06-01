
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System;

namespace app.models
{
    public class Localisation
    {
        public virtual Guid LocalisationId { get; set; }
        public virtual string Name { get; set; }

        public string country { get; set; }

        public string region { get; set; }


        public virtual Guid ProductionZoneeId { get; set; } 
    }
}


