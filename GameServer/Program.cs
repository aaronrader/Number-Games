
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NumberSums.Classes;

namespace NumberSumsServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowAllOrigins = "_myAllowAllOrigins";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowAllOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin().AllowAnyHeader();
                                  });
            });

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(MyAllowAllOrigins);

            app.MapGet("/numSums", ([FromQuery] ushort? numRows, [FromQuery] ushort? numCols, [FromQuery] float? density) =>
            {
                var factory = new BoardFactory();
                Board gameBoard;

                try
                {
                    factory.NRows = numRows ?? 8;
                    factory.NColumns = numCols ?? 8;
                    factory.Density = density ?? 0.3f;
                    gameBoard = factory.Generate();
                } catch (Exception e) { return Results.BadRequest(e.Message); }

                return Results.Ok(gameBoard);
            })
            .WithName("GetNumSums")
            .WithOpenApi();

            app.Run();
        }
    }
}
