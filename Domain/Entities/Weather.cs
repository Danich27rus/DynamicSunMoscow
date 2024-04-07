using Application.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Weather
{
    [Key]
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public float Temprature { get; set; }
    public int Humidity { get; set; }
    public float DewPoint { get; set; }
    public int Pressure { get; set; }
    public List<WindDirection> WindDirections { get; set; }
    public int WindSpeed { get; set; }
    public int? Cloudiness { get; set; }
    public int CloudBase { get; set; }
    public int? HorizontalVisibility { get; set; }
    public string? HumidityString { get; set; }
}
