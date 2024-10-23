using Microsoft.EntityFrameworkCore;
using PortalMVCCore.DAL.DB;
using PortalMVCCore.DAL.Repositories.Interfaces;
using PortalMVCCore.DAL.Repositories;
using Microsoft.Extensions.FileProviders;

namespace PortalMVCCore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            builder.Services.AddScoped<IHomeRepository, HomeRepository>();
            builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            builder.Services.AddScoped<IProgramasRepository, ProgramasRepository>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //faz com que o retorno json das querys venhan com o nome dos campos de acordo com o case formado
            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //Recurso para forçar o asp net core a acessar arquivos estáticos(por exemplo .js)
            //Por padrão, o projeto te força a colocar todos os .js na wwwroot porem os arquivos de grids ficariam separados da view
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(app.Environment.ContentRootPath, "Views")),
                RequestPath = "/Views"
            });

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
