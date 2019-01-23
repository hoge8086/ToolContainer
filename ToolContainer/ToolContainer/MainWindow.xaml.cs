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

using ToolContainer.Plugin;

namespace ToolContainer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //プラグインのユーザコントロールをタブに読込む
            for (int i = 0; i < App.Plugins.Count; i++)
            {
                var tab = new TabItem();
                tab.Content = App.Plugins[i].Panel;
                tab.Header = App.Plugins[i].Name;
                tabControl.Items.Add(tab);
            }

        }
    }
}
