using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using O2morny.Application.Common.Interfaces.Identity;
using O2morny.Application.Common.Interfaces.Persistence;
using O2morny.Application.Common.Interfaces.Services;
using O2morny.Application.Common.Mapping;
using O2morny.Infrastructure.FileSystem;
using O2morny.Infrastructure.Identity;
using O2morny.Infrastructure.Persistence;
using O2morny.Infrastructure.Persistence.Identity;
using O2morny.Infrastructure.Services;
using O2morny.Infrastructure.Settings;
using System.Text;

namespace O2morny.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Directories
            DirectoryInitializer.EnsureUploadDirectoriesExist();
            #endregion

            #region HttpContext
            services.AddHttpContextAccessor();
            #endregion

            #region DB Context & Identity
            services.AddDbContext<O2mornyContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("SqlConnection")
                );
            });
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = false;

                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Password.RequireDigit = false;

                options.Password.RequireLowercase = false;

                options.Password.RequireUppercase = false;

                options.Password.RequireNonAlphanumeric = false;

                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<O2mornyContext>()
            .AddDefaultTokenProviders();
            #endregion

            #region HangFire
            //services.AddHangfire(config => config
            //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseRecommendedSerializerSettings()
            //    .UseSqlServerStorage(configuration.GetConnectionString("SqlConnection"), new SqlServerStorageOptions
            //    {
            //        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //        QueuePollInterval = TimeSpan.Zero,
            //        UseRecommendedIsolationLevel = true,
            //        DisableGlobalLocks = true
            //    }));

            //services.AddHangfireServer();
            #endregion

            #region AutoMapper
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            var configExpression = new MapperConfigurationExpression();
            configExpression.AddProfile(typeof(MappingProfile));

            var mapperConfig = new MapperConfiguration(configExpression, loggerFactory);
            IMapper mapper = new Mapper(mapperConfig);

            services.AddSingleton(mapper);
            #endregion

            #region AuthenticationBuilder
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"])
                    ),
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddAuthorization();
            #endregion

            #region SignalR
            services.AddSignalR();
            #endregion

            #region Swagger
            services.AddSwaggerGen();
            #endregion

            services.Configure<AdminSettings>(configuration.GetSection("AdminSettings"));
            services.AddScoped<IApplicationDbContext, O2mornyContext>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IChatNotifier, ChatNotifier>();
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IUtilitiesService, UtilitiesService>();
            services.AddHttpClient<IWhatsappService, WhatsappService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            return services;
        }
    }
}