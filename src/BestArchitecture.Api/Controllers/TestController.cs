using BestArchitecture.Api.Controllers.Base;
using BestArchitecture.Application.Mappings;
using BestArchitecture.Shared.DTO.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace BestArchitecture.Api.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly ITestRepository _testRepository;

        public CustomersController(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        [SwaggerOperation(
          Summary = "Get customer by ID",
          Description = "Returns a list of countries")]
        [ProducesResponseType(typeof(List<CustomerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponseDto), StatusCodes.Status400BadRequest)]
        [HttpGet("GetCustomerById")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById([SwaggerParameter(Required = true)]int id)
        {
            var result = await _testRepository.GetById(id);
            if (result == null)
            {
                return BadRequest(new BaseResponseDto
                {
                    Errors = { "Error while fetching customer" },
                    Success = false
                });
            }

            return Ok(result.ToDto());
        }

    }
}
