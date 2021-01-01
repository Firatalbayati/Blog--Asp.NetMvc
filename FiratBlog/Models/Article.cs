namespace FiratBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Article")]
    public partial class Article
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Article()
        {
            Comment = new HashSet<Comment>();
        }

        public int ArticleId { get; set; }

        [Required(ErrorMessage = "Başlık alanı gereklidir !")]
        [MinLength((2), ErrorMessage = "En az 2 karakter girebilirsiniz !")]
        [MaxLength((80), ErrorMessage = "En fazla 80 karakter girebilirsiniz !")]
        public string Title { get; set; }


        [Required(ErrorMessage = "İçerik alanı gereklidir !")]
        [MinLength((100), ErrorMessage = "En az 100 karakter girebilirsiniz !")]
        [UIHint("tinymce_full_compressed"), AllowHtml]
        public string Contents { get; set; }

        [MaxLength((400), ErrorMessage = "En fazla 400 karakter girebilirsiniz !")]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Tarih alanı gereklidir !")]
        public DateTime Date { get; set; }

        [Required]
        public int Views { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int MemberId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Member Member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
