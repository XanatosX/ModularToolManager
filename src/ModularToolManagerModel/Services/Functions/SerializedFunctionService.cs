using Microsoft.Extensions.Logging;
using ModularToolManager.Models;
using ModularToolManager.Services.Serialization;
using ModularToolManagerModel.Services.IO;

namespace ModularToolManagerModel.Services.Functions;

/// <summary>
/// Service to save the function to the disc
/// </summary>
public class SerializedFunctionService : IFunctionService
{
    /// <summary>
    /// Service used for seralization
    /// </summary>
    private readonly ISerializeService? serializer;

    /// <summary>
    /// Service used to get the file paths for the application
    /// </summary>
    private readonly IPathService? pathService;

    /// <summary>
    /// Service to use for file system interactions
    /// </summary>
    public IFileSystemService FileSystemService { get; }

    /// <summary>
    /// The logging service to use
    /// </summary>
    private readonly ILogger<SerializedFunctionService>? loggingService;

    /// <summary>
    /// A list with all the cached functions
    /// </summary>
    private readonly List<FunctionModel> cachedFunctions;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="serializer">The serailzier class to use</param>
    /// <param name="pathService">The path service to use</param>
    public SerializedFunctionService(ISerializeService? serializer,
                                     IPathService? pathService,
                                     IFileSystemService fileSystemService,
                                     ILogger<SerializedFunctionService>? loggingService)
    {
        this.serializer = serializer;
        this.pathService = pathService;
        FileSystemService = fileSystemService;
        this.loggingService = loggingService;

        cachedFunctions = new List<FunctionModel>();
    }

    /// <inheritdoc/>
    public bool AddFunction(FunctionModel function)
    {
        loggingService?.LogTrace($"Add new function with name {function.DisplayName} and unique id {function.UniqueIdentifier}");
        var currentData = GetAvailableFunctions();
        if (currentData.Any(model => model.UniqueIdentifier == function.UniqueIdentifier) || !IsFunctionValid(function))
        {
            loggingService?.LogTrace($"Adding failed for function {function.DisplayName} with unique id {function.UniqueIdentifier}");
            return false;
        }
        currentData.Add(function);
        loggingService?.LogTrace($"Adding succeeded for function {function.DisplayName} with unique id {function.UniqueIdentifier}");
        if (SaveFunctionsToDisc(currentData))
        {
            return true;
        }
        loggingService?.LogError("Something went wrong while saving the function data");
        return false;
    }

    /// <summary>
    /// Is the function provided to add a valid one
    /// </summary>
    /// <param name="modelToCheck">The function model to check</param>
    /// <returns>True if the provided function is valid</returns>
    private bool IsFunctionValid(FunctionModel modelToCheck)
    {
        loggingService?.LogTrace($"Check if given function is valid name: {modelToCheck.DisplayName} unique id: {modelToCheck.UniqueIdentifier}");
        return !string.IsNullOrEmpty(modelToCheck.UniqueIdentifier)
                && modelToCheck.Plugin != null
                && !string.IsNullOrEmpty(modelToCheck.DisplayName);
    }

    /// <inheritdoc/>
    public void DeleteFunction(string identifier)
    {
        var newFunctions = GetAvailableFunctions().Where(function => function.UniqueIdentifier != identifier).ToList();
        SaveFunctionsToDisc(newFunctions);
    }

    /// <inheritdoc/>
    public List<FunctionModel> GetAvailableFunctions()
    {
        if (cachedFunctions.Count > 0)
        {
            return cachedFunctions;
        }
        string functionFile = GetFunctionPath();
        List<FunctionModel> returnData = Enumerable.Empty<FunctionModel>().ToList();
        using (StreamReader? dataStream = FileSystemService?.GetReadStream(functionFile))
        {
            if (dataStream is not null)
            {
                returnData = serializer?.GetDeserialized<List<FunctionModel>>(dataStream.BaseStream) ?? Enumerable.Empty<FunctionModel>().ToList();
            }
        }
        returnData = returnData.Where(function => function.Plugin is not null).ToList();
        cachedFunctions.AddRange(returnData);
        return returnData!;
    }

    /// <inheritdoc/>
    public FunctionModel? GetFunction(string identifier)
    {
        return GetAvailableFunctions().FirstOrDefault(functionModel => functionModel.UniqueIdentifier == identifier);
    }

    /// <summary>
    /// Save the function to the disc
    /// </summary>
    /// <param name="dataToSave">The data to persist</param>
    /// <returns>True if saving was a success</returns>
    private bool SaveFunctionsToDisc(List<FunctionModel> dataToSave)
    {
        DirectoryInfo? settingsFolder = pathService?.GetSettingsFolderPath();
        if (serializer == null || settingsFolder == null)
        {
            loggingService?.LogError("Cannot save function models to disc because there is no serializator available or the settings folder is empty");
            loggingService?.LogError($"Serializer: {serializer?.GetType().FullName ?? string.Empty}");
            loggingService?.LogError($"Settings folder: {settingsFolder?.FullName ?? string.Empty}");
            return false;
        }
        if (!settingsFolder.Exists)
        {
            settingsFolder.Create();
        }

        string? saveData = serializer?.GetSerialized(dataToSave);
        if (!string.IsNullOrEmpty(saveData))
        {
            using (StreamWriter? writer = FileSystemService?.GetWriteStream(GetFunctionPath()))
            {
                writer?.Write(saveData);
            }
            cachedFunctions.Clear();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Method to get the path to the function file
    /// </summary>
    /// <returns>The path to the function file to use</returns>
    private string GetFunctionPath()
    {
        return Path.Combine(pathService?.GetSettingsFolderPathString() ?? Path.GetTempPath(), "functions.con");
    }

    /// <inheritdoc/>
    public bool ReplaceFunction(FunctionModel function)
    {
        var storedFunction = cachedFunctions.FirstOrDefault(storedFunction => storedFunction.UniqueIdentifier == function.UniqueIdentifier);
        if (storedFunction is null)
        {
            return false;
        }
        storedFunction.DisplayName = function.DisplayName;
        storedFunction.Description = function.Description;
        storedFunction.SortOrder = function.SortOrder;
        storedFunction.Parameters = function.Parameters;
        storedFunction.Settings = function.Settings;
        storedFunction.Path = function.Path;
        return SaveFunctionsToDisc(cachedFunctions);
    }
}
