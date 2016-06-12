using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BaseService
{

    public enum ServiceState { On, Off}

    public class BaseService : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private Thread _serviceThread;
        private ServiceState _currentServiceState;
        private ManualResetEvent _serviceMRE;

        protected Thread ServiceThread
        {
            get {
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
            this._serviceMRE = null;
        }

        public virtual bool SetDatabaseLocation(string databaseLocation)
        {
            return false;
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
