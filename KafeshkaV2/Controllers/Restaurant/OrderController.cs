using KafeshkaV2.DAL.Model;
using KafeshkaV2.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KafeshkaV2.Controllers.Restaurant
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public OrderController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public System.Object GetOrders()
        {
            var result = (from a in _context.Orders
                join b in _context.Customers on a.CustomerId equals b.CustomerId
                select new
                {
                    a.OrderId,
                    a.OrderNo,
                    Customer = b.Name,
                    a.PMethod,
                    a.GTotal
                }).ToList();
            return result;
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(long id)
        {
            var order = (from a in _context.Orders
                where a.OrderId == id
                select new
                {
                    a.OrderId,
                    a.OrderNo,
                    a.PMethod,
                    a.CustomerId,
                    a.GTotal
                }).FirstOrDefault();
            var orderItems = (from a in _context.OrderItems
                join b in _context.Items on a.ItemId equals b.ItemId
                where a.OrderId == id
                select new
                {
                    a.OrderId,
                    a.OrderItemId,
                    a.ItemId,
                    ItemName = b.Name,
                    b.Price,
                    a.Quantity,
                    Total = a.Quantity * b.Price
                }).ToList();
            return Ok(new {order, orderItems});
        }

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                // Return a 201 Created status code along with the created order
                return Ok();
            }
            catch (Exception e)
            {
                // Log the exception
                Console.WriteLine(e);
                // Return a 500 Internal Server Error status code
                return StatusCode(500, "An error occurred while processing your request." + e.InnerException);
            }
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(long id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}