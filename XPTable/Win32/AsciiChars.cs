/*
 * Copyright © 2005, Mathew Hall
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 *
 *    - Redistributions of source code must retain the above copyright notice, 
 *      this list of conditions and the following disclaimer.
 * 
 *    - Redistributions in binary form must reproduce the above copyright notice, 
 *      this list of conditions and the following disclaimer in the documentation 
 *      and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
 * OF SUCH DAMAGE.
 */


using System;


namespace XPTable.Win32
{
	/// <summary>
	/// Represents the Ascii character values.
	/// </summary>
	public abstract class AsciiChars
	{
		/// <summary>
		/// Null character (NUL)
		/// </summary>
		public static readonly char Null = '\x0000';
		
		/// <summary>
		/// Start of heading character (SOH)
		/// </summary>
		public static readonly char StartOfHeading = '\x0001';
		
		/// <summary>
		/// Start of text character (STX)
		/// </summary>
		public static readonly char StartOfText = '\x0002';
		
		/// <summary>
		/// End of text character (ETX)
		/// </summary>
		public static readonly char EndOfText = '\x0003';
		
		/// <summary>
		/// End of transmission character (EOT)
		/// </summary>
		public static readonly char EndOfTransmission = '\x0004';
		
		/// <summary>
		/// Enquiry character (ENQ)
		/// </summary>
		public static readonly char Enquiry = '\x0005';
		
		/// <summary>
		/// Acknowledge character (ACK)
		/// </summary>
		public static readonly char Acknowledge = '\x0006';
		
		/// <summary>
		/// Bell character (BEL)
		/// </summary>
		public static readonly char Bell = '\x0007';
		
		/// <summary>
		/// Backspace character (BS)
		/// </summary>
		public static readonly char Backspace = '\x0008';
		
		/// <summary>
		/// Horizontal tab character (HT)
		/// </summary>
		public static readonly char HorizontalTab = '\x0009';
		
		/// <summary>
		/// Line Feed character (LF)
		/// </summary>
		public static readonly char LineFeed = '\x000a';
		
		/// <summary>
		/// Vertical tab character (VT)
		/// </summary>
		public static readonly char VerticalTab = '\x000b';
		
		/// <summary>
		/// Form Feed character (FF)
		/// </summary>
		public static readonly char FormFeed = '\x000c';
		
		/// <summary>
		/// Carriage Return character (CR)
		/// </summary>
		public static readonly char CarriageReturn = '\x000d';
		
		/// <summary>
		/// Shift Out character (SO)
		/// </summary>
		public static readonly char ShiftOut = '\x000e';
		
		/// <summary>
		/// Shift In character (SI)
		/// </summary>
		public static readonly char ShiftIn = '\x000f';

		
		/// <summary>
		/// Data link escape character (DLE)
		/// </summary>
		public static readonly char DataLinkEscape = '\x0010';
		
		/// <summary>
		/// XON character (DC1)
		/// </summary>
		public static readonly char XON = '\x0011';
		
		/// <summary>
		/// Device control 2 character (DC2)
		/// </summary>
		public static readonly char DeviceControl2 = '\x0012';
		
		/// <summary>
		/// XOFF character (DC3)
		/// </summary>
		public static readonly char XOFF = '\x0013';
		
		/// <summary>
		/// Device control 4 character (DC4)
		/// </summary>
		public static readonly char DeviceControl4 = '\x0014';
		
		/// <summary>
		/// Negative acknowledge character (NAK)
		/// </summary>
		public static readonly char NegativeAcknowledge = '\x0015';
		
		/// <summary>
		/// Synchronous idle character (SYN)
		/// </summary>
		public static readonly char SynchronousIdle = '\x0016';
		
		/// <summary>
		/// End transmission block character (ETB)
		/// </summary>
		public static readonly char EndTransmissionBlock = '\x0017';
		
		/// <summary>
		/// Cancel line character (CAN)
		/// </summary>
		public static readonly char CancelLine = '\x0018';
		
		/// <summary>
		/// End of medium character (EM)
		/// </summary>
		public static readonly char EndOfMedium = '\x0019';
		
		/// <summary>
		/// Substitute character (SUB)
		/// </summary>
		public static readonly char Substitute = '\x001a';
		
