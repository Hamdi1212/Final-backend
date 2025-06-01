
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System;

namespace app.models
{
    public class Stock
    {
        public virtual Guid StockId { get; set; }
        public virtual string Name { get; set; }
        [NotNull]
        public virtual int Quantity { get; set; }
        public virtual string type { get; set; }

    }
}

