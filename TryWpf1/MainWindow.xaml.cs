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
using System.ServiceModel.Syndication;

namespace TryWpf1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var lstItems = new ContentFetcher().GoFetch();
            foreach (var iterItem in lstItems) {
                StackPanel spanel = new StackPanel();

                TextBlock tbTitle = new TextBlock();
                tbTitle.MaxWidth = listBox.Width - 30d;

                ////This does no better than when we hardcode the scrollbar width as 30d, as in listBox.Width - 30d
                //Border border = (Border)VisualTreeHelper.GetChild(listBox, 0);
                //var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);            
                //tbTitle.MaxWidth = scrollViewer.ActualWidth;

                tbTitle.TextWrapping = TextWrapping.Wrap;
                tbTitle.Text = iterItem.Title.Text;
                TextBlock tbPubDate = new TextBlock();
                //tbPubDate.MaxWidth = 200d; //Don't bother; this won't grow very large
                tbPubDate.Text = iterItem.PublishDate.ToLocalTime().DateTime.ToShortDateString() + " " + iterItem.PublishDate.ToLocalTime().DateTime.ToShortTimeString();

                spanel.Children.Add(tbTitle);
                spanel.Children.Add(tbPubDate);
                spanel.Tag = iterItem;
                spanel.Margin = new Thickness() { Bottom=8 };

                listBox.Items.Add(spanel);
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selItem = listBox.SelectedItem;
            //var selObj = listBox.SelectedValue; //Both SelectedItem and SelectedValue seem to be the same
            StackPanel spanel = (StackPanel)selItem;
            SyndicationItem newsItem = (SyndicationItem)spanel.Tag;
            var selNewsItemUri = newsItem.Links.First().Uri;
            webBrowser.Navigate(selNewsItemUri);
        }
    }
}
