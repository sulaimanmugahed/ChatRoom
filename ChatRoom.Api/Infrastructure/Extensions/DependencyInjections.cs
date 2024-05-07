using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ChatRoom.Api.Contracts.Interfaces;
using ChatRoom.Api.Contracts.Wrappers;
using ChatRoom.Api.Domain.Models;
using ChatRoom.Api.Domain.Settings;
using ChatRoom.Api.Infrastructure.Data;
using ChatRoom.Api.Infrastructure.Services;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChatRoom.Api.Infrastructure.Extensions;

public static class DependencyInjections
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
	{
		// register dbcontext
		services.AddDbContext<ChatRoomDbContext>(options =>
		{
			options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
		});


		// register services
		services.AddTransient<IAccountService, AccountService>();
		services.AddScoped<IUserConnectionService, UserConnectionService>();
		services.AddScoped<IRoomService, RoomService>();
		services.AddScoped<IMessageService, MessageService>();
		services.AddScoped<ICurrentAuthUserService, CurrentAuthUserService>();
		services.AddScoped<IFollowService, FollowService>();

		services.Configure<JWTSettings>(configuration.GetSection(nameof(JWTSettings)));


		// register identity
		var identitySettings = configuration.GetSection(nameof(IdentitySettings)).Get<IdentitySettings>();
		services.AddIdentity<AppUser, AppRole>(options =>
		{

			options.SignIn.RequireConfirmedAccount = false;
			options.SignIn.RequireConfirmedEmail = false;
			options.User.RequireUniqueEmail = false;

			options.Password.RequireDigit = identitySettings.PasswordRequireDigit;
			options.Password.RequiredLength = identitySettings.PasswordRequiredLength;
			options.Password.RequireNonAlphanumeric = identitySettings.PasswordRequireNonAlphanumic;
			options.Password.RequireUppercase = identitySettings.PasswordRequireUppercase;
			options.Password.RequireLowercase = identitySettings.PasswordRequireLowercase;
		})
			   .AddEntityFrameworkStores<ChatRoomDbContext>()
			   .AddDefaultTokenProviders();


		// register jwt Bearer
		var jwtSettings = configuration.GetSection(nameof(JWTSettings)).Get<JWTSettings>();
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		})
			.AddJwtBearer(options =>
			{
				options.IncludeErrorDetails = true;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ClockSkew = TimeSpan.Zero,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtSettings.Issuer,
					ValidAudience = jwtSettings.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(jwtSettings.Key)
					),
				};

				options.Events = new JwtBearerEvents()
				{
					OnChallenge = context =>
					{
						context.HandleResponse();
						context.Response.StatusCode = 401;
						context.Response.ContentType = "application/json";
						var result = JsonSerializer.Serialize(new BaseResult(new Error(ErrorCode.AccessDenied, "You are not Authorized")));
						return context.Response.WriteAsync(result);
					},
					OnForbidden = context =>
					{
						context.Response.StatusCode = 403;
						context.Response.ContentType = "application/json";
						var result = JsonSerializer.Serialize(new BaseResult(new Error(ErrorCode.AccessDenied, "You are not authorized to access this resource")));
						return context.Response.WriteAsync(result);
					},
					OnTokenValidated = async context =>
					{
						var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<AppUser>>();
						var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
						if (claimsIdentity.Claims?.Any() != true)
							context.Fail("This token has no claims.");

						var securityStamp = claimsIdentity.FindFirst("AspNet.Identity.SecurityStamp");
						if (securityStamp is null)
							context.Fail("This token has no secuirty stamp");

						var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
						if (validatedUser == null)
							context.Fail("Token secuirty stamp is not valid.");
					},

				};
			});




		services.AddControllers().AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
		});


		// add public policy
		services.AddCors(x =>
		{
			x.AddPolicy("Any", b =>
			{
				b.AllowAnyOrigin();
				b.AllowAnyHeader();
				b.AllowAnyMethod();

			});
		});



		//Swagger
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(setup =>
		{
			setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
			});
			setup.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer",
							},
							Scheme = "Bearer",
							Name = "Bearer",
							In = ParameterLocation.Header,
						}, new List<string>()
					},
				});
		});

		services.AddSignalR();
		return services;
	}
}
