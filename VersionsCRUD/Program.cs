using NLog.Web;
using NLog.Extensions.Logging;
using VersionsCRUD.Models;

var builder = WebApplication.CreateBuilder(args);

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

logger.Info("start");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddNLog();
});


var app = builder.Build();

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello world!");
//});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMyMiddleware();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
});

app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Run();