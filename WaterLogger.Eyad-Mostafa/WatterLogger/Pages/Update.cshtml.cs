using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WatterLogger.Enums;
using WatterLogger.Models;

namespace WatterLogger.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }

        public UpdateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            DrinkingWater = GetById(id);
            return Page();
        }

        private DrinkingWaterModel GetById(int id)
        {
            var drinkingWaterModel = new DrinkingWaterModel();
            using var connection = new SqliteConnection(_configuration.GetConnectionString("SqliteConnection"));
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM drinking_water WHERE Id = {id}";
                var reader = tableCmd.ExecuteReader();
                if (reader.Read())
                {
                    drinkingWaterModel.Id = reader.GetInt32(0);
                    drinkingWaterModel.Date = DateTime.ParseExact(
                        reader.GetString(1),
                        "dd/MM/yyyy hh:mm:ss tt",
                        CultureInfo.InvariantCulture
                    );

                    drinkingWaterModel.Quantity = reader.GetInt32(2);
                    drinkingWaterModel.Type = Enum.Parse<TypesEnum>(reader.GetString(3));
                }

                return drinkingWaterModel;
            }
        }

        public IActionResult OnPost(int id)
        {
            using var connection = new SqliteConnection(_configuration.GetConnectionString("SqliteConnection"));
            {
                if(!ModelState.IsValid)
                    return Page();

                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"UPDATE drinking_water SET date = '{DrinkingWater.Date}', quantity = {DrinkingWater.Quantity}, Type = '{DrinkingWater.Type.ToString()}' WHERE Id = {id}";
                var reader = tableCmd.ExecuteReader();
                if (reader.RecordsAffected > 0)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error updating record.");
                    return Page();
                }
            }
        }
    }
}
