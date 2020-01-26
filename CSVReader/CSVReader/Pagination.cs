namespace CSVReader
{
    using System.Data;
    using System.Linq;
    using CSVReader.Interface;

    public class Pagination : IPagination
    {
        public int pageSize;
        public int pageNumber;
        public DataTable ApplyPaging(int pageSize, int pageNumber, DataTable dataTable)
        {
            pageNumber = --pageNumber;
            this.pageSize = pageSize;
            this.pageNumber = pageNumber;
            return dataTable.AsEnumerable().Skip(pageSize * pageNumber).Take(pageSize).CopyToDataTable();
        }
    }
}
