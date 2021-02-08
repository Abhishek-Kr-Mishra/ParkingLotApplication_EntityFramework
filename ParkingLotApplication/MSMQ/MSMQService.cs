using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApplication.MSMQ
{
    public class MSMQService
    {
        private readonly MessageQueue messageQueue = new MessageQueue();

        public MSMQService()
        {
            this.messageQueue.Path = @".\private$\parkingbills";
            if (MessageQueue.Exists(this.messageQueue.Path))
            {
            }
            else
            {
                MessageQueue.Create(this.messageQueue.Path);
            }
        }

        public void AddToQueue(string message)
        {
            this.messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            this.messageQueue.ReceiveCompleted += this.ReceiveFromQueue;

            this.messageQueue.Send(message);

            this.messageQueue.BeginReceive();

            this.messageQueue.Close();
        }

        /// <summary>
        /// Method to fetch message from MSMQ.
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public void ReceiveFromQueue(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = this.messageQueue.EndReceive(e.AsyncResult);

                string data = msg.Body.ToString();

                // Process the logic be sending the message

                // Restart the asynchronous receive operation.
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\Fellowship_Projects\ParkingLotApplication_EntityFramework\ParkingLotApplication\MSMQ\ParkingLot.txt", true))
                {
                    file.WriteLine(data);
                }

                this.messageQueue.BeginReceive();
            }
            catch (MessageQueueException qexception)
            {
                Console.WriteLine(qexception);
            }
        }
    }
}
