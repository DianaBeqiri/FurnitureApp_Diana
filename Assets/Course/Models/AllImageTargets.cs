using System;

namespace Assets.Course.Models
{
    [Serializable]
    public class AllImageTargets
    {
        public ImageTarget[] data;
        public Meta meta;
    }

    [Serializable]
    public class ImageTarget
    {
        public int id;
        public string name;
        public string description;
        public string color;
        public string image_url;
        public string model_url;
    }

    [Serializable]
    public class Meta
    {
        public string page;
    }
}


