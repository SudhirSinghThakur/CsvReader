namespace CSVReader
{
    using System;
    using System.IO;
    using System.Data;
    using System.Linq;
    using CSVReader.Interface;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CsvReader : ICsvReader
    {
        public DataTable fileRecords;
        public readonly ISearch search;
        public readonly IFilter filter;
        public readonly IConverter converter;
        public readonly IPagination pagination;
        public readonly ICombinSearch combinSearch;

        /// <summary>
        /// Constructor for initialization.
        /// </summary>
        public CsvReader(string filePath)
        {
            //TODO : DI implementation.
            search = new Search();
            filter = new Filter();
            converter = new ConvertTo();
            pagination = new Pagination();
            combinSearch = new CombineSearch(search);
            fileRecords = GetRecordFromFile(filePath);
        }

        /// <summary>
        /// Need to implement this.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetNexPage<T>() where T : new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all the records from file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetRecords<T>() where T : new()
        {
            if (fileRecords != null && fileRecords.AsEnumerable().Count() != 0)
            {
                return converter.ConvertToType<T>(fileRecords);
            }
            return null;
        }

        /// <summary>
        /// Get records from file by providing the search criteria.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<T> GetRecords<T>(Search search) where T : new()
        {
            var dataTable = fileRecords;
            if (search != null)
            {
                dataTable = search.SearchRecord(dataTable, search);
                return converter.ConvertToType<T>(dataTable);
            }
            return null;
        }

        /// <summary>
        /// Get the record from file by providing the combine search.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<T> GetRecords<T>(CombineSearch search) where T : new()
        {
            var dataTable = fileRecords;
            if (search != null)
            {
                dataTable = combinSearch.CombineSearchRecord(dataTable, search);
                return converter.ConvertToType<T>(dataTable);
            }
            return null;
        }

        /// <summary>
        /// This method is Generic CSV file reader which will read the data and return list of records which fulfill the search criteria.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<T> GetRecords<T>(int pageSize, int pageNumber, Search search, params string[] columnsNames) where T : new()
        {
            var dataTable = fileRecords;

            if (search != null)
            {
                dataTable = search.SearchRecord(dataTable, search);
            }
            if (filter != null)
            {
                dataTable = filter.FilterColumns(dataTable, columnsNames);
            }
            if (pageSize != 0 && pageNumber != 0)
            {
                dataTable = pagination.ApplyPaging(pageSize, pageNumber, dataTable);
            }
            return converter.ConvertToType<T>(dataTable);
        }

        /// <summary>
        /// This method is Generic CSV file reader which will read the data and return list of records which fulfill the combine Search criteria.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<T> GetRecords<T>(int pageSize, int pageNumber, CombineSearch search, params string[] filter) where T : new()
        {
            var dataTable = fileRecords;
            if (search != null)
            {
                dataTable = combinSearch.CombineSearchRecord(dataTable, search);
            }
            if (dataTable != null && dataTable.AsEnumerable().Count() != 0)
            {
                var dataView = new DataView(dataTable);
                if (filter != null)
                {
                    dataTable = dataView.ToTable(true, filter);
                }
            }
            if (pageSize != 0 && pageNumber != 0)
            {
                dataTable = pagination.ApplyPaging(pageSize, pageNumber, dataTable);
            }
            return converter.ConvertToType<T>(dataTable);
        }

        /// <summary>
        /// This method will set the data type of the column in data table.
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private Type GetDataTypeOfColum(string dataType)
        {
            switch (dataType)
            {
                case "int":
                    return typeof(int);
                case "long":
                    return typeof(long);
                case "dateTime":
                    return typeof(DateTime);
                default:
                    return typeof(string);
            }
        }

        /// <summary>
        /// Get the record from file and save it to data table.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private DataTable GetRecordFromFile(string filePath)
        {
            DataTable dataTable = new DataTable();
            using (StreamReader sr = new StreamReader(filePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                string[] headersDataType = sr.ReadLine().Split(',');
                for (int column = 0; column < headers.Length; column++)
                {
                    dataTable.Columns.Add(headers[column]);
                    dataTable.Columns[column].DataType = GetDataTypeOfColum(headersDataType[column]);

                }
                while (!sr.EndOfStream)
                {
                    string[] rows = Regex.Split(sr.ReadLine(), ",");
                    DataRow dataRow = dataTable.NewRow();
                    for (int header = 0; header < headers.Length; header++)
                    {
                        dataRow[header] = rows[header];
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }
    }
}
