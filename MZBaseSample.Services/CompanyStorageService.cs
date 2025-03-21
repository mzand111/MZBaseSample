using Microsoft.Extensions.Logging;
using MZBase.EntityFrameworkCore;
using MZBase.Infrastructure;
using MZBase.Infrastructure.Service.Exceptions;
using MZBaseSample.Data.DataContext;
using MZBaseSample.Data.Entity;
using MZBaseSample.Domain;
using MZBaseSample.Infrastructure;

namespace MZBaseSample.Services
{
    public class CompanyStorageService : EFCoreStorageBusinessService<Company, CompanyEntity, int, SampleDbUnitOfWork, SampleDBContext>
    {
        public CompanyStorageService(SampleDbUnitOfWork unitOfWork,
            IDateTimeProviderService dateTimeProvider,
            ILogger<Company> logger)
            : base(unitOfWork, dateTimeProvider, logger)
        {
        }

        protected override async Task ValidateOnAddAsync(Company item)
        {
            List<ModelFieldValidationResult> _validationErrors = new List<ModelFieldValidationResult>();
            await DoCommonValidationsAsync(_validationErrors, item);
            if (_validationErrors.Any())
            {
                var exp = new ServiceModelValidationException(_validationErrors, "Error validating the model");
                LogAdd(item, "Error in Adding item when validating:" + exp.JSONFormattedErrors, exp);
                throw exp;
            }
        }

        protected override async Task ValidateOnModifyAsync(Company receivedItem, Company storageItem)
        {
            List<ModelFieldValidationResult> _validationErrors = new List<ModelFieldValidationResult>();
            await DoCommonValidationsAsync(_validationErrors, receivedItem);
            if (_validationErrors.Any())
            {
                var exp = new ServiceModelValidationException(_validationErrors, "Error validating the model");
                LogModify(receivedItem, "Error in Modifying item when validating:" + exp.JSONFormattedErrors, exp);
                throw exp;
            }
        }
        private async Task DoCommonValidationsAsync(List<ModelFieldValidationResult> validationErrors, Company item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                validationErrors.Add(new ModelFieldValidationResult()
                {
                    Code = _logBaseID + 1,
                    FieldName = nameof(item.Name),
                    ValidationMessage = "The Field Can Not Be Empty"
                });
            }
            if (item.RegistrationNumber < 0)
            {
                validationErrors.Add(new ModelFieldValidationResult()
                {
                    Code = _logBaseID + 2,
                    FieldName = nameof(item.RegistrationNumber),
                    ValidationMessage = "Invalid value"
                });
            }
            if (item.RegistrationDate > _dateTimeProvider.GetNow())
            {
                validationErrors.Add(new ModelFieldValidationResult()
                {
                    Code = _logBaseID + 3,
                    FieldName = nameof(item.RegistrationDate),
                    ValidationMessage = "Can not be in the future"
                });
            }


        }
    }
}
