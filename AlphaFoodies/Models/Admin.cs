namespace AlphaFoodies.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        [Column("Admin")]
        public int Admin1 { get; set; }

        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(50)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(50)]
        public string Email_Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [StringLength(8)]
        public string Password { get; set; }

        [Required]
        public byte[] Picture { get; set; }
    }
}
