using Microsoft.EntityFrameworkCore;
using PortalMVCCore.DAL.DB;
using PortalMVCCore.DAL.Repositories.Interfaces;
using PortalMVCCore.DAL.Repositories;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.Cookies;
using PortalMVCCore.BLL.Services;
using PortalMVCCore.BLL.Services.Interfaces;

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
            builder.Services.AddScoped<IClientesRepository, ClientesRepository>();

            builder.Services.AddScoped<IHomeService, HomeService>();
            builder.Services.AddScoped<IUsuariosService, UsuariosService>();
            builder.Services.AddScoped<IClientesService, ClientesService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Autenticação
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
            });

            //faz com que o retorno json das querys venhan com o nome dos campos de acordo com o case formado
            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true; // Tornar o cookie essencial para o funcionamento do site
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

            // Ativar sessão antes de UseEndpoints
            app.UseSession();

            //Autenticação
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
