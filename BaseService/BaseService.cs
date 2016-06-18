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

    public abstract class BaseService : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private Thread _serviceThread;
        private ServiceState _currentServiceState;
        private string _serviceLocation;
        private ManualResetEvent _serviceMRE;

        public abstract string ServiceName{ get; }

        protected Thread ServiceThread
        {
            get
            {
                return this._serviceThread;
            }
            private set
            {
                this._serviceThread = value;
            }
        }

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

        public string ServiceLocation
        {
            get
            {
                return this._serviceLocation;
            }
            private set
            {
                this._serviceLocation = value;
            }
        }

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
            this.ServiceLocation = "";
            this._serviceMRE = null;
        }

        public virtual bool SetServiceLocation(string servicelLocation)
        {
            if (!Directory.Exists(servicelLocation))
                return false;
            ServiceLocation = servicelLocation;
            return true;
        }

        public virtual bool StartServiceAsync()
        {
            if (CurrentServiceState == ServiceState.On || TargetServiceState == ServiceState.On)
                return false;
            ServiceThread = new Thread(Run);
            ServiceThread.Start();
            return true;
        }

        public virtual bool StartService()
        {
            if (CurrentServiceState == ServiceState.On || TargetServiceState == ServiceState.On)
                return false;
            ServiceThread = new Thread(Run);
            ServiceThread.Start();
            while (CurrentServiceState != ServiceState.On) ;
            return true;
        }

        public virtual bool StopServiceAsync()
        {
            TargetServiceState = ServiceState.Off;
            return true;
        }

        public virtual bool StopService()
        {
            if (TargetServiceState == ServiceState.Off)
                return true;
            TargetServiceState = ServiceState.Off;
            _serviceMRE.WaitOne();
            return true;
        }

        private void Run()
        {
            TargetServiceState = ServiceState.On;
            CurrentServiceState = ServiceState.On;
            _serviceMRE = new ManualResetEvent(false);
            while (TargetServiceState == ServiceState.On)
            {
                Do();
            }
            _serviceMRE.Set();
            CurrentServiceState = ServiceState.Off;
        }

        protected virtual void Do()
        {
            Thread.Sleep(1000);
        }


    }
}
