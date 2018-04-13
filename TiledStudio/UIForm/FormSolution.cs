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

namespace TiledStudio
{
    public partial class FormSolution : DockContent
    {
        public FormSolution()
        {
            InitializeComponent();
        }

        TreeNode SelectedTreeNode { get; set; }
        TreeNode Root;

        public PropertyGrid MapProperty { get; set; }


        public void LoadFromArea()
        {
            SolutionTree.BeginUpdate();
            SolutionTree.Nodes.Clear();
            Root = SolutionTree.Nodes.Add(XmlHelper.Instance.ProjectName);
            foreach (var area in DataHelper.Instance.AllMapAreaList)
            {
                area.TreeViewNode = Root.Nodes.Add(area.Text);
                area.TreeViewNode.Name = area.Name;
                foreach (var floor in area.floors.Values)
                {
                    floor.TreeViewNode = area.TreeViewNode.Nodes.Add(floor.Text);
                    floor.TreeViewNode.Name = floor.ID.ToString();
                }
            }
            SolutionTree.EndUpdate();
            SolutionTree.ExpandAll();
        }

        MapArea FindMapArea(TreeNode node)
        {
            foreach (var area in DataHelper.Instance.AllMapAreaList)
            {
                if (area.TreeViewNode == node)
                {
                    return area;
                }
            }
            return null;
        }

        MapFloor FindMapFloor(TreeNode node)
        {
            var area = FindMapArea(node.Parent);
            if (area != null)
            {
                MapFloor floor = null;
                if (area.floors.TryGetValue(int.Parse(node.Name), out floor))
                {
                    return floor;
                }
            }
            return null;
        }

        //节点选中的时候调用
        private void SolutionTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SelectedTreeNode = e.Node;
            SelectedTreeNode.Expand();
            if (SelectedTreeNode.Level == 1)
            {
                var area = FindMapArea(SelectedTreeNode);
                MapProperty.SelectedObject = area;
                DataHelper.Instance.CurrentMapArea = area;
                DataHelper.Instance.CurrentMapFloor = area.GetFirstFloor();
            }
            else if (SelectedTreeNode.Level == 2)
            {
                var floor = FindMapFloor(SelectedTreeNode);
                floor.SetPropertyObject(MapProperty);
                DataHelper.Instance.CurrentMapArea = floor.belongArea;
                DataHelper.Instance.CurrentMapFloor = floor;

                if (floor.belongArea.isXYRArea)
                {
                    FormMain.Instance.fmEditor.MoveToCenter(floor.X, floor.Y);
                }
            }
        }

        //选中area节点后，insert插入floor节点，选中floor节点后delete删除之
        private void SolutionTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (SelectedTreeNode != null)
            {
                if (SelectedTreeNode.Level == 1)//area
                {
                    var area = FindMapArea(SelectedTreeNode);
                    if (e.KeyCode == Keys.Insert)
                    {
                        var floor = area.AddMapFloor();
                        var node = SelectedTreeNode.Nodes.Add(floor.Text);
                        node.Name = floor.ID.ToString();
                        floor.TreeViewNode = node;
                        SelectedTreeNode.Expand();
                    }
                }

                else if (SelectedTreeNode.Level == 2)
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        var area = FindMapArea(SelectedTreeNode.Parent);
                        if (area != null)
                        {
                            area.RemoveMapFloor(int.Parse(SelectedTreeNode.Name));
                        }
                        SelectedTreeNode.Remove();
                    }
                }
            }
        }

        private void 添加节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedTreeNode != null)
            {
                if (SelectedTreeNode.Level == 1)//area
                {
                    var area = FindMapArea(SelectedTreeNode);


                    var floor = area.AddMapFloor();
                    var node = SelectedTreeNode.Nodes.Add(floor.Text);
                    node.Name = floor.ID.ToString();
                    floor.TreeViewNode = node;
                    SelectedTreeNode.Expand();
                }
            }

        }

        private void 删除节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedTreeNode != null)
            {
                if (SelectedTreeNode.Level == 2)
                {

                    var area = FindMapArea(SelectedTreeNode.Parent);
                    if (area != null)
                    {
                        area.RemoveMapFloor(int.Parse(SelectedTreeNode.Name));
                    }
                    SelectedTreeNode.Remove();

                }
            }

        }
    }
}
