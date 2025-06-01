using System;
using System.ComponentModel.DataAnnotations;

namespace app.models
{
    public class Store
    {
        [Key]
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
    }
} 