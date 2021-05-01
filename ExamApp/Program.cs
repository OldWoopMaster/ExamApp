//.NET 5.0
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.IO;
using SQLite;

namespace ExamApp
{
    class Program
    {
        private static readonly string sqliteConnectionString = @"Logs.db";
        
        private static string GetData(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = @"application/json;charset=""utf-8""";
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Sending HTTP request..");
            string data = GetData("http://www.dsdev.tech/logs/20210103");
            Console.WriteLine("Done.\nParsing HTTP response..");
            DataM logs = new DataM();
            logs = JsonSerializer.Deserialize<DataM>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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
