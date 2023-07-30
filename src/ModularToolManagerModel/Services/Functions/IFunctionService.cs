using ModularToolManager.Models;

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
        /// Method to get a specific function
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
        /// Replace the function model with the save unique id with the new data set
        /// </summary>
        /// <param name="function">The new function to use</param>
        /// <returns>True if replacement was successful</returns>
        bool ReplaceFunction(FunctionModel function);

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

        /// <summary>
        /// Update a given function model and save it 
        /// </summary>
        /// <param name="identifier">The identifier </param>
        /// <param name="updateMethod">The update method</param>
        /// <returns>True if update was successful</returns>
        bool UpdateFunction(string identifier, Action<FunctionModel> updateMethod);

        /// <summary>
        /// Update a large number of function models and save it
        /// </summary>
        /// <param name="identifiers">The identifiers to use</param>
        /// <param name="updateMethod">The update method</param>
        /// <returns>True if update was successful</returns>
        IEnumerable<bool> UpdateFunction(IEnumerable<string> identifiers, Action<FunctionModel> updateMethod);
    }
}
