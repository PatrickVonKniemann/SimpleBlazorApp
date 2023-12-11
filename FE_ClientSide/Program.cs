using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FE_ClientSide;
using FE_ClientSide.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddHttpClient("FE_ClientSideAPI", client =>
    {
        client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
    })
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddAutoMapper(typeof(Core.Models.UserProfile).Assembly);
builder.Services.AddScoped<UserService>();

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Oidc", options.ProviderOptions);
});

await builder.Build().RunAsync();