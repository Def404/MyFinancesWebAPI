using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DesiredProductsController : ControllerBase
	{
		private readonly MyFinancesContext _context;

		public DesiredProductsController(MyFinancesContext context)
		{
			_context = context;
		}

		// GET: api/DesiredProducts
		[HttpGet("getAllDesiredProducts")]
		public async Task<ActionResult<IEnumerable<DesiredProduct>>> GetDesiredProducts(string login)
		{
			if (_context.DesiredProducts == null)
				return NotFound();

			return await _context.DesiredProducts
				.Include(dp => dp.Currency)
				.Where(dp => dp.Login == login)
				.ToListAsync();
		}

		// GET: api/DesiredProducts/5
		[HttpGet("getDesiredProduct")]
		public async Task<ActionResult<DesiredProduct>> GetDesiredProduct(long id)
		{
			if (_context.DesiredProducts == null)
				return NotFound();

			var desiredProduct = await _context.DesiredProducts
				.Include(dp => dp.Currency)
				.FirstOrDefaultAsync(dp => dp.DesiredProductId == id);

			if (desiredProduct == null)
				return NotFound();

			return desiredProduct;
		}

		// PUT: api/DesiredProducts/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutDesiredProduct(long id, DesiredProduct desiredProduct)
		{
			if (id != desiredProduct.DesiredProductId)
				return BadRequest();

			_context.Entry(desiredProduct).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!DesiredProductExists(id))
					return NotFound();
				
				throw;
			}

			return NoContent();
		}

		// POST: api/DesiredProducts
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<DesiredProduct>> PostDesiredProduct(DesiredProduct desiredProduct)
		{
			if (_context.DesiredProducts == null)
				return Problem("Entity set 'MyFinancesContext.DesiredProducts'  is null.");

			_context.DesiredProducts.Add(desiredProduct);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetDesiredProduct", new{id = desiredProduct.DesiredProductId}, desiredProduct);
		}

		// DELETE: api/DesiredProducts/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDesiredProduct(long id)
		{
			if (_context.DesiredProducts == null)
				return NotFound();

			var desiredProduct = await _context.DesiredProducts.FindAsync(id);

			if (desiredProduct == null)
				return NotFound();

			_context.DesiredProducts.Remove(desiredProduct);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool DesiredProductExists(long id)
		{
			return (_context.DesiredProducts?.Any(e => e.DesiredProductId == id)).GetValueOrDefault();
		}
	}
}