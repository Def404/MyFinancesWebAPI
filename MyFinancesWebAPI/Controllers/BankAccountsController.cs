using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Context;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers{
	[Route("api/[controller]")]
	[ApiController]
	public class BankAccountsController : ControllerBase{
		private readonly MyfinancesContext _context;

		public BankAccountsController(MyfinancesContext context){
			_context = context;
		}

		// GET: api/BankAccounts
		[HttpGet]
		public async Task<ActionResult<IEnumerable<BankAccount>>> GetBankAccounts(){
			if (_context.BankAccounts == null){
				return NotFound();
			}

			return await _context.BankAccounts
				.Include(b => b.Bank)
				.Include(c => c.Currency)
				.ToListAsync();
		}

		// GET: api/BankAccounts/5
		[HttpGet("{id}")]
		public async Task<ActionResult<BankAccount>> GetBankAccount(long id){
			if (_context.BankAccounts == null){
				return NotFound();
			}

			var bankAccount = await _context.BankAccounts.FindAsync(id);

			if (bankAccount == null){
				return NotFound();
			}

			return bankAccount;
		}

		// PUT: api/BankAccounts/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutBankAccount(long id, BankAccount bankAccount){
			if (id != bankAccount.BankAccountId){
				return BadRequest();
			}

			_context.Entry(bankAccount).State = EntityState.Modified;

			try{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException){
				if (!BankAccountExists(id)){
					return NotFound();
				}
				else{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/BankAccounts
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<BankAccount>> PostBankAccount(BankAccount bankAccount){
			if (_context.BankAccounts == null){
				return Problem("Entity set 'MyfinancesContext.BankAccounts'  is null.");
			}

			_context.BankAccounts.Add(bankAccount);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetBankAccount), new{id = bankAccount.BankAccountId}, bankAccount);
		}

		// DELETE: api/BankAccounts/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBankAccount(long id){
			if (_context.BankAccounts == null){
				return NotFound();
			}

			var bankAccount = await _context.BankAccounts.FindAsync(id);

			if (bankAccount == null){
				return NotFound();
			}

			_context.BankAccounts.Remove(bankAccount);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool BankAccountExists(long id){
			return (_context.BankAccounts?.Any(e => e.BankAccountId == id)).GetValueOrDefault();
		}
	}
}