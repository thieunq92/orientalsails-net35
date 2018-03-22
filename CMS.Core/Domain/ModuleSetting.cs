using System;

namespace CMS.Core.Domain
{
    /// <summary>
    /// Summary description for ModuleSetting.
    /// </summary>
    public class ModuleSetting
    {
        private string _friendlyName;
        private bool _isCustomType;
        private bool _isRequired;
        private ModuleType _moduleType;
        private string _name;
        private string _settingDataType;

        /// <summary>
        /// Property Name (string)
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Property FriendlyName (string)
        /// </summary>
        public string FriendlyName
        {
            get { return _friendlyName; }
            set { _friendlyName = value; }
        }

        /// <summary>
        /// Property _settingDataType (string)
        /// </summary>
        public string SettingDataType
        {
            get { return _settingDataType; }
            set { _settingDataType = value; }
        }

        /// <summary>
        /// Property IsCustomType (bool)
        /// </summary>
        public bool IsCustomType
        {
            get { return _isCustomType; }
            set { _isCustomType = value; }
        }

        /// <summary>
        /// Property IsRequired (bool)
        /// </summary>
        public bool IsRequired
        {
            get { return _isRequired; }
            set { _isRequired = value; }
        }

        /// <summary>
        /// Property ModuleType (ModuleType)
        /// </summary>
        public ModuleType ModuleType
        {
            get { return _moduleType; }
            set { _moduleType = value; }
        }

        /// <summary>
        /// Gets the CLR Type of the ModuleSetting
        /// </summary>
        /// <returns></returns>
        public Type GetRealType()
        {
            if (_settingDataType != null)
            {
                if (_isCustomType)
                {
                    // Use the assemblyname of the ModuleType to find the custom type
                    string fullName = _settingDataType + ", " + _moduleType.AssemblyName;
                    Type realType = Type.GetType(fullName);
                    if (realType == null)
                    {
                        throw new NullReferenceException(
                            String.Format("The custom type {0} was not found in assembly {1}."
                                          , _settingDataType, _moduleType.AssemblyName));
                    }
                    else
                    {
                        return realType;
                    }
                }
                else
                {
                    Type realType = Type.GetType(_settingDataType, true, true);
                    if (realType == null)
                    {
                        throw new NullReferenceException(String.Format("The CLR type {0} is invalid."
                                                                       , _settingDataType));
                    }
                    else
                    {
                        return realType;
                    }
                }
            }
            else
            {
                throw new NullReferenceException("Unable to get the type of the module setting");
            }
        }
    }
}