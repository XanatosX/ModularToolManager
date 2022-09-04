using ModularToolManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModularToolManagerModel.Services.Functions
{
    /// <summary>
    /// Service to load, save and manage functionhs
    /// </summary>
    public interface IFunctionService
    {
        /// <summary>
        /// Method to get all the function which are currently available
        /// </summary>
        /// <returns>A list with all the function currently stored</returns>
        List<FunctionModel> GetAvailableFunctions();

        /// <summary>
        /// Method to get all the functions which are available async
        /// </summary>
        /// <returns>A list with all the function currently stored</returns>
        async Task<List<FunctionModel>> GetAvailableFunctionAsync() => await Task.Run(() => GetAvailableFunctions());

        /// <summary>
        /// Method to get all a specific function
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns>The function model or null if nothing was found</returns>
        public FunctionModel? GetFunction(string identifier);

        /// <summary>
        /// Method to add a new function to the service
        /// </summary>
        /// <param name="function">The function to add</param>
        /// <returns>True if adding was successful</returns>
        bool AddFunction(FunctionModel function);

        /// <summary>
        /// Delete a function from the pool
        /// </summary>
        /// <param name="function">The function to delete</param>
        void DeleteFunction(FunctionModel function) => DeleteFunction(function.UniqueIdentifier);

        /// <summary>
        /// Delete a function from the pool
        /// </summary>
        /// <param name="identifier">The function Identifier to use</param>
        void DeleteFunction(string identifier);
    }
}
