using PropertyChanged;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RadioArchive
{
    /// <summary>
    /// A base view model fires property changed events as needed
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        private object mGlobalLock = new();

        /// <summary>
        /// The event that is fired when any child property changes its value 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {};

        /// <summary>
        /// Indicates if this view is busy 
        /// </summary>
        public bool Working { get; set; }

        /// <summary>
        /// call this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name) 
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public virtual void Dispose()
        {
        }

        #region CommandHelpers

        /// <summary>
        /// Runs the command if updating flag is not set.
        /// if the flag is true meaning the function is running then the action not run
        /// if the flag is false indicating no running function the action is run
        /// once the action finished if it was run then the flag is rest to false
        /// </summary>
        /// <param name="updatingFlag"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            // Lock to ensure single access to check
            lock (mGlobalLock)
            {
                // Check if flag property value is true meaning the function is already running
                if (updatingFlag.GetPropertyValue())
                    return;

                // Set the flag to true to indicate we're running 
                updatingFlag.SetPropertyValue(true);
            }

            try
            {
                //runs the passed action
                await action();
            }
            finally
            {
                //sets the property value back to false now that is now finished
                updatingFlag.SetPropertyValue(false);
            }
        }

        /// <summary>
        /// Runs the command if updating flag is not set.
        /// if the flag is true meaning the function is running then the action not run
        /// if the flag is false indicating no running function the action is run
        /// once the action finished if it was run then the flag is rest to false
        /// </summary>
        /// <param name="updatingFlag"></param>
        /// <param name="action"></param>
        /// <typeparam name="T">The type of action return</typeparam>
        /// <returns></returns>
        protected async Task<T> RunCommand<T>(Expression<Func<bool>> updatingFlag, Func<Task<T>> action, T defualtValue = default(T))
        {
            // Lock to ensure single access to check
            lock (mGlobalLock)
            {
                // Check if flag property value is true meaning the function is already running
                if (updatingFlag.GetPropertyValue())
                    return defualtValue;

                // Set the flag to true to indicate we're running 
                updatingFlag.SetPropertyValue(true);
            }

            try
            {
                //runs the passed action
                return await action();
            }
            finally
            {
                //sets the property value back to false now that is now finished
                updatingFlag.SetPropertyValue(false);
            }
        }

        #endregion
    }
}
