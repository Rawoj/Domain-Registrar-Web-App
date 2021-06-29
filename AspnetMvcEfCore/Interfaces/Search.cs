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
using System.Text.RegularExpressions;
using DomainRegistrarWebApp.Database;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainRegistrarWebApp.Models.Search
{

    public class Search
    {
        private readonly ApplicationDbContext _db;
        private readonly string _apiKey;

        private const string apiUrl = @"https://domain-availability.whoisxmlapi.com/api/";
        private const string apiVersion = "v1";
        private const string outputFormat = "JSON";
        public async Task<DomainInfo> CheckAvailability(string domainName)
        {
            if (!ValidateName(domainName))
            {
                return new DomainInfo()
                {
                    DomainName = "",
                    DomainAvailability = "Incorrect domain name"
                };
            }

            var result = await CheckAvailabilityLocal(domainName);
            // Already in local db, therefore it's taken
            if(result.DomainAvailability == "UNAVAILABLE")
            {
                return result;
            }
            string apiCall = BuildApiCall(domainName);

            Root resultResponse;
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(apiCall.ToString());
            string apiResponse = await response.Content.ReadAsStringAsync();
            resultResponse = JsonConvert.DeserializeObject<Root>(apiResponse);
            if (resultResponse.DomainInfo.DomainAvailability == "UNAVAILABLE")
            {
                result.DomainAvailability = resultResponse.DomainInfo.DomainAvailability;
            }           

            return result;

        }

        private static bool ValidateName(string domainName)
        {
            Regex rx = new (@"^((?!-)[A-Za-z0-9-]{1,63}(?<!-)\\.)+[A-Za-z]{2,6}$");
            MatchCollection matches = rx.Matches(domainName);
            return matches.Any();
        }

        private string BuildApiCall(string domainName)
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

            return apiCall.ToString();
        }

        /*
         * Since we're not really buying domain
         *  whoisxmlapi will still show them as available.
         *  To fix this, we're gonna check local database first
        */
        private async Task<DomainInfo> CheckAvailabilityLocal(string domainName)
        {
            var matches = await _db.BoughtDomains.FindAsync(new BoughtDomain()
            {
                Name = domainName
            });


            string availability = "UNAVAILABLE";
            if(matches == null)
            {
                availability = "AVAILABLE";
            }

            var domainInfo = new DomainInfo()
            {
                DomainName = domainName,
                DomainAvailability = availability
            };

            return domainInfo;
        }


        public Search(string apiKey, ApplicationDbContext db)
        {
            _apiKey = apiKey;
            _db = db;
        }
    }
}
