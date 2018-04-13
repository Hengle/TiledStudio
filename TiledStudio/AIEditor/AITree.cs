using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml;

namespace TiledStudio
{

    public interface ICondition { }
    public interface IAction { }

    class AITree
    {
        public static Dictionary<string, string> allNodeTypes = new Dictionary<string, string>()
        {
            {"NONE", "none" },
            {"条件", "con"},
            {"动作", "act"},
            {"顺序执行全部","aseq"},
            {"顺序直到失败","fseq"},
            {"顺序直到成功","sseq"},
            {"概率选择一个","psel"},
            {"条件选择一个","bsel"},
            {"逐个选择执行","fsel"},
            {"条件取反","not" },
            {"条件取与","and" },
            {"条件取或","or"  }
        };
        public static Dictionary<string, string> antiNodeTypes = new Dictionary<string, string>()
        {
            {"none", "NONE" },
            {"con","条件"},
            {"act","动作"},
            {"aseq","顺序执行全部"},
            {"fseq","顺序直到失败"},
            {"sseq","顺序直到成功"},
            {"psel","概率选择一个"},
            {"bsel","条件选择一个"},
            {"fsel","逐个选择执行"},
            {"not","条件取反" },
            {"and","条件取与" },
            {"or" ,"条件取或" }
        };


        public static readonly Dictionary<string, Tuple<int, string>> allConditions = new Dictionary<string, Tuple<int, string>>()
        {
            {"MonsterIsAlive",new Tuple<int,string>(0,"怪物活着")},
            {"MonsterHPCD",new Tuple<int,string>(1,"怪物回血CD{0}秒") },
            {"MonsterAtBirthPos",new Tuple<int,string>(0,"怪物站在出生点") },
            {"MonsterHPLessThan",new Tuple<int,string>(1,"怪物血量低于{0}%") },
            {"MonsterInAttackRange",new Tuple<int,string>(1,"怪物目标在攻击距离{0}内") },
            {"MonsterSeekCD",new Tuple<int,string>(1,"怪物锁敌CD{0}秒") },
            {"MonsterViewCD",new Tuple<int,string>(1,"怪物视野CD{0}秒") },
            {"MonsterIsMoving",new Tuple<int,string>(0,"怪物正在移动") },
            {"MonsterInPursuitRange",new Tuple<int,string>(1,"怪物目标在追击距离{0}内") },
            {"MonsterSeeRole",new Tuple<int,string>(0,"怪物周围有人") },
            {"MonsterLockObject",new Tuple<int,string>(0,"怪物有目标") },
            {"MonsterInEvadeRange",new Tuple<int,string>(1,"怪物目标在逃离范围{0}内" )},
            {"MonsterAction",new Tuple<int,string>(1,"怪物动作是{0}") },
            {"MonsterBuff",new Tuple<int,string>(1,"怪物有buff{0}") },

            {"MapAllMonsterDie",new Tuple<int,string>(0,"地图全部怪物死光") },
            {"MapMonsterZoneClean",new Tuple<int,string>(1,"地图怪区{0}清除") },
            {"MapTimerOver",new Tuple<int,string>(1,"地图定时器{0}超时") },
            {"MapTriggerOn",new Tuple<int,string>(1,"地图触发区{0}触发") },
            {"MapEventSet",new Tuple<int,string>(1,"地图事件{0}发生") },
            {"MapMonsterZoneEnabled",new Tuple<int,string>(1,"地图怪区{0}是否激活") }
        };

