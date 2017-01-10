using System;
using System.Collections.Generic;
using CVImageMatcher.Core.Models;
using CVImageMatcher.Core.Repositorys;
using Emgu.CV;
using Emgu.CV.Flann;
using Emgu.CV.Util;

namespace CVImageMatcher.Core
{
    public class IndexBuilder {


        public void BuildIndex()
        {
            var imageRepo = new LocalImagesRepository();
            var images = imageRepo.GetAll();

            var descriptors = new List<Mat>();
            var indexMappning = new Dictionary<int, Image>();

            var startIndex = 0;

            foreach (var image in images) {
                var descriptor = DescriptorManager.ExtractDescriptor(image);
                if (descriptor == null) continue;
                //descriptor.IsEnabledDispose = false;
                descriptors.Add(descriptor);
                image.IndexStart = startIndex;
                image.IndexEnd = startIndex + descriptor.Size.Height - 1;

                for(var a = image.IndexStart; a < image.IndexEnd; a++) {
                    indexMappning.Add(a, image);
                }
                
                //set new startIndex
                startIndex += descriptor.Rows;
            }
            IndexContext.CurrentMappingIndex = indexMappning;
            var indexParams =  new LshIndexParamses(10,10,0);
            IndexContext.ConcatDescriptors = DescriptorManager.ConcatDescriptors(descriptors);
            IndexContext.CurrentFlannIndex = new Index(IndexContext.ConcatDescriptors, indexParams);
            
        }

    }
}
