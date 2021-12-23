namespace AlphaFoodies.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        [Key]
        public int Order_Number { get; set; }

        public int ChefID { get; set; }

        public int WaiterID { get; set; }

        [Required]
        public string Order_Items { get; set; }

        [Column(TypeName = "date")]
        public DateTime Order_Date { get; set; }

        public TimeSpan Order_Time { get; set; }

        public decimal Order_Total { get; set; }

        public int Service_Rating { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        [StringLength(50)]
        public string Order_Status { get; set; }

        public TimeSpan Order_PrepTime { get; set; }

        [Required]
        [StringLength(50)]
        public string Order_Type { get; set; }

        public decimal Tip { get; set; }

        public virtual Chef Chef { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual Waiter Waiter { get; set; }
    }
}
