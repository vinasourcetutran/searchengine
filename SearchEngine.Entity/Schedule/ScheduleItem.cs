using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchEngine.Bot.Entity.Schedule
{
    public class ScheduleItem:BaseObject
    {
        /// <summary>
        /// General name of eschedule item
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Action: delete, update, ....
        /// </summary>
        public ScheduleAction Action { get; set; }
        /// <summary>
        /// Time to run
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// Latest time that schedule run
        /// </summary>
        public DateTime LastRunDateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// Status of schedule: cancelled, processing, done, ....
        /// </summary>
        public ScheduleStatus Status { get; set; }
        /// <summary>
        /// Extra config param
        /// </summary>
        public string XmlData { get; set; }
        /// <summary>
        /// Full name of class that schedule applied for: SearchEngine.Entity.Bot.HtmlPage
        /// </summary>
        public string EntityName { get; set; }
        /// <summary>
        /// ID of Entity
        /// </summary>
        public Guid EntityId { get; set; }
        /// <summary>
        /// Max retry times if fail
        /// </summary>
        public int MaxRetryTimes { get; set; }
        /// <summary>
        /// current retry times
        /// </summary>
        public int CurrentRetryTime { get; set; }
        /// <summary>
        /// response code from the last excute time: 200, 404
        /// </summary>
        public string ResponseCode { get; set; }
        /// <summary>
        /// Message appropriate with response code: success, fail, file not found, ...
        /// </summary>
        public string ResponseMessage { get; set; }
        /// <summary>
        /// format of response message: csv, xml, text, ....
        /// </summary>
        public SearchEngine.Entity.ResponseFormat ResponseFormat { get; set; }

    }
}
