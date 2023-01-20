using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoansController : ControllerBase
	{
		private readonly MyFinancesContext _context;

		public LoansController(MyFinancesContext context)
		{
			_context = context;
		}

		// GET: api/Loans
		[HttpGet("GetLoans")]
		public async Task<ActionResult<IEnumerable<Loan>>> GetLoans(string login)
		{
			if (_context.Loans == null)
				return NotFound();

			return await _context.Loans
				.Include(l => l.Bank)
				.Include(l => l.Currency)
				.Where(l => l.Login == login)
				.ToListAsync();
		}

		// GET: api/Loans/5
		[HttpGet("GetLoan")]
		public async Task<ActionResult<Loan>> GetLoan(long id)
		{
			if (_context.Loans == null)
				return NotFound();

			var loan = await _context.Loans
				.Include(l => l.Bank)
				.Include(l => l.Currency)
				.FirstOrDefaultAsync(l => l.LoanId == id);

			if (loan == null)
				return NotFound();

			return loan;
		}

		// GET: api/Loans/5
		[HttpGet("GetLoanId")]
		public async Task<ActionResult<long>> GetLoanId(string name)
		{
			if (_context.Loans == null)
				return NotFound();

			var loan = await _context.Loans
				.Include(l => l.Bank)
				.Include(l => l.Currency)
				.FirstOrDefaultAsync(l => l.Name == name);

			if (loan == null)
				return NotFound();

			return loan.LoanId;
		}

		// PUT: api/Loans/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutLoan(long id, Loan loan)
		{
			if (id != loan.LoanId)
				return BadRequest();

			_context.Entry(loan).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!LoanExists(id))
					return NotFound();
				throw;
			}

			return NoContent();
		}

		// POST: api/Loans
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Loan>> PostLoan(Loan loan)
		{
			if (_context.Loans == null)
				return Problem("Entity set 'MyFinancesContext.Loans'  is null.");

			_context.Loans.Add(loan);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetLoan", new{id = loan.LoanId}, loan);
		}


		private bool LoanExists(long id)
		{
			return (_context.Loans?.Any(e => e.LoanId == id)).GetValueOrDefault();
		}
	}
}