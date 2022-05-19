using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerApp20220519.Models
{
    [Table("TblStudent")]
    public class StudentModel
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentCode { get; set; }
        public string StudentName { get; set; }
    }
}
