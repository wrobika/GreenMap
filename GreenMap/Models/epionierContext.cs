using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace GreenMap
{
    public partial class epionierContext : DbContext
    {
        public epionierContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public epionierContext(DbContextOptions<epionierContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual DbSet<CbdhObj> CbdhObj { get; set; }
        public virtual DbSet<Dzielnice> Dzielnice { get; set; }
        public virtual DbSet<GraniceMiasta> GraniceMiasta { get; set; }
        public virtual DbSet<Hydro> Hydro { get; set; }
        public virtual DbSet<Odwiert> Odwiert { get; set; }
        public virtual DbSet<Zielen> Zielen { get; set; }
        public virtual DbSet<ZwierciadloGl> ZwierciadloGl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql(Configuration.GetConnectionString("EpionierContext"), 
                    x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");

            modelBuilder.Entity<CbdhObj>(entity =>
            {
                entity.HasKey(e => e.Objectid)
                    .HasName("cbdh_obj_pkey");

                entity.ToTable("cbdh_obj");

                entity.HasIndex(e => e.Geom)
                    .HasName("cbdh_obj_geom_1585908095333")
                    .HasMethod("gist");

                entity.Property(e => e.Objectid)
                    .HasColumnName("objectid")
                    .HasColumnType("numeric(12,0)");

                entity.Property(e => e.DzielnicaId)
                    .HasColumnName("dzielnica_id")
                    .HasColumnType("numeric(12,0)");

                entity.Property(e => e.Geom)
                    .HasColumnName("geom")
                    .HasColumnType("geometry(Geometry,2180)");

                entity.Property(e => e.NumerUjecia)
                    .HasColumnName("numer_ujecia")
                    .HasColumnType("numeric(12,0)");

                entity.Property(e => e.NumerWRbdh)
                    .HasColumnName("numer_w_rbdh")
                    .HasColumnType("numeric(12,0)");

                entity.Property(e => e.OtworId)
                    .HasColumnName("otwor_id")
                    .HasColumnType("numeric(12,0)");
            });

            modelBuilder.Entity<Dzielnice>(entity =>
            {
                entity.HasKey(e => e.Objectid)
                    .HasName("dzielnice_pkey");

                entity.ToTable("dzielnice");

                entity.HasIndex(e => e.Geom)
                    .HasName("dzielnice_geom_1585845344316")
                    .HasMethod("gist");

                entity.Property(e => e.Objectid)
                    .HasColumnName("objectid")
                    .ValueGeneratedNever();

                entity.Property(e => e.DataAktualna)
                    .HasColumnName("data_aktualna")
                    .HasColumnType("date");

                entity.Property(e => e.Geom)
                    .HasColumnName("geom")
                    .HasColumnType("geometry(Polygon,2180)");

                entity.Property(e => e.IdDzielnicy).HasColumnName("id_dzielnicy");

                entity.Property(e => e.Mapid).HasColumnName("mapid");

                entity.Property(e => e.MiastoId).HasColumnName("miasto_id");

                entity.Property(e => e.Mslink).HasColumnName("mslink");

                entity.Property(e => e.Nazwa)
                    .HasColumnName("nazwa")
                    .HasMaxLength(50);

                entity.Property(e => e.NazwaPelna)
                    .HasColumnName("nazwa_pelna")
                    .HasMaxLength(80);

                entity.Property(e => e.NrDzielnicy)
                    .HasColumnName("nr_dzielnicy")
                    .HasMaxLength(8);

                entity.Property(e => e.Opis)
                    .HasColumnName("opis")
                    .HasMaxLength(50);

                entity.Property(e => e.Powierzchnia).HasColumnName("powierzchnia");
            });

            modelBuilder.Entity<GraniceMiasta>(entity =>
            {
                entity.HasKey(e => e.Objectid)
                    .HasName("granice_miasta_pkey");

                entity.ToTable("granice_miasta");

                entity.HasIndex(e => e.Geom)
                    .HasName("granice_miasta_geom_1585845344320")
                    .HasMethod("gist");

                entity.Property(e => e.Objectid)
                    .HasColumnName("objectid")
                    .ValueGeneratedNever();

                entity.Property(e => e.DataAktualna)
                    .HasColumnName("data_aktualna")
                    .HasColumnType("date");

                entity.Property(e => e.Geom)
                    .HasColumnName("geom")
                    .HasColumnType("geometry(Polygon,2180)");

                entity.Property(e => e.Mapid).HasColumnName("mapid");

                entity.Property(e => e.Mslink).HasColumnName("mslink");

                entity.Property(e => e.Nazwa)
                    .HasColumnName("nazwa")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Hydro>(entity =>
            {
                entity.HasKey(e => e.Objectid)
                    .HasName("hydro_pkey");

                entity.ToTable("hydro");

                entity.HasIndex(e => e.Geom)
                    .HasName("hydro_geom_1585845344295")
                    .HasMethod("gist");

                entity.Property(e => e.Objectid).HasColumnName("objectid");

                entity.Property(e => e.Aktualnosc)
                    .HasColumnName("aktualnosc")
                    .HasColumnType("date");

                entity.Property(e => e.Counter).HasColumnName("counter");

                entity.Property(e => e.Dtm)
                    .HasColumnName("dtm")
                    .HasColumnType("date");

                entity.Property(e => e.DzielnicaId).HasColumnName("dzielnica_id");

                entity.Property(e => e.EsriOid).HasColumnName("esri_oid");

                entity.Property(e => e.Geom)
                    .HasColumnName("geom")
                    .HasColumnType("geometry(MultiPolygon,2180)");

                entity.Property(e => e.IdIip)
                    .HasColumnName("id_iip")
                    .HasMaxLength(128);

                entity.Property(e => e.JeNazwa)
                    .HasColumnName("je_nazwa")
                    .HasMaxLength(128);

                entity.Property(e => e.JeNr)
                    .HasColumnName("je_nr")
                    .HasMaxLength(128);

                entity.Property(e => e.Kod)
                    .HasColumnName("kod")
                    .HasMaxLength(8);

                entity.Property(e => e.KodNz)
                    .HasColumnName("kod_nz")
                    .HasMaxLength(128);

                entity.Property(e => e.ObrNazwa)
                    .HasColumnName("obr_nazwa")
                    .HasMaxLength(128);

                entity.Property(e => e.ObrNr)
                    .HasColumnName("obr_nr")
                    .HasMaxLength(128);

                entity.Property(e => e.TypKd)
                    .HasColumnName("typ_kd")
                    .HasMaxLength(128);

                entity.Property(e => e.TypNz)
                    .HasColumnName("typ_nz")
                    .HasMaxLength(128);

                entity.Property(e => e.ZrdKd)
                    .HasColumnName("zrd_kd")
                    .HasMaxLength(128);

                entity.Property(e => e.ZrdNz)
                    .HasColumnName("zrd_nz")
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Odwiert>(entity =>
            {
                entity.HasKey(e => e.Objectid)
                    .HasName("odwiert_pkey");

                entity.ToTable("odwiert");

                entity.Property(e => e.Objectid)
                    .HasColumnName("objectid")
                    .ValueGeneratedNever();

                entity.Property(e => e.DzielnicaId).HasColumnName("dzielnica_id");

                entity.Property(e => e.EurefX).HasColumnName("euref_x");

                entity.Property(e => e.EurefY).HasColumnName("euref_y");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Lokalizacja)
                    .HasColumnName("lokalizacja")
                    .HasMaxLength(17);

                entity.Property(e => e.NazwaObiektu)
                    .HasColumnName("nazwa_obiektu")
                    .HasMaxLength(50);

                entity.Property(e => e.NrRbdh).HasColumnName("nr_rbdh");

                entity.Property(e => e.NrUjecia).HasColumnName("nr_ujecia");

                entity.Property(e => e.OkresSpagu)
                    .HasColumnName("okres_spagu")
                    .HasMaxLength(12);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Zielen>(entity =>
            {
                entity.HasKey(e => e.Objectid)
                    .HasName("zielen_pkey");

                entity.ToTable("zielen");

                entity.HasIndex(e => e.Geom)
                    .HasName("zielen_geom_1585845344274")
                    .HasMethod("gist");

                entity.Property(e => e.Objectid)
                    .HasColumnName("objectid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Arkusz)
                    .HasColumnName("arkusz")
                    .HasMaxLength(110);

                entity.Property(e => e.DataAktua)
                    .HasColumnName("data_aktua")
                    .HasMaxLength(254);

                entity.Property(e => e.DlGeograf)
                    .HasColumnName("dl_geograf")
                    .HasMaxLength(15);

                entity.Property(e => e.DzielnicaId).HasColumnName("dzielnica_id");

                entity.Property(e => e.Forma)
                    .HasColumnName("forma")
                    .HasMaxLength(254);

                entity.Property(e => e.Gatunek)
                    .HasColumnName("gatunek")
                    .HasMaxLength(220);

                entity.Property(e => e.Geom)
                    .HasColumnName("geom")
                    .HasColumnType("geometry(MultiPolygon,2180)");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdFito)
                    .HasColumnName("id_fito")
                    .HasMaxLength(240);

                entity.Property(e => e.IdWydz)
                    .HasColumnName("id_wydz")
                    .HasMaxLength(7);

                entity.Property(e => e.Lokalizacj)
                    .HasColumnName("lokalizacj")
                    .HasMaxLength(200);

                entity.Property(e => e.Mapid).HasColumnName("mapid");

                entity.Property(e => e.Mslink).HasColumnName("mslink");

                entity.Property(e => e.Numer).HasColumnName("numer");

                entity.Property(e => e.OgrFid).HasColumnName("ogr_fid");

                entity.Property(e => e.Opis)
                    .HasColumnName("opis")
                    .HasMaxLength(254);

                entity.Property(e => e.Part)
                    .HasColumnName("part")
                    .HasMaxLength(1);

                entity.Property(e => e.PodsPraw)
                    .HasColumnName("pods_praw")
                    .HasMaxLength(254);

                entity.Property(e => e.Powierzchn).HasColumnName("powierzchn");

                entity.Property(e => e.SzGeograf)
                    .HasColumnName("sz_geograf")
                    .HasMaxLength(15);

                entity.Property(e => e.Typ)
                    .HasColumnName("typ")
                    .HasMaxLength(250);

                entity.Property(e => e.TypAgreg).HasColumnName("typ_agreg_");

                entity.Property(e => e.TypAgreg1)
                    .HasColumnName("typ_agreg")
                    .HasMaxLength(250);

                entity.Property(e => e.TypNr).HasColumnName("typ_nr");

                entity.Property(e => e.Uwagi)
                    .HasColumnName("uwagi")
                    .HasMaxLength(254);

                entity.Property(e => e.Uzasd)
                    .HasColumnName("uzasd")
                    .HasMaxLength(254);

                entity.Property(e => e.Walor)
                    .HasColumnName("walor")
                    .HasMaxLength(254);

                entity.Property(e => e.WalorNr).HasColumnName("walor_nr");
            });

            modelBuilder.Entity<ZwierciadloGl>(entity =>
            {
                entity.HasKey(e => e.Objectid)
                    .HasName("zwierciadlo_gl_pkey");

                entity.ToTable("zwierciadlo_gl");

                entity.Property(e => e.Objectid)
                    .HasColumnName("objectid")
                    .ValueGeneratedNever();

                entity.Property(e => e.DzielnicaId).HasColumnName("dzielnica_id");

                entity.Property(e => e.EurefX).HasColumnName("euref_x");

                entity.Property(e => e.EurefY).HasColumnName("euref_y");

                entity.Property(e => e.GlNawiercona)
                    .HasColumnName("gl_nawiercona")
                    .HasColumnType("numeric(7,2)");

                entity.Property(e => e.GlUstabilizowana)
                    .HasColumnName("gl_ustabilizowana")
                    .HasColumnType("numeric(7,2)");

                entity.Property(e => e.NrRbdh).HasColumnName("nr_rbdh");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
