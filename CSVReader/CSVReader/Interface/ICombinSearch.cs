namespace CSVReader.Interface
{
    using System.Data;

    public interface ICombinSearch
    {
        DataTable CombineSearchRecord(DataTable dataTable, CombineSearch combineSearch);
    }
}