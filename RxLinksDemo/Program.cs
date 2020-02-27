using System;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RxLinksDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var links = await System.IO.File.ReadAllLinesAsync(@"E:\Projects\Data\urls.txt");

            var query = links.ToObservable();

            var sub=query.Subscribe(async (url) => {

                var client = new HttpClient();
                try
                {
                    Console.WriteLine($" {url} started --- ");
                    client.Timeout = TimeSpan.FromSeconds(100);

                    var content = await client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                    var pageSize = content.Content.Headers.ContentLength.Value;
                    Console.WriteLine($" --- {url}                    (Size : {pageSize}) [{Thread.CurrentThread.ManagedThreadId}]");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine($" {url}      (Size : {0}) [{Thread.CurrentThread.ManagedThreadId}] - ERR");
                }

            });

            


            Console.ReadLine();

        }
    }
}
