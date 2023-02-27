using Depot.Database;
using Depot.Helpers;
using Depot.Models.Opts;
using Depot.Models.Users;
using Depot.Services.Users;
using Depot.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Depot.Helpers.Mapper;
using Depot.Repositories.Departments;
using Depot.Repositories.Employees;
using Depot.Repositories.Entities;
using Depot.Repositories.Entities.EntityManufactures;
using Depot.Repositories.Entities.EntityModels;
using Depot.Repositories.Entities.EntityTypes;
using Depot.Repositories.Requisitions;
using Depot.Repositories.Requisitions.RequisitionStatuses;
using Depot.Repositories.Transactions;
using Depot.Repositories.Users;
using Depot.Services.Departments;
using Depot.Services.Employees;
using Depot.Services.Entities;
using Depot.Services.Entities.EntitiesManufactures;
using Depot.Services.Entities.EntitiesTypes;
using Depot.Services.Entities.EntityModels;
using Depot.Services.Requisitions;
using Depot.Services.Requisitions.RequisitionStatuses;
using Depot.Services.Transactions;

// TODO Функционал оператора
// TODO Изменение функционала сотрудника чтобы создавалась заявка
// TODO Когда пользователь резервирует оборудование, фронт создает заявку, а оператор потом резервирует оборудование

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true);
builder.Configuration.AddEnvironmentVariables();

builder.Services
    .AddDbContext<DepotDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDataBase"), npgsql => npgsql.MigrationsAssembly($"{nameof(Depot)}")));

builder.Services.AddIdentity<User, Role>(opts =>
{
    opts.Password.RequireDigit = false;
    opts.Password.RequiredLength = 1;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequiredUniqueChars = 0;
    opts.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<DepotDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        var jwtOptions = new JwtOptions();
        builder.Configuration.GetSection(nameof(JwtOptions)).Bind(jwtOptions);
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,
            ValidIssuer = jwtOptions.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
        };
    });

builder.Services.AddAuthorization(PolicyHelper.AddDepotPolicies);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        var securityScheme = new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        };
        options.AddSecurityDefinition("Bearer", securityScheme);
        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        };
        options.AddSecurityRequirement(securityRequirement);

        options.OperationFilter<SwaggerDefaultValues>();
});

builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddCors();

builder.Services.AddAutoMapper(typeof(AutoMappingProfile).Assembly);

builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
builder.Services.AddScoped<IEntitiesRepository, EntitiesRepository>();
builder.Services.AddScoped<IEntityManufacturesRepository, EntityManufacturesRepository>();
builder.Services.AddScoped<IEntityModelsRepository, EntityModelsRepository>();
builder.Services.AddScoped<IEntityTypesRepository, EntityTypesRepository>();
builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();
builder.Services.AddScoped<ITransactionTypesRepository, TransactionTypesRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IRequisitionStatusesRepository, RequisitionStatusesRepository>();
builder.Services.AddScoped<IRequisitionsRepository, RequisitionsRepository>();

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEntitiesService, EntitiesService>();
builder.Services.AddScoped<IEntityManufactureService, EntityManufactureService>();
builder.Services.AddScoped<IEntityTypesService, EntityTypesService>();
builder.Services.AddScoped<IEntityModelsService, EntityModelsService>();
builder.Services.AddScoped<ITransactionsService, TransactionsService>();
builder.Services.AddScoped<ITransactionTypesService, TransactionTypesService>();
builder.Services.AddScoped<IRequisitionStatusesService, RequisitionStatusesService>();
builder.Services.AddScoped<IRequisitionsService, RequisitionsService>();
builder.Services.AddScoped<JwtHelper>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DepotDbContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

app.MapControllers();

app.Run();