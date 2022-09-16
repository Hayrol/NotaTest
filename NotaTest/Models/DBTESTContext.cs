using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NotaTest.Models
{
    public partial class DBTESTContext : DbContext
    {
        public DBTESTContext()
        {
        }

        public DBTESTContext(DbContextOptions<DBTESTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<NotaModel> Nota { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotaModel>(entity =>
            {
                entity.HasKey(e => e.IdNota)
                    .HasName("PK__NOTA__4B2ACFF2CA99158B");

                entity.ToTable("NOTA");

                entity.Property(e => e.Cuerpo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
