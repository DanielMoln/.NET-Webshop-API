using DuncansbyAPI.lib.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebshopAPI.lib.Services;

namespace WebshopAPI
{
    public class Program
    {
        public static string CONNECTION_STRING { get; private set; }
        public static byte[] JWT_KEY { get; private set; }
        public static string Audience { get; private set; }
        public static string Issuer { get; private set; }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Reading JWT datas from appsettings.json
            CONNECTION_STRING = builder.Configuration.GetConnectionString("SQL");
            JWT_KEY = Encoding.UTF8.GetBytes(builder.Configuration["JWT:KEY"]);
            Audience = builder.Configuration["JWT:Audience"];
            Issuer = builder.Configuration["JWT:Issuer"];

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Default Entity Model Property Validator (etc. FromBody is empty or not)
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Configuring Authentication & Authorization
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(a =>
                {
                    a.RequireHttpsMetadata = false;
                    a.SaveToken = true;
                    a.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        IssuerSigningKey = new SymmetricSecurityKey(JWT_KEY),
                        ValidAudience = Audience,
                        ValidIssuer = Issuer
                    };
                });
            builder.Services.AddAuthorization(a =>
            {
                a.AddPolicy("User", b => b.RequireClaim(ClaimTypes.Role, "1"));
                a.AddPolicy("Admin", b => b.RequireClaim(ClaimTypes.Role, "2"));
            });

            // Dependency Injections
            builder.Services.AddScoped<UserManagerService>();
            builder.Services.AddScoped<RoleManagerService>();
            builder.Services.AddScoped<ProductManagerService>();
            builder.Services.AddScoped<OrdersManagerService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}