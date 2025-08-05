using System.ComponentModel.DataAnnotations;
using WatterLogger.Enums;

namespace WatterLogger.Models;

public class DrinkingWaterModel
{
    public int Id { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

    [Range(0, Double.MaxValue, ErrorMessage = "Please enter a valid quantity.")]
    public double Quantity { get; set; }
    public TypesEnum Type { get; set;}
}
