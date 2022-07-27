using System;

namespace ModularToolManager.ViewModels.Extenions;

/// <summary>
/// Interface to define modal window events
/// </summary>
public interface IModalWindowEvents
{
    /// <summary>
    /// Window is getting closed event
    /// </summary>
    event EventHandler Closing;
}
