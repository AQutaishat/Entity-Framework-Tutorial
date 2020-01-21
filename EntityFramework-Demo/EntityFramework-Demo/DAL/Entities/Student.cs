using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EntityFramework_Demo
{
    [Table("StudentMaster")]
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
       
        [Required]
        [MaxLength(500)]
        public string StudentName { get; set; }
        public int Age { get; set; }
        
        [NotMapped]
        public String NotMappedProperty { get; set; }
    }
}