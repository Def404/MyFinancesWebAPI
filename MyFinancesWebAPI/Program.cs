using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyFinancesWebAPI;
using MyFinancesWebAPI.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MyFinancesContext>();
builder.Services.AddSingleton(AuthOptions.GetSymmetricSecurityKey());
builder.Services.AddControllers();
//builder.Services.AddAuthorization();

const string jwtSchemeName = "JwtBearer";

builder.Services
	.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = jwtSchemeName;
		options.DefaultChallengeScheme = jwtSchemeName;
	})
	.AddJwtBearer(jwtSchemeName,jwtBearerOptions => 
	{
		jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidIssuer = AuthOptions.ISSUER,
		ValidateAudience = true,
		ValidAudience = AuthOptions.AUDIENCE,
		ValidateLifetime = true,
		IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
		ValidateIssuerSigningKey = true,
		
		ClockSkew = TimeSpan.FromSeconds(30)
	};
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});
//app.MapControllers();

app.Run();