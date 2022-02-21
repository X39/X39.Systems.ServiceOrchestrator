using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using X39.Util.Blazor.Attributes;

namespace X39.Util.Blazor;

[PublicAPI]
public static class ServiceCollectionExtensions
{
    public static void AddMarkedSingletonsFrom(this IServiceCollection services, Assembly assembly)
    {
        static IEnumerable<(Type ServiceType, Type ImplementationType)> GetSingletonTypeTuples(Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<SingletonAttribute>();
                if (attribute is null)
                    continue;
                if (!type.IsSealed)
                    throw new Exception($"The type {type.FullName()} is not sealed.");
                if (attribute.ConditionMethod is not null)
                {
                    var conditionMethods = type
                        .GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                        .Where((methodInfo) => methodInfo.GetParameters().Length == 0)
                        .Where((methodInfo) => methodInfo.ReturnType.IsEquivalentTo(typeof(bool)))
                        .ToArray();
                    if (conditionMethods.Length != 1)
                        throw new Exception(
                            $"The type {type.FullName()} is marked to have a condition " +
                            "method but does not supply one matching the expected signature.");
                    var conditionMethod = conditionMethods.Single();
                    var result = conditionMethod.Invoke(null, Array.Empty<object>());
                    if (result is not bool conditionResult)
                        throw new Exception(
                            $"The type {type.FullName()} is marked to have a condition " +
                            "method but the result is not a valid boolean.");
                    if (!conditionResult)
                        continue;
                }

                if (attribute.ConditionProperty is not null)
                {
                    var conditionProperties = type
                        .GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                        .Where((propertyInfo) => propertyInfo.PropertyType.IsEquivalentTo(typeof(bool)))
                        .Where((propertyInfo) => propertyInfo.GetMethod is not null)
                        .Where((propertyInfo) => propertyInfo.SetMethod is null)
                        .ToArray();
                    if (conditionProperties.Length != 1)
                        throw new Exception(
                            $"The type {type.FullName()} is marked to have a condition " +
                            "property but does not supply one matching the expected signature.");
                    var conditionProperty = conditionProperties.Single();
                    var result = conditionProperty.GetMethod!.Invoke(null, Array.Empty<object>());
                    if (result is not bool conditionResult)
                        throw new Exception(
                            $"The type {type.FullName()} is marked to have a condition " +
                            "property but the result is not a valid boolean.");
                    if (!conditionResult)
                        continue;
                }

                if (attribute.ServiceType is not null)
                {
                    if (!attribute.ServiceType.IsAssignableFrom(type))
                        throw new Exception(
                            $"The type {type.FullName()} is providing the {nameof(SingletonAttribute.ServiceType)} " +
                            $"{attribute.ServiceType.FullName()} but is not implementing that.");
                }

                yield return (attribute.ServiceType ?? type, type);
            }
        }

        foreach (var (serviceType, implementingType) in GetSingletonTypeTuples(assembly))
        {
            services.AddSingleton(serviceType, implementingType);
        }
    }
}