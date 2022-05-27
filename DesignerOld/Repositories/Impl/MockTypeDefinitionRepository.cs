using X39.Systems.ServiceOrchestrator.Data.Entity;
using X39.Util.Blazor.Attributes;

namespace X39.Systems.ServiceOrchestrator.Designer.Repositories.Impl;
#pragma warning disable CS1998
[Singleton(ServiceType = typeof(ITypeDefinitionRepository), ConditionProperty = nameof(IsLoaded))]
public sealed class TypeDefinitionRepository : ITypeDefinitionRepository
{
#if DEBUG
    private static bool IsLoaded => true;
#else
    private static bool IsLoaded => false;
#endif

    // ReSharper disable once ArrangeObjectCreationWhenTypeEvident
    private readonly List<TypeDefinition> _data = new List<TypeDefinition>()
    {
        new() {Guid = Guid.NewGuid(), Name = "TypeDefinition 1"},
        new() {Guid = Guid.NewGuid(), Name = "TypeDefinition 2"},
        new() {Guid = Guid.NewGuid(), Name = "TypeDefinition 3"},
        new() {Guid = Guid.NewGuid(), Name = "TypeDefinition 4"},
    };

    public async IAsyncEnumerable<TypeDefinition> GetTypeDefinitionsAsync()
    {
        foreach (var typeDefinition in _data)
            yield return typeDefinition;
    }

    public Task<TypeDefinition> CreateTypeDefinitionAsync(CancellationToken cancellationToken, TypeDefinition typeDefinition)
    {
        typeDefinition = typeDefinition with {Guid = Guid.NewGuid()};
        _data.Add(typeDefinition);
        return Task.FromResult(typeDefinition);
    }
}
#pragma warning restore CS1998