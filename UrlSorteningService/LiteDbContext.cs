using LiteDB;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlSorteningService.LiteDb;

namespace UrlSorteningService
{
    public class LiteDbContext : ILiteDbContext
    {
        public LiteDatabase Database { get; }

        public LiteDbContext(IOptions<LiteDbOptions> options)
        {
            try
            {
                Database = new LiteDatabase(options.Value.DatabaseLocation);
            }catch
            {
               //TODO: a middleware exception handling will be added on the next version.
            }
        }
    }
}
