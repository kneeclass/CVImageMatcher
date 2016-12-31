using System.Collections.Generic;
using System.IO;
using CVImageMatcher.Core.Models;

namespace CVImageMatcher.Core.Repositorys
{
    public class LocalImagesRepository : IImageRespository
    {
        public IEnumerable<Image> GetAll()
        {
            var dbImages = Directory.GetFiles("ImageDB");
            foreach(var imagePath in dbImages) {
                yield return new Image {
                    LocalPath = imagePath
                };
            }
            

        }
    }
}
