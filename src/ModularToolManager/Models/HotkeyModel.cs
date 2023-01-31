using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Models;

/// <summary>
/// Model representing a single hotkey
/// </summary>
internal class HotkeyModel
{
    /// <summary>
    /// The name of the hotkey
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// The description of the hotkey
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Where does the hotkey work
    /// </summary>
    public string? WorkingOn { get; init; }

    /// <summary>
    /// The keys to press to trigger the hotkey
    /// </summary>
    public List<string>? Keys { get; init; }
}
