namespace FIT5032_Assignment_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rating
    {
        public int Id { get; set; }
        
        [Required]
        public int DentistId { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        [Range(0,5.0)]
        public Double Score { get; set; }

        public virtual Dentist Dentist { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
