using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

#nullable disable

namespace GroupFourLibrary.Models
{
    public partial class GroupFourLibraryContext : IdentityDbContext
    {
        public GroupFourLibraryContext()
        {
        }

        public GroupFourLibraryContext(DbContextOptions<GroupFourLibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookCopy> BookCopies { get; set; }
        public virtual DbSet<BookStore> BookStores { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=GroupFourLibraryDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BookKeyId)
                    .HasName("PK__Book__14564D3F14E549E7");

                entity.ToTable("Book");

                entity.Property(e => e.Author)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BookId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BookTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Publisher)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BookCopy>(entity =>
            {
                entity.ToTable("BookCopy");

                entity.Property(e => e.BookCopyId).HasColumnName("BookCopyID");

                entity.Property(e => e.BookStoreId).HasColumnName("BookStoreID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookCopies)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("fk_column");

                entity.HasOne(d => d.BookStore)
                    .WithMany(p => p.BookCopies)
                    .HasForeignKey(d => d.BookStoreId)
                    .HasConstraintName("FK__BookCopy__BookSt__440B1D61");
            });

            modelBuilder.Entity<BookStore>(entity =>
            {
                entity.ToTable("BookStore");

                entity.Property(e => e.BookStoreId).HasColumnName("BookStoreID");

                entity.Property(e => e.BookStoreAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BookStoreName)
                    .HasMaxLength(75)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservation");

                entity.Property(e => e.ReservationId).HasColumnName("ReservationID");

                entity.Property(e => e.BookCopyId).HasColumnName("BookCopyID");

                entity.Property(e => e.DateReserved).HasColumnType("date");

                entity.Property(e => e.DateReturned).HasColumnType("date");

                entity.HasOne(d => d.BookCopy)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.BookCopyId)
                    .HasConstraintName("FK__Reservati__DateR__46E78A0C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
