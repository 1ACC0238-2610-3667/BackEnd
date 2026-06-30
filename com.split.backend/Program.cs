using com.split.backend.Bills.Application.Internal.CommandServices;
using com.split.backend.Bills.Application.Internal.QueryServices;
using com.split.backend.Bills.Domain.Repositories;
using com.split.backend.Bills.Domain.Services;
using com.split.backend.Bills.Infrastructure.Persistence.EFC.Repositories;
using com.split.backend.Households.Application.CommandServices;
using com.split.backend.Households.Application.QueryServices;
using com.split.backend.Households.Domain.Repositories;
using com.split.backend.Households.Domain.Services;
using com.split.backend.Households.Infrastructure.Persistence.EFC.Repositories;
using com.split.backend.IAM.Application.Internal.CommandServices;
using com.split.backend.IAM.Application.Internal.OutboundServices;
using com.split.backend.IAM.Application.Internal.QueryServices;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.IAM.Domain.Services;
using com.split.backend.IAM.Infrastructure.Hashing.BCrypt.Services;
using com.split.backend.IAM.Infrastructure.Persistence.EFC.Repositories;
using com.split.backend.IAM.Infrastructure.Pipeline.MiddleWare.Extensions;
using com.split.backend.IAM.Infrastructure.Tokens.JWT.Configuration;
using com.split.backend.IAM.Infrastructure.Tokens.JWT.Services;
using com.split.backend.Settings.Application.Internal.CommandServices;
using com.split.backend.Settings.Application.Internal.OutboundServices.ACL;
using com.split.backend.Settings.Application.Internal.QueryServices;
using com.split.backend.Settings.Domain.Repositories;
using com.split.backend.Settings.Domain.Services;
using com.split.backend.Settings.Infrastructure.Persistence.EFC.Repositories;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using com.split.backend.Shared.Interfaces.ASP.Configuration;
using com.split.backend.Bills.Application.ACL;
using com.split.backend.Bills.Interface.ACL;    
using com.split.backend.HouseholdMembers.Application.ACL;
using com.split.backend.HouseholdMembers.Application.Internal.CommandServices;
using com.split.backend.HouseholdMembers.Application.Internal.QueryServices;
using com.split.backend.HouseholdMembers.Domain.Repositories;
using com.split.backend.HouseholdMembers.Domain.Services;
using com.split.backend.HouseholdMembers.Infrastructure.Persistence.EFC.Repositories;
using com.split.backend.HouseholdMembers.Interface.ACL;
using com.split.backend.Invitations.Application.Internal.CommandServices;
using com.split.backend.Invitations.Application.Internal.QueryServices;
using com.split.backend.Invitations.Domain.Repositories;
using com.split.backend.Invitations.Domain.Services;
using com.split.backend.Invitations.Infrastructure.Persistence.EFC.Repositories;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using IUnitOfWork = com.split.backend.Shared.Domain.Repositories.IUnitOfWork;
using UnitOfWork = com.split.backend.Shared.Infrastructure.Persistence.EFC.Repositories.UnitOfWork;
using Microsoft.Data.Sqlite;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Text;
using com.split.backend.Contributions.Application.Internal.CommandServices;
using com.split.backend.Contributions.Application.Internal.QueryServices;
using com.split.backend.Contributions.Domain.Repositories;
using com.split.backend.Contributions.Domain.Services;
using com.split.backend.Contributions.Infrastructure.Persistence.EFC.Repositories;
using com.split.backend.MemberContributions.Application.Internal.CommandServices;
using com.split.backend.MemberContributions.Application.Internal.QueryServices;
using com.split.backend.MemberContributions.Domain.Repositories;
using com.split.backend.MemberContributions.Domain.Services;
using com.split.backend.MemberContributions.Infrastructure.Persistence.EFC.Repositories;
using com.split.backend.Shared.Infrastructure.Mediator.Cortex.Configuration;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));


// Add Cors Policy

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


/*SqliteConnection? sqliteConnection = new SqliteConnection("DataSource=:memory:");
sqliteConnection.Open();*/

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if(connectionString == null) throw new InvalidOperationException("Connection string not found.");


