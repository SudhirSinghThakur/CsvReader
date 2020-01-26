namespace CSVReader.Interface
{
    using System.Data;

    public interface ISearch
    {
        DataTable SearchRecord(DataTable dataTable, Search search);
    }
}