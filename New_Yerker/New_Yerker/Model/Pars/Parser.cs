using New_Yerker.Model.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace New_Yerker.Model.Pars
{
    public class Parser
    {
        public string pathImages { get; set; }

        public async Task<Position> GetPosFromSite(string group)
        {
            var json = await GetDataAsync(group);
            var p = await ParsJson(json, group);
            return p;
        }

        private async Task<string> GetDataAsync(string group)
        {
            var request = (HttpWebRequest)WebRequest.Create($"https://api.newyorker.de/csp/products/public/product/" + group + "?country=ru");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Accept = "application/json";

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
        private async Task<Position> ParsJson(string json, string group)
        {
            JObject _json = JObject.Parse(json);

            var variant = _json["variants"].ToDictionary(e => e["product_id"])[group];
            List<Size> _size = new List<Size>();
            foreach (var item in variant["sizes"])
            {
                Size s = new Size
                {
                    BaraCode = item["bar_code"].ToString(),
                    SizeName = item["size_name"].ToString()
                };
                _size.Add(s);
            }

            return new Position
            {
                Name = _json["descriptions"].ToDictionary(e => e["language"])["RU"]["description"].ToString(),
                Group = _json["id"].ToString(),
                OldPrice = Convert.ToInt32(variant["original_price"].ToString()),
                NewPrice = Convert.ToInt32(variant["current_price"].ToString()),
                ImagaSource = variant["images"]
                    .ToList().First(e => e["type"].ToString() == "CUTOUT")["key"].ToString(),
                Sizes = _size
            };
        }
        private void DownloadImagesAsync(string source)
        {
            string site = @"https://nyblobstoreprod.blob.core.windows.net/product-images-public/";

            using(WebClient client = new WebClient())
            {
                if (!Directory.Exists(pathImages))
                    Directory.CreateDirectory(pathImages);

                client.DownloadFileAsync(new Uri(site + source), pathImages + source);
            }
        }
    }
}
