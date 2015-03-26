using System;

namespace Reporting.Minion
{
    public class ChangeMessageSizeEventArgs : EventArgs
    {
        public ChangeMessageSizeEventArgs(int messageSize)
        {
            MessageSize = messageSize;
        }

        public int MessageSize { get; set; }
    }
}