using LottoSheli.SendPrinter.App.Controls;
using LottoSheli.SendPrinter.App.Controls.Basic;

namespace LottoSheli.SendPrinter.App.EventArg
{
    /// <summary>
    /// Event args for <see cref="UCLeftMenu.MenuItemChanged" event/>
    /// </summary>
    public class UCLeftMenuItemChangedEventArgs : System.EventArgs
    {
        public LeftMenuItemType MenuItem { get; init; }

        public bool Cancel { get; set; }
    }
}
