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
        [JsonIgnore]
        [Column("Session")]
        public Session? Session { get; set; }
        [Column("Session_ID")]
        public int SessionId
        {
            /*
            get { return Session.SessionId; } set { }
            */
            get
            {
                if (Session != null)
                {
                    return Session.SessionId;
                }
                else
                    return 0;
            }
            set { Session = new Session() { SessionId = value }; }
        }

        [Column("Privileges")]
        public string Privileges { get; set; }

    }


    [Table("SESSION")]
    public class Session
    {
        [Key]
        [Column("Session_ID")]
        public int SessionId { get; set; }
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
        public DateTime Gegossen
        {
            get; set;
        }
        [Column("Groesse")]
        public double Groesse { get; set; }
        [JsonIgnore]
        [Column("User")]
        public User? User { get; set; }
        [Column("Username")]
        public string Username { get { if (User != null) return User.Username; else return ""; } set { User = new User() { Username = value }; } }
        [JsonIgnore]
        [Column("Gruppe")]
        public Gruppe Gruppe { get; set; }
        [Column("Gruppenname")]
        public string Gruppenname { get { if (Gruppe != null) return Gruppe.Gruppenname; else return ""; } set { Gruppe = new Gruppe() { Gruppenname = value }; } }      

        [JsonIgnore]
        [Column("Pflanzenart")]
        public Pflanzenart Pflanzenart { get; set; }
        [Column("Pflanzeartname")]
        public string Pflanzeartname { get { if (Pflanzenart != null) return Pflanzenart.Bezeichnung; else return ""; } set { Pflanzenart = new Pflanzenart() { Bezeichnung = value }; } }
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
        public string Username
        {
            get
            {
                if (User != null)
                    return User.Username;
                else
                    return "";
            }
            set { User = new User() { Username = value }; }
        }

    }
    [Table("PFLANZENART")]
    public class Pflanzenart
    {
        [Key]
        [Column("Bezeichnung")]
        public string Bezeichnung { get; set; }
        [Column("Lichtbeduerfnisse")]
        public string Lichtbeduerfnisse { get; set; }
        [Column("Topfgroesse")]
        public double Topfgroesse { get; set; }
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
                if (!Pflanzen.Any())
                {
                    TestDatenGenerieren();
                }
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
        }


        private void TestDatenGenerieren()
        {
            Random r = new Random();
            Session deadsession = null;
            if (!Sessions.Any())
            {
                deadsession = new Session() { SessionId = -1, Status = false, Datum = DateTime.Now };
                Sessions.Add(deadsession);
                this.SaveChanges();
            }
            else
            {
                deadsession = Sessions.First();
            }

            if (!Users.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    User n = null;
                    if (i == 0)
                    {
                        n = new User() { Username = "User" + i, EMail = "user" + i + "@mail.com", Passwort = "password" + i, Session = deadsession, Privileges = "Administrator" };
                    }
                    else
                    {
                        n = new User() { Username = "User" + i, EMail = "user" + i + "@mail.com", Passwort = "password" + i, Session = deadsession, Privileges = "User" };
                    }
                    this.Users.Add(n);
                }
            }
            this.SaveChanges();
            var users = Users.ToList();

            if (!Gruppen.Any())
            {
                for (int i = 0; i < 50; i++)
                {
                    Gruppe n = new Gruppe() { User = users[r.Next(0, 9)], Beschreibung = "Gruppenbeschreibung " + i, Gruppenname = "Gruppe " + i };
                    this.Gruppen.Add(n);
                }
            }
            SaveChanges();
            var groups = this.Gruppen.ToList();

            if (!Pflanzenarten.Any())
            {
                for (int i = 0; i < 15; i++)
                {
                    Pflanzenart n = new Pflanzenart() { Bezeichnung = "Pflanzenart" + i, Erde = "humos", Lichtbeduerfnisse = "hell", Luftfeuchtigkeit = 50 + i, Topfgroesse = 30 + i, Wasserzyklus = i };
                    this.Pflanzenarten.Add(n);
                }
            }
            this.SaveChanges();
            var pflanzenarten = Pflanzenarten.ToList();



            if (!Pflanzen.Any())
            {
                for (int o = 0; o < 150; o++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        Pflanze n = new Pflanze()
                        {
                            Bild = "plant" + r.Next(1, 3),
                            Gegossen = DateTime.Now.AddMinutes(-r.Next(0,21600)),
                            Groesse = r.Next(2, 50),
                            Pflanzenname = "Meine Pflanze" + r.Next(1, 100),
                            Gruppe = groups[r.Next(0, 50)],
                            Pflanzenart = pflanzenarten[r.Next(0, 9)],
                            User = users[k]
                        };
                        this.Pflanzen.Add(n);
                    }
                }
            }
            this.SaveChanges();
        }

        public bool RegisterUser(string username, string password, string email)
        {
            //TODO: Abfragen ob strings Rahmenbedingungen entsprechen
            if (Users.Where(s => s.Username.Equals(username)).Count() > 0)
            {
                return false;
            }
            else
            {
                Users.Add(new User() { Username = username, EMail = email, Passwort = password, Privileges = "User" });
                this.SaveChanges();
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

        public int VerifyUser(string user, string password)
        {
            int session = 0;
            foreach (User u in Users)
            {
                if (u.Username.Equals(user))
                {
                    if (u.Passwort.Equals(password))
                    {
                        if (u.Session != null)
                        {
                            if (u.Session.Datum > DateTime.Now.AddMinutes(-60) && u.SessionId > 0)
                            {
                                session = u.SessionId;
                                break;
                            }
                            else
                            {
                                if (u.Privileges.Equals("Administrator"))
                                {
                                    u.Session = CreateNewSession(true);
                                }
                                else
                                {
                                    u.Session = CreateNewSession(false);
                                }
                                session = u.Session.SessionId;
                                break;
                            }
                        }
                        else
                        {
                            if (u.Privileges.Equals("Administrator"))
                            {
                                u.Session = CreateNewSession(true);
                            }
                            else
                            {
                                u.Session = CreateNewSession(false);
                            }
                            session = u.Session.SessionId;
                            break;
                        }
                    }
                }
            }
            this.SaveChanges();
            return session;
        }

        public bool VerifyUser(string user, int sessionid)
        {
            foreach (User u in Users)
            {
                if (u.Username.Equals(user))
                {
                    if (u.Session == null)
                    {
                        return false;

                    }
                    if (u.Session.SessionId.Equals(sessionid))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private Session CreateNewSession(bool admin)
        {
            Random r = new Random();
            Session news = null;
            if (admin)
            {
                news = new Session() { SessionId = r.Next(1, 9999), Datum = DateTime.Now, Status = true };
            }
            else
            {
                news = new Session() { SessionId = r.Next(10000, 500000), Datum = DateTime.Now, Status = true };
            }
            Sessions.Add(news);
            return news;
        }

        public string UserPflanzen(string user, int sessionid)
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

        public string PflanzenArten(string user, int sessionid)
        {
            string returnstring = "";
            foreach (Pflanzenart pa in Pflanzenarten)
            {
                returnstring += JsonSerializer.Serialize(pa);
            }
            return returnstring;
        }

        public string Initialize(string user, int sessionid)
        {
            string returnstring = "";
            returnstring += PflanzenArten(user, sessionid);
            returnstring += "|";
            returnstring += UserGruppen(user, sessionid);
            returnstring += "|";
            returnstring += UserPflanzen(user, sessionid);
            return returnstring;
        }

        public string UserGruppen(string user, int sessionid)
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

        public bool ChangePassword(string user, string password, string newpassword)
        {
            if (VerifyUser(user, password) > 0)
            {
                Users.Find(user).Passwort = newpassword;
                this.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteUser(string user, string password, string action)
        {
            if (action.Equals("loeschen"))
            {
                var groups = Gruppen
                      .Where(s => s.User.Username.Equals(user))
                      .ToList();
                foreach (Gruppe g in groups)
                {
                    g.User = null;
                }
                var plants = Pflanzen
                      .Where(s => s.User.Username.Equals(user))
                      .ToList();
                foreach (Pflanze p in plants)
                {
                    p.User = null;
                }
                User u = Users.Find(user);
                Users.Remove(u);
                this.SaveChanges();
            }
            return false;
        }


        public bool PflanzeHinzufügen(PflanzeMessage pmsg)
        {
            UserSessionData usd = pmsg.usd;
            Pflanze newpflanze = pmsg.pflanze;

            newpflanze.User = Users.Find(usd.user);
            Gruppe g = Gruppen
                       .Where(g => g.Gruppenname == newpflanze.Gruppe.Gruppenname).FirstOrDefault();
            newpflanze.Gruppe = g;
            Pflanzenart pa = Pflanzenarten
                .Where(g => g.Bezeichnung == newpflanze.Pflanzenart.Bezeichnung).FirstOrDefault();
            newpflanze.Pflanzenart = pa;

            Pflanzen.Add(newpflanze);
            this.SaveChanges();
            return true;
        }

        public bool GruppeHinzufügen(GruppeMessage gmsg)
        {
            if (VerifyUser(gmsg.usd.user, gmsg.usd.sessionid))
            {
                Gruppe newgruppe = null;
                try
                {
                    newgruppe = gmsg.gruppe;
                    newgruppe.User = Users.Where(u => u.Username == newgruppe.Username).FirstOrDefault();
                }
                catch (Exception e)
                {
                    return false;
                }
                Gruppen.Add(newgruppe);
                this.SaveChanges();
                return true;
            }
            return false;

        }

        public string GetUsers(string user, int sessionid)
        {
            if (VerifyUser(user, sessionid) && Users.Find(user).Privileges.Equals("Administrator"))
            {
                string returnstring = "";
                foreach (User u in Users)
                {
                    returnstring += JsonSerializer.Serialize(u) + "|";
                }
                returnstring = returnstring.Remove(returnstring.Length - 1);
                return returnstring;
            }
            return null;
        }


        public bool AdminEditUser(UserSessionData uSDAdmin, FullUserData userData, string action)
        {
            if (VerifyUser(uSDAdmin.user, uSDAdmin.sessionid) && Users.Find(uSDAdmin.user).Privileges.Equals("Administrator"))
            {
                ChangePassword(userData.loginData.user, Users.Find(userData.loginData.user).Passwort, action);
                Users.Find(userData.loginData.user).EMail = userData.email;
                this.SaveChanges();
                return true;
            }
            return false;
        }

        public bool GruppeBearbeiten(GruppeMessage gmsg)
        {
            try
            {
                Gruppe g = gmsg.gruppe;
                Gruppen.Find(g.GruppenID).Gruppenname = g.Gruppenname;
                Gruppen.Find(g.GruppenID).Beschreibung = g.Beschreibung;
                Gruppen.Find(g.GruppenID).User = Users.Find(g.Username);
                this.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PflanzeBearbeiten(PflanzeMessageEdit pmsg)
        {
            try
            {
                Pflanze p = pmsg.pflanze;
                Pflanzen.Find(pmsg.Pflanzen_ID).Pflanzenname = p.Pflanzenname;
                Pflanzen.Find(pmsg.Pflanzen_ID).Bild = p.Bild;
                Pflanzen.Find(pmsg.Pflanzen_ID).Gegossen = p.Gegossen;
                Pflanzen.Find(pmsg.Pflanzen_ID).Groesse = p.Groesse;

                Pflanzen.Find(pmsg.Pflanzen_ID).Gruppe = Gruppen.Where(g => g.Gruppenname == p.Gruppenname).FirstOrDefault();
                Pflanzen.Find(pmsg.Pflanzen_ID).Pflanzenart = Pflanzenarten.Where(g => g.Bezeichnung == p.Pflanzeartname).FirstOrDefault();
                Pflanzen.Find(pmsg.Pflanzen_ID).User = Users.Where(g => g.Username == p.Username).FirstOrDefault();





                this.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool GruppeLöschen(GruppeMessage gmsg)
        {
            try
            {
                Gruppe g = gmsg.gruppe;
                var plants = Pflanzen
                .Where(s => s.Gruppe.GruppenID.Equals(g.GruppenID))
                .ToList();
                foreach (Pflanze p in plants)
                {
                    p.Gruppe = null;
                }
                Gruppen.Remove(g);
                this.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool PflanzeLöschen(PflanzeMessageEdit pmsg)
        {
            try
            {
                Pflanze p = Pflanzen.Find(pmsg.Pflanzen_ID);
                Pflanzen.Remove(p);
                this.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

