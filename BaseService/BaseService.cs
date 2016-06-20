using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseService
{

    public enum ServiceState { On, Off}

    public abstract class BaseService<T> : INotifyPropertyChanged where T: BaseService<T>, new()
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private ServiceState _currentServiceState;

        private static T _instance;

        public abstract string ServiceName{ get; }

        public static T GetInstance()
        {
            if (_instance == null)
                _instance = new T();
            return _instance;
        }

        protected Thread ServiceThread{ get; set; }
        private ServiceState TargetServiceState { get; set; }

        public ServiceState CurrentServiceState
        {
            get
            {
                return this._currentServiceState;
            }
            private set
            {
                this._currentServiceState = value;
                NotifyPropertyChanged("CurrentServiceState");
            }
        }

        public string ServiceDirectory{ get; private set; }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /*==============================================================================================================*/

        protected BaseService()
        {
            this.ServiceThread = null;
            this.TargetServiceState = ServiceState.Off;
            this.CurrentServiceState = ServiceState.Off;
            this.ServiceDirectory = "";
        }

        public virtual bool SetServiceDirectory(string servicelLocation)
        {
            if (!Directory.Exists(servicelLocation))
                return false;
            ServiceDirectory = servicelLocation;
            return true;
        }

        public virtual bool StartServiceAsync()
        {
            if (CurrentServiceState == ServiceState.On || TargetServiceState == ServiceState.On)
                return true;
            ServiceThread = new Thread(Run);
            ServiceThread.Start();
            return true;
        }

        public virtual bool StartService()
        {
            if (CurrentServiceState == ServiceState.On || TargetServiceState == ServiceState.On)
                return true;
            CurrentServiceState = ServiceState.On;
            return true;
        }

        public virtual bool StopService()
        {
            if (TargetServiceState == ServiceState.Off)
                return true;

            TargetServiceState = ServiceState.Off;

            if(ServiceThread != null && ServiceThread.IsAlive)
            {
                ServiceThread.Join();
            }
            else
            {
                CurrentServiceState = ServiceState.Off;
            }
            
            return true;
        }

        public virtual bool StopServiceAsync()
        {
            if (TargetServiceState == ServiceState.Off)
                return true;

            TargetServiceState = ServiceState.Off;

            if (ServiceThread == null || !ServiceThread.IsAlive)
            {
                CurrentServiceState = ServiceState.Off;
            }

            return true;
        }

        private void Run()
        {
            TargetServiceState = ServiceState.On;
            CurrentServiceState = ServiceState.On;
            while (TargetServiceState == ServiceState.On)
            {
                Do();
            }
            CurrentServiceState = ServiceState.Off;
        }

        protected virtual void Do()
        {
            Console.WriteLine("Running Do");
            Thread.Sleep(1000);
        }


    }
}
