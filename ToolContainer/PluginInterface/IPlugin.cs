using System.Windows.Controls;

namespace PluginInterface
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        UserControl Panel { get; }
    }
}
