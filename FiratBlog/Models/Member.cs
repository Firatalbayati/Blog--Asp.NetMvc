namespace FiratBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Member")]
    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            Article = new HashSet<Article>();
            Comment = new HashSet<Comment>();
        }

        public int MemberId { get; set; }

        [Required(ErrorMessage = "Kullanıcı Adı alanı gereklidir !")]
        [MinLength((3), ErrorMessage = "En az 3 karakter girebilirsiniz !")]
        [MaxLength((50), ErrorMessage = "En fazla 50 karakter girebilirsiniz !")]
        public string UserName { get; set; }

        [EmailAddress]
        [MinLength((12), ErrorMessage = "En az 12 karakter girebilirsiniz !")]
        [MaxLength((50), ErrorMessage = "En fazla 50 karakter girebilirsiniz !")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı gereklidir !")]
        [MinLength((8), ErrorMessage = "En az 8 karakter girebilirsiniz !")]
        [MaxLength((50), ErrorMessage = "En fazla 50 karakter girebilirsiniz !")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ad Soyad alanı gereklidir !")]
        [MinLength((3), ErrorMessage = "En az 3 karakter girebilirsiniz !")]
        [MaxLength((50), ErrorMessage = "En fazla 50 karakter girebilirsiniz !")]
        public string NameSurname { get; set; }

        [MaxLength((400), ErrorMessage = "En fazla 400 karakter girebilirsiniz !")]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Lütfen bir kategori seçin !")]
        public int AuthorityId { get; set; }

        [StringLength(10)]
        public string IsResetPw { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Article { get; set; }

        public virtual Authority Authority { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
