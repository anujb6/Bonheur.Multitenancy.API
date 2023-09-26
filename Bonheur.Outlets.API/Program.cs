using Bonheur.Outlets.Dataservice.Abstracts.Database;
using Bonheur.Outlets.Dataservice.Abstracts.Shops;
using Bonheur.Outlets.Dataservice.Abstracts.Tenants;
using Bonheur.Outlets.Dataservice.Abstracts.Users;
using Bonheur.Outlets.Dataservice.EntityData.Outlets;
using Bonheur.Outlets.Dataservice.EntityData.Tenants.Models;
using Bonheur.Outlets.Dataservice.EntityData.Tenants;
using Bonheur.Outlets.Dataservice.Helper;
using Bonheur.Outlets.Dataservice.Services;
using Bonheur.Outlets.Manager.Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Bonheur.Outlets.Dataservice.Services.TenantServices;
using Bonheur.Outlets.Manager.Managers.TenantManager;

var builder = WebApplication.CreateBuilder(args);

var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

builder.Services.AddDbContext<OutletsContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("CentralDatabase"), serverVersion, mysqlOptions =>
    {
        mysqlOptions.EnableRetryOnFailure();
    })
);

builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
    });
});

//Context factory
builder.Services.AddScoped<ITenantDbContextFactory, TenantDbContextFactory>();

//managers
builder.Services.AddTransient<ShopManager>();
builder.Services.AddTransient<UserManager>();
builder.Services.AddTransient<ServiceManager>();

//helper
builder.Services.AddTransient<IDatabaseHelper, DatabaseHelper>();
builder.Services.AddTransient<IShopsHelper, ShopsHelper>();

//dataservices
builder.Services.AddTransient<IBuisnesServiceHelper, BuisnesServices>();
builder.Services.AddTransient<IShopService, ShopService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IServiceRecords, ServiceRecords>();
builder.Services.AddTransient<IStaffService, StaffServices>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseCors("AllowOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
