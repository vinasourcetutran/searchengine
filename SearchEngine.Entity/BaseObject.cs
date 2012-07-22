using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchEngine.Bot.Entity
{
    public class BaseObject
    {
        #region construction
        public BaseObject()
        {
            this.Id = Guid.NewGuid();
        }
        public BaseObject(Guid guid)
        {
            this.Id = guid;
        }

        public BaseObject(string guid)
        {
            this.Id = new Guid(guid);
        }
        #endregion

        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
