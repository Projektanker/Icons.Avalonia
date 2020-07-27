using Avalonia;
using Avalonia.Logging.Serilog;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;

namespace Demo
{
    internal class Program
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
            // Register icon provider(s)
            IconProvider.Register<FontAwesomeIconProvider>();

            return AppBuilder.Configure<App>()
                           .UsePlatformDetect()
                           .LogToDebug();
        }
    }
}