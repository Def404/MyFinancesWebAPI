using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SubscriptionsController : ControllerBase
	{
		private readonly MyFinancesContext _context;

		public SubscriptionsController(MyFinancesContext context)
		{
			_context = context;
		}

		// GET: api/Subsriptions
		[HttpGet("GetSubscriptions")]
		public async Task<ActionResult<IEnumerable<Subscription>>> GetSubscriptions(string login)
		{
			if (_context.Subscriptions == null)
			{
				return NotFound();
			}

			return await _context.Subscriptions
				.Include(s => s.BankAccount)
				.Include(s => s.BankAccount.Bank)
				.Include(s => s.BankAccount.Currency)
				.Where(s => s.Login == login)
				.ToListAsync();
		}

		// GET: api/Subsriptions/5
		[HttpGet("GetSubscription")]
		public async Task<ActionResult<Subscription>> GetSubscription(long id)
		{
			if (_context.Subscriptions == null)
			{
				return NotFound();
			}

			var subscription = await _context.Subscriptions
				.Include(s => s.BankAccount)
				.Include(s => s.BankAccount.Bank)
				.Include(s => s.BankAccount.Currency)
				.FirstOrDefaultAsync(s => s.SubscriptionId == id);

			if (subscription == null)
			{
				return NotFound();
			}

			return subscription;
		}

		[HttpPut("PutSubscription")]
		public async Task<IActionResult> PutSubscription(long id, Subscription subscription)
		{
			if (id != subscription.SubscriptionId)
			{
				return BadRequest();
			}
			
			_context.Entry(subscription).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!SubscriptionExists(id))
				{
					return NotFound();
				}
				throw;
			}

			return NoContent();
		}
		
		// POST: api/Subsriptions
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Subscription>> PostSubscription(Subscription subscription)
		{
			if (_context.Subscriptions == null)
			{
				return Problem("Entity set 'MyFinancesContext.Subscriptions'  is null.");
			}

			_context.Subscriptions.Add(subscription);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetSubscription", new{id = subscription.SubscriptionId}, subscription);
		}

		// DELETE: api/Subsriptions/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSubscription(long id)
		{
			if (_context.Subscriptions == null)
			{
				return NotFound();
			}

			var subscription = await _context.Subscriptions.FindAsync(id);

			if (subscription == null)
			{
				return NotFound();
			}

			_context.Subscriptions.Remove(subscription);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool SubscriptionExists(long id)
		{
			return (_context.Subscriptions?.Any(e => e.SubscriptionId == id)).GetValueOrDefault();
		}
	}
}