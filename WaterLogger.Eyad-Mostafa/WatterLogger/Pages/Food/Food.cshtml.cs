using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WatterLogger.Enums;
using WatterLogger.Models;

namespace WatterLogger.Pages.Food;
public class FoodModel : PageModel
{
    private readonly IConfiguration _configuration;
    public List<MealsModel> Records { get; set; }

    public FoodModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnGet()
    {
        Records = GetAllRecords();
    }

    private List<MealsModel> GetAllRecords()
    {
        using var connection = new SqliteConnection(_configuration.GetConnectionString("SqliteConnection"));
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                $"SELECT * FROM meals";

            var tableData = new List<MealsModel>();
            var reader = tableCmd.ExecuteReader();

            while (reader.Read())
            {
                tableData.Add(new MealsModel
                {
                    Id = reader.GetInt32(0),
                    Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentCulture.DateTimeFormat),
                    Meal = Enum.Parse<MealsEnum>(reader.GetString(2))
                });
            }

            return tableData;
        }
    }
}
