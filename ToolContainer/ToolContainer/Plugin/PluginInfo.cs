using PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// プラグインの実装方法
/// 参考:<https://dobon.net/vb/dotnet/programing/plugin.html>
/// </summary>
namespace ToolContainer.Plugin
{
    /// <summary>
    /// プラグインに関する情報
    /// </summary>
    public class PluginInfo
    {
        /// <summary>
        /// アセンブリファイルのパス
        /// </summary>
        public string Location { get; private set; }
        /// <summary>
        /// クラスの名前
        /// </summary>
        public string ClassName { get; private set; }

        /// <summary>
        /// PluginInfoクラスのコンストラクタ
        /// </summary>
        /// <param name="path">アセンブリファイルのパス</param>
        /// <param name="cls">クラスの名前</param>
        private PluginInfo(string path, string cls)
        {
            this.Location = path;
            this.ClassName = cls;
        }

        /// <summary>
        /// 有効なプラグインを探す
        /// ★DLL名が「～Plugin.dll」となるものをロード対象とする
        /// </summary>
        /// <returns>有効なプラグインのPluginInfo配列</returns>
        public static PluginInfo[] FindPlugins()
        {
            System.Collections.ArrayList plugins =
                new System.Collections.ArrayList();
            //IPlugin型の名前
            string ipluginName = typeof(IPlugin).FullName;

            //プラグインフォルダ
            string folder = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly
                .GetExecutingAssembly().Location);
            folder += "\\plugins";
            if (!System.IO.Directory.Exists(folder))
                throw new ApplicationException(
                    "プラグインフォルダ\"" + folder +
                    "\"が見つかりませんでした。");

            //.dllファイルを探す
            string[] dlls =
                System.IO.Directory.GetFiles(folder, "*Plugin.dll", System.IO.SearchOption.AllDirectories);

            foreach (string dll in dlls)
            {
                try
                {
                    //アセンブリとして読み込む
                    System.Reflection.Assembly asm =
                        System.Reflection.Assembly.LoadFrom(dll);
                    foreach (Type t in asm.GetTypes())
                    {
                        //アセンブリ内のすべての型について、
                        //プラグインとして有効か調べる
                        if (t.IsClass && t.IsPublic && !t.IsAbstract &&
                            t.GetInterface(ipluginName) != null)
                        {
                            //PluginInfoをコレクションに追加する
                            plugins.Add(
                                new PluginInfo(dll, t.FullName));
                        }
                    }
                }
                catch
                {
                }
            }

            //コレクションを配列にして返す
            return (PluginInfo[]) plugins.ToArray(typeof(PluginInfo));
        }

        /// <summary>
        /// プラグインクラスのインスタンスを作成する
        /// </summary>
        /// <returns>プラグインクラスのインスタンス</returns>
        public IPlugin CreateInstance()
        {
            try
            {
                //アセンブリを読み込む
                System.Reflection.Assembly asm =
                    System.Reflection.Assembly.LoadFrom(this.Location);
                //クラス名からインスタンスを作成する
                return (IPlugin)asm.CreateInstance(this.ClassName);
            }
            catch
            {
                return null;
            }
        }
    }
}
