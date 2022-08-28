using ModularToolManager.Models;
using System;
using System.Collections.Generic;

namespace ModularToolManager.Services.Functions
{
    internal class MockupFunctionService : IFunctionService
    {
        private List<FunctionModel> functions;

        public MockupFunctionService()
        {
            functions = new List<FunctionModel>()
            {
                new FunctionModel(Guid.NewGuid().ToString(), "Mocked Test", null , String.Empty, string.Empty, int.MinValue)
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

        public bool AddFunction(FunctionModel function)
        {
            functions.Add(function);
            return true;
        }

        public void DeleteFunction(string identifier)
        {
            functions.RemoveAll(function => function.UniqueIdentifier == identifier);
        }
    }
}
