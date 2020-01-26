namespace CSVReader.Interface
{
    using System.Data;

    public interface IPagination
    {
        DataTable ApplyPaging(int pageSize, int pageNumber, DataTable dataTable);
    }
}
