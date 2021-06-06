using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortener.Model {
    public class ShortURLDb : DbContext {
        public DbSet<ShortURL> ShortURLs { get; set; }

        public ShortURLDb(DbContextOptions options)
            : base(options) { }
    }
}
