using System.Text;
using BusinessObject.Services;
using DataAccess.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


// Add services to the container.
var jwtSettings = builder.Configuration.GetSection("Jwt");

// Cấu hình Jwt Bearer Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],  // Cấu hình Issuer từ appsettings.json
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"], // Cấu hình Audience từ appsettings.json
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero, // Tùy chỉnh thời gian chênh lệch khi hết hạn token (thường là 5 phút, để bằng 0 sẽ không có thời gian chênh lệch)
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"])) // Khoá bí mật được dùng để ký JWT
        };
    });


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<TokenService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
