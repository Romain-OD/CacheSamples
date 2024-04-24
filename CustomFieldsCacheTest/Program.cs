// See https://aka.ms/new-console-template for more information


using Microsoft.Extensions.DependencyInjection;
using InMemoryCacheSampleSample;
using InMemoryCacheSample.Models;
using InMemoryCacheSample.Service;

var services = new ServiceCollection();
services.AddTransient<IUserSettingsService, UserSettingsService>();
services.AddMemoryCache();
services.AddTransient<CustomFieldRequest>();
var serviceProvider = services.BuildServiceProvider();

var customFieldsRequest = serviceProvider.GetService<CustomFieldRequest>();

var customFields = customFieldsRequest.GetCustomFieldsFor(CustomFieldTypeUsedFor.Client).Result;
Console.WriteLine($"Nombre de champs personnalisés pour les clients : {customFields.Count}");

foreach(var customField in customFields)
{
    Console.WriteLine(customField.Name);
}

