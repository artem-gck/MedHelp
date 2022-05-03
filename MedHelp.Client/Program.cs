using Blazored.LocalStorage;
using IgniteUI.Blazor.Controls;
using MedHelp.Client;
using MedHelp.Client.Services;
using MedHelp.Client.Services.Logic;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpClient = new HttpClient()
{
    BaseAddress = new Uri($"https://localhost:{builder.Configuration["port"]}/api/")
};

httpClient.DefaultRequestHeaders.Accept.Clear();
httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IBasicService, BasicService>();
builder.Services.AddTransient<IDoctorService, DoctorService>();
builder.Services.AddTransient<IPatientService, PatientService>();

builder.Services.AddScoped(typeof(IIgniteUIBlazor), typeof(IgniteUIBlazor));
builder.Services.AddScoped(sp => httpClient);

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredLocalStorage(config =>
    config.JsonSerializerOptions.WriteIndented = true);

await builder.Build().RunAsync();
