using System;
using System.Windows.Input;

namespace RadioArchive
{
    /// <summary>
    /// Basic command that runs an action
    /// </summary>
    class RelayParamiterizedCommand : ICommand
    {
        #region Private members
        /// <summary>
        /// action to run
        /// </summary>
        private Action<object> mAction; 
        #endregion

        #region Public property
        /// <summary>
        /// the event that fire when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor
        public RelayParamiterizedCommand(Action<object> action)
        {
            mAction = action;
        }
        #endregion

        #region Command methods
        /// <summary>
        /// a relay command that always executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;


        public void Execute(object parameter)
        {
            mAction(parameter);
        } 
        #endregion
    }
}
