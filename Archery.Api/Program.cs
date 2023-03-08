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

var builder = WebApplication.CreateBuilder(args);

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
        options.UseSqlite(builder.Configuration.GetConnectionString("DB"), b => b.MigrationsAssembly("Archery.Api")))

    .AddScoped<UserRepository>()
    .AddScoped<ParcourRepository>();

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
