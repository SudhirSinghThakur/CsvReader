namespace CSVReader
{
    using System;
    using System.Data;
    using System.Linq;
    using CSVReader.Interface;

    /// <summary>
    /// Search the records in dataTable by providing the columns name, data type of the columns and operator.
    /// Supported operators are : >, < ,>= ,<= and ==.
    /// </summary>
    public class Search : ISearch
    {
        public string Field { get; set; }
        public string Value { get; set; }
        public string Operator { get; set; }

        public DataTable SearchRecord(DataTable dataTable, Search search)
        {
            var typeOfColum = dataTable.Columns[search.Field].DataType.Name;
            switch (typeOfColum)
            {
                case "String":
                    {
                        return dataTable.AsEnumerable()
                        .Where(row => row.Field<string>(search.Field) == (search.Value)).CopyToDataTable();
                    }
                case "Int32":
                    {
                        switch (search.Operator)
                        {
                            case ">":
                                {
                                    var dt = dataTable.AsEnumerable().
                                    Where(row => row.Field<int>(search.Field) > Convert.ToInt32(search.Value));
                                    if (dt.Count() == 0)
                                    {
                                        return new DataTable();
                                    }
                                    return dt.CopyToDataTable();
                                }
                            case "<":
                                {
                                    var dt = dataTable.AsEnumerable().
                                    Where(row => row.Field<int>(search.Field) < Convert.ToInt32(search.Value));
                                    if (dt.Count() == 0)
                                    {
                                        return new DataTable();
                                    }
                                    return dt.CopyToDataTable();
                                }
                            case ">=":
                                {
                                    var dt = dataTable.AsEnumerable().
                                    Where(row => row.Field<int>(search.Field) >= Convert.ToInt32(search.Value));
                                    if (dt.Count() == 0)
                                    {
                                        return new DataTable();
                                    }
                                    return dt.CopyToDataTable();
                                }
                            case "<=":
                                {
                                    var dt = dataTable.AsEnumerable().
                                    Where(row => row.Field<int>(search.Field) <= Convert.ToInt32(search.Value));
                                    if (dt.Count() == 0)
                                    {
                                        return new DataTable();
                                    }
                                    return dt.CopyToDataTable();
                                }
                            case "==":
                                {
                                    var dt = dataTable.AsEnumerable().
                                    Where(row => row.Field<int>(search.Field) == Convert.ToInt32(search.Value));
                                    if (dt.Count() == 0)
                                    {
                                        return new DataTable();
                                    }
                                    return dt.CopyToDataTable();
                                }
                        }
                        break;
                    }
                case "Int64":
                    {
                        switch (search.Operator)
                        {
                            case ">":
                                {
                                    var dt = dataTable.AsEnumerable().
                                    Where(row => row.Field<long>(search.Field) > Convert.ToInt32(search.Value));
                                    if (dt.Count() == 0)
                                    {
                                        return new DataTable();
                                    }
                                    return dt.CopyToDataTable();
                                }
                            case "<":
                                {
                                    var dt = dataTable.AsEnumerable().
                                    Where(row => row.Field<long>(search.Field) < Convert.ToInt32(search.Value));
                                    if (dt.Count() == 0)
                                    {
                                        return new DataTable();
                                    }
                                    return dt.CopyToDataTable();
                                }
                            case ">=":
                                {
                                    var dt = dataTable.AsEnumerable().
                                    Where(row => row.Field<long>(search.Field) >= Convert.ToInt32(search.Value));
                                    if (dt.Count() == 0)
                                    {
                                        return new DataTable();
                                    }
                                    return dt.CopyToDataTable();
                                }
                            case "<=":
                                {
                                    var dt = dataTable.AsEnumerable().
                                    Where(row => row.Field<long>(search.Field) <= Convert.ToInt32(search.Value));
                                    if (dt.Count() == 0)
                                    {
                                        return new DataTable();
                                    }
                                    return dt.CopyToDataTable();
                                }
                            case "==":
                                {
                                    var dt = dataTable.AsEnumerable().
                                    Where(row => row.Field<long>(search.Field) == Convert.ToInt32(search.Value));
                                    if (dt.Count() == 0)
                                    {
                                        return new DataTable();
                                    }
                                    return dt.CopyToDataTable();
                                }
                        }
                        break;
                    }
                case "DateTime":
                    {
                        switch (search.Operator)
                        {
                            case ">":
                                {
                                    var dt = dataTable.AsEnumerable().
                                    Where(row => row.Field<DateTime>(search.Field) > Convert.ToDateTime(search.Value));
                                    if (dt.Count() == 0)
                                    {
                                        return new DataTable();
                                    }
                                    return dt.CopyToDataTable();
                                }
                            case "<":
                                {
                                    var dt = dataTable.AsEnumerable().
                                    Where(row => row.Field<DateTime>(search.Field) < Convert.ToDateTime(search.Value));
                                    if (dt.Count() == 0)
                                    {
                                        return new DataTable();
                                    }
                                    return dt.CopyToDataTable();
                                }
                            case ">=":
                                {
                                    var dt = dataTable.AsEnumerable().
                                    Where(row => row.Field<DateTime>(search.Field) >= Convert.ToDateTime(search.Value));
                                    if (dt.Count() == 0)
                                    {
                                        return new DataTable();
                                    }
                                    return dt.CopyToDataTable();
                                }
                            case "<=":
                                {
                                    var dt = dataTable.AsEnumerable().
                                    Where(row => row.Field<DateTime>(search.Field) <= Convert.ToDateTime(search.Value));
                                    if (dt.Count() == 0)
                                    {
                                        return new DataTable();
                                    }
                                    return dt.CopyToDataTable();
                                }
                            case "==":
                                {
                                    var dt = dataTable.AsEnumerable().
                                    Where(row => row.Field<DateTime>(search.Field) == Convert.ToDateTime(search.Value));
                                    if (dt.Count() == 0)
                                    {
                                        return new DataTable();
                                    }
                                    return dt.CopyToDataTable();
                                }
                        }
                        break;
                    }
            }
            return dataTable;
        }
    }
}