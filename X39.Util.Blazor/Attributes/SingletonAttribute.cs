using System.Reflection;
using JetBrains.Annotations;

namespace X39.Util.Blazor.Attributes;

/// <summary>
/// Special attribute to mark a class as singleton.
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
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