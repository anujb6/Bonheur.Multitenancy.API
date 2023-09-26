using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Manager.Exceptions
{
    [Serializable()]
    public class OutletsDataException : Exception, ISerializable
    {
        public Dictionary<string, string> ValidationMessages { get; set; }

        public OutletsDataException()
            : base()
        {
            this.ValidationMessages = new Dictionary<string, string>();
        }

        public OutletsDataException(Dictionary<string, string> validationMessages)
            : this()
        {
            this.ValidationMessages = validationMessages;
        }

        public OutletsDataException(params KeyValuePair<string, string>[] validationMessages)
            : this()
        {
            foreach (var item in validationMessages)
            {
                this.ValidationMessages.Add(item.Key, item.Value);
            }
        }
    }
}
