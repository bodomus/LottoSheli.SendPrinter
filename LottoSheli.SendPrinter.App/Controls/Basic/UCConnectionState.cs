using LottoSheli.SendPrinter.Remote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LottoSheli.SendPrinter.App.Controls.Basic
{
    public partial class UCConnectionState : UserControl
    {
        private readonly Dictionary<RemoteConnectionState, Color> COLORS = new Dictionary<RemoteConnectionState, Color> 
        { 
            { RemoteConnectionState.Error, Color.Red }, 
            { RemoteConnectionState.Connected, Color.ForestGreen }, 
            { RemoteConnectionState.Disconnected, Color.Gray }, 
            { RemoteConnectionState.Connecting, Color.DarkOrange } 
        };
        private string _name;
        private RemoteConnectionState _state = RemoteConnectionState.Disconnected;
        private Color _accentColor => COLORS.TryGetValue(_state, out Color stateColor) ? stateColor : Color.Gray;
        private string _label => $"{_name}: {_state}";

        public string TargetName 
        { 
            get => _name; 
            set { SetTargetName(value); } 
        }

        public RemoteConnectionState State 
        { 
            get => _state; 
            set { SetState(value); } 
        }

        public UCConnectionState()
        {
            InitializeComponent();
        }

        public void SetTargetName(string name) 
        {
            if (InvokeRequired)
                Invoke(() => SetTargetName(name));
            else 
            {
                _name = name;
                _nameLabel.Text = _label;
                _nameLabel.Invalidate();
            }
        }

        public void SetState(RemoteConnectionState value)
        {
            if (InvokeRequired)
                Invoke(() => SetState(value));
            else
            {
                _state = value;
                _nameLabel.Text = _label;
                _nameLabel.ForeColor = _accentColor;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(_accentColor), 4), e.ClipRectangle);
        }

        
    }
}
