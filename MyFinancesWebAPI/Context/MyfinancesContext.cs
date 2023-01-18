using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Context;

public partial class MyfinancesContext : DbContext
{
    public MyfinancesContext()
    {
    }

    public MyfinancesContext(DbContextOptions<MyfinancesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<BankAccount> BankAccounts { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=myfinances;User Id=adef;Password=199as55");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.BankId).HasName("banks_pkey");

            entity.ToTable("banks", tb => tb.HasComment("В данном классификаторе содержится название банка и его цвет (для визуализации)"));

            entity.Property(e => e.BankId).HasColumnName("bank_id");
            entity.Property(e => e.Colour)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("colour");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<BankAccount>(entity =>
        {
            entity.HasKey(e => e.BankAccountId).HasName("bank_accounts_pkey");

            entity.ToTable("bank_accounts", tb => tb.HasComment("Сущность для счета в банке"));

            entity.Property(e => e.BankAccountId).HasColumnName("bank_account_id");
            entity.Property(e => e.Balance)
                .HasColumnType("money")
                .HasColumnName("balance");
            entity.Property(e => e.BankId).HasColumnName("bank_id");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .HasColumnName("description");
            entity.Property(e => e.Login)
                .HasMaxLength(20)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .HasColumnName("name");
            entity.Property(e => e.Visibility).HasColumnName("visibility");
            
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyId).HasName("currencies_pkey");

            entity.ToTable("currencies", tb => tb.HasComment("Сущность хранит наименования валют, их сокращенные названия и символы"));

            entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .HasColumnName("name");
            entity.Property(e => e.ShortName)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("short_name");
            entity.Property(e => e.Sign)
                .HasMaxLength(1)
                .HasColumnName("sign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
