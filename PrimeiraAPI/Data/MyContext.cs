using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Data
{
    public class MyContext : IdentityDbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Pagar> Pagamentos { get; set; }
        public DbSet<Pets> PetsDono { get; set; }
        public DbSet<Produto> Produtos { get; set; }

 
        public DbSet<Trabalhos> Trabalhados { get; set; }

        public DbSet<Vender> Vendas { get; set; }

        public DbSet<VenderProduto> VenderProdutos { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Fornecedor>().ToTable("Fornecedores");
            modelBuilder.Entity<Pagar>().ToTable("Pagamentos");
            modelBuilder.Entity<Pets>().ToTable("PetsDono");
            modelBuilder.Entity<Produto>().ToTable("Produtos");

            modelBuilder.Entity<Trabalhos>().ToTable("Trabalhados");
            modelBuilder.Entity<Vender>().ToTable("Vendas");
            modelBuilder.Entity<VenderProduto>().ToTable("VenderProdutos");
        }
    }
}
