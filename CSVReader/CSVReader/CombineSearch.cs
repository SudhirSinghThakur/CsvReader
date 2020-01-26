using CSVReader.Interface;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CSVReader
{
    /// <summary>
    /// Combine search is used for filtering records based on result of searches.
    /// Only && and || operator is supported for now.
    /// </summary>
    public class CombineSearch : ICombinSearch
    {
        public readonly ISearch search;
        public string Operator { get; set; }
        public List<Search> SearchList { get; set; }

        /// <summary>
        /// Default constructor of the class.
        /// </summary>
        public CombineSearch()
        {

        }

        /// <summary>
        /// Parameterize constructor of the class.
        /// </summary>
        /// <param name="search"></param>
        public CombineSearch(ISearch search)
        {
            this.search = search;
        }

        /// <summary>
        /// Return the data table which satisfy the search criteria. 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="combineSearch"></param>
        /// <returns></returns>
        public DataTable CombineSearchRecord(DataTable dataTable, CombineSearch combineSearch)
        {
            if (combineSearch.Operator == "&&")
            {
                foreach (var search in combineSearch.SearchList)
                {
                    if (dataTable.AsEnumerable().Count() == 0)
                    {
                        break;
                    }
                    dataTable = search.SearchRecord(dataTable, search);
                }
            }
            else if (combineSearch.Operator == "||")
            {
                var results = new List<DataTable>();
                var searchresult = new DataTable();
                foreach (var search in combineSearch.SearchList)
                {
                    if (dataTable.AsEnumerable().Count() == 0)
                    {
                        break;
                    }
                    results.Add(search.SearchRecord(dataTable, search));
                }
                foreach (var result in results)
                {
                    searchresult.Merge(result);
                }
                dataTable = searchresult;
            }

            return dataTable;
        }
    }
}