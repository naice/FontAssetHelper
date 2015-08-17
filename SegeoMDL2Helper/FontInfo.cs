using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontAssetHelper
{
    public class FontInfo
    {
        public List<FontSymbol> Symbols { get; set; }
        public string FontFamily { get; set; }
        public string FriendlyName { get; set; }
        public string ResourcePrefix { get; set; }
        public double FontSize { get; set; }

        public FontInfo()
        {
            Symbols = new List<FontSymbol>();
        }

        public override string ToString()
        {
            return FriendlyName ?? FontFamily;
        }
    }
}
