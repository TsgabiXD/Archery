global using System;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using System.Text;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.IdentityModel.Tokens;

using Microsoft.OpenApi.Models;
using System.Security.Claims;

using Archery.Model;
using Archery.Repository;
using Archery.Api.Auth;

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
var connection = pcName.Contains("03302") ? "DB" :
                pcName == "P3643" ? "DBLukaPC" :
                pcName.Contains("AGVGCQH") ? "DBTobiPC" :
                pcName.Contains("F186T1U") ? "DBTobiPCDaheim" :
                pcName.Contains("336692") ? "DBTobiPCWork" :
                pcName.Contains("ROELZJNB") ? "DBJohnnyPCWork" :
                throw new Exception("No Specified PC!!!");

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

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers()

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    .Services.AddEndpointsApiExplorer()

    .AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new List<string>()
                }
            });
        })

    .AddDbContext<ArcheryContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString(connection), b => b.MigrationsAssembly("Archery.Api")))

    .AddScoped<UserRepository>()
    .AddScoped<ParcourRepository>()
    .AddScoped<TargetRepository>()
    .AddScoped<EventRepository>()
    .AddScoped<TokenService, TokenService>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                "sdl lajhödlakä-öasefGashaösiehfla.ishleuiagwkebfa,jksbngl.ailw.,jk.JKHL:KUJ:LJHBFALJUHÖ:OIh.igH:ILHG-ÖOJAÖ-POjöihR-GLAKENLGHABS;BDAJSERFILUABJRGJK:SDFN:YKLDNLXJ:FBN-Y:"
            )),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = tokenCtx =>
            {
                var tokenElements = tokenCtx.SecurityToken.ToString()?.Split("}.{")[1].Split(new string[] { "\",\"", "\":\"", "\":", "\"", "}" }, StringSplitOptions.None);

                if (tokenElements == null) throw new Exception("Something is wrong with your Token and its Claims");

                var name = tokenElements[Array.FindIndex(tokenElements, e => e == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name") + 1].ToString();
                var role = tokenElements[Array.FindIndex(tokenElements, e => e == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role") + 1].ToString();
                var uId = tokenElements[Array.FindIndex(tokenElements, e => e == "userId") + 1].ToString();

                if (name == null || role == null || uId == null)
                    throw new Exception("Something is wrong with your Token and its Claims");

                if (tokenCtx.HttpContext.User.Identity is ClaimsIdentity ctxIdentity)
                {
                    var validationClaimsPrincipal =
                        new ClaimsPrincipal(
                            new ClaimsIdentity(ctxIdentity,
                                ctxIdentity.Claims,
                                "Password",
                                ClaimTypes.Name,
                                ClaimTypes.Role)
                        );

                    (validationClaimsPrincipal?.Identity as ClaimsIdentity)?.AddClaims(
                        new List<Claim>(){
                            new (ClaimTypes.Role, role),
                            new ("name", name),
                            new ("userId", uId)
                        }
                    );

                    tokenCtx.HttpContext.User = validationClaimsPrincipal!;
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services
    .AddIdentityCore<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<ArcheryContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await CreateDbAsync(app.Services, app.Environment);

await app.RunAsync();
