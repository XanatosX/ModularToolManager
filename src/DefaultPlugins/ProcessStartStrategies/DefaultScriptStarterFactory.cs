using ModularToolManagerPlugin.Enums;

namespace DefaultPlugins.ProcessStartStrategies;

/// <summary>
/// Factory to create the correct process start strategy for the current OS
/// </summary>
internal sealed class DefaultScriptStarterFactory
{
    /// <summary>
    /// Strategy cache
    /// </summary>
    private readonly List<IProcessStartStrategy> strategies;

    /// <summary>
    /// The action used for logging
    /// </summary>
    private readonly Action<LogSeverity, string>? loggingAction;

    /// <summary>
    /// Create a new instance of this class
    /// <paramref name="loggingAction">The action used for logging messages</paramref>>
    /// </summary>
    public DefaultScriptStarterFactory(Action<LogSeverity, string>? loggingAction)
    {
        strategies = new();
        this.loggingAction = loggingAction;
    }

    /// <summary>
    /// Create a new instance of this class
    /// Logging is disabled with this constructor
    /// </summary>
    public DefaultScriptStarterFactory() : this(null) { }

    /// <summary>
    /// Create the correct start info for the current operation system
    /// </summary>
    /// <param name="parameters">The parameters to use</param>
    /// <param name="path">The path of the file to execute</param>
    /// <returns>The process start info</returns>
    public IProcessStartStrategy? CreateStartInfo(string path)
    {
        IProcessStartStrategy? info = null;
        if (OperatingSystem.IsWindows())
        {
            info = GetStrategy<WindowsStarterStrategy>(() => new WindowsStarterStrategy());
        }
        if (OperatingSystem.IsLinux())
        {
            string? terminalApp = null;
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string? line;
                        while ((line = reader.ReadLine()) is not null)
                        {
                            if (line.StartsWith("#!"))
                            {
                                terminalApp = line.Replace("#!", string.Empty);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (loggingAction is not null)
                {
                    loggingAction(LogSeverity.Warning, e.Message);
                }
            }
            info = terminalApp switch
            {
                "/bin/bash" => GetStrategy<LinuxBashStarterStrategy>(() => new LinuxBashStarterStrategy()),
                _ => null
            };
        }

        return info;
    }

    /// <summary>
    /// Get the given strategy from cache or create a new one and cache it
    /// </summary>
    /// <param name="creationMethod">The method used to create the strategy</param>
    /// <typeparam name="T">The type of strategy to create</typeparam>
    /// <returns>A useable strategy or null</returns>
    public IProcessStartStrategy? GetStrategy<T>(Func<T> creationMethod) where T : IProcessStartStrategy
    {
        T? returnStrategy = strategies.OfType<T>().FirstOrDefault();
        if (returnStrategy is null)
        {
            returnStrategy = creationMethod();
            strategies.Add(returnStrategy);
        }

        return returnStrategy;
    }
}