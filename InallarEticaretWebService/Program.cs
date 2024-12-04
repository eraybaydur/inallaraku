using InallarEticaretWebService.Helpers;
using InallarEticaretWebService.Repository.Interfaces;
using InallarEticaretWebService.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NotthomePortalApi;
using NotthomePortalApi.Models;
using System.Text;
using TurkPosWSTEST;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ITurkPosWSTESTAsyncRepository>(provider => new TurkPosWSTESTAsyncRepository(new TurkPosWSTESTSoapClient(TurkPosWSTESTSoapClient.EndpointConfiguration.TurkPos_x0020_WS_x0020_TESTSoap12)));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddCors(options =>
 {
     options.AddPolicy("AllowSpecificOrigin",
         builder =>
         {
             builder
             .WithOrigins("http://localhost:3000")
             .AllowAnyMethod()
             .AllowAnyHeader()
             .AllowCredentials();
         });
 });

builder.Services.AddAuthorization();

var app = builder.Build();

var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration.AddJsonFile("appsettings.json", false, true);
builder.Configuration.AddJsonFile($"appsettings.{enviroment}.json", true, true);
builder.Configuration.AddEnvironmentVariables();

// App Settings
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

Settings.ConnectionString = configuration.GetValue<string>("DefaultConnection");
Settings.FirmaNo = configuration.GetValue<string>("FirmaNo");
Settings.DonemNo = configuration.GetValue<string>("DonemNo");
Settings.ApiAuthKey = configuration.GetValue<string>("ApiAuthKey");
Settings.ProsesController = new ProsesController(configuration);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("AllowSpecificOrigin");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
