namespace CSVReader.Interface
{
    using System.Data;
    using System.Collections.Generic;

    public interface IConverter
    {
        List<T> ConvertToType<T>(DataTable datatable) where T : new();
    }
}
