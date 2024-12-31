using rehber.Models;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Repositories.Implementations;
using Services.Contracts;
using Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace rehber.Infrastructure.Extensions
{
    public static class ServiceExtension 
    {

        public static void ConfigureDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DataConnection"));
                options.EnableSensitiveDataLogging(true);////////// Dikkat et
            }).AddScoped<DataContext>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<Users, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureSession(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache(); // Add in-memory caching for session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout süresi
                options.Cookie.Name = "Rehber.Session";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true; // GDPR uyumluluğu için
                options.Cookie.SameSite = SameSiteMode.Lax; // Cross-site request için
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void ConfigureRepositoryRegistration(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IEducationStatusRepository, EducationStatusRepository>();
            services.AddScoped<IReferanceRepository, ReferanceRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<ISurveyQuestionRepository, SurveyQuestionRepository>();
            services.AddScoped<ISurveyResponseRepository, SurveyResponseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INotesRepository, NotesRepository>();
            services.AddScoped<IRequestSuggestionsRepository, RequestSuggestionsRepository>();

        }

        public static void ConfigureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
			services.AddScoped<IEducationStatusService, EducationStatusManager>();
            services.AddScoped<IReferanceService, ReferanceManager>();
            services.AddScoped<ISurveyService, SurveyManager>();
            services.AddScoped<ISurveyQuestionService, SurveyQuestionManager>();
            services.AddScoped<ISurveyResponseService, SurveyResponseManager>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<INotesService, NotesManager>();
            services.AddScoped<IRequestSuggestionsService, RequestSuggestionsManager>();
        }

        public static void ConfigureApplicationCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Account/Login");
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;

                // Varsayılan olarak 1 saatlik oturum süresi
                int sessionTimeoutInHours = 1;

                // Kullanıcı oturum açtıysa, kullanıcı bilgilerini alalım
                options.Events.OnValidatePrincipal = async context =>
                {
                    var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<Users>>();
                    var user = await userManager.GetUserAsync(context.Principal);

                    // Kullanıcının özel session timeout süresi varsa, bunu kullan
                    if (user != null && user.SessionTimeoutInHours > 0)
                    {
                        sessionTimeoutInHours = user.SessionTimeoutInHours;
                    }

                    // Çerez süresi kullanıcıya özel session süresi olarak ayarlanır
                    context.Properties.ExpiresUtc = DateTimeOffset.UtcNow.AddHours(sessionTimeoutInHours);
                };

                // Çerez süresi ayarları
                options.ExpireTimeSpan = TimeSpan.FromHours(sessionTimeoutInHours);
                options.SlidingExpiration = true;  // Bu şekilde çerez süresi yenilenir

                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true; // GDPR uyumluluğu için
                options.AccessDeniedPath = new PathString("/Account/AccessDenied");
            });
        }


        public static void ConfigureRouting(this IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = false;
            });
        }
    }
}
