using System;
using System.Globalization;
using System.Text;

#pragma warning disable S2368

namespace MorseCodeTranslator
{
    public static class Translator
    {
        public static string TranslateToMorse(string message)
        {
            var translateToMorse = new StringBuilder();
            WriteMorse(MorseCodes.CodeTable, message, translateToMorse);
            return translateToMorse.ToString();
        }

        public static string TranslateToText(string morseMessage)
        {
            var translateToText = new StringBuilder();
            WriteText(MorseCodes.CodeTable, morseMessage, translateToText);
            return translateToText.ToString();
        }

        public static void WriteMorse(char[][] codeTable, string message, StringBuilder morseMessageBuilder, char dot = '.', char dash = '-', char separator = ' ')
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "Message not be null.");
            }

            if (morseMessageBuilder == null)
            {
                throw new ArgumentNullException(nameof(morseMessageBuilder));
            }

            if (codeTable == null)
            {
                throw new ArgumentNullException(nameof(codeTable));
            }

            for (int i = 0; i < message.Length; i++)
            {
                if (char.IsWhiteSpace(message[i]) || char.IsPunctuation(message[i]))
                {
                    continue;
                }

                if (i != 0)
                {
                    morseMessageBuilder.Append(" ");
                }

                for (int j = 0; j < codeTable.Length; j++)
                {
                    if (char.ToUpper(message[i], CultureInfo.InvariantCulture) == codeTable[j][0])
                    {
                        morseMessageBuilder.Append(codeTable[j][1..]);
                    }
                }
            }

            ReplaceMorse(morseMessageBuilder, dot, dash, separator);
        }

        public static void WriteText(char[][] codeTable, string morseMessage, StringBuilder messageBuilder, char dot = '.', char dash = '-', char separator = ' ')
        {
            if (morseMessage == null)
            {
                throw new ArgumentNullException(nameof(morseMessage), "Message not be null.");
            }

            if (messageBuilder == null)
            {
                throw new ArgumentNullException(nameof(messageBuilder));
            }

            if (codeTable == null)
            {
                throw new ArgumentNullException(nameof(codeTable));
            }

            string[] morseMessageArray = ReplaceText(morseMessage, dot, dash, separator);
            for (int i = 0; i < morseMessageArray.Length; i++)
            {
                for (int j = 0; j < codeTable.Length; j++)
                {
                    string morseCode = new string(codeTable[j][1..]);
                    if (morseMessageArray[i] == morseCode)
                    {
                        messageBuilder.Append(MorseCodes.CodeTable[j][0]);
                    }
                }
            }
        }

        public static void ReplaceMorse(StringBuilder morseMessageBuilder, char dot, char dash, char separator)
        {
            if (morseMessageBuilder == null)
            {
                throw new ArgumentNullException(nameof(morseMessageBuilder));
            }

            for (int i = 0; i < morseMessageBuilder.Length; i++)
            {
                switch (morseMessageBuilder[i])
                {
                    case '.':
                        morseMessageBuilder[i] = dot;
                        break;
                    case '-':
                        morseMessageBuilder[i] = dash;
                        break;
                    case ' ':
                        morseMessageBuilder[i] = separator;
                        break;
                }
            }
        }

        public static string[] ReplaceText(string morseMessage, char dot, char dash, char separator)
        {
            if (morseMessage == null)
            {
                throw new ArgumentNullException(nameof(morseMessage));
            }

            StringBuilder sb = new StringBuilder();
            foreach (char c in morseMessage)
            {
                if (c == dot)
                {
                    sb.Append('.');
                }

                if (c == dash)
                {
                    sb.Append('-');
                }

                if (c == separator)
                {
                    sb.Append(' ');
                }
            }

            return sb.ToString().Split(' ');
        }
    }
}
