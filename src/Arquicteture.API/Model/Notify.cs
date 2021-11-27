using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arquicteture.API.Model
{
    public class Notify
    {
        public List<string> Notifies { get; private set; }

        protected Notify()
        {
            Notifies = new List<string>();
        }

        public void AddNotify(string notify)
        {
            Notifies.Add(notify);
        }
    }
}
