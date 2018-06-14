using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WpfCustomControlLibrary.Commands;

namespace WpfCustomControlLibrary
{
    [TemplatePart(Name = "PART_CloseImg",Type =typeof(RangeBase))]
    public class CustomMeassageBox : Window
    {
        #region Property
        public new string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public IList<Button> CtrlButtonCollection
        {
            get { return (IList<Button>)GetValue(CtrlButtonCollectionProperty); }
            set { SetValue(CtrlButtonCollectionProperty, value); }
        }

        public Style CtrlButtonStyle
        {
            get { return (Style)GetValue(CtrlButtonStyleProperty); }
            set { SetValue(CtrlButtonStyleProperty, value); }
        }
        #endregion
        #region DependencyProperty
        public static new DependencyProperty TitleProperty;
        public static DependencyProperty MessageProperty;
        public static DependencyProperty CtrlButtonCollectionProperty;
        public static DependencyProperty CtrlButtonStyleProperty;
        #endregion

        static CustomMeassageBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomMeassageBox), new FrameworkPropertyMetadata(typeof(CustomMeassageBox)));

            TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(CustomMeassageBox));
            MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(CustomMeassageBox));
            CtrlButtonCollectionProperty = DependencyProperty.Register("CtrlButtonCollection", typeof(IList<Button>), typeof(CustomMeassageBox));
            CtrlButtonStyleProperty = DependencyProperty.Register("CtrlButtonStyle", typeof(Style), typeof(CustomMeassageBox));
        }

        public CustomMeassageBox()
        {
            SetValue(CtrlButtonCollectionProperty, new List<Button>());

            try
            {
                this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                this.AllowsTransparency = true;
                this.WindowStyle = System.Windows.WindowStyle.None;
                this.ShowInTaskbar = true;
                this.Topmost = true;

                Resources.Source = new Uri(@"/WpfCustomControlLibrary;component/Themes/Generic.xaml", UriKind.Relative);

                CtrlButtonStyle = Resources["default_Button"] as Style;

            }
            catch
            {
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Image imageClose = GetTemplateChild("PART_CloseImg") as Image;
            if (imageClose != null)
            {
                imageClose.MouseEnter += (s, e) => {
                    imageClose.Opacity = 1;
                    this.Cursor = Cursors.Hand;
                };

                imageClose.MouseLeave += (s, e) =>
                {
                    imageClose.Opacity = 0.7;
                    this.Cursor = Cursors.Arrow;
                };
            }
            foreach (Button item in CtrlButtonCollection)
            {
                item.MouseEnter += (s, e) => {
                    this.Cursor = Cursors.Hand;
                };

                item.MouseLeave += (s, e) => {
                    this.Cursor = Cursors.Arrow;
                };
            }
            

        }

        /// <summary>
        ///  显示一个消息，然后返回结果的消息框。
        /// </summary>
        /// <param name="messageBoxText"> 指定文本显示的 System.String 。</param>
        /// <returns> 指定的 System.Windows.MessageBoxResult 值哪个消息框按钮由用户单击。</returns>
        public static MessageBoxResult Show(string messageBoxText)
        {
            return Show(null, messageBoxText, "消息", MessageBoxButton.OKCancel, MessageBoxResult.None);
        }
        /// <summary>
        /// 显示一个消息和标题栏声明的消息框;并返回结果。
        /// </summary>
        /// <param name="messageBoxText">指定文本显示的 System.String 。</param>
        /// <param name="caption">指定标题栏声明中显示的 System.String 。</param>
        /// <returns>System.Windows.MessageBoxResult</returns>
        public static MessageBoxResult Show(string messageBoxText, string caption)
        {
            return Show(null, messageBoxText, caption, MessageBoxButton.OKCancel, MessageBoxResult.None);
        }

        /// <summary>
        /// 显示在指定的窗口前面的消息框。消息框显示该消息并返回结果。
        /// </summary>
        /// <param name="owner">表示消息框的所有者窗口的 System.Windows.Window 。</param>
        /// <param name="messageBoxText">指定文本显示的 System.String。 </param>
        /// <returns>指定的 System.Windows.MessageBoxResult 值哪个消息框按钮由用户单击。</returns>
        public static MessageBoxResult Show(Window owner, string messageBoxText)
        {
            return Show(owner, messageBoxText, "提示消息", MessageBoxButton.OKCancel, MessageBoxResult.None);
        }

        /// <summary>
        /// 显示一个消息、标题栏声明和按钮的消息框;并返回结果。
        /// </summary>
        /// <param name="messageBoxText">指定文本显示的 System.String 。</param>
        /// <param name="caption">指定标题栏声明中显示的 System.String 。</param>
        /// <param name="button">指定的 System.Windows.MessageBoxButton 值要显示的按钮或按钮。</param>
        /// <returns>指定的 System.Windows.MessageBoxResult 值哪个消息框按钮由用户单击。</returns>
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            return Show(null, messageBoxText, caption, button, MessageBoxResult.None);
        }

        /// <summary>
        /// 显示在指定的窗口前面的消息框。消息框显示消息和标题栏声明;它返回结果。
        /// </summary>
        /// <param name="owner">表示消息框的所有者窗口的 System.Windows.Window 。</param>
        /// <param name="messageBoxText">指定文本显示的 System.String 。</param>
        /// <param name="caption">指定标题栏声明中显示的 System.String 。</param>
        /// <returns>指定的 System.Windows.MessageBoxResult 值哪个消息框按钮由用户单击。</returns>
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption)
        {
            return Show(owner, messageBoxText, caption, MessageBoxButton.OKCancel, MessageBoxResult.None);
        }

        /// <summary>
        /// 显示在指定的窗口前面的消息框。消息框显示消息、标题栏声明和按钮;它还返回结果。
        /// </summary>
        /// <param name="owner">表示消息框的所有者窗口的 System.Windows.Window 。</param>
        /// <param name="messageBoxText">指定文本显示的 System.String 。</param>
        /// <param name="caption">指定标题栏声明中显示的 System.String 。</param>
        /// <param name="button">指定的 System.Windows.MessageBoxButton 值要显示的按钮或按钮。</param>
        /// <returns>指定的 System.Windows.MessageBoxResult 值哪个消息框按钮由用户单击。</returns>
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
        {
            return Show(owner, messageBoxText, caption, button, MessageBoxResult.None);
        }

        /// <summary>
        /// 显示具有消息、标题栏说明、按钮的消息框;这接受一个默认消息框的结果并返回结果。
        /// </summary>
        /// <param name="messageBoxText">指定文本显示的 System.String 。</param>
        /// <param name="caption">指定标题栏声明中显示的 System.String 。</param>
        /// <param name="button">指定的 System.Windows.MessageBoxButton 值要显示的按钮或按钮。</param>
        /// <param name="defaultResult">指定消息框的默认结果的 System.Windows.MessageBoxResult 值。</param>
        /// <returns>指定的 System.Windows.MessageBoxResult 值哪个消息框按钮由用户单击。</returns>
        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult)
        {
            return Show(null, messageBoxText, caption, button, defaultResult);
        }

        /// <summary>
        /// 显示在指定的窗口前面的消息框。显示具有消息、标题栏说明、按钮的消息框;这接受一个默认消息框的结果并返回结果。
        /// </summary>
        /// <param name="owner">表示消息框的所有者窗口的 System.Windows.Window 。</param>
        /// <param name="messageBoxText">指定文本显示的 System.String 。</param>
        /// <param name="caption">指定标题栏声明中显示的 System.String 。</param>
        /// <param name="button">指定的 System.Windows.MessageBoxButton 值要显示的按钮或按钮。</param>
        /// <param name="defaultResult">指定消息框的默认结果的 System.Windows.MessageBoxResult 值。</param>
        /// <returns>指定的 System.Windows.MessageBoxResult 值哪个消息框按钮由用户单击。</returns>
        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxResult defaultResult)
        {
            // MessageBox.Show();
            CustomMeassageBox customMessageBox = new CustomMeassageBox();
            customMessageBox.Owner = owner;
            customMessageBox.Message = messageBoxText;
            customMessageBox.Title = caption;
            if (owner != null)
            {
                customMessageBox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }

            switch (button)
            {
                case MessageBoxButton.OKCancel:
                    customMessageBox.CtrlButtonCollection.Add(CreateCtrlButtonWithResult(customMessageBox, "确定", true, defaultResult == MessageBoxResult.OK));
                    customMessageBox.CtrlButtonCollection.Add(CreateCtrlButtonWithResult(customMessageBox, "取消", null, defaultResult == MessageBoxResult.Cancel));
                    break;
                case MessageBoxButton.YesNo:
                    customMessageBox.CtrlButtonCollection.Add(CreateCtrlButtonWithResult(customMessageBox, "是", true, defaultResult == MessageBoxResult.Yes));
                    customMessageBox.CtrlButtonCollection.Add(CreateCtrlButtonWithResult(customMessageBox, "否", false, defaultResult == MessageBoxResult.No));

                    break;
                case MessageBoxButton.YesNoCancel:
                    customMessageBox.CtrlButtonCollection.Add(CreateCtrlButtonWithResult(customMessageBox, "是", true, defaultResult == MessageBoxResult.Yes));
                    customMessageBox.CtrlButtonCollection.Add(CreateCtrlButtonWithResult(customMessageBox, "否", false, defaultResult == MessageBoxResult.No));
                    customMessageBox.CtrlButtonCollection.Add(CreateCtrlButtonWithResult(customMessageBox, "取消", null, defaultResult == MessageBoxResult.Cancel));
                    break;
                case MessageBoxButton.OK:
                default:
                    customMessageBox.CtrlButtonCollection.Add(CreateCtrlButtonWithResult(customMessageBox, "确定", true, defaultResult == MessageBoxResult.OK));
                    break;
            }
            bool? result = customMessageBox.ShowDialog();

            // 返回MessageBoxResult
            switch (button)
            {
                case MessageBoxButton.OKCancel:
                    {
                        return result == true ? MessageBoxResult.OK : MessageBoxResult.Cancel;
                    }
                case MessageBoxButton.YesNo:
                    {
                        return result == true ? MessageBoxResult.Yes : MessageBoxResult.No;
                    }
                case MessageBoxButton.YesNoCancel:
                    {
                        return result == true ? MessageBoxResult.Yes : result == false ? MessageBoxResult.No : MessageBoxResult.Cancel;
                    }
                case MessageBoxButton.OK:
                    {
                        return result == true ? MessageBoxResult.OK : MessageBoxResult.Cancel;
                    }
                default:
                    {
                        return result == true ? MessageBoxResult.OK : MessageBoxResult.Cancel;
                    }
            }
        }

        private static Button CreateCtrlButtonWithResult(CustomMeassageBox customMessageBox, string content, bool? dialogResult, bool isDefault = false)
        {
            var btn = new Button() { Style = customMessageBox.CtrlButtonStyle, Content = content, IsDefault = isDefault };
            CommandBinding binding = new CommandBinding(CustomButtonCommand.ButtonLeftClickCommand);
            binding.Executed += (o, e) => { MessageBoxBtn_Excuted(o, e, customMessageBox, dialogResult); };
            btn.CommandBindings.Add(binding);
            return btn;
        }
        /// <summary>
        /// 由于原因，按cancel和no按钮，都会返回false
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        /// <param name="customMessageBox"></param>
        /// <param name="dialogResult"></param>
        private static void MessageBoxBtn_Excuted(object o, ExecutedRoutedEventArgs e, CustomMeassageBox customMessageBox, bool? dialogResult)
        {
            if (dialogResult.HasValue)
            {
                customMessageBox.DialogResult = dialogResult;
            }
            else
            {
                customMessageBox.Close();
            }
        }




    }
}