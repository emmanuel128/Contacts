using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Interfaces
{
    interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
