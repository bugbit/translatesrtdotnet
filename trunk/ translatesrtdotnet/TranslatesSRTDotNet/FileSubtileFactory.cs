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
    delegate FileSubtile FileSubtileFactoryHandler(string argReferrer);

    static class FileSubtileFactory
    {
        private static Dictionary<string, FileSubtileFactoryHandler> mFileSubtiles = new Dictionary<string, FileSubtileFactoryHandler>();

        static FileSubtileFactory()
        {
            mFileSubtiles.Add(".sub", (r) => { return new FileSub(r); });
        }

        static public FileSubtile GetFileSubtitle(string argExtension, string argReferrer)
        {
            FileSubtileFactoryHandler pEvent;

            if (!mFileSubtiles.TryGetValue(argExtension, out pEvent))
                throw new ParamException("Extension Error {0}", argExtension);

            return pEvent(argReferrer);
        }
    }
}
