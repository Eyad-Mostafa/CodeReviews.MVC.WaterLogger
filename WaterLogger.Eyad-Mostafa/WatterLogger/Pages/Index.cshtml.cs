using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WatterLogger.Enums;
using WatterLogger.Models;

namespace WatterLogger.Pages;
public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;
    public List<DrinkingWaterModel> Records { get; set; }

    public IndexModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnGet()
    {
        Records = GetAllRecords();
    }

    private List<DrinkingWaterModel> GetAllRecords()
    {
        using var connection = new SqliteConnection(_configuration.GetConnectionString("SqliteConnection"));
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                $"SELECT * FROM drinking_water";

            var tableData = new List<DrinkingWaterModel>();
            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
            {
                tableData.Add(new DrinkingWaterModel
                {
                    Id = reader.GetInt32(0),
                    Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentCulture.DateTimeFormat),
                    Quantity = reader.GetDouble(2),
                    Type = Enum.Parse<TypesEnum>(reader.GetString(3))
                });
            }

            return tableData;
        }
    }
}
