namespace CVImageMatcher.Core.Models
{
    public class Image
    {
        public int IndexEnd { get; internal set; }
        public int IndexStart { get; internal set; }
        public string LocalPath { get; internal set; }

        public Image Copy() {
            return new Image {
                IndexStart = IndexStart,
                IndexEnd = IndexEnd,
                LocalPath = LocalPath
            };
        }

    }
}
