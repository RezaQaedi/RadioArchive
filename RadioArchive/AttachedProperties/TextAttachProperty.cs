using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RadioArchive
{
    /// <summary>
    /// the IsFocusedProperty for focuse (keyboard) this element on load
    /// </summary>
    public class IsFocusedProperty : BaseAttachedProperty<IsFocusedProperty, bool> 
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //if we dont have a control then return
            if (!(sender is Control control))
                return;

            //focuse this control when loaded
            control.Loaded += (s, se) => control.Focus();
        }
    }
    
    /// <summary>
    /// the FocusProperty for focuse this element on load
    /// </summary>
    public class FocusProperty : BaseAttachedProperty<FocusProperty, bool> 
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //if we dont have a control then return
            if (sender is not Control control)
                return;

            if((bool)e.NewValue)
                //focuse this control 
                control.Focus();
        }
    } 

    /// <summary>
    /// the FocusAndSelectProperty for focuse and Select this element on load
    /// </summary>
    public class FocusAndSelectProperty : BaseAttachedProperty<FocusAndSelectProperty, bool> 
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //if we dont have a control then return
            if (sender is TextBoxBase control)
            {
                if ((bool)e.NewValue)
                {
                    //focuse this control 
                    control.Focus();

                    //select the text 
                    control.SelectAll();
                }
            }

            // handel the password box as well 
            if (sender is PasswordBox passwordBox)
            {
                if ((bool)e.NewValue)
                {
                    //focuse this control 
                    passwordBox.Focus();

                    //select the text 
                    passwordBox.SelectAll();
                }
            }
        }
    }
}
