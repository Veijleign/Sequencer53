/*
 * Created by SharpDevelop.
 * User: perivar.nerseth
 * Date: 20.07.2012
 * Time: 12:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace MidiVstTest
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectMIDIINToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxMidiInDevices = new System.Windows.Forms.ToolStripComboBox();
            this.selectMIDIOUTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxMidiOutDevices = new System.Windows.Forms.ToolStripComboBox();
            this.selectAudioOutputDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxAudioOutDevices = new System.Windows.Forms.ToolStripComboBox();
            this.vSTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupToolStripMenuItem,
            this.vSTToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(673, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectMIDIINToolStripMenuItem,
            this.comboBoxMidiInDevices,
            this.selectMIDIOUTToolStripMenuItem,
            this.comboBoxMidiOutDevices,
            this.selectAudioOutputDeviceToolStripMenuItem,
            this.comboBoxAudioOutDevices});
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(61, 24);
            this.setupToolStripMenuItem.Text = "Setup";
            // 
            // selectMIDIINToolStripMenuItem
            // 
            this.selectMIDIINToolStripMenuItem.CheckOnClick = true;
            this.selectMIDIINToolStripMenuItem.Name = "selectMIDIINToolStripMenuItem";
            this.selectMIDIINToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.selectMIDIINToolStripMenuItem.Text = "Select MIDI IN...";
            this.selectMIDIINToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SelectMIDIINToolStripMenuItemCheckedChanged);
            // 
            // comboBoxMidiInDevices
            // 
            this.comboBoxMidiInDevices.Enabled = false;
            this.comboBoxMidiInDevices.Name = "comboBoxMidiInDevices";
            this.comboBoxMidiInDevices.Size = new System.Drawing.Size(121, 28);
            this.comboBoxMidiInDevices.SelectedIndexChanged += new System.EventHandler(this.TscMIDIINSelectedIndexChanged);
            // 
            // selectMIDIOUTToolStripMenuItem
            // 
            this.selectMIDIOUTToolStripMenuItem.CheckOnClick = true;
            this.selectMIDIOUTToolStripMenuItem.Name = "selectMIDIOUTToolStripMenuItem";
            this.selectMIDIOUTToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.selectMIDIOUTToolStripMenuItem.Text = "Select MIDI OUT...";
            this.selectMIDIOUTToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SelectMIDIOUTToolStripMenuItemCheckedChanged);
            // 
            // comboBoxMidiOutDevices
            // 
            this.comboBoxMidiOutDevices.Enabled = false;
            this.comboBoxMidiOutDevices.Name = "comboBoxMidiOutDevices";
            this.comboBoxMidiOutDevices.Size = new System.Drawing.Size(121, 28);
            this.comboBoxMidiOutDevices.SelectedIndexChanged += new System.EventHandler(this.TscMIDIOUTSelectedIndexChanged);
            // 
            // selectAudioOutputDeviceToolStripMenuItem
            // 
            this.selectAudioOutputDeviceToolStripMenuItem.CheckOnClick = true;
            this.selectAudioOutputDeviceToolStripMenuItem.Name = "selectAudioOutputDeviceToolStripMenuItem";
            this.selectAudioOutputDeviceToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.selectAudioOutputDeviceToolStripMenuItem.Text = "Select AUDIO OUT ...";
            this.selectAudioOutputDeviceToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SelectAudioOutputDeviceToolStripMenuItemCheckedChanged);
            // 
            // comboBoxAudioOutDevices
            // 
            this.comboBoxAudioOutDevices.Enabled = false;
            this.comboBoxAudioOutDevices.Name = "comboBoxAudioOutDevices";
            this.comboBoxAudioOutDevices.Size = new System.Drawing.Size(121, 28);
            this.comboBoxAudioOutDevices.SelectedIndexChanged += new System.EventHandler(this.TscASIOOutSelectedIndexChanged);
            // 
            // vSTToolStripMenuItem
            // 
            this.vSTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.showToolStripMenuItem,
            this.editParametersToolStripMenuItem});
            this.vSTToolStripMenuItem.Name = "vSTToolStripMenuItem";
            this.vSTToolStripMenuItem.Size = new System.Drawing.Size(48, 24);
            this.vSTToolStripMenuItem.Text = "VST";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.loadToolStripMenuItem.Text = "Load...";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItemClick);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Enabled = false;
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.showToolStripMenuItem.Text = "Show...";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItemClick);
            // 
            // editParametersToolStripMenuItem
            // 
            this.editParametersToolStripMenuItem.Enabled = false;
            this.editParametersToolStripMenuItem.Name = "editParametersToolStripMenuItem";
            this.editParametersToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.editParametersToolStripMenuItem.Text = "Edit Parameters...";
            this.editParametersToolStripMenuItem.Click += new System.EventHandler(this.EditParametersToolStripMenuItemClick);
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Location = new System.Drawing.Point(13, 37);
            this.buttonClearLog.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(100, 28);
            this.buttonClearLog.TabIndex = 6;
            this.buttonClearLog.Text = "Play";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.ButtonClearLogClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 30);
            this.button1.TabIndex = 7;
            this.button1.Text = "Open Sequencer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 519);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonClearLog);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "MidiVstTest";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.Button buttonClearLog;
		private System.Windows.Forms.ToolStripMenuItem editParametersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
		private System.Windows.Forms.ToolStripComboBox comboBoxMidiOutDevices;
		private System.Windows.Forms.ToolStripMenuItem selectMIDIOUTToolStripMenuItem;
		private System.Windows.Forms.ToolStripComboBox comboBoxMidiInDevices;
		private System.Windows.Forms.ToolStripMenuItem selectMIDIINToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem vSTToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem selectAudioOutputDeviceToolStripMenuItem;
		private System.Windows.Forms.ToolStripComboBox comboBoxAudioOutDevices;
        private System.Windows.Forms.Button button1;
    }
}
