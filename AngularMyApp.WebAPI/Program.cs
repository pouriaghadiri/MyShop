using AngularMyApp.Core.Services.Implementations;
using AngularMyApp.Core.Services.Interfaces;
using AngularMyApp.Core.Utilities.Extentions.Connections;
using AngularMyApp.DataLayer.Entities.Account;
using AngularMyApp.DataLayer.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddApplicationDbContext(builder.Configuration);

        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ISliderService, SliderService>();
        builder.Services.AddScoped<IUserTokenService, UserTokenService>();
        builder.Services.AddScoped<IProductService, ProductService>();

        #region Authentication
        builder.Services.AddAuthentication( options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(option =>
        {
            option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = builder.Configuration.GetValue<string>("JwtConfig:issuer"),
                ValidAudience = builder.Configuration.GetValue<string>("JwtConfig:audience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtConfig:key"))),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
            };
        });
        #endregion

        #region CORS
        builder.Services.AddCors(option =>
            option.AddPolicy("CORSPolicy", builder =>
            {
                builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .Build();
            })
        );
        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors("CORSPolicy");
        
        app.UseAuthentication();

        app.UseAuthorization();
        
        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}