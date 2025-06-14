using System.Data;

namespace BestArchitecture.Application.Extensions
{
    public static class DatabaseExtentions
    {
        public static DataTable ToDataTable(this IEnumerable<int> list, string columnName)
        {
            var table = new DataTable();
            table.Columns.Add(columnName, typeof(int));

            foreach (var item in list)
                table.Rows.Add(item);

            return table;
        }
    }
}
