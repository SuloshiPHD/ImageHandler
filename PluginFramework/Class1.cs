
using System.Drawing;

namespace PluginFramework
{
    public interface IFilter
    {
        Image RunPlugin(Image src);
        string Name { get; }

       
    }
}
