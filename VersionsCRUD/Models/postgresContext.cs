using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VersionsCRUD.Models
{
    public partial class postgresContext : DbContext
    {
        internal Task<IEnumerable<object>> features;

        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessLog> AccessLogs { get; set; } = null!;
        public virtual DbSet<Attachment> Attachments { get; set; } = null!;
        public virtual DbSet<Bug> Bugs { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<Feature> Features { get; set; } = null!;
        public virtual DbSet<Metadata> Metadata { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Release> Releases { get; set; } = null!;
        public virtual DbSet<Subscription> Subscriptions { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Version> Versions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

            modelBuilder.Entity<AccessLog>(entity =>
            {
                entity.ToTable("access_logs");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Documentid).HasColumnName("documentid");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.AccessLogs)
                    .HasForeignKey(d => d.Documentid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_document_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AccessLogs)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_user_id");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("attachments");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Documentid).HasColumnName("documentid");

                entity.Property(e => e.Filename)
                    .HasMaxLength(255)
                    .HasColumnName("filename");

                entity.Property(e => e.Filesize).HasColumnName("filesize");

                entity.Property(e => e.Filetype)
                    .HasMaxLength(50)
                    .HasColumnName("filetype");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.Documentid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("attachments_document_id_fkey");
            });

            modelBuilder.Entity<Bug>(entity =>
            {
                entity.ToTable("bug");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Contributor)
                    .HasMaxLength(255)
                    .HasColumnName("contributor");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Fixed).HasColumnName("fixed");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Reported).HasColumnName("reported");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comments");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Comment1).HasColumnName("comment");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Documentid).HasColumnName("documentid");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Documentid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_document_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_user_id");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("document");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.Versionid).HasColumnName("versionid");

                entity.HasOne(d => d.Version)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.Versionid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_version_id");
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.ToTable("feature");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Release).HasColumnName("release");
            });

            modelBuilder.Entity<Metadata>(entity =>
            {
                entity.ToTable("metadata");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Documentid).HasColumnName("documentid");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Metadata)
                    .HasForeignKey(d => d.Documentid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("metadata_document_id_fkey");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permission");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Documentid).HasColumnName("documentid");

                entity.Property(e => e.Edit).HasColumnName("edit");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.View).HasColumnName("view");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.Documentid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("permission_document_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("permission_user_id_fkey");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("projects");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Updated)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Release>(entity =>
            {
                entity.ToTable("release");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Versionid).HasColumnName("versionid");

                entity.HasOne(d => d.Version)
                    .WithMany(p => p.Releases)
                    .HasForeignKey(d => d.Versionid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("release_version_id_fkey");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("subscription");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Documentid).HasColumnName("documentid");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.Documentid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_document_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_user_id");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tags");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Iduser).HasColumnName("iduser");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Taggeduser)
                    .HasMaxLength(255)
                    .HasColumnName("taggeduser");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("tags_id_user_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Version>(entity =>
            {
                entity.ToTable("versions");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.BugId).HasColumnName("bug_id");

                entity.Property(e => e.Created)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.FeatureId).HasColumnName("feature_id");

                entity.Property(e => e.IsMajor)
                    .HasColumnName("is_major")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.IsMinor)
                    .HasColumnName("is_minor")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.IsPatch)
                    .HasColumnName("is_patch")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Link)
                    .HasMaxLength(255)
                    .HasColumnName("link");

                entity.Property(e => e.Platform)
                    .HasMaxLength(255)
                    .HasColumnName("platform");

                entity.Property(e => e.Projectid).HasColumnName("projectid");

                entity.Property(e => e.Updated)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Versionnumber)
                    .HasMaxLength(255)
                    .HasColumnName("versionnumber");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Versions)
                    .HasForeignKey(d => d.Projectid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_projectid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
