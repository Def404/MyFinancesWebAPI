using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionsController : ControllerBase
	{
		private readonly MyFinancesContext _context;

		public TransactionsController(MyFinancesContext context)
		{
			_context = context;
		}

		// GET: api/Transactions
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
		{
			if (_context.Transactions == null)
			{
				return NotFound();
			}

			return await _context.Transactions.ToListAsync();
		}

		// GET: api/Transactions/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Transaction>> GetTransaction(long id)
		{
			if (_context.Transactions == null)
			{
				return NotFound();
			}

			var transaction = await _context.Transactions.FindAsync(id);

			if (transaction == null)
			{
				return NotFound();
			}

			return transaction;
		}
		
		// POST: api/Transactions
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
		{
			if (_context.Transactions == null)
			{
				return Problem("Entity set 'MyFinancesContext.Transactions'  is null.");
			}

			_context.Transactions.Add(transaction);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetTransaction", new{id = transaction.TransactionId}, transaction);
		}
		

		private bool TransactionExists(long id)
		{
			return (_context.Transactions?.Any(e => e.TransactionId == id)).GetValueOrDefault();
		}
	}
}