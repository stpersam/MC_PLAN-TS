using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;


namespace _ClassLibrary____Common
{

    [Table("strings")]
    public class Manipulationstring
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int EntryId { get; set; }
        [Column("String_Input")]
        public string StringInput { get; set; }
        [Column("Date")]
        public DateTime Date { get; set; }
        [Column("Service_Input")]
        public string ServiceInput { get; set; }
        [Column("Service_Output")]
        public string ServiceOutput { get; set; }

    }

    class DB_Context
    {
    
    
    }
}
