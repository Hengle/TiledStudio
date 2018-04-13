using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledStudio
{
    public class PropertyGridManager : CollectionBase, ICustomTypeDescriptor
    {
        public void Add(Property value)
        {
            int flag = -1;
            if (value != null)
            {
                if (base.List.Count > 0)
                {
                    IList<Property> mList = new List<Property>();
                    for (int i = 0; i < base.List.Count; i++)
                    {
                        Property p = base.List[i] as Property;
                        if (value.Name == p.Name)
                        {
                            flag = i;
                        }
                        mList.Add(p);
                    }
                    if (flag == -1)
                    {
                        mList.Add(value);
                    }
                    base.List.Clear();
                    foreach (Property p in mList)
                    {
                        base.List.Add(p);
                    }
                }
                else
                {
                    base.List.Add(value);
                }
            }
        }
        public void Remove(Property value)
        {
            if (value != null && base.List.Count > 0)
                base.List.Remove(value);
        }
        public Property this[int index]
        {
            get
            {
                return (Property)base.List[index];
            }
            set
            {
                base.List[index] = (Property)value;
            }
        }
        #region ICustomTypeDescriptor 成员  
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }
        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }
        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }
        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }
        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }
        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }
        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptor[] newProps = new PropertyDescriptor[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                Property prop = (Property)this[i];
                newProps[i] = new CustomPropertyDescriptor(ref prop, attributes);
            }
            return new PropertyDescriptorCollection(newProps);
        }
        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this, true);
        }
        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
        #endregion
    }

    public class Property
    {
        private string _name = string.Empty;
        private object _value = null;
        private bool _readonly = false;
        private bool _visible = true;
        private string _category = string.Empty;
        TypeConverter _converter = null;
        object _editor = null;
        private string _displayname = string.Empty;
        public Property(string sName, object sValue)
        {
            this._name = sName;
            this._value = sValue;
        }
        public Property(string sName, object sValue, bool sReadonly, bool sVisible)
        {
            this._name = sName;
            this._value = sValue;
            this._readonly = sReadonly;
            this._visible = sVisible;
        }
        public string Name  //获得属性名  
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public string DisplayName   //属性显示名称  
        {
            get
            {
                return _displayname;
            }
            set
            {
                _displayname = value;
            }
        }
        public TypeConverter Converter  //类型转换器，我们在制作下拉列表时需要用到  
        {
            get
            {
                return _converter;
            }
            set
            {
                _converter = value;
            }
        }
        public string Category  //属性所属类别  
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }
        public object Value  //属性值  
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public bool ReadOnly  //是否为只读属性  
        {
            get
            {
                return _readonly;
            }
            set
            {
                _readonly = value;
            }
        }
        public bool Visible  //是否可见  
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }
        public virtual object Editor   //属性编辑器  
        {
            get
            {
                return _editor;
            }
            set
            {
                _editor = value;
            }
        }
    }

    public class CustomPropertyDescriptor : PropertyDescriptor
    {
        Property m_Property;
        public CustomPropertyDescriptor(ref Property myProperty, Attribute[] attrs)
            : base(myProperty.Name, attrs)
        {
            m_Property = myProperty;
        }
        #region PropertyDescriptor 重写方法  
        public override bool CanResetValue(object component)
        {
            return false;
        }
        public override Type ComponentType
        {
            get
            {
                return null;
            }
        }
        public override object GetValue(object component)
        {
            return m_Property.Value;
        }
        public override string Description
        {
            get
            {
                return m_Property.Name;
            }
        }
        public override string Category
        {
            get
            {
                return m_Property.Category;
            }
        }
        public override string DisplayName
        {
            get
            {
                return m_Property.DisplayName != "" ? m_Property.DisplayName : m_Property.Name;
            }
        }
        public override bool IsReadOnly
        {
            get
            {
                return m_Property.ReadOnly;
            }
        }
        public override void ResetValue(object component)
        {
            //Have to implement  
        }
        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
        public override void SetValue(object component, object value)
        {
            m_Property.Value = value;
        }
        public override TypeConverter Converter
        {
            get
            {
                return m_Property.Converter;
            }
        }
        public override Type PropertyType
        {
            get { return m_Property.Value.GetType(); }
        }
        public override object GetEditor(Type editorBaseType)
        {
            return m_Property.Editor == null ? base.GetEditor(editorBaseType) : m_Property.Editor;
        }
        #endregion
    }

    public class PropertySorter : ExpandableObjectConverter
    {
        #region Methods
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            //
            // This override returns a list of properties in order
            //
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(value, attributes);
            ArrayList orderedProperties = new ArrayList();
            foreach (PropertyDescriptor pd in pdc)
            {
                Attribute attribute = pd.Attributes[typeof(PropertyOrderAttribute)];
                if (attribute != null)
                {
                    //
                    // If the attribute is found, then create an pair object to hold it
                    //
                    PropertyOrderAttribute poa = (PropertyOrderAttribute)attribute;
                    orderedProperties.Add(new PropertyOrderPair(pd.Name, poa.Order));
                }
                else
                {
                    //
                    // If no order attribute is specifed then given it an order of 0
                    //
                    orderedProperties.Add(new PropertyOrderPair(pd.Name, 0));
                }
            }
            //
            // Perform the actual order using the value PropertyOrderPair classes
            // implementation of IComparable to sort
            //
            orderedProperties.Sort();
            //
            // Build a string list of the ordered names
            //
            ArrayList propertyNames = new ArrayList();
            foreach (PropertyOrderPair pop in orderedProperties)
            {
                propertyNames.Add(pop.Name);
            }
            //
            // Pass in the ordered list for the PropertyDescriptorCollection to sort by
            //
            return pdc.Sort((string[])propertyNames.ToArray(typeof(string)));
        }
        #endregion
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyOrderAttribute : Attribute
    {
        //
        // Simple attribute to allow the order of a property to be specified
        //
        private int _order;
        public PropertyOrderAttribute(int order)
        {
            _order = order;
        }

        public int Order
        {
            get
            {
                return _order;
            }
        }
    }

    public class PropertyOrderPair : IComparable
    {
        private int _order;
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public PropertyOrderPair(string name, int order)
        {
            _order = order;
            _name = name;
        }

        public int CompareTo(object obj)
        {
            //
            // Sort the pair objects by ordering by order value
            // Equal values get the same rank
            //
            int otherOrder = ((PropertyOrderPair)obj)._order;
            if (otherOrder == _order)
            {
                //
                // If order not specified, sort by name
                //
                string otherName = ((PropertyOrderPair)obj)._name;
                return string.Compare(_name, otherName);
            }
            else if (otherOrder > _order)
            {
                return -1;
            }
            return 1;
        }
    }


}
