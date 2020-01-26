namespace CSVReader.Interface
{
    using System.Collections.Generic;

    public interface ICsvReader
    {
        List<T> GetNexPage<T>() where T : new();
        List<T> GetRecords<T>() where T : new();
        List<T> GetRecords<T>(Search search) where T : new();
        List<T> GetRecords<T>(CombineSearch search) where T : new();
        List<T> GetRecords<T>(int pageSize, int pageNumber, Search search, params string[] filter) where T : new();
        List<T> GetRecords<T>(int pageSize, int pageNumber, CombineSearch search, params string[] filter) where T : new();
    }
}
