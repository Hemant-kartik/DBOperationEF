using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using DBOprationEFAPP.Data; // ✅ Include your Data namespace

namespace DBOprationEFAPP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ Corrected 'Services' (capital S)
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb")));

            // Add services to the container.
            builder.Services.AddControllers();

            // OpenAPI setup (if using Swashbuckle or NSwag)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); // this replaces AddOpenApi()

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
