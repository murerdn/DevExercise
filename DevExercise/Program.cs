using CommandLine;
using DevExercise.Models;
using DevExercise.Services;
using DevExercise.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DevExercise
{
    internal class Program
    {
        public class Options
        {
            [Option('r', "ReleaseType", Required = true, HelpText = "Sets the release type.")]
            public string ReleaseType { get; set; }

            [Option('f', "FilePath", Required = true, HelpText = "Sets the path of config file.")]
            public string FilePath { get; set; }
        }

        static void Main(string[] args)
        {
            var builder = new HostBuilder().ConfigureServices((contex, services) =>
            {
                services.AddScoped<ILogger>(s => s.GetService<ILogger<JsonHandler>>());
                services.AddScoped<ILogger>(s => s.GetService<ILogger<ProjectDetailsService>>());
                services.AddScoped<IJsonHandler, JsonHandler>();
                services.AddScoped<IProjectDetailsService, ProjectDetailsService>();
            }).UseConsoleLifetime();

            var app = builder.Build();
            var service = app.Services.GetService<IProjectDetailsService>();

            try
            {
                Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
                    {
                        Enum.TryParse(o.ReleaseType, out ReleaseType releaseType);
                        service.UpdateProjectDetailsVersion(releaseType, o.FilePath);
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, "Unexpected error.");
            }
        }
    }
}
