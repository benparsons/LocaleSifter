using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LocaleSifter
{
    public class LocaleSifter
    {
        public Dictionary<string, Dictionary<string, string>> LocaleContent { get; set; }

        public LocaleSifter()
        {
            LocaleContent = new Dictionary<string, Dictionary<string, string>>();
        }

        public bool LoadResxFile(string path)
        {
            XDocument xDocument = XDocument.Load(path);
            List<XElement> elements = xDocument.Root.Elements("data").ToList();
            Dictionary<string, string> localElements = new Dictionary<string, string>();
            foreach (XElement element in elements)
            {
                localElements.Add(element.Attribute("name").Value, element.Element("value").Value);
            }
            LocaleContent.Add(path, localElements);
            return true;
        }

        public Dictionary<string, List<string>> MissingElements()
        {
            Dictionary<string, List<string>> missingElements = new Dictionary<string, List<string>>();
            
            for (int i = 1; i < LocaleContent.Count; i++)
            {
                Dictionary<string, string> translatedElements = LocaleContent.ElementAt(i).Value;
                List<string> missingElementsForLocale = new List<string>();
                foreach (string original in LocaleContent.ElementAt(0).Value.Keys)
                {
                    if (!translatedElements.ContainsKey(original))
                        missingElementsForLocale.Add(original);
                }
                missingElements.Add(LocaleContent.ElementAt(i).Key, missingElementsForLocale);
            }
            return missingElements;
        }
    }
}
