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
    public class AnswerSheet
    {
        protected IList<AnswerOption> options;
        protected IList<AnswerGroup> groups;

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string Email { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual Cruise Cruise { get; set; }
        public virtual bool IsSent { get; set; }
        public virtual string Guide { get; set; }
        public virtual string Driver { get; set; }
        public virtual string RoomNumber { get; set; }
        public virtual bool Deleted { get; set; }


        public virtual IList<AnswerOption> Options
        {
            get
            {
                if (options == null)
                {
                    options = new List<AnswerOption>();
                }
                return options;
            }
            set { options = value; }
        }

        public virtual IList<AnswerGroup> Groups
        {
            get
            {
                if (groups == null)
                {
                    groups = new List<AnswerGroup>();
                }
                return groups;
            }
            set { groups = value; }
        }

        public virtual AnswerGroup GetGroup(QuestionGroup group)
        {
            foreach (AnswerGroup answerGroup in Groups)
            {
                if (answerGroup.Group.Id == group.Id)
                {
                    return answerGroup;
                }
            }
            return new AnswerGroup();
        }

        public virtual AnswerOption GetOption(Question question)
        {
            foreach (AnswerOption option in Options)
            {
                if (option.Question.Id == question.Id)
                {
                    return option;
                }
            }
            return new AnswerOption();
        }
    }
}
