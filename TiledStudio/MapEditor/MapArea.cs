using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiledStudio
{
    #region 区域
    public class MapArea
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public bool Show { get; set; } = true;
        public TreeNode TreeViewNode { get; set; }

        public bool isOneGridPenArea = false;
        public bool isXYRArea = false;
        public Dictionary<int, MapFloor> floors = new Dictionary<int, MapFloor>();

        protected int AutoID { get { return _autoid++; } }
        private int _autoid = 1;

        public MapFloor GetFirstFloor()
        {
            if (floors.Count > 0)
            {
                return floors.Values.ToArray()[0];
            }
            return null;
        }

        public void RemoveMapFloor(int floorid)
        {
            if (floors.ContainsKey(floorid))
            {
                var floor = floors[floorid];
                foreach (var g in floor.grids)
                {
                    g.floors.Remove(floor);
                }
                floors.Remove(floorid);
            }
        }

        public void AddMapFloor(MapFloor floor)
        {
            floors.Add(floor.ID, floor);
            _autoid = Math.Max(_autoid, floor.ID) + 1;
        }

        public virtual MapFloor AddMapFloor()
        {
            throw new NotImplementedException();
        }

    }

    class TransportArea : MapArea
    {
        public TransportArea()
        {
            Name = "transport";
            Text = "传送点";
            isXYRArea = true;
        }

        public override MapFloor AddMapFloor()
        {
            var floor = new TransportFloor(this)
            {
                ID = AutoID,
            };
            floor.Text = Text + floor.ID.ToString();
            floors.Add(floor.ID, floor);
            return floor;
        }

    }
    class SafeRegionArea : MapArea
    {
        public SafeRegionArea()
        {
            Name = "saferegion";
            Text = "安全区";
        }

        public override MapFloor AddMapFloor()
        {
            var floor = new MapFloor(this)
            {
                ID = AutoID,
            };
            floor.Text = Text + floor.ID.ToString();

            floors.Add(floor.ID, floor);
            return floor;

        }

    }
    class ObstructionArea : MapArea
    {
        public ObstructionArea()
        {
            Name = "obstruction";
            Text = "阻挡区";
        }

        public override MapFloor AddMapFloor()
        {
            var floor = new MapFloor(this)
            {
                ID = AutoID,
            };
            floor.Text = Text + floor.ID.ToString();

            floors.Add(floor.ID, floor);
            return floor;

        }

    }
    class BornArea : MapArea
    {
        public BornArea()
        {
            Name = "born";
            Text = "出生点";
            isXYRArea = true;
        }

        public override MapFloor AddMapFloor()
        {
            var floor = new MapFloor(this)
            {
                ID = AutoID,
            };
            floor.Text = Text + floor.ID.ToString();

            floors.Add(floor.ID, floor);
            return floor;

        }

    }

    class NavigateArea : MapArea
    {
        public NavigateArea()
        {
            Name = "navigate";
            Text = "导航线";
            isOneGridPenArea = true;
        }

        public override MapFloor AddMapFloor()
        {
            var floor = new Path(this)
            {
                ID = AutoID,
            };
            floor.Text = Text + floor.ID.ToString();

            floors.Add(floor.ID, floor);
            return floor;

        }

    }

    class PatrolArea : MapArea
    {
        public PatrolArea()
        {
            Name = "patrol";
            Text = "巡逻线";
            isOneGridPenArea = true;
        }

        public override MapFloor AddMapFloor()
        {
            var floor = new Path(this)
            {
                ID = AutoID,
            };
            floor.Text = Text + floor.ID.ToString();

            floors.Add(floor.ID, floor);
            return floor;

        }

    }
    class NPCArea : MapArea
    {
        public NPCArea()
        {
            Name = "npc";
            Text = "NPC区";
            isXYRArea = true;
            isOneGridPenArea = true;
        }

        public string ConfigFile { get; set; }

        public override MapFloor AddMapFloor()
        {
            var floor = new NPCFloor(this)
            {
                ID = AutoID,
            };
            floor.Text = Text + floor.ID.ToString();

            floors.Add(floor.ID, floor);
            return floor;

        }

    }
    class MonsterArea : MapArea
    {
        public MonsterArea()
        {
            Name = "monster";
            Text = "怪物区";
            isXYRArea = true;
        }

        public string ConfigFile { get; set; }

        public override MapFloor AddMapFloor()
        {
            var floor = new MonsterFloor(this)
            {
                ID = AutoID,
            };
            floor.Text = Text + floor.ID.ToString();

            floors.Add(floor.ID, floor);
            return floor;

        }

    }
    class MonsterPursuitArea : MapArea
    {
        public MonsterPursuitArea()
        {
            Name = "pursuit";
            Text = "追击区";
        }

        public override MapFloor AddMapFloor()
        {
            var floor = new MapFloor(this)
            {
                ID = AutoID,
            };
            floor.Text = Text + floor.ID.ToString();

            floors.Add(floor.ID, floor);
            return floor;

        }

    }
    class CollisionArea : MapArea
    {
        public CollisionArea()
        {
            Name = "collision";
            Text = "碰撞区";
        }

        public override MapFloor AddMapFloor()
        {
            var floor = new MapFloor(this)
            {
                ID = AutoID,
            };
            floor.Text = Text + floor.ID.ToString();

            floors.Add(floor.ID, floor);
            return floor;

        }

    }
    class TriggerArea : MapArea
    {
        public TriggerArea()
        {
            Name = "trigger";
            Text = "触发区";
        }

        public override MapFloor AddMapFloor()
        {
            var floor = new MapFloor(this)
            {
                ID = AutoID,
            };
            floor.Text = Text + floor.ID.ToString();

            floors.Add(floor.ID, floor);
            return floor;

        }

    }

    class SceneObjArea : MapArea
    {
        public string ConfigFile { get; set; }

        public SceneObjArea()
        {
            Name = "sceneobj";
            Text = "摆放物";
            isXYRArea = true;
            isOneGridPenArea = true;
        }

        public override MapFloor AddMapFloor()
        {
            var floor = new SceneObjFloor(this)
            {
                ID = AutoID,
            };
            floor.Text = Text + floor.ID.ToString();

            floors.Add(floor.ID, floor);
            return floor;

        }

    }
    #endregion
}
