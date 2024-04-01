using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MarketBot.Models
{
    public partial class TelegramBotContext : DbContext
    {
        public TelegramBotContext()
        {
        }

        public TelegramBotContext(DbContextOptions<TelegramBotContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuthorizedAccountTelegram> AuthorizedAccountTelegrams { get; set; } = null!;
        public virtual DbSet<DotaAccount> DotaAccounts { get; set; } = null!;
        public virtual DbSet<NewTelegramAccount> NewTelegramAccounts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=TelegramBot;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorizedAccountTelegram>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AuthorizedAccountTelegram");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.PriceAccount)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TelegramCount)
                    .HasMaxLength(10)
                    .HasColumnName("telegramCount")
                    .IsFixedLength();
            });

            modelBuilder.Entity<DotaAccount>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DotaAccount");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.PriceAccaunt)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TelegramCount)
                    .HasMaxLength(10)
                    .HasColumnName("telegramCount")
                    .IsFixedLength();
            });

            modelBuilder.Entity<NewTelegramAccount>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("NewTelegramAccount");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.PriceAccount)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.TelegramCount)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
