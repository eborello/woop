using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SpoilBlock.Models
{
    [Index(nameof(Imdbid), Name = "Unique_IMDBID", IsUnique = true)]
    public partial class Medium
    {
        public Medium()
        {
            WoopuserMedia = new HashSet<WoopuserMedium>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("IMDBID")]
        [StringLength(50)]
        public string Imdbid { get; set; } = null!;
        [StringLength(50)]
        public string Title { get; set; } = null!;
        [StringLength(400)]
        public string Description { get; set; } = null!;
        [StringLength(200)]
        public string Image { get; set; } = null!;

        [InverseProperty(nameof(WoopuserMedium.Media))]
        public virtual ICollection<WoopuserMedium> WoopuserMedia { get; set; }
    }
}
