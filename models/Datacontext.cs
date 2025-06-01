using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace app.models
{
    public class Datacontext : DbContext
    {
        public Datacontext(DbContextOptions opt) : base(opt) {}
        public DbSet<Usermodel> Users { get; set; }
        public DbSet<Equipement> Equipements { get; set; }
        public DbSet<Localisation> localisations { get; set; }
        public DbSet<ProductionZonee> ProductionZonees { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<Category> categorys { get; set; }
        public DbSet<Worker> workers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Projet> Projets { get; set; }
        public DbSet<Ligne> Lignes { get; set; }
        public DbSet<Poste> Postes { get; set; }
    }
}
