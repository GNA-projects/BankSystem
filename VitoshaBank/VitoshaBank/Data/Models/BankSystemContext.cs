using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VitoshaBank.Data.Models
{
    public partial class BankSystemContext : DbContext
    {
        public BankSystemContext()
        {
        }

        public BankSystemContext(DbContextOptions<BankSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BankAccounts> BankAccounts { get; set; }
        public virtual DbSet<Cards> Cards { get; set; }
        public virtual DbSet<Credits> Credits { get; set; }
        public virtual DbSet<Deposits> Deposits { get; set; }
        public virtual DbSet<SupportTickets> SupportTickets { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Wallets> Wallets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccounts>(entity =>
            {
                entity.HasIndex(e => e.CardId)
                    .HasName("card_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Iban)
                    .HasName("IBAN_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.CardId)
                    .HasColumnName("card_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Iban)
                    .IsRequired()
                    .HasColumnName("IBAN")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Card)
                    .WithOne(p => p.BankAccounts)
                    .HasForeignKey<BankAccounts>(d => d.CardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_card_id_Cards_id");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.BankAccounts)
                    .HasForeignKey<BankAccounts>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_id_Users_id");
            });

            modelBuilder.Entity<Cards>(entity =>
            {
                entity.HasIndex(e => e.BankAccountId)
                    .HasName("bankAccount_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CardNumber)
                    .HasName("cart_number_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.BankAccountId)
                    .HasColumnName("bankAccount_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasColumnName("card_number")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Cvv)
                    .HasColumnName("CVV")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.BankAccount)
                    .WithOne(p => p.Cards)
                    .HasForeignKey<Cards>(d => d.BankAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_card_bankAccount_id_BankAccount_id");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Cards)
                    .HasForeignKey<Cards>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_card_user_id_Users_id");
            });

            modelBuilder.Entity<Credits>(entity =>
            {
                entity.HasIndex(e => e.Iban)
                    .HasName("IBAN_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.CreditAmount)
                    .HasColumnName("credit_amount")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.Iban)
                    .IsRequired()
                    .HasColumnName("IBAN")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.Instalment)
                    .HasColumnName("instalment")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.Interest)
                    .HasColumnName("interest")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Credits)
                    .HasForeignKey<Credits>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_credits_user_id_Users_id");
            });

            modelBuilder.Entity<Deposits>(entity =>
            {
                entity.HasIndex(e => e.Iban)
                    .HasName("IBAN_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.Divident)
                    .HasColumnName("divident")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.Iban)
                    .IsRequired()
                    .HasColumnName("IBAN")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.PaymentDate)
                    .HasColumnName("payment_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.TermOfPayment)
                    .HasColumnName("term_of_payment")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Deposits)
                    .HasForeignKey<Deposits>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_deposits_user_id_Users_id");
            });

            modelBuilder.Entity<SupportTickets>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.HasResponce)
                    .HasColumnName("hasResponce")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.SupportTickets)
                    .HasForeignKey<SupportTickets>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tickets_user_id_Users_id");
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RecieverAccountId)
                    .HasName("fk_transactions_reciever_id_BankAccount_id");

                entity.HasIndex(e => e.SenderAccountId)
                    .HasName("fk_transactions_sender_id_BankAccount_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.RecieverAccountId)
                    .HasColumnName("reciever_account_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SenderAccountId)
                    .HasColumnName("sender_account_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TransactionAmount)
                    .HasColumnName("transaction_amount")
                    .HasColumnType("decimal(10,0)");

                entity.HasOne(d => d.RecieverAccount)
                    .WithMany(p => p.TransactionsRecieverAccount)
                    .HasForeignKey(d => d.RecieverAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_transactions_reciever_id_BankAccount_id");

                entity.HasOne(d => d.SenderAccount)
                    .WithMany(p => p.TransactionsSenderAccount)
                    .HasForeignKey(d => d.SenderAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_transactions_sender_id_BankAccount_id");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("Userscol_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.LastTransactionId)
                    .HasName("fk_users_transaction_id_Transactions_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.LastTransactionId)
                    .HasColumnName("last_transaction_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.RegisterDate)
                    .HasColumnName("register_date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Wallets>(entity =>
            {
                entity.HasIndex(e => e.Amount)
                    .HasName("amount_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Iban)
                    .HasName("IBAN_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.Iban)
                    .IsRequired()
                    .HasColumnName("IBAN")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Wallets)
                    .HasForeignKey<Wallets>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_wallets_user_id_Users_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
