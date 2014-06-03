using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bootpad {
    public static class Extensions {
        public static void Add(this ToolStripMenuItem item, string label, EventHandler onclick) {
            var i = item.DropDownItems.Add(label, null, onclick);
            i.Name = label;
        }
    }
}
