using System.Reflection;
using JetBrains.Annotations;

namespace X39.Util.Blazor.Attributes;

/// <summary>
/// Special attribute to mark a class as singleton.
/// </summary>
/// <remarks>
/// This attribute cannot be inherited.
/// </remarks>
/// <example>
/// <code>
///     public interface IMyConditionalService
///     {
///         bool SomeFunc();
///     }
///     [Singleton(ServiceType = typeof(IMyConditionalService), ConditionProperty = nameof(IsLoaded))]
///     public class MyConditionalService : IMyConditionalService
///     {
///     #if DEBUG
///         private static bool IsLoaded => true;
///     #else
///         private static bool IsLoaded => false;
///     #endif
///         public bool SomeFunc(){
///             return true;
///         }
///     }
///     [Singleton(ServiceType = typeof(IMyConditionalService), ConditionProperty = nameof(IsLoaded))]
///     public class MyConditionalServiceMock : IMyConditionalService
///     {
///     #if DEBUG
///         private static bool IsLoaded => false;
///     #else
///         private static bool IsLoaded => true;
///     #endif
///         public bool SomeFunc(){
///             return true;
///         }
///     }
/// </code>
/// </example>
[PublicAPI]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class SingletonAttribute : Attribute
{
    /// <summary>
    /// An optional service type that is implemented with the Singleton.
    /// </summary>
    public Type? ServiceType { get; set; }
    
    /// <summary>
    /// An optional method serving as condition for whether the type should be appended or not.
    /// Must live in the implementing type.
    /// </summary>
    /// <example>
    /// <code>
    ///     public static bool SingletonCondition();
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    ///     private static bool SingletonCondition();
    /// </code>
    /// </example>
    public string? ConditionMethod { get; set; }
    
    /// <summary>
    /// An optional property serving as condition for whether the type should be appended or not.
    /// Must live in the implementing type.
    /// </summary>
    /// <example>
    /// <code>
    ///     public static bool SingletonCondition { get; }
    /// </code>
    /// </example>
    /// <example>
    /// <code>
    ///     private static bool SingletonCondition { get; }
    /// </code>
    /// </example>
    public string? ConditionProperty { get; set; }
}