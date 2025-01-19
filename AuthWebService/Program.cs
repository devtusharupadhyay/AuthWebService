
using AuthWebService.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthWebService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
       
        builder.AddServiceDefaults();
        builder.Services.AddAuthorization();

        //builder.Services.AddIdentityApiEndpoints<User>()
        //.AddEntityFrameworkStores<AppDbContext>();
        
        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables().Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        
        //var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
        //var connectionString = builder.Configuration["ConnectionStrings:AzureConnection"];

        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddIdentityCore<User>()
        .AddEntityFrameworkStores<AppDbContext>();
        //.AddApiEndpoints();

        var app = builder.Build();

        //app.MapIdentityApi<User>();



        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
