using System.Data;
using System.Reflection;

namespace Badop.Infrastructure.Storage;

internal static class DataTableBuilder
{
    internal static DataTable CreateDataTable<T>(IEnumerable<T> list)
    {
        Type type = typeof(T);
        var properties = type.GetProperties();

        DataTable dataTable = new DataTable();
        foreach (PropertyInfo info in properties)
        {
            dataTable.Columns.Add(new DataColumn(info.Name,
                Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
        }

        foreach (T entity in list)
        {
            object[] values = new object[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                values[i] = properties[i]?.GetValue(entity);
            }

            dataTable.Rows.Add(values);
        }

        return dataTable;
    }

    internal static DataTable CreateDataTableForSingleColumn<T>(string columnName, IEnumerable<T> list)
    {
        if (!typeof(T).IsPrimitive)
        {
            throw new Exception(
                "This is used when we need to pass a datatable with a single column of a primitive type.");
        }

        var table = new System.Data.DataTable();
        table.Columns.Add(columnName, typeof(T));

        foreach (var p in list)
        {
            var row = table.NewRow();
            row[0] = p;
            table.Rows.Add(row);
        }

        return table;
    }
}