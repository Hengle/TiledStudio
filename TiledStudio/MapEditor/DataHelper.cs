using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TiledStudio
{

    class DataHelper
    {
        private DataHelper()
        {
            AllMapAreaList.Add(AllAQQData);
            AllMapAreaList.Add(AllObsData);
            AllMapAreaList.Add(AllNPCData);
            AllMapAreaList.Add(AllMonsterZoneData);
            AllMapAreaList.Add(AllPursuitData);
            AllMapAreaList.Add(AllTriggerAreaData);
            AllMapAreaList.Add(AllCollisionAreaData);
            AllMapAreaList.Add(AllSceneObjAreaData);
            AllMapAreaList.Add(AllBornData);
            AllMapAreaList.Add(AllTeleportData);
            AllMapAreaList.Add(AllNavigatePathData);
            AllMapAreaList.Add(AllPatrolArea);
        }

        public static readonly DataHelper Instance = new DataHelper();
        public List<MapArea> AllMapAreaList = new List<MapArea>();
        public MapArea CurrentMapArea;
        public MapFloor CurrentMapFloor;

        public SafeRegionArea AllAQQData = new SafeRegionArea();
        public ObstructionArea AllObsData = new ObstructionArea();
        public BornArea AllBornData = new BornArea();
        public NPCArea AllNPCData = new NPCArea();
        public NavigateArea AllNavigatePathData = new NavigateArea();
        public PatrolArea AllPatrolArea=new PatrolArea();
        public MonsterArea AllMonsterZoneData = new MonsterArea();
        public TransportArea AllTeleportData = new TransportArea();
        public MonsterPursuitArea AllPursuitData = new MonsterPursuitArea();
        public CollisionArea AllCollisionAreaData = new CollisionArea();
        public TriggerArea AllTriggerAreaData = new TriggerArea();
        public SceneObjArea AllSceneObjAreaData = new SceneObjArea();

    }
}
