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

    [Table("user")]
    public class User
    {
        [Key]
        [Column("Username")]
        public int UserId { get; set; }
        [Column("email")]
        public string EMail { get; set; }
        [Column("Passwort")]
        public string Passwort { get; set; }

    }

    [Table("pflanze")]
    public class Pflanze
    {
        [Key]
        [Column("Pflanzenname")]
        public string EMail { get; set; }
        [Column("Bild")]
        public string Bild { get; set; }
        [Column("Gegossen")]
        public DateTime Gegossen { get; set; }
        [Column("Größe")]
        public double Groesse { get; set; }

    }

    public class DB_Context :DbContext
    {
        public DbSet<Pflanze> TestPflanzen { get; set; }

        public DB_Context() { }
        public DB_Context(bool ensurecreated)
        {
            if (ensurecreated)
            {
                this.Database.EnsureCreated();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder myOptionsBuilder)
        {
                        base.OnConfiguring(myOptionsBuilder);

            NpgsqlConnectionStringBuilder dbBuilder = new NpgsqlConnectionStringBuilder();
            dbBuilder.ApplicationName = "Plan-tsDB.EF";
            dbBuilder.Database = "postgres";
            dbBuilder.Host = "localhost";
            dbBuilder.Port = 5432;
            dbBuilder.Username = "postgres";

            myOptionsBuilder.UseNpgsql(dbBuilder.ToString());
        }

        public string GetTestPflanzen() { 

        }
    }
}
