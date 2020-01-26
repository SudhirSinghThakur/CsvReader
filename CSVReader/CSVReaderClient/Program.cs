using CSVReader;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace CSVReaderClient
{
    class Program
    {
        public static readonly string empFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"SampleData\Emp.csv");
        public static readonly string fooFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"SampleData\Foo.csv");

        static void Main(string[] args)
        {
            CombineSearch combineSearch = new CombineSearch
            {
                SearchList = new List<Search>
                {
                    new Search
                    {
                        Field = "Salary",
                        Operator = "==",
                        Value = "99"
                    },
                   new Search
                    {
                        Field = "Name",
                        Operator = "==",
                        Value = "Test1"
                    }
                },
                Operator = "||"
            };

            Search s = new Search
            {
                Field = "DOB",
                Operator = "==",
                Value = "10/10/1986"
            };

            string[] Filter = new string[] { "EmpNo", "Name", "Salary" };

            var csv = new CsvReader(empFilePath);

            var records = csv.GetRecords<Employee>(3, 1, s, Filter);

            var combineSearchRecord = csv.GetRecords<Employee>(1, 2, combineSearch, Filter);

            var recordsFOo = csv.GetRecords<Employee>();
        }
    }
}
