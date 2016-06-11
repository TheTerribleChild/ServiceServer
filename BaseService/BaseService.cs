using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseService
{
    public class BaseService
    {
        private Thread _serviceThread;
        private bool _isRunning;
        

        protected Thread ServiceThread
        {
            get { return this._serviceThread; }
        }

        public bool IsRunning
        {
            get { return this._isRunning; }
            set { this._isRunning = value; }
        }

        protected BaseService()
        {
            Console.WriteLine("BaseService Created");
        }

        public virtual void StartService()
        {
            Console.WriteLine("BaseService StartService Called");
            _serviceThread = new Thread(Run);
            _serviceThread.Start();
        }

        public virtual void StopService()
        {
            Console.WriteLine("BaseService StopService Called");
            IsRunning = false;
        }

        private void Run()
        {
            Console.WriteLine("BaseService Run Started");
            IsRunning = true;
            while (IsRunning)
            {
                Do();
            }
            Console.WriteLine("BaseService Run Ended");
        }

        protected virtual void Do()
        {
            Console.WriteLine("BaseService doing");
            Thread.Sleep(1000);
        }


    }
}
