using System;
using System.Windows;

namespace RadioArchive
{
    /// <summary>
    /// the base atach propery to replace te vanila wpf property
    /// </summary>
    /// <typeparam name="Parent">the parent class to be attached propery</typeparam>
    /// <typeparam name="Property">the type of this attached property</typeparam>
    public abstract class BaseAttachedProperty<Parent, Property> where Parent : new()
    {

        #region public event

        /// <summary>
        /// fires when the value changed
        /// </summary>
        Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };
        
        /// <summary>
        /// fires when the value changed even the value is the same 
        /// </summary>
        Action<DependencyObject, object> ValueUpdated = (sender, value) => { };

        #endregion

        #region public properties
        /// <summary>
        /// a singleton instance of our parent class  
        /// </summary>
        public static Parent Instance { get; set; } = new Parent();
        #endregion

        #region attach peroperty definers

        /// <summary>
        /// the attach propert for this class
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached("Value", 
                typeof(Property), 
                typeof(BaseAttachedProperty<Parent, Property>),
                new UIPropertyMetadata(default(Property), 
                    new PropertyChangedCallback(OnValuePropertyChanged),
                    new CoerceValueCallback(OnValuePropertyUpdated)
                ));

        /// <summary>
        /// the callback event when the <see cref="ValueProperty"/> changed
        /// </summary>
        /// <param name="d">the UI element that had its property changed</param>
        /// <param name="e">the argument for the event</param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //calling the parent method
            (Instance as BaseAttachedProperty<Parent, Property>)?.OnValueChanged(d, e);

            //call event listeners
            (Instance as BaseAttachedProperty<Parent, Property>)?.ValueChanged(d, e);
        }
        
        /// <summary>
        /// the callback event when the <see cref="ValueProperty"/> changed, even it is the same value 
        /// </summary>
        /// <param name="d">the UI element that had its property changed</param>
        /// <param name="e">the argument for the event</param>
        private static object OnValuePropertyUpdated(DependencyObject d, object value)
        {
            //calling the parent method
            (Instance as BaseAttachedProperty<Parent, Property>)?.OnValueUpdated(d, value);

            //call event listeners
            (Instance as BaseAttachedProperty<Parent, Property>)?.ValueUpdated(d, value);

            //return the value 
            return value;
        }

        /// <summary>
        /// sets the attached property
        /// </summary>
        /// <param name="d">the element to get the property from</param>
        /// <returns></returns>
        public static void SetValue(DependencyObject d, Property value) => d.SetValue(ValueProperty, value);

        /// <summary>
        /// gets the attached property
        /// </summary>
        /// <param name="d">the element to get the property from</param>
        /// <returns></returns>
        public static Property GetValue(DependencyObject d) => (Property)d.GetValue(ValueProperty);


        #endregion

        #region Event Methods

        /// <summary>
        /// the method that is called when any attached property of its type is changed
        /// </summary>
        /// <param name="sender">the UI element that its propery is changed for</param>
        /// <param name="e">the argument for this event</param>
        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { } 

        /// <summary>
        /// the method that is called when any attached property of its type is changed, event its the same
        /// </summary>
        /// <param name="sender">the UI element that its propery is changed for</param>
        /// <param name="value">the object itself</param>
        public virtual void OnValueUpdated(DependencyObject sender, object value) { }

        #endregion
    }
}
