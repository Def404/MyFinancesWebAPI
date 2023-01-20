using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LocalStoragesController : ControllerBase
	{
		private readonly MyFinancesContext _context;

		public LocalStoragesController(MyFinancesContext context)
		{
			_context = context;
		}

		// GET: api/LocalStorages
		[HttpGet("GetLocalStorages")]
		public async Task<ActionResult<IEnumerable<LocalStorage>>> GetLocalStorages(string login)
		{
			if (_context.LocalStorages == null)
			{
				return NotFound();
			}

			return await _context.LocalStorages
				.Include(ls => ls.Currency)
				.Include(ls => ls.LocalStorageClassifier)
				.Where(ls => ls.Login == login)
				.ToListAsync();
		}

		// GET: api/LocalStorages/5
		[HttpGet("GetLocalStorage")]
		public async Task<ActionResult<LocalStorage>> GetLocalStorage(long id)
		{
			if (_context.LocalStorages == null)
			{
				return NotFound();
			}

			var localStorage = await _context.LocalStorages
				.Include(ls => ls.Currency)
				.Include(ls => ls.LocalStorageClassifier)
				.FirstOrDefaultAsync(ls => ls.LocalStorageId == id);

			if (localStorage == null)
			{
				return NotFound();
			}

			return localStorage;
		}
		
		[HttpGet("GetLocalStorageId")]
		public async Task<ActionResult<long>> GetLocalStorageId(string name, string login)
		{
			if (_context.LocalStorages == null)
			{
				return NotFound();
			}

			var localStorage = await _context.LocalStorages
				.Include(ls => ls.Currency)
				.Include(ls => ls.LocalStorageClassifier)
				.FirstOrDefaultAsync(ls => ls.Name == name && ls.Login == login);

			if (localStorage == null)
			{
				return NotFound();
			}

			return localStorage.LocalStorageId;
		}

		// PUT: api/LocalStorages/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutLocalStorage(long id, LocalStorage localStorage)
		{
			if (id != localStorage.LocalStorageId)
			{
				return BadRequest();
			}

			_context.Entry(localStorage).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!LocalStorageExists(id))
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

		// POST: api/LocalStorages
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<LocalStorage>> PostLocalStorage(LocalStorage localStorage)
		{
			if (_context.LocalStorages == null)
			{
				return Problem("Entity set 'MyFinancesContext.LocalStorages'  is null.");
			}

			_context.LocalStorages.Add(localStorage);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetLocalStorage", new{id = localStorage.LocalStorageId}, localStorage);
		}
		

		private bool LocalStorageExists(long id)
		{
			return (_context.LocalStorages?.Any(e => e.LocalStorageId == id)).GetValueOrDefault();
		}
	}
}