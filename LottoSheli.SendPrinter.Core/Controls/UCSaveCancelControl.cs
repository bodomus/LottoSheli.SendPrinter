using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LottoSheli.SendPrinter.Core.Controls
{
    public partial class UCSaveCancelControl : UserControl
    {
        /// <summary>
        /// Invokes when Save button clicked
        /// </summary>
        [Category("Control Actions")]
        [Description("Invokes when Save button clicked")]
        public event EventHandler SaveClick;

        /// <summary>
        /// Invokes when Cancel button clicked
        /// </summary>
        [Category("Control Actions")]
        [Description("Invokes when Cancel button clicked")]
        public event EventHandler CancelClick;

        public UCSaveCancelControl()
        {
            InitializeComponent();
        }

        public void HideCancel()
        {
            btnCancel.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OnSaveClick(e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnCancelClick(e);
        }

        /// <summary>
        /// Invokes when Save button clicked
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSaveClick(EventArgs e)
        {
            SaveClick?.Invoke(this, e);
        }

        /// <summary>
        /// Invokes when Cancel button clicked
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCancelClick(EventArgs e)
        {
            CancelClick?.Invoke(this, e);
        }
    }
}
