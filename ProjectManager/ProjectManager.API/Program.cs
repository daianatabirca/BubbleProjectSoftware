using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using ProjectManager.API.FluentValidations;
using ProjectManager.DbContexts;
using ProjectManager.Repository.Repositories;
using ProjectManager.Services.Mappings;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/projectManager.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(ProjectRequestValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(ProjectRequestUpdateValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(ProjectRequestPatchValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(StatusRequestValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(RelationTypeRequestValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(ProjectObjectTypeRequestValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(ProjectObjectRequestValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(ProjectObjectRequestUpdateValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(ProjectObjectRequestPatchValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(CommentRequestValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(CommentRequestUpdateValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(CommentRequestPatchValidator).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

builder.Services.AddDbContext<ProjectManagerContext>(dbContextOptions => dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:ProjectManagerDBConnectionString"])); //registering with scoped lifetime

// Restul configurarilor...

builder.Services.AddScoped<IProjectManagerRepository, ProjectManagerRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IStatusService, StatusService>();

builder.Services.AddScoped<IRelationTypeRepository, RelationTypeRepository>();
builder.Services.AddScoped<IRelationTypeService, RelationTypeService>();

builder.Services.AddScoped<IProjectObjectTypeRepository, ProjectObjectTypeRepository>();
builder.Services.AddScoped<IProjectObjectTypeService, ProjectObjectTypeService>();

builder.Services.AddScoped<IProjectObjectRepository, ProjectObjectRepository>();
builder.Services.AddScoped<IProjectObjectService, ProjectObjectService>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<IProjectObjectHistoryRepository, ProjectObjectHistoryRepository>();
builder.Services.AddScoped<IProjectObjectHistoryService, ProjectObjectHistoryService>();

builder.Services.AddScoped<IProjectObjectRelationRepository, ProjectObjectRelationRepository>();
builder.Services.AddScoped<IProjectObjectRelationService, ProjectObjectRelationService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Midlewares

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); //adds endpoints for controller actions without specifying routes
});

app.Run();
