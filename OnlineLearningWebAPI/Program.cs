using System.Text.Json;
using Microsoft.AspNetCore.OData;
using OnlineLearningWebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);
var allowedOrigins = builder.Configuration.GetValue<string>("CorsSettings:AllowedOrigins");

// Accept CORS API REACT
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowReactApp", policy =>
	{
		policy.WithOrigins("https://eduquest-web-fe.vercel.app")
			  .AllowAnyMethod()
			  .AllowAnyHeader()
			  .AllowCredentials();
	});
});

builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.IgnoreNullValues = true;
		options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
	});

builder.Services.AddControllers().AddOData(option => option.Filter().Select().OrderBy().Expand().EnableQueryFeatures().SetMaxTop(100));

//// Add services to the container.
//builder.Services.AddControllers().AddNewtonsoftJson(options =>
//    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Config DTO Scope
builder.Services.AddDtoScopeConfig(builder, builder.Configuration);
// Config Service Scope
builder.Services.AddServiceScopeConfig();

// config Authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

// Add Authorize
builder.Services.AddAuthorizeConfig(builder.Configuration);

//Config Swagger UI
builder.Services.AddSwaggerConfig();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
		options.RoutePrefix = string.Empty;
	});
}

app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.UseRouting();
app.UseODataBatching();
// Middleware Authentication v� Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map Controllers
app.MapControllers();

app.Run();
