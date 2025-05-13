using BestArchitecture.Application.Mappings;

namespace BestArchitecture.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _cutomerRepo;
        private readonly IOrderRepository _orderRepo;

        public CustomersController(ICustomerRepository customerRepo, IOrderRepository orderRepo)
        {
            _cutomerRepo = customerRepo;
            _orderRepo = orderRepo;
        }

        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<CustomerDto>> Get(int customerId)
        {
            // 1) Dohvati entitet iz Domene
            Customer? customerEntity = await _cutomerRepo.GetByIdAsync(customerId);
            if (customerEntity is null)
                return NotFound();

            //Dohvati order iz Domene
            var orders = await _orderRepo.GetAllOrdersByCustomer(customerId);

            CustomerDto dto = customerEntity.ToDto(orders);

            return Ok(dto);
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            var entities = await _cutomerRepo.ListAsync();
            // Mapiranje liste
            return entities
                .Select(e => e.ToDto())
                .ToList();
        }
    }
}
