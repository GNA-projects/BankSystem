using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VitoshaBank.Data.Models
{
    public partial class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext()
        {
        }

        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Credit> Credits { get; set; }
        public virtual DbSet<Deposit> Deposits { get; set; }
        public virtual DbSet<SupportTicke> SupportTickes { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=remotemysql.com;port=3306;user=7Fv3OS8L1w;password=q5yOBEVSOh;database=7Fv3OS8L1w", x => x.ServerVersion("8.0.13-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.HasIndex(e => e.Iban, "IBAN_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CardId, "card_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "user_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasPrecision(10)
                    .HasColumnName("amount");

                entity.Property(e => e.CardId)
                    .HasColumnType("int(11)")
                    .HasColumnName("card_id");

                entity.Property(e => e.Iban)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("IBAN")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Card)
                    .WithOne(p => p.BankAccount)
                    .HasForeignKey<BankAccount>(d => d.CardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_card_id_Cards_id");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.BankAccount)
                    .HasForeignKey<BankAccount>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_id_Users_id");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasIndex(e => e.BankAccountId, "bankAccount_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CardNumber, "cart_number_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "user_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasPrecision(10)
                    .HasColumnName("amount");

                entity.Property(e => e.BankAccountId)
                    .HasColumnType("int(11)")
                    .HasColumnName("bankAccount_id");

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("card_number")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Cvv)
                    .HasColumnType("int(11)")
                    .HasColumnName("CVV");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.BankAccountNavigation)
                    .WithOne(p => p.CardNavigation)
                    .HasForeignKey<Card>(d => d.BankAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_card_bankAccount_id_BankAccount_id");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Card)
                    .HasForeignKey<Card>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_card_user_id_Users_id");
            });

            modelBuilder.Entity<Credit>(entity =>
            {
                entity.HasIndex(e => e.Iban, "IBAN_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "user_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasPrecision(10)
                    .HasColumnName("amount");

                entity.Property(e => e.CreditAmount)
                    .HasPrecision(10)
                    .HasColumnName("credit_amount");

                entity.Property(e => e.Iban)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("IBAN")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Instalment)
                    .HasPrecision(10)
                    .HasColumnName("instalment");

                entity.Property(e => e.Interest)
                    .HasPrecision(10)
                    .HasColumnName("interest");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Credit)
                    .HasForeignKey<Credit>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_credits_user_id_Users_id");
            });

            modelBuilder.Entity<Deposit>(entity =>
            {
                entity.HasIndex(e => e.Iban, "IBAN_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "user_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasPrecision(10)
                    .HasColumnName("amount");

                entity.Property(e => e.Divident)
                    .HasPrecision(10)
                    .HasColumnName("divident");

                entity.Property(e => e.Iban)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("IBAN")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("payment_date");

                entity.Property(e => e.TermOfPayment)
                    .HasColumnType("int(11)")
                    .HasColumnName("term_of_payment");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Deposit)
                    .HasForeignKey<Deposit>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_deposits_user_id_Users_id");
            });

            modelBuilder.Entity<SupportTicke>(entity =>
            {
                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "user_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.HasResponce)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("hasResponce");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("title")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.SupportTicke)
                    .HasForeignKey<SupportTicke>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tickets_user_id_Users_id");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasIndex(e => e.RecieverAccountId, "fk_transactions_reciever_id_BankAccount_id");

                entity.HasIndex(e => e.SenderAccountId, "fk_transactions_sender_id_BankAccount_id");

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.RecieverAccountId)
                    .HasColumnType("int(11)")
                    .HasColumnName("reciever_account_id");

                entity.Property(e => e.SenderAccountId)
                    .HasColumnType("int(11)")
                    .HasColumnName("sender_account_id");

                entity.Property(e => e.TransactionAmount)
                    .HasPrecision(10)
                    .HasColumnName("transaction_amount");

                entity.HasOne(d => d.RecieverAccount)
                    .WithMany(p => p.TransactionRecieverAccounts)
                    .HasForeignKey(d => d.RecieverAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_transactions_reciever_id_BankAccount_id");

                entity.HasOne(d => d.SenderAccount)
                    .WithMany(p => p.TransactionSenderAccounts)
                    .HasForeignKey(d => d.SenderAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_transactions_sender_id_BankAccount_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "Userscol_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.LastTransactionId, "fk_users_transaction_id_Transactions_id");

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("datetime")
                    .HasColumnName("birth_date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("email")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("first_name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("last_name")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.LastTransactionId)
                    .HasColumnType("int(11)")
                    .HasColumnName("last_transaction_id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("password")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.RegisterDate)
                    .HasColumnType("datetime")
                    .HasColumnName("register_date");

                entity.HasOne(d => d.LastTransaction)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LastTransactionId)
                    .HasConstraintName("fk_users_transaction_id_Transactions_id");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasIndex(e => e.Iban, "IBAN_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Amount, "amount_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "user_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasPrecision(10)
                    .HasColumnName("amount");

                entity.Property(e => e.Iban)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasColumnName("IBAN")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Wallet)
                    .HasForeignKey<Wallet>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_wallets_user_id_Users_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
