using AutoMarket.Server.Core;
using AutoMarket.Server.Core.Models;
using AutoMarket.Server.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Hosting.Internal;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExtractDB
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            await ExtractDB();
        }

        private static async Task ExtractDB()
        {
            var generationJsonPath = @"D:\Work\AutoMarket-USA\Backend\ExtractDB\generation.json";
            var countryJsonPath = @"D:\Work\AutoMarket-USA\Backend\ExtractDB\country.json";
            var makeJsonPath = @"D:\Work\AutoMarket-USA\Backend\ExtractDB\make.json";
            var modelJsonPath = @"D:\Work\AutoMarket-USA\Backend\ExtractDB\model.json";
            var modificationJsonPath = @"D:\Work\AutoMarket-USA\Backend\ExtractDB\modification.json";
            var technicalConditionJsonPath = @"D:\Work\AutoMarket-USA\Backend\ExtractDB\technicalCondition.json";
            var gearBoxJsonPath = @"D:\Work\AutoMarket-USA\Backend\ExtractDB\gearBox.json";
            var fuelJsonPath = @"D:\Work\AutoMarket-USA\Backend\ExtractDB\fuel.json";
            var driveTrainJsonPath = @"D:\Work\AutoMarket-USA\Backend\ExtractDB\driveTrain.json";
            var bodyTypeJsonPath = @"D:\Work\AutoMarket-USA\Backend\ExtractDB\bodyType.json";

            try
            {
                var _ctx = new DbContextOptionsBuilder<DataContext>()
                    .UseSqlServer("Server=.;Database=AutoMarketDB;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False")
                    .Options;

                var dataContext = new DataContext(_ctx);

                var generationRepository = new GenerationRepository(dataContext);

                var generations = await generationRepository.GetAllAsync();

                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                var json = JsonSerializer.Serialize(generations, jsonOptions);
                
                if (!File.Exists(generationJsonPath))
                {
                    File.Create(generationJsonPath).Close();
                }

                await File.WriteAllTextAsync(generationJsonPath, json);

                foreach (var g in generations)
                {
                    Console.WriteLine($"ID: {g.Id}");
                    Console.WriteLine($"Make: {g.Name}");
                }

            } catch (Exception ex) 
            {
                await Console.Out.WriteLineAsync($"Error extracting data from the database: {ex.Message}");
            }
        }
    }
}