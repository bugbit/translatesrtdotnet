using System;
using System.Collections.Generic;
using System.Text;

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
            throw new NotImplementedException();
        }
    }
}
