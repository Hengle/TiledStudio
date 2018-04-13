using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace TiledStudio
{
    public partial class FormProperty : DockContent
    {
        public FormProperty()
        {
            InitializeComponent();
        }

        public PropertyGrid Property
        {
            get { return this.ppgAI; }
        }

        //用以支持自定义下拉框
        public class DropDownListConverter : StringConverter
        {
            object[] m_Objects;
            public DropDownListConverter(object[] objects)
            {
                m_Objects = objects;
            }
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return true;
            }
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(m_Objects);
            }
        }
    }

    public class MonsterListConverter : StringConverter
    {
        StandardValuesCollection collection = null;
        public MonsterListConverter()
        {
            List<string> items = new List<string>();
            foreach(var item in XmlHelper.Instance.AllMonstersConfig.Values)
            {
                items.Add(item.ToString());
            }
            collection = new StandardValuesCollection(items.ToArray());
        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return collection;
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }

    public class NPCListConverter : StringConverter
    {
        StandardValuesCollection collection = null;
        public NPCListConverter()
        {
            List<string> items = new List<string>();
            foreach (var item in XmlHelper.Instance.AllNpcsConfig.Values)
            {
                items.Add(item.ToString());
            }
            collection = new StandardValuesCollection(items.ToArray());
        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return collection;
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

    }

    public class SceneObjListConverter : StringConverter
    {
        StandardValuesCollection collection = null;
        public SceneObjListConverter()
        {
            List<string> items = new List<string>();
            foreach (var item in XmlHelper.Instance.AllSceneObjsConfig.Values)
            {
                items.Add(item.ToString());
            }
            collection = new StandardValuesCollection(items.ToArray());
        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return collection;
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

    }

    //AI节点类型
    public class AINodeTypeConverter:StringConverter
    {
        StandardValuesCollection collection = null;
        public AINodeTypeConverter()
        {
            collection = new StandardValuesCollection(AITree.allNodeTypes.Keys.ToArray());
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return collection;
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

    }

    //AI条件节点类型
    public class AINodeConditionConverter:StringConverter
    {
        StandardValuesCollection collection = null;
        public AINodeConditionConverter()
        {
            collection = new StandardValuesCollection(AITree.allConditions.Keys.ToArray());
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return collection;
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

    }
    //AI动作节点类型
    public class AINodeActionConverter:StringConverter
    {
        StandardValuesCollection collection = null;
        public AINodeActionConverter()
        {
            collection = new StandardValuesCollection(AITree.allActions.Keys.ToArray());
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return collection;
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

    }

    public class AIHelper
    {
        public static void SetPropertyVisibility(object obj, string propertyName, bool visible)
        {
            Type type = typeof(BrowsableAttribute);
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
            AttributeCollection attrs = props[propertyName].Attributes;
            FieldInfo fld = type.GetField("browsable", BindingFlags.Instance | BindingFlags.NonPublic);
            fld.SetValue(attrs[type], visible);
        }

        public static void SetPropertyReadOnly(object obj, string propertyName, bool readOnly)
        {
            Type type = typeof(System.ComponentModel.ReadOnlyAttribute);
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
            AttributeCollection attrs = props[propertyName].Attributes;
            FieldInfo fld = type.GetField("isReadOnly", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance);
            fld.SetValue(attrs[type], readOnly);
        }

    }

}
