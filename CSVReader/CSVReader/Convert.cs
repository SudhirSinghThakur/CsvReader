namespace CSVReader
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using CSVReader.Interface;
    using System.Collections.Generic;

    /// <summary>
    /// ConvertTo class definition.
    /// </summary>
    public class ConvertTo : IConverter
    {
        /// <summary>
        /// This is used for converting the data table to Type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datatable"></param>
        /// <returns></returns>
        public List<T> ConvertToType<T>(DataTable datatable) where T : new ()
        {
            List<T> objects = new List<T>();
            try
            {
                List<string> columnsNames = new List<string>();
                foreach (DataColumn DataColumn in datatable.Columns)
                {
                    columnsNames.Add(DataColumn.ColumnName);
                }
                objects = datatable.AsEnumerable().ToList().ConvertAll(row => GetObject<T>(row, columnsNames));
                return objects;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while ConvertTo type T. Exception : {0}", ex.Message);
            }
            return null;
        }

        /// <summary>
        /// This method is used for converting the data table row to type T object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="columnsName"></param>
        /// <returns></returns>
        private static T GetObject<T>(DataRow row, List<string> columnsName) where T : new()
        {
            T obj = new T();
            try
            {
                string value = "";
                string columnname = "";
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        value = row[columnname].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = row[columnname].ToString().Replace("%", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception while Creating Object of type T. Exception : {0}", ex.Message);
            }
            return obj;
        }
    }
}
