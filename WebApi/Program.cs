using Business.Services.Abstract;
using Business.Services.Concrete;
using DataAccess.Context;
using DataAccess.UnitOfWork;
using FluentValidation;
using Mapping;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Validation.GuestValidators;
using WebApi.Filters;
using WebApi.Middlewares;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddValidatorsFromAssembly(typeof(CreateGuestDtoValidator).Assembly);
            builder.Services.AddScoped<IGuestService, GuestService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped(typeof(ValidationFilter<>));
            var app = builder.Build();


            // Swagger middleware'lerini ekle
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
