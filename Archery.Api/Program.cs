global using System;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;

using Archery.Model;
using Archery.Repository;

static async Task CreateDbAsync(IServiceProvider serviceProvider, IWebHostEnvironment env)
{
    if (!env.IsProduction())
    {
        await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ArcheryContext>();

        await dbContext.Database.MigrateAsync();
    }
}

var pcName = Environment.MachineName;
var conection = pcName.Contains("03302") ? "DB" :
                pcName == "P3643" ? "DBLukaPC" :
                "DBTobiPC";

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Configuration.GetRequiredSection(nameof(AppSettings));
builder.Services.Configure<AppSettings>(appSettings);

builder.Environment.WebRootPath = appSettings.GetValue<string>(nameof(builder.Environment.WebRootPath));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins(builder.Configuration.GetValue<string>("CorsPolicyHosts").Split(";"))
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddControllers()

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    .Services.AddEndpointsApiExplorer()

    .AddSwaggerGen()

    .AddDbContext<ArcheryContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString(conection), b => b.MigrationsAssembly("Archery.Api")))

    .AddScoped<UserRepository>()
    .AddScoped<ParcourRepository>()
    .AddScoped<TargetRepository>()
    .AddScoped<EventRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

await CreateDbAsync(app.Services, app.Environment);

await app.RunAsync();
