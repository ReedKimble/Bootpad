using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Bootpad {
    public partial class Frame : Form {
        HtmlEditor he;
        WebBrowser wb;
        System.Windows.Forms.Timer ut;
        bool changed = false;

        public Frame() {
            WindowState = FormWindowState.Maximized;
            Text = "Bootpad";
            Font = new Font("Anonymous Pro", 10);
            Dock = DockStyle.Fill;

            he = new HtmlEditor();
            wb = new WebBrowser { Dock = DockStyle.Fill, ScriptErrorsSuppressed = false };
            ut = new System.Windows.Forms.Timer { Interval = 5000 };
            ut.Tick += delegate {
                if (changed) {
                    new Thread(new ThreadStart(delegate { 
                        wb.DocumentText = he.Text;
                        changed = false;
                    })).Start();                    
                }                
            };
            
            he.TextChanged += delegate {
                changed = true;
            };

            var sc0 = new SplitContainer { Parent = this, Dock = DockStyle.Fill, SplitterDistance = 100 };
            sc0.Panel1.Controls.Add(he);
            sc0.Panel2.Controls.Add(wb);

            ut.Start();
            
        }        
    }
}
