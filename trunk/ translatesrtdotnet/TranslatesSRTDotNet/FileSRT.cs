using System;
using System.Collections.Generic;
using System.Text;

namespace TranslatesSRTDotNet
{
    class FileSRT : FileSubtile
    {
        /// <summary>
        /// The SubRip file format is "perhaps the most basic of all subtitle formats."[9] SubRip files are named with the extension .srt, and contain formatted plain text. The time format used is hours:minutes:seconds,milliseconds. The decimal separator used is the comma, since the program was written in France. The line break used is often the CR+LF pair. Subtitles are numbered sequentially, starting at 1.
        /// Subtitle number
        /// Start time --> End time
        /// Text of subtitle (one or more lines)
        /// Blank line[10][9]
        /// </summary>
        /// <param name="argLine"></param>
        /// <returns></returns>
        protected override string TranslateLine(string argLine)
        {
            throw new NotImplementedException();
        }
    }
}
