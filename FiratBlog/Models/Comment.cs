namespace FiratBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Lütfen geçerli bir yorum girin !")]
        [MinLength((3), ErrorMessage = "En az 3 karakter girebilirsiniz !")]
        [MaxLength((200), ErrorMessage = "En fazla 200 karakter girebilirsiniz !")]
        public string Contents { get; set; }

        [Required]
        public int MemberId { get; set; }
        [Required]
        public int ArticleId { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public virtual Article Article { get; set; }

        public virtual Member Member { get; set; }
    }
}
