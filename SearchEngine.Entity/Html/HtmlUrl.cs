using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RLM.Core.Entity;

namespace SearchEngine.Entity.Html
{
    public class HtmlUrl : BaseEntityObject
    {
        public Guid HtmlUrlID { get; set; }
        public Guid HtmlPageID { get; set; }
        public string Url { get; set; }
        public string Domain { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public int MaxRetry { get; set; }
        public int Retry { get; set; }
        public DateTime LastRetryDate { get; set; }
        public DateTime NextRetryDate { get; set; }
        public bool IsBlackList { get; set; }
        public int Priority { get; set; }
        public DateTime CrawlDate { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }

        #region Override
        public override string EntityId
        {
            get
            {
                return HtmlUrlID.ToString();
            }
        }
        public override string EntityName
        {
            get
            {
                return this.Title;
            }
        }
        public override string EntityType
        {
            get
            {
                return this.GetType().FullName;
            }
        }
        #endregion

        #region Constructor
        public HtmlUrl()
        {
            this.HtmlUrlID = Guid.NewGuid();
            this.CreatedDate = DateTime.UtcNow;
        }
        #endregion
    }
}
