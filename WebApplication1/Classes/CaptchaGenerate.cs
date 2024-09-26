using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Drawing;

namespace WebApplication1.Classes
{
    public class CaptchaGenerate
    {
        private static Random random = new Random();
        public string Text { get; private set; }

        public CaptchaGenerate()
        {
            Text = GenerateCaptchaText(5);
           
            if (HasDigit(Text))
            {
                Text += GenerateCaptchaText(1, false); 
            }
            else
            {
                Text += GenerateCaptchaText(1, true); 
            }
        }

        private string GenerateCaptchaText(int length, bool isDigit = false)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";
            var charSet = isDigit ? digits : chars;

            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = charSet[random.Next(charSet.Length)];
            }
            return new string(result);
        }

        private bool HasDigit(string text)
        {
            foreach (char c in text)
            {
                if (char.IsDigit(c))
                    return true;
            }
            return false;
        }
    }
}
