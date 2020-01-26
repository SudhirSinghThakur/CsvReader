using System.Data;

namespace CSVReader.Interface
{
    public interface IFilter
    {
        DataTable FilterColumns(DataTable dataTable, params string[] columnsNames);
    }
}
