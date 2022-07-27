using ModularToolManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Functions
{
    internal interface IFunctionService
    {

        List<FunctionModel> GetAvailableFunctions();
    }
}
