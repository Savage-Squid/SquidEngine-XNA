using System;
using System.Collections.Generic;
using System.Text;

namespace SquidEngine.Attributes
{
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class SquidComponentAttribute : Attribute
    {
        readonly string _friendlyName;
        private bool _showinEditor = true;
        private string _group;

        // This is a positional argument.
        public SquidComponentAttribute(string friendlyname)
        {
            this._friendlyName = friendlyname;
        }
        
        public string FriendlyName
        {
            get
            {
                return this._friendlyName;
            }
        }

        public bool ShowInEditor
        {
            get { return _showinEditor; }
            set { _showinEditor = value; }
        }
        
        public string Group
        {
            get { return _group; }
            set { _group = value; }
        }
    }

    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = false,AllowMultiple = false)]
    public sealed class SquidComponentPropertyAttribute : Attribute
    {
        readonly string _friendlyName;
        private string _defaultValue;

        bool _templateRef;

        // This is a positional argument.
        public SquidComponentPropertyAttribute(string friendlyName)
        {
            this._friendlyName = friendlyName;
            this.DefaultValue = null;
        }

        public SquidComponentPropertyAttribute(string friendlyName, string defaultValue)
        {
            this._friendlyName = friendlyName;
            this._defaultValue = defaultValue;
        }

        public string FriendlyName
        {
            get
            {
                return this._friendlyName;
            }
        }

        public bool TemplateRef
        {
            get { return _templateRef; }
            set { _templateRef = value; }
        }

        
        public string DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }       
    }

    [global::System.AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
    public sealed class SquidEngineObjectTypeAttribute : Attribute
    {
        public SquidEngineObjectTypeAttribute()
        {

        }
    }

    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class SquidEffectAttribute : Attribute
    {
        readonly string _friendlyName;
        private bool _showinEditor = true;
        private string _group;

        // This is a positional argument.
        public SquidEffectAttribute(string friendlyname)
        {
            this._friendlyName = friendlyname;
        }

        public string FriendlyName
        {
            get
            {
                return this._friendlyName;
            }
        }

        public bool ShowInEditor
        {
            get { return _showinEditor; }
            set { _showinEditor = value; }
        }

        public string Group
        {
            get { return _group; }
            set { _group = value; }
        }
    }
}
