
using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;
using E_Commerce.Web.Extensions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using ServiceAbstraction;
using Services;
using Shared.ErrorModels;
using System.Reflection.Metadata;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();           
            builder.Services.AddSwaggerService();
            builder.Services.AddInfrastructureServices(builder.Configuration);            
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices();
            #endregion

            var app = builder.Build();

            #region DataSeeding

            await app.SeedDataBaseAsync();
            #endregion

            #region Configure the HTTP request pipeline.

            app.UseCustomExceptionMiddleWare();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseAuthorization();


            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
