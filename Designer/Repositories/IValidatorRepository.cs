using X39.Systems.ServiceOrchestrator.Data.Entity;

namespace X39.Systems.ServiceOrchestrator.Designer.Repositories;

public interface IValidatorRepository
{
    IAsyncEnumerable<Validator> GetValidatorsAsync();
    Task<Validator> CreateValidatorAsync(CancellationToken cancellationToken, Validator validator);
}