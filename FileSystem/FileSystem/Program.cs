using FileSystem.ConfigurationSettings;
using FileSystem.Core.Dto;
using FileSystem.Core.Implementation;
using FileSystem.Core.Interface;
using FileSystem.Logger;
using Serilog;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var isDevelopment = environment == Environments.Development;
IConfiguration config = ConfigurationSettings.GetConfig(isDevelopment);
LogSetting.SetUpSerilog(config);

try
{

    Log.Information("Application is starting...");
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;

    // Add services to the container.
    builder.Services.AddSingleton(Log.Logger);
    builder.Services.AddScoped<IFolderService, FolderService>();
    builder.Services.AddScoped<IFileService, FileService>();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(typeof(FileSystemProfile));
    var app = builder.Build();
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
catch(Exception ex)
{
    Log.Fatal(ex.Message, ex.StackTrace, "The application failed to start correctly");
}
finally
{
    Log.CloseAndFlush();
}

