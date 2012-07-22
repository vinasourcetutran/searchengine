using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;

namespace RLM.Core.Framework.Workflow
{
    public class WorflowActivity<T>
    {
        #region Variable
        T item;
        IList<WorflowActivity<T>> childs;
        Hash<object> parameters;
        #endregion

        #region Properties
        public Hash<object> Parameters { get; set; }

        /// <summary>
        /// Context data item for activity
        /// </summary>
        public T Item
        {
            get { return item; }
            set { item = value;}
        }

        /// <summary>
        /// List of child activities of current activity
        /// </summary>
        public IList<WorflowActivity<T>> Child
        {
            get { return childs; }
            set { childs = value; }
        }

        /// <summary>
        /// true if want excute parent Activity first, and then excute all childs
        /// </summary>
        public bool IsParentFirst { get; set; }

        /// <summary>
        /// true if want to excute first node that match
        /// </summary>
        public bool IsExcuteFirstMatch { get; set; }

        public bool IsReuseParentParameter { get; set; }
        #endregion

        #region Construction
        public WorflowActivity()
        {
            this.childs = new List<WorflowActivity<T>>();
            this.parameters = new Hash<object>();
            this.IsParentFirst = true;
            this.IsExcuteFirstMatch = true;
            this.item = default(T);
        }
        #endregion

        #region methods
        public void AddChild(WorflowActivity<T> child)
        {
            this.childs.Add(child);
        }

        public virtual bool IsValid()
        {
            return true;
        }

        public virtual void Excute(T item)
        {
            this.item = item;

            if (!this.IsValid()) { return; }
            
            if (this.IsParentFirst)
            {
                this.Excute();
            }
            foreach (WorflowActivity<T> child in this.childs)
            {
                if (child.IsReuseParentParameter)
                {
                    child.parameters = this.parameters;
                }
                child.item = item;
                if (!child.IsValid()) { continue; }
                child.Excute(item);
                if (this.IsExcuteFirstMatch) { break; }
            }
            if (!this.IsParentFirst)
            {
                this.Excute();
            }
        }

        protected virtual void Excute()
        {
            // Do nothing
            //throw new NotImplementedException();
        }
        #endregion
    }
}
