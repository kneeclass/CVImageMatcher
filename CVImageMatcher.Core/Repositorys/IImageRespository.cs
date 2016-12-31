using System.Collections.Generic;
using CVImageMatcher.Core.Models;

namespace CVImageMatcher.Core.Repositorys
{
    interface IImageRespository {
        IEnumerable<Image> GetAll();
    }
}
