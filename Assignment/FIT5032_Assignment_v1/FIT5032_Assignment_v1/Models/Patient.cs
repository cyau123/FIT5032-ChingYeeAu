namespace FIT5032_Assignment_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Patient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }

        public string UserId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^(0)(\d{9})$", ErrorMessage = "The {0} must be a 10 digit valid phone number.")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
