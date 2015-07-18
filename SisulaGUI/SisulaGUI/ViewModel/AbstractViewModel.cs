using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

//This class will be used on most of the ViewModels for the Icommand/inotfy implementation.(for ui interaction in WPF)

namespace SisulaGUI.ViewModel
{
    public abstract class AbstractViewModel : ICommand, INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public System.Windows.Input.ICommand iCommand { get; set; }

        public event System.EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public AbstractViewModel()
        {
            this.iCommand = this;
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public virtual bool CanExecute(object o)
        {
            return true;
        }

        public virtual void Execute(object o)
        {
            throw new System.Exception("Execute is not implemented in child class");
        }
    }
}
