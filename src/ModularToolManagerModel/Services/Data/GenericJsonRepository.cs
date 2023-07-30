using Microsoft.Extensions.Logging;
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

    public GenericJsonRepository(
        string fileName,
        IFileSystemService fileSystemService,
        ISerializeService serializeService)
    {
        this.repositoryFilePath = fileName;
        this.fileSystemService = fileSystemService;
        this.serializeService = serializeService;
    }

    public bool Delete(T entity)
    {
        List<T> entities = GetAll(item => !item.Id?.Equals(entity.Id) ?? true).ToList();
        SaveData(entities);
        return FindById(entity.Id) is null;
    }

    public bool DeleteById(I id)
    {
        var entity = FindById(id);
        return entity is null ? false : Delete(entity);
    }

    public T? FindById(I id)
    {
        return GetAll().FirstOrDefault(item => item.Id?.Equals(id) ?? false);
    }

    public IEnumerable<T> GetAll(Func<T, bool> filter)
    {
        return GetAll().Where(filter);
    }

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
