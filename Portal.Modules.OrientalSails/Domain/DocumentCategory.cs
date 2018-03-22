using System;
using System.Collections;

namespace Portal.Modules.OrientalSails.Domain
{
    #region Room

    /// <summary>
    /// Room object for NHibernate mapped table 'os_Room'.
    /// </summary>
    public class DocumentCategory
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Url { get; set; }
        public virtual DocumentCategory Parent { get; set; }
    }

    #endregion
}