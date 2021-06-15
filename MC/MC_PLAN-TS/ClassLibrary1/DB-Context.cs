using System;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Linq;
using System.Text.Json.Serialization;

namespace _ClassLibraryCommon
{

    [Table("USER")]
    public class User
    {
        [Key]
        [Column("Username")]
        public string Username { get; set; }
        [Column("email")]
        public string EMail { get; set; }
        [Column("Passwort")]
        public string Passwort { get; set; }
        [Column("Session")]
        public Session Session { get; set; }

    }


    [Table("SESSION")]
    public class Session
    {
        [Key]
        [Column("Session_ID")]
        public double SessionId { get; set; }
        [Column("Datum")]
        public DateTime Datum { get; set; }
        [Column("Status")]
        public bool Status { get; set; }


    }

    [Table("PFLANZE")]
    public class Pflanze
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Pflanzen_ID")]
        public int PflanzenID { get; set; }
        [Column("Pflanzenname")]
        public string Pflanzenname { get; set; }
        [Column("Bild")]
        public string Bild { get; set; }
        [Column("Gegossen")]
        public DateTime Gegossen { get; set; }
        [Column("Groesse")]
        public double Groesse { get; set; }
        [JsonIgnore]
        [Column("User")]
        public User User { get; set; }
        [Column("Username")]
        public string Username { get { return User.Username; } set { } }
        [JsonIgnore]
        [Column("Gruppe")]
        public Gruppe Gruppe { get; set; }
        [Column("Gruppenname")]
        public string Gruppenname { get { return Gruppe.Gruppenname; } set { } }
        [JsonIgnore]
        [Column("Pflanzenart")]
        public Pflanzenart Pflanzenart { get; set; }
        [Column("Pflanzeartname")]
        public string Pflanzeartname { get { return Pflanzenart.Bezeichnung; } set { } }

    }

    [Table("GRUPPE")]
    public class Gruppe
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Gruppen_ID")]
        public int GruppenID { get; set; }
        [Column("Gruppenname")]
        public string Gruppenname { get; set; }
        [Column("Beschreibung")]
        public string Beschreibung { get; set; }
        [JsonIgnore]
        [Column("User")]
        public User User { get; set; }
        [Column("Username")]
        public string Username { get { return User.Username; } set { } }

    }
    [Table("PFLANZENART")]
    public class Pflanzenart
    {
        [Key]
        [Column("Bezeichnung")]
        public string Bezeichnung { get; set; }
        [Column("Lichtbeduerfnisse")]
        public string Lichtbeduerfnisse { get; set; }
        [Column("Topfgröße")]
        public double Topfgröße { get; set; }
        [Column("Erde")]
        public string Erde { get; set; }
        [Column("Wasserzyklus")]
        public double Wasserzyklus { get; set; }
        [Column("Luftfeuchtigkeit")]
        public int Luftfeuchtigkeit { get; set; }

    }

    public class DB_Context : DbContext
    {
        public DbSet<Pflanze> Pflanzen { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Gruppe> Gruppen { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Pflanzenart> Pflanzenarten { get; set; }

        public DB_Context() { }

        public DB_Context(bool ensurecreated)
        {
            if (ensurecreated)
            {
                this.Database.EnsureCreated();
                this.SaveChanges();
            }
            TestDatenGenerieren();
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
        }


        private void TestDatenGenerieren()
        {
            if (!Users.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    User n = new User() { Username = "User" + i, EMail = "user" + i + "@mail.com", Passwort = "password" + i, Session = null };
                    this.Users.Add(n);
                }
            }
            this.SaveChanges();
            var users = Users.ToList();

            if (!Gruppen.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    Gruppe n = new Gruppe() { User = users[i], Beschreibung = "Gruppenbeschreibung " + i, Gruppenname = "Gruppe " + i };
                    this.Gruppen.Add(n);
                }
            }
            SaveChanges();
            var groups = this.Gruppen.ToList();

            if (!Pflanzenarten.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    Pflanzenart n = new Pflanzenart() { Bezeichnung = "Pflanzenart" + i, Erde = "humos", Lichtbeduerfnisse = "hell", Luftfeuchtigkeit = 50 + i, Topfgröße = 30 + i, Wasserzyklus = i };
                    this.Pflanzenarten.Add(n);
                }
            }
            this.SaveChanges();
            var pflanzenarten = Pflanzenarten.ToList();

            if (!Pflanzen.Any())
            {
                for (int k = 0; k < 10; k++)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Pflanze n = new Pflanze() { Bild = "url" + i, Gegossen = DateTime.Now, Groesse = 55 + i, Pflanzenname = "Plant " + i, Gruppe = groups[i], Pflanzenart = pflanzenarten[i], User = users[k] };
                        this.Pflanzen.Add(n);
                    }
                }
            }
            this.SaveChanges();
        }

        public bool RegisterUser(string username, string password, string email)
        {
            //TODO: Abfragen ob strings Rahmenbedingungen entsprechen
            if (Users.Where(s => s.Username.Equals(username)).Count() <= 0)
            {
                Users.Add(new User() { Username = username, EMail = email, Passwort = password });
                return true;
            }

            return false;
        }

        public string GetTestPflanzen()
        {
            string returnstring = "";
            Pflanzen.OrderBy(p => p.Pflanzenname);
            foreach (Pflanze s in this.Pflanzen)
            {
                returnstring += JsonSerializer.Serialize(s);
            }
            return returnstring;
        }

        public double VerifyUser(string user, string password)
        {
            foreach (User u in Users)
            {
                if (u.Username.Equals(user))
                {
                    if (u.Passwort.Equals(password))
                    {

                        if (u.Session == null)
                        {
                            u.Session = CreateNewSession();
                        }
                        if (u.Session.Datum < DateTime.Now.AddMinutes(-60))
                        {
                            u.Session = CreateNewSession();
                        }
                        return u.Session.SessionId;
                    }
                }
            }
            return 0;
        }

        public bool VerifyUser(string user, double sessionid)
        {
            foreach (User u in Users)
            {
                if (u.Username.Equals(user))
                {
                    if (u.Session.SessionId.Equals(sessionid))
                    {

                        if (u.Session == null)
                        {
                            u.Session = CreateNewSession();
                        }
                        if (u.Session.Datum < DateTime.Now.AddMinutes(-60))
                        {
                            u.Session = CreateNewSession();
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        private Session CreateNewSession()
        {
            Random r = new Random();
            return new Session() { SessionId = r.Next(0, 5000000), Datum = DateTime.Now, Status = true };
        }

        public string UserPflanzen(string user, double sessionid)
        {
            string returnstring = "";
            if (VerifyUser(user, sessionid))
            {
                var plants = Pflanzen
                    .Where(s => s.User.Username.Equals(user))
                    .ToList();
                foreach (Pflanze p in plants)
                {
                    returnstring += JsonSerializer.Serialize(p);
                }
            }
            return returnstring;
        }

        public string PflanzenArten(string user, double sessionid)
        {
            string returnstring = "";
            foreach (Pflanzenart pa in Pflanzenarten)
            {
                returnstring += JsonSerializer.Serialize(pa);
            }
            return returnstring;
        }

        public string Initialize(string user, double sessionid)
        {
            string returnstring = "";
            returnstring += PflanzenArten(user, sessionid);
            returnstring += "|";
            returnstring += UserGruppen(user, sessionid);
            returnstring += "|";
            returnstring += UserPflanzen(user, sessionid);
            return returnstring;
        }

        public string UserGruppen(string user, double sessionid)
        {
            string returnstring = "";
            var groups = Gruppen
                   .Where(s => s.User.Username.Equals(user))
                   .ToList();
            foreach (Gruppe g in groups)
            {
                returnstring += JsonSerializer.Serialize(g);
            }
            return returnstring;
        }
    }
}

