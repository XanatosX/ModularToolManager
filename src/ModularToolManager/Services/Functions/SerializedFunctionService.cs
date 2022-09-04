using ModularToolManager.Models;
using ModularToolManager.Services.IO;
using ModularToolManager.Services.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModularToolManager.Services.Functions;

/// <summary>
/// Service to save the function to the disc
/// </summary>
internal class SerializedFunctionService : IFunctionService
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
    /// Create a new instance of this class
    /// </summary>
    /// <param name="serializer">The serailzier class to use</param>
    /// <param name="pathService">The path service to use</param>
    public SerializedFunctionService(ISerializeService? serializer, IPathService? pathService)
    {
        this.serializer = serializer;
        this.pathService = pathService;
    }

    /// <inheritdoc/>
    public bool AddFunction(FunctionModel function)
    {
        var currentData = GetAvailableFunctions();
        if (currentData.Any(model => model.UniqueIdentifier == function.UniqueIdentifier) || !IsFunctionValid(function))
        {
            return false;
        }
        currentData.Add(function);
        return SaveFunctionsToDisc(currentData);
    }

    /// <summary>
    /// Is the function provided to add a valid one
    /// </summary>
    /// <param name="modelToCheck">The function model to check</param>
    /// <returns>True if the provided function is valid</returns>
    private bool IsFunctionValid(FunctionModel modelToCheck)
    {
        return !string.IsNullOrEmpty(modelToCheck.UniqueIdentifier)
                && modelToCheck.Plugin != null
                && !string.IsNullOrEmpty(modelToCheck.DisplayName);
    }

    /// <inheritdoc/>
    public void DeleteFunction(string identifier)
    {
        var functions = GetAvailableFunctions().Where(function => function.UniqueIdentifier != identifier);
        SaveFunctionsToDisc(functions.ToList());
    }

    /// <inheritdoc/>
    public List<FunctionModel> GetAvailableFunctions()
    {
        string functionFile = GetFunctionPath();
        if (!File.Exists(functionFile))
        {
            return Enumerable.Empty<FunctionModel>().ToList();
        }
        List<FunctionModel> returnData = Enumerable.Empty<FunctionModel>().ToList();
        using (FileStream fileStream = new FileStream(functionFile, FileMode.Open, FileAccess.Read))
        {
            returnData = serializer?.GetDeserialized<List<FunctionModel>>(fileStream) ?? Enumerable.Empty<FunctionModel>().ToList();
        }
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
            //@Note add logging
            return false;
        }
        if (!settingsFolder.Exists)
        {
            settingsFolder.Create();
        }
        string? saveData = serializer?.GetSerialized(dataToSave);
        if (!string.IsNullOrEmpty(saveData))
        {
            using (FileStream fileStream = new FileStream(GetFunctionPath(), FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.Write(saveData);
                }
            }
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
}
