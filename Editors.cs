using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Bootpad {
    class CommonEditor : TextBox {   
        public CommonEditor() {
            Multiline = true;
            Dock = DockStyle.Fill;
            BorderStyle = BorderStyle.None;
            AcceptsTab = true;
            ScrollBars = ScrollBars.Vertical;         
            InitContextMenu();
        }

        private void WalkAddingMenuItems(XmlNodeList nodes, ToolStripMenuItem parent) {
            if (nodes != null) {
                foreach (XmlNode n in nodes) {
                    ToolStripMenuItem curItem = null;

                    if (parent == null) {
                        curItem = ContextMenuStrip.Items.Add(n.Attributes["name"].Value) as ToolStripMenuItem;
                    } else {
                        curItem = parent.DropDownItems.Add(n.Attributes["name"].Value) as ToolStripMenuItem;
                    }

                    curItem.Name = n.Attributes["name"].Value;

                    var snippets = n.SelectNodes("snip");
                    foreach (XmlNode snippet in snippets) {
                        curItem.DropDownItems.Add(snippet.Attributes["name"].Value, null, delegate {
                            var sel = Environment.NewLine + SelectedText.Trim() + Environment.NewLine;
                            SelectedText = string.Format(snippet.InnerText.Trim(), sel) + Environment.NewLine;
                        });
                    }

                    WalkAddingMenuItems(n.SelectNodes("node"), curItem);
                }
            }
        }
        protected virtual void InitContextMenu() {
            var c = new ContextMenuStrip();

            c.Items.Add("Cut", null, delegate { Cut(); });
            c.Items.Add("Copy", null, delegate { Copy(); });
            c.Items.Add("Paste", null, delegate { Paste(); });
            c.Items.Add("Delete", null, delegate { SelectedText = ""; });
            c.Items.Add("-");
            c.Items.Add("Select All", null, delegate { SelectAll(); });
            c.Items.Add("-");
            c.Items.Add("Upcase", null, delegate { SelectedText = SelectedText.ToUpper(); });
            c.Items.Add("Downcase", null, delegate { SelectedText = SelectedText.ToLower(); });
            c.Items.Add("-");

            ContextMenuStrip = c;
        }

        protected virtual void AddSnippets(string snippetPath, ToolStripMenuItem parent = null) {
            if (Snippets.Instance != null) {
                WalkAddingMenuItems(Snippets.Instance.SelectNodes(snippetPath), parent);
            }
        }
    }

    class HtmlEditor : CommonEditor {
        protected override void InitContextMenu() {
            base.InitContextMenu();
            AddSnippets("//html/node");
            var erb = ContextMenuStrip.Items["ERB"] as ToolStripMenuItem;
            if (erb != null) {
                //erb.DropDownItems.Add("Make Partial", null, delegate { });
                erb.Add("Make Partial", delegate { MessageBox.Show("Here"); });
            }
        }
    }
}
