using GameServer.Classes;
using Microsoft.AspNetCore.Mvc;
using NumberSums.Classes;
using Minesweeper.Classes;

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
                    new() {Name = "Minesweeper", Endpoint = "/minesweeper"}
                };
                return Results.Ok(gameList);
            });

            app.MapGet("/numSums", ([FromQuery] ushort? numRows, [FromQuery] ushort? numCols, [FromQuery] float? density) =>
            {
                var factory = new NumberSums.Classes.BoardFactory();
                NumberSums.Classes.Board gameBoard;

                try
                {
                    if (numRows is not null) factory.NRows = (ushort)numRows;
                    if (numCols is not null) factory.NColumns = (ushort)numCols;
                    if (density is not null) factory.Density = (float)density;
                    gameBoard = factory.Generate();
                } catch (Exception e) { return Results.BadRequest(e.Message); }

                return Results.Ok(gameBoard);
            });

            app.MapGet("/minesweeper", ([FromQuery] ushort? numRows, [FromQuery] ushort? numCols, [FromQuery] float? density) =>
            {
                var factory = new Minesweeper.Classes.BoardFactory();
                Minesweeper.Classes.Board gameBoard;

                try
                {
                    if (numRows is not null) factory.NRows = (ushort)numRows;
                    if (numCols is not null) factory.NColumns = (ushort)numCols;
                    if (density is not null) factory.Density = (float)density;
                    gameBoard = factory.Generate();
                } catch (Exception e) { return Results.BadRequest(e.Message); }

                return Results.Ok(gameBoard);
            });

            app.Run();
        }
    }
}
