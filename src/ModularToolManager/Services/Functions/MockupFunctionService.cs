using ModularToolManager.Models;
using ModularToolManager.Services.Plugin;
using ModularToolManagerPlugin.Plugin;
using ModularToolManagerPlugin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularToolManager.Services.Functions
{
    internal class MockupFunctionService : IFunctionService
    {
        private List<FunctionModel> functions;

        public MockupFunctionService()
        {
            functions = new List<FunctionModel>()
            {
                new FunctionModel(Guid.NewGuid().ToString(), "Mocked Test", null, String.Empty, string.Empty, int.MinValue)
            };
        }

        public List<FunctionModel> GetAvailableFunctions()
        {
            return functions;
        }

        public FunctionModel GetFunction(string identifier)
        {
            return functions.Find(function => function.UniqueIdentifier == identifier);
        }

        public void AddFunction(FunctionModel function)
        {
            functions.Add(function);
        }

        public void DeleteFunction(string identifier)
        {
            functions.RemoveAll(function => function.UniqueIdentifier == identifier);
        }
    }
}