builder.Services.AddDbContext<AppDbContext>(options =>
{
    /*if (builder.Environment.IsProduction())
    {
        options.UseSqlite(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information);
    }*/
    if (builder.Environment.IsDevelopment() || builder.Environment.IsProduction())
    {
        options.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        )
        .LogTo(Console.WriteLine, LogLevel.Information);
    }
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var secret = builder.Configuration["TokenSettings:Secret"]
                 ?? throw new InvalidOperationException("TokenSettings:Secret configuration is missing.");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

//Learn more about configuring Swagger/OpenApi at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Com.Harmonix.API",
            Version = "v1",
            Description = "Harmonix API",
            TermsOfService = new Uri("https://github.com/harmonix"),
            Contact = new OpenApiContact
            {
                Name = "Harmonix S.A.C.",
                Email = "harmonix@gmail.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
    options.EnableAnnotations();
});

//Dependency Injection

//Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IAM Bounded Context Injection Configuration
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

builder.Services.AddScoped<IUserIncomeRepository, UserIncomeRepository>();
builder.Services.AddScoped<IUserIncomeCommandService, UserIncomeCommandServices>();
builder.Services.AddScoped<IUserIncomeQueryService, UserIncomeQueryServices>();


//MemberContribution BC Injection Configuration
builder.Services.AddScoped<IMemberContributionRepository, MemberContributionRepository>();
builder.Services.AddScoped<IMemberContributionCommandService, MemberContributionCommandServices>();
builder.Services.AddScoped<IMemberContributionQueryService, MemberContributionQueryServices>();


// Bills Bounded Context Injection Configuration 
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IBillCommandService, BillCommandService>();
builder.Services.AddScoped<IBillQueryService, BillQueryService>();
builder.Services.AddScoped<IBillsContextFacade, BillsContextFacade>();
// HouseHold Bounded Context Injection Configuration

builder.Services.AddScoped<IHouseHoldRepository, HouseHoldRepository>();
builder.Services.AddScoped<IHouseHoldCommandService, HouseHoldCommandService>();
builder.Services.AddScoped<IHouseHoldQueryService, HouseHoldQueryService>();

//Contribution BoundedContext Injection Configuration
builder.Services.AddScoped<IContributionRepository, ContributionRepository>();
builder.Services.AddScoped<IContributionCommandService, ContributionCommandService>();
builder.Services.AddScoped<IContributionQueryService, ContributionQueryService>();

//Income Allocation Bounded Context Injection Configuration
builder.Services.AddScoped<IIncomeAllocationRepository, IncomeAllocationRepository>();
builder.Services.AddScoped<IIncomeAllocationCommandService, IncomeAllocationCommandService>();
builder.Services.AddScoped<IIncomeAllocationQueryService, IncomeAllocationQueryService>();

// Settings Bounded Context Injection Configuration
builder.Services.AddScoped<ISettingsRepository, SettingsRepository>();
builder.Services.AddScoped<ISettingsCommandService, SettingsCommandService>();
builder.Services.AddScoped<ISettingsQueryService, SettingsQueryService>();
builder.Services.AddScoped<IExternalIamService, ExternalIamService>();

// HouseholdMembers Bounded Context Injection Configuration
builder.Services.AddScoped<IHouseholdMemberRepository, HouseholdMemberRepository>();
builder.Services.AddScoped<IHouseholdMemberCommandService, HouseholdMemberCommandService>();
builder.Services.AddScoped<IHouseholdMemberQueryService, HouseholdMemberQueryService>();

// Invitations Bounded Context
builder.Services.AddScoped<IInvitationRepository, InvitationRepository>();
builder.Services.AddScoped<IInvitationCommandService, InvitationCommandService>();
builder.Services.AddScoped<IInvitationQueryService, InvitationQueryService>();

// ACL Facades for HouseholdMembers
builder.Services.AddScoped<IHouseholdContextFacade, HouseholdContextFacade>();
builder.Services.AddScoped<IUserContextFacade, UserContextFacade>();

// TokenSettings Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();

//Mediator Configuration

//Add Mediator Injection Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

// Add Cortex Mediator for Event Handling
builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: [typeof(Program)], configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
    });

var app = builder.Build();

// Verify if the database exists and create it if it doesn't 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    if (app.Environment.IsDevelopment())
        context.Database.EnsureDeleted();
    
    context.Database.EnsureCreated();

}

//Configure the HTTP request pipeline
if (app.Environment.IsDevelopment() ||
    Environment.GetEnvironmentVariable("RENDER") == "true")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//Add Authorization Middleware to Pipeline
app.UseRouting();

//Apply CORS Policy
app.UseCors("AllowAllPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseRequestAuthorization();

if(app.Environment.IsDevelopment())
    app.UseHttpsRedirection();

app.MapControllers();

app.Run();
