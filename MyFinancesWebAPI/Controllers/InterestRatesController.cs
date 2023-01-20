using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InterestRatesController : ControllerBase
	{
		private readonly MyFinancesContext _context;

		public InterestRatesController(MyFinancesContext context)
		{
			_context = context;
		}
		
		// PUT: api/InterestRates/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutInterestRate(int id, InterestRate interestRate)
		{
			if (id != interestRate.InterestRateId)
				return BadRequest();

			_context.Entry(interestRate).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!InterestRateExists(id))
					return NotFound();
				
				throw;
			}

			return NoContent();
		}
		
		private bool InterestRateExists(int id)
		{
			return (_context.InterestRates?.Any(e => e.InterestRateId == id)).GetValueOrDefault();
		}
	}
}