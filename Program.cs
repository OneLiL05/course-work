using Supabase;
using trade_compas.Interfaces;
using trade_compas.Interfaces.Repositories;
using trade_compas.Interfaces.Helpers;
using trade_compas.Repositories;
using trade_compas.Utils;

var builder = WebApplication.CreateBuilder(args);

var supabase = new Client(
    "https://owkernseqxrqtoxjzyii.supabase.co",
    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im93a2VybnNlcXhycXRveGp6eWlpIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTUxODEyNDQsImV4cCI6MjAzMDc1NzI0NH0.7T3zHZbnWcs4vqAw86nQkVbALiR8sy11zXSxEzceZZc",
    new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true,
    });

await supabase.InitializeAsync();

builder.Services.AddScoped<Client>(_ => supabase);
builder.Services.AddScoped<IPathHelper, PathHelper>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();