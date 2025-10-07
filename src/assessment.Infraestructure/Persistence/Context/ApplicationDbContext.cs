using assessment.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;

namespace assessment.Infraestructure.Persistence.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : DbContext(options)
{
    private readonly IConfiguration _configuration = configuration;

    public DbSet<Professor> Professors { get; set; } = null!;
    public DbSet<ProfessorStats> ProfessorStats { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Ratings> Ratings { get; set; } = null!;
    public DbSet<Evaluation> Evaluations { get; set; } = null!;
    public DbSet<EvaluationResponse> EvaluationResponses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public IDbConnection CreateConnection() => new SqlConnection(_configuration.GetConnectionString("Connection"));
}
