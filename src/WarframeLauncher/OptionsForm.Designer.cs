using System.ComponentModel;
using System.Windows.Forms;

namespace LaunchFrame.UI
{
    partial class OptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null!;

        private CheckedListBox checkedUrls;
        private TextBox txtNewUrl;
        private Button btnAdd;
        private Button btnRemove;
        private Button btnOk;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new Container();
            this.checkedUrls = new CheckedListBox();
            this.txtNewUrl = new TextBox();
            this.btnAdd = new Button();
            this.btnRemove = new Button();
            this.btnOk = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();
            // 
            // checkedUrls
            // 
            this.checkedUrls.CheckOnClick = true;
            this.checkedUrls.FormattingEnabled = true;
            this.checkedUrls.Location = new System.Drawing.Point(12, 12);
            this.checkedUrls.Size = new System.Drawing.Size(360, 148);
            this.checkedUrls.Name = "checkedUrls";
            // 
            // txtNewUrl
            // 
            this.txtNewUrl.Location = new System.Drawing.Point(12, 173);
            this.txtNewUrl.Size = new System.Drawing.Size(264, 23);
            this.txtNewUrl.Name = "txtNewUrl";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(282, 172);
            this.btnAdd.Size = new System.Drawing.Size(90, 25);
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.OnAddUrl);
            this.btnAdd.Name = "btnAdd";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(12, 206);
            this.btnRemove.Size = new System.Drawing.Size(90, 25);
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.OnRemoveUrl);
            this.btnRemove.Name = "btnRemove";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(196, 206);
            this.btnOk.Size = new System.Drawing.Size(88, 25);
            this.btnOk.Text = "OK";
            this.btnOk.DialogResult = DialogResult.OK;
            this.btnOk.Click += new System.EventHandler(this.OnAccept);
            this.btnOk.Name = "btnOk";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(284, 206);
            this.btnCancel.Size = new System.Drawing.Size(88, 25);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 246);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtNewUrl);
            this.Controls.Add(this.checkedUrls);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Webpages";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
