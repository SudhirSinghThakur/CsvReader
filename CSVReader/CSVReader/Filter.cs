namespace CSVReader
{
    using System.Data;
    using CSVReader.Interface;

    /// <summary>
    /// Filter class to get the specific columns from the CSV file.
    /// </summary>
    public class Filter : IFilter
    {
        /// <summary>
        /// Filter the columns in data table by providing list of columns.
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="columnsNames"></param>
        /// <returns></returns>
        public DataTable FilterColumns(DataTable dataTable, params string[] columnsNames)
        {
            var dataView = new DataView(dataTable);
            if (columnsNames != null)
            {
                dataTable = dataView.ToTable(true, columnsNames);
            }
            return dataTable;
        }
    }
}
