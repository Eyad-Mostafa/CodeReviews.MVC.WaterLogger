using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WatterLogger.Models;

namespace WatterLogger.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }


            using (var connection = new SqliteConnection(_configuration.GetConnectionString("SqliteConnection")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"INSERT INTO drinking_water(date, quantity, Type) VALUES('{DrinkingWater.Date}', {DrinkingWater.Quantity}, '{DrinkingWater.Type.ToString()}')";
                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

            return Page();
        }
    }
}
