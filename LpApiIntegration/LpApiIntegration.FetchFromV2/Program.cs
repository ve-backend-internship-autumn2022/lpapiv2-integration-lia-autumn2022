using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LpApiIntegration.FetchFromV2;
using LpApiIntegration.FetchFromV2.StudentModels;
using LpApiIntegration.FetchFromV2.GroupModel;
using System.Text;
using System.Text.Json;
using LpApiIntegration.FetchFromV2.StaffMemberModels;
using LpApiIntegration.FetchFromV2.API;
using Microsoft.EntityFrameworkCore;
using LpApiIntegration.FetchFromV2.Db;

using IHost host = Host.CreateDefaultBuilder(args).Build();
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

ApiSettings apiSettings = config.GetRequiredSection("ApiSettings").Get<ApiSettings>();


// Application code should start here.

// Students //
string jsonStudents = FetchFromApi.GetStudents(apiSettings);

//Deserializing json to object
var studentResponse = JsonSerializer.Deserialize<StudentsApiResponse>(jsonStudents);

//Checks if student exist and Adding Students
DbManager.StudentManager(studentResponse);


//foreach (var item in studentResponse.Data.ReferenceData.Groups)
//{
//    if (item.ExtendedProperties != null)
//    {
//        Console.WriteLine($"\n{item.Name} {item.Code}");
//        for (int groupNumber = 0; groupNumber < item.ExtendedProperties.Length; groupNumber++)
//        {
//            Console.WriteLine(item.ExtendedProperties[groupNumber].Value);
//        }
//    }
//}



// Groups //
string jsonGroups = FetchFromApi.GetGroups(apiSettings);

// Testing Deserializing json to object
//var groupResponse = JsonSerializer.Deserialize<GroupsApiResponse>(jsonGroups);
//Console.WriteLine(groupResponse.ApiVersion);

//foreach (var item in groupResponse.Data.ParentGroups)
//{
//    Console.WriteLine($"\n{item.Name} {item.Code}\n");
//    for (int groupNumber = 0; groupNumber < item.Groups.Length; groupNumber++)
//    {
//        Console.WriteLine(item.Groups[groupNumber].IsGroupManager);
//    }
//}



// Staff //
string jsonStaffMembers = FetchFromApi.GetStaffMembers(apiSettings);

// Testing Deserializing json to object
var staffResponse = JsonSerializer.Deserialize<StaffMembersApiResponse>(jsonStaffMembers);

//foreach (var item in staffResponse.Data.StaffMembers)
//{
//    Console.WriteLine($"\n{item.FirstName} {item.LastName}\n");
//    for (int groupNumber = 0; groupNumber < item.Groups.Length; groupNumber++)
//    {
//        Console.WriteLine(item.Groups[groupNumber].IsGroupManager);
//    }
//}



await host.RunAsync();