
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using AutoMapper;
using E_commercial_Web_RESTAPI.Data;
using E_commercial_Web_RESTAPI.Interfaces;
using E_commercial_Web_RESTAPI.Models;
using E_commercial_Web_RESTAPI.Repositories;
using E_commercial_Web_RESTAPI.Repositories.Repository_Impl;
using E_commercial_Web_RESTAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using System.Configuration;
using Order = E_commercial_Web_RESTAPI.Models.Order;
using Product = E_commercial_Web_RESTAPI.Models.Product;
using Polly;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using E_commercial_Web_RESTAPI.Helpers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(option =>
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
            new string[]{}
        }
    });
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<ApplicationDBcontext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>(options => {

    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 7;

})
.AddEntityFrameworkStores<ApplicationDBcontext>();

builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
        )

    };
});


builder.Services.AddScoped<ICustomerRepository, CustomerRepository_Impl>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository_Impl>();
builder.Services.AddScoped<IStripePaymentService,StripePaymentService>();
builder.Services.AddScoped<IOrder, OrderService>();
//builder.Services.AddScoped<IRepository<Product>, Repository_Impl<Product>>();
builder.Services.AddScoped<IRepository<Order>, Repository_Impl<Order>>();
//builder.Services.AddScoped<ITokenService,TokenService_Impl>();
builder.Services.AddScoped<ICart,CartRepository_Impl>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped(typeof(IRepository<>), (typeof(Repository_Impl<>)));
builder.Services.AddAutoMapper(typeof(Program));



builder.Services.AddHttpClient();
builder.Services.AddControllers();




var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
   // var context = services.GetRequiredService<ApplicationDBcontext>();
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    //var userManager = services.GetRequiredService<UserManager<AppUser>>();

    try
    {
       var context = services.GetRequiredService<ApplicationDBcontext>();
        await context.Database.MigrateAsync();
        await Seed.SeedAsync(loggerFactory,services);
        

    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occured during migration");
    }

}
await app.RunAsync();
