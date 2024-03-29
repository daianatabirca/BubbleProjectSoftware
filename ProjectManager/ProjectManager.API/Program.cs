using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using ProjectManager.API.FluentValidations;
using ProjectManager.DbContexts;
using ProjectManager.Repository.Repositories;
using ProjectManager.Services.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProjectRequestValidator>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<StatusRequestValidator>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RelationTypeRequestValidator>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProjectObjectTypeRequestValidator>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProjectObjectRequestValidator>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProjectObjectRequestUpdateValidator>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProjectObjectRequestPatchValidator>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CommentsRequestValidator>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CommentsRequestUpdateValidator>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CommentsRequestPatchValidator>());

//builder.Services.AddControllers(options =>
//{
//    options.ReturnHttpNotAcceptable = true;
//}).AddNewtonsoftJson()
//.AddXmlDataContractSerializerFormatters();

//builder.Services.AddValidatorsFromAssemblyContaining<ProjectRequestValidator>();


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

builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();
builder.Services.AddScoped<ICommentsService, CommentsService>();

//builder.Services.AddTransient<IValidator<ProjectManager.DomainModel.Models.Requests.ProjectRequestDTO>, ProjectRequestValidator>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(ProjectProfile));

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

//app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); //adds endpoints for controller actions without specifying routes
});

app.Run();
