using System.Text.Json.Serialization;
using meuprimeirocrud_karol.Contracts.Repository.MovimentoArmazenamento;
using meuprimeirocrud_karol.Contracts.Repository.TipoSolo;
using meuprimeirocrud_karol.Middleware;
using meuprimeirocrud_karol.Repository.MovimentoArmazenamento;
using meuprimeirocrud_karol.Repository.TipoSolo;

namespace meuprimeirocrud_karol
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(options =>{options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());});
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IMovimentoArmazenamentoRepository, MovimentoArmazenamentoRepository>();
            builder.Services.AddScoped<ITipoSoloRepository, TipoSoloRepository>();
            builder.Services.AddLogging();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}