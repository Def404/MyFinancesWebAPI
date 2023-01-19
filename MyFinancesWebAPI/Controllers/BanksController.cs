using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinancesWebAPI.Contexts;
using MyFinancesWebAPI.Models;

namespace MyFinancesWebAPI.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase{
        private readonly MyFinancesContext _context;

        public BanksController(MyFinancesContext context){
            _context = context;
        }

        // GET: api/Banks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBanks(){
            if (_context.Banks == null){
                return NotFound();
            }

            return await _context.Banks.ToListAsync();
        }

        // GET: api/Banks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bank>> GetBank(int id){
            if (_context.Banks == null){
                return NotFound();
            }

            var bank = await _context.Banks.FindAsync(id);

            if (bank == null){
                return NotFound();
            }

            return bank;
        }

        // PUT: api/Banks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBank(int id, Bank bank){
            if (id != bank.BankId){
                return BadRequest();
            }

            _context.Entry(bank).State = EntityState.Modified;

            try{
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException){
                if (!BankExists(id)){
                    return NotFound();
                }
                else{
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Banks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bank>> PostBank(Bank bank){
            if (_context.Banks == null){
                return Problem("Entity set 'MyFinancesContext.Banks'  is null.");
            }

            _context.Banks.Add(bank);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBank", new{ id = bank.BankId }, bank);
        }

        // DELETE: api/Banks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBank(int id){
            if (_context.Banks == null){
                return NotFound();
            }

            var bank = await _context.Banks.FindAsync(id);
            if (bank == null){
                return NotFound();
            }

            _context.Banks.Remove(bank);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BankExists(int id){
            return (_context.Banks?.Any(e => e.BankId == id)).GetValueOrDefault();
        }
    }
}