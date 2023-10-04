using Fs;
using Fs.Entities;
using Fs.Interfaces;
using Fs.Repositories;
using Fs.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFileSystemService, FileSystemService>();
builder.Services.AddScoped<IStorageRepository, DiskStorageRepository>();
builder.Services.AddScoped<IMetaDataRepository, EfMetaDataRepository>();
builder.Services.AddScoped<IFileContentComparer, FileContentComparer>();

builder.Services.AddDbContext<CoreContext>();

// Versioning
builder.Services.AddApiVersioning(options => { options.ReportApiVersions = true; });
builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("App run in {StartEnv} on {Url}. BasePath: {BasePath}",  app.Environment.EnvironmentName, app.Configuration["DeployUrl"], Environment.GetEnvironmentVariable("ASPNETCORE_BASE_PATH") ?? "");
using (var scope = app.Services.CreateScope())
{
    await using var dbContext = scope.ServiceProvider.GetRequiredService<CoreContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.Run();
