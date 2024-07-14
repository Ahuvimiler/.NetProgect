using AutoMapper;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using MODELS.Models;
using System.Globalization;
using BL.Interfaces;
using BL.Services;
using DAL.Data;
using DAL.Profilies; 

namespace Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(CultureInfo.InvariantCulture);
            });

            // ����� ������� ����� �������
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ����� AutoMapper ����� ����
            builder.Services.AddSingleton<IMapper>(provider =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    // ����� �������� ���
                    cfg.AddProfile<UserProfile>();
                    cfg.AddProfile<DonationProfile>();
                });
                return config.CreateMapper();
            });

            // ����� DbContext
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDataBase")));

            // ����� UserData ����� �������
            builder.Services.AddScoped<UserData>();
            builder.Services.AddScoped<DonationData>();


            // ����� IUserService �� ������ ��� UserService
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IDonationService, DonationService>();


            var app = builder.Build();

            // ����� ����� �� ����� HTTP
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