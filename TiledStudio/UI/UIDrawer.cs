using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TiledStudio
{
    class DrawerBox
    {
        public Button DrawerButton;
        public ListView DrawerView;
        public bool IsExpand { get; set; }
        public DrawerGroup Container { get; set; }
        public ImageList itemIconList { get; set; }
        public List<string> itemTitleList { get; set; }

        public DrawerBox(DrawerGroup group, string title, ImageList icons, List<string> txts)
        {
            Container = group;
            IsExpand = false;
            itemIconList = icons;
            itemTitleList = txts;

            DrawerButton = new Button();
            DrawerButton.Text = title;
            DrawerButton.FlatStyle = FlatStyle.System;
            DrawerButton.Click += DrawerBox_Click;

            DrawerView = new ListView();
            DrawerView.View = View.List;
            DrawerView.SmallImageList = itemIconList;
            DrawerView.BorderStyle = BorderStyle.None;
            DrawerView.Visible = false;
            
            for(int i = 0; i < itemTitleList.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = 0;
                if (itemIconList.Images.Count - 1 > i)
                {
                    item.ImageIndex = i;
                }
                item.Text = itemTitleList[i];
                DrawerView.Items.Add(item);
            }
        }

        public void DrawerBox_Click(object sender, EventArgs e)
        {
            IsExpand = !IsExpand;
            DrawerView.Visible = IsExpand;
            if (Container.SelectedItem != null && Container.SelectedItem!=this && Container.SelectedItem.IsExpand)
            {
                Container.SelectedItem.DrawerBox_Click(null, null);
            }
            Container.SelectedItem = this;
            Container.AutoLayout();
        }

    }

    class DrawerGroup
    {
        public List<DrawerBox> allDrawerList = new List<DrawerBox>();
        public DrawerBox SelectedItem { get; set; }

        private Size _clientSize;
        public Size ClientSize
        {
            get { return _clientSize; }
            set
            {
                _clientSize = value;
                AutoLayout();
            }
        }
        
        public const int DrawerHeight= 30;
        public const int DrawerVmargin = 2;
        public const int DrawerHmargin = 2;

        public void AddDrawer(string title, ImageList icons, List<string> txts)
        {
            DrawerBox d = new DrawerBox(this, title, icons, txts);
            allDrawerList.Add(d);

            AutoLayout();
        }

        public void AutoLayout()
        {
            //先计算位置
            int viewMaxHeight = ClientSize.Height - allDrawerList.Count * (DrawerHeight + DrawerVmargin);
            int top = DrawerVmargin;
            foreach(var d in allDrawerList)
            {
                d.DrawerButton.Top = top;
                d.DrawerButton.Left = DrawerHmargin / 2;
                d.DrawerButton.Height = DrawerHeight;
                d.DrawerButton.Width = ClientSize.Width - DrawerHmargin;
                d.DrawerView.Width = ClientSize.Width - DrawerHmargin;
                d.DrawerView.Left = DrawerHmargin / 2;

                top += DrawerHeight + DrawerVmargin;
                if(SelectedItem == d)
                {
                    if (d.IsExpand)
                    {
                        d.DrawerView.Top = top;
                        d.DrawerView.Height = viewMaxHeight;
                        top += viewMaxHeight + DrawerVmargin;
                    }
                }
            }
        }
    }
}
