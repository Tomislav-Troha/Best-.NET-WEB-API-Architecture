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
        private readonly ICustomerRepository _repo;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDto>> Get(int id)
        {
            // 1) Dohvati entitet iz Domene
            Customer? customerEntity = await _repo.GetByIdAsync(id);
            if (customerEntity is null)
                return NotFound();

            // 2) Pretvori ga u DTO s tvojom ekstenzijom
            CustomerDto dto = customerEntity.ToDto<CustomerDto>(_mapper);

            //Dohvati order iz Domene
            Order? order = new Order
            {
                Id = 1,
                Code = "Code",
                City = "Grad"
            };

            OrderDto orderDto = order.ToDto<OrderDto>(_mapper);

            dto.Order = orderDto;

            return Ok(dto);
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            var entities = await _repo.ListAsync();
            // Mapiranje liste
            return entities
                .Select(e => e.ToDto<CustomerDto>(_mapper))
                .ToList();
        }
    }
}
