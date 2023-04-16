using Authentication.Core.Interfaces;
using Authentication.Core.MongoDb.Context;
using Authentication.Core.MongoDb.Repository;
using Authentication.Core.Options;
using Authentication.Services.FunctionalLocations;
using Authentication.Services.MineInformations;
using Authentication.Services.SiteInformations;
using Authentication.Services.Users;
using AuthenticationWebapi.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var appSettings = builder.Configuration.Get<AppSettingsOptions>();

// Add db services to the container.
builder.Services.AddSingleton<IMongoDbContext>(x => new MongoDbContext(appSettings.ApiConfiguration.MongoDb.ConnectionString, appSettings.ApiConfiguration.MongoDb.DatabaseName));
builder.Services.AddScoped(typeof(IMongoDbRepository<>), typeof(MongoDbRepository<>));

// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFunctionalLocationService, FunctionalLocationService>();
builder.Services.AddScoped<ISiteInformationService, SiteInformationService>();
builder.Services.AddScoped<IMineInformationService, MineInformationService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<ISessionService, SessionService>();

builder.Services.Configure<ApiConfigurationOptions>(builder.Configuration.GetSection(nameof(AppSettingsOptions.ApiConfiguration)));


//Configure authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = appSettings.ApiConfiguration.JwtToken.Issuer,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.ApiConfiguration.JwtToken.Secret)),
        ValidAudience = appSettings.ApiConfiguration.JwtToken.Audience,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(1)
    };
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(x=> x.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
