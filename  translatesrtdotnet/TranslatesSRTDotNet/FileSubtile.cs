// Translate Subtitle extension .srt y .sub for .net and Mono
//
//   Copyright (C) <2010>  <Oscar Hernández Bañó>

//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.

//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.

//   You should have received a copy of the GNU General Public License
//   along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using Google.API.Translate;

namespace TranslatesSRTDotNet
{
    abstract class FileSubtile
    {
        private string mFrom = null;

        public string From
        {
            get { return mFrom; }
            set { mFrom = value; }
        }
        private string mTo;

        public string To
        {
            get { return mTo; }
            set { mTo = value; }
        }
        private string mFormat = "text";

        public string Format
        {
            get { return mFormat; }
            set { mFormat = value; }
        }
        private Encoding mEncoding = null;

        public Encoding Encoding
        {
            get { return mEncoding; }
            set { mEncoding = value; }
        }
        private string mFileTranslate;

        public string FileTranslate
        {
            get { return mFileTranslate; }
            set { mFileTranslate = value; }
        }

        TranslateClient mClient = null;

        public FileSubtile(string argReferrer)
        {
            mClient = new TranslateClient(argReferrer);
        }

        protected string Translate(string argTexto)
        {
            string pText = string.Empty;
            int pNumExc = 1;

            for (; ; )
                try
                {
                    pText = mClient.Translate(argTexto, mFrom, mTo, mFormat);
                    break;
                }
                catch (Exception ex)
                {
                    if (pNumExc++ > 3)
                        throw ex;
                    System.Threading.Thread.Sleep(10);
                }

            return pText;
        }

        protected abstract string TranslateLine(string argLine);

        /// <summary>
        /// file.sub a file-es.sub
        /// </summary>
        /// <param name="argFile"></param>
        /// <returns></returns>
        public static string GetFileNameTranslate(string argFile, string argLangCode)
        {
            string pDir = Path.GetDirectoryName(argFile);
            string pName = Path.GetFileNameWithoutExtension(argFile);
            string pExt = Path.GetExtension(argFile);
            string pNameExtTranslate = string.Format("{0}-{1}{2}", pName, argLangCode, pExt);

            return Path.Combine(pDir, pNameExtTranslate);
        }

        private StreamWriter NewStreamWriter()
        {
            if (mEncoding == null)
                return new StreamWriter(mFileTranslate);
            return new StreamWriter(mFileTranslate, false, mEncoding);
        }

        public void TranslateFile(string argFile, IProcess argProcess)
        {
            using (StreamWriter pWriter = NewStreamWriter())
            {
                string[] pLines;

                argProcess.Init(string.Format("Read File {0}", argFile), null, null);
                if (mEncoding == null)
                    pLines = File.ReadAllLines(argFile);
                else
                    pLines = File.ReadAllLines(argFile, mEncoding);
                argProcess.Done();
                argProcess.Init("Translate", 0, pLines.Length - 1);
                argProcess.Position = 0;
                foreach (string pLine in pLines)
                {
                    Debug.WriteLine("{0} -> ", pLine);
                    string pLineTranslate = TranslateLine(pLine);
                    pWriter.WriteLine(pLineTranslate);
                    Debug.WriteLine(pLineTranslate);
                    argProcess.Position++;
                }
            }
        }
    }
}
