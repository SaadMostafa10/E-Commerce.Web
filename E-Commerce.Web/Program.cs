
using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using ServiceAbstraction;
using Services;
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding,DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(Service.AssemblyReference).Assembly);
            //builder.Services.AddAutoMapper(typeof(ProductService).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            #endregion

            var app = builder.Build();


            #region DataSeeding
            using var Scoope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scoope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();

            #endregion
            #region Configure the HTTP request pipeline.

            // app.Use(async (RequestContext, NextMiddleWare) =>
            // {
            //     Console.WriteLine("Request Under Proccessing");
            //     await NextMiddleWare.Invoke();
            //     Console.WriteLine("Waiting Response");
            //     Console.WriteLine(RequestContext.Response.Body);
            // });
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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
