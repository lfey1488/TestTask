using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        // Проверка подключения к MySQL
        try
        {
            using var connection = new MySqlConnection("Server=localhost;Database=testtask;Uid=appuser;Pwd=appuserpass;");
            connection.Open();
            Debug.WriteLine("Подключение к базе данных успешно!");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Ошибка подключения к базе данных: {ex.Message}");
            return;
        }
        var serviceProvider = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddMySql5()
                .WithGlobalConnectionString("Server=localhost;Database=testtask;Uid=appuser;Pwd=appuserpass;")
                .ScanIn(typeof(Program).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);

        using var scope = serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}