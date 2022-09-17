﻿using Microsoft.Extensions.Logging;
using ModularToolManager.Models;
using ModularToolManager.Services.Serialization;
using ModularToolManagerModel.Services.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Settings;

/// <summary>
/// Service to serialize the application settings
/// </summary>
internal class SerializedSettingsService : ISettingsService
{
    /// <summary>
    /// The serializer to use
    /// </summary>
    private readonly ISerializeService serializer;

    /// <summary>
    /// The path service to use
    /// </summary>
    private readonly IPathService pathService;

    /// <summary>
    /// The file system service to use
    /// </summary>
    private readonly IFileSystemService fileSystemService;

    /// <summary>
    /// The cached application settings to retrieve if nothing changed
    /// </summary>
    private ApplicationSettings? cachedApplicationSettings;

    /// <summary>
    /// Create a new instance of this service class
    /// </summary>
    /// <param name="serializer">The serializer to use</param>
    /// <param name="pathService">The path service to use</param>
    /// <param name="fileSystemService">The file system service to use</param>
    public SerializedSettingsService(ISerializeService serializer,
                                     IPathService pathService,
                                     IFileSystemService fileSystemService)
    {
        this.serializer = serializer;
        this.pathService = pathService;
        this.fileSystemService = fileSystemService;
    }

    /// <inheritdoc/>
    public ApplicationSettings GetApplicationSettings()
    {
        if (cachedApplicationSettings is not null)
        {
            return cachedApplicationSettings;
        }
        ApplicationSettings returnData = new ApplicationSettings();
        var settingsFile = pathService.GetSettingsFilePathString();

        using (StreamReader? reader = fileSystemService.GetReadStream(settingsFile))
        {
            if (reader is not null)
            {
                returnData = serializer.GetDeserialized<ApplicationSettings>(reader.BaseStream) ?? returnData;
            }

        }
        cachedApplicationSettings = returnData;
        return returnData;
    }

    /// <inheritdoc/>
    public bool SaveApplicationSettings(ApplicationSettings newSettings)
    {
        cachedApplicationSettings = null;

        var settingsFile = pathService.GetSettingsFilePathString();
        bool success = false;
        using (StreamWriter? writer = fileSystemService.GetWriteStream(settingsFile))
        {
            if (writer is not null)
            {
                serializer.GetSerializedStream(newSettings).CopyTo(writer.BaseStream);
                success = true;
            }
        }
        return success;
    }
}