        public static readonly Dictionary<string, Tuple<int, string>> allActions = new Dictionary<string, Tuple<int, string>>()
        {
            {"MonsterPursuit",new Tuple<int,string>(0,"怪物追击")},
            {"MonsterView", new Tuple<int, string>(0,"怪物视野刷新") },
            {"MonsterSeek",new Tuple<int,string>(0,"怪物锁敌")},
            {"MonsterPatrol",new Tuple<int,string>(2,"怪物在地图{0}巡逻线{1}巡逻")},
            {"MonsterReturn",new Tuple<int,string>(0,"怪物返回出生点")},
            {"MonsterWondering",new Tuple<int,string>(1,"怪物无聊且有{0}%的概率自走")  },
            {"MonsterAttack",new Tuple<int,string>(0,"怪物攻击") },
            {"MonsterLostTarget",new Tuple<int,string>(0,"怪物丢失目标" )},
            {"MonsterRecoverHP",new Tuple<int,string>(0,"怪物回血") },
            {"MonsterAddHP",new Tuple<int,string>(0,"怪物加血") },
            {"MonsterEvade",new Tuple<int,string>(0,"怪物逃离" )},
            {"MonsterSetMapEvent",new Tuple<int,string>(1,"怪物触发地图事件{0}") },
            {"MonsterFullMapPusuit",new Tuple<int,string>(0,"怪物全图追击") },

            {"MapCreateTimer",new Tuple<int,string>(1,"创建定时器{0}" )},
            {"MapEnableNPCZone",new Tuple<int,string>(1,"激活NPC区{0}") },
            {"MapDisableNPCZone" ,new Tuple<int,string>(1,"禁用NPC区{0}")},
            {"MapEnableSceneObject",new Tuple<int,string>(1,"激活场景摆放物{0}") },
            {"MapDisableSceneObject",new Tuple<int,string>(1,"禁用场景摆放物{0}" )},
            {"MapEnableTriggerZone",new Tuple<int,string>(1,"激活触发区{0}") },
            {"MapDisableTriggerZone",new Tuple<int,string>(1,"禁用触发区{0}") },
            {"MapEnableMonsterZone",new Tuple<int,string>(1,"激活怪区{0}") },
            {"MapDisableMonsterZone",new Tuple<int,string>(1,"禁用怪区{0}") },
            {"MapCallMagic",new Tuple<int,string>(3,"地图[{0},{1}]凭空放技能{2}") },
            {"MapVictory",new Tuple<int,string>(0,"副本胜利") },
            {"MapDefeat",new Tuple<int,string>(0,"副本失败") }
        };

        //public static void Init()
        //{
        //    //怪物动作
        //    allActions.Add("MonsterPursuit", new ActionMonsterPursuit());
        //    allActions.Add("MonsterSeek", new ActionMonsterSeek());
        //    allActions.Add("MonsterPatrol", new ActionMonsterPatrol());
        //    allActions.Add("MonsterReturn", new ActionMonsterReturn());
        //    allActions.Add("MonsterWondering", new ActionMonsterWondering());
        //    allActions.Add("MonsterAttack", new ActionMonsterAttack());
        //    allActions.Add("MonsterLostTarget", new ActionMonsterLostTarget());
        //    allActions.Add("MonsterRecoverHP", new ActionMonsterRecoverHP());
        //    allActions.Add("MonsterAddHP", new ActionMonsterAddHP());
        //    allActions.Add("MonsterView", new ActionMonsterView());
        //    allActions.Add("MonsterChangeAction", new ActionMonsterChangeAction());
        //    allActions.Add("MonsterEvade", new ActionMonsterEvade());
        //    allActions.Add("MonsterSetMapEvent", new ActionMonsterSetMapEvent());
        //    allActions.Add("MonsterFullMapPusuit", new ActionMonsterFullMapPusuit());
        //    //地图动作
        //    allActions.Add("MapFubenEnd", new ActionMapFubenEnd());
        //    allActions.Add("MapCreateTimer", new ActionMapCreateTimer());
        //    allActions.Add("MapEnableNPCZone", new ActionMapEnableNPCZone());
        //    allActions.Add("MapDisableNPCZone", new ActionMapDisableNPCZone());
        //    allActions.Add("MapEnableSceneObject", new ActionMapEnableSceneObject());
        //    allActions.Add("MapDisableSceneObject", new ActionMapDisableSceneObject());
        //    allActions.Add("MapEnableTriggerZone", new ActionMapEnableTriggerZone());
        //    allActions.Add("MapDisableTriggerZone", new ActionMapDisableTriggerZone());
        //    allActions.Add("MapEnableMonsterZone", new ActionMapEnableMonsterZone());
        //    allActions.Add("MapDisableMonsterZone", new ActionMapDisableMonsterZone());
        //    allActions.Add("MapCallMagic", new ActionMapCallMagic());
        //    allActions.Add("MapVictory", new ActionMapVictory());
        //    allActions.Add("MapDefeat", new ActionMapDefeat());

