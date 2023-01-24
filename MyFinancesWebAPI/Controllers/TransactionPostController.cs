using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionPostController : ControllerBase
{
	private readonly MyFinancesContext _context;

	public TransactionPostController(MyFinancesContext context)
	{
		_context = context;
	}

	// GET: api/TransactionPost
	[HttpGet("GetTransactionPost")]
	public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionPosts(string login)
	{
		if (_context.TransactionPosts == null)
			return NotFound();
		
		var transactionPosts = await _context.TransactionPosts
			.Include(t => t.BankAccount)
				.Include(t => t.BankAccount.Currency)
			.Include(t => t.CreditCard)
				.Include(t => t.CreditCard.Currency)
			.Include(t => t.Loan)
				.Include(t => t.Loan.Currency)
			.Include(t => t.Deposit)
				.Include(t => t.Deposit.Currency)
			.Include(t => t.LocalStorage)
				.Include(t => t.LocalStorage.Currency)
			.Include(t => t.TransactionType)
			.Where(t => t.Login == login)
			.OrderBy(t => t.TransactionDate)
			.ToListAsync();
		
		return RewTransactions(transactionPosts);
	}
	
	// GET: api/TransactionPost
	[HttpGet("GetTransactionByWalletType")]
	public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionByWalletType(string login, int type, DateTime? firstDate, DateTime? endDate)
	{
		if (_context.TransactionPosts == null)
			return NotFound();
		
		firstDate ??= DateTime.MinValue;
		endDate ??= DateTime.MaxValue;

		var transactionQ = await _context.TransactionPosts
			.Include(t => t.BankAccount)
			.Include(t => t.BankAccount.Currency)
			.Include(t => t.CreditCard)
			.Include(t => t.CreditCard.Currency)
			.Include(t => t.Loan)
			.Include(t => t.Loan.Currency)
			.Include(t => t.Deposit)
			.Include(t => t.Deposit.Currency)
			.Include(t => t.LocalStorage)
			.Include(t => t.LocalStorage.Currency)
			.Include(t => t.TransactionType)
			.Where(t => t.Login == login)
			.ToListAsync();

		var transactionPosts = type switch // 0 - bankAccount, 1 - debitCard, 2 - localStorage, 3 - deposit, 4 - creditCard, 5 - loan 
		{
			0 => transactionQ
				.Where(t => t.BankAccountId != null)
				.Where(b => !_context.DebitCards.Select(d => d.BankAccountId).Contains(b.BankAccountId))
				.Where(t => t.TransactionDate >= firstDate && t.TransactionDate <= endDate)
				.OrderBy(t => t.TransactionDate)
				.ToList(),
			1 => transactionQ
				.Where(t => t.BankAccount != null)
				.Where(b => _context.DebitCards.Select(d => d.BankAccountId).Contains(b.BankAccountId))
				.Where(t => t.TransactionDate >= firstDate && t.TransactionDate <= endDate)
				.OrderBy(t => t.TransactionDate)
				.ToList(),
			2 => transactionQ
				.Where(t => t.LocalStorageId != null)
				.Where(t => t.TransactionDate >= firstDate && t.TransactionDate <= endDate)
				.OrderBy(t => t.TransactionDate)
				.ToList(),
			3 => transactionQ
				.Where(t => t.Deposit != null)
				.Where(t => t.TransactionDate >= firstDate && t.TransactionDate <= endDate)
				.OrderBy(t => t.TransactionDate)
				.ToList(),
			4 => transactionQ
				.Where(t => t.CreditCardId != null)
				.Where(t => t.TransactionDate >= firstDate && t.TransactionDate <= endDate)
				.OrderBy(t => t.TransactionDate)
				.ToList(),
			5 => transactionQ
				.Where(t => t.LoanId != null)
				.Where(t => t.TransactionDate >= firstDate && t.TransactionDate <= endDate)
				.OrderBy(t => t.TransactionDate)
				.ToList(),
			_ => null
		};

		if (transactionPosts.Count == 0 || transactionPosts == null)
			return NotFound();

		return RewTransactions(transactionPosts);
	}

	/*[HttpGet("GetTransactionByWalletName")]
	public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionByWalletName(string login, string walletName, 
		DateTime? firstDate, 
		DateTime? endDate)
	{
		if (_context.TransactionPosts == null)
			return NotFound();
		
		firstDate ??= DateTime.MinValue;
		endDate ??= DateTime.MaxValue;
		
		var transactionPosts = await _context.TransactionPosts
			.Include(t => t.BankAccount)
			.Include(t => t.BankAccount.Currency)
			.Include(t => t.CreditCard)
			.Include(t => t.CreditCard.Currency)
			.Include(t => t.Loan)
			.Include(t => t.Loan.Currency)
			.Include(t => t.Deposit)
			.Include(t => t.Deposit.Currency)
			.Include(t => t.LocalStorage)
			.Include(t => t.LocalStorage.Currency)
			.Include(t => t.TransactionType)
			.Where(t => t.Login == login && t.)
			.OrderBy(t => t.TransactionDate)
			.ToListAsync();
		
		return RewTransactions(transactionPosts);
	}*/
	
	[HttpGet("GetTransactionByDate")]
	public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionByDate(string login, DateTime? firstDate, 
		DateTime? endDate)
	{
		if (_context.TransactionPosts == null)
			return NotFound();
		
		firstDate ??= DateTime.MinValue;
		endDate ??= DateTime.MaxValue;
		
		var transactionPosts = await _context.TransactionPosts
			.Include(t => t.BankAccount)
			.Include(t => t.BankAccount.Currency)
			.Include(t => t.CreditCard)
			.Include(t => t.CreditCard.Currency)
			.Include(t => t.Loan)
			.Include(t => t.Loan.Currency)
			.Include(t => t.Deposit)
			.Include(t => t.Deposit.Currency)
			.Include(t => t.LocalStorage)
			.Include(t => t.LocalStorage.Currency)
			.Include(t => t.TransactionType)
			.Where(t => t.Login == login)
			.Where(t =>
				t.TransactionDate >= firstDate && 
				t.TransactionDate <= endDate)
			.OrderBy(t => t.TransactionDate)
			.ToListAsync();
		
		return RewTransactions(transactionPosts);
	}
	
	// GET: api/TransactionPost/5
	[HttpGet("{id}")]
	public async Task<ActionResult<TransactionPost>> GetTransactionPost(long id)
	{
		if (_context.TransactionPosts == null)
		{
			return NotFound();
		}

		var transactionPost = await _context.TransactionPosts.FindAsync(id);

		if (transactionPost == null)
		{
			return NotFound();
		}

		return transactionPost;
	}

	// POST: api/TransactionPost
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPost]
	public async Task<ActionResult<TransactionPost>> PostTransactionPost(TransactionPost transactionPost)
	{
		if (_context.TransactionPosts == null)
		{
			return Problem("Entity set 'MyFinancesContext.TransactionPosts'  is null.");
		}

		_context.TransactionPosts.Add(transactionPost);
		await _context.SaveChangesAsync();

		return CreatedAtAction("GetTransactionPost", new{id = transactionPost.TransactionId}, transactionPost);
	}

	private static List<Transaction> RewTransactions(List<TransactionPost> transactionPosts)
	{
		var returnTransaction = new List<Transaction>();

		foreach (var transaction in transactionPosts)
		{
			var walletName = "";
			var currencySign = '\0';

			if (transaction.BankAccountId != null)
			{
				walletName = transaction.BankAccount.Name;
				currencySign = transaction.BankAccount.Currency.Sign;
			}else if (transaction.CreditCard != null)
			{
				walletName = transaction.CreditCard.Name;
				currencySign = transaction.CreditCard.Currency.Sign;
			}
			else if (transaction.Loan != null)
			{
				walletName = transaction.Loan.Name;
				currencySign = transaction.Loan.Currency.Sign;
			}
			else if (transaction.Deposit != null)
			{
				walletName = transaction.Deposit.Name;
				currencySign = transaction.Deposit.Currency.Sign;
			}
			else if (transaction.LocalStorage != null)
			{
				walletName = transaction.LocalStorage.Name;
				currencySign = transaction.LocalStorage.Currency.Sign;
			}
			
			returnTransaction.Add(new Transaction{
				TransactionId = transaction.TransactionId,
				Amount = transaction.Amount,
				TransactionDate = transaction.TransactionDate,
				Description = transaction.Description,
				Login = transaction.Login,
				TransactionType = transaction.TransactionType,
				WalletName = walletName,
				CurrencySign = currencySign
			});
		}

		return returnTransaction;
	}
}