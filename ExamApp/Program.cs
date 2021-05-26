//.NET 5.0
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using SQLite;

namespace ExamApp
{
    class Program
    {
        private static readonly string sqliteConnectionString = @"Logs.db";
        public static string date = "";
   
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Define date:");
                date = Console.ReadLine();
            }
            else date = args[0];
            if (date.Equals(""))
                throw new ArgumentException("No date argument");
            Console.WriteLine("Argument: " + date);
            Console.WriteLine("Sending HTTP request..");
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://www.dsdev.tech/logs/" + date);
            Console.WriteLine("Done.\nParsing HTTP response..");
            var logs = await httpClient.GetFromJsonAsync<DataM>(date);
            Console.WriteLine("Done.\nStarting sort..");
            logs.Sort();
            Console.WriteLine("Done\nInserting into db..");
            SQLiteConnection db = new SQLiteConnection(sqliteConnectionString, false);
            db.InsertAll(logs.Logs);
            db.Close();
            Console.WriteLine("Done.\nFinished all Jobs.\nPress Enter to exit");
            _ = Console.ReadLine();
            return;
        }
    }
}
