using System;
using System.Collections.Generic;
using System.Text;
using Google.API.Translate;

namespace TranslatesSRTDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            string Text = "This is a string to translate";
            Console.WriteLine("Before Translation:{0}", Text);
            Text = Google.API.Translate.Translator.Translate(Text, Google.API.Translate.Language.English,
            Google.API.Translate.Language.French);
            Console.WriteLine("Before Translation:{0}", Text);
            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to terminate Test");
            Console.ReadLine();
        }
    }
}
