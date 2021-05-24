namespace WpfVideoClub
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Film")]
    public partial class Film
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Film()
        {
            Iznajmljivanja = new HashSet<Iznajmljivanje>();
        }

        public int FilmId { get; set; }

        [Required]
        [StringLength(50)]
        public string NazivFilma { get; set; }

        public int Godina { get; set; }

        public int ZanrId { get; set; }

        public virtual Zanr Zanr { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Iznajmljivanje> Iznajmljivanja { get; set; }

        public override string ToString()
        {
            return NazivFilma;
        }
    }
}
