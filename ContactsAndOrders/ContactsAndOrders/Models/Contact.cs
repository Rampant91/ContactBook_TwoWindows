using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactBook_TwoWindows.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        [NotMapped]
        public bool Editable { get; set; } = false;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Patronymic { get; set; }

        public string? City { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public IEnumerable<Order>? Orders { get; set; }
    }
}