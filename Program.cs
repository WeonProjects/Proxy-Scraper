using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.IO;
using System;
using System.Threading;
using System.Diagnostics;
using ProxyScraper;

class Program
{

    // Gabriele Bologna'a yardımı için teşekkürler.

    public static ResourceSemaphore httpSemaphore, socks4Semaphore, socks5Semaphore;
    public static string httpProxies, socks4Proxies, socks5Proxies;
    public static Stopwatch httpStopwatch, socks4Stopwatch, socks5Stopwatch;

    public static int http, socks4, socks5, bitis;

    static void Main()
    {
        Console.Title = "Proxy Scraper v1.0 | by Weon";
        Console.WriteLine("[!] Proxyler çekiliyor, lütfen bekleyin.\n");

        httpSemaphore = new ResourceSemaphore();
        socks4Semaphore = new ResourceSemaphore();
        socks5Semaphore = new ResourceSemaphore();

        httpStopwatch = new Stopwatch();
        socks4Stopwatch = new Stopwatch();
        socks5Stopwatch = new Stopwatch();

        httpStopwatch.Start();
        socks4Stopwatch.Start();
        socks5Stopwatch.Start();

        System.Net.ServicePointManager.DefaultConnectionLimit = int.MaxValue;
        System.Net.ServicePointManager.MaxServicePoints = int.MaxValue;
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        new Thread(() => CheckHTTP()).Start();
        new Thread(() => CheckSOCKS4()).Start();
        new Thread(() => CheckSOCKS5()).Start();

        foreach (Tuple<string, string> tuple in ProxyList.httpRequests)
        {
            new Thread(() => GetHTTP(tuple.Item1, tuple.Item2)).Start();
        }

        foreach (Tuple<string, string> tuple in ProxyList.socks4Requests)
        {
            new Thread(() => GetSOCKS4(tuple.Item1, tuple.Item2)).Start();
        }

        foreach (Tuple<string, string> tuple in ProxyList.socks5Requests)
        {
            new Thread(() => GetSOCKS5(tuple.Item1, tuple.Item2)).Start();
        }

        while (bitis != 3)
        {
            Thread.Sleep(100);
        }

        Console.WriteLine("\n[!] Bitti, çıkmak için bir tuşa bas.");
        Console.ReadLine();
    }

    public static void CheckHTTP()
    {
        while (http != ProxyList.httpRequests.Count)
        {
            Thread.Sleep(100);
        }

        Thread.Sleep(100);
        WriteProxies("http.txt", httpProxies, "HTTP");
    }

    public static void CheckSOCKS4()
    {
        while (socks4 != ProxyList.socks4Requests.Count)
        {
            Thread.Sleep(100);
        }

        Thread.Sleep(100);
        WriteProxies("socks4.txt", socks4Proxies, "SOCKS4");
    }

    public static void CheckSOCKS5()
    {
        while (socks5 != ProxyList.socks5Requests.Count)
        {
            Thread.Sleep(100);
        }

        Thread.Sleep(100);
        WriteProxies("socks5.txt", socks5Proxies, "SOCKS5");
    }

    public static void WriteProxies(string fileName, string fileContent, string proxyType)
    {
        string newContent = "";
        int count = 0;

        foreach (string line in SplitToLines(fileContent))
        {
            string newLine = line.Replace(" ", "").Replace('\t'.ToString(), "");
            
            if (newLine == "")
            {
                continue;
            }

            if (newContent == "")
            {
                newContent = newLine;
            }
            else
            {
                newContent = newContent + "\r\n" + newLine;
            }

            count++;
        }

        try
        {
            System.IO.File.WriteAllText(fileName, newContent);
            string tookTime = "";

            if (proxyType.Equals("HTTP"))
            {
                httpStopwatch.Stop();
                tookTime = httpStopwatch.ElapsedMilliseconds.ToString();
            }
            else if (proxyType.Equals("SOCKS4"))
            {
                socks4Stopwatch.Stop();
                tookTime = socks4Stopwatch.ElapsedMilliseconds.ToString();
            }
            else if (proxyType.Equals("SOCKS5"))
            {
                socks5Stopwatch.Stop();
                tookTime = socks5Stopwatch.ElapsedMilliseconds.ToString();
            }

            Console.WriteLine("[!] " + count + " Adet " + proxyType + " proxy çekildi. '" + fileName + "' dosyası oluşturuldu. Ms: " + tookTime);
        }
        catch
        {
            Console.WriteLine("[!] Dosya kaydedilirken bir hata oluştu: '" + fileName + "'.");
        }

        Interlocked.Increment(ref bitis);
    }

    public static IEnumerable<string> SplitToLines(string input)
    {
        if (input == null)
        {
            yield break;
        }

        using (System.IO.StringReader reader = new System.IO.StringReader(input))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }

    public static void GetHTTP(string url, string host)
    {
        goHere: while (httpSemaphore.IsResourceNotAvailable())
        {
            Thread.Sleep(100);
        }

        if (httpSemaphore.IsResourceAvailable())
        {
            httpSemaphore.LockResource();

            if (httpProxies == "")
            {
                httpProxies = FetchResource(url, host);
            }
            else
            {
                httpProxies += Environment.NewLine + FetchResource(url, host);
            }

            httpSemaphore.UnlockResource();
        }
        else
        {
            goto goHere;
        }

        http++;
    }

    public static void GetSOCKS4(string url, string host)
    {
        goHere: while (socks4Semaphore.IsResourceNotAvailable())
        {
            Thread.Sleep(100);
        }

        if (socks4Semaphore.IsResourceAvailable())
        {
            socks4Semaphore.LockResource();

            if (socks4Proxies == "")
            {
                socks4Proxies = FetchResource(url, host);
            }
            else
            {
                socks4Proxies += Environment.NewLine + FetchResource(url, host);
            }

            socks4Semaphore.UnlockResource();
        }
        else
        {
            goto goHere;
        }

        socks4++;
    }

    public static void GetSOCKS5(string url, string host)
    {
        goHere: while (socks5Semaphore.IsResourceNotAvailable())
        {
            Thread.Sleep(100);
        }

        if (socks5Semaphore.IsResourceAvailable())
        {
            socks5Semaphore.LockResource();

            if (socks4Proxies == "")
            {
                socks5Proxies = FetchResource(url, host);
            }
            else
            {
                socks5Proxies += Environment.NewLine + FetchResource(url, host);
            }

            socks5Semaphore.UnlockResource();
        }
        else
        {
            goto goHere;
        }

        socks5++;
    }

    public static string FetchResource(string url, string host)
    {
        try
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Proxy = null;
            request.UseDefaultCredentials = false;
            request.AllowAutoRedirect = false;

            var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

            request.Method = "GET";

            var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
            {
                ["Host"] = host,
            });

            field.SetValue(request, headers);

            var response = request.GetResponse();
            bool isValid = false;
            string content = Encoding.UTF8.GetString(ReadFully(response.GetResponseStream()));

            response.Close();
            response.Dispose();

            return content;
        }
        catch
        {
            return "";
        }
    }

    public static byte[] ReadFully(Stream input)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            input.CopyTo(ms);
            return ms.ToArray();
        }
    }
}