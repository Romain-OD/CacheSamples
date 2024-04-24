using Bogus;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Models;
using Services;
using System.Text.Json;

namespace RedisSample;
public class CustomFieldRequestRedis(IUserSettingsService userSettingsService, IDistributedCache distributedCache)
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
        var cachedData = await distributedCache.GetStringAsync(cacheKey);

        if (cachedData == null)
        {
            var res = new List<CustomFieldJs>();

            //Fake data with bogus for sample
            var faker = new Faker<CustomFieldJs>()
               .RuleFor(cf => cf.Name, f => f.Lorem.Word());
            res = faker.Generate(10);

            var jsonData = JsonSerializer.Serialize(res);
            await distributedCache.SetStringAsync(cacheKey, jsonData);

            return res;
        }

        return JsonSerializer.Deserialize<List<CustomFieldJs>>(cachedData);
    
    }
    private async Task<List<CustomFieldJs>> GetCfsForResources()
    {
        var companyId = userSettingsService.GetUserDescription().CompanyId;
        var cacheKey = $"CustomFields_Ressources_{companyId}";
        var cachedData = await distributedCache.GetStringAsync(cacheKey);

        if (cachedData == null)
        {
            var res = new List<CustomFieldJs>();

            //Fake data with bogus for sample
            var faker = new Faker<CustomFieldJs>()
               .RuleFor(cf => cf.Name, f => f.Lorem.Word());
            res = faker.Generate(10);

            var jsonData = JsonSerializer.Serialize(res);
            await distributedCache.SetStringAsync(cacheKey, jsonData);

            return res;
        }

        return JsonSerializer.Deserialize<List<CustomFieldJs>>(cachedData);
    }

    private async Task<List<CustomFieldJs>> GetCfsForTasks()
    {
        var companyId = userSettingsService.GetUserDescription().CompanyId;
        var cacheKey = $"CustomFields_Tasks_{companyId}";
        var cachedData = await distributedCache.GetStringAsync(cacheKey);

        if (cachedData == null)
        {
            var res = new List<CustomFieldJs>();

            //Fake data with bogus for sample
            var faker = new Faker<CustomFieldJs>()
               .RuleFor(cf => cf.Name, f => f.Lorem.Word());
            res = faker.Generate(10);

            var jsonData = JsonSerializer.Serialize(res);
            await distributedCache.SetStringAsync(cacheKey, jsonData);

            return res;
        }

        // Convertir les données JSON en liste
        return JsonSerializer.Deserialize<List<CustomFieldJs>>(cachedData);
    }
    private async Task<List<CustomFieldJs>> GetCfsForProjects()
    {
        var companyId = userSettingsService.GetUserDescription().CompanyId;
        var cacheKey = $"CustomFields_Projects_{companyId}";
        var cachedData = await distributedCache.GetStringAsync(cacheKey);

        if (cachedData == null)
        {
            var res = new List<CustomFieldJs>();

            //Fake data with bogus for sample
            var faker = new Faker<CustomFieldJs>()
               .RuleFor(cf => cf.Name, f => f.Lorem.Word());
            res = faker.Generate(10);

            var jsonData = JsonSerializer.Serialize(res);
            await distributedCache.SetStringAsync(cacheKey, jsonData);

            return res;
        }

        return JsonSerializer.Deserialize<List<CustomFieldJs>>(cachedData);
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
        distributedCache.Remove($"CustomFields_Clients_{companyId}");
        distributedCache.Remove($"CustomFields_Resources_{companyId}");
        distributedCache.Remove($"CustomFields_Tasks_{companyId}");
        distributedCache.Remove($"CustomFields_Projects_{companyId}");
    }
}
