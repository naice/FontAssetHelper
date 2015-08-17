using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FontAssetHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FontInfos fontInfos;

        public MainWindow()
        {
            InitializeComponent();
            InitializeFontInfos();
        }

        private void InitializeFontInfos()
        {
            try
            {
                fontInfos = Newtonsoft.Json.JsonConvert.DeserializeObject<FontInfos>(System.IO.File.ReadAllText("data.json"));
                fontInfos.Init();
                DataContext = fontInfos;
            }
            catch (Exception ex)
            {
                fontInfos = new FontInfos();
                MessageBox.Show(ex.Message, "Error loading data.json");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (fontInfos != null)
            {
                fontInfos.SearchToken = edSearch.Text;
                fontInfos.Search();
            }
        }

        private void putclipboard(string val)
        {
            try
            {
                Clipboard.SetText(val);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Copy error!");
            }
        }

        private void CopyName(object sender, RoutedEventArgs e)
        {
            MenuItem item = e.OriginalSource as MenuItem;
            if (item != null && item.DataContext != null)
            {
                FontSymbol dat = item.DataContext as FontSymbol;

                putclipboard(dat.Name);
            }

        }

        private void CopyHex(object sender, RoutedEventArgs e)
        {
            MenuItem item = e.OriginalSource as MenuItem;
            if (item != null && item.DataContext != null)
            {
                FontSymbol dat = item.DataContext as FontSymbol;
                putclipboard(dat.XamlMarkup);
            }
        }

        private void CopyChar(object sender, RoutedEventArgs e)
        {

            MenuItem item = e.OriginalSource as MenuItem;
            if (item != null && item.DataContext != null)
            {
                FontSymbol dat = item.DataContext as FontSymbol;

                putclipboard(dat.Symbol);
            }
            
        }

        private void CopyStaticRessource(object sender, RoutedEventArgs e)
        {
            MenuItem item = e.OriginalSource as MenuItem;
            if (item != null && item.DataContext != null)
            {
                FontSymbol dat = item.DataContext as FontSymbol;
                putclipboard("{StaticResource " + fontInfos.CurrentFontInfo.ResourcePrefix + dat.Name + "}");
            }
        }
    }
}
