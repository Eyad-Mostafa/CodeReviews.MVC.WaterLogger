using System.ComponentModel.DataAnnotations;
using WatterLogger.Enums;

namespace WatterLogger.Models;

public class MealsModel
{
    public int Id { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

    public MealsEnum Meal { get; set; }
}
