using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlSorteningService.LiteDb;
using UrlSorteningService.Models;

namespace UrlSorteningService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlSorteningController : ControllerBase
    {
        private readonly ILiteDbUrlSorteningService _urlSorteningDbService;
        public UrlSorteningController(ILiteDbUrlSorteningService urlSorteningDbService)
        {
            _urlSorteningDbService = urlSorteningDbService;
        }
        [HttpGet("getShortUrl")]
        public ActionResult GetShortUrl(string Url)
        {
            string domain = $"{Request.Scheme}://{Request.Host}";

            if (!Uri.TryCreate(Url, UriKind.Absolute, out var validUri))
                return BadRequest("URL is invalid.");

            UrlSorteningModel urlSorteningModel = _urlSorteningDbService.GetByUrl(validUri.ToString());

            if (urlSorteningModel == null) {
                 urlSorteningModel = new UrlSorteningModel(validUri, domain, _urlSorteningDbService.GetUrlSorteningMaxId() + 1);
                _urlSorteningDbService.Insert(urlSorteningModel);
            }

            return Ok(domain + "/" + urlSorteningModel.UrlHash);
        }

        [HttpGet("getRedirectUrl")]
        public void GetRedirectUrl(string UrlHash)
        {
            if (!Uri.TryCreate(UrlHash, UriKind.Absolute, out var validUri))
                Response.WriteAsync("Url is not valid.");

            UrlSorteningModel urlSorteningModel = _urlSorteningDbService.GetByUrlHash(validUri.Scheme + "://" + validUri.Authority, validUri.Segments.LastOrDefault());

            if (urlSorteningModel == null)
                Response.WriteAsync("Url not found.");
            else
                Response.Redirect(urlSorteningModel.Url);
        }

        [HttpGet("generateCustomUrl")]
        public ActionResult GenerateCustomUrl(string Url, string customDomain)
        {
            if (!Uri.TryCreate(Url, UriKind.Absolute, out var validUri))
                return BadRequest("URL is invalid.");

            UrlSorteningModel urlSorteningModel = _urlSorteningDbService.GetByUrl(validUri.ToString());

            if (urlSorteningModel == null)
            {
                urlSorteningModel = new UrlSorteningModel(validUri, customDomain, _urlSorteningDbService.GetUrlSorteningMaxId() + 1);
                _urlSorteningDbService.Insert(urlSorteningModel);
            }
            else
            {
                urlSorteningModel.Domain = customDomain;
                _urlSorteningDbService.Update(urlSorteningModel);
            }

            return Ok(urlSorteningModel.Domain + "/" + urlSorteningModel.UrlHash);
        }
    }
}
