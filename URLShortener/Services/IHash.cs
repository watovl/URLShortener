using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace URLShortener.Services {
    public interface IHash {
        string GetHash(string text);
    }
}
