using AuthSystem.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AuthSystem.Areas.Identity.Data;

namespace AuthSystem.Models
{
    public class NContext : IdentityDbContext<ApplicationUser>
    {

        public NContext(DbContextOptions<NContext>options):base (options)
        {

        }
      
        public DbSet<Articolo> Articoli { get; set; }
        public DbSet<OdlFaseVersamento> OdlFaseVersamenti { get; set; }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<DistintaBase> DistintaBasi { get; set; }
      
        public DbSet<CentroDiLavoro> CentriDiLavoro { get; set; }
        public DbSet<MacchinaFisica> MacchinaFisica { get; set; }
        public DbSet<Prenotazione> Prenotazioni { get; set; }
        public DbSet<Postazione> Postazioni { get; set; }
        public DbSet<Odl> Odls { get; set; }
        public DbSet<OdlFase> OdlFasi { get; set; }

        //public DbSet<OdlStateEnum> OdlStato { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // any guid
            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            // any guid, but nothing is against to use the same one
            const string ROLE_ID = ADMIN_ID;

            //Ruolo Admin
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = ROLE_ID,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });

            //Ruolo Normale
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "37c42e1d - 92e5 - 4216 - a308 - 2fa43d187bf1",
                    Name = "User",
                    NormalizedName = "User"

                });

            //Credenziali di accesso Admin
            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,

                SecurityStamp = string.Empty
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Horsa123@");

            //Inserimento nel db?!?
            builder.Entity<ApplicationUser>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });

            //Posti in ufficio
            var Posto1 = new Postazione
            {
                IdPostazione = 1,
                NomePostazione = "Posto1",
                Descrizione = "Blocco in alto: posto in alto a sinistra"
            };

            builder.Entity<Postazione>().HasData(Posto1);

            var Posto2 = new Postazione
            {
                IdPostazione = 2,
                NomePostazione = "Posto2",
                Descrizione = "Blocco in alto: posto in alto al centro"

            };

            builder.Entity<Postazione>().HasData(Posto2);

            var Posto3 = new Postazione
            {
                IdPostazione = 3,
                NomePostazione = "Posto3",
                Descrizione = "Blocco in alto: posto in alto a destra"

            };

            builder.Entity<Postazione>().HasData(Posto3);

            var Posto4 = new Postazione
            {
                IdPostazione = 4,
                NomePostazione = "Posto4",
                Descrizione = "Blocco in alto: posto in basso a destra"

            };

            builder.Entity<Postazione>().HasData(Posto4);

            var Posto5 = new Postazione
            {
                IdPostazione = 5,
                NomePostazione = "Posto5",
                Descrizione = "Blocco in alto: posto in basso al centro"

            };

            builder.Entity<Postazione>().HasData(Posto5);

            var Posto6 = new Postazione
            {
                IdPostazione = 6,
                NomePostazione = "Posto6",
                Descrizione = "Blocco in alto: posto in basso a sinistra"
            };

            builder.Entity<Postazione>().HasData(Posto6);

            var Posto7 = new Postazione
            {
                IdPostazione = 7,
                NomePostazione = "Posto7",
                Descrizione = "Blocco centrale: posto in alto a destra"

            };

            builder.Entity<Postazione>().HasData(Posto7);

            var Posto8 = new Postazione
            {
                IdPostazione = 8,
                NomePostazione = "Posto8",
                Descrizione = "Blocco centrale: posto in alto al centro"

            };

            builder.Entity<Postazione>().HasData(Posto8);

            var Posto9 = new Postazione
            {
                IdPostazione = 9,
                NomePostazione = "Posto9",
                Descrizione = "Blocco centrale: posto in alto a sinistra"

            };

            builder.Entity<Postazione>().HasData(Posto9);

            var Posto10 = new Postazione
            {
                IdPostazione = 10,
                NomePostazione = "Posto10",
                Descrizione = "Blocco centrale: posto in basso a sinistra"

            };

            builder.Entity<Postazione>().HasData(Posto10);

            var Posto11 = new Postazione
            {
                IdPostazione = 11,
                NomePostazione = "Posto11",
                Descrizione = "Blocco centrale: posto in basso al centro"

            };

            builder.Entity<Postazione>().HasData(Posto11);

            var Posto12 = new Postazione
            {
                IdPostazione = 12,
                NomePostazione = "Posto12",
                Descrizione = "Blocco centrale: posto in basso a destra"

            };

            builder.Entity<Postazione>().HasData(Posto12);

            var Posto13 = new Postazione
            {
                IdPostazione = 13,
                NomePostazione = "Posto13",
                Descrizione = "Blocco in basso: posto in alto al centro"

            };

            builder.Entity<Postazione>().HasData(Posto13);

            var Posto14 = new Postazione
            {
                IdPostazione = 14,
                NomePostazione = "Posto14",
                Descrizione = "Blocco in basso: posto in alto a sinistra"

            };

            builder.Entity<Postazione>().HasData(Posto14);

            var Posto15 = new Postazione
            {
                IdPostazione = 15,
                NomePostazione = "Posto15",
                Descrizione = "Blocco in basso: posto in basso a sinistra"

            };

            builder.Entity<Postazione>().HasData(Posto15);

            var Posto16= new Postazione
            {
                IdPostazione = 16,
                NomePostazione = "Posto16",
                Descrizione = "Blocco in basso: posto in basso al centro"

            };

            builder.Entity<Postazione>().HasData(Posto16);

            var Posto17 = new Postazione
            {
                IdPostazione = 17,
                NomePostazione = "Posto17",
                Descrizione = "Blocco in basso: posto in basso a destra"

            };

            builder.Entity<Postazione>().HasData(Posto17);

            var Posto18 = new Postazione
            {
                IdPostazione = 18,
                NomePostazione = "Posto18",
                Descrizione = "Blocco in basso: posto in alto a destra"

            };

            builder.Entity<Postazione>().HasData(Posto18);

            var Posto19 = new Postazione
            {
                IdPostazione = 19,
                NomePostazione = "Posto19",
                Descrizione = "Sala MOVE: posto in alto a destra"

            };

            builder.Entity<Postazione>().HasData(Posto19);

            var Posto20 = new Postazione
            {
                IdPostazione = 20,
                NomePostazione = "Posto20",
                Descrizione = "Sala MOVE: posto in alto a sinistra"

            };

            builder.Entity<Postazione>().HasData(Posto20);

            var Posto21 = new Postazione
            {
                IdPostazione = 21,
                NomePostazione = "Posto21",
                Descrizione = "Sala MOVE: posto a capotavola sinistra"

            };

            builder.Entity<Postazione>().HasData(Posto21);

            var Posto22 = new Postazione
            {
                IdPostazione = 22,
                NomePostazione = "Posto22",
                Descrizione = "Sala MOVE: posto in basso a destra"

            };

            builder.Entity<Postazione>().HasData(Posto22);

            var Posto23 = new Postazione
            {
                IdPostazione = 23,
                NomePostazione = "Posto23",
                Descrizione = "Sala MOVE: posto in basso a sinistra"

            };

            builder.Entity<Postazione>().HasData(Posto23);

            var Posto24 = new Postazione
            {
                IdPostazione = 24,
                NomePostazione = "Posto24",
                Descrizione = "Sala Digital Manufacturing: posto a sinistra"

            };

            builder.Entity<Postazione>().HasData(Posto24);

            var Posto25 = new Postazione
            {
                IdPostazione = 25,
                NomePostazione = "Posto25",
                Descrizione = "Sala Digital Manufacturing: posto in basso "

            };

            builder.Entity<Postazione>().HasData(Posto25);

            var Posto26 = new Postazione
            {
                IdPostazione = 26,
                NomePostazione = "Posto26",
                Descrizione = "Sala Digital Manufacturing: posto a destra"

            };

            builder.Entity<Postazione>().HasData(Posto26);




        }



    }


}
