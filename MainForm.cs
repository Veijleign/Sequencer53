using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

using NAudio.Midi;
using NAudio.Wave;

using Jacobi.Vst.Core;
using System.Threading;

using B;

using System.Windows.Forms.Integration;

namespace MidiVstTest
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		VSTForm vstForm = null;
		List<BInrerface.IXyidiEvent> xyidiEvents;
		MidiIn midiIn;
		MidiOut midiOut;
		bool isKeyDown = false;

		public static Dictionary<string, string> LastDirectoryUsed = new Dictionary<string, string>();

		public MainForm()
		{
			this.xyidiEvents = new List<BInrerface.IXyidiEvent>();
			InitializeComponent();

			for (int device = 0; device < MidiIn.NumberOfDevices; device++)
			{
				comboBoxMidiInDevices.Items.Add(MidiIn.DeviceInfo(device).ProductName);
			}
			if (comboBoxMidiInDevices.Items.Count > 0)
			{
				comboBoxMidiInDevices.SelectedIndex = 0;
			}
			for (int device = 0; device < MidiOut.NumberOfDevices; device++)
			{
				comboBoxMidiOutDevices.Items.Add(MidiOut.DeviceInfo(device).ProductName);
			}
			if (comboBoxMidiOutDevices.Items.Count > 0)
			{
				comboBoxMidiOutDevices.SelectedIndex = 0;
			}

			if (comboBoxMidiInDevices.Items.Count == 0)
			{
				MessageBox.Show("No MIDI input devices available");
			}
			else
			{
				if (midiIn == null)
				{
					midiIn = new MidiIn(comboBoxMidiInDevices.SelectedIndex);
					midiIn.MessageReceived += new EventHandler<MidiInMessageEventArgs>(midiIn_MessageReceived);
					midiIn.ErrorReceived += new EventHandler<MidiInMessageEventArgs>(midiIn_ErrorReceived);
				}
				midiIn.Start();
				comboBoxMidiInDevices.Enabled = false;
			}

			if (comboBoxMidiOutDevices.Items.Count == 0)
			{
				MessageBox.Show("No MIDI output devices available");
			}
			else
			{
				if (midiOut == null)
				{
					midiOut = new MidiOut(comboBoxMidiOutDevices.SelectedIndex);
				}
			}

			// Add Audio Output Types
			InitialiseAsioControls();
		}

		private void InitialiseAsioControls()
		{
			var asioDriverNames = AsioOut.GetDriverNames();
			foreach (string driverName in asioDriverNames)
			{
				comboBoxAudioOutDevices.Items.Add(driverName);
			}
			if (comboBoxAudioOutDevices.Items.Count > 0)
			{
				comboBoxAudioOutDevices.SelectedIndex = 0;
			}
		}

		void LoadToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (vstForm != null)
			{
				vstForm.Dispose();
				vstForm = null;

				showToolStripMenuItem.Enabled = false;
				editParametersToolStripMenuItem.Enabled = false;
				loadToolStripMenuItem.Text = "Load...";
			}
			else
			{
				var ofd = new OpenFileDialog();
				ofd.Title = "Select VST:";
				ofd.Filter = "VST Files (*.dll)|*.dll";
				if (LastDirectoryUsed.ContainsKey("VSTDir"))
				{
					ofd.InitialDirectory = LastDirectoryUsed["VSTDir"];
				}
				else
				{
					ofd.InitialDirectory = UtilityAudio.GetVSTDirectory();
				}
				DialogResult res = ofd.ShowDialog();

				if (res != DialogResult.OK || !File.Exists(ofd.FileName)) return;

				try
				{
					if (LastDirectoryUsed.ContainsKey("VSTDir"))
					{
						LastDirectoryUsed["VSTDir"] = Directory.GetParent(ofd.FileName).FullName;
					}
					else
					{
						LastDirectoryUsed.Add("VSTDir", Directory.GetParent(ofd.FileName).FullName);
					}
					vstForm = new VSTForm(ofd.FileName, comboBoxAudioOutDevices.Text);
					vstForm.Show();

					showToolStripMenuItem.Enabled = true;
					editParametersToolStripMenuItem.Enabled = true;

					loadToolStripMenuItem.Text = "Unload...";
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}

		}

		void ShowToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (vstForm != null)
			{
				if (vstForm.Visible) vstForm.BringToFront();
				else vstForm.Visible = true;
			}
		}

		void EditParametersToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (vstForm != null)
				vstForm.ShowEditParameters();
		}


		void SelectMIDIINToolStripMenuItemCheckedChanged(object sender, EventArgs e)
		{
			if (selectMIDIINToolStripMenuItem.Checked)
				comboBoxMidiInDevices.Enabled = true;
			else
			{
				comboBoxMidiInDevices.Enabled = false;
			}

		}

		void SelectMIDIOUTToolStripMenuItemCheckedChanged(object sender, EventArgs e)
		{
			if (selectMIDIOUTToolStripMenuItem.Checked)
				comboBoxMidiOutDevices.Enabled = true;
			else
			{
				comboBoxMidiOutDevices.Enabled = false;
			}
		}

		void SelectAudioOutputDeviceToolStripMenuItemCheckedChanged(object sender, EventArgs e)
		{
			if (selectAudioOutputDeviceToolStripMenuItem.Checked)
				comboBoxAudioOutDevices.Enabled = true;
			else
			{
				comboBoxAudioOutDevices.Enabled = false;
			}
		}

		void TscMIDIINSelectedIndexChanged(object sender, EventArgs e)
		{
		}

		void TscMIDIOUTSelectedIndexChanged(object sender, EventArgs e)
		{
		}

		void TscASIOOutSelectedIndexChanged(object sender, EventArgs e)
		{
		}

		void midiIn_ErrorReceived(object sender, MidiInMessageEventArgs e)
		{
			//progressLog1.LogMessage(Color.Red, String.Format("Time {0} Message 0x{1:X8} Event {2}",
															 //e.Timestamp, e.RawMessage, e.MidiEvent));
		}

		void midiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
		{
			//progressLog1.LogMessage(Color.Blue, String.Format("Time {0} Message 0x{1:X8} Event {2}",
															  //e.Timestamp, e.RawMessage, e.MidiEvent));

			if (VSTForm.vst != null)
			{
				MidiEvent midiEvent = e.MidiEvent;
				byte[] midiData = { 0, 0, 0 };
				if (midiEvent is NoteEvent)
				{
					var me = (NoteEvent)midiEvent;
					midiData = new byte[] {
						(byte) me.CommandCode, 	// Cmd
						(byte) me.NoteNumber,	// Val 1
						(byte) me.Velocity,		// Val 2
					};
				}
				else if (midiEvent is ControlChangeEvent)
				{
					var cce = (ControlChangeEvent)midiEvent;
					midiData = new byte[] {
						0xB0, 						// Cmd
						(byte) cce.Controller,		// Val 1
						(byte) cce.ControllerValue,	// Val 2
					};
				}
				else if (midiEvent is PitchWheelChangeEvent)
				{
					// Pitch Wheel Value 0 is minimum, 0x2000 (8192) is default, 0x4000 (16384) is maximum
					var pe = (PitchWheelChangeEvent)midiEvent;
					midiData = new byte[] {
						0xE0, 							// Cmd
						(byte)(pe.Pitch & 0x7f),		// Val 1
						(byte)((pe.Pitch >> 7) & 0x7f),	// Val 2
					};
				}
				//progressLog1.LogMessage(Color.Chocolate, String.Format("Sending mididata 0x00{0:X2}{1:X2}{2:X2}",
																	  // midiData[2], midiData[1], midiData[0]));
				var vse =
					new VstMidiEvent(/*DeltaFrames*/ 0,
									 /*NoteLength*/ 0,
									 /*NoteOffset*/ 0,
									 midiData,
									 /*Detune*/ 0,
									 /*NoteOffVelocity*/ 0);

				var ve = new VstEvent[1];
				ve[0] = vse;

				VSTForm.vst.pluginContext.PluginCommandStub.ProcessEvents(ve);
			}
		}
		void SoundXyidiEvents()
		{
			int time = 0;
			foreach (var xe in xyidiEvents)
			{
				//progressLog1.LogMessage(Color.Blue, String.Format("Proces XyidiEvent start {0}, duration {1}",
				//xe.StartTime, xe.Duration));
				while (time < xe.StartTime)
				{
					Thread.Sleep(100);
					time += 100;
					//progressLog1.LogMessage(Color.Blue, time.ToString());
				}
				time = 0;

				Thread newThread = new Thread(SoundXyidiEvent);
				//newThread.IsBackground = false;
				newThread.Start(xe);
				newThread.Priority = ThreadPriority.Highest;
				newThread.Join();
			}

			void SoundXyidiEvent(object e)
			{

				if (!(e is XyidiEvent)) { return; }
				Thread newThread2 = new Thread(SetPitchWeel);
				//newThread2.IsBackground = false;new
				newThread2.Priority = ThreadPriority.Highest;

				newThread2.Start(e);
				newThread2.Join();

				var xe = (XyidiEvent)e;
				var curNote = xe.Note;
				int curNoteClosestAnalog = curNote.NoteLogTemper12ClosestAnalog;
				var note12Freq = xe.Note.Note12Freq;

				//progressLog1.LogMessage(Color.Blue, String.Format("Proces MIDI note12 {0}, velociti {1}, duration{2}",
				//curNoteClosestAnalog, xe.Velocity, xe.Duration));
				VSTForm.vst.MIDI_NoteOn((byte)curNote.NoteLogTemper12ClosestAnalog, (byte)xe.Velocity);
				Thread.Sleep((int)xe.Duration);
				VSTForm.vst.MIDI_NoteOn((byte)curNote.NoteLogTemper12ClosestAnalog, 0);
			}
		}

		void ButtonClearLogClick(object sender, EventArgs e)
		{
			using (StreamReader sr = new StreamReader("1"))
			{
				xyidiEvents.Clear();
				int i = Convert.ToInt32(sr.ReadLine());
				for (; i > 0; i--)
				{
					int note = Convert.ToInt32(sr.ReadLine());
					int start = Convert.ToInt32(sr.ReadLine());
					int duration = Convert.ToInt32(sr.ReadLine());
					xyidiEvents.Add(new XyidiEvent(new NoteLogTemper53(note), 100, start, duration));
				}
				sr.Close();
			}
			xyidiEvents.Reverse();
			Thread myThread = new Thread(SoundXyidiEvents);
			//myThread.IsBackground = false;
			myThread.Start();
			
		}
		void SetPitchWeel(object e)

		{
			if (!(e is XyidiEvent)) { return; }
			var xe = (XyidiEvent)e;
			var curNote = xe.Note;
			int curNoteClosestAnalog = curNote.NoteLogTemper12ClosestAnalog;
			var note12Freq = xe.Note.Note12Freq;
			float deltaFreq = (float)note12Freq[curNoteClosestAnalog] - curNote.Frequency;
			int pitch = 8192;
			float delta12NoteFreq = 0;
			if (deltaFreq > 0) // 12Note > 53Note
			{
				delta12NoteFreq = (float)note12Freq[curNoteClosestAnalog] - (float)note12Freq[curNoteClosestAnalog - 1];
				pitch = 8192 - (int)(deltaFreq / (delta12NoteFreq / 4096));
			}
			else if (deltaFreq < 0)
			{
				delta12NoteFreq = (float)note12Freq[curNoteClosestAnalog + 1] - (float)note12Freq[curNoteClosestAnalog];
				pitch = 8192 - (int)(deltaFreq / (delta12NoteFreq / 4096));
			}
			if (pitch > 16384) { pitch = 16384; }
			if (pitch < 0) { pitch = 0; }
			var midiData = new byte[] {
						0xE0, 							// Cmd
						(byte)((int)pitch & 0x7f),		// Val 1
						(byte)(((int)pitch >> 7) & 0x7f),	// Val 2
					};
			var vstMidiEvent = new VstMidiEvent(/*DeltaFrames*/ 0,
							 /*NoteLength*/ 0,
							 /*NoteOffset*/ 0,
							 midiData,
							 /*Detune*/ 0,
							 /*NoteOffVelocity*/ 0);
			var ve = new VstEvent[1];
			ve[0] = vstMidiEvent;
			//progressLog1.LogMessage(Color.Blue, String.Format("Proces MIDI with pitch {0}, and delta pitch{1}, delta wheel{2}",
															 // pitch, deltaFreq, (delta12NoteFreq / 4096) * (pitch - 8192)));
			VSTForm.vst.pluginContext.PluginCommandStub.ProcessEvents(ve);

		}
		void MainFormKeyDown(object sender, KeyEventArgs e)
		{
			// disable anoying beep sound when pressing down key
			e.SuppressKeyPress = true;

			if (isKeyDown)
			{
				return;
			}
			isKeyDown = true;


			
			//SoundXyidiEvents();

			// do what you want to do
			//progressLog1.LogMessage(Color.Blue, String.Format("Key Down {0}, {1}",
															  //e.KeyCode, e.KeyValue));

			//const byte midiVelocity = 100;
			//byte midiNote = KeyEventArgToMidiNote(e);

			//if (VSTForm.vst != null && midiNote != 0) {
			//	VSTForm.vst.MIDI_NoteOn(midiNote, midiVelocity);
			//}
		}

		void MainFormKeyUp(object sender, KeyEventArgs e)
		{
			isKeyDown = false;

			//progressLog1.LogMessage(Color.Blue, String.Format("Key Up {0}, {1}",
															 // e.KeyCode, e.KeyValue));
			/*const byte midiVelocity = 0;
			byte midiNote = KeyEventArgToMidiNote(e);
			
			// only bother with the keys that trigger midi notes
			if (VSTForm.vst != null && midiNote != 0) {
				VSTForm.vst.MIDI_NoteOn(midiNote, midiVelocity);
			}*/
		}


		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (midiIn != null)
			{
				midiIn.Dispose();
				midiIn = null;
			}
			if (midiOut != null)
			{
				midiOut.Dispose();
				midiOut = null;
			}
			if (vstForm != null)
			{
				vstForm.Dispose();
				vstForm = null;
			}
			UtilityAudio.Dispose();
		}
		private void button1_Click(object sender, EventArgs e)
        {
			var wpfwindow = new Piano.PianoRoll();
			ElementHost.EnableModelessKeyboardInterop(wpfwindow);
			wpfwindow.Show();
		}
    }
	}


	
