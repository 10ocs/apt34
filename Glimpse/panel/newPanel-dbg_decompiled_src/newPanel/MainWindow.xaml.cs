using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;

namespace newPanel
{
	// Token: 0x02000009 RID: 9
	public partial class MainWindow : Window, IStyleConnector
	{
		// Token: 0x06000043 RID: 67 RVA: 0x0000358C File Offset: 0x0000178C
		public MainWindow()
		{
			try
			{
				this.InitializeComponent();
				this.loadDnsAgents();
				this.dt = new DispatcherTimer();
				this.dt.Tick += this.dt_tick;
				this.dt.Interval = new TimeSpan(0, 0, 10);
				this.dt.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000362C File Offset: 0x0000182C
		private void dt_tick(object sender, EventArgs e)
		{
			this.loadDnsAgents();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003638 File Offset: 0x00001838
		public string Timer(string last_file_address)
		{
			string text = Utility.file_reader(last_file_address);
			bool flag = text != "-1";
			string result;
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
					text2 = "NOW";
				}
				result = text2;
			}
			else
			{
				result = "-1";
			}
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000362C File Offset: 0x0000182C
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.loadDnsAgents();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003749 File Offset: 0x00001949
		public void OnWindowClosing(object sender, CancelEventArgs e)
		{
			this.dt.Stop();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003758 File Offset: 0x00001958
		public void loadDnsAgents()
		{
			this.dnsAgents.Clear();
			Utility.create_path(Settings.dns_agent_path);
			string[] directories = Directory.GetDirectories(Settings.dns_agent_path);
			int num = 0;
			foreach (string text in directories)
			{
				num++;
				agent agent = new agent();
				agent.count = num;
				agent.id = text.Remove(0, Settings.dns_agent_path.Length + 1);
				agent.ip = Utility.file_reader(text + "\\ip");
				agent.lastActivity = this.Timer(Settings.dns_agent_path + "\\" + agent.id + "\\last");
				agent.comment = Utility.file_reader(Settings.dns_agent_path + "\\" + agent.id + "\\comment");
				bool flag = agent.comment == "-1";
				if (flag)
				{
					agent.comment = "new agent";
				}
				this.dnsAgents.Add(agent);
			}
			this.agetnsGrid.ItemsSource = null;
			this.agetnsGrid.ItemsSource = this.dnsAgents;
			this.agent_counts.Content = this.dnsAgents.Count;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000038B0 File Offset: 0x00001AB0
		private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
		{
			try
			{
				agent agent = (agent)this.agetnsGrid.SelectedItem;
				controlPanel controlPanel = new controlPanel(agent.id, agent.ip, agent.lastActivity);
				controlPanel.Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003914 File Offset: 0x00001B14
		private void delete_this_bot(object sender, RoutedEventArgs e)
		{
			try
			{
				agent agent = (agent)this.agetnsGrid.SelectedItem;
				bool flag = Utility.copy_entire_path(Settings.dns_agent_path + "\\" + agent.id, Settings.app_root_path + "\\recycle_bin\\" + agent.id);
				if (flag)
				{
					Utility.delete_entire_path(Settings.dns_agent_path + "\\" + agent.id);
				}
				this.loadDnsAgents();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000039B0 File Offset: 0x00001BB0
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Process.Start(Settings.app_root_path);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000039C0 File Offset: 0x00001BC0
		private void OnKeyDownHandlerComment(object sender, KeyEventArgs e)
		{
			bool flag = e.Key == Key.Return;
			if (flag)
			{
				try
				{
					agent agent = (agent)this.agetnsGrid.SelectedItem;
					string text = ((TextBox)sender).Text;
					Utility.write_file(text, Settings.dns_agent_path + "\\" + agent.id + "\\comment");
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003B24 File Offset: 0x00001D24
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 4:
				((TextBox)target).KeyDown += this.OnKeyDownHandlerComment;
				break;
			case 5:
				((Button)target).Click += this.delete_this_bot;
				break;
			case 6:
			{
				EventSetter eventSetter = new EventSetter();
				eventSetter.Event = Control.MouseDoubleClickEvent;
				eventSetter.Handler = new MouseButtonEventHandler(this.Row_DoubleClick);
				((Style)target).Setters.Add(eventSetter);
				break;
			}
			}
		}

		// Token: 0x04000022 RID: 34
		private DispatcherTimer dt = null;

		// Token: 0x04000023 RID: 35
		private List<agent> dnsAgents = new List<agent>();
	}
}
