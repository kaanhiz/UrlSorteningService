using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlSorteningService.Models;

namespace UrlSorteningService.LiteDb
{
    public interface ILiteDbUrlSorteningService
    {
        int GetUrlSorteningMaxId();
        UrlSorteningModel GetByUrl(string url);
        UrlSorteningModel GetByUrlHash(string domain, string url);
        int Insert(UrlSorteningModel model);
        bool Update(UrlSorteningModel model);
    }
}
