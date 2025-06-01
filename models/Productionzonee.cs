using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using System;
namespace app.models
{
    public class ProductionZonee
    {
        [NotNull]
        public virtual Guid Id { get; set; }

        public virtual string ZoneName { get; set; }

        public virtual string ProductionType { get;set; }

        public virtual Guid localisationId { get; set; }



    }
}

