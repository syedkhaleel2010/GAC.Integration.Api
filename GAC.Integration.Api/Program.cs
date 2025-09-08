using GAC.Integration.Api.MiddleWare;
using GAC.Integration.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new UpperCaseGuidConverter());
    });



// JWT Authentication configuration
var jwtKey = builder.Configuration["Jwt:Key"] ?? "A@9v!p#Z2^bL$w8&Qx*G7m%T1s)R4u{EA@9v!p#Z2^bL$w8&Qx*G7m%T1s)R4u{E";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "GAC.Integration.ApiGAC.Integration.Api";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<DtoEntityMapper>());
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddDataServices();
// Fix for 'Configuration' issue


// Fix for 'AddCoresAllowAll' issue
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        {
       builder.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
   });
});
builder.Services.AddCors(options => options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Apply the CORS policy
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
