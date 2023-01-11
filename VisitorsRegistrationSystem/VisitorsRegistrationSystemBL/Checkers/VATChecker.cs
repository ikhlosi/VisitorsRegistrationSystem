using Castle.Components.DictionaryAdapter.Xml;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorsRegistrationSystemBL.Exceptions;

namespace VisitorsRegistrationSystemBL.Checkers {
    public static class VATChecker {
        public static bool IsValid(string vat) {
            string url = $"https://controleerbtwnummer.eu/api/validate/{vat}.json";
            var client = new RestClient(url);
            var request = new RestRequest();
            var response = client.Get(request);
            if (response.IsSuccessStatusCode) {
                bool valid = (bool)JObject.Parse(response.Content)["valid"];
                return valid;
            } else {
                throw new VATcheckerException(response.ErrorMessage);
            }
        }
    }
}
