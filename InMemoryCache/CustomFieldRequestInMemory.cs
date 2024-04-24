using Bogus;
using Microsoft.Extensions.Caching.Memory;
using Models;
using Services;

namespace InMemoryCacheSampleSample;
public class CustomFieldRequestInMemory(IUserSettingsService userSettingsService, IMemoryCache memoryCache)
{
    public async Task<List<CustomFieldJs>> GetCustomFieldsFor(CustomFieldTypeUsedFor cfObject)
    {
        var res = cfObject switch
        {
            CustomFieldTypeUsedFor.Project => await GetCfsForProjects(),
            CustomFieldTypeUsedFor.Task => await GetCfsForTasks(),
            CustomFieldTypeUsedFor.Resource => await GetCfsForResources(),
            CustomFieldTypeUsedFor.Client => await GetCfsForClients(),
            _ => throw new ArgumentOutOfRangeException(nameof(cfObject), cfObject, null),
        };
        return res;
    }
    private async Task<List<CustomFieldJs>> GetCfsForClients()
    {
        var companyId = userSettingsService.GetUserDescription().CompanyId;
        var cacheKey = $"CustomFields_Clients_{companyId}";
        if (!memoryCache.TryGetValue(cacheKey, out List<CustomFieldJs> res))
        {
            res = new List<CustomFieldJs>();

            //Fake data with bogus for sample
            var faker = new Faker<CustomFieldJs>()
               .RuleFor(cf => cf.Name, f => f.Lorem.Word());
            res = faker.Generate(10);

            memoryCache.Set(cacheKey, res);
        }

        return res;
    }
    private async Task<List<CustomFieldJs>> GetCfsForResources()
    {
        var companyId = userSettingsService.GetUserDescription().CompanyId;
        var cacheKey = $"CustomFields_Ressources_{companyId}";
        if (!memoryCache.TryGetValue(cacheKey, out List<CustomFieldJs> res))
        {
            res = GenerateFakeDataWithBogus();
            memoryCache.Set(cacheKey, res);
        }

        return res ?? [];
    }

    private async Task<List<CustomFieldJs>> GetCfsForTasks()
    {
        var companyId = userSettingsService.GetUserDescription().CompanyId;
        var cacheKey = $"CustomFields_Tasks_{companyId}";
        if (!memoryCache.TryGetValue(cacheKey, out List<CustomFieldJs> res))
        {
            res = new List<CustomFieldJs>();

            //Fake data with bogus for sample
            var faker = new Faker<CustomFieldJs>()
               .RuleFor(cf => cf.Name, f => f.Lorem.Word());
            res = faker.Generate(10);

            memoryCache.Set(cacheKey, res);
        }

        return res;
    }
    private async Task<List<CustomFieldJs>> GetCfsForProjects()
    {
        var companyId = userSettingsService.GetUserDescription().CompanyId;
        var cacheKey = $"CustomFields_Projects_{companyId}";
        if (!memoryCache.TryGetValue(cacheKey, out List<CustomFieldJs> res))
        {
            res = new List<CustomFieldJs>();

            //Fake data with bogus for sample
            var faker = new Faker<CustomFieldJs>()
               .RuleFor(cf => cf.Name, f => f.Lorem.Word());
            res = faker.Generate(10);

            memoryCache.Set(cacheKey, res);
        }

        return res;
    }

    private static List<CustomFieldJs> GenerateFakeDataWithBogus()
    {
        List<CustomFieldJs> res;
        //Fake data with bogus for sample
        var faker = new Faker<CustomFieldJs>()
           .RuleFor(cf => cf.Name, f => f.Lorem.Word());
        res = faker.Generate(10);
        return res;
    }

    public void InvalidateCacheForCompany()
    {
        var companyId = userSettingsService.GetUserDescription().CompanyId;
        memoryCache.Remove($"CustomFields_Clients_{companyId}");
        memoryCache.Remove($"CustomFields_Resources_{companyId}");
        memoryCache.Remove($"CustomFields_Tasks_{companyId}");
        memoryCache.Remove($"CustomFields_Projects_{companyId}");
    }
}
