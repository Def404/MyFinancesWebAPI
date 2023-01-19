using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebitCardsController : ControllerBase
    {
        private readonly MyFinancesContext _context;

        public DebitCardsController(MyFinancesContext context)
        {
            _context = context;
        }

        // GET: api/DebitCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DebitCard>>> GetDebitCards([FromQuery] string login)
        {
            if (_context.DebitCards == null)
                return NotFound();
            
            return await _context.DebitCards
                .Include(d =>  d.BankAccount)
                .Include(d => d.PaymentSystem)
                .Where(d => d.Login == login)
                .ToListAsync();
        }

        // GET: api/DebitCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DebitCard>> GetDebitCard(int id)
        {
            if (_context.DebitCards == null)
                return NotFound();
            

            var debitCard = await _context.DebitCards
                .Include(d => d.PaymentSystem)
                .Include(d =>  d.BankAccount)
                .FirstOrDefaultAsync(d => d.DebitCardId == id);

            if (debitCard == null)
                return NotFound();

            return debitCard;
        }

        // PUT: api/DebitCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDebitCard(int id, DebitCard debitCard)
        {
            if (id != debitCard.DebitCardId)
            {
                return BadRequest();
            }

            _context.Entry(debitCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DebitCardExists(id))
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

        // POST: api/DebitCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DebitCard>> PostDebitCard(DebitCard debitCard)
        {
            if (_context.DebitCards == null)
            {
                return Problem("Entity set 'MyFinancesContext.DebitCards'  is null.");
            }

            _context.DebitCards.Add(debitCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDebitCard", new { id = debitCard.DebitCardId }, debitCard);
        }
        
        private bool DebitCardExists(int id)
        {
            return (_context.DebitCards?.Any(e => e.DebitCardId == id)).GetValueOrDefault();
        }
    }
}
