using System;
using System.Collections;
using System.Collections.Generic;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Domain
{
    public class CostingTable
    {
        protected IList costs;
        protected IList activeCosts;
        protected Dictionary<CostType, Costing> activeMap;

        public virtual int Id { get; set; }
        public virtual DateTime ValidFrom { get; set; }
        public virtual DateTime? ValidTo { get; set; }
        public virtual SailsTrip Trip { get; set; }
        public virtual TripOption Option { get; set; }

        public virtual IList Costs
        {
            get
            {
                if (costs == null)
                {
                    costs = new ArrayList();
                }
                return costs;
            }
            set { costs = value; }
        }

        public virtual IList GetActiveCost(IList costTypes)
        {
            if (activeCosts == null)
            {
                activeCosts = new ArrayList();
                Dictionary<CostType, Costing> map = new Dictionary<CostType, Costing>();
                foreach (Costing cost in Costs)
                {
                    if (!map.ContainsKey(cost.Type))
                    {
                        map.Add(cost.Type, cost);
                    }
                }

                foreach (CostType type in costTypes)
                {
                    if (map.ContainsKey(type))
                    {
                        activeCosts.Add(map[type]);
                    }
                    else
                    {
                        Costing newCost = new Costing();
                        newCost.Type = type;
                        newCost.Table = this;
                        activeCosts.Add(newCost);
                    }
                }
            }
            return activeCosts;
        }

        public virtual Dictionary<CostType, Costing> GetCostMap(IList costTypes)
        {
            if (activeMap == null)
            {
                activeMap = new Dictionary<CostType, Costing>();
                foreach (Costing cost in Costs)
                {
                    if (!activeMap.ContainsKey(cost.Type))
                    {
                        activeMap.Add(cost.Type, cost);
                    }
                }
            }
            return activeMap;
        }
    }
}