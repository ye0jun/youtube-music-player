using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Youtube_Master
{
    class Youtube
    {
        private string url = "https://www.googleapis.com/youtube/v3/";
        private string searchQuery = "search?q=";
        private string keyQuery = "&key=AIzaSyB-yKQr68f2CctB04cCNhhGZP62mEg3aHg";
        private string partQuery = "&part=snippet&maxResults=10";
        public delegate void SearchCallback(JArray result);

        public Youtube()
        {
        }

        public void Search(string search, SearchCallback callback)
        {
            var finalUrl = url + searchQuery + search + keyQuery + partQuery;
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                var data = wc.DownloadString(finalUrl);
                var json = JObject.Parse(data);
                var items = json["items"];
                var result = new JArray();
                foreach (JObject item in items)
                {
                    try
                    {
                        if(item["id"]["videoId"].ToString() != "null")
                        {
                            var myItem = new JObject();
                            myItem.Add("videoId", item["id"]["videoId"]);
                            myItem.Add("title", item["snippet"]["title"]);
                            string url = item["snippet"]["thumbnails"]["high"]["url"].ToString();
                            myItem.Add("thumbnails", url);
                            result.Add(myItem);
                        }
                    }
                    catch
                    {

                    }
                }
                callback(result);
            }
        }
    }
}
