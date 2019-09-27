#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Toolbox.Data
{
    public class ApplicationDbContext : IdentityDbContext<ToolboxUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Widget> Widgets { get; set; }
        public DbSet<UserWidget> UserWidgets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserWidget>()
                .HasKey(uw => new { uw.WidgetId, uw.UserId });

            modelBuilder.Entity<UserWidget>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserWidgets)
                .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<UserWidget>()
                .HasOne(bc => bc.Widget)
                .WithMany(c => c.UserWidgets)
                .HasForeignKey(bc => bc.WidgetId);
        }
    }

    public class ToolboxUser : IdentityUser
    {
        public ICollection<UserWidget> UserWidgets { get; set; }
    }

    public class Widget
    {
        [Key]
        [Required]
        public string Id { get; set; }

        public ICollection<UserWidget> UserWidgets { get; set; }
    }

    public class UserWidget
    {
        public string WidgetId { get; set; }

        [ForeignKey("WidgetId")]
        public Widget Widget { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ToolboxUser User { get; set; }
    }
}
