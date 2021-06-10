using System;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Linq;

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
        public string Pflanzenname { get; set; }
        [Column("Bild")]
        public string Bild { get; set; }
        [Column("Gegossen")]
        public DateTime Gegossen { get; set; }
        [Column("Größe")]
        public double Groesse { get; set; }

    }

    public class DB_Context : DbContext
    {
        public DbSet<Pflanze> TestPflanzen { get; set; }

        public DB_Context()
        {
            TestDatenGenerieren();

        }

        public DB_Context(bool ensurecreated)
        {
            TestDatenGenerieren();
            if (ensurecreated)
            {
                bool x = this.Database.EnsureCreated();
                x = x;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder myOptionsBuilder)
        {
            base.OnConfiguring(myOptionsBuilder);

            NpgsqlConnectionStringBuilder dbBuilder = new NpgsqlConnectionStringBuilder();
            dbBuilder.ApplicationName = "PlantsDB.EF";
            dbBuilder.Database = "postgres";
            dbBuilder.Host = "localhost";
            dbBuilder.Port = 5432;
            dbBuilder.Username = "postgres";

            myOptionsBuilder.UseNpgsql(dbBuilder.ToString());

            //this.Database.EnsureCreated();
        }


        private void TestDatenGenerieren()
        {
            if (TestPflanzen.Count<Pflanze>() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    Pflanze n = new Pflanze() { Bild = "url" + i, Gegossen = DateTime.Now, Groesse = 55 + i, Pflanzenname = "Plant" + i };
                    this.TestPflanzen.Add(n);
                }
            }
            this.SaveChanges();
        }

        public string GetTestPflanzen()
        {
            string returnstring = "";

            TestPflanzen.OrderBy(p => p.Pflanzenname);
            foreach (Pflanze s in this.TestPflanzen)
            {
                returnstring += JsonSerializer.Serialize(s) + "|";
                returnstring += "test";
            }

            return returnstring;
        }
    }
}
