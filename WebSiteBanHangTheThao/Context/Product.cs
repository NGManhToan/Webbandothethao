//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSiteBanHangTheThao.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Orders = new HashSet<Order>();
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avata { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string Price { get; set; }
        public Nullable<int> BrandId { get; set; }
        public string Sale { get; set; }
    
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpLoad { get; set; }
    }
}