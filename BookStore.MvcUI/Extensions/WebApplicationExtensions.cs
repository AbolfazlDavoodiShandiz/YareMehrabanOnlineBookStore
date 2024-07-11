using BookStore.Data;
using BookStore.Entities.Product;
using BookStore.Services.Interfaces;
using Newtonsoft.Json;
using System.Text.Json;
using static System.Formats.Asn1.AsnWriter;

namespace BookStore.MvcUI.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> InitialDatabase(this WebApplication application)
        {
            var scope = application.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                string categoryFilePath = @"~/../DatabaseInitialData/CategoryData.json";

                if (File.Exists(categoryFilePath))
                {
                    var jsonString = File.ReadAllText(categoryFilePath);

                    var fileCategoryList = JsonConvert.DeserializeObject<List<Category>>(jsonString);

                    if (dbContext.Categories.Count() == 0)
                    {
                        List<Category> categories = new List<Category>();

                        foreach (var category in fileCategoryList)
                        {
                            categories.Add(new Category { Name = category.Name, ParentId = category.ParentId });
                        }

                        await dbContext.Categories.AddRangeAsync(categories);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return application;
        }
    }
}