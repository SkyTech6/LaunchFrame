using System.ComponentModel;
using System.Windows.Forms;

namespace LaunchFrame.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        private TableLayoutPanel tableLayout;
        private CheckBox chkAlecaframe;
        private TextBox txtAlecaframePath;
        private Button btnBrowseAlecaframe;
        private TextBox txtAlecaframeArgs;
        private CheckBox chkOverframe;
        private TextBox txtOverframePath;
        private Button btnBrowseOverframe;
        private TextBox txtOverframeArgs;
        private CheckBox chkWarframe;
        private TextBox txtWarframeUri;
        private CheckBox chkWaitForHelpers;
        private CheckBox chkSkipIfRunning;
        private Button btnLaunch;
        private Button btnSave;
        private Button btnOptions;
        private Button btnCreateOneClick;
        private Label lblStatus;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.tableLayout = new TableLayoutPanel();
            this.chkAlecaframe = new CheckBox();
            this.txtAlecaframePath = new TextBox();
            this.btnBrowseAlecaframe = new Button();
            this.txtAlecaframeArgs = new TextBox();
            this.chkOverframe = new CheckBox();
            this.txtOverframePath = new TextBox();
            this.btnBrowseOverframe = new Button();
            this.txtOverframeArgs = new TextBox();
            this.chkWarframe = new CheckBox();
            this.txtWarframeUri = new TextBox();
            this.chkWaitForHelpers = new CheckBox();
            this.chkSkipIfRunning = new CheckBox();
            this.btnLaunch = new Button();
            this.btnSave = new Button();
            this.btnOptions = new Button();
            this.btnCreateOneClick = new Button();
            this.lblStatus = new Label();
            this.tableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 3;
            this.tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            this.tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            this.tableLayout.RowCount = 9;
            this.tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F)); // Aleca path
            this.tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // Aleca args
            this.tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F)); // Overframe path
            this.tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F)); // Overframe args
            this.tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F)); // Warframe
            this.tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F)); // Wait
            this.tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F)); // Skip
            this.tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F)); // Create one-click
            this.tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // Buttons
            this.tableLayout.Dock = DockStyle.Top;
            this.tableLayout.Padding = new Padding(8);
            this.tableLayout.Size = new Size(760, 320);
            this.tableLayout.Name = "tableLayout";
            // 
            // chkAlecaframe
            // 
            this.chkAlecaframe.AutoSize = true;
            this.chkAlecaframe.Text = "AlecaFrame";
            this.chkAlecaframe.Dock = DockStyle.Fill;
            this.chkAlecaframe.TextAlign = ContentAlignment.MiddleLeft;
            this.tableLayout.Controls.Add(this.chkAlecaframe, 0, 0);
            // 
            // txtAlecaframePath
            // 
            this.txtAlecaframePath.Dock = DockStyle.Fill;
            this.tableLayout.Controls.Add(this.txtAlecaframePath, 1, 0);
            // 
            // btnBrowseAlecaframe
            // 
            this.btnBrowseAlecaframe.Text = "Browse";
            this.btnBrowseAlecaframe.Dock = DockStyle.Fill;
            this.btnBrowseAlecaframe.Click += new EventHandler(this.OnBrowseAlecaframe);
            this.tableLayout.Controls.Add(this.btnBrowseAlecaframe, 2, 0);

            // txtAlecaframeArgs
            this.txtAlecaframeArgs.Dock = DockStyle.Fill;
            this.txtAlecaframeArgs.PlaceholderText = "Arguments (optional)";
            this.tableLayout.Controls.Add(this.txtAlecaframeArgs, 1, 1);
            var lblAlecaframeArgs = new Label
            {
                Text = "Args",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            this.tableLayout.Controls.Add(lblAlecaframeArgs, 0, 1);
            // 
            // chkOverframe
            // 
            this.chkOverframe.AutoSize = true;
            this.chkOverframe.Text = "Overframe";
            this.chkOverframe.Dock = DockStyle.Fill;
            this.chkOverframe.TextAlign = ContentAlignment.MiddleLeft;
            this.tableLayout.Controls.Add(this.chkOverframe, 0, 2);
            // 
            // txtOverframePath
            // 
            this.txtOverframePath.Dock = DockStyle.Fill;
            this.tableLayout.Controls.Add(this.txtOverframePath, 1, 2);
            // 
            // btnBrowseOverframe
            // 
            this.btnBrowseOverframe.Text = "Browse";
            this.btnBrowseOverframe.Dock = DockStyle.Fill;
            this.btnBrowseOverframe.Click += new EventHandler(this.OnBrowseOverframe);
            this.tableLayout.Controls.Add(this.btnBrowseOverframe, 2, 2);

            // txtOverframeArgs
            this.txtOverframeArgs.Dock = DockStyle.Fill;
            this.txtOverframeArgs.PlaceholderText = "Arguments (optional)";
            this.tableLayout.Controls.Add(this.txtOverframeArgs, 1, 3);
            var lblOverframeArgs = new Label
            {
                Text = "Args",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            this.tableLayout.Controls.Add(lblOverframeArgs, 0, 3);
            // 
            // chkWarframe
            // 
            this.chkWarframe.AutoSize = true;
            this.chkWarframe.Text = "Warframe";
            this.chkWarframe.Dock = DockStyle.Fill;
            this.chkWarframe.TextAlign = ContentAlignment.MiddleLeft;
            this.tableLayout.Controls.Add(this.chkWarframe, 0, 4);
            // 
            // txtWarframeUri
            // 
            this.txtWarframeUri.Dock = DockStyle.Fill;
            this.tableLayout.Controls.Add(this.txtWarframeUri, 1, 4);
            // 
            // chkWaitForHelpers
            // 
            this.chkWaitForHelpers.AutoSize = true;
            this.chkWaitForHelpers.Text = "Wait for helper readiness";
            this.chkWaitForHelpers.Dock = DockStyle.Fill;
            this.tableLayout.Controls.Add(this.chkWaitForHelpers, 0, 5);
            this.tableLayout.SetColumnSpan(this.chkWaitForHelpers, 2);
            // 
            // chkSkipIfRunning
            // 
            this.chkSkipIfRunning.AutoSize = true;
            this.chkSkipIfRunning.Text = "Skip if already running";
            this.chkSkipIfRunning.Dock = DockStyle.Fill;
            this.tableLayout.Controls.Add(this.chkSkipIfRunning, 0, 6);
            this.tableLayout.SetColumnSpan(this.chkSkipIfRunning, 2);
            // 
            // btnOptions
            // 
            this.btnOptions.Text = "Webpages";
            this.btnOptions.Dock = DockStyle.Fill;
            this.btnOptions.Click += new EventHandler(this.OnOpenOptions);
            this.tableLayout.Controls.Add(this.btnOptions, 2, 5);
            // 
            // btnSave
            // 
            this.btnSave.Text = "Save";
            this.btnSave.Dock = DockStyle.Fill;
            this.btnSave.Click += new EventHandler(this.OnSaveConfig);
            this.tableLayout.Controls.Add(this.btnSave, 2, 6);

            // btnCreateOneClick
            this.btnCreateOneClick.Text = "Create One-Click";
            this.btnCreateOneClick.Dock = DockStyle.Fill;
            this.btnCreateOneClick.Click += new EventHandler(this.OnCreateOneClick);
            this.tableLayout.Controls.Add(this.btnCreateOneClick, 2, 7);
            // 
            // btnLaunch
            // 
            this.btnLaunch.Text = "Launch Session";
            this.btnLaunch.Dock = DockStyle.Fill;
            this.tableLayout.Controls.Add(this.btnLaunch, 2, 8);
            this.tableLayout.Controls.Add(this.btnLaunch, 2, 7);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = false;
            this.lblStatus.Dock = DockStyle.Bottom;
            this.lblStatus.Text = "Ready";
            this.lblStatus.Padding = new Padding(8, 4, 8, 4);
            this.lblStatus.Name = "lblStatus";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(784, 360);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.tableLayout);
            this.MinimumSize = new Size(700, 360);
            this.Name = "MainForm";
            this.Text = "LaunchFrame";
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
