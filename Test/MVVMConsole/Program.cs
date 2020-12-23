using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVVMConsole
{
    class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        /// <summary>Формирует поток байт данных</summary>
        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(data_url,HttpCompletionOption.ResponseHeadersRead);

            return await response.Content.ReadAsStreamAsync();
        }

        /// <summary>Разбивает поток на последовательность строк</summary>
        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStream().Result;
            using var data_reader = new StreamReader(data_stream);

            while(!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                yield return line;
            }
        }

        /// <summary>Получение необходимых данных - дат</summary>
        private static DateTime[] GetDates() => GetDataLines()
                                                .First()
                                                .Split(',')
                                                .Skip(4)
                                                .Select(s=> DateTime.Parse(s,CultureInfo.InvariantCulture))
                                                .ToArray();

        static void Main(string[] args)
        {
            //var client = new HttpClient();

            //var response = client.GetAsync(data_url).Result;
            //var csv_str = response.Content.ReadAsStringAsync().Result;

            //foreach (var data_line in GetDataLines())
            //    Console.WriteLine(data_line);

            var dates = GetDates();

            Console.WriteLine(string.Join("\r\n",dates));

            Console.ReadLine();
        }
    }
}
