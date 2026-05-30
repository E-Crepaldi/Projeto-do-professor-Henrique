using Cine99.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Cine99.Data {
public class ApplicationDbContext : IdentityDbContext {
public ApplicationDbContext(
DbContextOptions<ApplicationDbContext> options)
: base(options) { }
 
public DbSet<Filme> Filmes { get; set; }
public DbSet<Avaliacao> Avaliacoes { get; set; }
}
}