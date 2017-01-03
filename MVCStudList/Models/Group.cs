namespace MVCStudList.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Group : System.IEquatable<Group>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Group()
        {
            Students = new HashSet<Student>();
        }

        public Group(string name)
        {
            Students = new HashSet<Student>();
            Name = name;
        }

             [Key]
        public int IDGroup { get; set; }

        [Required]
        [StringLength(16)]
        public string Name { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] Stamp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }

        public bool Equals(Group other)
        {
            return this.IDGroup == other.IDGroup;
        }
    }
}
