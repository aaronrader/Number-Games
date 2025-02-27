
using System.Runtime.CompilerServices;
using System.Text.Json;
using GameServer.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using NumberSums.Classes;

namespace NumberSumsServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowLocalOrigins = "_myAllowLocalOrigins";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowLocalOrigins,
                                  policy =>
                                  {
                                      policy.SetIsOriginAllowed(origin => new Uri(origin).IsLoopback).AllowAnyHeader();
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

            app.UseCors(MyAllowLocalOrigins);

            app.MapGet("/games", () =>
            {
                var gameList = new GameInfo[]
                {
                    new() {Name = "Number Sums", Endpoint = "/numSums"},
                    //new() {Name = "Sudoku", Endpoint = "/sudoku"}
                };
                return Results.Ok(gameList);
            })
            .WithName("GetGames")
            .WithOpenApi();

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
