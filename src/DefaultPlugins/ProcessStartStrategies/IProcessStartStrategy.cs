using System.Diagnostics;

namespace DefaultPlugins.ProcessStartStrategies;

/// <summary>
/// Interface to define a single strategy for running tasks on an os
/// </summary>
internal interface IProcessStartStrategy
{
    /// <summary>
    /// Create the start information to run the process for a given os.
    /// </summary>
    /// <param name="parameters">The parameters to use</param>
    /// <param name="path">The path to the file to run</param>
    /// <returns>A useable start info, if possible</returns>
    ProcessStartInfo? GetStartInfo(string parameters, string path);
}