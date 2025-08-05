using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WatterLogger.Enums;
using WatterLogger.Models;

namespace WatterLogger.Pages.Food
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public MealsModel Meals { get; set; }

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            Meals = GetById(id);
            return Page();
        }

        private MealsModel GetById(int id)
        {
            var mealsModel = new MealsModel();
            using var connection = new SqliteConnection(_configuration.GetConnectionString("SqliteConnection"));
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM meals WHERE Id = {id}";
                var reader = tableCmd.ExecuteReader();
                if (reader.Read())
                {
                    mealsModel.Id = reader.GetInt32(0);
                    mealsModel.Date = DateTime.ParseExact(
                        reader.GetString(1),
                        "dd/MM/yyyy hh:mm:ss tt",
                        CultureInfo.InvariantCulture
                    );
                    mealsModel.Meal = Enum.Parse<MealsEnum>(reader.GetString(2));
                }

                return mealsModel;
            }
        }

        public IActionResult OnPost(int id)
        {
            using var connection = new SqliteConnection(_configuration.GetConnectionString("SqliteConnection"));
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"DELETE FROM meals WHERE Id = {id}";
                var reader = tableCmd.ExecuteReader();
                if (reader.RecordsAffected > 0)
                {
                    return RedirectToPage("./Food");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error deleting record.");
                    return Page();
                }
            }
        }

    }
}
