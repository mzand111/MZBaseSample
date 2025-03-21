
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MZBase.Infrastructure;
using MZBaseSample.Data.DataContext;
using MZBaseSample.Infrastructure;
using MZBaseSample.Services;
using System.Reflection;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace MZBaseSample.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string defualtConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.

            #region swagger
            builder.Services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chatbase.Backend", Version = "v1" });
                    c.AddEnumsWithValuesFixFilters(o =>
                    {
                        // add schema filter to fix enums (add 'x-enumNames' for NSwag or its alias from XEnumNamesAlias) in schema
                        o.ApplySchemaFilter = true;

                        // alias for replacing 'x-enumNames' in swagger document
                        o.XEnumNamesAlias = "x-enum-varnames";

                        // alias for replacing 'x-enumDescriptions' in swagger document
                        o.XEnumDescriptionsAlias = "x-enum-descriptions";

                        // add parameter filter to fix enums (add 'x-enumNames' for NSwag or its alias from XEnumNamesAlias) in schema parameters
                        o.ApplyParameterFilter = true;

                        // add document filter to fix enums displaying in swagger document
                        o.ApplyDocumentFilter = true;

                        // add descriptions from DescriptionAttribute or xml-comments to fix enums (add 'x-enumDescriptions' or its alias from XEnumDescriptionsAlias for schema extensions) for applied filters
                        o.IncludeDescriptions = true;

                        // add remarks for descriptions from xml-comments
                        o.IncludeXEnumRemarks = true;

                        // get descriptions from DescriptionAttribute then from xml-comments
                        o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;

                        // new line for enum values descriptions
                        // o.NewLine = Environment.NewLine;
                        o.NewLine = "\n";


                    });



                    try
                    {
                        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                        c.IncludeXmlComments(xmlPath);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                });
            #endregion

            #region business classes
            builder.Services.AddDbContext<SampleDBContext>(options =>
                {
                    options.UseSqlServer(defualtConnectionString);
                    options.UseAsyncSeeding(async (context, _, cancellationToken) =>
                     {
                         await SampleDBContext.SeedStaticDataAsync(context, cancellationToken);
                     });
                    options.UseSeeding((context, _) =>
                    {
                        SampleDBContext.SeedStaticData(context);
                    });
                }
            );
            builder.Services.AddScoped<IDateTimeProviderService, DateTimeProviderService>();
            //UoW
            builder.Services.AddScoped<ISampleDbUnitOfWork, SampleDbUnitOfWork>();
            builder.Services.AddScoped<SampleDbUnitOfWork, SampleDbUnitOfWork>();
            //Services
            builder.Services.AddScoped<CompanyStorageService, CompanyStorageService>();
            #endregion
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<SampleDBContext>();
                    await context.Database.MigrateAsync();


                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.EnableDeepLinking();
            });
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
