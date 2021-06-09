using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection;
using System.IO;

using PluginFramework;

namespace ImageHandler
{
    public partial class Form1 : Form
    {
        Dictionary<string, IFilter> _filters = new Dictionary<string, IFilter>();

        public Form1()
        {
            InitializeComponent();

            var assembly = Assembly.GetExecutingAssembly();
            var folder = Path.GetDirectoryName(assembly.Location);

            LoadFilters(folder);
            createFilterMenu();
        }

        void LoadFilters(string folder)
        {
            _filters.Clear();
            foreach(var dll in Directory.GetFiles(folder, "*.dll"))
            {
                try
                {
                    var asm = Assembly.LoadFrom(dll);
                    foreach(var type in asm.GetTypes())
                    {
                        if(type.GetInterface("IFilter") == typeof(IFilter))
                        {
                            var filter = Activator.CreateInstance(type) as IFilter;
                            _filters[filter.Name] = filter;
                        }
                    }

                }
                catch (BadImageFormatException) { }
                {

                }
            }
        }


        void createFilterMenu()
        {
            pluginsToolStripMenuItem.DropDownItems.Clear();

            foreach(KeyValuePair<string, IFilter> pair in _filters)
            {
                var item = new ToolStripMenuItem(pair.Key);
                item.Click += new EventHandler(menuItem_Click);
                pluginsToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        void menuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            var filter = _filters[menuItem.Text];
            try
            {
                this.Cursor = Cursors.WaitCursor;
                pictureBox1.Image = filter.RunPlugin(pictureBox1.Image);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }
    }
}
