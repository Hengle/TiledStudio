using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledStudio
{
    #region 地图条件


    //副本地图上的指定id的怪物全部死亡
    public class ConditionMapAllMonsterDie : ICondition
    {
    }

    //副本怪区激活
    public class ConditionMapMonsterZoneEnabled : ICondition
    {
        private int _zoneId;
    }

    //副本地图指定怪区怪物全部死亡
    public class ConditionMapMonsterZoneClean : ICondition
    {
        private int _zoneId;
    }


    //倒计时到达
    public class ConditionMapTimerOver : ICondition
    {
        private int _timerId;
    }


    //检查触发器开关
    public class ConditionMapTriggerOn : ICondition
    {
        private int _triggerId;
    }


    #endregion

    #region 地图动作

    public class ActionMapFubenEnd :IAction
    {
    }

    //倒计时一段时间触发，创建一个倒计时
    public class ActionMapCreateTimer : IAction
    {
        private int _interval;//秒
        private int _timerId;
    }

    //激活指定怪区
    public class ActionMapEnableMonsterZone : IAction
    {
        private int _monsterZoneId;
    }

    //禁用怪区
    public class ActionMapDisableMonsterZone : IAction
    {
        private int _monsterZoneId;
    }

    //激活指定触发区
    public class ActionMapEnableTriggerZone : IAction
    {
        private int _triggerZoneId;
    }

    //屏蔽触发区
    public class ActionMapDisableTriggerZone : IAction
    {
        private int _triggerZoneId;

    }

    //激活场景摆放物
    public class ActionMapEnableSceneObject : IAction
    {
        private int _sceneObjId;

    }

    //禁用场景摆放物
    public class ActionMapDisableSceneObject : IAction
    {
        private int _sceneObjId;
    }

    //npc激活
    public class ActionMapEnableNPCZone : IAction
    {
        private int _npcZoneID;
    }

    //npc禁用
    public class ActionMapDisableNPCZone : IAction
    {
        private int _npcZoneID;
    }

    //凭空放技能
    public class ActionMapCallMagic : IAction
    {
        private int _magicCode;
        private int _gridX;
        private int _gridY;
    }

    //地图成功结束
    public class ActionMapVictory : IAction
    {
    }

    //失败
    public class ActionMapDefeat : IAction
    {
    }
    #endregion
}
