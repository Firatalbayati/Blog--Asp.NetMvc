namespace FiratBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contact")]
    public partial class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Ad Soyad alanı gereklidir !")]
        [MinLength((3), ErrorMessage = "En az 3 karakter girebilirsiniz !")]
        [MaxLength((50), ErrorMessage = "En fazla 50 karakter girebilirsiniz !")]
        public string NameSurname { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Lütfen e-mail adresinizi giriniz !")]
        [MinLength((12), ErrorMessage = "En az 12 karakter girebilirsiniz !")]
        [MaxLength((50), ErrorMessage = "En fazla 50 karakter girebilirsiniz !")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Konu başlığı gereklidir !")]
        [MinLength((3), ErrorMessage = "En az 3 karakter girebilirsiniz !")]
        [MaxLength((50), ErrorMessage = "En fazla 50 karakter girebilirsiniz !")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Mesaj alanı gereklidir !")]
        [MinLength((3), ErrorMessage = "En az 3 karakter girebilirsiniz !")]
        [MaxLength((500), ErrorMessage = "En fazla 500 karakter girebilirsiniz !")]
        public string Message { get; set; }
    }
}
