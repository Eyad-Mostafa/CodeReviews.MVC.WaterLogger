using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WatterLogger.Enums;
using WatterLogger.Models;

namespace WatterLogger.Pages.Food
{
    public class UpdateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public MealsModel Meal { get; set; }

        public UpdateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            Meal = GetById(id);
            return Page();
        }

        private MealsModel GetById(int id)
        {
            var mealModel = new MealsModel();
            using var connection = new SqliteConnection(_configuration.GetConnectionString("SqliteConnection"));
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM meals WHERE Id = {id}";
                var reader = tableCmd.ExecuteReader();
                if (reader.Read())
                {
                    mealModel.Id = reader.GetInt32(0);
                    mealModel.Date = DateTime.ParseExact(
                        reader.GetString(1),
                        "dd/MM/yyyy hh:mm:ss tt",
                        CultureInfo.InvariantCulture
                    );

                    mealModel.Meal = Enum.Parse<MealsEnum>(reader.GetString(2));
                }

                return mealModel;
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
                    $"UPDATE meals SET date = '{Meal.Date}', meal = '{Meal.Meal.ToString()}' WHERE Id = {id}";
                var reader = tableCmd.ExecuteReader();
                if (reader.RecordsAffected > 0)
                {
                    return RedirectToPage("./Food");
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
