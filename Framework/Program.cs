using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Framework;
using JsonUtilities;
using GameStateInventory;
using Blazored.Toast;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<JsonUtility>();

builder.Services.AddScoped<GameState>();

builder.Services.AddScoped<FrameworkItems.Items>();

// Notifications
builder.Services.AddBlazoredToast();

await builder.Build().RunAsync();
