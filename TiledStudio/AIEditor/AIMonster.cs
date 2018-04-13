using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledStudio
{
    #region 怪物AI条件

    //怪物活着
    public class ConditionMonsterAlive :ICondition
    {
    }

    //怪物在出生点
    public class ConditionMonsterAtBirth : ICondition
    {
    }

    public class ConditionMonsterHPCD : ICondition
    {
        private int _recoverHPCD;
    }


    //判断怪物血量低于百分之几
    public class ConditionMonsterHP : ICondition
    {
        private float _waterline;
    }

    //判断怪物是否在退避距离内
    public class ConditionMonsterEvadeRange : ICondition
    {
    }

    //判断怪物锁定的敌人是否在攻击距离内
    public class ConditionMonsterAttackRange : ICondition
    {
    }

    //判断怪物锁定的敌人是否在追击距离内
    public class ConditionMonsterPursuitRange : ICondition
    {
    }

    //判断锁敌cd时间
    public class ConditionMonsterSeekCD : ICondition
    {
        private long _minLockTicks;//未锁敌时重新锁敌CD
        private long _maxLockTicks;//锁敌状态下重新锁敌CD
    }

    //判断视野刷新CD
    public class ConditionMonsterViewCD : ICondition
    {
        private long _minViewCD;
        private long _maxViewCD;
    }

    //判断怪物是否正在移动
    public class ConditionMonsterMoving : ICondition
    {
    }

    //怪物移动一步CD
    public class ConditionMonsterChangeActionCD : ICondition
    {
        private long _oneStepTick;
    }

    //判断怪物周围有人
    public class ConditionMonsterSeeRole : ICondition
    {
    }

    //判断怪物有锁定目标
    public class ConditionMonsterLockObject : ICondition
    {
    }

    //判断怪物当前动作
    public class ConditionMonsterAction : ICondition
    {
        int _action;
    }

    //判断怪物身上buff
    public class ConditionMonsterBuff : ICondition
    {
        int _buffId;
    }

    //判断怪物和锁定目标的战斗力
    public class ConditionMonsterCombatForce : ICondition
    {
    }


    #endregion


    #region 怪物AI动作

    //自动回血
    public class ActionMonsterRecoverHP :IAction
    {
    }

    //怪物加血（负值减血）
    public class ActionMonsterAddHP :IAction
    {
        private int _addHP;
    }


    //怪物追击锁定目标（走一步）
    public class ActionMonsterPursuit :IAction
    {

    }

    //怪物视野对象更新
    public class ActionMonsterView :IAction
    {
    }

    //怪物锁敌
    public class ActionMonsterSeek :IAction
    {

    }

    //怪物巡逻
    public class ActionMonsterPatrol :IAction
    {
        private int _mapCode;
        private int _lineId;
    }

    //怪物脱战（丢失锁定目标）
    public class ActionMonsterLostTarget :IAction
    {
    }

    //怪物返回
    public class ActionMonsterReturn :IAction
    {
    }


    //怪物无聊
    public class ActionMonsterWondering :IAction
    {
        private float _walkPercent;
    }

    //怪物攻击
    public class ActionMonsterAttack :IAction
    {
    }

    //怪物逃离
    public class ActionMonsterEvade :IAction
    {
    }

    //改变动作
    public class ActionMonsterChangeAction :IAction
    {
        private int _action;
    }

    //怪物全屏追击【使用导航网格】
    public class ActionMonsterFullMapPusuit :IAction
    {
    }

    //怪物触发地图事件
    public class ActionMonsterSetMapEvent :IAction
    {
        private int eventId;
    }
    #endregion
}
