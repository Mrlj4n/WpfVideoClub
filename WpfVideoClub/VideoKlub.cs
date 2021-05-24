using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WpfVideoClub
{
    public partial class VideoKlub : DbContext
    {
        public VideoKlub()
            : base("name=VideoKlub")
        {
        }

        public virtual DbSet<Clan> Clanovi { get; set; }
        public virtual DbSet<Film> Filmovi { get; set; }
        public virtual DbSet<Iznajmljivanje> Iznajmljivanja { get; set; }
        public virtual DbSet<Zanr> Zanrovi { get; set; }
        public virtual DbSet<ViewIznajmljivanja> ViewIznajmljivanja { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clan>()
                .Property(e => e.Jmbg)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Clan>()
                .HasMany(e => e.Iznajmljivanja)
                .WithRequired(e => e.Clan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Film>()
                .HasMany(e => e.Iznajmljivanja)
                .WithRequired(e => e.Film)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Iznajmljivanje>()
                .Property(e => e.Cena)
                .HasPrecision(10, 3);

            modelBuilder.Entity<Zanr>()
                .HasMany(e => e.Filmovi)
                .WithRequired(e => e.Zanr)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ViewIznajmljivanja>()
                .Property(e => e.Cena)
                .HasPrecision(10, 3);
        }
    }
}
