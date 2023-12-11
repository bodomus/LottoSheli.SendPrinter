using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LottoSheli.SendPrinter.App.Controls.Basic
{
    /// <summary>
    /// Left menu Item type
    /// </summary>
    public enum LeftMenuItemType
    {
        None,
        Dashboard,
        Print,
        ScanQueue,
        TicketCheck,
        SettingsMain,
        SettingsPrint,
        SettingsScan,
        SettingsOCR,
        Logs,
        Authorize,
        Exit
    }

    public class LeftMenuItem
    {
        private bool _selected = false;
        private bool _hl = false;
        private IconButton _btn;
        private LeftMenuItemType _itemType;

        public bool Selected
        {
            get => _selected;
            set => SetSelected(value);
        }

        public bool Visible
        {
            get => _btn.Visible;
            set => _btn.Visible = value;
        }

        public bool Highlight 
        {
            get => _hl;
            set => SetHighlight(value);
        }

        public IconButton Button => _btn;

        public LeftMenuItemType ItemType => _itemType;

        public event EventHandler Select;

        public LeftMenuItem(LeftMenuItemType itemType, IconButton button)
        {
            
            _itemType = itemType;
            _btn = button;
            _btn.Click += _btn_Click;
            _btn.Paint += _btn_Paint;
            
        }

        private void _btn_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(
                new SolidBrush(_hl ? _btn.ForeColor : _btn.BackColor), 
                new Rectangle(0, 0, 6, _btn.Height));
        }

        private void _btn_Click(object sender, EventArgs e)
        {
            SetSelected(true);
        }

        private void SetSelected(bool val)
        {
            bool changed = val != _selected;

            _selected = val;

            _btn.Enabled = !_selected;
            SetHighlight(val);

            if (changed && val)
                Select?.Invoke(this, EventArgs.Empty);
        }

        private void SetHighlight(bool hl) 
        {
            _hl = hl;
            
            _btn.BackColor = hl ? SystemColors.ControlLightLight : SystemColors.Control;
            _btn.ForeColor = hl ? SystemColors.ActiveCaptionText : SystemColors.ControlText;
            _btn.IconColor = hl ? SystemColors.ActiveCaptionText : SystemColors.ControlText;
            _btn.Invalidate();
        }
    }
}
