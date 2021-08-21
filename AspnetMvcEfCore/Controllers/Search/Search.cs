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
using DomainRegistrarWebApp.Interfaces;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainRegistrarWebApp.Models.Search
{

    public class Search : ISearch
    {
        private readonly ApplicationDbContext _db;
        private readonly string _apiKey;

        private const string apiUrl = @"https://domain-availability.whoisxmlapi.com/api/";
        private const string apiVersion = "v1";
        private const string outputFormat = "JSON";
        public async Task<DomainInfo> CheckAvailability(string domainName)
        {
            if (!IsNameValid(domainName))
            {
                return new DomainInfo()
                {
                    DomainName = "",
                    DomainAvailability = "Incorrect domain name"
                };
            }
            /*
            *  Since we're not actually buying domains
            *  whoisxmlapi will still show bought ones as available.
            *  To fix this, we're gonna check local database first
            */
            var result = await CheckAvailabilityLocal(domainName);
            // Already in local db, therefore it's taken
            if(result.DomainAvailability == "UNAVAILABLE")
            {
                return new DomainInfo()
                {
                    DomainName = domainName,
                    DomainAvailability = result.DomainAvailability
                };
            }
            string apiCall = BuildApiCall(domainName);

            Root resultResponse;
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(apiCall.ToString());
            string apiResponse = await response.Content.ReadAsStringAsync();
            resultResponse = JsonConvert.DeserializeObject<Root>(apiResponse);
            return new DomainInfo()
            {
                DomainName = domainName,
                DomainAvailability = resultResponse.DomainInfo.DomainAvailability,
            };          
        }

        private static bool IsNameValid(string domainName)
        {
            return Uri.IsWellFormedUriString(domainName, UriKind.RelativeOrAbsolute);
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

       
        private async Task<DomainInfo> CheckAvailabilityLocal(string domainName)
        {
            IBoughtDomainsDataService dataService = new BoughtDomainsDataService(_db);
            var matches = await dataService.GetBoughtDomainAsync(new BoughtDomain()
            {
                Name = domainName
            });


            string availability;
            if (matches == null)
            {
                availability = "AVAILABLE";
            }
            else
            {
                availability = "UNAVAILABLE";
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
