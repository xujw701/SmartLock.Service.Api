using SmartELock.Core.Domain.Services;
using SmartELock.Service.Api.Dto.Requests;
using SmartELock.Service.Api.Dto.Responses;
using SmartELock.Service.Api.Mappers;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartELock.Service.Api.Controllers
{
    [RoutePrefix("api/companies")]
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;

        private readonly ICompanyMapper _companyMapper;

        public CompanyController(ICompanyService companyService, ICompanyMapper companyMapper)
        {
            _companyService = companyService;

            _companyMapper = companyMapper;
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateCompany(CompanyPostDto companyPostDto)
        {
            var command = _companyMapper.MapToCreateCommand(companyPostDto);

            var id = await _companyService.CreateCompany(command);

            var response = new DefaultCreatedPostResponseDto
            {
                Id = id
            };

            return Created($"{id}", response);
        }
    }
}