using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace Browser
{
    internal class Brows
    {
        public ChromeDriver driver;
        public Brows()
        {
            Random r = new Random();

            List<Proxy> proxies = new List<Proxy>();
            var proxy = new Proxy();
            proxy.HttpProxy = "103.146.170.252:83";
            proxies.Add(proxy);
            proxy = new Proxy();
            proxy.HttpProxy = "212.83.165.229:2019";
            proxies.Add(proxy);
            proxy = new Proxy();
            proxy.HttpProxy = "133.18.195.135:8080";
            proxies.Add(proxy);
            proxy = new Proxy();
            proxy.HttpProxy = "206.189.195.74:8080";
            proxies.Add(proxy);
            proxy = new Proxy();
            proxy.HttpProxy = "46.38.242.194:80";
            proxies.Add(proxy);

            var serv = ChromeDriverService.CreateDefaultService();
            serv.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("headless");
            options.AddArgument("incognito");
            options.AddArgument("--silent");
            options.Proxy = proxies[r.Next(0, 4)];
            driver = new ChromeDriver(serv, options);
        }
        public void setLink(string link)
        {
            driver.Url = link;
        }
        public void quit()
        {
            driver?.Quit();
        }
        public ChromeDriver getDriver()
        {
            return driver;
        }
    }
}
