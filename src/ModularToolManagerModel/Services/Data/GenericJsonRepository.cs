using ModularToolManager.Services.Serialization;
using ModularToolManagerModel.Services.Data;
using ModularToolManagerModel.Services.IO;

namespace ModularToolManager.Services.Data;

/// <summary>
/// Generic implementation of the IRepository interface
/// </summary>
/// <typeparam name="T">The type of data to be stored in the repository</typeparam>
/// <typeparam name="I">The type of the id for the given repository data type 'T'</typeparam>
internal class GenericJsonRepository<T, I> : IRepository<T, I> where T : IRepositoryDataSet<I>
{
    /// <summary>
    /// The path to the file where the repository is stored
    /// </summary>
    private readonly string repositoryFilePath;

    /// <summary>
    /// The service used for interactions with the file system
    /// </summary>
    private readonly IFileSystemService fileSystemService;

    /// <summary>
    /// The service used to serialize the data for the file system
    /// </summary>
    private readonly ISerializeService serializeService;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="pathService">The path service to use</param>
    /// <param name="fileSystemService">The file system service to use</param>
    /// <param name="serializeService">The serialize service to use</param>
    public GenericJsonRepository(IPathService pathService,
                                 IFileSystemService fileSystemService,
                                 ISerializeService serializeService) : this(Path.Combine(pathService.GetSettingsFolderPathString(), $"{typeof(T).Name}.json"),
                                                                                     fileSystemService,
                                                                                     serializeService)
    {
    }

    /// <summary>
    /// Create a new instance of this repository
    /// </summary>
    /// <param name="fileName">The filename to use for the repository</param>
    /// <param name="fileSystemService">The file system service to use</param>
    /// <param name="serializeService">The serialize service to use</param>
    public GenericJsonRepository(
        string fileName,
        IFileSystemService fileSystemService,
        ISerializeService serializeService)
    {
        repositoryFilePath = fileName;
        this.fileSystemService = fileSystemService;
        this.serializeService = serializeService;
    }

    /// <summary>
    /// Delete a entry from the repository
    /// </summary>
    /// <param name="entity">The entity to delete</param>
    /// <returns>True if the deletion was successful</returns>
    public bool Delete(T entity)
    {
        List<T> entities = GetAll(item => !item.Id?.Equals(entity.Id) ?? true).ToList();
        SaveData(entities);
        return FindById(entity.Id) is null;
    }

    /// <summary>
    /// Delete a entity by it's id
    /// </summary>
    /// <param name="id">The id of the entity to delete</param>
    /// <returns>True if successfully deleted</returns>
    public bool DeleteById(I id)
    {
        var entity = FindById(id);
        return entity is null ? false : Delete(entity);
    }

    /// <summary>
    /// Find an entity by it's id
    /// </summary>
    /// <param name="id">The id of the entity to finx</param>
    /// <returns>The entity or null if nothing was found</returns>
    public T? FindById(I id)
    {
        return GetAll().FirstOrDefault(item => item.Id?.Equals(id) ?? false);
    }

    /// <summary>
    /// Get all the entites which are available and do match the given filter
    /// </summary>
    /// <param name="filter">The filter to use for searching the entities</param>
    /// <returns>A list with entities matching the given filter</returns>
    public IEnumerable<T> GetAll(Func<T, bool> filter)
    {
        return GetAll().Where(filter);
    }

    /// <summary>
    /// Get all the entites which are available
    /// </summary>
    /// <returns>A list with all the available entites</returns>
    public IEnumerable<T> GetAll()
    {
        if (!File.Exists(repositoryFilePath))
        {
            return Enumerable.Empty<T>();
        }

        IEnumerable<T> result = Enumerable.Empty<T>();
        using (StreamReader? dataStream = fileSystemService?.GetReadStream(repositoryFilePath))
        {
            if (dataStream is not null)
            {
                result = serializeService?.GetDeserialized<List<T>>(dataStream.BaseStream) ?? Enumerable.Empty<T>().ToList();
            }
        }

        return result;
    }

    /// <summary>
    /// Insert a new entity into the dataset
    /// </summary>
    /// <param name="entity">The entity to add</param>
    /// <returns>The newly inserted entity or null if nothing was added</returns>
    public T? Insert(T entity)
    {
        if (FindById(entity.Id) is not null)
        {
            return default;
        }
        List<T> entities = GetAll().ToList();
        entities.Add(entity);

        return SaveData(entities) ? FindById(entity.Id) : default;
    }

    /// <summary>
    /// Update an entity with the new data
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <returns>The newly updated entity or null if something went wrong</returns>
    public T? Update(T entity)
    {
        List<T> entities = GetAll().ToList();
        var storedEntity = entities.FirstOrDefault(loadedEntity => loadedEntity.Id?.Equals(entity.Id) ?? false);
        if (storedEntity is null)
        {
            return default;
        }
        int index = entities.IndexOf(storedEntity);
        if (index == -1)
        {
            return default;
        }

        entities.RemoveAt(index);
        entities.Insert(index, entity);

        return SaveData(entities) ? FindById(entity.Id) : default;
    }

    /// <summary>
    /// Save the data to the disc
    /// </summary>
    /// <param name="dataSet">The dataset to store</param>
    /// <returns>True if saving was successful</returns>
    private bool SaveData(IEnumerable<T> dataSet)
    {
        FileInfo info = new FileInfo(repositoryFilePath);
        if (!info.Directory?.Exists ?? false)
        {
            info.Directory?.Create();
        }
        string? saveData = serializeService?.GetSerialized(dataSet);
        if (!string.IsNullOrEmpty(saveData))
        {
            using (StreamWriter? writer = fileSystemService?.GetWriteStream(repositoryFilePath))
            {
                writer?.Write(saveData);
            }
            return true;
        }

        return false;
    }
}