		/// <summary>
		/// Escape character (ESC)
		/// </summary>
		public static readonly char Escape = '\x001b';
		
		/// <summary>
		/// File separator character (FS)
		/// </summary>
		public static readonly char FileSeparator = '\x001c';
		
		/// <summary>
		/// Group separator character (GS)
		/// </summary>
		public static readonly char GroupSeparator = '\x001d';
		
		/// <summary>
		/// Record separator character (RS)
		/// </summary>
		public static readonly char RecordSeparator = '\x001e';
		
		/// <summary>
		/// Unit separator character (US)
		/// </summary>
		public static readonly char UnitSeparator = '\x001f';

		
		/// <summary>
		/// Space character (SP)
		/// </summary>
		public static readonly char Space = '\x0020';
		
		/// <summary>
		/// Exclamation mark character (!)
		/// </summary>
		public static readonly char ExclamationMark = '\x0021';
		
		/// <summary>
		/// Quotation mark character (")
		/// </summary>
		public static readonly char QuotationMark = '\x0022';
		
		/// <summary>
		/// Cross hatch character (#)
		/// </summary>
		public static readonly char CrossHatch = '\x0023';
		
		/// <summary>
		/// Dollar sign character ($)
		/// </summary>
		public static readonly char DollarSign = '\x0024';
		
		/// <summary>
		/// Percent sign character (%)
		/// </summary>
		public static readonly char PercentSign = '\x0025';
		
		/// <summary>
		/// Ampersand character (&amp;)
		/// </summary>
		public static readonly char Ampersand = '\x0026';
		
		/// <summary>
		/// Closing single quote or Apostrophe character (')
		/// </summary>
		public static readonly char ClosingSingleQuote = '\x0027';
		
		/// <summary>
		/// Opening parentheses character (()
		/// </summary>
		public static readonly char OpeningParentheses = '\x0028';
		
		/// <summary>
		/// Closing parentheses character ())
		/// </summary>
		public static readonly char ClosingParentheses = '\x0029';
		
		/// <summary>
		/// Asterisk character (*)
		/// </summary>
		public static readonly char Asterisk = '\x002a';
		
		/// <summary>
		/// Plus character (+)
		/// </summary>
		public static readonly char Plus = '\x002b';
		
		/// <summary>
		/// Comma character (,)
		/// </summary>
		public static readonly char Comma = '\x002c';
		
		/// <summary>
		/// Hyphen character (-)
		/// </summary>
		public static readonly char Hyphen = '\x002d';
		
		/// <summary>
		/// FullStop character (.)
		/// </summary>
		public static readonly char FullStop = '\x002e';
		
		/// <summary>
		/// Forward slash character (/)
		/// </summary>
		public static readonly char ForwardSlash = '\x002f';

		
		/// <summary>
		/// Zero (0)
		/// </summary>
		public static readonly char Zero = '\x0030';
		
		/// <summary>
		/// One character (1)
		/// </summary>
		public static readonly char One = '\x0031';
		
		/// <summary>
		/// Two (2)
		/// </summary>
		public static readonly char Two = '\x0032';
		
		/// <summary>
		/// Three (3)
		/// </summary>
		public static readonly char Three = '\x0033';
		
		/// <summary>
		/// Four (4)
		/// </summary>
		public static readonly char Four = '\x0034';
		
		/// <summary>
		/// Five (5)
		/// </summary>
		public static readonly char Five = '\x0035';
		
		/// <summary>
		/// Six (6)
		/// </summary>
		public static readonly char Six = '\x0036';
		
		/// <summary>
		/// Seven (7)
		/// </summary>
		public static readonly char Seven = '\x0037';
		
		/// <summary>
		/// Eight (8)
		/// </summary>
		public static readonly char Eight = '\x0038';
		
		/// <summary>
		/// Nine (9)
		/// </summary>
		public static readonly char Nine = '\x0039';
		
		/// <summary>
		/// Colon character (:)
		/// </summary>
		public static readonly char Colon = '\x003a';
		
		/// <summary>
		/// Semicolon character (;)
		/// </summary>
		public static readonly char Semicolon = '\x003b';
		
		/// <summary>
		/// Less than character (&lt;)
		/// </summary>
		public static readonly char LessThan = '\x003c';
		
