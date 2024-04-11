using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Base.Context;
using Infrastructure.Base.Repository;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Globalization;

namespace Base.Repository
{
    public class WeatherDataRepositoryAsync : GenericRepositoryAsync<Weather>, IWeatherDataRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public WeatherDataRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddWeathersFromFile(string filepath)
        {
            IWorkbook workbook;

            using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(fileStream);
            }

            int numberOfSheets = workbook.NumberOfSheets;

            if (numberOfSheets <= 0) 
            {
                return;
            }

            for (int sheetNum = 0; sheetNum < numberOfSheets; sheetNum++)
            {
                ISheet sheet = workbook.GetSheetAt(sheetNum);
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);

                    if (row.PhysicalNumberOfCells < 12)
                    {
                        continue;
                    }

                    if (row.GetCell(3).CellType != CellType.Numeric)
                    {
                        continue;
                    }

                    if (row != null)
                    {
                        Weather weatherData = new();

                        weatherData.DateTime = DateTime.ParseExact(row.GetCell(0).StringCellValue, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        weatherData.DateTime = weatherData.DateTime.AddTicks(DateTime.ParseExact(row.GetCell(1).StringCellValue, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay.Ticks);
                        weatherData.Temprature = (float)row.GetCell(2).NumericCellValue;
                        weatherData.Humidity = (int)row.GetCell(3).NumericCellValue;
                        weatherData.DewPoint = (float)row.GetCell(4).NumericCellValue;
                        weatherData.Pressure = (int)row.GetCell(5).NumericCellValue;
                        weatherData.WindDirections = row.GetCell(6).StringCellValue;
                        weatherData.WindSpeed = null;
                        if (row.GetCell(7).CellType == CellType.Numeric)
                        {
                            weatherData.WindSpeed = (int?)row.GetCell(7).NumericCellValue;
                        }
                        weatherData.Cloudiness = null;
                        if (row.GetCell(8).CellType == CellType.Numeric)
                        {
                            weatherData.Cloudiness = (int?)row.GetCell(8).NumericCellValue;
                        }
                        weatherData.CloudBase = null;
                        if (row.GetCell(9).CellType == CellType.Numeric)
                        {
                            weatherData.CloudBase = (int)row.GetCell(9).NumericCellValue;
                        }
                        weatherData.HorizontalVisibility = null;
                        if (row.GetCell(10).CellType == CellType.Numeric)
                        {
                            weatherData.HorizontalVisibility = (int?)row.GetCell(10).NumericCellValue;
                        }
                        weatherData.HumidityString = row.GetCell(11).StringCellValue;
                            
                        await _dbContext.Weathers.AddAsync(weatherData);
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task<IEnumerable<Weather>> GetByMonthAsync(int month)
        {
            return await _dbContext.Weathers.Where(w => w.DateTime.Month == month).ToListAsync();
        }

        public async Task<IEnumerable<Weather>> GetByYearAsync(int year)
        {
            return await _dbContext.Weathers.Where(w => w.DateTime.Year == year).ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbContext.Weathers.CountAsync();
        }
    }
}
