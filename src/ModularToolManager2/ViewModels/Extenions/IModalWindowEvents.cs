using System;

namespace ModularToolManager2.ViewModels.Extenions;

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
