using Advanced_Combat_Tracker;
using MethodReplacer;
using NAudio.Wave;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

[assembly: AssemblyTitle("NuSound")]
[assembly: AssemblyDescription("Uses NAudio's DirectSoundOut for playing sounds, and provides more TTS options.")]
[assembly: AssemblyCompany("Lavish of Unrest")]
[assembly: AssemblyVersion("1.0.0.1")]

namespace ACT_Plugin
{
	public class NuSound : UserControl, IActPluginV1
	{
		#region Designer Created Code (Avoid editing)
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            this.comboBoxDevice = new System.Windows.Forms.ComboBox();
            this.directSoundDeviceInfoBindingSource = new ACT_Plugin.SafeBindingSource(this.components);
            this.comboBoxVoice = new System.Windows.Forms.ComboBox();
            this.voiceInfoBindingSource = new ACT_Plugin.SafeBindingSource(this.components);
            this.comboBoxTTSAPI = new System.Windows.Forms.ComboBox();
            this.synthFactoryBindingSource = new ACT_Plugin.SafeBindingSource(this.components);
            this.trackBarVoiceRate = new System.Windows.Forms.TrackBar();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.directSoundDeviceInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.voiceInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.synthFactoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVoiceRate)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label1.Location = new System.Drawing.Point(82, 26);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(82, 21);
            label1.TabIndex = 1;
            label1.Text = "Output Device:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(151, 186);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(452, 13);
            label2.TabIndex = 2;
            label2.Text = "\"All life begins with Nu and ends with Nu. This is the truth! This is my belief! " +
    "...At least for now.\"";
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label3.Location = new System.Drawing.Point(85, 54);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(79, 21);
            label3.TabIndex = 3;
            label3.Text = "TTS Voice:";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxDevice
            // 
            this.comboBoxDevice.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxDevice.DataSource = this.directSoundDeviceInfoBindingSource;
            this.comboBoxDevice.DisplayMember = "Description";
            this.comboBoxDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDevice.FormattingEnabled = true;
            this.comboBoxDevice.Location = new System.Drawing.Point(170, 27);
            this.comboBoxDevice.MaxDropDownItems = 50;
            this.comboBoxDevice.Name = "comboBoxDevice";
            this.comboBoxDevice.Size = new System.Drawing.Size(477, 21);
            this.comboBoxDevice.TabIndex = 0;
            this.comboBoxDevice.ValueMember = "Guid";
            this.comboBoxDevice.SelectedValueChanged += new System.EventHandler(this.comboBoxDevice_SelectedValueChanged);
            // 
            // directSoundDeviceInfoBindingSource
            // 
            this.directSoundDeviceInfoBindingSource.AllowNew = false;
            this.directSoundDeviceInfoBindingSource.DataSource = typeof(NAudio.Wave.DirectSoundDeviceInfo);
            this.directSoundDeviceInfoBindingSource.UIThreadMarshal = this;
            // 
            // comboBoxVoice
            // 
            this.comboBoxVoice.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxVoice.DataSource = this.voiceInfoBindingSource;
            this.comboBoxVoice.DisplayMember = "Description";
            this.comboBoxVoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVoice.FormattingEnabled = true;
            this.comboBoxVoice.Location = new System.Drawing.Point(264, 54);
            this.comboBoxVoice.Name = "comboBoxVoice";
            this.comboBoxVoice.Size = new System.Drawing.Size(383, 21);
            this.comboBoxVoice.TabIndex = 4;
            this.comboBoxVoice.ValueMember = "Name";
            this.comboBoxVoice.SelectedValueChanged += new System.EventHandler(this.comboBoxVoice_SelectedValueChanged);
            // 
            // voiceInfoBindingSource
            // 
            this.voiceInfoBindingSource.DataSource = typeof(ACT_Plugin.VoiceInfo);
            this.voiceInfoBindingSource.UIThreadMarshal = this;
            // 
            // comboBoxTTSAPI
            // 
            this.comboBoxTTSAPI.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxTTSAPI.DataSource = this.synthFactoryBindingSource;
            this.comboBoxTTSAPI.DisplayMember = "Name";
            this.comboBoxTTSAPI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTTSAPI.FormattingEnabled = true;
            this.comboBoxTTSAPI.Location = new System.Drawing.Point(170, 54);
            this.comboBoxTTSAPI.Name = "comboBoxTTSAPI";
            this.comboBoxTTSAPI.Size = new System.Drawing.Size(88, 21);
            this.comboBoxTTSAPI.TabIndex = 5;
            this.comboBoxTTSAPI.ValueMember = "Name";
            this.comboBoxTTSAPI.SelectedValueChanged += new System.EventHandler(this.comboBoxTTSAPI_SelectedValueChanged);
            // 
            // synthFactoryBindingSource
            // 
            this.synthFactoryBindingSource.DataSource = typeof(ACT_Plugin.ISpeechSynthesizerFactory);
            this.synthFactoryBindingSource.UIThreadMarshal = this;
            // 
            // trackBarVoiceRate
            // 
            this.trackBarVoiceRate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.trackBarVoiceRate.Location = new System.Drawing.Point(170, 81);
            this.trackBarVoiceRate.Minimum = -10;
            this.trackBarVoiceRate.Name = "trackBarVoiceRate";
            this.trackBarVoiceRate.Size = new System.Drawing.Size(477, 45);
            this.trackBarVoiceRate.TabIndex = 6;
            this.trackBarVoiceRate.TickFrequency = 2;
            this.trackBarVoiceRate.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarVoiceRate.ValueChanged += new System.EventHandler(this.trackBarVoiceRate_ValueChanged);
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label4.Location = new System.Drawing.Point(85, 81);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(79, 45);
            label4.TabIndex = 7;
            label4.Text = "TTS Rate:";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NuSound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(label4);
            this.Controls.Add(this.trackBarVoiceRate);
            this.Controls.Add(this.comboBoxTTSAPI);
            this.Controls.Add(this.comboBoxVoice);
            this.Controls.Add(label3);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.comboBoxDevice);
            this.Name = "NuSound";
            this.Size = new System.Drawing.Size(760, 199);
            ((System.ComponentModel.ISupportInitialize)(this.directSoundDeviceInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.voiceInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.synthFactoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVoiceRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private ComboBox comboBoxDevice;
        private ComboBox comboBoxTTSAPI;
        private ComboBox comboBoxVoice;
        private SafeBindingSource directSoundDeviceInfoBindingSource;
        private SafeBindingSource synthFactoryBindingSource;
        private SafeBindingSource voiceInfoBindingSource;
        private TrackBar trackBarVoiceRate;

        public NuSound()
		{
			InitializeComponent();
		}

        #endregion

        Label lblStatus;	// The status label that appears in ACT's Plugin tab
		string settingsFile;
		SettingsSerializer xmlSettings;
        FormActMain.PlaySoundDelegate oldPlaySoundMethod;

        byte[] originalTTS;
        IntPtr ACT_TTSMethod;
        static TrackBar tbarTtsVol;
        private class FormActMain_newTTS : FormActMain
        {
            public FormActMain_newTTS(string[] args) : base(args) {
            }

            public void newTTS(string text)
            {
                string fixtext = text.Replace("/", " ");
                Int32 ttsVol = 100;
                if (tbarTtsVol != null)
                    ttsVol = tbarTtsVol.Value;
                AudioPlayer.PlayTTS(text, ttsVol);
            }
        }

        void PlaySoundAudioPlayer(string file, Int32 volumePercent)
        {
            AudioPlayer.PlayWaveFile(file, volumePercent);
        }

        #region IActPluginV1 Members
		public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            lblStatus = pluginStatusText;	// Hand the status label's reference to our local var
            pluginScreenSpace.Controls.Add(this);	// Add this UserControl to the tab ACT provides
            this.Dock = DockStyle.Fill;	// Expand the UserControl to fill the tab's client space

            string actPath = Assembly.GetEntryAssembly().Location;
            string actFile = Path.GetFileNameWithoutExtension(actPath);
            settingsFile = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "Config\\NuSound." + actFile + ".config.xml");

            try
            {
                tbarTtsVol = null;
                FieldInfo opSound_field = ActGlobals.oFormActMain.GetType().GetField("opSound", BindingFlags.NonPublic | BindingFlags.Instance);
                Object opSound = opSound_field.GetValue(ActGlobals.oFormActMain);
                FieldInfo opSound_tbarTtsVol_field = opSound.GetType().GetField("tbarTtsVol", BindingFlags.NonPublic | BindingFlags.Instance);
                tbarTtsVol = (TrackBar)opSound_tbarTtsVol_field.GetValue(opSound);
            }
            catch (NullReferenceException) { }
            catch (InvalidCastException) { }

            xmlSettings = new SettingsSerializer(this);	// Create a new settings serializer and pass it this instance
            LoadSettings();

            // reduce delay for first spoken text
            TTSProvider.SpeakAsync("", 0);

            // Create some sort of parsing event handler.  After the "+=" hit TAB twice and the code will be generated for you.
            IntPtr new_TTSMethod = Replacer.GetFunctionPointer(typeof(FormActMain_newTTS).GetMethod("newTTS", BindingFlags.Instance | BindingFlags.Public).MethodHandle);
            ACT_TTSMethod = Replacer.GetFunctionPointer(typeof(FormActMain).GetMethod("TTS", BindingFlags.Instance | BindingFlags.Public).MethodHandle);
            Replacer.InsertJumpToFunction(ACT_TTSMethod, new_TTSMethod, out originalTTS);

            oldPlaySoundMethod = ActGlobals.oFormActMain.PlaySoundMethod;
            ActGlobals.oFormActMain.PlaySoundMethod = new FormActMain.PlaySoundDelegate(PlaySoundAudioPlayer);

            lblStatus.Text = "Plugin Started";
        }

		public void DeInitPlugin()
		{
			// Unsubscribe from any events you listen to when exiting!
            ActGlobals.oFormActMain.PlaySoundMethod = oldPlaySoundMethod;
            Replacer.RestoreFunction(ACT_TTSMethod, originalTTS);
            tbarTtsVol = null;

			SaveSettings();
			lblStatus.Text = "Plugin Exited";
		}
		#endregion

        #region IActPluginV1 Settings

        void PopulateTTSVoices()
        {
            comboBoxVoice.BeginUpdate();
            voiceInfoBindingSource.Clear();
            bool inList = false;
            foreach (var info in TTSProvider.TTSFactory.GetVoices())
            {
                if (info.Name.Equals(TTSProvider.VoiceIdentifier))
                    inList = true;
                voiceInfoBindingSource.Add(info);
            }
            if (!inList)
                TTSProvider.VoiceIdentifier = (String)comboBoxVoice.SelectedValue;
            comboBoxVoice.EndUpdate();
        }

        void LoadSettings()
        {
            directSoundDeviceInfoBindingSource.Clear();
            {
                DirectSoundDeviceInfo info = new DirectSoundDeviceInfo();
                info.Description = "(Default Playback Device)";
                info.Guid = DirectSoundOut.DSDEVID_DefaultPlayback;
                directSoundDeviceInfoBindingSource.Add(info);
            }
            foreach (var info in DirectSoundOut.Devices)
                directSoundDeviceInfoBindingSource.Add(info);

            synthFactoryBindingSource.Clear();
            foreach (var info in TTSProvider.TTSFactories)
                synthFactoryBindingSource.Add(info);

            PopulateTTSVoices();

            xmlSettings.AddControlSetting(comboBoxDevice.Name, comboBoxDevice);
            xmlSettings.AddControlSetting(comboBoxTTSAPI.Name, comboBoxTTSAPI);
            xmlSettings.AddControlSetting(comboBoxVoice.Name, comboBoxVoice);
            xmlSettings.AddControlSetting(trackBarVoiceRate.Name, trackBarVoiceRate);

            if (File.Exists(settingsFile))
            {
                FileStream fs = new FileStream(settingsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlTextReader xReader = new XmlTextReader(fs);

                try
                {
                    while (xReader.Read())
                    {
                        if (xReader.NodeType == XmlNodeType.Element)
                        {
                            if (xReader.LocalName == "SettingsSerializer")
                            {
                                xmlSettings.ImportFromXml(xReader);
                                xmlSettings.FinializeComboBoxes();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Error loading settings: " + ex.Message;
                }
                xReader.Close();
            }
        }

        void SaveSettings()
		{
			FileStream fs = new FileStream(settingsFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
			XmlTextWriter xWriter = new XmlTextWriter(fs, Encoding.UTF8);
			xWriter.Formatting = Formatting.Indented;
			xWriter.Indentation = 1;
			xWriter.IndentChar = '\t';
			xWriter.WriteStartDocument(true);
			xWriter.WriteStartElement("Config");	// <Config>
			xWriter.WriteStartElement("SettingsSerializer");	// <Config><SettingsSerializer>
			xmlSettings.ExportToXml(xWriter);	// Fill the SettingsSerializer XML
			xWriter.WriteEndElement();	// </SettingsSerializer>
			xWriter.WriteEndElement();	// </Config>
			xWriter.WriteEndDocument();	// Tie up loose ends (shouldn't be any)
			xWriter.Flush();	// Flush the file buffer to disk
			xWriter.Close();
		}

        private void comboBoxDevice_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxDevice.SelectedValue != null)
                AudioPlayer.DeviceIdentifier = (Guid)comboBoxDevice.SelectedValue;
        }

        private void comboBoxTTSAPI_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxTTSAPI.SelectedItem != null)
            {
                TTSProvider.TTSFactory = (ISpeechSynthesizerFactory)comboBoxTTSAPI.SelectedItem;
                PopulateTTSVoices();
            }
        }

        private void comboBoxVoice_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxVoice.SelectedValue != null)
                TTSProvider.VoiceIdentifier = (String)comboBoxVoice.SelectedValue;
        }

        private void trackBarVoiceRate_ValueChanged(object sender, EventArgs e)
        {
            TTSProvider.VoiceRate = trackBarVoiceRate.Value;
        }
        #endregion
    }
}
