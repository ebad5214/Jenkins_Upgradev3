using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Pi.Math;
using Pi.Runtime;
using PowerArgs;
using System;
using System.IO;

var arguments = Args.Parse<Arguments>(args);
switch (arguments.Mode)
{
    case RunMode.Web:
        Console.WriteLine("Running in web mode...");
        CreateHostBuilder(args).Build().Run();
        break;

    case RunMode.Console:
        Console.WriteLine(GetPi(arguments.DecimalPlaces));
        break;

    case RunMode.File:
        File.WriteAllText(arguments.OutputPath, GetPi(arguments.DecimalPlaces));
        Console.WriteLine($"Wrote pi to: {arguments.DecimalPlaces} dp; at: {arguments.OutputPath}");
        break;
}

string GetPi(int decimalPlaces)
{
    return MachinFormula.Calculate(decimalPlaces).ToString();
}

IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
