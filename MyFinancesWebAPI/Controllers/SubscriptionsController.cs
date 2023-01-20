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

		[HttpPut("PutStopSubscription")]
		public async Task<IActionResult> PutStopSubscription(long id)
		{
			var sub = await _context.Subscriptions.FindAsync(id);

			if (sub == null)
			{
				return NotFound();
			}
			
			sub.EndDate = DateOnly.FromDateTime(DateTime.Now);
			_context.Entry(sub).State = EntityState.Modified;

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
		
		[HttpPut("PutRenewSubscription")]
		public async Task<IActionResult> PutRenewSubscription(long id, DateOnly purchaseDate, DateOnly endDate)
		{
			var sub = await _context.Subscriptions.FindAsync(id);

			if (sub == null)
			{
				return NotFound();
			}

			sub.PurchaseDate = purchaseDate;
			sub.EndDate = endDate;
			_context.Entry(sub).State = EntityState.Modified;

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