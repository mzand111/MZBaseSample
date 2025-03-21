using Microsoft.AspNetCore.Mvc;
using MZBase.Infrastructure;
using MZBase.Infrastructure.Service.Exceptions;
using MZBaseSample.Api.Dto;
using MZBaseSample.Domain;
using MZBaseSample.Services;
using MZSimpleDynamicLinq.Core;

namespace MZBaseSample.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyStorageService _service;
        private readonly IDateTimeProviderService _dateTimeProvider;
        const string CurrentUser = "Mahdi"; //In a real world scenario, this should be set to the user based on the identity subsystem of the API
        public CompanyController(CompanyStorageService storageService, IDateTimeProviderService dateTimeProvider)
        {
            _service = storageService;
            _dateTimeProvider = dateTimeProvider;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Post(AddCompanyDto company)
        {
            try
            {
                Company domainCompany = company.GetDomainObject();
                domainCompany.CreatedBy = CurrentUser;
                domainCompany.LastModifiedBy = CurrentUser;
                domainCompany.CreationTime = _dateTimeProvider.GetNow();
                domainCompany.LastModificationTime = domainCompany.CreationTime;
                var g = await _service.AddAsync(domainCompany);
                return Ok(g);
            }
            catch (ServiceException ex)
            {
                if (ex is ServiceModelValidationException)
                {
                    return StatusCode(500, ex.Message + ", " + (ex as ServiceModelValidationException).JSONFormattedErrors);
                }
                return StatusCode(500, ex.ToServiceExceptionString());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> Put(EditCompanyDto company)
        {
            try
            {
                Company domainCompany = company.GetDomainObject();
                var item = await _service.RetrieveByIdAsync(company.ID);
                if (item == null)
                    return NotFound("Item not found");
                //Check API calling permissions
                if (item.CreatedBy != CurrentUser)
                {
                    return Unauthorized();
                }

                domainCompany.LastModificationTime = _dateTimeProvider.GetNow();
                domainCompany.LastModifiedBy = CurrentUser;
                await _service.ModifyAsync(domainCompany);
                return Ok();
            }
            catch (ServiceException ex)
            {
                if (ex is ServiceModelValidationException)
                {
                    return StatusCode(500, ex.Message + ", " + (ex as ServiceModelValidationException).JSONFormattedErrors);
                }
                return StatusCode(500, ex.ToServiceExceptionString());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = await _service.RetrieveByIdAsync(id);
                if (item == null)
                    return NotFound("Item not found");

                if (item.CreatedBy != CurrentUser)
                {
                    return Unauthorized();
                }
                await _service.RemoveByIdAsync(id);
                return Ok();
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.ToServiceExceptionString());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LinqDataResult<Company>>> GetAll(int pageSize, int pageNumber)
        {
            LinqDataRequest request = new LinqDataRequest
            {
                Skip = pageNumber,
                Take = pageSize,
            };
            var sort = new List<Sort>();
            sort.Add(new Sort()
            {
                Dir = "Desc",
                Field = "RegistrationDate"
            });
            request.Sort = sort;
            try
            {
                var g = await _service.ItemsAsync(request);
                return Ok(g);
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, ex.ToServiceExceptionString());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
