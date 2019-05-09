using System;
using System.ComponentModel;
using System.Windows;

namespace ToggleSwitch.Utils
{
	// Token: 0x02000004 RID: 4
	public class ActualSizePropertyProxy : FrameworkElement, INotifyPropertyChanged
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600005C RID: 92 RVA: 0x00003168 File Offset: 0x00001368
		// (remove) Token: 0x0600005D RID: 93 RVA: 0x000031A0 File Offset: 0x000013A0
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600005E RID: 94 RVA: 0x000031D5 File Offset: 0x000013D5
		private static void OnElementPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d != null)
			{
				((ActualSizePropertyProxy)d).OnElementChanged(e);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000031E6 File Offset: 0x000013E6
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000031F8 File Offset: 0x000013F8
		public FrameworkElement Element
		{
			get
			{
				return (FrameworkElement)base.GetValue(ActualSizePropertyProxy.ElementProperty);
			}
			set
			{
				base.SetValue(ActualSizePropertyProxy.ElementProperty, value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003206 File Offset: 0x00001406
		public double ActualHeightValue
		{
			get
			{
				if (this.Element != null)
				{
					return this.Element.ActualHeight;
				}
				return 0.0;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003225 File Offset: 0x00001425
		public double ActualWidthValue
		{
			get
			{
				if (this.Element != null)
				{
					return this.Element.ActualWidth;
				}
				return 0.0;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003244 File Offset: 0x00001444
		private void OnElementChanged(DependencyPropertyChangedEventArgs e)
		{
			FrameworkElement frameworkElement = (FrameworkElement)e.OldValue;
			FrameworkElement frameworkElement2 = (FrameworkElement)e.NewValue;
			if (frameworkElement != null)
			{
				frameworkElement.SizeChanged -= this.ElementSizeChanged;
			}
			if (frameworkElement2 != null)
			{
				frameworkElement2.SizeChanged += this.ElementSizeChanged;
			}
			this.NotifyPropertyChanged();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000329B File Offset: 0x0000149B
		private void ElementSizeChanged(object sender, SizeChangedEventArgs e)
		{
			this.NotifyPropertyChanged();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000032A3 File Offset: 0x000014A3
		private void NotifyPropertyChanged()
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs("ActualWidthValue"));
				this.PropertyChanged(this, new PropertyChangedEventArgs("ActualHeightValue"));
			}
		}

		// Token: 0x04000031 RID: 49
		public static readonly DependencyProperty ElementProperty = DependencyProperty.Register("Element", typeof(FrameworkElement), typeof(ActualSizePropertyProxy), new PropertyMetadata(null, new PropertyChangedCallback(ActualSizePropertyProxy.OnElementPropertyChanged)));
	}
}
