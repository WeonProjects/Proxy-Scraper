using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyScraper
{
    public static class ProxyList
    {

        public static List<Tuple<string, string>> httpRequests = new List<Tuple<string, string>>()
    {
        new Tuple<string, string>("https://api.proxyscrape.com/?request=displayproxies&proxytype=http", "api.proxyscrape.com"),
        new Tuple<string, string>("https://www.proxy-list.download/api/v1/get?type=http", "proxy-list.download"),
        new Tuple<string, string>("https://www.proxyscan.io/download?type=http", "proxyscan.io"),
        new Tuple<string, string>("https://api.openproxylist.xyz/http.txt", "api.openproxylist.xyz"),
        new Tuple<string, string>("https://raw.githubusercontent.com/TheSpeedX/PROXY-List/master/http.txt", "raw.githubusercontent.com"),
        new Tuple<string, string>("https://raw.githubusercontent.com/ShiftyTR/Proxy-List/master/http.txt", "raw.githubusercontent.com"),
        new Tuple<string, string>("https://raw.githubusercontent.com/jetkai/proxy-list/main/online-proxies/txt/proxies-http.txt", "raw.githubusercontent.com"),
        new Tuple<string, string>("https://raw.githubusercontent.com/mertguvencli/http-proxy-list/main/proxy-list/data.txt", "raw.githubusercontent.com"),
    };

        public static List<Tuple<string, string>> socks4Requests = new List<Tuple<string, string>>()
    {
        new Tuple<string, string>("https://api.proxyscrape.com/?request=displayproxies&proxytype=socks4", "api.proxyscrape.com"),
        new Tuple<string, string>("https://raw.githubusercontent.com/jetkai/proxy-list/main/online-proxies/txt/proxies-socks4.txt", "raw.githubusercontent.com"),
        new Tuple<string, string>("https://www.proxy-list.download/api/v1/get?type=socks4", "proxy-list.download"),
        new Tuple<string, string>("https://www.proxyscan.io/download?type=socks4", "proxyscan.io"),
        new Tuple<string, string>("https://api.openproxylist.xyz/socks4.txt", "api.openproxylist.xyz"),
        new Tuple<string, string>("https://raw.githubusercontent.com/ShiftyTR/Proxy-List/master/socks4.txt", "raw.githubusercontent.com"),
        new Tuple<string, string>("https://raw.githubusercontent.com/TheSpeedX/PROXY-List/master/socks4.txt", "raw.githubusercontent.com"),
    };

        public static List<Tuple<string, string>> socks5Requests = new List<Tuple<string, string>>()
    {
        new Tuple<string, string>("https://api.proxyscrape.com/?request=displayproxies&proxytype=socks5", "api.proxyscrape.com"),
        new Tuple<string, string>("https://www.proxy-list.download/api/v1/get?type=socks5", "proxy-list.download"),
        new Tuple<string, string>("https://www.proxyscan.io/download?type=socks5", "proxyscan.io"),
        new Tuple<string, string>("https://raw.githubusercontent.com/ShiftyTR/Proxy-List/master/socks5.txt", "raw.githubusercontent.com"),
        new Tuple<string, string>("https://raw.githubusercontent.com/jetkai/proxy-list/main/online-proxies/txt/proxies-socks5.txt", "raw.githubusercontent.com"),
        new Tuple<string, string>("https://api.openproxylist.xyz/socks5.txt", "api.openproxylist.xyz"),
        new Tuple<string, string>("https://raw.githubusercontent.com/TheSpeedX/PROXY-List/master/socks5.txt", "raw.githubusercontent.com"),
    };

    }
}
