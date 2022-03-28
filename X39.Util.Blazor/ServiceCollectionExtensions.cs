using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using X39.Util.Blazor.Attributes;

namespace X39.Util.Blazor;

[PublicAPI]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all classes with <see cref="SingletonAttribute"/> set of the <paramref name="assembly"/>
    /// to the <paramref name="services"/> as singleton.
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    ///     <item>
    ///         Given <see cref="SingletonAttribute.ConditionProperty"/> or
    ///         <see cref="SingletonAttribute.ConditionMethod"/> is set,
    ///         it will be evaluated.
    ///         If it resolves to true, it will be added.
    ///         Otherwise it won't.
    ///     </item>
    ///     <item>
    ///         This method will run static constructors on types having <see cref="SingletonAttribute"/> set.
    ///     </item>
    /// </list>
    /// </remarks>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <exception cref="Exception"></exception>
    public static void AddAttributedSingletonsOf(this IServiceCollection services, Assembly assembly)
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
                RuntimeHelpers.RunClassConstructor(type.TypeHandle);
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