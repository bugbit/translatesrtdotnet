using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Google.API.Translate;

namespace TranslatesSRTDotNet
{
    abstract class FileSubtile
    {
        private Language? mFrom = null;
        private Language mTo;
        private TranslateFormat mFormat = TranslateFormat.text;
        private Encoding mEncoding = null;
        private string mFileTranslate;

        protected string Translate(string argTexto)
        {
            string pText;
            if (mFrom.HasValue)
                pText = Translator.Translate(argTexto, mFrom.Value, mTo, mFormat);
            else
            {
                Language pFrom;
                pText = Translator.Translate(argTexto, mTo, mFormat, out pFrom);
                mFrom = pFrom;
            }

            return pText;
        }

        protected abstract string TranslateLine(string argLine);

        /// <summary>
        /// file.sub a file-es.sub
        /// </summary>
        /// <param name="argFile"></param>
        /// <returns></returns>
        public static string GetFileNameTranslate(string argFile,string argLangCode)
        {
            string pDir = Path.GetDirectoryName(argFile);
            string pName = Path.GetFileNameWithoutExtension(argFile);
            string pExt = Path.GetExtension(argFile);
            string pNameExtTranslate = string.Format("{0}-{1}{2}", pName, argLangCode, pExt);

            return Path.Combine(pDir, pNameExtTranslate);
        }

        public void ReadFile(string argFile)
        {
            using (StreamWriter pWriter = new StreamWriter(mFileTranslate))
            {
                string[] pLines;

                if (mEncoding == null)
                    pLines = File.ReadAllLines(argFile);
                else
                    pLines = File.ReadAllLines(argFile, mEncoding);
                foreach (string pLine in pLines)
                {
                    Console.WriteLine("{0} -> ",pLine);
                    string pLineTranslate = TranslateLine(pLine);
                    pWriter.WriteLine(pLineTranslate);
                    Console.WriteLine(pLineTranslate);
                }
            }
        }
    }
}
