namespace MyFinancesWebAPI.Models;

public class TransactionPost
{
	public long TransactionId { get; set; }

	public decimal Amount { get; set; }

	public DateTime TransactionDate { get; set; }

	public string? Description { get; set; }

	public string Login { get; set; } = null!;

	public long? BankAccountId { get; set; }

	public long? DepositId { get; set; }

	public long? CreditCardId { get; set; }

	public long? LoanId { get; set; }

	public long? LocalStorageId { get; set; }

	public int TransactionTypeId { get; set; }

	public virtual BankAccount? BankAccount { get; set; }

	public virtual CreditCard? CreditCard { get; set; }

	public virtual Deposit? Deposit { get; set; }

	public virtual Loan? Loan { get; set; }

	public virtual LocalStorage? LocalStorage { get; set; }
    
	public virtual TransactionType? TransactionType { get; set; }
}