using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Framework;
using JsonUtilities;
using GameStateInventory;
using Blazored.Toast;
using Framework.Slides;
using Framework.Keyboard;
using Framework.Mouse;
using Framework.Sound;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<JsonUtility>();

builder.Services.AddScoped<FrameworkItems.Items>();

// Notifications
builder.Services.AddBlazoredToast();

// First register both of those, then execute the initialization for both
// builder.Services.AddScoped<SlideService>();
builder.Services.AddScoped<SlideService>();
builder.Services.AddScoped<GameState>();


// // TODO: Find better solution to execute functions at startup
// // Apparently this only contains Services registered prior to its initialization, and it doesn't update
// var ServiceProvider = builder.Services.BuildServiceProvider();

// // Initialize the slides
// //! This does not seem to work as intended, don't know why though
// // await ServiceProvider.GetRequiredService<SlideService>().Init();

// // execute the async method to load the gamestate and items
// await ServiceProvider.GetRequiredService<GameState>().LoadGameStateAndItemsAsync();
// // builder.Sexrvices.AddScoped(sp => {new GameState(sp.GetRequiredService<JsonUtility>(), sp.GetRequiredService<FrameworkItems.Items>())});

builder.Services.AddScoped<KeyboardService>();
builder.Services.AddScoped<MouseService>();

builder.Services.AddScoped<SoundService>();

await builder.Build().RunAsync();