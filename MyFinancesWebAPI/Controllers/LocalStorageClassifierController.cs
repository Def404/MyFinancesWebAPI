using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LocalStorageClassifierController : ControllerBase
	{
		private readonly MyFinancesContext _context;

		public LocalStorageClassifierController(MyFinancesContext context)
		{
			_context = context;
		}

		// GET: api/LocalStorageClassifier
		[HttpGet]
		public async Task<ActionResult<IEnumerable<LocalStorageClassifier>>> GetLocalStorageClassifiers()
		{
			if (_context.LocalStorageClassifiers == null)
			{
				return NotFound();
			}

			return await _context.LocalStorageClassifiers.ToListAsync();
		}

		// GET: api/LocalStorageClassifier/5
		[HttpGet("{id}")]
		public async Task<ActionResult<LocalStorageClassifier>> GetLocalStorageClassifier(int id)
		{
			if (_context.LocalStorageClassifiers == null)
			{
				return NotFound();
			}

			var localStorageClassifier = await _context.LocalStorageClassifiers.FindAsync(id);

			if (localStorageClassifier == null)
			{
				return NotFound();
			}

			return localStorageClassifier;
		}

		// PUT: api/LocalStorageClassifier/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutLocalStorageClassifier(int id, LocalStorageClassifier localStorageClassifier)
		{
			if (id != localStorageClassifier.LocalStorageClassifierId)
			{
				return BadRequest();
			}

			_context.Entry(localStorageClassifier).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!LocalStorageClassifierExists(id))
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

		// POST: api/LocalStorageClassifier
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<LocalStorageClassifier>> PostLocalStorageClassifier(
			LocalStorageClassifier localStorageClassifier)
		{
			if (_context.LocalStorageClassifiers == null)
			{
				return Problem("Entity set 'MyFinancesContext.LocalStorageClassifiers'  is null.");
			}

			_context.LocalStorageClassifiers.Add(localStorageClassifier);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetLocalStorageClassifier", new{id = localStorageClassifier.LocalStorageClassifierId},
				localStorageClassifier);
		}

		// DELETE: api/LocalStorageClassifier/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteLocalStorageClassifier(int id)
		{
			if (_context.LocalStorageClassifiers == null)
			{
				return NotFound();
			}

			var localStorageClassifier = await _context.LocalStorageClassifiers.FindAsync(id);

			if (localStorageClassifier == null)
			{
				return NotFound();
			}

			_context.LocalStorageClassifiers.Remove(localStorageClassifier);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool LocalStorageClassifierExists(int id)
		{
			return (_context.LocalStorageClassifiers?.Any(e => e.LocalStorageClassifierId == id)).GetValueOrDefault();
		}
	}
}