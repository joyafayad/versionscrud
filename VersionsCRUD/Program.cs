using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using NLog.Web;
using System.Reflection;
using VersionsCRUD.Middleware;
using VersionsCRUD.Models;

var builder = WebApplication.CreateBuilder(args);

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

logger.Info("start");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<postgresContext>(options =>
   options.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;"));

// Add services to the container.2
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



bool enableSwagger = builder.Configuration.GetValue<bool>("EnableSwagger");
if (enableSwagger)
{

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });
}



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

//bool useJwt = builder.Configuration.GetValue<bool>("UseJwt");
//if (useJwt)
//{

//    builder.Services.AddAuthentication(JwtBearerDefault.AuthenticationScheme)
//         .AddJwtBearer(options =>
//         {
//             options.TokenValidationParameters = new TokenValidationParameters
//             {
//                 ValidateIssuer = true,
//                 ValidateAudience = true,
//                 ValidateLifetime = true,
//                 ValidateIssuerSigningKey = true,
//                 ValidIssuer = Configuration["Jwt:Issuer"],
//                 ValidAudience = Configuration["Jwt:Audience"],
//                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]))
//             };
//         });
//}


app.UseAuthorization();

    app.MapControllers();

    app.MapRazorPages();

    app.Run(); 
