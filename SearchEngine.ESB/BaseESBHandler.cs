using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace SearchEngine.ESB
{
    public class BaseESBHandler<T> : IMessageHandler<T>
    {
        #region Properties
        public IBus Bus { get; set; }
        #endregion

        #region Construction
        public BaseESBHandler()
        {
        }

        public BaseESBHandler(IBus bus)
        {
            this.Bus = bus;
        }
        #endregion

        #region Interface implement
        public virtual void Handle(T message)
        {
            throw new NotImplementedException();
        }
        #endregion

        
    }
}
