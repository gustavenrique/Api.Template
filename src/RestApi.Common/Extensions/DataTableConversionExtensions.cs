using System.Data;

namespace RestApi.Common.Extensions;

/// <summary>
/// Extensions para conversão de tipos nativos para DataTable
/// </summary>
public static class DataTableConversionExtensions
{
    public static DataTable ParaDataTable(this IReadOnlyCollection<Guid> guids, string coluna = "ID")
    {
        DataTable tabela = new();

        tabela.Columns.Add(coluna, typeof(Guid));

        foreach (var categoria in guids)
        {
            tabela.Rows.Add(categoria);
        }

        return tabela;
    }
}