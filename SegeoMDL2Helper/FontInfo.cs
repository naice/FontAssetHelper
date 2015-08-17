using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FontAssetHelper
{
    public class FontInfo
    {
        public List<FontSymbol> Symbols { get; set; }
        public string FontFamily { get; set; }
        public string FriendlyName { get; set; }
        public string ResourcePrefix { get; set; }
        public double FontSize { get; set; }


        private FontFamily _XFont;
        public FontFamily XFont
        {
            get
            {
                if (_XFont == null)
                {
                    try
                    {
                        string ff = FontFamily;
                        if (!string.IsNullOrEmpty(ff))
                        {
                            if (ff.Contains("%local%"))
                            {
                                ff = ff.Replace("%local%", Environment.CurrentDirectory);
                            }
                        }

                        _XFont = new FontFamily(ff);
                    }
                    catch (Exception ex)
                    {
                        _XFont = new FontFamily("Segoe UI");
                    }
                }

                return _XFont;
            }
        }

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
