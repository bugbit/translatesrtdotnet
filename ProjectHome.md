As Translate v2 API, which is now available as a paid service, i create a website using the Traductor de Google, this web is:

http://bugbit.hostignition.com/TranslatesSubtile/

Translate Subtitle extension .srt y .sub for .net and Mono

Syntax:

TranslatesSRTDotNet [Options](Options.md) <file input> [output](file.md)

file input			file for translate
file output			file output translate. Default <file input>-

&lt;lang&gt;

.sub|.srt

Options:

-r,--referrer 

&lt;url&gt;

		your http referrer header valid for Google.API.Translate. Default TranslatesSRTDotNet.exe.config see below for more detalls
-f,--from 

&lt;lang&gt;

		The language of the original text.
-t,--to 

&lt;lang&gt;

			The target language you want to translate to.
-a,--format {txt|html}		format subtitles. Default:txt
-e,--encoding <codepage|named>	Encoding file. Default:utf-8

TranslatesSRTDotNet.exe.config:


&lt;applicationSettings&gt;


> <TranslatesSRTDotNet.Properties.Settings>
> > 

&lt;setting name="Referrer" serializeAs="String"&gt;


> > > 

&lt;value&gt;

your url

&lt;/value&gt;



> > 

&lt;/setting&gt;



> </TranslatesSRTDotNet.Properties.Settings>


&lt;/applicationSettings&gt;

