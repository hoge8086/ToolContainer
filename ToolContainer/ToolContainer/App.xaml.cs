using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ToolContainer.Plugin;
using PluginInterface;

namespace ToolContainer
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        public static List<IPlugin> Plugins { get; private set; } = new List<IPlugin>();

        protected override void OnStartup (System.Windows.StartupEventArgs e)
        {
            try
            {
                //インストールされているプラグインを調べる
                PluginInfo[] pis = PluginInfo.FindPlugins();

                //すべてのプラグインクラスのインスタンスを作成する
                for (int i = 0; i < pis.Length; i++)
                {
                    try
                    {
                        Plugins.Add(pis[i].CreateInstance());
                    }
                    catch { }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ツールまとめる君", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Shutdown();
            }
        }
    }
}
