using PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestPlugin
{
    public class TestPlugin : IPlugin
    {
        public string Name { get { return "テストプラグイン"; } }

        public string Description { get { return "テスト用のプラグインです."; } }

        public UserControl Panel { get { return new TestUserControl(); } }
    }
}
