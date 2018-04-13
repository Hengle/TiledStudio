using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace TiledStudio
{

    class OpenMapEventArgs : EventArgs
    {
        public int MapScale { get; set; }
    }

    class SaveProjectEventArgs : EventArgs
    {
        public string FileName { get; set; }
    }

    class MonsterConfigItem
    {
        public int monsterID;
        public string name;

        public override string ToString()
        {
            return $"{monsterID} {name}";
        }
    }

    class NpcConfigItem
    {
        public int npcID;
        public string name;

        public override string ToString()
        {
            return $"{npcID} {name}";
        }

    }

    class SceneObjConfigItem
    {
        public int id;
        public string name;
        public int dir;
        public List<SceneObjPoint>[] plist;

        public override string ToString()
        {
            return $"{id} {name}";
        }
    }

    public class Grid
    {
        public int areaId;
        public int gridX;
        public int gridY;
        public List<MapFloor> floors = new List<MapFloor>();
        public Rectangle rectAngle;
        private Point _centerPoint;
        public Point CenterPoint
        {
            get
            {
                _centerPoint.X = rectAngle.X + FormMain.Instance.fmEditor.nGridShowSize / 2;
                _centerPoint.Y = rectAngle.Y + FormMain.Instance.fmEditor.nGridShowSize / 2;
                return _centerPoint;
            }
        }

        public int tableX;//打表专用
        public int tableY;

    }

}
