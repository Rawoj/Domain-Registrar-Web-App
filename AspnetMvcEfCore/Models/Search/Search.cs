using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using DomainRegistrarWebApp.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainRegistrarWebApp.Models.Search
{

    public class Search : ISearch
    {
        private string apiKey;

        private void SetApiKey(string value)
        {
            apiKey = value;
        }

        readonly string apiUrl = @"https://domain-availability.whoisxmlapi.com/api/";
        readonly string apiVersion = "v1";
        readonly string outputFormat = "JSON";
        public ISearchResult CheckAvailability(string domainName)
        {        

            StringBuilder apiCall = new();
            apiCall.Append(apiUrl);
            apiCall.Append(apiVersion);
            apiCall.Append('?');
            apiCall.Append("apiKey=");
            apiCall.Append(apiKey);
            apiCall.Append('&');
            apiCall.Append("domainName=");
            apiCall.Append(domainName);
            apiCall.Append('&');
            apiCall.Append("outputFormat=");
            apiCall.Append(outputFormat);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiCall.ToString());
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader sr = new(resStream);
            var jsonObject = JsonSerializer.Deserialize<SearchResult>(sr.ReadToEnd());

            ISearchResult result = new SearchResult
            {
                DomainName = jsonObject.DomainName,
                DomainAvailability = jsonObject.DomainAvailability
            };
            return result;
        }

        public Search()
        {
            var x = new AppKeyConfig();
            SetApiKey(x.WhoIsXmlApiKey);
        }
    }
}
