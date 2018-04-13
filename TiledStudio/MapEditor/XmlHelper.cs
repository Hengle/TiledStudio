using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Drawing;
using System.IO;
using System.Xml.Linq;

namespace TiledStudio
{
    class XmlHelper
    {
        XmlHelper() { }
        public static readonly XmlHelper Instance = new XmlHelper();

        public string Workspace { get; set; }
        public string ImageFileName { get; set; }
        public string ProjectName { get; set; }

        #region 保存和加载工程配置
        public void LoadProject(string filename, object grids)
        {
            XmlHelper.Instance.ProjectName = System.IO.Path.GetFileNameWithoutExtension(filename);
            var AllGrids = grids as Grid[,];

            XmlDocument xmlDoc = new XmlDocument();
            //清空现在的图层、图片、网格信息
            xmlDoc.Load(filename);
            //遍历root下所有图层
            XmlElement root = xmlDoc.SelectSingleNode("Root") as XmlElement;
            ImageFileName = root.GetAttribute("image");
            //图片
            FormMain.Instance.fmEditor.LoadImage(ImageFileName);

            int scale = int.Parse(root.GetAttribute("scale"));
            FormMain.Instance.fmEditor.InitMapScale(scale);
            int imageY = FormMain.Instance.fmEditor.GetImageGridNumY();

            //aqq
            var node = root.SelectSingleNode("aqq");
            if (node != null)
            {
                var nodelist = node.ChildNodes;
                SafeRegionArea area = DataHelper.Instance.AllAQQData;
                foreach (XmlNode nd in nodelist)
                {
                    //每个node是一个图层，grid属性是该图层节点坐标
                    MapFloor floor = new MapFloor(area);
                    XmlAttributeCollection attrs = nd.Attributes;
                    foreach (XmlAttribute attr in attrs)
                    {
                        if (attr.Name == "id")
                        {
                            floor.ID = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "depth")
                        {
                            floor.Depth = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "color")
                        {
                            floor.FloorColor = Color.FromArgb(int.Parse(attr.Value));
                        }
                        else if (attr.Name == "text")
                        {
                            floor.Text = attr.Value;
                        }
                        else if (attr.Name == "grid")
                        {
                            string[] pos = attr.Value.Split(',');
                            foreach (string s in pos)
                            {
                                if (string.IsNullOrEmpty(s)) continue;
                                string[] p = s.Split('_');
                                int x = int.Parse(p[0]);
                                int y = imageY - int.Parse(p[1]);
                                floor.grids.Add(AllGrids[x, y]);
                                AllGrids[x, y].floors.Add(floor);
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(floor.Text))
                    {
                        floor.Text = area.Text + floor.ID.ToString();
                    }

                    area.AddMapFloor(floor);
                }
                //UIHelper.Instance.FormAqq.LoadFromXML(area);

            }

            //obj
            node = root.SelectSingleNode("obj");
            if (node != null)
            {
                var nodelist = node.ChildNodes;
                ObstructionArea area = DataHelper.Instance.AllObsData;
                foreach (XmlNode nd in nodelist)
                {
                    //每个node是一个图层，grid属性是该图层节点坐标
                    MapFloor floor = new MapFloor(area);
                    XmlAttributeCollection attrs = nd.Attributes;
                    foreach (XmlAttribute attr in attrs)
                    {
                        if (attr.Name == "id")
                        {
                            floor.ID = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "depth")
                        {
                            floor.Depth = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "color")
                        {
                            floor.FloorColor = Color.FromArgb(int.Parse(attr.Value));
                        }
                        else if (attr.Name == "text")
                        {
                            floor.Text = attr.Value;
                        }
                        else if (attr.Name == "grid")
                        {
                            string[] pos = attr.Value.Split(',');
                            foreach (string s in pos)
                            {
                                if (string.IsNullOrEmpty(s)) continue;
                                string[] p = s.Split('_');
                                int x = int.Parse(p[0]);
                                int y = imageY - int.Parse(p[1]);
                                floor.grids.Add(AllGrids[x, y]);
                                AllGrids[x, y].floors.Add(floor);
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(floor.Text))
                    {
                        floor.Text = area.Text + floor.ID.ToString();
                    }

                    area.AddMapFloor(floor);
                }
                //UIHelper.Instance.FormObj.LoadFromXML(objarea);
            }

            //collision
            node = root.SelectSingleNode("collision");
            if (node != null)
            {
                var nodelist = node.ChildNodes;
                CollisionArea area = DataHelper.Instance.AllCollisionAreaData;
                foreach (XmlNode nd in nodelist)
                {
                    //每个node是一个图层，grid属性是该图层节点坐标
                    MapFloor floor = new MapFloor(area);
                    XmlAttributeCollection attrs = nd.Attributes;
                    foreach (XmlAttribute attr in attrs)
                    {
                        if (attr.Name == "id")
                        {
                            floor.ID = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "depth")
                        {
                            floor.Depth = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "color")
                        {
                            floor.FloorColor = Color.FromArgb(int.Parse(attr.Value));
                        }
                        else if (attr.Name == "text")
                        {
                            floor.Text = attr.Value;
                        }
                        else if (attr.Name == "grid")
                        {
                            string[] pos = attr.Value.Split(',');
                            foreach (string s in pos)
                            {
                                if (string.IsNullOrEmpty(s)) continue;
                                string[] p = s.Split('_');
                                int x = int.Parse(p[0]);
                                int y = imageY - int.Parse(p[1]);
                                floor.grids.Add(AllGrids[x, y]);
                                AllGrids[x, y].floors.Add(floor);
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(floor.Text))
                    {
                        floor.Text = area.Text + floor.ID.ToString();
                    }

                    area.AddMapFloor(floor);
                }
                //UIHelper.Instance.FormCollision.LoadFromXML(objarea);
            }

            //maparea
            node = root.SelectSingleNode("maparea");
            if (node != null)
            {
                var area = DataHelper.Instance.AllPursuitData;
                foreach (XmlNode nd in node.ChildNodes)
                {
                    //每个node是一个图层，grid属性是该图层节点坐标
                    MapFloor floor = new MapFloor(area);
                    XmlAttributeCollection attrs = nd.Attributes;
                    foreach (XmlAttribute attr in attrs)
                    {
                        if (attr.Name == "id")
                        {
                            floor.ID = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "depth")
                        {
                            floor.Depth = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "color")
                        {
                            floor.FloorColor = Color.FromArgb(int.Parse(attr.Value));
                        }
                        else if (attr.Name == "text")
                        {
                            floor.Text = attr.Value;
                        }
                        else if (attr.Name == "grid")
                        {
                            string[] pos = attr.Value.Split(',');
                            foreach (string s in pos)
                            {
                                if (string.IsNullOrEmpty(s)) continue;
                                string[] p = s.Split('_');
                                int x = int.Parse(p[0]);
                                int y = imageY - int.Parse(p[1]);
                                floor.grids.Add(AllGrids[x, y]);
                                AllGrids[x, y].floors.Add(floor);
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(floor.Text))
                    {
                        floor.Text = area.Text + floor.ID.ToString();
                    }

                    area.AddMapFloor(floor);
                }
                //UIHelper.Instance.FormMapArea.LoadFromXML(area);

            }

            //born
            node = root.SelectSingleNode("born");
            if (node != null)
            {
                var nodelist = node.ChildNodes;
                BornArea area = DataHelper.Instance.AllBornData;
                foreach (XmlNode nd in nodelist)
                {
                    //每个node是一个图层，grid属性是该图层节点坐标
                    MapFloor floor = new MapFloor(area);

                    XmlAttributeCollection attrs = nd.Attributes;
                    foreach (XmlAttribute attr in attrs)
                    {
                        if (attr.Name == "id")
                        {
                            floor.ID = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "depth")
                        {
                            floor.Depth = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "color")
                        {
                            floor.FloorColor = Color.FromArgb(int.Parse(attr.Value));
                        }
                        else if (attr.Name == "text")
                        {
                            floor.Text = attr.Value;
                        }
                        else if (attr.Name == "x")
                        {
                            floor.X = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "y")
                        {
                            floor.Y = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "Radius")
                        {
                            floor.Radius = int.Parse(attr.Value);
                        }
                    }
                    if (string.IsNullOrEmpty(floor.Text))
                    {
                        floor.Text = area.Text + floor.ID.ToString();
                    }

                    FormMain.Instance.fmEditor.MarkGridWithRadius(floor.X, floor.Y, floor.Radius, floor);

                    area.AddMapFloor(floor);
                }
                //UIHelper.Instance.FormBorn.LoadFromXML(bornarea);
            }

            //monster
            node = root.SelectSingleNode("monster");
            if (node != null)
            {
                var nodelist = node.ChildNodes;
                MonsterArea area = DataHelper.Instance.AllMonsterZoneData;
                area.ConfigFile = (node as XmlElement).GetAttribute("configfile");
                LoadMonsters(area.ConfigFile);

                foreach (XmlNode nd in nodelist)
                {
                    //每个node是一个图层，grid属性是该图层节点坐标
                    MonsterFloor floor = new MonsterFloor(area);
                    XmlAttributeCollection attrs = nd.Attributes;
                    foreach (XmlAttribute attr in attrs)
                    {
                        if (attr.Name == "id")
                        {
                            floor.ID = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "depth")
                        {
                            floor.Depth = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "color")
                        {
                            floor.FloorColor = Color.FromArgb(int.Parse(attr.Value));
                        }
                        else if (attr.Name == "text")
                        {
                            floor.Text = attr.Value;
                        }
                        else if (attr.Name == "X")
                        {
                            floor.X = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "Y")
                        {
                            floor.Y = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "Radius")
                        {
                            floor.Radius = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "num")
                        {
                            floor.MonsterNum = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "pursuitId")
                        {
                            floor.PursuitId = int.Parse(attr.Value);
                        }
                        else if(attr.Name == "monsterId")
                        {
                            floor.MonsterId = int.Parse(attr.Value);
                        }
                    }

                    if (string.IsNullOrEmpty(floor.Text))
                    {
                        floor.Text = area.Text + floor.ID.ToString() + " " + AllMonstersConfig[floor.MonsterId].name;
                    }

                    FormMain.Instance.fmEditor.MarkGridWithRadius(floor.X, floor.Y, floor.Radius, floor);
                    area.AddMapFloor(floor);
                }
                // UIHelper.Instance.FormMonster.LoadFromXML(mstarea);
            }


            //npc
            node = root.SelectSingleNode("npc");
            if (node != null)
            {
                var nodelist = node.ChildNodes;
                NPCArea area = DataHelper.Instance.AllNPCData;
                area.ConfigFile = (node as XmlElement).GetAttribute("configfile");
                LoadNpcs(area.ConfigFile);
                foreach (XmlNode nd in nodelist)
                {
                    //每个node是一个图层，grid属性是该图层节点坐标
                    NPCFloor floor = new NPCFloor(area);
                    XmlAttributeCollection attrs = nd.Attributes;
                    foreach (XmlAttribute attr in attrs)
                    {
                        if (attr.Name == "id")
                        {
                            floor.ID = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "depth")
                        {
                            floor.Depth = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "color")
                        {
                            floor.FloorColor = Color.FromArgb(int.Parse(attr.Value));
                        }
                        else if (attr.Name == "text")
                        {
                            floor.Text = attr.Value;
                        }
                        else if (attr.Name == "npcid")
                        {
                            floor.NPCID = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "dir")
                        {
                            floor.Dir = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "x")
                        {
                            floor.X = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "y")
                        {
                            floor.Y = int.Parse(attr.Value);
                        }
                    }


                    if (string.IsNullOrEmpty(floor.Text))
                    {
                        floor.Text = area.Text + floor.ID.ToString() + " " + AllNpcsConfig[floor.NPCID].name;
                    }

                    FormMain.Instance.fmEditor.MarkGridWithRadius(floor.X, floor.Y, floor.Radius, floor);
                    area.AddMapFloor(floor);
                    
                }
                //UIHelper.Instance.FormNPC.LoadFromXML(npcarea);
            }

            //teleport
            node = root.SelectSingleNode("teleport");
            if (node != null)
            {
                var nodelist = node.ChildNodes;
                TransportArea area = DataHelper.Instance.AllTeleportData;
                foreach (XmlNode nd in nodelist)
                {
                    //每个node是一个图层，grid属性是该图层节点坐标
                    TransportFloor floor = new TransportFloor(area);
                    XmlAttributeCollection attrs = nd.Attributes;
                    foreach (XmlAttribute attr in attrs)
                    {
                        if (attr.Name == "id")
                        {
                            floor.ID = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "depth")
                        {
                            floor.Depth = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "color")
                        {
                            floor.FloorColor = Color.FromArgb(int.Parse(attr.Value));
                        }
                        else if (attr.Name == "x")
                        {
                            floor.X = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "y")
                        {
                            floor.Y = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "text")
                        {
                            floor.Text = attr.Value;
                        }
                        else if (attr.Name == "Radius")
                        {
                            floor.Radius = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "tip")
                        {
                            floor.tip = attr.Value;
                        }
                        else if (attr.Name == "tox")
                        {
                            floor.tox = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "toy")
                        {
                            floor.toy = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "tomapid")
                        {
                            floor.toMapId = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "todir")
                        {
                            floor.todir = int.Parse(attr.Value);
                        }
                    }


                    if (string.IsNullOrEmpty(floor.Text))
                    {
                        floor.Text = area.Text + floor.ID.ToString();
                    }

                    FormMain.Instance.fmEditor.MarkGridWithRadius(floor.X, floor.Y, floor.Radius, floor);
                    area.AddMapFloor(floor);
                }
                //UIHelper.Instance.FormTeleport.LoadFromXML(teleportarea);
            }

            //path
            node = root.SelectSingleNode("path");
            if (node != null)
            {
                var nodelist = node.ChildNodes;
                NavigateArea area = DataHelper.Instance.AllNavigatePathData;
                foreach (XmlNode nd in nodelist)
                {
                    //每个node是一个图层，grid属性是该图层节点坐标
                    MapFloor floor = new Path(area);
                    XmlAttributeCollection attrs = nd.Attributes;
                    foreach (XmlAttribute attr in attrs)
                    {
                        if (attr.Name == "id")
                        {
                            floor.ID = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "depth")
                        {
                            floor.Depth = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "color")
                        {
                            floor.FloorColor = Color.FromArgb(int.Parse(attr.Value));
                        }
                        else if (attr.Name == "text")
                        {
                            floor.Text = attr.Value;
                        }
                        else if (attr.Name == "grid")
                        {
                            string[] lines = attr.Value.Split('|');
                            foreach (var line in lines)
                            {
                                string[] points = line.Split(',');

                                string[] p = points[0].Split('_');
                                int x = int.Parse(p[0]);
                                int y = imageY - int.Parse(p[1]);
                                Grid startGrid = AllGrids[x, y];
                                floor.grids.Add(startGrid);
                                startGrid.floors.Add(floor);

                                string[] q = points[1].Split('_');
                                x = int.Parse(q[0]);
                                y = imageY - int.Parse(q[1]);
                                Grid endGrid = AllGrids[x, y];
                                floor.grids.Add(endGrid);
                                endGrid.floors.Add(floor);

                                var _path = floor as Path;
                                _path.OnDrawKeyPoint(startGrid);
                                _path.OnDrawKeyPoint(endGrid);
                                _path.IsDrawing = false;
                                _path.LastMousePoint.X = 0;
                            }
                        }
                    }


                    if (string.IsNullOrEmpty(floor.Text))
                    {
                        floor.Text = area.Text + floor.ID.ToString();
                    }

                    area.AddMapFloor(floor);
                }
                //UIHelper.Instance.FormPath.LoadFromXML(patharea);
            }

            //patrol
            node = root.SelectSingleNode("patrol");
            if (node != null)
            {
                var nodelist = node.ChildNodes;
                PatrolArea area = DataHelper.Instance.AllPatrolArea;
                foreach (XmlNode nd in nodelist)
                {
                    //每个node是一个图层，grid属性是该图层节点坐标
                    Path floor = new Path(area);
                    XmlAttributeCollection attrs = nd.Attributes;
                    foreach (XmlAttribute attr in attrs)
                    {
                        if (attr.Name == "id")
                        {
                            floor.ID = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "depth")
                        {
                            floor.Depth = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "color")
                        {
                            floor.FloorColor = Color.FromArgb(int.Parse(attr.Value));
                        }
                        else if (attr.Name == "text")
                        {
                            floor.Text = attr.Value;
                        }
                        else if (attr.Name == "isclose")
                        {
                            floor.IsClosePath = Convert.ToBoolean(attr.Value);
                        }
                        else if (attr.Name == "grid")
                        {
                            string[] lines = attr.Value.Split('|');
                            foreach (var line in lines)
                            {
                                string[] points = line.Split(',');

                                string[] p = points[0].Split('_');
                                int x = int.Parse(p[0]);
                                int y = imageY - int.Parse(p[1]);
                                Grid startGrid = AllGrids[x, y];
                                floor.grids.Add(startGrid);
                                startGrid.floors.Add(floor);

                                string[] q = points[1].Split('_');
                                x = int.Parse(q[0]);
                                y = imageY - int.Parse(q[1]);
                                Grid endGrid = AllGrids[x, y];
                                floor.grids.Add(endGrid);
                                endGrid.floors.Add(floor);

                                var _path = floor as Path;
                                _path.OnDrawKeyPoint(startGrid);
                                _path.OnDrawKeyPoint(endGrid);
                                _path.IsDrawing = false;
                                _path.LastMousePoint.X = 0;
                            }
                        }
                    }


                    if (string.IsNullOrEmpty(floor.Text))
                    {
                        floor.Text = area.Text + floor.ID.ToString();
                    }

                    area.AddMapFloor(floor);
                }
                //UIHelper.Instance.FormPatrol.LoadFromXML(patharea);
            }

            //触发区
            node = root.SelectSingleNode("trigger");
            if (node != null)
            {
                var area = DataHelper.Instance.AllTriggerAreaData;
                foreach (XmlNode nd in node.ChildNodes)
                {
                    var floor = new MapFloor(area);
                    XmlAttributeCollection attrs = nd.Attributes;
                    foreach (XmlAttribute attr in attrs)
                    {
                        if (attr.Name == "id")
                        {
                            floor.ID = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "depth")
                        {
                            floor.Depth = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "color")
                        {
                            floor.FloorColor = Color.FromArgb(int.Parse(attr.Value));
                        }
                        else if (attr.Name == "text")
                        {
                            floor.Text = attr.Value;
                        }
                        else if (attr.Name == "grid")
                        {
                            string[] pos = attr.Value.Split(',');
                            foreach (string s in pos)
                            {
                                if (string.IsNullOrEmpty(s)) continue;
                                string[] p = s.Split('_');
                                int x = int.Parse(p[0]);
                                int y = imageY - int.Parse(p[1]);
                                floor.grids.Add(AllGrids[x, y]);
                                AllGrids[x, y].floors.Add(floor);
                            }
                        }
                    }


                    if (string.IsNullOrEmpty(floor.Text))
                    {
                        floor.Text = area.Text + floor.ID.ToString();
                    }

                    area.AddMapFloor(floor);
                }
                //UIHelper.Instance.FormTragger.LoadFromXML(area);

            }

            //场景摆放物
            node = root.SelectSingleNode("sceneobjs");
            if (node != null)
            {
                var nodelist = node.ChildNodes;
                var area = DataHelper.Instance.AllSceneObjAreaData;
                area.ConfigFile = (node as XmlElement).GetAttribute("config");
                LoadSceneObjects(area.ConfigFile);

                foreach (XmlNode nd in nodelist)
                {
                    //每个node是一个图层，grid属性是该图层节点坐标
                    var floor = new SceneObjFloor(area);
                    XmlAttributeCollection attrs = nd.Attributes;
                    foreach (XmlAttribute attr in attrs)
                    {
                        if (attr.Name == "id")
                        {
                            floor.ID = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "depth")
                        {
                            floor.Depth = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "color")
                        {
                            floor.FloorColor = Color.FromArgb(int.Parse(attr.Value));
                        }
                        else if (attr.Name == "text")
                        {
                            floor.Text = attr.Value;
                        }
                        else if (attr.Name == "dir")
                        {
                            floor.Dir = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "objid")
                        {
                            floor.objID = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "num")
                        {
                            floor.num = int.Parse(attr.Value);
                        }
                        else if (attr.Name == "isrand")
                        {
                            floor.isRandObj = Convert.ToBoolean(attr.Value);
                        }
                        else if (attr.Name == "grid")
                        {
                            string[] pos = attr.Value.Split(',');
                            foreach (string s in pos)
                            {
                                if (string.IsNullOrEmpty(s)) continue;
                                string[] p = s.Split('_');
                                int x = int.Parse(p[0]);
                                int y = imageY - int.Parse(p[1]);
                                floor.RandXYGrids.Add(AllGrids[x, y]);
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(floor.Text))
                    {
                        floor.Text = area.Text+floor.ID.ToString()+" "+ AllSceneObjsConfig[floor.objID].name;
                    }
                    floor.SelectItem = AllSceneObjsConfig[floor.objID];
                    var plist = floor.SelectItem.plist[floor.Dir];
                    foreach (var grid in floor.RandXYGrids)
                    {
                        FormMain.Instance.fmEditor.MarkGridWithList(grid.gridX, grid.gridY, plist, floor);
                    }


                    area.AddMapFloor(floor);
                }
                // UIHelper.Instance.FormSceneobj.LoadFromXML(area);
            }

            FormMain.Instance.fmSolution.LoadFromArea();
        }




        public void SaveProject(string filename, object grids)
        {
            var AllGrids = grids as Grid[,];
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = null;
            root = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(root);
            //记录图片路径
            root.SetAttribute("image", ImageFileName);
            root.SetAttribute("scale", FormMain.Instance.fmEditor.nImportMapScale.ToString());
            //安全区
            if (DataHelper.Instance.AllAQQData.floors.Count > 0)
            {
                var node = xmlDoc.CreateElement("aqq");
                root.AppendChild(node);
                foreach (var floor in DataHelper.Instance.AllAQQData.floors.Values)
                {
                    var xml = xmlDoc.CreateElement("a");
                    node.AppendChild(xml);

                    StringBuilder sb = new StringBuilder();
                    foreach (var g in floor.grids)
                    {
                        sb.AppendFormat("{0}_{1},", g.gridX, g.gridY);
                    }
                    if (sb.Length > 1)
                    {
                        sb.Length--;
                    }

                    xml.SetAttribute("id", floor.ID.ToString());
                    xml.SetAttribute("depth", floor.Depth.ToString());
                    xml.SetAttribute("color", floor.FloorColor.ToArgb().ToString());
                    xml.SetAttribute("text", floor.Text);
                    xml.SetAttribute("grid", sb.ToString());
                }
            }

            //阻挡区
            if (DataHelper.Instance.AllObsData.floors.Count > 0)
            {
                var node = xmlDoc.CreateElement("obj");
                root.AppendChild(node);
                foreach (var floor in DataHelper.Instance.AllObsData.floors.Values)
                {
                    var xml = xmlDoc.CreateElement("o");
                    node.AppendChild(xml);

                    StringBuilder sb = new StringBuilder();
                    foreach (var g in floor.grids)
                    {
                        sb.AppendFormat("{0}_{1},", g.gridX, g.gridY);
                    }
                    if (sb.Length > 0)
                        sb.Length--;
                    xml.SetAttribute("id", floor.ID.ToString());
                    xml.SetAttribute("depth", floor.Depth.ToString());
                    xml.SetAttribute("color", floor.FloorColor.ToArgb().ToString());
                    xml.SetAttribute("text", floor.Text);
                    xml.SetAttribute("grid", sb.ToString());
                }
            }

            //碰撞区
            if (DataHelper.Instance.AllCollisionAreaData.floors.Count > 0)
            {
                var node = xmlDoc.CreateElement("collision");
                root.AppendChild(node);
                foreach (var floor in DataHelper.Instance.AllCollisionAreaData.floors.Values)
                {
                    var xml = xmlDoc.CreateElement("c");
                    node.AppendChild(xml);

                    StringBuilder sb = new StringBuilder();
                    foreach (var g in floor.grids)
                    {
                        sb.AppendFormat("{0}_{1},", g.gridX, g.gridY);
                    }
                    if (sb.Length > 0)
                        sb.Length--;
                    xml.SetAttribute("id", floor.ID.ToString());
                    xml.SetAttribute("depth", floor.Depth.ToString());
                    xml.SetAttribute("color", floor.FloorColor.ToArgb().ToString());
                    xml.SetAttribute("text", floor.Text);
                    xml.SetAttribute("grid", sb.ToString());
                }
            }

            //地图区域
            if (DataHelper.Instance.AllPursuitData.floors.Count > 0)
            {
                var node = xmlDoc.CreateElement("maparea");
                root.AppendChild(node);
                foreach (var floor in DataHelper.Instance.AllPursuitData.floors.Values)
                {
                    var xml = xmlDoc.CreateElement("a");
                    node.AppendChild(xml);

                    StringBuilder sb = new StringBuilder();
                    foreach (var g in floor.grids)
                    {
                        sb.AppendFormat("{0}_{1},", g.gridX, g.gridY);
                    }
                    if (sb.Length > 0)
                        sb.Length--;
                    xml.SetAttribute("id", floor.ID.ToString());
                    xml.SetAttribute("depth", floor.Depth.ToString());
                    xml.SetAttribute("color", floor.FloorColor.ToArgb().ToString());
                    xml.SetAttribute("text", floor.Text);
                    xml.SetAttribute("grid", sb.ToString());
                }
            }

            //npc
            if (DataHelper.Instance.AllNPCData.floors.Count > 0)
            {
                var node = xmlDoc.CreateElement("npc");
                node.SetAttribute("configfile", DataHelper.Instance.AllNPCData.ConfigFile);
                root.AppendChild(node);
                foreach (var floor in DataHelper.Instance.AllNPCData.floors.Values)
                {
                    var xml = xmlDoc.CreateElement("n");
                    node.AppendChild(xml);

                    NPCFloor npc = floor as NPCFloor;
                    xml.SetAttribute("id", floor.ID.ToString());
                    xml.SetAttribute("depth", floor.Depth.ToString());
                    xml.SetAttribute("color", floor.FloorColor.ToArgb().ToString());
                    xml.SetAttribute("text", floor.Text);
                    xml.SetAttribute("npcid", npc.NPCID.ToString());
                    //xml.SetAttribute("index", npc.itemIndex.ToString());
                    xml.SetAttribute("dir", npc.Dir.ToString());
                    xml.SetAttribute("x", npc.X.ToString());
                    xml.SetAttribute("y", npc.Y.ToString());
                }

            }

            //场景对象
            if (DataHelper.Instance.AllSceneObjAreaData.floors.Count > 0)
            {
                var node = xmlDoc.CreateElement("sceneobjs");
                node.SetAttribute("configfile", DataHelper.Instance.AllSceneObjAreaData.ConfigFile);
                root.AppendChild(node);
                foreach (var floor in DataHelper.Instance.AllSceneObjAreaData.floors.Values)
                {
                    var xml = xmlDoc.CreateElement("Item");
                    node.AppendChild(xml);

                    var obj = floor as SceneObjFloor;
                    xml.SetAttribute("id", floor.ID.ToString());
                    xml.SetAttribute("depth", floor.Depth.ToString());
                    xml.SetAttribute("color", floor.FloorColor.ToArgb().ToString());
                    xml.SetAttribute("text", floor.Text);
                    xml.SetAttribute("dir", obj.Dir.ToString());
                    xml.SetAttribute("objid", obj.objID.ToString());
                    xml.SetAttribute("num", obj.num.ToString());
                    xml.SetAttribute("isrand", obj.isRandObj.ToString());
                    StringBuilder sb = new StringBuilder();
                    foreach (var g in obj.RandXYGrids)
                    {
                        sb.AppendFormat("{0}_{1},", g.gridX, g.gridY);
                    }
                    if (sb.Length > 1)
                        sb.Length--;

                    xml.SetAttribute("grid", sb.ToString());
                }

            }

            //monster
            if (DataHelper.Instance.AllMonsterZoneData.floors.Count > 0)
            {
                var node = xmlDoc.CreateElement("monster");
                node.SetAttribute("configfile", DataHelper.Instance.AllMonsterZoneData.ConfigFile);
                root.AppendChild(node);
                foreach (var floor in DataHelper.Instance.AllMonsterZoneData.floors.Values)
                {
                    var xml = xmlDoc.CreateElement("m");
                    node.AppendChild(xml);

                    var mz = floor as MonsterFloor;
                    xml.SetAttribute("id", floor.ID.ToString());
                    xml.SetAttribute("depth", floor.Depth.ToString());
                    xml.SetAttribute("color", floor.FloorColor.ToArgb().ToString());
                    xml.SetAttribute("text", floor.Text);
                    xml.SetAttribute("monsterId", mz.MonsterId.ToString());
                    xml.SetAttribute("Radius", mz.Radius.ToString());
                    xml.SetAttribute("num", mz.MonsterNum.ToString());
                    xml.SetAttribute("X", mz.X.ToString());
                    xml.SetAttribute("Y", mz.Y.ToString());
                    xml.SetAttribute("pursuitId", mz.PursuitId.ToString());
                }

            }

            //path
            if (DataHelper.Instance.AllNavigatePathData.floors.Count > 0)
            {
                var node = xmlDoc.CreateElement("path");
                root.AppendChild(node);
                foreach (var floor in DataHelper.Instance.AllNavigatePathData.floors.Values)
                {
                    var xml = xmlDoc.CreateElement("p");
                    node.AppendChild(xml);

                    StringBuilder sb = new StringBuilder();
                    var _path = floor as Path;
                    foreach (var e in _path.AllPathEdges)
                    {
                        sb.AppendFormat("{0}_{1},{2}_{3}|", e.StartGrid.gridX, e.StartGrid.gridY, e.EndGrid.gridX, e.EndGrid.gridY);
                    }
                    if (sb.Length > 1)
                        sb.Length--;
                    xml.SetAttribute("id", floor.ID.ToString());
                    xml.SetAttribute("depth", floor.Depth.ToString());
                    xml.SetAttribute("color", floor.FloorColor.ToArgb().ToString());
                    xml.SetAttribute("text", floor.Text);
                    xml.SetAttribute("grid", sb.ToString());
                }

            }

            //patrol
            if (DataHelper.Instance.AllPatrolArea.floors.Count > 0)
            {
                var node = xmlDoc.CreateElement("patrol");
                root.AppendChild(node);
                foreach (var floor in DataHelper.Instance.AllPatrolArea.floors.Values)
                {
                    var xml = xmlDoc.CreateElement("p");
                    node.AppendChild(xml);

                    StringBuilder sb = new StringBuilder();
                    var _path = floor as Path;
                    foreach (var e in _path.AllPathEdges)
                    {
                        sb.AppendFormat("{0}_{1},{2}_{3}|", e.StartGrid.gridX, e.StartGrid.gridY, e.EndGrid.gridX, e.EndGrid.gridY);
                    }
                    if (sb.Length > 1)
                        sb.Length--;
                    xml.SetAttribute("id", floor.ID.ToString());
                    xml.SetAttribute("depth", floor.Depth.ToString());
                    xml.SetAttribute("color", floor.FloorColor.ToArgb().ToString());
                    xml.SetAttribute("text", floor.Text);
                    xml.SetAttribute("isclose", _path.IsClosePath.ToString());
                    xml.SetAttribute("grid", sb.ToString());
                }

            }


            //teleport
            if (DataHelper.Instance.AllTeleportData.floors.Count > 0)
            {
                var node = xmlDoc.CreateElement("teleport");
                root.AppendChild(node);
                foreach (var floor in DataHelper.Instance.AllTeleportData.floors.Values)
                {
                    var xml = xmlDoc.CreateElement("t");
                    node.AppendChild(xml);

                    var p = floor as TransportFloor;
                    xml.SetAttribute("id", floor.ID.ToString());
                    xml.SetAttribute("depth", floor.Depth.ToString());
                    xml.SetAttribute("color", floor.FloorColor.ToArgb().ToString());
                    xml.SetAttribute("text", floor.Text);
                    xml.SetAttribute("tip", p.tip);
                    xml.SetAttribute("x", p.X.ToString());
                    xml.SetAttribute("y", p.Y.ToString());
                    xml.SetAttribute("Radius", p.Radius.ToString());
                    xml.SetAttribute("tox", p.tox.ToString());
                    xml.SetAttribute("toy", p.toy.ToString());
                    xml.SetAttribute("tomapid", p.toMapId.ToString());
                    xml.SetAttribute("todir", p.todir.ToString());
                }

            }

            //born
            if (DataHelper.Instance.AllBornData.floors.Count > 0)
            {
                var node = xmlDoc.CreateElement("born");
                root.AppendChild(node);
                foreach (var floor in DataHelper.Instance.AllBornData.floors.Values)
                {
                    var xml = xmlDoc.CreateElement("b");
                    node.AppendChild(xml);

                    xml.SetAttribute("id", floor.ID.ToString());
                    xml.SetAttribute("depth", floor.Depth.ToString());
                    xml.SetAttribute("color", floor.FloorColor.ToArgb().ToString());
                    xml.SetAttribute("text", floor.Text);
                    xml.SetAttribute("x", floor.X.ToString());
                    xml.SetAttribute("y", floor.Y.ToString());
                    xml.SetAttribute("Radius", floor.Radius.ToString());
                }

            }

            //trigger
            if (DataHelper.Instance.AllTriggerAreaData.floors.Count > 0)
            {
                var node = xmlDoc.CreateElement("trigger");
                root.AppendChild(node);
                foreach (var floor in DataHelper.Instance.AllTriggerAreaData.floors.Values)
                {
                    var xml = xmlDoc.CreateElement("item");
                    node.AppendChild(xml);
                    StringBuilder sb = new StringBuilder();
                    foreach (var g in floor.grids)
                    {
                        sb.AppendFormat("{0}_{1},", g.gridX, g.gridY);
                    }
                    if (sb.Length > 0)
                        sb.Length--;
                    xml.SetAttribute("id", floor.ID.ToString());
                    xml.SetAttribute("depth", floor.Depth.ToString());
                    xml.SetAttribute("color", floor.FloorColor.ToArgb().ToString());
                    xml.SetAttribute("text", floor.Text);
                    xml.SetAttribute("grid", sb.ToString());
                }
            }

            xmlDoc.Save(filename);

        }

        #endregion

        #region 导入配置文件

        public Dictionary<int, MonsterConfigItem> AllMonstersConfig = new Dictionary<int, MonsterConfigItem>();
        public void LoadMonsters(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return;
            }
            XDocument doc = XDocument.Load(filename);
            var elems = doc.Element("Config");

            foreach (var elem in elems.Elements())
            {
                int id = int.Parse(elem.Attribute("ID").Value);
                string name = elem.Attribute("SName").Value + "(map:" + elem.Attribute("MapCode").Value + ")";
                var item = new MonsterConfigItem { monsterID=id, name = name};
                AllMonstersConfig.Add(id, item);
            }
        }


        public Dictionary<int, NpcConfigItem> AllNpcsConfig = new Dictionary<int, NpcConfigItem>();
        public void LoadNpcs(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return;
            }
            XDocument doc = XDocument.Load(filename);
            XElement root = doc.Element("Config");

            foreach (var elem in root.Elements())
            {
                int npcid = int.Parse(elem.Attribute("ID").Value);
                string name = elem.Attribute("SName").Value;
                string func = elem.Attribute("Function").Value;

                var item = new NpcConfigItem { npcID = npcid, name = $"{func} {name}" };
                AllNpcsConfig.Add(npcid, item);
            }
        }

        public Dictionary<int, SceneObjConfigItem> AllSceneObjsConfig = new Dictionary<int, SceneObjConfigItem>();
        public void LoadSceneObjects(string file)
        {
            var xml = XElement.Load(file);
            foreach (var e in xml.Elements())
            {
                var item = new SceneObjConfigItem();
                item.id = Convert.ToInt32(e.Attribute("ID").Value);
                item.name = e.Attribute("Name").Value;
                item.plist = new List<SceneObjPoint>[8];
                for (int i = 0; i < 8; i++)
                {
                    item.plist[i] = new List<SceneObjPoint>();
                    string ds = e.Attribute("D" + i).Value;
                    var fs = ds.Split('|');
                    if (!string.IsNullOrEmpty(fs[0]))
                    {
                        foreach (var s in fs[0].Split(','))
                        {
                            var p = s.Split('_');
                            int x = Convert.ToInt32(p[0]);
                            int y = Convert.ToInt32(p[1]);
                            item.plist[i].Add(new SceneObjPoint(x, y, false));
                        }
                    }
                    if (!string.IsNullOrEmpty(fs[1]))
                    {
                        foreach (var s in fs[1].Split(','))
                        {
                            var p = s.Split('_');
                            int x = Convert.ToInt32(p[0]);
                            int y = Convert.ToInt32(p[1]);
                            item.plist[i].Add(new SceneObjPoint(x, y, true));
                        }
                    }


                }
                AllSceneObjsConfig.Add(item.id, item);
            }
        }

        #endregion
        #region 导出XML

        public void ExportAQQ(string filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Item");
            doc.AppendChild(root);
            foreach (var floor in DataHelper.Instance.AllAQQData.floors)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var g in floor.Value.grids)
                {
                    if (g.gridY < 0)
                    {
                        continue;
                    }
                    sb.AppendFormat("{0}_{1},", g.gridX, g.gridY);
                }
                if (sb.Length > 1)
                    sb.Length--;
                root.SetAttribute("ID", floor.Key.ToString());
                if (sb.Length <= 0)
                {
                    root.SetAttribute("Value", "");
                }
                else
                {
                    root.SetAttribute("Value", sb.ToString());
                }
                break;//只有第一个会存下来
            }

            doc.Save(filename);
        }

        internal void ExportTrigger(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Config");
            doc.AppendChild(root);
            foreach (var floor in DataHelper.Instance.AllTriggerAreaData.floors)
            {
                XmlElement xml = doc.CreateElement("Item");
                root.AppendChild(xml);
                StringBuilder sb = new StringBuilder();
                foreach (var g in floor.Value.grids)
                {
                    //出现了y值<0的情况，直接不导出即可
                    if (g.gridY < 0)
                    {
                        continue;
                    }
                    sb.AppendFormat("{0}_{1},", g.gridX, g.gridY);
                }
                if (sb.Length > 0)
                    sb.Length--;
                xml.SetAttribute("ID", floor.Key.ToString());
                xml.SetAttribute("Value", sb.Length <= 0 ? "" : sb.ToString());
            }

            doc.Save(fileName);
        }

        public void ExportOBJ(string filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Item");
            doc.AppendChild(root);
            foreach (var floor in DataHelper.Instance.AllObsData.floors.Values)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var g in floor.grids)
                {
                    //出现了y值<0的情况，直接不导出即可
                    if (g.gridY < 0)
                    {
                        continue;
                    }
                    sb.AppendFormat("{0}_{1},", g.gridX, g.gridY);
                }
                if (sb.Length > 1)
                    sb.Length--;
                root.SetAttribute("ID", floor.ID.ToString());
                root.SetAttribute("Value", sb.Length <= 0 ? "" : sb.ToString());
                break;//只有第一个会存下来
            }

            doc.Save(filename);

        }

        public void ExportCollision(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Config");
            doc.AppendChild(root);
            foreach (var floor in DataHelper.Instance.AllCollisionAreaData.floors.Values)
            {
                var item = doc.CreateElement("Item");
                StringBuilder sb = new StringBuilder();
                foreach (var g in floor.grids)
                {
                    //出现了y值<0的情况，直接不导出即可
                    if (g.gridY < 0)
                    {
                        continue;
                    }
                    sb.AppendFormat("{0}_{1},", g.gridX, g.gridY);
                }
                if (sb.Length > 1)
                    sb.Length--;
                item.SetAttribute("ID", floor.ID.ToString());
                item.SetAttribute("Value", sb.Length <= 0 ? "" : sb.ToString());
                root.AppendChild(item);
            }

            doc.Save(fileName);

        }

        public void ExportArea(string filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Map");
            doc.AppendChild(root);
            foreach (var floor in DataHelper.Instance.AllPursuitData.floors.Values)
            {
                XmlElement xml = doc.CreateElement("Area");
                root.AppendChild(xml);
                StringBuilder sb = new StringBuilder();
                foreach (var g in floor.grids)
                {
                    //出现了y值<0的情况，直接不导出即可
                    if (g.gridY < 0)
                    {
                        continue;
                    }
                    sb.AppendFormat("{0}_{1},", g.gridX, g.gridY);
                }
                if (sb.Length > 0)
                    sb.Length--;
                xml.SetAttribute("ID", floor.ID.ToString());
                xml.SetAttribute("Value", sb.Length <= 0 ? "" : sb.ToString());
            }

            doc.Save(filename);

        }

        public void ExportNPC(string filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Map");
            XmlElement npcs = doc.CreateElement("NPCs");
            doc.AppendChild(root);
            root.AppendChild(npcs);
            //int kGridSize = UIHelper.Instance.MainForm.kGridSize;
            foreach (var floor in DataHelper.Instance.AllNPCData.floors)
            {
                XmlElement npc = doc.CreateElement("NPC");
                NPCFloor obj = floor.Value as NPCFloor;
                npc.SetAttribute("ID", obj.ID.ToString());
                npc.SetAttribute("Code", obj.NPCID.ToString());
                npc.SetAttribute("X", obj.X.ToString());
                npc.SetAttribute("Y", obj.Y.ToString());
                npc.SetAttribute("Dir", obj.Dir.ToString());
                npcs.AppendChild(npc);
            }

            doc.Save(filename);

        }

        public void ExportMonster(string filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Map");
            XmlElement npcs = doc.CreateElement("Monsters");
            doc.AppendChild(root);
            root.AppendChild(npcs);
            //int kGridSize = UIHelper.Instance.MainForm.kGridSize;

            foreach (var floor in DataHelper.Instance.AllMonsterZoneData.floors)
            {
                XmlElement monster = doc.CreateElement("Monster");
                var obj = floor.Value as MonsterFloor;
                monster.SetAttribute("ID", obj.ID.ToString());
                monster.SetAttribute("Code", obj.MonsterId.ToString());
                //int x = obj.gridX / 2 * kGridSize + kGridSize / 2;
                monster.SetAttribute("X", obj.X.ToString());
                //int y = obj.gridY / 2 * kGridSize + kGridSize / 2;
                monster.SetAttribute("Y", obj.Y.ToString());
                monster.SetAttribute("Radius", obj.Radius.ToString());
                monster.SetAttribute("Num", obj.MonsterNum.ToString());
                monster.SetAttribute("MapArea", obj.PursuitId.ToString());
                monster.SetAttribute("TimeSlot", "3");
                monster.SetAttribute("PursuitRadius", "10");
                monster.SetAttribute("BirthType", "0");
                monster.SetAttribute("TimePoints", "0");
                monster.SetAttribute("BirthRate", "1");

                npcs.AppendChild(monster);
            }

            doc.Save(filename);

        }

        public void ExportPath(string filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Path");
            doc.AppendChild(root);
            //int kGridSize = UIHelper.Instance.MainForm.kGridSize;

            foreach (var floor in DataHelper.Instance.AllNavigatePathData.floors)
            {
                XmlElement path = doc.CreateElement("Item");
                root.AppendChild(path);
                StringBuilder sb = new StringBuilder();
                var _path = floor.Value as Path;
                foreach (var e in _path.AllPathEdges)
                {
                    sb.AppendFormat("{0}_{1},{2}_{3}|", e.StartGrid.gridX, e.StartGrid.gridY, e.EndGrid.gridX, e.EndGrid.gridY);
                }
                if (sb.Length > 1)
                    sb.Length--;
                path.SetAttribute("ID", floor.Key.ToString());
                path.SetAttribute("Value", sb.ToString());
            }

            doc.Save(filename);

        }

        public void ExportPatrol(string filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Path");
            doc.AppendChild(root);

            foreach (var floor in DataHelper.Instance.AllPatrolArea.floors)
            {
                XmlElement path = doc.CreateElement("Item");
                root.AppendChild(path);
                StringBuilder sb = new StringBuilder();
                var _path = floor.Value as Path;
                foreach (var e in _path.AllPathPoints)
                {
                    sb.AppendFormat("{0},{1}|", e.grid.gridX, e.grid.gridY);
                }
                if (sb.Length > 1)
                    sb.Length--;
                path.SetAttribute("ID", floor.Key.ToString());
                path.SetAttribute("IsClose", _path.IsClosePath.ToString());
                path.SetAttribute("Value", sb.ToString());
            }

            doc.Save(filename);

        }

        public void ExportTeleport(string filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Map");
            XmlElement npcs = doc.CreateElement("Teleports");
            doc.AppendChild(root);
            root.AppendChild(npcs);

            foreach (var floor in DataHelper.Instance.AllTeleportData.floors)
            {
                XmlElement teleport = doc.CreateElement("Teleport");
                var obj = floor.Value as TransportFloor;
                teleport.SetAttribute("Code", obj.ID.ToString());
                teleport.SetAttribute("Tip", obj.tip);
                teleport.SetAttribute("To", obj.toMapId.ToString());
                teleport.SetAttribute("ToX", obj.tox.ToString());
                teleport.SetAttribute("ToY", obj.toy.ToString());
                teleport.SetAttribute("ToDirection", obj.todir.ToString());
                teleport.SetAttribute("X", obj.X.ToString());
                teleport.SetAttribute("Y", obj.Y.ToString());
                teleport.SetAttribute("Radius", obj.Radius.ToString());
                npcs.AppendChild(teleport);
            }

            doc.Save(filename);

        }

        public void ExportSceneObj(string filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Config");
            doc.AppendChild(root);

            foreach (var floor in DataHelper.Instance.AllSceneObjAreaData.floors)
            {
                XmlElement item = doc.CreateElement("Item");
                var obj = floor.Value as SceneObjFloor;
                item.SetAttribute("ID", obj.ID.ToString());
                item.SetAttribute("SceneObjId", obj.SelectItem.id.ToString());
                item.SetAttribute("Dir", obj.Dir.ToString());
                item.SetAttribute("Num", obj.num.ToString());
                item.SetAttribute("IsRandObj", obj.isRandObj.ToString());

                StringBuilder sb = new StringBuilder();
                foreach (var g in obj.RandXYGrids)
                {
                    if (g.gridY < 0)
                    {
                        continue;
                    }
                    sb.AppendFormat("{0}_{1},", g.gridX, g.gridY);
                }
                if (sb.Length > 0)
                {
                    sb.Length--;
                }

                item.SetAttribute("Grids", sb.ToString());
                root.AppendChild(item);
            }

            doc.Save(filename);

        }

        #endregion

    }
}
