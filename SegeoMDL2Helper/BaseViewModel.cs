using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace FontAssetHelper
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                Action action = new Action(()=> PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName)));

                if (App.Current.Dispatcher.Thread.Equals(System.Threading.Thread.CurrentThread))
                {
                    action();
                }
                else
                {
                    App.Current.Dispatcher.Invoke(action);
                }                
            }
        }


    }
}
