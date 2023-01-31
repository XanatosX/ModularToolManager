using Microsoft.Extensions.Logging;
using ModularToolManager.Models;
using ModularToolManager.Services.Dependencies;
using ModularToolManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModularToolManager.Services.IO;


internal class GetApplicationInformationService
{
    private readonly ResourceReaderService readerService;
    private readonly ILogger<GetApplicationInformationService> logger;
    private const string LICENSE_FILE_NAME = "LICENSE";

    private string? license;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="readerService">The reader service to use</param>
    public GetApplicationInformationService(ResourceReaderService readerService, ILogger<GetApplicationInformationService> logger)
    {
        this.readerService = readerService;
        this.logger = logger;
    }

    /// <summary>
    /// Get the license for the project
    /// </summary>
    /// <returns>The license information</returns>
    public string? GetLicense()
    {
        license ??= readerService.GetResourceData(LICENSE_FILE_NAME);
        return license;
    }

    /// <summary>
    /// Get the version of the application
    /// </summary>
    /// <returns>The version of the project or null</returns>
    public Version? GetVersion()
    {
        var assembly = Assembly.GetExecutingAssembly();
        Version? returnVersion = null;
        try
        {

            var versionString = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
            returnVersion = new Version(versionString ?? "0.0.0");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not get version from assembly");
        }

        return returnVersion;
    }

    /// <summary>
    /// Get the dependencies for this application
    /// </summary>
    /// <returns>A list with all the dependencies</returns>
    public IEnumerable<DependencyModel> GetDependencies()
    {
        IEnumerable<DependencyModel> returnDependencies = Enumerable.Empty<DependencyModel>();
        using (Stream? stream = readerService.GetResourceStream("dependencies.json"))
        {
            if (stream is not null && stream != Stream.Null)
            {
                returnDependencies = JsonSerializer.Deserialize<IEnumerable<DependencyModel>>(stream) ?? returnDependencies;
            }
        }

        return returnDependencies;
    }
}
