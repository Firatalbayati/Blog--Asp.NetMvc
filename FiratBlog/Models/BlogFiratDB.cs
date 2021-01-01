namespace FiratBlog.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BlogFiratDB : DbContext
    {
        public BlogFiratDB()
            : base("name=BlogFiratDB")
        {
        }

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Authority> Authority { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
