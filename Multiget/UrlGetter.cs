using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Multiget
{
    internal class UrlGetter
    {
        static int filecounter = 0;
        private readonly IEnumerable<string> urls;

        public UrlGetter(IEnumerable<string> urls)
        {
            this.urls = urls;
        }

        private async Task GetSingleUrl(WebRequest req)
        {
            filecounter++;
            using (var outputStream = File.CreateText($"gtest-{filecounter:D3}.jpg"))
            using (var response = req.GetResponse())
            using (var webStream = response.GetResponseStream())
            {
                await webStream.CopyToAsync(outputStream.BaseStream);
            }
        }

        internal async Task GetAll()
        {
            foreach (var url in urls)
            {
                var request = WebRequest.Create(url);
                await GetSingleUrl(request);
            }
        }
    }
}