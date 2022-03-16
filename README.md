# Icons.Avalonia
A library to easily display icons in an Avalonia App.

[![CI-CD](https://github.com/Projektanker/Icons.Avalonia/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/Projektanker/Icons.Avalonia/actions/workflows/ci-cd.yml)

## NuGet
| Name | Description | Version |
|:-|:-|:-|
| [Projektanker.Icons.Avalonia](https://www.nuget.org/packages/Projektanker.Icons.Avalonia/) | Core library | ![Nuget](https://badgen.net/nuget/v/Projektanker.Icons.Avalonia) |
| [Projektanker.Icons.Avalonia.FontAwesome](https://www.nuget.org/packages/Projektanker.Icons.Avalonia.FontAwesome/) | [Font Awesome Free](https://fontawesome.com) | ![Nuget](https://badgen.net/nuget/v/Projektanker.Icons.Avalonia.FontAwesome) |
| [Projektanker.Icons.Avalonia.MaterialDesign](https://www.nuget.org/packages/Projektanker.Icons.Avalonia.MaterialDesign/) | [Material Design Icons](https://materialdesignicons.com/) | ![Nuget](https://badgen.net/nuget/v/Projektanker.Icons.Avalonia.MaterialDesign) |

## Icon providers
| Name | Prefix | Example|
|:-|:-:|:-|
|FontAwesome| `fa` | `fa-github`
|MaterialDesign| `mdi` | `mdi-github`
## Usage
A full example is available in the [demo](demo) directory.

### 1. Register icon providers on app start up
Register the icon provider(s) with the `AppBuilder`.
```csharp
class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    public static void Main(string[] args)
    {
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .WithIcons(container => container
                .Register<FontAwesomeIconProvider>()
                .Register<MaterialDesignIconProvider>());
    }
}
```

### 2. Add xml namespace

Add `xmlns:i="clr-namespace:Projektanker.Icons.Avalonia;assembly=Projektanker.Icons.Avalonia"` to your view.

### 3. Use the icon

**Standalone**
```xml
<i:Icon Value="fab fa-github" />
```

**Attached to ContentControl (e.g. Button)**
```xml
<Button i:Attached.Icon="fab fa-github" />
```

**Attached to MenuItem**
```xml
<MenuItem Header="About" i:MenuItem.Icon="fas fa-info-circle" />
```

### Done

![Screenshot](https://github.com/Projektanker/Icons.Avalonia/raw/main/resources/demo.png)

## Implement your own Icon Provider
Just implement the `IIconProvider` interface:
```csharp
namespace Projektanker.Icons.Avalonia
{
    /// <summary>
    /// Represents an icon reader.
    /// </summary>
    public interface IIconReader
    {
        /// <summary>
        /// Gets the SVG path of the requested icon.
        /// </summary>
        /// <param name="value">The value specifying the icon to return it's path from.</param>
        /// <returns>The path of the icon.</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">
        /// The icon associated with the specified <paramref name="value"/> does not exists.
        /// </exception>
        string GetIconPath(string value);
    }

    /// <summary>
    /// Represents an icon provider.
    /// </summary>
    public interface IIconProvider : IIconReader
    {
        /// <summary>
        /// Gets the prefix of the <see cref="IIconProvider"/>.
        /// </summary>
        string Prefix { get; }
    }
}
```
and register it with the `IIconProviderContainer`:
```csharp
container.Register<MyCustomIconProvider>()
```
or
```csharp
IIconProvider provider = new MyCustomIconProvider(/* custom ctor arguments */);
container.Register(provider);
```

The `IIconProvider.Prefix` property has to be unique within all registered providers. It is used to select the right provider. E.g. `FontAwesomeIconProvider`'s prefix is `fa`.
