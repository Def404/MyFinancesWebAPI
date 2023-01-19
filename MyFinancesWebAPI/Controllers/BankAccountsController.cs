using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountsController : ControllerBase
    {
        private readonly MyFinancesContext _context;

        public BankAccountsController(MyFinancesContext context)
        {
            _context = context;
        }

        // GET: api/BankAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetBankAccounts([FromQuery]string login)
        {
            if (_context.BankAccounts == null)
                return NotFound();
            
            return await _context.BankAccounts
                .Include(b => b.Bank)
                .Include(b =>  b.Currency)
                .Where(b => !_context.DebitCards
                    .Select(d => d.BankAccountId)
                    .Contains(b.BankAccountId))
                .Where(b => b.Login == login)
                .ToListAsync();
        }
        
        // GET: api/BankAccounts/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetBankAccountsAll([FromQuery]string login)
        {
            if (_context.BankAccounts == null)
                return NotFound();
            
            return await _context.BankAccounts
                .Include(b => b.Bank)
                .Include(b =>  b.Currency)
                .Where(b => b.Login == login)
                .ToListAsync();
            
        }

        // GET: api/BankAccounts/5
        [HttpGet("id")]
        public async Task<ActionResult<BankAccount>> GetBankAccount([FromQuery]long id)
        {
            if (_context.BankAccounts == null)
                return NotFound();
            
            var bankAccount = await _context.BankAccounts
                .Include(b => b.Bank)
                .Include(b =>  b.Currency)
                .FirstOrDefaultAsync(b =>b.BankAccountId==id);

            if (bankAccount == null)
                return NotFound();

            return bankAccount;
        }
        
        // GET: api/BankAccounts/name{}
        [HttpGet("name")]
        public async Task<ActionResult<long>> GetBankAccountId([FromQuery]string bankAccountName)
        {
            if (_context.BankAccounts == null)
                return NotFound();
            

            var bankAccount = await _context.BankAccounts
                .Include(b => b.Bank)
                .Include(b =>  b.Currency)
                .FirstOrDefaultAsync(b => b.Name == bankAccountName);

            if (bankAccount == null)
                return NotFound();
            
            return bankAccount.BankAccountId;
        }

        // PUT: api/BankAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankAccount(long id, BankAccount bankAccount)
        {
            if (id != bankAccount.BankAccountId)
                return BadRequest();
            
            _context.Entry(bankAccount).State = EntityState.Modified;

            try{
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException){
                if (!BankAccountExists(id))
                    return NotFound();
                
                throw;
            }

            return NoContent();
        }

        // POST: api/BankAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BankAccount>> PostBankAccount(BankAccount bankAccount)
        {
            if (_context.BankAccounts == null)
                return Problem("Entity set 'MyFinancesContext.BankAccounts'  is null.");
            
            _context.BankAccounts.Add(bankAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBankAccount", new{ id = bankAccount.BankAccountId }, bankAccount);
        }
        
        
        private bool BankAccountExists(long id){
            return (_context.BankAccounts?.Any(e => e.BankAccountId == id)).GetValueOrDefault();
        }
    }
}