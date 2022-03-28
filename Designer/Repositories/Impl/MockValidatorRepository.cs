using X39.Systems.ServiceOrchestrator.Data.Entity;
using X39.Util.Blazor.Attributes;

namespace X39.Systems.ServiceOrchestrator.Designer.Repositories.Impl;
#pragma warning disable CS1998
[Singleton(ServiceType = typeof(IValidatorRepository), ConditionProperty = nameof(IsLoaded))]
public sealed class ValidatorRepository : IValidatorRepository
{
#if DEBUG
    private static bool IsLoaded => true;
#else
    private static bool IsLoaded => false;
#endif

    // ReSharper disable once ArrangeObjectCreationWhenTypeEvident
    private readonly List<Validator> _data = new List<Validator>()
    {
        new() {Id = Guid.NewGuid(), Name = "Validator 1"},
        new() {Id = Guid.NewGuid(), Name = "Validator 2"},
        new() {Id = Guid.NewGuid(), Name = "Validator 3"},
        new() {Id = Guid.NewGuid(), Name = "Validator 4"},
    };

    public async IAsyncEnumerable<Validator> GetValidatorsAsync()
    {
        foreach (var validator in _data)
            yield return validator;
    }

    public Task<Validator> CreateValidatorAsync(CancellationToken cancellationToken, Validator validator)
    {
        validator = validator with {Id = Guid.NewGuid()};
        _data.Add(validator);
        return Task.FromResult(validator);
    }
}
#pragma warning restore CS1998