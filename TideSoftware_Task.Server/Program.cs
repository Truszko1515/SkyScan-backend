using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Repositiories;
using Business_Logic_Layer.Services;
using Business_Logic_Layer.Services.LocationServices;
using Core.External_Integrations.ExternalServices;
using Core.External_Integrations.ExternalServices.FlightRadarServices;
using Core.External_Integrations.Interfaces;
using Core.Options;
using Data_Accces_Layer;
using Data_Accces_Layer.Interfaces;
using Data_Accces_Layer.Repository;
using Microsoft.EntityFrameworkCore;

namespace TideSoftware_Task.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMemoryCache();

            // IOptions pattern
            builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));
            builder.Services.Configure<ApiKeys>(builder.Configuration.GetSection(nameof(ApiKeys)));
            builder.Services.Configure<ExternalUrls>(builder.Configuration.GetSection("FlightRadarUrls"));
            builder.Services.Configure<FlightRadarApiOptions>(builder.Configuration.GetSection(nameof(FlightRadarApiOptions)));
            
            // Configuring database connection
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

                options.UseSqlServer(connectionString);
            });

            
            // Registering services
            builder.Services.AddTransient<IDatabaseHealthCheckService, DatabaseHealthCheckService>();
            builder.Services.AddTransient<IAddLocationService, AddLocationService>();
            builder.Services.AddTransient<IGetLocationService, GetLocationService>();
            builder.Services.AddTransient<IGetAllLocationsService, GetAllLocationsService>();
            builder.Services.AddTransient<IDeleteLocationService, DeleteLocationService>();

            // Registering database repository
            builder.Services.AddTransient<ILocationRepository, LocationRespository>();
            builder.Services.AddTransient<IFlightRepository, FlightsRepository>();

            // Registering HttpClients that are injected to services 
            builder.Services.AddHttpClient<IExternalLocationApiService, ExternalLocationApiService>((ServiceProvider, httpClient) =>
            {
                var configuration = ServiceProvider.GetRequiredService<IConfiguration>();
                var baseUrl = configuration.GetSection("GeoCodeUrls")
                                           .GetValue<string>("GeoCodeBaseUrl");

                httpClient.BaseAddress = new Uri(baseUrl);
            });
            builder.Services.AddHttpClient<IFlightPositionsService, FlightPositionsService>((ServiceProvider, httpClient) =>
            {
                var configuration = ServiceProvider.GetRequiredService<IConfiguration>();

                var baseUrl = configuration.GetSection("FlightRadarUrls")
                                           .GetValue<string>("BaseUrl");

                var endpointUrl = configuration.GetSection("FlightRadarUrls")
                                               .GetValue<string>("EndpointLivePositionFull");

                var BasicUserApiKey = configuration.GetSection("ApiKeys")
                                                 .GetValue<string>("FlightRadarApiKey");

                httpClient.BaseAddress = new Uri(baseUrl + endpointUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Accept-Version", "v1");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {BasicUserApiKey}");
            });
            builder.Services.AddHttpClient<IAirportService, AirportService>((ServiceProvider, httpClient) =>
            {
                var configuration = ServiceProvider.GetRequiredService<IConfiguration>();

                var baseUrl = configuration.GetSection("FlightRadarUrls")
                                           .GetValue<string>("BaseUrl");

                var endpointUrl = configuration.GetSection("FlightRadarUrls")
                                               .GetValue<string>("EndpointAirportsLight");

                var BasicUserApiKey = configuration.GetSection("ApiKeys")
                                                 .GetValue<string>("FlightRadarApiKey");

                httpClient.BaseAddress = new Uri(baseUrl + endpointUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Accept-Version", "v1");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {BasicUserApiKey}");
            });
            builder.Services.AddHttpClient<IAirlineService, AirlineService>((ServiceProvider, httpClient) =>
            {
                var configuration = ServiceProvider.GetRequiredService<IConfiguration>();

                var baseUrl = configuration.GetSection("FlightRadarUrls")
                                           .GetValue<string>("BaseUrl");

                var endpointUrl = configuration.GetSection("FlightRadarUrls")
                                               .GetValue<string>("EndpointAirlinesLight");

                var BasicUserApiKey = configuration.GetSection("ApiKeys")
                                                 .GetValue<string>("FlightRadarApiKey");

                httpClient.BaseAddress = new Uri(baseUrl + endpointUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Accept-Version", "v1");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {BasicUserApiKey}");
            });

            /*builder.Services.AddHttpClient<IFlightPositionsService, FlightPositionsSandboxService>((ServiceProvider, httpClient) =>
            {
                var configuration = ServiceProvider.GetRequiredService<IConfiguration>();

                var baseUrl = configuration.GetSection("FlightRadarUrls")
                                           .GetValue<string>("BaseUrlSandbox");

                var endpointUrl = configuration.GetSection("FlightRadarUrls")
                                               .GetValue<string>("EndpointLivePositionsLight");

                var sandboxApiKey = configuration.GetSection("ApiKeys")
                                                 .GetValue<string>("FlightRadarSandboxApiKey");

                httpClient.BaseAddress = new Uri(baseUrl + endpointUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Add("Accept-Version", "v1");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {sandboxApiKey}");
            });*/

            // Decorating services (Scrutor libray used)
            builder.Services.Decorate<IAddLocationService, AddLocationServiceWithExternalApi>();
            builder.Services.Decorate<IFlightPositionsService, CachedFlightPositionsService>();

            // Configuring CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("LocalHostPolicy",
                    policy =>
                    {
                        policy
                              .SetIsOriginAllowedToAllowWildcardSubdomains()
                              .WithOrigins("https://localhost:4200", "http://localhost:4200", "http://localhost:3000")
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowAnyHeader();
                    });
            });

            var app = builder.Build();


            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("LocalHostPolicy");

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
