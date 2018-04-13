using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Xml.Linq;

namespace TiledStudio
{
    public partial class FormDrawer : DockContent
    {
        DrawerGroup group = new DrawerGroup();
        public FormDrawer()
        {
            InitializeComponent();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            group.ClientSize = ClientSize;
        }

        public void LoadFromXML(string filename)
        {
            //清除控件
            drawerpanel.Controls.Clear();
            group.allDrawerList.Clear();

            XElement xml = XElement.Load(filename);
            var controls = xml.Element("AIControl");
            List<string> nodeNames = new List<string>();
            foreach(var e in controls.Elements())
            {
                nodeNames.Add(e.Attribute("dscr").Value);
            }
            group.AddDrawer("控制节点", ControlNodes, nodeNames);
            nodeNames = new List<string>();
            foreach(var e in xml.Element("AICondition").Elements())
            {
                nodeNames.Add(e.Attribute("dscr").Value);
            }
            group.AddDrawer("条件节点", ConditionNode, nodeNames);
            nodeNames = new List<string>();
            foreach(var e in xml.Element("AIAction").Elements())
            {
                nodeNames.Add(e.Attribute("dscr").Value);
            }
            group.AddDrawer("行为节点", ActionNode, nodeNames);


            foreach (var d in group.allDrawerList)
            {
                this.drawerpanel.Controls.Add(d.DrawerButton);
                this.drawerpanel.Controls.Add(d.DrawerView);
            }

        }
    }
}
