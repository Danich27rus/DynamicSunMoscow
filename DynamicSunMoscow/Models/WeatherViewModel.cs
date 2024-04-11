using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DynamicSunMoscow.Models;

public class WeatherViewModel
{
    public SelectList Years { get; set; }
    public string SelectedYear { get; set; }
    public SelectList Months { get; set; }
    public string SelectedMonth { get; set; }
    public IEnumerable<Weather> Weathers { get; set; }
    public PageModel PageModel { get; set; }

    public WeatherViewModel(List<string> years, List<string> months, string selectedYear, string selectedMonth)
    {
        Years = new SelectList(years);
        Months = new SelectList(months);
        SelectedYear = selectedYear;
        SelectedMonth = selectedMonth;
    }   
}
