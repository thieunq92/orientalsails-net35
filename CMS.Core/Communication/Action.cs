namespace CMS.Core.Communication
{
    /// <summary>
    /// The Action class describes a single action that a module can perform when calling or
    /// called from other modules.
    /// </summary>
    public class Action
    {
        #region -- PRIVATE MEMBERS --

        private readonly string _name;
        private readonly string[] _parameters;

        #endregion

        #region -- CONSTRUCTORS --

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Action(string name, string[] parameters)
        {
            _name = name;
            _parameters = parameters;
        }

        #endregion

        #region -- PROPERTIES --

        /// <summary>
        /// The name of the action.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// The list of parameters that are allowed for the action. 
        /// NOTE: this is kind of loosely coupled. Only the parameter names are required.
        /// No type specification is required. The modules have to take care of validating
        /// the parameters themselves.
        /// </summary>
        public string[] Parameters
        {
            get { return _parameters; }
        }

        #endregion

        #region -- METHODS --

        /// <summary>
        /// Equals override.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Action)
            {
                bool isEqual = true;
                Action otherAction = (Action) obj;
                if (_name != otherAction.Name)
                {
                    isEqual = false;
                }
                else
                {
                    if (_parameters == null)
                    {
                        isEqual = otherAction.Parameters == null;
                    }
                    else
                    {
                        if (_parameters.Length == otherAction.Parameters.Length)
                        {
                            for (int i = 0; i < _parameters.Length; i++)
                            {
                                if (_parameters[i] != otherAction.Parameters[i])
                                {
                                    isEqual = false;
                                }
                            }
                        }
                        else
                        {
                            isEqual = false;
                        }
                    }
                }
                return isEqual;
            }
            return false;
        }

        /// <summary>
        /// GetHashCode override.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _name.GetHashCode() ^ _parameters.GetHashCode();
        }

        #endregion
    }
}