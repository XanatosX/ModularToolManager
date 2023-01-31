using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Models;
internal class DependencyModel
{
    public string? Name { get; init; }

    public string? Version { get; init; }

    public string? LicenseUrl { get; init; }

    public string? ProjectUrl { get; init; }
}
