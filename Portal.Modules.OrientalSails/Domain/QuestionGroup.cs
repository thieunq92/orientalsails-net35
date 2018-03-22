using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class QuestionGroup
    {
        private IList<Question> questions;

        public virtual int Id { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual string Name { get; set; }
        public virtual string Selection1 { get; set; }
        public virtual string Selection2 { get; set; }
        public virtual string Selection3 { get; set; }
        public virtual string Selection4 { get; set; }
        public virtual string Selection5 { get; set; }
        public virtual int Priority { get; set; }
        public virtual int GoodChoice { get; set; }
        public virtual bool IsInDayboatForm { get; set; }

        public virtual IList<Question> Questions
        {
            get
            {
                if (questions == null)
                {
                    questions = new List<Question>();
                }
                return questions;
            }
            set { questions = value; }
        }

        public virtual IList<string> Selections
        {
            get
            {
                IList<string> result = new List<string>();
                if (!string.IsNullOrEmpty(Selection1))
                {
                    result.Add(Selection1);
                }
                if (!string.IsNullOrEmpty(Selection2))
                {
                    result.Add(Selection2);
                }
                if (!string.IsNullOrEmpty(Selection3))
                {
                    result.Add(Selection3);
                }
                if (!string.IsNullOrEmpty(Selection4))
                {
                    result.Add(Selection4);
                }
                if (!string.IsNullOrEmpty(Selection5))
                {
                    result.Add(Selection5);
                }
                return result;
            }
        }
    }
}