        //    //怪物条件
        //    allConditions.Add("MonsterHPCD", new ConditionMonsterHPCD());
        //    allConditions.Add("MonsterAtBirthPos", new ConditionMonsterAtBirth());
        //    allConditions.Add("MonsterHPLessThan", new ConditionMonsterHP());
        //    allConditions.Add("MonsterInAttackRange", new ConditionMonsterAttackRange());
        //    allConditions.Add("MonsterIsAlive", new ConditionMonsterAlive());
        //    allConditions.Add("MonsterSeekCD", new ConditionMonsterSeekCD());
        //    allConditions.Add("MonsterViewCD", new ConditionMonsterViewCD());
        //    allConditions.Add("MonsterActionCD", new ConditionMonsterChangeActionCD());
        //    allConditions.Add("MonsterIsMoving", new ConditionMonsterMoving());
        //    allConditions.Add("MonsterInPursuitRange", new ConditionMonsterPursuitRange());
        //    allConditions.Add("MonsterSeeRole", new ConditionMonsterSeeRole());
        //    allConditions.Add("MonsterLockObject", new ConditionMonsterLockObject());
        //    allConditions.Add("MonsterAction", new ConditionMonsterAction());
        //    allConditions.Add("MonsterBuff", new ConditionMonsterBuff());
        //    allConditions.Add("MonsterInEvadeRange", new ConditionMonsterEvadeRange());

        //    //地图条件
        //    allConditions.Add("MapAllMonsterDie", new ConditionMapAllMonsterDie());
        //    allConditions.Add("MapMonsterZoneClean", new ConditionMapMonsterZoneClean());
        //    allConditions.Add("MapTimerOver", new ConditionMapTimerOver());
        //    allConditions.Add("MapTriggerOn", new ConditionMapTriggerOn());
        //    allConditions.Add("MapMonsterZoneEnabled", new ConditionMapMonsterZoneEnabled());
        //}

        public AINode Root { get; set; } = new AINode();


        public static void ParseCondition(AINode node, string actions)
        {
            string[] a = actions.Split(',');
            if (a.Length == 0)
            {
                return;
            }
            node.ConditionType = a[0];

            if (a.Length > 1) { node.ConditionParam1 = float.Parse(a[1]); }
            if (a.Length > 2) { node.ConditionParam2 = float.Parse(a[2]); }
            if (a.Length > 3) { node.ConditionParam2 = float.Parse(a[3]); }

        }

        public static void ParseAction(AINode node, string actions)
        {
            string[] a = actions.Split(',');
            if (a.Length == 0)
            {
                return;
            }
            node.ActionType = a[0];

            if (a.Length > 1) { node.ActionParam1 = float.Parse(a[1]); }
            if (a.Length > 2) { node.ActionParam2 = float.Parse(a[2]); }
            if (a.Length > 3) { node.ActionParam2 = float.Parse(a[3]); }
        }

        public void Load(string filename)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);
            Root = MakeNodeFromXML(xmlDoc.DocumentElement);
            Root.AutoLayout();
        }

        AINode MakeNodeFromXML(XmlElement xml)
        {
            AINode node = new AINode();
            var t = xml.GetAttribute("type");
            node.NodeType = antiNodeTypes[t];
            if (t == "con" || t == "bsel")
            {
                string s = xml.GetAttribute("condition");
                ParseCondition(node, s);
            }
            if (t == "act")
            {
                string s = xml.GetAttribute("action");
                ParseAction(node, s);
            }
            if (node.Parent != null)
            {
                var pt = allNodeTypes[node.Parent.NodeType];
                if (pt == "bsel")
                {
                    node.BSelectValue = bool.Parse(xml.GetAttribute("bselect"));
                }
                if (pt == "psel")
                {
                    node.PSelectWeight = int.Parse(xml.GetAttribute("weight"));
                }
            }

            foreach(var e in xml)
            {
                AINode n = MakeNodeFromXML(e as XmlElement);
                n.MyNodePtr = node.Children.AddLast(n);
            }
            return node;
        }

        public void Save(string filename)
        {
            XmlDocument xml = new XmlDocument();
            xml.AppendChild(MakeXmlElement(Root, xml));
            xml.Save(filename);
        }

        XmlElement MakeXmlElement(AINode node, XmlDocument xml)
        {
            XmlElement elem = xml.CreateElement("Node");
            var t = allNodeTypes[node.NodeType];
            elem.SetAttribute("type", t);
            if (t == "con" || t == "bsel")
            {
                elem.SetAttribute("condition", node.GetXmlCondition());
            }
            if (t == "act")
            {
                elem.SetAttribute("action", node.GetXmlAction());
            }
            if (node.Parent != null)
            {
                var pt = allNodeTypes[node.Parent.NodeType];
                if (pt == "bsel")
                {
                    elem.SetAttribute("bselect", node.BSelectValue.ToString());
                }
                if (pt == "psel")
                {
                    elem.SetAttribute("weight", node.PSelectWeight.ToString());
                }
            }
            foreach (var n in node.Children)
            {
                var e = MakeXmlElement(n, xml);
                elem.AppendChild(e);
            }
            return elem;
        }

    }
}
