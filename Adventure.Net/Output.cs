using System;
using System.IO;
using System.Text;

namespace Adventure.Net
{
    public class Output 
    {
        private readonly TextWriter target;
        
        public Output(TextWriter destination)
        {
            target = destination;
        }

        public void Bold(string text)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Print(text);
            Console.ForegroundColor = currentColor;
        }

        public void Print(string text)
        {
            if (String.IsNullOrEmpty(text))
                return;

            string[] lines = text.Split('\n');
            foreach(string line in lines)
                target.WriteLine(WordWrap(line));
        }

        public void Print(string format, params object[] arg)
        {
            target.WriteLine(WordWrap(format), arg);    
        }

        public void PrintLine()
        {
            target.WriteLine();
        }

        public void Write(string text)
        {
            target.Write(WordWrap(text));
        }

        public string Buffer
        {
            get
            {
                return target.ToString();
            }
        }

        private static string WordWrap(string text)
        {
            // if the text contains line feeds or tabs assume that it is pre-formatted
            if (text.Contains("\n") || text.Contains("\t"))
            {
                return text.Replace("\t", "  ");
            }

            StringBuilder sb = new StringBuilder();
            string[] words = text.Split(' ');

            int charsInLine = 0;

            for (int i = 0; i < words.Length; i++)
            {
                sb.AppendFormat("{0} ", words[i]);
                charsInLine += words[i].Length + 1;

                if (i < words.Length - 1)
                {
                    if (charsInLine + words[i + 1].Length >= 79)
                    {
                        sb.Append(Environment.NewLine);
                        charsInLine = 0;
                    }    
                }
            }
            
            return sb.ToString().Trim();
        }
    }
}