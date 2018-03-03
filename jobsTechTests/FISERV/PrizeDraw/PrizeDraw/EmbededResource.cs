using System.IO;
using System.Reflection;

namespace PrizeDraw
{
    // note: this is a class I've crafted for my own pet project, and is not necessarly part of this challendge.
    public class EmbededResource
    {
        private readonly Assembly _assembly;
        private string _content;

        public EmbededResource(Assembly assembly)
        {
            _assembly = assembly;
        }

        public EmbededResource Get(string resourcePath)
        {
            var result = "";
            using (Stream stream = _assembly.GetManifestResourceStream(resourcePath))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            _content = result;

            return this;
        }

        public string Content => _content;
        public string[] ContentAsArray => _content.Split("\r\n");

    }
}
