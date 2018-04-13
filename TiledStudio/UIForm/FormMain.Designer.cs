namespace TiledStudio
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sdfadsfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开工程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存工程CtrlSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加载地图文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.选择笔刷ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图层显隐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.阻挡区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.安全区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.碰撞区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.触发区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导航线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.巡逻线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.怪物区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nPC点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.摆放物ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.追击区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.出生点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.传送点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MakeXML = new System.Windows.Forms.ToolStripMenuItem();
            this.导出地图区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出安全区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出阻挡区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出碰撞区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出NPC区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出怪物区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出路线区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出传送点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出巡逻线ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出摆放物ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出触发区ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.制作场景元素ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Help = new System.Windows.Forms.ToolStripMenuItem();
            this.MainDockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.DlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.导入配表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入怪物表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入NPC表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入SceneObjectxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sdfadsfToolStripMenuItem,
            this.导入配表ToolStripMenuItem,
            this.加载地图文件ToolStripMenuItem,
            this.选择笔刷ToolStripMenuItem,
            this.图层显隐ToolStripMenuItem,
            this.MakeXML,
            this.工具ToolStripMenuItem,
            this.制作场景元素ToolStripMenuItem1,
            this.Help});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(861, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "MainMenu";
            // 
            // sdfadsfToolStripMenuItem
            // 
            this.sdfadsfToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开工程ToolStripMenuItem,
            this.保存工程CtrlSToolStripMenuItem});
            this.sdfadsfToolStripMenuItem.Name = "sdfadsfToolStripMenuItem";
            this.sdfadsfToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.sdfadsfToolStripMenuItem.Text = "工程";
            // 
            // 打开工程ToolStripMenuItem
            // 
            this.打开工程ToolStripMenuItem.Name = "打开工程ToolStripMenuItem";
            this.打开工程ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.打开工程ToolStripMenuItem.Text = "打开工程 Ctrl+E";
            this.打开工程ToolStripMenuItem.Click += new System.EventHandler(this.打开工程ToolStripMenuItem_Click);
            // 
            // 保存工程CtrlSToolStripMenuItem
            // 
            this.保存工程CtrlSToolStripMenuItem.Name = "保存工程CtrlSToolStripMenuItem";
            this.保存工程CtrlSToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.保存工程CtrlSToolStripMenuItem.Text = "保存工程 Ctrl+S";
            this.保存工程CtrlSToolStripMenuItem.Click += new System.EventHandler(this.保存工程CtrlSToolStripMenuItem_Click);
            // 
            // 加载地图文件ToolStripMenuItem
            // 
            this.加载地图文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.加载地图文件ToolStripMenuItem.Name = "加载地图文件ToolStripMenuItem";
            this.加载地图文件ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.加载地图文件ToolStripMenuItem.Text = "加载地图";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItem2.Text = "100%";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItem3.Text = "50%";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // 选择笔刷ToolStripMenuItem
            // 
            this.选择笔刷ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x1ToolStripMenuItem,
            this.x3ToolStripMenuItem,
            this.x5ToolStripMenuItem,
            this.x7ToolStripMenuItem});
            this.选择笔刷ToolStripMenuItem.Name = "选择笔刷ToolStripMenuItem";
            this.选择笔刷ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.选择笔刷ToolStripMenuItem.Text = "选择笔刷";
            // 
            // x1ToolStripMenuItem
            // 
            this.x1ToolStripMenuItem.Name = "x1ToolStripMenuItem";
            this.x1ToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.x1ToolStripMenuItem.Text = "1x1";
            // 
            // x3ToolStripMenuItem
            // 
            this.x3ToolStripMenuItem.Name = "x3ToolStripMenuItem";
            this.x3ToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.x3ToolStripMenuItem.Text = "3x3";
            // 
            // x5ToolStripMenuItem
            // 
            this.x5ToolStripMenuItem.Name = "x5ToolStripMenuItem";
            this.x5ToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.x5ToolStripMenuItem.Text = "5x5";
            // 
            // x7ToolStripMenuItem
            // 
            this.x7ToolStripMenuItem.Name = "x7ToolStripMenuItem";
            this.x7ToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.x7ToolStripMenuItem.Text = "7x7";
            // 
            // 图层显隐ToolStripMenuItem
            // 
            this.图层显隐ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.阻挡区ToolStripMenuItem,
            this.安全区ToolStripMenuItem,
            this.碰撞区ToolStripMenuItem,
            this.触发区ToolStripMenuItem,
            this.导航线ToolStripMenuItem,
            this.巡逻线ToolStripMenuItem,
            this.怪物区ToolStripMenuItem,
            this.nPC点ToolStripMenuItem,
            this.摆放物ToolStripMenuItem,
            this.追击区ToolStripMenuItem,
            this.出生点ToolStripMenuItem,
            this.传送点ToolStripMenuItem});
            this.图层显隐ToolStripMenuItem.Name = "图层显隐ToolStripMenuItem";
            this.图层显隐ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.图层显隐ToolStripMenuItem.Text = "图层显隐";
            // 
            // 阻挡区ToolStripMenuItem
            // 
            this.阻挡区ToolStripMenuItem.Checked = true;
            this.阻挡区ToolStripMenuItem.CheckOnClick = true;
            this.阻挡区ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.阻挡区ToolStripMenuItem.Name = "阻挡区ToolStripMenuItem";
            this.阻挡区ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.阻挡区ToolStripMenuItem.Text = "阻挡区";
            // 
            // 安全区ToolStripMenuItem
            // 
            this.安全区ToolStripMenuItem.Checked = true;
            this.安全区ToolStripMenuItem.CheckOnClick = true;
            this.安全区ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.安全区ToolStripMenuItem.Name = "安全区ToolStripMenuItem";
            this.安全区ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.安全区ToolStripMenuItem.Text = "安全区";
            // 
            // 碰撞区ToolStripMenuItem
            // 
            this.碰撞区ToolStripMenuItem.Checked = true;
            this.碰撞区ToolStripMenuItem.CheckOnClick = true;
            this.碰撞区ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.碰撞区ToolStripMenuItem.Name = "碰撞区ToolStripMenuItem";
            this.碰撞区ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.碰撞区ToolStripMenuItem.Text = "碰撞区";
            // 
            // 触发区ToolStripMenuItem
            // 
            this.触发区ToolStripMenuItem.Checked = true;
            this.触发区ToolStripMenuItem.CheckOnClick = true;
            this.触发区ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.触发区ToolStripMenuItem.Name = "触发区ToolStripMenuItem";
            this.触发区ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.触发区ToolStripMenuItem.Text = "触发区";
            // 
            // 导航线ToolStripMenuItem
            // 
            this.导航线ToolStripMenuItem.Checked = true;
            this.导航线ToolStripMenuItem.CheckOnClick = true;
            this.导航线ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.导航线ToolStripMenuItem.Name = "导航线ToolStripMenuItem";
            this.导航线ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.导航线ToolStripMenuItem.Text = "导航线";
            // 
            // 巡逻线ToolStripMenuItem
            // 
            this.巡逻线ToolStripMenuItem.Checked = true;
            this.巡逻线ToolStripMenuItem.CheckOnClick = true;
            this.巡逻线ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.巡逻线ToolStripMenuItem.Name = "巡逻线ToolStripMenuItem";
            this.巡逻线ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.巡逻线ToolStripMenuItem.Text = "巡逻线";
            // 
            // 怪物区ToolStripMenuItem
            // 
            this.怪物区ToolStripMenuItem.Checked = true;
            this.怪物区ToolStripMenuItem.CheckOnClick = true;
            this.怪物区ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.怪物区ToolStripMenuItem.Name = "怪物区ToolStripMenuItem";
            this.怪物区ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.怪物区ToolStripMenuItem.Text = "怪物区";
            // 
            // nPC点ToolStripMenuItem
            // 
            this.nPC点ToolStripMenuItem.Checked = true;
            this.nPC点ToolStripMenuItem.CheckOnClick = true;
            this.nPC点ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.nPC点ToolStripMenuItem.Name = "nPC点ToolStripMenuItem";
            this.nPC点ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.nPC点ToolStripMenuItem.Text = "NPC点";
            // 
            // 摆放物ToolStripMenuItem
            // 
            this.摆放物ToolStripMenuItem.Checked = true;
            this.摆放物ToolStripMenuItem.CheckOnClick = true;
            this.摆放物ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.摆放物ToolStripMenuItem.Name = "摆放物ToolStripMenuItem";
            this.摆放物ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.摆放物ToolStripMenuItem.Text = "摆放物";
            // 
            // 追击区ToolStripMenuItem
            // 
            this.追击区ToolStripMenuItem.Checked = true;
            this.追击区ToolStripMenuItem.CheckOnClick = true;
            this.追击区ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.追击区ToolStripMenuItem.Name = "追击区ToolStripMenuItem";
            this.追击区ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.追击区ToolStripMenuItem.Text = "追击区";
            // 
            // 出生点ToolStripMenuItem
            // 
            this.出生点ToolStripMenuItem.Checked = true;
            this.出生点ToolStripMenuItem.CheckOnClick = true;
            this.出生点ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.出生点ToolStripMenuItem.Name = "出生点ToolStripMenuItem";
            this.出生点ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.出生点ToolStripMenuItem.Text = "出生点";
            // 
            // 传送点ToolStripMenuItem
            // 
            this.传送点ToolStripMenuItem.Checked = true;
            this.传送点ToolStripMenuItem.CheckOnClick = true;
            this.传送点ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.传送点ToolStripMenuItem.Name = "传送点ToolStripMenuItem";
            this.传送点ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.传送点ToolStripMenuItem.Text = "传送点";
            // 
            // MakeXML
            // 
            this.MakeXML.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导出地图区ToolStripMenuItem,
            this.导出安全区ToolStripMenuItem,
            this.导出阻挡区ToolStripMenuItem,
            this.导出碰撞区ToolStripMenuItem,
            this.导出NPC区ToolStripMenuItem,
            this.导出怪物区ToolStripMenuItem,
            this.导出路线区ToolStripMenuItem,
            this.导出传送点ToolStripMenuItem,
            this.导出巡逻线ToolStripMenuItem,
            this.导出摆放物ToolStripMenuItem,
            this.导出触发区ToolStripMenuItem});
            this.MakeXML.Name = "MakeXML";
            this.MakeXML.Size = new System.Drawing.Size(70, 21);
            this.MakeXML.Text = "导出XML";
            // 
            // 导出地图区ToolStripMenuItem
            // 
            this.导出地图区ToolStripMenuItem.Name = "导出地图区ToolStripMenuItem";
            this.导出地图区ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.导出地图区ToolStripMenuItem.Text = "导出地图区";
            this.导出地图区ToolStripMenuItem.Click += new System.EventHandler(this.导出地图区ToolStripMenuItem_Click);
            // 
            // 导出安全区ToolStripMenuItem
            // 
            this.导出安全区ToolStripMenuItem.Name = "导出安全区ToolStripMenuItem";
            this.导出安全区ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.导出安全区ToolStripMenuItem.Text = "导出安全区";
            this.导出安全区ToolStripMenuItem.Click += new System.EventHandler(this.导出安全区ToolStripMenuItem_Click);
            // 
            // 导出阻挡区ToolStripMenuItem
            // 
            this.导出阻挡区ToolStripMenuItem.Name = "导出阻挡区ToolStripMenuItem";
            this.导出阻挡区ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.导出阻挡区ToolStripMenuItem.Text = "导出阻挡区";
            this.导出阻挡区ToolStripMenuItem.Click += new System.EventHandler(this.导出阻挡区ToolStripMenuItem_Click);
            // 
            // 导出碰撞区ToolStripMenuItem
            // 
            this.导出碰撞区ToolStripMenuItem.Name = "导出碰撞区ToolStripMenuItem";
            this.导出碰撞区ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.导出碰撞区ToolStripMenuItem.Text = "导出碰撞区";
            this.导出碰撞区ToolStripMenuItem.Click += new System.EventHandler(this.导出碰撞区ToolStripMenuItem_Click);
            // 
            // 导出NPC区ToolStripMenuItem
            // 
            this.导出NPC区ToolStripMenuItem.Name = "导出NPC区ToolStripMenuItem";
            this.导出NPC区ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.导出NPC区ToolStripMenuItem.Text = "导出NPC区";
            this.导出NPC区ToolStripMenuItem.Click += new System.EventHandler(this.导出NPC区ToolStripMenuItem_Click);
            // 
            // 导出怪物区ToolStripMenuItem
            // 
            this.导出怪物区ToolStripMenuItem.Name = "导出怪物区ToolStripMenuItem";
            this.导出怪物区ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.导出怪物区ToolStripMenuItem.Text = "导出怪物区";
            this.导出怪物区ToolStripMenuItem.Click += new System.EventHandler(this.导出怪物区ToolStripMenuItem_Click);
            // 
            // 导出路线区ToolStripMenuItem
            // 
            this.导出路线区ToolStripMenuItem.Name = "导出路线区ToolStripMenuItem";
            this.导出路线区ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.导出路线区ToolStripMenuItem.Text = "导出路线区";
            this.导出路线区ToolStripMenuItem.Click += new System.EventHandler(this.导出路线区ToolStripMenuItem_Click);
            // 
            // 导出传送点ToolStripMenuItem
            // 
            this.导出传送点ToolStripMenuItem.Name = "导出传送点ToolStripMenuItem";
            this.导出传送点ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.导出传送点ToolStripMenuItem.Text = "导出传送点";
            this.导出传送点ToolStripMenuItem.Click += new System.EventHandler(this.导出传送点ToolStripMenuItem_Click);
            // 
            // 导出巡逻线ToolStripMenuItem
            // 
            this.导出巡逻线ToolStripMenuItem.Name = "导出巡逻线ToolStripMenuItem";
            this.导出巡逻线ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.导出巡逻线ToolStripMenuItem.Text = "导出巡逻线";
            this.导出巡逻线ToolStripMenuItem.Click += new System.EventHandler(this.导出巡逻线ToolStripMenuItem_Click);
            // 
            // 导出摆放物ToolStripMenuItem
            // 
            this.导出摆放物ToolStripMenuItem.Name = "导出摆放物ToolStripMenuItem";
            this.导出摆放物ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.导出摆放物ToolStripMenuItem.Text = "导出摆放物";
            this.导出摆放物ToolStripMenuItem.Click += new System.EventHandler(this.导出摆放物ToolStripMenuItem_Click);
            // 
            // 导出触发区ToolStripMenuItem
            // 
            this.导出触发区ToolStripMenuItem.Name = "导出触发区ToolStripMenuItem";
            this.导出触发区ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.导出触发区ToolStripMenuItem.Text = "导出触发区";
            this.导出触发区ToolStripMenuItem.Click += new System.EventHandler(this.导出触发区ToolStripMenuItem_Click);
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.工具ToolStripMenuItem.Text = "AI编辑器";
            this.工具ToolStripMenuItem.Click += new System.EventHandler(this.aI编辑器ToolStripMenuItem_Click);
            // 
            // 制作场景元素ToolStripMenuItem1
            // 
            this.制作场景元素ToolStripMenuItem1.Name = "制作场景元素ToolStripMenuItem1";
            this.制作场景元素ToolStripMenuItem1.Size = new System.Drawing.Size(92, 21);
            this.制作场景元素ToolStripMenuItem1.Text = "制作场景元素";
            this.制作场景元素ToolStripMenuItem1.Click += new System.EventHandler(this.制作场景元素ToolStripMenuItem_Click);
            // 
            // Help
            // 
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(44, 21);
            this.Help.Text = "帮助";
            this.Help.Click += new System.EventHandler(this.Help_Click);
            // 
            // MainDockPanel
            // 
            this.MainDockPanel.ActiveAutoHideContent = null;
            this.MainDockPanel.AllowEndUserDocking = false;
            this.MainDockPanel.AllowEndUserNestedDocking = false;
            this.MainDockPanel.AutoSize = true;
            this.MainDockPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MainDockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainDockPanel.DockLeftPortion = 300D;
            this.MainDockPanel.Location = new System.Drawing.Point(0, 25);
            this.MainDockPanel.Name = "MainDockPanel";
            this.MainDockPanel.Size = new System.Drawing.Size(861, 514);
            this.MainDockPanel.TabIndex = 1;
            // 
            // 导入配表ToolStripMenuItem
            // 
            this.导入配表ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导入NPC表ToolStripMenuItem,
            this.导入怪物表ToolStripMenuItem,
            this.导入SceneObjectxmlToolStripMenuItem});
            this.导入配表ToolStripMenuItem.Name = "导入配表ToolStripMenuItem";
            this.导入配表ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.导入配表ToolStripMenuItem.Text = "导入配表";
            // 
            // 导入怪物表ToolStripMenuItem
            // 
            this.导入怪物表ToolStripMenuItem.Name = "导入怪物表ToolStripMenuItem";
            this.导入怪物表ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.导入怪物表ToolStripMenuItem.Text = "导入Monster表";
            this.导入怪物表ToolStripMenuItem.Click += new System.EventHandler(this.导入怪物表ToolStripMenuItem_Click);
            // 
            // 导入NPC表ToolStripMenuItem
            // 
            this.导入NPC表ToolStripMenuItem.Name = "导入NPC表ToolStripMenuItem";
            this.导入NPC表ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.导入NPC表ToolStripMenuItem.Text = "导入NPC表";
            this.导入NPC表ToolStripMenuItem.Click += new System.EventHandler(this.导入NPC表ToolStripMenuItem_Click);
            // 
            // 导入SceneObjectxmlToolStripMenuItem
            // 
            this.导入SceneObjectxmlToolStripMenuItem.Name = "导入SceneObjectxmlToolStripMenuItem";
            this.导入SceneObjectxmlToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.导入SceneObjectxmlToolStripMenuItem.Text = "导入SceneObject表";
            this.导入SceneObjectxmlToolStripMenuItem.Click += new System.EventHandler(this.导入SceneObjectxmlToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 539);
            this.Controls.Add(this.MainDockPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TiledStudio";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel MainDockPanel;
        private System.Windows.Forms.ToolStripMenuItem sdfadsfToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开工程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存工程CtrlSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择笔刷ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x7ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图层显隐ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 阻挡区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 安全区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 碰撞区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 触发区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导航线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 巡逻线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 怪物区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nPC点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 摆放物ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 追击区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 出生点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 传送点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeXML;
        private System.Windows.Forms.ToolStripMenuItem 导出地图区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出安全区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出阻挡区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出碰撞区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出NPC区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出怪物区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出路线区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出传送点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出巡逻线ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出摆放物ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出触发区ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Help;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加载地图文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 制作场景元素ToolStripMenuItem1;
        private System.Windows.Forms.SaveFileDialog DlgSaveFile;
        private System.Windows.Forms.ToolStripMenuItem 导入配表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入NPC表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入怪物表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入SceneObjectxmlToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog DlgOpenFile;
    }
}

