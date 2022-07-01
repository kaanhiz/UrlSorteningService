using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlSorteningService.Models
{
    public class UrlSorteningModel
    {
        public int Id { get; protected set; }
        public string Url { get; protected set; }
        public string Domain { get; set; }
        public string UrlHash { get; protected set; }

        public UrlSorteningModel(Uri url, string domain, int id)
        {
            Url = url.ToString();
            Domain = domain;
            UrlHash = WebEncoders.Base64UrlEncode(BitConverter.GetBytes(id));
        }
    }
}
