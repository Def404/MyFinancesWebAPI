using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionTypesController : ControllerBase
	{
		private readonly MyFinancesContext _context;

		public TransactionTypesController(MyFinancesContext context)
		{
			_context = context;
		}

		// GET: api/TransactionTypes
		[HttpGet("GetTransactionTypes")]
		public async Task<ActionResult<IEnumerable<TransactionType>>> GetTransactionTypes()
		{
			if (_context.TransactionTypes == null)
			{
				return NotFound();
			}

			return await _context.TransactionTypes.ToListAsync();
		}
		
		[HttpGet("TransactionTypesForBankAccount")]
		public async Task<ActionResult<IEnumerable<TransactionForBankAccount>>> GetTransactionTypesForBankAccount()
		{
			if (_context.TransactionForBankAccounts == null)
			{
				return NotFound();
			}

			return await _context.TransactionForBankAccounts.ToListAsync();
		}

		[HttpGet("TransactionTypesForCreditCard")]
		public async Task<ActionResult<IEnumerable<TransactionForCreditCard>>> GetTransactionTypesForCreditCard()
		{
			if (_context.TransactionForCreditCards == null)
			{
				return NotFound();
			}

			return await _context.TransactionForCreditCards.ToListAsync();
		}
		
		[HttpGet("TransactionTypesForDeposit")]
		public async Task<ActionResult<IEnumerable<TransactionForDeposit>>> GetTransactionTypesForDeposit()
		{
			if (_context.TransactionForDeposits == null)
			{
				return NotFound();
			}

			return await _context.TransactionForDeposits.ToListAsync();
		}
		
		[HttpGet("TransactionTypesForLoan")]
		public async Task<ActionResult<IEnumerable<TransactionForLoan>>> GetTransactionTypesForLoan()
		{
			if (_context.TransactionForLoans == null)
			{
				return NotFound();
			}

			return await _context.TransactionForLoans.ToListAsync();
		}
		
		[HttpGet("TransactionTypesForLocalStorage")]
		public async Task<ActionResult<IEnumerable<TransactionForLocalStorage>>> GetTransactionTypesForLocalStorage()
		{
			if (_context.TransactionForLocalStorages == null)
			{
				return NotFound();
			}

			return await _context.TransactionForLocalStorages.ToListAsync();
		}
		
		
		// GET: api/TransactionTypes/5
		[HttpGet("{id}")]
		public async Task<ActionResult<TransactionType>> GetTransactionType(int id)
		{
			if (_context.TransactionTypes == null)
			{
				return NotFound();
			}

			var transactionType = await _context.TransactionTypes.FindAsync(id);

			if (transactionType == null)
			{
				return NotFound();
			}

			return transactionType;
		}

		// PUT: api/TransactionTypes/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTransactionType(int id, TransactionType transactionType)
		{
			if (id != transactionType.TransactionTypeId)
			{
				return BadRequest();
			}

			_context.Entry(transactionType).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TransactionTypeExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/TransactionTypes
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<TransactionType>> PostTransactionType(TransactionType transactionType)
		{
			if (_context.TransactionTypes == null)
			{
				return Problem("Entity set 'MyFinancesContext.TransactionTypes'  is null.");
			}

			_context.TransactionTypes.Add(transactionType);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetTransactionType", new{id = transactionType.TransactionTypeId}, transactionType);
		}

		// DELETE: api/TransactionTypes/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTransactionType(int id)
		{
			if (_context.TransactionTypes == null)
			{
				return NotFound();
			}

			var transactionType = await _context.TransactionTypes.FindAsync(id);

			if (transactionType == null)
			{
				return NotFound();
			}

			_context.TransactionTypes.Remove(transactionType);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool TransactionTypeExists(int id)
		{
			return (_context.TransactionTypes?.Any(e => e.TransactionTypeId == id)).GetValueOrDefault();
		}
	}
}