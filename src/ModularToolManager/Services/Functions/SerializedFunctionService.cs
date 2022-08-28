using ModularToolManager.Models;
using ModularToolManager.Services.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModularToolManager.Services.Functions;

internal class SerializedFunctionService : IFunctionService
{
    private readonly ISerializeService? serializer;
    private readonly IPathService? pathService;

    public SerializedFunctionService(ISerializeService? serializer, IPathService? pathService)
    {
        this.serializer = serializer;
        this.pathService = pathService;
    }

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

    private bool IsFunctionValid(FunctionModel modelToCheck)
    {
        return !string.IsNullOrEmpty(modelToCheck.UniqueIdentifier)
                && modelToCheck.Plugin != null
                && !string.IsNullOrEmpty(modelToCheck.DisplayName);
    }

    public void DeleteFunction(string identifier)
    {
        throw new NotImplementedException();
    }

    public List<FunctionModel> GetAvailableFunctions()
    {
        DirectoryInfo? settingsFolder = pathService?.GetSettingsFolderPath();
        if (settingsFolder?.Exists == false)
        {
            return Enumerable.Empty<FunctionModel>().ToList();
        }
        return Enumerable.Empty<FunctionModel>().ToList();
    }

    public FunctionModel GetFunction(string identifier)
    {
        return GetAvailableFunctions().FirstOrDefault(functionModel => functionModel.UniqueIdentifier == identifier);
    }

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
        string saveData = serializer?.GetSerialized(dataToSave);
        return true;
    }
}
