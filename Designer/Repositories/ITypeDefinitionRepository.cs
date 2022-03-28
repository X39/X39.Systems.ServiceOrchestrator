using X39.Systems.ServiceOrchestrator.Data.Entity;

namespace X39.Systems.ServiceOrchestrator.Designer.Repositories;

public interface ITypeDefinitionRepository
{
    IAsyncEnumerable<TypeDefinition> GetTypeDefinitionsAsync();
    Task<TypeDefinition> CreateTypeDefinitionAsync(CancellationToken cancellationToken, TypeDefinition typeDefinition);
}