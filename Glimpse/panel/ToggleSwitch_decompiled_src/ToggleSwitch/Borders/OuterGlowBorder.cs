using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ToggleSwitch.Borders
{
	// Token: 0x02000008 RID: 8
	public class OuterGlowBorder : ContentControl
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00003F3F File Offset: 0x0000213F
		public OuterGlowBorder()
		{
			base.DefaultStyleKey = typeof(OuterGlowBorder);
			base.SizeChanged += this.OuterGlowContentControlSizeChanged;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003F69 File Offset: 0x00002169
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003F7B File Offset: 0x0000217B
		[Category("Appearance")]
		[Description("Sets whether the content is clipped or not.")]
		public bool ClipContent
		{
			get
			{
				return (bool)base.GetValue(OuterGlowBorder.ClipContentProperty);
			}
			set
			{
				base.SetValue(OuterGlowBorder.ClipContentProperty, value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003F8E File Offset: 0x0000218E
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003FA0 File Offset: 0x000021A0
		[Category("Appearance")]
		[Description("The outer glow opacity.")]
		public double OuterGlowOpacity
		{
			get
			{
				return (double)base.GetValue(OuterGlowBorder.OuterGlowOpacityProperty);
			}
			set
			{
				base.SetValue(OuterGlowBorder.OuterGlowOpacityProperty, value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003FB3 File Offset: 0x000021B3
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003FC5 File Offset: 0x000021C5
		[Category("Appearance")]
		[Description("The outer glow size.")]
		public double OuterGlowSize
		{
			get
			{
				return (double)base.GetValue(OuterGlowBorder.OuterGlowSizeProperty);
			}
			set
			{
				base.SetValue(OuterGlowBorder.OuterGlowSizeProperty, value);
				this.UpdateGlowSize(this.OuterGlowSize);
				this.UpdateStops(new Size(base.ActualWidth, base.ActualHeight));
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003FFB File Offset: 0x000021FB
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000400D File Offset: 0x0000220D
		[Category("Appearance")]
		[Description("The outer glow color.")]
		public Color OuterGlowColor
		{
			get
			{
				return (Color)base.GetValue(OuterGlowBorder.OuterGlowColorProperty);
			}
			set
			{
				base.SetValue(OuterGlowBorder.OuterGlowColorProperty, value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004020 File Offset: 0x00002220
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00004034 File Offset: 0x00002234
		[Category("Appearance")]
		[Description("Sets the corner radius on the border.")]
		public CornerRadius CornerRadius
		{
			get
			{
				return (CornerRadius)base.GetValue(OuterGlowBorder.CornerRadiusProperty);
			}
			set
			{
				base.SetValue(OuterGlowBorder.CornerRadiusProperty, value);
				this.ShadowCornerRadius = new CornerRadius(Math.Abs(value.TopLeft * 1.5), Math.Abs(value.TopRight * 1.5), Math.Abs(value.BottomRight * 1.5), Math.Abs(value.BottomLeft * 1.5));
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000040B5 File Offset: 0x000022B5
		// (set) Token: 0x06000096 RID: 150 RVA: 0x000040C7 File Offset: 0x000022C7
		[Category("Appearance")]
		[Description("Sets the corner radius on the border.")]
		public CornerRadius ShadowCornerRadius
		{
			get
			{
				return (CornerRadius)base.GetValue(OuterGlowBorder.ShadowCornerRadiusProperty);
			}
			set
			{
				base.SetValue(OuterGlowBorder.ShadowCornerRadiusProperty, value);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000040DC File Offset: 0x000022DC
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this._shadowOuterStop1 = (GradientStop)base.GetTemplateChild("PART_ShadowOuterStop1");
			this._shadowOuterStop2 = (GradientStop)base.GetTemplateChild("PART_ShadowOuterStop2");
			this._shadowVertical1 = (GradientStop)base.GetTemplateChild("PART_ShadowVertical1");
			this._shadowVertical2 = (GradientStop)base.GetTemplateChild("PART_ShadowVertical2");
			this._shadowHorizontal1 = (GradientStop)base.GetTemplateChild("PART_ShadowHorizontal1");
			this._shadowHorizontal2 = (GradientStop)base.GetTemplateChild("PART_ShadowHorizontal2");
			this._outerGlowBorder = (Border)base.GetTemplateChild("PART_OuterGlowBorder");
			this.UpdateGlowSize(this.OuterGlowSize);
			this.UpdateGlowColor(this.OuterGlowColor);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000041A1 File Offset: 0x000023A1
		internal void UpdateGlowSize(double size)
		{
			if (this._outerGlowBorder != null)
			{
				this._outerGlowBorder.Margin = new Thickness(-Math.Abs(size));
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000041C4 File Offset: 0x000023C4
		internal void UpdateGlowColor(Color color)
		{
			if (this._shadowVertical1 != null)
			{
				this._shadowVertical1.Color = color;
			}
			if (this._shadowVertical2 != null)
			{
				this._shadowVertical2.Color = color;
			}
			if (this._shadowOuterStop1 != null)
			{
				this._shadowOuterStop1.Color = Color.FromArgb(0, color.R, color.G, color.B);
			}
			if (this._shadowOuterStop2 != null)
			{
				this._shadowOuterStop2.Color = Color.FromArgb(0, color.R, color.G, color.B);
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004255 File Offset: 0x00002455
		private static void OuterGlowColorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			if (eventArgs.NewValue != null)
			{
				((OuterGlowBorder)dependencyObject).UpdateGlowColor((Color)eventArgs.NewValue);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004277 File Offset: 0x00002477
		private void OuterGlowContentControlSizeChanged(object sender, SizeChangedEventArgs e)
		{
			this.UpdateStops(e.NewSize);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004288 File Offset: 0x00002488
		private void UpdateStops(Size size)
		{
			if (size.Width > 0.0 && size.Height > 0.0)
			{
				if (this._shadowHorizontal1 != null)
				{
					this._shadowHorizontal1.Offset = Math.Abs(this.OuterGlowSize) / (size.Width + Math.Abs(this.OuterGlowSize) + Math.Abs(this.OuterGlowSize));
				}
				if (this._shadowHorizontal2 != null)
				{
					this._shadowHorizontal2.Offset = 1.0 - Math.Abs(this.OuterGlowSize) / (size.Width + Math.Abs(this.OuterGlowSize) + Math.Abs(this.OuterGlowSize));
				}
				if (this._shadowVertical1 != null)
				{
					this._shadowVertical1.Offset = Math.Abs(this.OuterGlowSize) / (size.Height + Math.Abs(this.OuterGlowSize) + Math.Abs(this.OuterGlowSize));
				}
				if (this._shadowVertical2 != null)
				{
					this._shadowVertical2.Offset = 1.0 - Math.Abs(this.OuterGlowSize) / (size.Height + Math.Abs(this.OuterGlowSize) + Math.Abs(this.OuterGlowSize));
				}
			}
		}

		// Token: 0x0400004F RID: 79
		public static readonly DependencyProperty OuterGlowOpacityProperty = DependencyProperty.Register("OuterGlowOpacity", typeof(double), typeof(OuterGlowBorder), null);

		// Token: 0x04000050 RID: 80
		public static readonly DependencyProperty OuterGlowSizeProperty = DependencyProperty.Register("OuterGlowSize", typeof(double), typeof(OuterGlowBorder), null);

		// Token: 0x04000051 RID: 81
		public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(OuterGlowBorder), null);

		// Token: 0x04000052 RID: 82
		public static readonly DependencyProperty ShadowCornerRadiusProperty = DependencyProperty.Register("ShadowCornerRadius", typeof(CornerRadius), typeof(OuterGlowBorder), null);

		// Token: 0x04000053 RID: 83
		public static readonly DependencyProperty OuterGlowColorProperty = DependencyProperty.Register("OuterGlowColor", typeof(Color), typeof(OuterGlowBorder), new PropertyMetadata(Colors.Black, new PropertyChangedCallback(OuterGlowBorder.OuterGlowColorChanged)));

		// Token: 0x04000054 RID: 84
		public static readonly DependencyProperty ClipContentProperty = DependencyProperty.Register("ClipContent", typeof(bool), typeof(OuterGlowBorder), null);

		// Token: 0x04000055 RID: 85
		private Border _outerGlowBorder;

		// Token: 0x04000056 RID: 86
		private GradientStop _shadowHorizontal1;

		// Token: 0x04000057 RID: 87
		private GradientStop _shadowHorizontal2;

		// Token: 0x04000058 RID: 88
		private GradientStop _shadowOuterStop1;

		// Token: 0x04000059 RID: 89
		private GradientStop _shadowOuterStop2;

		// Token: 0x0400005A RID: 90
		private GradientStop _shadowVertical1;

		// Token: 0x0400005B RID: 91
		private GradientStop _shadowVertical2;
	}
}
