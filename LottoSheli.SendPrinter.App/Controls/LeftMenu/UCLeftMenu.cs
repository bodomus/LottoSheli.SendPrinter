using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

using FontAwesome.Sharp;

using LottoSheli.SendPrinter.App.Controls.Basic;
using LottoSheli.SendPrinter.App.EventArg;
using LottoSheli.SendPrinter.DTO;
using LottoSheli.SendPrinter.Entity;

namespace LottoSheli.SendPrinter.App.Controls
{
    public partial class UCLeftMenu : UserControl
    {
        private IconButton currentBtn;
        private Panel leftBorderBtn;

        private IconButton currentSettingsBtn;
        private Panel leftBorderSettingsBtn;

        private IconButton currentScanQueueBtn;
        private Panel leftBorderScanQueueBtn;
        private EntityObservableCollection<TicketTask> _ticketTaskDataSource;
        private LeftMenuItemType currentMenuItem;

        private List<LeftMenuItem> _menuItems = new List<LeftMenuItem>();

        /// <summary>
        /// Raises when menu item was changed.
        /// </summary>
        [Category("Menu Actions")]
        public event EventHandler<UCLeftMenuItemChangedEventArgs> MenuItemChanged;

        /// <summary>
        /// Raises when exit button clicked.
        /// </summary>
        [Category("Menu Actions")]
        public event EventHandler Exit;

        public UCLeftMenu()
        {
            InitializeComponent();

            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, ibtnDashboard.Height);
            pnlMenu.Controls.Add(leftBorderBtn);

            leftBorderSettingsBtn = new Panel();
            leftBorderSettingsBtn.Size = new Size(7, ibtnDashboard.Height);
            pnlSettingsSubMenu.Controls.Add(leftBorderSettingsBtn);

            leftBorderScanQueueBtn = new Panel();
            leftBorderScanQueueBtn.Size = new Size(7, ibtnDashboard.Height);

            _menuItems.Add(CreateMenuItem(LeftMenuItemType.Dashboard, ibtnDashboard));
            _menuItems.Add(CreateMenuItem(LeftMenuItemType.Print, ibtnPrint));
            _menuItems.Add(CreateMenuItem(LeftMenuItemType.ScanQueue, ibtnScanQueue));
            _menuItems.Add(CreateMenuItem(LeftMenuItemType.TicketCheck, ibtnTicketCheck));
            _menuItems.Add(CreateMenuItem(LeftMenuItemType.None, ibtnSettings));
            _menuItems.Add(CreateMenuItem(LeftMenuItemType.SettingsMain, ibtnSettingsMain));
            _menuItems.Add(CreateMenuItem(LeftMenuItemType.SettingsPrint, ibtnSettingsPrint));
            _menuItems.Add(CreateMenuItem(LeftMenuItemType.SettingsScan, ibtnSettingsScan));
            _menuItems.Add(CreateMenuItem(LeftMenuItemType.SettingsOCR, ibtnOCR));
            _menuItems.Add(CreateMenuItem(LeftMenuItemType.Logs, ibtnLogs));
            _menuItems.Add(CreateMenuItem(LeftMenuItemType.Exit, ibtnExit));
        }

        private LeftMenuItem CreateMenuItem(LeftMenuItemType itemType, IconButton button) 
        {
            var item = new LeftMenuItem(itemType, button);
            item.Select += HandleSelectionChange;
            return item;
        }

        private void HandleSelectionChange(object sender, EventArgs e)
        {
            if (sender is LeftMenuItem targetItem) 
            {
                foreach (var item in _menuItems)
                    if (item != targetItem)
                        item.Selected = false;

                ToggleSettingsSubmenu();
                var eventArgs = new UCLeftMenuItemChangedEventArgs() { MenuItem = targetItem.ItemType };
                switch (targetItem.ItemType) 
                { 
                    case LeftMenuItemType.Exit:
                        OnExit(EventArgs.Empty);
                        break;
                    case LeftMenuItemType.None:
                        break;
                    default:
                        OnMenuItemChanged(eventArgs);
                        break;
                }
            }
        }

        private void ToggleSettingsSubmenu() 
        {
            var selectedSettingItem = _menuItems.Find(item => IsSettingMenuItem(item) && item.Selected);
            var show = null != selectedSettingItem;

            if (show != pnlSettingsSubMenu.Visible)
                pnlSettingsSubMenu.Visible = show;

            if (show && selectedSettingItem.Button == ibtnSettings) 
            {
                SwitchMenuItem();
                selectedSettingItem.Highlight = true;
            }
        }

        public void SwitchMenuItem()
        {
            if (Visible)
            {
                int currentIndex = _menuItems.FindIndex(mitem => mitem.Selected);
                int nextIndex = (currentIndex + 1) % _menuItems.Count;
                _menuItems[nextIndex].Selected = true;
            }
        }

        private void SafeQeueuCollectionUpdate(NotifyCollectionChangedAction action, IEnumerable<TicketTask> affectedEnitites, Action<NotifyCollectionChangedAction, IEnumerable<TicketTask>> invoke)
        {
            if (InvokeRequired)
            {
                var d = new Action<NotifyCollectionChangedAction, IEnumerable<TicketTask>, Action<NotifyCollectionChangedAction, IEnumerable<TicketTask>>>(SafeQeueuCollectionUpdate);
                BeginInvoke(d, new object[] { action, affectedEnitites, invoke });
            }
            else
            {
                invoke?.Invoke(action, affectedEnitites);
                ibtnScanQueue.Text = _ticketTaskDataSource.Count > 0 ? $"Scan Queue ({_ticketTaskDataSource.Count})" : "Scan Queue";
            }
        }

        private void UCLeftMenu_Load(object sender, EventArgs e)
        {
            _menuItems[0].Selected = true;
        }

        private void pbLogo_Click(object sender, EventArgs e)
        {
            OpenBrowser("https://lottosheli.co.il/");
        }

        private static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else
                {
                    throw;
                }
            }
        }

        protected virtual void OnMenuItemChanged(UCLeftMenuItemChangedEventArgs e)
        {
            MenuItemChanged?.Invoke(this, e);
        }

        protected virtual void OnExit(EventArgs e)
        {
            Exit?.Invoke(this, e);
        }

        private bool IsSettingMenuItem(LeftMenuItem targetItem) => targetItem.ItemType switch 
        {
            LeftMenuItemType.None => true,
            LeftMenuItemType.SettingsMain => true,
            LeftMenuItemType.SettingsPrint => true,
            LeftMenuItemType.SettingsScan => true,
            LeftMenuItemType.SettingsOCR => true,
            _ => false
        };
    }
}
