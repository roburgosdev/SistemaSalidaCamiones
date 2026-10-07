using Microsoft.EntityFrameworkCore;

namespace Evaluacion3.Models
{
    public partial class EvaluacionContext : DbContext
    {
        public EvaluacionContext() { }
        public EvaluacionContext(DbContextOptions<EvaluacionContext> options) : base(options) { }

        public virtual DbSet<Camion> Camiones { get; set; }
        public virtual DbSet<Chofer> Choferes { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<EstadoSalida> EstadosSalidas { get; set; }
        public virtual DbSet<Planta> Plantas { get; set; }
        public virtual DbSet<Salida> Salidas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=EVALUACION_WEB691;Trusted_Connection=SSPI;MultipleActiveResultSets=true;Trust Server Certificate=true");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.Id_Estado);
                entity.ToTable("ESTADO");
                entity.Property(e => e.Id_Estado).HasColumnName("ID_ESTADO");
                entity.Property(e => e.Nom_Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOM_ESTADO");
            });

            modelBuilder.Entity<EstadoSalida>(entity =>
            {
                entity.HasKey(e => e.Id_Estado_Salida);
                entity.ToTable("ESTADO_SALIDA");
                entity.Property(e => e.Id_Estado_Salida).HasColumnName("ID_ESTADO_SALIDA");
                entity.Property(e => e.Nom_Estado_Salida)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOM_ESTADO_SALIDA");
            });

            modelBuilder.Entity<Planta>(entity =>
            {
                entity.HasKey(e => e.Id_Planta);
                entity.ToTable("PLANTA");
                entity.Property(e => e.Id_Planta).HasColumnName("ID_PLANTA");
                entity.Property(e => e.Nom_Planta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOM_PLANTA");
            });

            modelBuilder.Entity<Camion>(entity =>
            {
                entity.HasKey(e => e.Cod_Camion);
                entity.ToTable("CAMION");
                entity.Property(e => e.Cod_Camion)
                    .ValueGeneratedNever()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("COD_CAMION");
                entity.Property(e => e.Id_Planta).HasColumnName("ID_PLANTA");
                entity.Property(e => e.Id_Estado).HasColumnName("ID_ESTADO");
            });

            modelBuilder.Entity<Chofer>(entity =>
            {
                entity.HasKey(e => e.Id_Chofer);
                entity.ToTable("CHOFER");
                entity.Property(e => e.Id_Chofer).HasColumnName("ID_CHOFER");
                entity.Property(e => e.Nom_Chofer)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOM_CHOFER");
                entity.Property(e => e.Id_Estado).HasColumnName("ID_ESTADO");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Cod_Usuario);
                entity.ToTable("USUARIOS");
                entity.Property(e => e.Cod_Usuario)
                    .ValueGeneratedNever()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO");
                entity.Property(e => e.Nom_Persona)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOM_PERSONA");
                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");
                entity.Property(e => e.Id_Estado).HasColumnName("ID_ESTADO");
            });

            modelBuilder.Entity<Salida>(entity =>
            {
                entity.HasKey(e => e.Id_Salida);
                entity.ToTable("SALIDA");
                entity.Property(e => e.Id_Salida).HasColumnName("ID_SALIDA");
                entity.Property(e => e.Fecha_Salida)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_SALIDA");
                entity.Property(e => e.Id_Planta).HasColumnName("ID_PLANTA");
                entity.Property(e => e.Cod_Camion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("COD_CAMION");
                entity.Property(e => e.Id_Chofer).HasColumnName("ID_CHOFER");
                entity.Property(e => e.Id_Estado_Salida).HasColumnName("ID_ESTADO_SALIDA");
                entity.Property(e => e.Cod_Usuario)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO");
            });
        }
    }
}