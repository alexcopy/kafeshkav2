using KafeshkaV2.BL.validators.payment;
using KafeshkaV2.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KafeshkaV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PaymentDetailValidator _validator;

        public PaymentDetailController(AppDbContext context, PaymentDetailValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        // GET: api/PaymentDetailController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentDetails()
        {
            return await _context.PaymentDetail.ToListAsync();
        }

        // GET: api/PaymentDetailController/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetail.FindAsync(id);

            if (paymentDetail == null)
            {
                return NotFound();
            }

            return paymentDetail;
        }

        // PUT: api/PaymentDetailController/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDetail(int id, PaymentDetail paymentDetail)
        {
            // Validate the paymentDetail object using the validator
            var validationResult = await _validator.ValidateAsync(paymentDetail);

            // Check if validation fails
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            // Continue with your existing logic
            if (id != paymentDetail.PaymentDetailId)
            {
                return BadRequest();
            }

            _context.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(await _context.PaymentDetail.ToListAsync());
        }

        // POST: api/PaymentDetailController
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            // Validate the paymentDetail object using the validator
            var validationResult = await _validator.ValidateAsync(paymentDetail);

            // Check if validation fails
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            _context.PaymentDetail.Add(paymentDetail);
            await _context.SaveChangesAsync();
            // CreatedAtAction("GetPaymentDetail", new { id = paymentDetail.PaymentDetailId }, paymentDetail)
            return Ok(await _context.PaymentDetail.ToListAsync());
        }

        // DELETE: api/PaymentDetailController/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetail.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }

            _context.PaymentDetail.Remove(paymentDetail);
            await _context.SaveChangesAsync();

            return Ok(await _context.PaymentDetail.ToListAsync());
        }

        private bool PaymentDetailExists(int id)
        {
            return _context.PaymentDetail.Any(e => e.PaymentDetailId == id);
        }
    }
}