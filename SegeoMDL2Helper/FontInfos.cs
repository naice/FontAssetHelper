using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegeoMDL2Helper
{
    public class FontInfos : BaseViewModel
    {
        private List<FontInfo> _Data;
        public List<FontInfo> Data
        {
            get { return _Data; }
            set
            {
                if (value != _Data)
                {
                    _Data = value;
                    RaisePropertyChanged("Data");
                }
            }
        }

        private FontInfo _CurrentFontInfo;
        [Newtonsoft.Json.JsonIgnore]
        public FontInfo CurrentFontInfo
        {
            get { return _CurrentFontInfo; }
            set
            {
                if (value != _CurrentFontInfo)
                {
                    _CurrentFontInfo = value;
                    SearchToken = "";
                    Search();
                    RaisePropertyChanged("CurrentFontInfo");
                }
            }
        }

        private string _SearchToken;
        [Newtonsoft.Json.JsonIgnore]
        public string SearchToken
        {
            get { return _SearchToken; }
            set
            {
                if (value != _SearchToken)
                {
                    _SearchToken = value;
                    RaisePropertyChanged("SearchToken");
                }
            }
        }
        
        private ObservableCollection<FontSymbol> _ItemsSource;
        [Newtonsoft.Json.JsonIgnore]
        public ObservableCollection<FontSymbol> ItemsSource
        {
            get { return _ItemsSource; }
            set
            {
                if (value != _ItemsSource)
                {
                    _ItemsSource = value;
                    RaisePropertyChanged("ItemsSource");
                }
            }
        }

        public FontInfos()
        {
            Data = new List<FontInfo>();
        }

        public void Init()
        {
            foreach (var item in Data)
            {
                foreach (var item2 in item.Symbols)
                {
                    item2.FontInfo = item;
                }
            }

            CurrentFontInfo = Data.FirstOrDefault();
            SearchToken = "";
            Search();            
        }

        public void Search()
        {
            IEnumerable<FontSymbol> data;

            if (CurrentFontInfo != null)
            {
                if (!string.IsNullOrEmpty(SearchToken))
                {
                    string token = SearchToken.ToLower();
                    data = CurrentFontInfo.Symbols.Where(A => !string.IsNullOrEmpty(A.SearchToken) && A.SearchToken.Contains(token));
                }
                else
                {
                    data = CurrentFontInfo.Symbols;
                }
            }
            else
            {
                data = new List<FontSymbol>();
            }

            ItemsSource = new ObservableCollection<FontSymbol>(data);
        }
    }
}
