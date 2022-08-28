using ModularToolManager.Models;
using System.Collections.Generic;

namespace ModularToolManager.Services.Functions
{
    internal interface IFunctionService
    {
        List<FunctionModel> GetAvailableFunctions();


        public FunctionModel GetFunction(string identifier);


        bool AddFunction(FunctionModel function);


        void DeleteFunction(FunctionModel function) => DeleteFunction(function.UniqueIdentifier);


        void DeleteFunction(string identifier);
    }
}