		/// <summary>
		/// Equals character (=)
		/// </summary>
		public static readonly char EqualsSign = '\x003d';
		
		/// <summary>
		/// Greater than character (&gt;)
		/// </summary>
		public static readonly char GreaterThan = '\x003e';
		
		/// <summary>
		/// Question mark character (?)
		/// </summary>
		public static readonly char QuestionMark = '\x003f';

		
		/// <summary>
		/// At-sign character (@)
		/// </summary>
		public static readonly char AtSign = '\x0040';
		
		/// <summary>
		/// Uppercase A
		/// </summary>
		public static readonly char UppercaseA = '\x0041';
		
		/// <summary>
		/// Uppercase B
		/// </summary>
		public static readonly char UppercaseB = '\x0042';
		
		/// <summary>
		/// Uppercase C
		/// </summary>
		public static readonly char UppercaseC = '\x0043';
		
		/// <summary>
		/// Uppercase D
		/// </summary>
		public static readonly char UppercaseD = '\x0044';
		
		/// <summary>
		/// Uppercase E
		/// </summary>
		public static readonly char UppercaseE = '\x0045';
		
		/// <summary>
		/// Uppercase F
		/// </summary>
		public static readonly char UppercaseF = '\x0046';
		
		/// <summary>
		/// Uppercase G
		/// </summary>
		public static readonly char UppercaseG = '\x0047';
		
		/// <summary>
		/// Uppercase H
		/// </summary>
		public static readonly char UppercaseH = '\x0048';
		
		/// <summary>
		/// Uppercase I
		/// </summary>
		public static readonly char UppercaseI = '\x0049';
		
		/// <summary>
		/// Uppercase J
		/// </summary>
		public static readonly char UppercaseJ = '\x004a';
		
		/// <summary>
		/// Uppercase K
		/// </summary>
		public static readonly char UppercaseK = '\x004b';
		
		/// <summary>
		/// Uppercase L
		/// </summary>
		public static readonly char UppercaseL = '\x004c';
		
		/// <summary>
		/// Uppercase M
		/// </summary>
		public static readonly char UppercaseM = '\x004d';
		
		/// <summary>
		/// Uppercase N
		/// </summary>
		public static readonly char UppercaseN = '\x004e';
		
		/// <summary>
		/// Uppercase O
		/// </summary>
		public static readonly char UppercaseO = '\x004f';

		
		/// <summary>
		/// Uppercase P
		/// </summary>
		public static readonly char UppercaseP = '\x0050';
		
		/// <summary>
		/// Uppercase Q
		/// </summary>
		public static readonly char UppercaseQ = '\x0051';
		
		/// <summary>
		/// Uppercase R
		/// </summary>
		public static readonly char UppercaseR = '\x0052';
		
		/// <summary>
		/// Uppercase S
		/// </summary>
		public static readonly char UppercaseS = '\x0053';
		
		/// <summary>
		/// Uppercase T
		/// </summary>
		public static readonly char UppercaseT = '\x0054';
		
		/// <summary>
		/// Uppercase U
		/// </summary>
		public static readonly char UppercaseU = '\x0055';
		
		/// <summary>
		/// Uppercase V
		/// </summary>
		public static readonly char UppercaseV = '\x0056';
		
		/// <summary>
		/// Uppercase W
		/// </summary>
		public static readonly char UppercaseW = '\x0057';
		
		/// <summary>
		/// Uppercase X
		/// </summary>
		public static readonly char UppercaseX = '\x0058';
		
		/// <summary>
		/// Uppercase Y
		/// </summary>
		public static readonly char UppercaseY = '\x0059';
		
		/// <summary>
		/// Uppercase Z
		/// </summary>
		public static readonly char UppercaseZ = '\x005a';
		
		/// <summary>
		/// Opening square bracket character ([)
		/// </summary>
		public static readonly char OpeningSquareBracket = '\x005b';
		
		/// <summary>
		/// Backslash character (\)
		/// </summary>
		public static readonly char Backslash = '\x005c';
		
		/// <summary>
		/// Closing square bracket character (])
		/// </summary>
		public static readonly char ClosingSquareBracket = '\x005d';
		
