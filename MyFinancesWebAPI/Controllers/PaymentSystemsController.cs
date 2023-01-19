using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentSystemsController : ControllerBase
    {
        private readonly MyFinancesContext _context;

        public PaymentSystemsController(MyFinancesContext context)
        {
            _context = context;
        }

        // GET: api/PaymentSystems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentSystem>>> GetPaymentSystems()
        {
            if (_context.PaymentSystems == null)
            {
                return NotFound();
            }

            return await _context.PaymentSystems.ToListAsync();
        }

        // GET: api/PaymentSystems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentSystem>> GetPaymentSystem(int id)
        {
            if (_context.PaymentSystems == null)
            {
                return NotFound();
            }

            var paymentSystem = await _context.PaymentSystems.FindAsync(id);

            if (paymentSystem == null)
            {
                return NotFound();
            }

            return paymentSystem;
        }

        // PUT: api/PaymentSystems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentSystem(int id, PaymentSystem paymentSystem)
        {
            if (id != paymentSystem.PaymentSystemId)
            {
                return BadRequest();
            }

            _context.Entry(paymentSystem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentSystemExists(id))
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

        // POST: api/PaymentSystems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaymentSystem>> PostPaymentSystem(PaymentSystem paymentSystem)
        {
            if (_context.PaymentSystems == null)
            {
                return Problem("Entity set 'MyFinancesContext.PaymentSystems'  is null.");
            }

            _context.PaymentSystems.Add(paymentSystem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentSystem", new { id = paymentSystem.PaymentSystemId }, paymentSystem);
        }

        // DELETE: api/PaymentSystems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentSystem(int id)
        {
            if (_context.PaymentSystems == null)
            {
                return NotFound();
            }

            var paymentSystem = await _context.PaymentSystems.FindAsync(id);
            if (paymentSystem == null)
            {
                return NotFound();
            }

            _context.PaymentSystems.Remove(paymentSystem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentSystemExists(int id)
        {
            return (_context.PaymentSystems?.Any(e => e.PaymentSystemId == id)).GetValueOrDefault();
        }
    }
}
