using AutoMapper;
using BestArchitecture.Application.DTO;
using BestArchitecture.Application.Extensions;
using BestArchitecture.Domain.Entities;
using BestArchitecture.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BestArchitecture.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _cutomerRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository customerRepo, IOrderRepository orderRepo, IMapper mapper)
        {
            _cutomerRepo = customerRepo;
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<CustomerDto>> Get(int customerId)
        {
            // 1) Dohvati entitet iz Domene
            Customer? customerEntity = await _cutomerRepo.GetByIdAsync(customerId);
            if (customerEntity is null)
                return NotFound();

            // 2) Pretvori ga u DTO s tvojom ekstenzijom
            CustomerDto dto = customerEntity.ToDto<CustomerDto>(_mapper);

            //Dohvati order iz Domene
            var orders = await _orderRepo.GetAllOrdersByCustomer(customerId);

            if (orders is not null)
                dto.Orders = orders.Select(o => o.ToDto<OrderDto>(_mapper)).ToList();
            
            return Ok(dto);
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            var entities = await _cutomerRepo.ListAsync();
            // Mapiranje liste
            return entities
                .Select(e => e.ToDto<CustomerDto>(_mapper))
                .ToList();
        }
    }
}
