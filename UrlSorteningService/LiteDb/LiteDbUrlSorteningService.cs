using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlSorteningService.Models;

namespace UrlSorteningService.LiteDb
{
    public class LiteDbUrlSorteningService : ILiteDbUrlSorteningService
    {
        private readonly LiteDatabase _liteDb;

        public LiteDbUrlSorteningService(ILiteDbContext liteDbContext)
        {
            _liteDb = liteDbContext.Database;
        }

        public int GetUrlSorteningMaxId()
        {
            var links = _liteDb.GetCollection<UrlSorteningModel>("UrlSortening")
                            .FindAll();
            if (!links.Any())
                return 0;
            else 
                return links.LastOrDefault().Id;
        }

        public UrlSorteningModel GetByUrl(string url)
        {
            return _liteDb.GetCollection<UrlSorteningModel>("UrlSortening")
                .Find(x => x.Url == url).FirstOrDefault();
        }

        public UrlSorteningModel GetByUrlHash(string domain, string urlHash)
        {
            return _liteDb.GetCollection<UrlSorteningModel>("UrlSortening")
                .Find(x => x.Domain == domain && x.UrlHash == urlHash).FirstOrDefault();
        }

        public int Insert(UrlSorteningModel model)
        {
            return _liteDb.GetCollection<UrlSorteningModel>("UrlSortening")
                .Insert(model);
        }

        public bool Update(UrlSorteningModel model)
        {
            return _liteDb.GetCollection<UrlSorteningModel>("UrlSortening")
                .Update(model);
        }
    }
}
