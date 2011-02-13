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
using Google.API.Translate;

namespace TranslatesSRTDotNet
{
    class Program
    {
        static void ShowHelp()
        {
            Console.WriteLine("TranslatesSRTDotNet [Options] <file input> [file output]");
            Console.WriteLine();
            Console.WriteLine("file input\t\t\tfile for translate");
            Console.WriteLine("file output\t\t\tfile output translate. Default <file input>-<lang>.sub");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine();
            Console.WriteLine("-r,--referrer <url>\t\tyour http referrer header valid for Google.API.Translate. Default TranslatesSRTDotNet.exe.config see below for more detalls");
            Console.WriteLine("-f,--from <lang>\t\tThe language of the original text.");
            Console.WriteLine("-t,--to <lang>\t\t\tThe target language you want to translate to.");
            Console.WriteLine("-a,--format {txt|html}\t\tformat subtitles. Default:txt");
            Console.WriteLine("-e,--encoding <codepage|named>\tEncoding file. Default:utf-8");
            Console.WriteLine();
            Console.WriteLine("TranslatesSRTDotNet.exe.config:");
            Console.WriteLine("<applicationSettings>");
            Console.WriteLine("\t<TranslatesSRTDotNet.Properties.Settings>");
            Console.WriteLine("\t\t<setting name=\"Referrer\" serializeAs=\"String\">");
            Console.WriteLine("\t\t\t<value>your url</value>");
            Console.WriteLine("\t\t</setting>");
            Console.WriteLine("\t</TranslatesSRTDotNet.Properties.Settings>");
            Console.WriteLine("</applicationSettings>");
        }
        static FileSubtile CreateFileSubtile(ParamsCommandLine argParams)
        {
            if (string.IsNullOrEmpty(argParams.FileInput))
                throw new ParamException("missing input file");
            string pExtension = Path.GetExtension(argParams.FileInput);
            string pReferrer = argParams.Referrer;

            if (string.IsNullOrEmpty(pReferrer))
                pReferrer = Properties.Settings.Default.Referrer;
            if (string.IsNullOrEmpty(pReferrer))
                throw new ParamException("Missing Referrer");
            if (string.IsNullOrEmpty(argParams.To))
                throw new ParamException("Missing Language To");
            FileSubtile pFileSubtile = FileSubtileFactory.GetFileSubtitle(pExtension, pReferrer);
            if (!string.IsNullOrEmpty(argParams.FileOutput))
                pFileSubtile.FileTranslate = argParams.FileOutput;
            else
                pFileSubtile.FileTranslate = FileSubtile.GetFileNameTranslate(argParams.FileInput, argParams.To);
            if (!string.IsNullOrEmpty(argParams.From))
                pFileSubtile.From = argParams.From;
            pFileSubtile.To = argParams.To;
            if (!string.IsNullOrEmpty(argParams.Format))
                pFileSubtile.From = argParams.Format;
            if (argParams.Encoding != null)
                pFileSubtile.Encoding = argParams.Encoding;

            return pFileSubtile;
        }
        static void Main(string[] args)
        {
            try
            {
                ParamsCommandLine pParams = new ParamsCommandLine(args);

                if (pParams.Help)
                    ShowHelp();
                else
                {
                    FileSubtile pFileSubtile = CreateFileSubtile(pParams);
                    ProcessConsole pProcess = new ProcessConsole();
                    try
                    {
                        pFileSubtile.TranslateFile(pParams.FileInput, pProcess);
                    }
                    finally
                    {
                        pProcess.Done();
                    }
                }
            }
            catch (ParamException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Use /? or --help,-h for more information");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
            Console.WriteLine();
        }
    }
}
