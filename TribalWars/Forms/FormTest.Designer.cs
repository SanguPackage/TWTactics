namespace TribalWars
{
    partial class FormTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.uiCommandManager1 = new Janus.Windows.UI.CommandBars.UICommandManager(this.components);
            this.TopRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
            this.LeftRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
            this.RightRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
            this.BottomRebar1 = new Janus.Windows.UI.CommandBars.UIRebar();
            this.uiContextMenu1 = new Janus.Windows.UI.CommandBars.UIContextMenu();
            this.Command0 = new Janus.Windows.UI.CommandBars.UICommand("Command0");
            this.Command1 = new Janus.Windows.UI.CommandBars.UICommand("Command1");
            this.Command2 = new Janus.Windows.UI.CommandBars.UICommand("Command2");
            this.Command3 = new Janus.Windows.UI.CommandBars.UICommand("Command3");
            this.Command01 = new Janus.Windows.UI.CommandBars.UICommand("Command0");
            this.Command02 = new Janus.Windows.UI.CommandBars.UICommand("Command0");
            this.Command11 = new Janus.Windows.UI.CommandBars.UICommand("Command1");
            this.Command21 = new Janus.Windows.UI.CommandBars.UICommand("Command2");
            this.Command31 = new Janus.Windows.UI.CommandBars.UICommand("Command3");
            ((System.ComponentModel.ISupportInitialize)(this.uiCommandManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopRebar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftRebar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightRebar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomRebar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiContextMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // uiCommandManager1
            // 
            this.uiCommandManager1.BottomRebar = this.BottomRebar1;
            this.uiCommandManager1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.Command0,
            this.Command1,
            this.Command2,
            this.Command3});
            this.uiCommandManager1.ContainerControl = this;
            this.uiCommandManager1.ContextMenus.AddRange(new Janus.Windows.UI.CommandBars.UIContextMenu[] {
            this.uiContextMenu1});
            this.uiCommandManager1.Id = new System.Guid("26b37e26-bcda-4c60-8499-e67e7d2e09e5");
            this.uiCommandManager1.LeftRebar = this.LeftRebar1;
            this.uiCommandManager1.RightRebar = this.RightRebar1;
            this.uiCommandManager1.TopRebar = this.TopRebar1;
            // 
            // TopRebar1
            // 
            this.TopRebar1.CommandManager = this.uiCommandManager1;
            this.TopRebar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopRebar1.Location = new System.Drawing.Point(0, 0);
            this.TopRebar1.Name = "TopRebar1";
            this.TopRebar1.Size = new System.Drawing.Size(690, 0);
            // 
            // LeftRebar1
            // 
            this.LeftRebar1.CommandManager = this.uiCommandManager1;
            this.LeftRebar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftRebar1.Location = new System.Drawing.Point(0, 0);
            this.LeftRebar1.Name = "LeftRebar1";
            this.LeftRebar1.Size = new System.Drawing.Size(0, 642);
            // 
            // RightRebar1
            // 
            this.RightRebar1.CommandManager = this.uiCommandManager1;
            this.RightRebar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightRebar1.Location = new System.Drawing.Point(690, 0);
            this.RightRebar1.Name = "RightRebar1";
            this.RightRebar1.Size = new System.Drawing.Size(0, 642);
            // 
            // BottomRebar1
            // 
            this.BottomRebar1.CommandManager = this.uiCommandManager1;
            this.BottomRebar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomRebar1.Location = new System.Drawing.Point(0, 642);
            this.BottomRebar1.Name = "BottomRebar1";
            this.BottomRebar1.Size = new System.Drawing.Size(690, 0);
            // 
            // uiContextMenu1
            // 
            this.uiContextMenu1.CommandManager = this.uiCommandManager1;
            this.uiContextMenu1.Commands.AddRange(new Janus.Windows.UI.CommandBars.UICommand[] {
            this.Command01,
            this.Command02,
            this.Command11,
            this.Command21,
            this.Command31});
            this.uiContextMenu1.Key = "ContextMenu1";
            this.uiContextMenu1.ShowShadowUnderMenus = Janus.Windows.UI.InheritableBoolean.True;
            this.uiContextMenu1.CommandControlValueChanged += new Janus.Windows.UI.CommandBars.CommandEventHandler(this.uiContextMenu1_CommandControlValueChanged);
            // 
            // Command0
            // 
            this.Command0.CommandType = Janus.Windows.UI.CommandBars.CommandType.ColorPickerCommand;
            this.Command0.ControlValue = System.Drawing.Color.Black;
            this.Command0.IsEditableControl = Janus.Windows.UI.InheritableBoolean.False;
            this.Command0.Key = "Command0";
            this.Command0.Name = "Command0";
            this.Command0.Text = "Test123";
            // 
            // Command1
            // 
            this.Command1.Key = "Command1";
            this.Command1.Name = "Command1";
            this.Command1.Text = "test2";
            // 
            // Command2
            // 
            this.Command2.CommandType = Janus.Windows.UI.CommandBars.CommandType.ControlPopup;
            this.Command2.Key = "Command2";
            this.Command2.Name = "Command2";
            this.Command2.Text = "test3";
            // 
            // Command3
            // 
            this.Command3.CommandType = Janus.Windows.UI.CommandBars.CommandType.ControlPopup;
            this.Command3.Key = "Command3";
            this.Command3.Name = "Command3";
            this.Command3.Text = "test4";
            // 
            // Command01
            // 
            this.Command01.ControlValue = System.Drawing.Color.Black;
            this.Command01.Key = "Command0";
            this.Command01.Name = "Command01";
            // 
            // Command02
            // 
            this.Command02.ControlValue = System.Drawing.Color.Black;
            this.Command02.Key = "Command0";
            this.Command02.Name = "Command02";
            // 
            // Command11
            // 
            this.Command11.Key = "Command1";
            this.Command11.Name = "Command11";
            // 
            // Command21
            // 
            this.Command21.Key = "Command2";
            this.Command21.Name = "Command21";
            // 
            // Command31
            // 
            this.Command31.Key = "Command3";
            this.Command31.Name = "Command31";
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 642);
            this.Controls.Add(this.LeftRebar1);
            this.Controls.Add(this.RightRebar1);
            this.Controls.Add(this.TopRebar1);
            this.Controls.Add(this.BottomRebar1);
            this.Name = "FormTest";
            this.Text = "FormTest";
            this.Load += new System.EventHandler(this.FormTest_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormTest_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiCommandManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopRebar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftRebar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightRebar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomRebar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiContextMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.UI.CommandBars.UICommandManager uiCommandManager1;
        private Janus.Windows.UI.CommandBars.UIRebar BottomRebar1;
        private Janus.Windows.UI.CommandBars.UIRebar LeftRebar1;
        private Janus.Windows.UI.CommandBars.UIRebar RightRebar1;
        private Janus.Windows.UI.CommandBars.UIRebar TopRebar1;
        private Janus.Windows.UI.CommandBars.UICommand Command0;
        private Janus.Windows.UI.CommandBars.UICommand Command1;
        private Janus.Windows.UI.CommandBars.UICommand Command2;
        private Janus.Windows.UI.CommandBars.UICommand Command3;
        private Janus.Windows.UI.CommandBars.UIContextMenu uiContextMenu1;
        private Janus.Windows.UI.CommandBars.UICommand Command01;
        private Janus.Windows.UI.CommandBars.UICommand Command02;
        private Janus.Windows.UI.CommandBars.UICommand Command11;
        private Janus.Windows.UI.CommandBars.UICommand Command21;
        private Janus.Windows.UI.CommandBars.UICommand Command31;


    }
}