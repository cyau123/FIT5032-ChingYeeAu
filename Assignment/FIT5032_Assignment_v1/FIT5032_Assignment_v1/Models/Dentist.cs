namespace FIT5032_Assignment_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Dentist
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dentist()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^(0)(\d{9})$", ErrorMessage = "The {0} must be a 10 digit valid phone number.")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int LocationId { get; set; }

        public decimal? AggregatedRating { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }
        
        public virtual Location Location { get; set; }
    }
}
