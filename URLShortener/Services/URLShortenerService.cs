using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using URLShortener.Model;

namespace URLShortener.Services {
    public class URLShortenerService {
        private readonly ShortURLDb _db;
        private readonly IHash _hash;

        public URLShortenerService(ShortURLDb db, IHash hash) {
            _db = db;
            _hash = hash;
        }

        public async Task<string> CreateShortURL(string url) {
            string token = _hash.GetHash(url);
            ShortURL shortURL = new ShortURL { FullURL = url, Token = token };

            await _db.ShortURLs.AddAsync(shortURL);
            await _db.SaveChangesAsync();

            return token;
        }

        public string GetShortURL(string url) {
            ShortURL shortURL = _db.ShortURLs.FirstOrDefault(u => u.FullURL == url);
            return shortURL?.Token;
        }

        public string GetFullURL(string token) {
            ShortURL url = _db.ShortURLs.FirstOrDefault(u => u.Token == token);
            return url?.FullURL;
        }
    }
}
