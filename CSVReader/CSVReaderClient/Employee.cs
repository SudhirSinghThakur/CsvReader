using System;
using System.Reflection;
using CSVReader;

namespace CSVReaderClient
{
    /// <summary>
    /// This class is required to Map CSV file. As a client you have to create New class for each your CSV file.
    /// </summary>
    internal class Employee 
    {
        public long EmpNo { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public long Salary { get; set; }
        public string Address { get; set; }
    }
}
