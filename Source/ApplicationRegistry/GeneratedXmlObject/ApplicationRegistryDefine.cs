﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.34014
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// このソース コードは xsd によって自動生成されました。Version=4.0.30319.1 です。
// 
namespace ApplicationRegistries.GeneratedXmlObject {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
        "tryBehavior.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
        "tryBehavior.xsd", IsNullable=false)]
    public partial class ApplicationRegistryBehavior {
        
        private ApplicationRegistryBehaviorEntry[] entryField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Entry")]
        public ApplicationRegistryBehaviorEntry[] Entry {
            get {
                return this.entryField;
            }
            set {
                this.entryField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
        "tryBehavior.xsd")]
    public partial class ApplicationRegistryBehaviorEntry {
        
        private object itemField;
        
        private string idField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CommandLineArgument", typeof(CommandLineArgument), Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
            "tryDefine.xsd")]
        [System.Xml.Serialization.XmlElementAttribute("EnvironmentVariable", typeof(EnvironmentVariable), Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
            "tryDefine.xsd")]
        [System.Xml.Serialization.XmlElementAttribute("Registry", typeof(Registry), Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
            "tryDefine.xsd")]
        [System.Xml.Serialization.XmlElementAttribute("StaticValue", typeof(StaticValue), Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
            "tryDefine.xsd")]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="ID")]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
        "tryDefine.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
        "tryDefine.xsd", IsNullable=false)]
    public partial class CommandLineArgument {
        
        private string argumentNameField;
        
        private string defaultValueField;
        
        private bool ignoreCaseField;
        
        /// <remarks/>
        public string ArgumentName {
            get {
                return this.argumentNameField;
            }
            set {
                this.argumentNameField = value;
            }
        }
        
        /// <remarks/>
        public string DefaultValue {
            get {
                return this.defaultValueField;
            }
            set {
                this.defaultValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool ignoreCase {
            get {
                return this.ignoreCaseField;
            }
            set {
                this.ignoreCaseField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
        "tryDefine.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
        "tryDefine.xsd", IsNullable=false)]
    public partial class EnvironmentVariable {
        
        private string variableNameField;
        
        private string defaultValueField;
        
        /// <remarks/>
        public string VariableName {
            get {
                return this.variableNameField;
            }
            set {
                this.variableNameField = value;
            }
        }
        
        /// <remarks/>
        public string DefaultValue {
            get {
                return this.defaultValueField;
            }
            set {
                this.defaultValueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
        "tryDefine.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
        "tryDefine.xsd", IsNullable=false)]
    public partial class Registry {
        
        private string keyField;
        
        private string nameField;
        
        private string defaultValueField;
        
        /// <remarks/>
        public string Key {
            get {
                return this.keyField;
            }
            set {
                this.keyField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string DefaultValue {
            get {
                return this.defaultValueField;
            }
            set {
                this.defaultValueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
        "tryDefine.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
        "tryDefine.xsd", IsNullable=false)]
    public partial class StaticValue {
        
        private string valueField;
        
        /// <remarks/>
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
        "tryDefine.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
        "tryDefine.xsd", IsNullable=false)]
    public partial class ApplicationRegistryDefine {
        
        private ApplicationRegistryDefineEntry[] entryField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Entry")]
        public ApplicationRegistryDefineEntry[] Entry {
            get {
                return this.entryField;
            }
            set {
                this.entryField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
        "tryDefine.xsd")]
    public partial class ApplicationRegistryDefineEntry {
        
        private string descriptionField;
        
        private object itemField;
        
        private string idField;
        
        private TypeEnum typeField;
        
        /// <remarks/>
        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CommandLineArgument", typeof(CommandLineArgument))]
        [System.Xml.Serialization.XmlElementAttribute("EnvironmentVariable", typeof(EnvironmentVariable))]
        [System.Xml.Serialization.XmlElementAttribute("Registry", typeof(Registry))]
        [System.Xml.Serialization.XmlElementAttribute("StaticValue", typeof(StaticValue))]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public TypeEnum Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegis" +
        "tryDefine.xsd")]
    public enum TypeEnum {
        
        /// <remarks/>
        @string,
        
        /// <remarks/>
        @int,
        
        /// <remarks/>
        @bool,
    }
}