		/// <summary>
		/// Caret (Circumflex) character (^)
		/// </summary>
		public static readonly char Caret = '\x005e';
		
		/// <summary>
		/// Underscore character (_)
		/// </summary>
		public static readonly char Underscore = '\x005f';

		
		/// <summary>
		/// Opening single quote character (`)
		/// </summary>
		public static readonly char OpeningSingleQuote = '\x0060';
		
		/// <summary>
		/// Lowercase a
		/// </summary>
		public static readonly char LowercaseA = '\x0061';
		
		/// <summary>
		/// Lowercase b
		/// </summary>
		public static readonly char LowercaseB = '\x0062';
		
		/// <summary>
		/// Lowercase c
		/// </summary>
		public static readonly char LowercaseC = '\x0063';
		
		/// <summary>
		/// Lowercase d
		/// </summary>
		public static readonly char LowercaseD = '\x0064';
		
		/// <summary>
		/// Lowercase e
		/// </summary>
		public static readonly char LowercaseE = '\x0065';
		
		/// <summary>
		/// Lowercase f
		/// </summary>
		public static readonly char LowercaseF = '\x0066';
		
		/// <summary>
		/// Lowercase g
		/// </summary>
		public static readonly char LowercaseG = '\x0067';
		
		/// <summary>
		/// Lowercase h
		/// </summary>
		public static readonly char LowercaseH = '\x0068';
		
		/// <summary>
		/// Lowercase i
		/// </summary>
		public static readonly char LowercaseI = '\x0069';
		
		/// <summary>
		/// Lowercase j
		/// </summary>
		public static readonly char LowercaseJ = '\x006a';
		
		/// <summary>
		/// Lowercase k
		/// </summary>
		public static readonly char LowercaseK = '\x006b';
		
		/// <summary>
		/// Lowercase l
		/// </summary>
		public static readonly char LowercaseL = '\x006c';
		
		/// <summary>
		/// Lowercase m
		/// </summary>
		public static readonly char LowercaseM = '\x006d';
		
		/// <summary>
		/// Lowercase n
		/// </summary>
		public static readonly char LowercaseN = '\x006e';
		
		/// <summary>
		/// Lowercase o
		/// </summary>
		public static readonly char LowercaseO = '\x006f';

		
		/// <summary>
		/// Lowercase p
		/// </summary>
		public static readonly char LowercaseP = '\x0070';
		
		/// <summary>
		/// Lowercase q
		/// </summary>
		public static readonly char LowercaseQ = '\x0071';
		
		/// <summary>
		/// Lowercase r
		/// </summary>
		public static readonly char LowercaseR = '\x0072';
		
		/// <summary>
		/// Lowercase s
		/// </summary>
		public static readonly char LowercaseS = '\x0073';
		
		/// <summary>
		/// Lowercase t
		/// </summary>
		public static readonly char LowercaseT = '\x0074';
		
		/// <summary>
		/// Lowercase u
		/// </summary>
		public static readonly char LowercaseU = '\x0075';
		
		/// <summary>
		/// Lowercase v
		/// </summary>
		public static readonly char LowercaseV = '\x0076';
		
		/// <summary>
		/// Lowercase w
		/// </summary>
		public static readonly char LowercaseW = '\x0077';
		
		/// <summary>
		/// Lowercase x
		/// </summary>
		public static readonly char LowercaseX = '\x0078';
		
		/// <summary>
		/// Lowercase y
		/// </summary>
		public static readonly char LowercaseY = '\x0079';
		
		/// <summary>
		/// Lowercase z
		/// </summary>
		public static readonly char LowercaseZ = '\x007a';
		
		/// <summary>
		/// Opening curly brace character ({)
		/// </summary>
		public static readonly char OpeningCurlyBrace = '\x007b';
		
		/// <summary>
		/// Vertical line character (|)
		/// </summary>
		public static readonly char VerticalLine = '\x007c';
		
		/// <summary>
		/// Closing curly brace character (})
		/// </summary>
		public static readonly char ClosingCurlyBrace = '\x007d';
		
		/// <summary>
		/// Tilde character (~)
		/// </summary>
		public static readonly char Tilde = '\x007e';
		
		/// <summary>
		/// Delete character (DEL)
		/// </summary>
		public static readonly char Delete = '\x007f';
	}
}
