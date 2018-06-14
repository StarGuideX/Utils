using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfCustomControlLibrary.Commands
{
    public class CustomButtonCommand
    {
        private static RoutedUICommand buttonLeftClickCommand;

        static CustomButtonCommand()
        {
            
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new MouseGesture(MouseAction.LeftClick, ModifierKeys.None));

            buttonLeftClickCommand = new RoutedUICommand("buttonLeftClickCommand", "buttonLeftClickCommand", typeof(CustomButtonCommand), inputs);
        }

        public static RoutedUICommand ButtonLeftClickCommand
        {
            get { return buttonLeftClickCommand; }
        }
    }
}
