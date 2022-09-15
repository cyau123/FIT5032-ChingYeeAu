namespace FIT5032_Assignment_v1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Booking
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Time")]
        public DateTime StartDateTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        [Display(Name ="End Time")]
        public DateTime EndDateTime { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public int DentistId { get; set; }
        [Required]
        public int PatientId { get; set; }

        public virtual Dentist Dentist { get; set; }

        public virtual Location Location { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
