using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontAssetHelper
{
    public class FontSymbol
    {
        public string Name { get; set; }
        public string Hex { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public FontInfo FontInfo { get; set; }
        private string _Symbol = null;
        [Newtonsoft.Json.JsonIgnore]
        public string Symbol
        {
            get
            {
                if (!string.IsNullOrEmpty(Hex) && string.IsNullOrEmpty(_Symbol))
                    _Symbol = char.ConvertFromUtf32(int.Parse(Hex, NumberStyles.HexNumber));
                return _Symbol;
            }
        }
        private string _XamlMarkup = null;
        [Newtonsoft.Json.JsonIgnore]
        public string XamlMarkup
        {
            get
            {
                if (!string.IsNullOrEmpty(Hex) && string.IsNullOrEmpty(_XamlMarkup))
                {
                    int d = int.Parse(Hex, NumberStyles.HexNumber);
                    if (Hex.Length > 4)
                    {
                        _XamlMarkup = string.Format("&#x{0:X8};", d);
                    }
                    else
                    {
                        _XamlMarkup = string.Format("&#x{0:X4};", d);
                    }
                }
                return _XamlMarkup;
            }
        }

        private string _SearchToken = null;
        [Newtonsoft.Json.JsonIgnore]
        public string SearchToken
        {
            get
            {
                _SearchToken = "";
                if (string.IsNullOrEmpty(_SearchToken))
                {
                    if (!string.IsNullOrEmpty(Name))
                        _SearchToken = Name.ToLower();
                    if (!string.IsNullOrEmpty(XamlMarkup))
                        _SearchToken += " " + XamlMarkup.ToLower();
                }

                return _SearchToken;
            }
        }
    }
}
