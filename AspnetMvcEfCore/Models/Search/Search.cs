using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DomainRegistrarWebApp.Models;
using System.Net.Http;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainRegistrarWebApp.Models.Search
{

    public class Search
    {
        private readonly string _apiKey;

        private const string apiUrl = @"https://domain-availability.whoisxmlapi.com/api/";
        private const string apiVersion = "v1";
        private const string outputFormat = "JSON";
        public async Task<DomainInfo> CheckAvailability(string domainName)
        {

            StringBuilder apiCall = new();
            apiCall.Append(apiUrl);
            apiCall.Append(apiVersion);
            apiCall.Append('?');
            apiCall.Append("apiKey=");
            apiCall.Append(_apiKey);
            apiCall.Append('&');
            apiCall.Append("domainName=");
            apiCall.Append(domainName);
            apiCall.Append('&');
            apiCall.Append("outputFormat=");
            apiCall.Append(outputFormat);

            Root resultResponse;
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(apiCall.ToString());
            string apiResponse = await response.Content.ReadAsStringAsync();
            resultResponse = JsonConvert.DeserializeObject<Root>(apiResponse);
            DomainInfo result = new()
            {
                domainName = resultResponse.DomainInfo.domainName,
                domainAvailability = resultResponse.DomainInfo.domainAvailability
            };

            return result;

            /*
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader sr = new(resStream);
            var jsonObject = JsonSerializer.Deserialize<SearchResult>(sr.ReadToEnd());
            */


        }

        public Search(string apiKey)
        {
            _apiKey = apiKey;
        }
    }
}
