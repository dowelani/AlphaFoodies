namespace AlphaFoodies.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuItem")]
    public partial class MenuItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MenuItem()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        [Key]
        public int Item_Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Item_Name { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Description { get; set; }

        public decimal Price { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        public byte[] Picture { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
