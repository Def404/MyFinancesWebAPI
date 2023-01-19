using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : ControllerBase
    {
        private readonly MyFinancesContext _context;

        public CreditCardsController(MyFinancesContext context)
        {
            _context = context;
        }

        // GET: api/CreditCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditCard>>> GetCreditCards([FromQuery]string login)
        {
            if (_context.CreditCards == null)
                return NotFound();
            
            return await _context.CreditCards
                .Include(c => c.Bank)
                .Include(c => c.Currency)
                .Include(c => c.PaymentSystem)
                .Where(c => c.Login == login)
                .ToListAsync();
        }

        // GET: api/CreditCards/5
        [HttpGet("id")]
        public async Task<ActionResult<CreditCard>> GetCreditCard([FromQuery]long id)
        {
            if (_context.CreditCards == null)
                return NotFound();
            

            var creditCard = await _context.CreditCards
                .Include(c => c.Bank)
                .Include(c => c.Currency)
                .Include(c => c.PaymentSystem)
                .FirstOrDefaultAsync(c => c.CreditCardId == id);

            if (creditCard == null)
                return NotFound();
            
            return creditCard;
        }
        
        // GET: api/CreditCards/name
        [HttpGet("name")]
        public async Task<ActionResult<CreditCard>> GetCreditCardId([FromQuery]string name)
        {
            if (_context.CreditCards == null)
                return NotFound();
            

            var creditCard = await _context.CreditCards
                .Include(c => c.Bank)
                .Include(c => c.Currency)
                .Include(c => c.PaymentSystem)
                .FirstOrDefaultAsync(c => c.Name == name);

            if (creditCard == null)
                return NotFound();

            return creditCard;
        }

        // PUT: api/CreditCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditCard(long id, CreditCard creditCard)
        {
            if (id != creditCard.CreditCardId)
            {
                return BadRequest();
            }

            _context.Entry(creditCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditCardExists(id))
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

        // POST: api/CreditCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreditCard>> PostCreditCard(CreditCard creditCard)
        {
            if (_context.CreditCards == null)
            {
                return Problem("Entity set 'MyFinancesContext.CreditCards'  is null.");
            }

            _context.CreditCards.Add(creditCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCreditCard", new { id = creditCard.CreditCardId }, creditCard);
        }
        
        private bool CreditCardExists(long id)
        {
            return (_context.CreditCards?.Any(e => e.CreditCardId == id)).GetValueOrDefault();
        }
    }
}
