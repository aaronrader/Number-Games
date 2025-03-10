using GameServer.Classes;
using Microsoft.AspNetCore.Mvc;
using NumberSums.Classes;

namespace GameServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var CorsPolicy = "AllowLocalOrigins";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: CorsPolicy, policy =>
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

            app.UseCors(CorsPolicy);

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
                    if (numRows is not null) factory.NRows = (ushort)numRows;
                    if (numCols is not null) factory.NColumns = (ushort)numCols;
                    if (density is not null) factory.Density = (ushort)density;
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
