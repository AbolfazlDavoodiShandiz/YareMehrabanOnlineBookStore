using BookStore.Data;
using BookStore.Entities.Product;
using Newtonsoft.Json;

namespace BookStore.MvcUI.Utility.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> InitialDatabase(this WebApplication application)
        {
            var scope = application.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                string categoryFilePath = @"Utility/DatabaseInitialData/CategoryData.json";
                string publicationFilePath = @"Utility/DatabaseInitialData/PublicationData.json";

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

                if (File.Exists(publicationFilePath))
                {
                    var jsonString = File.ReadAllText(publicationFilePath);

                    var filePublicationList = JsonConvert.DeserializeObject<List<Publication>>(jsonString);

                    if (dbContext.Publications.Count() == 0)
                    {
                        List<Publication> publications = new List<Publication>();

                        foreach (var publication in filePublicationList)
                        {
                            publications.Add(new Publication { Name = publication.Name });
                        }

                        await dbContext.Publications.AddRangeAsync(publications);
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