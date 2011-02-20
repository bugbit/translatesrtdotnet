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

namespace TranslatesSRTDotNet
{
    class FileSRT : FileSubtile
    {
        private enum EState
        {
            Number,Time,Texts
        }

        private EState mState = EState.Number;

        /// <summary>
        /// The SubRip file format is "perhaps the most basic of all subtitle formats."[9] SubRip files are named with the extension .srt, and 
        /// contain formatted plain text. The time format used is hours:minutes:seconds,milliseconds. The decimal separator used is the comma, 
        /// since the program was written in France. The line break used is often the CR+LF pair. Subtitles are numbered sequentially, 
        /// starting at 1.
        /// Subtitle number
        /// Start time --> End time
        /// Text of subtitle (one or more lines)
        /// Blank line[10][9]
        /// </summary>
        /// <param name="argLine"></param>
        /// <returns></returns>
        protected override string TranslateLine(string argLine)
        {
            switch (mState)
            {
                case EState.Number:
                    mState = EState.Time;
                    break;
                case EState.Time:
                    mState = EState.Texts;
                    break;
                case EState.Texts:
                    if (string.IsNullOrEmpty(argLine))
                        mState = EState.Number;
                    else
                        return Translate(argLine);
                    break;
            }

            return argLine;
        }

        public FileSRT(string argReferrer) : base(argReferrer) { }
    }
}
