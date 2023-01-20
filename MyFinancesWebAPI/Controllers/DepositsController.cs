using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepositsController : ControllerBase
	{
		private readonly MyFinancesContext _context;

		public DepositsController(MyFinancesContext context)
		{
			_context = context;
		}

		// GET: api/Deposits
		[HttpGet("login")]
		public async Task<ActionResult<IEnumerable<Deposit>>> GetDeposits([FromQuery]string login)
		{
			if (_context.Deposits == null)
				return NotFound();

			return await _context.Deposits
				.Include(d => d.Bank)
				.Include(d => d.Currency)
				.Where(d => d.Login == login).
				ToListAsync();
		}

		// GET: api/Deposits/5
		[HttpGet("id")]
		public async Task<ActionResult<Deposit>> GetDeposit([FromQuery]long id)
		{
			if (_context.Deposits == null)
				return NotFound();

			var deposit = await _context.Deposits
				.Include(d => d.Bank)
				.Include(d => d.Currency)
				.FirstOrDefaultAsync(d => d.DepositId == id);

			if (deposit == null)
				return NotFound();

			return deposit;
		}
		
		// GET: api/Deposits/5
		[HttpGet("name")]
		public async Task<ActionResult<Deposit>> GetDepositId([FromQuery]string name)
		{
			if (_context.Deposits == null)
				return NotFound();

			var deposit = await _context.Deposits
				.Include(d => d.Bank)
				.Include(d => d.Currency)
				.FirstOrDefaultAsync(d => d.Name == name);

			if (deposit == null)
				return NotFound();

			return deposit;
		}

		// PUT: api/Deposits/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutDeposit(long id, Deposit deposit)
		{
			if (id != deposit.DepositId)
				return BadRequest();

			_context.Entry(deposit).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!DepositExists(id))
					return NotFound();

				throw;
			}

			return NoContent();
		}

		// POST: api/Deposits
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Deposit>> PostDeposit(Deposit deposit)
		{
			if (_context.Deposits == null)
				return Problem("Entity set 'MyFinancesContext.Deposits'  is null.");

			_context.Deposits.Add(deposit);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetDeposit", new{id = deposit.DepositId}, deposit);
		}
		

		private bool DepositExists(long id)
		{
			return (_context.Deposits?.Any(e => e.DepositId == id)).GetValueOrDefault();
		}
	}
}