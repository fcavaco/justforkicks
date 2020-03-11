using System;

namespace Qualified.kata.markdownparser
{
    public class Challenge
    {
        public static string MarkdownParser(string markdown)
        {
            markdown = markdown.Trim();

            // check if is invalid header
            if (!IsValid(markdown))
                return markdown;
            
            var items = markdown.Split(' ');
            var lenght = items[0].Split('#').Length;
            
            // remove any trailing white spaces
            var htmlValue = markdown.Remove(0, lenght).Trim();
            
            var htmlHeader = $"h{lenght - 1}";
            var html = $"<{htmlHeader}>{htmlValue}</{htmlHeader}>";
            return html;
        }

        private static bool IsValid(string markdown)
        {
            var chars = markdown.ToCharArray();
            bool valid = false;
            for (int i = 1; i < 7; i++)
            {
                if (chars[i-1] == '#' && chars[i] != '#')
                {
                    valid = Char.IsWhiteSpace(chars[i]);
                    break;
                }
            }
            return valid;
        }
    }
}
