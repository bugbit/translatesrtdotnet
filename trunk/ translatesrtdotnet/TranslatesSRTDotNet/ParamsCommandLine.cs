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
    struct ParamsCommandLine
    {
        public string FileInput;
        public string FileOutput;
        public string Referrer;
        public string From;
        public string To;
        public string Format;
        public Encoding Encoding;
        public bool Help;

        public ParamsCommandLine(string[] args)
            : this()
        {
            Help = false;
            for (int i = 0; i < args.Length; i++)
            {
                string pOptionStr = args[i];
                if (pOptionStr.StartsWith("-") || pOptionStr.StartsWith("--"))
                {
                    string pParamStr = string.Empty;
                    bool pNoArg = i + 1 == args.Length;
                    if (!pNoArg)
                        pParamStr = args[++i];
                    switch (pOptionStr.ToLower())
                    {
                        case "-r":
                        case "--referrer":
                            if (pNoArg)
                                throw new ParamException("Missing argument referrer");
                            Referrer = pParamStr;
                            break;
                        case "-f":
                        case "--from":
                            if (pNoArg)
                                throw new ParamException("Missing argument from");
                            From = pParamStr;
                            break;
                        case "-t":
                        case "--to":
                            if (pNoArg)
                                throw new ParamException("Missing argument to");
                            To = pParamStr;
                            break;
                        case "-a":
                        case "--format":
                            if (pNoArg)
                                throw new ParamException("Missing argument format");
                            Format = pParamStr;
                            break;
                        case "-e":
                        case "--encoding":
                            if (pNoArg)
                                throw new ParamException("Missing argument format");
                            int pCodePage;
                            if (int.TryParse(pParamStr, out pCodePage))
                            {
                                Encoding = Encoding.GetEncoding(pCodePage);
                            }
                            else
                            {
                                Encoding = Encoding.GetEncoding(pParamStr);
                            }
                            break;
                        case "-h":
                        case "--help":
                            Help = true;
                            break;
                        default:
                            throw new ParamException("Option {0} not known", pOptionStr);
                    }
                }
                else if (pOptionStr == "/?")
                    Help = true;
                else
                {
                    if (string.IsNullOrEmpty(FileInput))
                        FileInput = pOptionStr;
                    else if (string.IsNullOrEmpty(FileOutput))
                        FileOutput = pOptionStr;
                    else
                        throw new ParamException("too many parameters");
                }
            }
        }
    }
}
