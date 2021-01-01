using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FiratBlog.Models
{

    public class TinyMCEModel
    {

        [Required(ErrorMessage = "İçerik alanı gereklidir !")]
        [MinLength((100), ErrorMessage = "En az 100 karakter girebilirsiniz !")]
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        public string Content { get; set; }

    }
}