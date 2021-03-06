namespace MVCStudList.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Student
    {
        [Key]
        public int IDStudent { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        public string IndexNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        [StringLength(32)]
        public string BirthPlace { get; set; }

        public int IDGroup { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] Stamp { get; set; }

        [ForeignKey("IDGroup")]
        public virtual Group Group { get; set; }

        // TODO check for IDStudent!
        public Student(string firstName, string lastName, string birthPlace, string indexNr, DateTime birthDate, int groupID)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthPlace = birthPlace;
            IndexNo = indexNr;
            IDGroup = groupID;
            BirthDate = birthDate;
        }

        private Student()
        {

        }
    }
}
