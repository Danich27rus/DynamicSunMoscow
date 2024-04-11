using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Weather
{
    [Key]
    public Guid Id { get; set; }
    /// <summary>
    /// Время засечки значений
    /// </summary>
    public DateTime DateTime { get; set; }
    /// <summary>
    /// Темепература воздуха, градусы C
    /// </summary>
    public float Temprature { get; set; }
    /// <summary>
    /// Относительная влажность воздуха, %
    /// </summary>
    public int Humidity { get; set; }
    /// <summary>
    /// Точка росы, гр. Ц.
    /// </summary>
    public float DewPoint { get; set; }
    /// <summary>
    /// Атмосферное давление, мм рт.ст.
    /// </summary>
    public int Pressure { get; set; }
    /// <summary>
    /// Направление ветра
    /// </summary>
    public string? WindDirections { get; set; }
    /// <summary>
    /// Скорость ветра, м/с
    /// </summary>
    public int? WindSpeed { get; set; }
    /// <summary>
    /// Облачность, %
    /// </summary>
    public int? Cloudiness { get; set; }
    /// <summary>
    /// Нижняя граница облачности
    /// </summary>
    public int? CloudBase { get; set; }
    /// <summary>
    /// Горизонтальная видимость, км
    /// </summary>
    public int? HorizontalVisibility { get; set; }
    /// <summary>
    /// Погодные явления
    /// </summary>
    public string? HumidityString { get; set; }
}