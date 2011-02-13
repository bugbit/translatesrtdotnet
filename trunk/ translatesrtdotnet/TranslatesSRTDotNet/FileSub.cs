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
using System.Text.RegularExpressions;

namespace TranslatesSRTDotNet
{
    class FileSub : FileSubtile
    {
        /// <summary>
        /// MicroDVD subtitle files have .sub filename extensions. These files may come along with a video file. Media player applications that support external subtitle files are responsible for locating correct .sub files.
        /// MicroDVD subtitle files consist of multiple lines, each defining a portion of the subtitle text that must be displayed between certain given video frames. The line syntax is:
        /// {start-frame}{stop-frame}Text
        /// For example, if "Hello!" is to be displayed during the first 25 frames of a digital video, clip or movie, the corresponding .sub file must contain the line:[1]
        /// Text multilines separados por |
        /// {0}{25}Hello!
        /// </summary>
        /// <param name="argLine"></param>
        /// <returns></returns>
        protected override string TranslateLine(string argLine)
        {
            Regex pRegEx = new Regex(@"({\d*}{\d*})(.*)");
            Match pMatch = pRegEx.Match(argLine);
            if (!pMatch.Success)
                return argLine;
            if (pMatch.Groups.Count != 3)
                return argLine;
            string[] pTexts = pMatch.Groups[2].Value.Split('|');
            string[] pTextsTrans = new string[pTexts.Length];
            for (int i = 0; i < pTexts.Length; i++)
                pTextsTrans[i] = Translate(pTexts[i]);
            string pTranslate = string.Join("|", pTextsTrans);
            return string.Format("{0}{1}", pMatch.Groups[1].Value, pTranslate);
        }

        public FileSub(string argReferrer) : base(argReferrer) { }
    }
}
