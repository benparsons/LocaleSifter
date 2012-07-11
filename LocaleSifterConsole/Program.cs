using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocaleSifter;

namespace LocaleSifterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            LocaleSifter.LocaleSifter localSifter = new LocaleSifter.LocaleSifter();
            string english = @"C:\dev\smartFE\761\Core\Resources\QuodResource.resx";
            string italian = @"C:\dev\smartFE\761\Core\Resources\QuodResource.it.resx";

            localSifter.LoadResxFile(english);
            localSifter.LoadResxFile(italian);
            Dictionary<string, string> englishResults = localSifter.LocaleContent[english];
            foreach (var item in englishResults)
            {
                Console.WriteLine(item.Key + "\t" + item.Value);
            }
            Console.ReadKey();
            Console.WriteLine("==============================================");
            Dictionary<string, List<string>> missingElements = localSifter.MissingElements();
            foreach (var locale in missingElements)
            {
                Console.WriteLine(locale.Key);
                foreach (string missingItem in locale.Value)
                {
                    Console.Write(missingItem + ", ");
                }
            }
            Console.ReadKey();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\"Quod Key\",\"EnglishTranslation\"");//,\"Italian\"");
            List<string> items = missingElements.ElementAt(0).Value;
            
            foreach (string missingItem in items)
            {
                sb.AppendLine("\"" + missingItem + "\",\"" + localSifter.LocaleContent[english][missingItem] + "\"");
            }
            //TextWriter tw = new StreamWriter("output.csv");
            //tw.WriteLine(sb);
            //tw.Close();
            Console.ReadKey();
            Console.WriteLine("==============================================");

            Dictionary<string, List<string>> matchedValues = localSifter.MatchedValues();
            foreach (var locale in matchedValues)
            {
                Console.WriteLine("MATCHED VALUES : " + locale.Key);
                foreach (string matchedValue in locale.Value)
                {
                    Console.Write(matchedValue + ", ");
                }
            }
            Console.WriteLine("============================================== MATCHED END");
            Console.ReadKey();
        }
    }
}
