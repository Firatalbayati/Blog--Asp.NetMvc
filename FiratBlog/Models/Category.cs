namespace FiratBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Article = new HashSet<Article>();
        }

        [Required]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Kategori Adı alanı gereklidir !")]
        [MinLength((3), ErrorMessage = "En az 3 karakter girebilirsiniz !")]
        [MaxLength((30), ErrorMessage = "En fazla 30 karakter girebilirsiniz !")]
        public string CategoryName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Article { get; set; }
    }
}
