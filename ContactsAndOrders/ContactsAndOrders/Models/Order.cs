using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactBook_TwoWindows.Models
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

        public string? Date { get; set; }

        public string? Discription { get; set; }
    }
}