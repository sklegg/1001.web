using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Web1001;
using Web1001.Services;
using Web1001.Shared.Auth;
using Material.Blazor;
using Web1001.Shared.Config;
using Web1001.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var config = new CustomConfiguration() {
    ApiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5050"
};

builder.Services.AddTransient<CookieHandler>();
builder.Services.AddHttpClient("WebAPI", client => client.BaseAddress = new Uri(config.ApiBaseUrl)).AddHttpMessageHandler<CookieHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebAPI"));

builder.Services
    .AddSingleton<GlobalState>()
    .AddSingleton<ICustomConfiguration, CustomConfiguration>()
    .AddScoped<ISongsService, SongsService>()
    .AddScoped<IReviewService, ReviewService>()
    .AddScoped<IUserService, UserService>().AddMBServices();

var app =  builder.Build();

await app.RunAsync();