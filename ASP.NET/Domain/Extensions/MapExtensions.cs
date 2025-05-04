using System.Reflection;

namespace Domain.Extensions;

public static class MapExtensions
{
  public static TDestination MapTo<TDestination>(this object source)
  {
    ArgumentNullException.ThrowIfNull(source, nameof(source));

    TDestination destination = Activator.CreateInstance<TDestination>()!;

    var sourceProperties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
    var destinationProperties = destination.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

    foreach (var destinationProperty in destinationProperties)
    {
      var sourceProperty = sourceProperties.FirstOrDefault(x => x.Name == destinationProperty.Name && destinationProperty.PropertyType.IsAssignableFrom(x.PropertyType));
      if (sourceProperty != null && destinationProperty.CanWrite)
      {
        var value = sourceProperty.GetValue(source);
        destinationProperty.SetValue(destination, value);
      }
    }

    return destination;
  }
}