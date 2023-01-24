using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Controllers;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Contexts;

public partial class MyFinancesContext : DbContext
{
	public MyFinancesContext()
	{
	}

	public MyFinancesContext(DbContextOptions<MyFinancesContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Bank> Banks { get; set; }

	public virtual DbSet<BankAccount> BankAccounts { get; set; }

	public virtual DbSet<CreditCard> CreditCards { get; set; }

	public virtual DbSet<Currency> Currencies { get; set; }

	public virtual DbSet<DebitCard> DebitCards { get; set; }

	public virtual DbSet<Deposit> Deposits { get; set; }

	public virtual DbSet<DesiredProduct> DesiredProducts { get; set; }

	public virtual DbSet<InterestRate> InterestRates { get; set; }

	public virtual DbSet<Loan> Loans { get; set; }

	public virtual DbSet<LocalStorage> LocalStorages { get; set; }

	public virtual DbSet<LocalStorageClassifier> LocalStorageClassifiers { get; set; }

	public virtual DbSet<PaymentSystem> PaymentSystems { get; set; }

	public virtual DbSet<Subscription> Subscriptions { get; set; }

	public virtual DbSet<Transaction> Transactions { get; set; }

	public virtual DbSet<TransactionForBankAccount> TransactionForBankAccounts { get; set; }

	public virtual DbSet<TransactionForCreditCard> TransactionForCreditCards { get; set; }

	public virtual DbSet<TransactionForDeposit> TransactionForDeposits { get; set; }

	public virtual DbSet<TransactionForLoan> TransactionForLoans { get; set; }

	public virtual DbSet<TransactionForLocalStorage> TransactionForLocalStorages { get; set; }

	public virtual DbSet<TransactionType> TransactionTypes { get; set; }
	public virtual DbSet<TransactionPost> TransactionPosts { get; set; }

	public virtual DbSet<User?> Users { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https: //go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
		=> optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=myfinances;User Id=adef;Password=199as55");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasPostgresExtension("pgcrypto");

		modelBuilder.Entity<Bank>(entity =>
		{
			entity.HasKey(e => e.BankId).HasName("banks_pkey");

			entity.ToTable("banks",
				tb => tb.HasComment("В данном классификаторе содержится название банка и его цвет (для визуализации)"));

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

		modelBuilder.Entity<CreditCard>(entity =>
		{
			entity.HasKey(e => e.CreditCardId).HasName("credit_cards_pkey");

			entity.ToTable("credit_cards", tb => tb.HasComment("Сущность для кредитной карты банка"));

			entity.Property(e => e.CreditCardId).HasColumnName("credit_card_id");
			entity.Property(e => e.ActionDate).HasColumnName("action_date");
			entity.Property(e => e.BankId).HasColumnName("bank_id");

			entity.Property(e => e.CreditLimit)
				.HasColumnType("money")
				.HasColumnName("credit_limit");

			entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

			entity.Property(e => e.Description)
				.HasMaxLength(150)
				.HasColumnName("description");

			entity.Property(e => e.FreePeriod).HasColumnName("free_period");

			entity.Property(e => e.Login)
				.HasMaxLength(20)
				.HasColumnName("login");

			entity.Property(e => e.Name)
				.HasMaxLength(15)
				.HasColumnName("name");

			entity.Property(e => e.PaymentSystemId).HasColumnName("payment_system_id");
			entity.Property(e => e.Visibility).HasColumnName("visibility");

		});

		modelBuilder.Entity<Currency>(entity =>
		{
			entity.HasKey(e => e.CurrencyId).HasName("currencies_pkey");

			entity.ToTable("currencies",
				tb => tb.HasComment("Сущность хранит наименования валют, их сокращенные названия и символы"));

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

		modelBuilder.Entity<DebitCard>(entity =>
		{
			entity.HasKey(e => e.DebitCardId).HasName("debit_cards_pkey");

			entity.ToTable("debit_cards", tb => tb.HasComment("Сущность, описывающая банковскую карту"));

			entity.Property(e => e.DebitCardId).HasColumnName("debit_card_id");
			entity.Property(e => e.ActionDate).HasColumnName("action_date");
			entity.Property(e => e.BankAccountId).HasColumnName("bank_account_id");

			entity.Property(e => e.Login)
				.HasMaxLength(20)
				.HasColumnName("login");

			entity.Property(e => e.PaymentSystemId).HasColumnName("payment_system_id");
			entity.Property(e => e.Visibility).HasColumnName("visibility");
		});

		modelBuilder.Entity<Deposit>(entity =>
		{
			entity.HasKey(e => e.DepositId).HasName("deposits_pkey");

			entity.ToTable("deposits");

			entity.Property(e => e.DepositId).HasColumnName("deposit_id");

			entity.Property(e => e.Amount)
				.HasColumnType("money")
				.HasColumnName("amount");

			entity.Property(e => e.BankId).HasColumnName("bank_id");
			entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

			entity.Property(e => e.Description)
				.HasMaxLength(150)
				.HasColumnName("description");

			entity.Property(e => e.InterestRate).HasColumnName("interest_rate");

			entity.Property(e => e.Login)
				.HasMaxLength(20)
				.HasColumnName("login");

			entity.Property(e => e.Name)
				.HasMaxLength(15)
				.HasColumnName("name");

			entity.Property(e => e.Period).HasColumnName("period");
			entity.Property(e => e.Visibility).HasColumnName("visibility");

		});

		modelBuilder.Entity<DesiredProduct>(entity =>
		{
			entity.HasKey(e => e.DesiredProductId).HasName("desired_products_pkey");

			entity.ToTable("desired_products",
				tb => tb.HasComment("Сущность для товара, который пользователь намерен приобрести в будущем"));

			entity.Property(e => e.DesiredProductId).HasColumnName("desired_product_id");
			entity.Property(e => e.CurrencyId).HasColumnName("currency_id");
			entity.Property(e => e.Link).HasColumnName("link");

			entity.Property(e => e.Login)
				.HasMaxLength(20)
				.HasColumnName("login");

			entity.Property(e => e.Name)
				.HasMaxLength(20)
				.HasColumnName("name");

			entity.Property(e => e.Price)
				.HasColumnType("money")
				.HasColumnName("price");

			entity.Property(e => e.ShopName)
				.HasMaxLength(20)
				.HasColumnName("shop_name");

		});

		modelBuilder.Entity<InterestRate>(entity =>
		{
			entity.HasKey(e => e.InterestRateId).HasName("interest_rates_pkey");

			entity.ToTable("interest_rates",
				tb => tb.HasComment("Данная сущность требуется для записи процентных ставок для кредита"));

			entity.Property(e => e.InterestRateId).HasColumnName("interest_rate_id");
			entity.Property(e => e.CreditCardId).HasColumnName("credit_card_id");

			entity.Property(e => e.Description)
				.HasMaxLength(150)
				.HasColumnName("description");

			entity.Property(e => e.Name)
				.HasMaxLength(15)
				.HasColumnName("name");

			entity.Property(e => e.Percent).HasColumnName("percent");

		});

		modelBuilder.Entity<Loan>(entity =>
		{
			entity.HasKey(e => e.LoanId).HasName("loans_pkey");

			entity.ToTable("loans", tb => tb.HasComment("Сущность для кредита в банке"));

			entity.Property(e => e.LoanId).HasColumnName("loan_id");

			entity.Property(e => e.Amount)
				.HasColumnType("money")
				.HasColumnName("amount");

			entity.Property(e => e.BankId).HasColumnName("bank_id");
			entity.Property(e => e.CreditPeriod).HasColumnName("credit_period");
			entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

			entity.Property(e => e.Description)
				.HasMaxLength(150)
				.HasColumnName("description");

			entity.Property(e => e.InterestRate).HasColumnName("interest_rate");

			entity.Property(e => e.Login)
				.HasMaxLength(20)
				.HasColumnName("login");

			entity.Property(e => e.Name)
				.HasMaxLength(15)
				.HasColumnName("name");

			entity.Property(e => e.StartDate).HasColumnName("start_date");
			entity.Property(e => e.Visibility).HasColumnName("visibility");

		});

		modelBuilder.Entity<LocalStorage>(entity =>
		{
			entity.HasKey(e => e.LocalStorageId).HasName("local_storage_pkey");

			entity.ToTable("local_storage",
				tb => tb.HasComment(
					"Сущность хранилища, предназначенное для наличных денег, например, кошелек, копилка и т.п."));

			entity.Property(e => e.LocalStorageId).HasColumnName("local_storage_id");

			entity.Property(e => e.Balance)
				.HasColumnType("money")
				.HasColumnName("balance");

			entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

			entity.Property(e => e.Description)
				.HasMaxLength(150)
				.HasColumnName("description");

			entity.Property(e => e.LocalStorageClassifierId).HasColumnName("local_storage_classifier_id");

			entity.Property(e => e.Login)
				.HasMaxLength(20)
				.HasColumnName("login");

			entity.Property(e => e.Name)
				.HasMaxLength(15)
				.HasColumnName("name");

			entity.Property(e => e.Visibility).HasColumnName("visibility");

		});

		modelBuilder.Entity<LocalStorageClassifier>(entity =>
		{
			entity.HasKey(e => e.LocalStorageClassifierId).HasName("local_storage_classifier_pkey");

			entity.ToTable("local_storage_classifier",
				tb => tb.HasComment(
					"В данном классификаторе, содержится информация для визуализации приложения, такая как, название и номер иконки выбранного хранилища"));

			entity.Property(e => e.LocalStorageClassifierId).HasColumnName("local_storage_classifier_id");

			entity.Property(e => e.Name)
				.HasMaxLength(15)
				.HasColumnName("name");

			entity.Property(e => e.PictureNumber).HasColumnName("picture_number");
		});

		modelBuilder.Entity<PaymentSystem>(entity =>
		{
			entity.HasKey(e => e.PaymentSystemId).HasName("payment_systems_pkey");

			entity.ToTable("payment_systems",
				tb => tb.HasComment("Классификатор платежных систем, содержащий в себе название и изображение"));

			entity.Property(e => e.PaymentSystemId).HasColumnName("payment_system_id");
			entity.Property(e => e.ImagePath).HasColumnName("image_path");

			entity.Property(e => e.Name)
				.HasMaxLength(20)
				.HasColumnName("name");
		});

		modelBuilder.Entity<Subscription>(entity =>
		{
			entity.HasKey(e => e.SubscriptionId).HasName("subscriptions_pkey");

			entity.ToTable("subscriptions",
				tb => tb.HasComment("Сущность для подписок на различные сервисы (например, музыка)"));

			entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");
			entity.Property(e => e.BankAccountId).HasColumnName("bank_account_id");
			entity.Property(e => e.EndDate).HasColumnName("end_date");

			entity.Property(e => e.Login)
				.HasMaxLength(20)
				.HasColumnName("login");

			entity.Property(e => e.Name)
				.HasMaxLength(20)
				.HasColumnName("name");

			entity.Property(e => e.Price)
				.HasColumnType("money")
				.HasColumnName("price");

			entity.Property(e => e.PurchaseDate).HasColumnName("purchase_date");

		});

		modelBuilder.Entity<Transaction>(entity =>
		{
			entity.HasKey(e => e.TransactionId).HasName("transactions_pkey");

			entity.HasIndex(e => e.TransactionDate, "transactions_transaction_date_idx");

			entity.HasIndex(e => e.TransactionId, "transactions_transaction_id_idx").IsUnique();

			//entity.HasIndex(e => e.TransactionTypeId, "transactions_transaction_type_id_idx");

			entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

			entity.Property(e => e.Amount)
				.HasColumnType("money")
				.HasColumnName("amount");

			entity.Property(e => e.Description)
				.HasMaxLength(150)
				.HasColumnName("description");

			entity.Property(e => e.Login)
				.HasMaxLength(20)
				.HasColumnName("login");

			entity.Property(e => e.TransactionDate)
				.HasColumnType("timestamp without time zone")
				.HasColumnName("transaction_date");

			//entity.Property(e => e.TransactionTypeId).HasColumnName("transaction_type_id");
			entity.Property(e => e.CurrencySign).HasColumnName("currency_sign");
			entity.Property(e => e.WalletName).HasColumnName("wallet_name");

		});

		modelBuilder.Entity<TransactionForBankAccount>(entity =>
		{
			entity
				.HasNoKey()
				.ToView("transaction_for_bank_account");

			entity.Property(e => e.Class)
				.HasMaxLength(1)
				.HasColumnName("class");

			entity.Property(e => e.Name)
				.HasMaxLength(20)
				.HasColumnName("name");

			entity.Property(e => e.TransactionTypeId).HasColumnName("transaction_type_id");
		});

		modelBuilder.Entity<TransactionForCreditCard>(entity =>
		{
			entity
				.HasNoKey()
				.ToView("transaction_for_credit_card");

			entity.Property(e => e.Class)
				.HasMaxLength(1)
				.HasColumnName("class");

			entity.Property(e => e.Name)
				.HasMaxLength(20)
				.HasColumnName("name");

			entity.Property(e => e.TransactionTypeId).HasColumnName("transaction_type_id");
		});

		modelBuilder.Entity<TransactionForDeposit>(entity =>
		{
			entity
				.HasNoKey()
				.ToView("transaction_for_deposit");

			entity.Property(e => e.Class)
				.HasMaxLength(1)
				.HasColumnName("class");

			entity.Property(e => e.Name)
				.HasMaxLength(20)
				.HasColumnName("name");

			entity.Property(e => e.TransactionTypeId).HasColumnName("transaction_type_id");
		});

		modelBuilder.Entity<TransactionForLoan>(entity =>
		{
			entity
				.HasNoKey()
				.ToView("transaction_for_loan");

			entity.Property(e => e.Class)
				.HasMaxLength(1)
				.HasColumnName("class");

			entity.Property(e => e.Name)
				.HasMaxLength(20)
				.HasColumnName("name");

			entity.Property(e => e.TransactionTypeId).HasColumnName("transaction_type_id");
		});

		modelBuilder.Entity<TransactionForLocalStorage>(entity =>
		{
			entity
				.HasNoKey()
				.ToView("transaction_for_local_storage");

			entity.Property(e => e.Class)
				.HasMaxLength(1)
				.HasColumnName("class");

			entity.Property(e => e.Name)
				.HasMaxLength(20)
				.HasColumnName("name");

			entity.Property(e => e.TransactionTypeId).HasColumnName("transaction_type_id");
		});

		modelBuilder.Entity<TransactionType>(entity =>
		{
			entity.HasKey(e => e.TransactionTypeId).HasName("transaction_types_pkey");

			entity.ToTable("transaction_types",
				tb => tb.HasComment("Классификатор для определения вида транзакции (перевод, платеж, пополнение и т.п.)"));

			entity.Property(e => e.TransactionTypeId).HasColumnName("transaction_type_id");

			entity.Property(e => e.Class)
				.HasMaxLength(1)
				.HasColumnName("class");

			entity.Property(e => e.Name)
				.HasMaxLength(20)
				.HasColumnName("name");
		});

		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(e => e.Login).HasName("users_pkey");

			entity.ToTable("users", tb => tb.HasComment("Сущность содержит в себе информацию о пользователе приложения"));

			entity.Property(e => e.Login)
				.HasMaxLength(20)
				.HasColumnName("login");

			entity.Property(e => e.BirthDate).HasColumnName("birth_date");

			entity.Property(e => e.Email)
				.HasMaxLength(320)
				.HasColumnName("email");

			entity.Property(e => e.FirstName)
				.HasMaxLength(25)
				.HasColumnName("first_name");

			entity.Property(e => e.LastName)
				.HasMaxLength(25)
				.HasColumnName("last_name");

			entity.Property(e => e.Password).HasColumnName("password");
			entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");
		});

		modelBuilder.Entity<TransactionPost>(entity =>
		{
			entity.HasKey(e => e.TransactionId).HasName("transactions_pkey");

			entity.ToTable("transactions", tb => tb.HasComment("Сущность для транзакций осуществляющих пользователем"));

			entity.HasIndex(e => e.BankAccountId, "transactions_bank_account_id_idx");

			entity.HasIndex(e => e.CreditCardId, "transactions_credit_card_id_idx");

			entity.HasIndex(e => e.DepositId, "transactions_deposit_id_idx");

			entity.HasIndex(e => e.LoanId, "transactions_loan_id_idx");

			entity.HasIndex(e => e.LocalStorageId, "transactions_local_storage_id_idx");

			entity.HasIndex(e => e.TransactionDate, "transactions_transaction_date_idx");

			entity.HasIndex(e => e.TransactionId, "transactions_transaction_id_idx").IsUnique();

			entity.HasIndex(e => e.TransactionTypeId, "transactions_transaction_type_id_idx");

			entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

			entity.Property(e => e.Amount)
				.HasColumnType("money")
				.HasColumnName("amount");

			entity.Property(e => e.BankAccountId).HasColumnName("bank_account_id");
			entity.Property(e => e.CreditCardId).HasColumnName("credit_card_id");
			entity.Property(e => e.DepositId).HasColumnName("deposit_id");

			entity.Property(e => e.Description)
				.HasMaxLength(150)
				.HasColumnName("description");

			entity.Property(e => e.LoanId).HasColumnName("loan_id");
			entity.Property(e => e.LocalStorageId).HasColumnName("local_storage_id");

			entity.Property(e => e.Login)
				.HasMaxLength(20)
				.HasColumnName("login");

			entity.Property(e => e.TransactionDate)
				.HasColumnType("timestamp without time zone")
				.HasColumnName("transaction_date");

			entity.Property(e => e.TransactionTypeId).HasColumnName("transaction_type_id");

		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}