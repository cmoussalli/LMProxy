using CMouss.IdentityFramework;
using MLProxy.Components;
using MLProxy.DAL;
using App = MLProxy.Components.App;

namespace MLProxy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.MapControllers();



            IDFManager.Configure(new IDFManagerConfig
            {
                DatabaseType = DatabaseType.SQLite,
                //DBConnectionString = "Server=projcetbravossql.database.windows.net;Database=ProjectBravos;User Id=BravosAdmin;Password=BPl@yer23212;",
                DBConnectionString = "Data Source=database.db;",
                DefaultListPageSize = 25,
                DBLifeCycle = DBLifeCycle.Both,
                IsActiveByDefault = true,
                IsLockedByDefault = false,
                DefaultTokenLifeTime = new LifeTime(365, 0, 0),
                AllowUserMultipleSessions = true,
                TokenEncryptionKey = "Medi@22222",
                TokenValidationMode = TokenValidationMode.DecryptOnly,
                AdministratorUserName = "admin",
                AdministratorPassword = "Medi@22222",
                AdministratorRoleName = "Administrators",
                IDGeneratorLevel = IDGeneratorLevel.Guid32

            });
            MLProxyContext db = new MLProxyContext();
            db.Database.EnsureCreated();
            IDFManager.Context = new();


            db.InsertMasterData();
            //HotFix for IDFManager
            IDFManager.RefreshIDFStorage();

            if (db.Roles.Count() == 1)
            {
                db.InsertDemoData();
            }



            app.Run();
        }
    }
}
