using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsAndOrders.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int ContactId { get; set; }
        [NotMapped]
        public bool Editable { get; set; } = false;

        public string? Name { get; set; }

        public double? Price { get; set; }

        public int? Amount { get; set; }

        public string Date { get; set; }

        public string? Discription { get; set; }

        public virtual Contact? Contact { get; set; }
    }
}
