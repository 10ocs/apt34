using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace newPanel
{
	// Token: 0x02000004 RID: 4
	public partial class ResultWindow : Window
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002F51 File Offset: 0x00001151
		public ResultWindow()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002F64 File Offset: 0x00001164
		public ResultWindow(string address)
		{
			this.InitializeComponent();
			bool flag = File.Exists(address);
			if (flag)
			{
				this.result_view.Text = File.ReadAllText(address);
			}
		}
	}
}
