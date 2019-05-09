using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ToggleSwitch.Borders
{
	// Token: 0x02000007 RID: 7
	public class InnerGlowBorder : ContentControl
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00003A21 File Offset: 0x00001C21
		public InnerGlowBorder()
		{
			base.DefaultStyleKey = typeof(InnerGlowBorder);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003A39 File Offset: 0x00001C39
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003A4B File Offset: 0x00001C4B
		[Category("Appearance")]
		[Description("Sets whether the content is clipped or not.")]
		public bool ClipContent
		{
			get
			{
				return (bool)base.GetValue(InnerGlowBorder.ClipContentProperty);
			}
			set
			{
				base.SetValue(InnerGlowBorder.ClipContentProperty, value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003A5E File Offset: 0x00001C5E
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003A70 File Offset: 0x00001C70
		[Category("Appearance")]
		[Description("Set 0 for behind the shadow, 1 for in front.")]
		public int ContentZIndex
		{
			get
			{
				return (int)base.GetValue(InnerGlowBorder.ContentZIndexProperty);
			}
			set
			{
				base.SetValue(InnerGlowBorder.ContentZIndexProperty, value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003A83 File Offset: 0x00001C83
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003A95 File Offset: 0x00001C95
		[Category("Appearance")]
		[Description("The inner glow opacity.")]
		public double InnerGlowOpacity
		{
			get
			{
				return (double)base.GetValue(InnerGlowBorder.InnerGlowOpacityProperty);
			}
			set
			{
				base.SetValue(InnerGlowBorder.InnerGlowOpacityProperty, value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003AA8 File Offset: 0x00001CA8
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003ABA File Offset: 0x00001CBA
		[Category("Appearance")]
		[Description("The inner glow color.")]
		public Color InnerGlowColor
		{
			get
			{
				return (Color)base.GetValue(InnerGlowBorder.InnerGlowColorProperty);
			}
			set
			{
				base.SetValue(InnerGlowBorder.InnerGlowColorProperty, value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003ACD File Offset: 0x00001CCD
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003ADF File Offset: 0x00001CDF
		[Category("Appearance")]
		[Description("The inner glow size.")]
		public Thickness InnerGlowSize
		{
			get
			{
				return (Thickness)base.GetValue(InnerGlowBorder.InnerGlowSizeProperty);
			}
			set
			{
				base.SetValue(InnerGlowBorder.InnerGlowSizeProperty, value);
				this.UpdateGlowSize(value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003AF9 File Offset: 0x00001CF9
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003B0B File Offset: 0x00001D0B
		[Category("Appearance")]
		[Description("Sets the corner radius on the border.")]
		public CornerRadius CornerRadius
		{
			get
			{
				return (CornerRadius)base.GetValue(InnerGlowBorder.CornerRadiusProperty);
			}
			set
			{
				base.SetValue(InnerGlowBorder.CornerRadiusProperty, value);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003B20 File Offset: 0x00001D20
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this._leftGlow = (base.GetTemplateChild("PART_LeftGlow") as Border);
			this._topGlow = (base.GetTemplateChild("PART_TopGlow") as Border);
			this._rightGlow = (base.GetTemplateChild("PART_RightGlow") as Border);
			this._bottomGlow = (base.GetTemplateChild("PART_BottomGlow") as Border);
			this._leftGlowStop0 = (base.GetTemplateChild("PART_LeftGlowStop0") as GradientStop);
			this._leftGlowStop1 = (base.GetTemplateChild("PART_LeftGlowStop1") as GradientStop);
			this._topGlowStop0 = (base.GetTemplateChild("PART_TopGlowStop0") as GradientStop);
			this._topGlowStop1 = (base.GetTemplateChild("PART_TopGlowStop1") as GradientStop);
			this._rightGlowStop0 = (base.GetTemplateChild("PART_RightGlowStop0") as GradientStop);
			this._rightGlowStop1 = (base.GetTemplateChild("PART_RightGlowStop1") as GradientStop);
			this._bottomGlowStop0 = (base.GetTemplateChild("PART_BottomGlowStop0") as GradientStop);
			this._bottomGlowStop1 = (base.GetTemplateChild("PART_BottomGlowStop1") as GradientStop);
			this.UpdateGlowColor(this.InnerGlowColor);
			this.UpdateGlowSize(this.InnerGlowSize);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003C54 File Offset: 0x00001E54
		internal void UpdateGlowColor(Color color)
		{
			if (this._leftGlowStop0 != null)
			{
				this._leftGlowStop0.Color = color;
			}
			if (this._leftGlowStop1 != null)
			{
				this._leftGlowStop1.Color = Color.FromArgb(0, color.R, color.G, color.B);
			}
			if (this._topGlowStop0 != null)
			{
				this._topGlowStop0.Color = color;
			}
			if (this._topGlowStop1 != null)
			{
				this._topGlowStop1.Color = Color.FromArgb(0, color.R, color.G, color.B);
			}
			if (this._rightGlowStop0 != null)
			{
				this._rightGlowStop0.Color = color;
			}
			if (this._rightGlowStop1 != null)
			{
				this._rightGlowStop1.Color = Color.FromArgb(0, color.R, color.G, color.B);
			}
			if (this._bottomGlowStop0 != null)
			{
				this._bottomGlowStop0.Color = color;
			}
			if (this._bottomGlowStop1 != null)
			{
				this._bottomGlowStop1.Color = Color.FromArgb(0, color.R, color.G, color.B);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003D6C File Offset: 0x00001F6C
		internal void UpdateGlowSize(Thickness newGlowSize)
		{
			if (this._leftGlow != null)
			{
				this._leftGlow.Width = Math.Abs(newGlowSize.Left);
			}
			if (this._topGlow != null)
			{
				this._topGlow.Height = Math.Abs(newGlowSize.Top);
			}
			if (this._rightGlow != null)
			{
				this._rightGlow.Width = Math.Abs(newGlowSize.Right);
			}
			if (this._bottomGlow != null)
			{
				this._bottomGlow.Height = Math.Abs(newGlowSize.Bottom);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003DF5 File Offset: 0x00001FF5
		private static void InnerGlowColorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			if (eventArgs.NewValue != null)
			{
				((InnerGlowBorder)dependencyObject).UpdateGlowColor((Color)eventArgs.NewValue);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003E17 File Offset: 0x00002017
		private static void InnerGlowSizeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			((InnerGlowBorder)dependencyObject).UpdateGlowSize((Thickness)eventArgs.NewValue);
		}

		// Token: 0x0400003D RID: 61
		public static readonly DependencyProperty InnerGlowOpacityProperty = DependencyProperty.Register("InnerGlowOpacity", typeof(double), typeof(InnerGlowBorder), null);

		// Token: 0x0400003E RID: 62
		public static readonly DependencyProperty InnerGlowSizeProperty = DependencyProperty.Register("InnerGlowSize", typeof(Thickness), typeof(InnerGlowBorder), new PropertyMetadata(new PropertyChangedCallback(InnerGlowBorder.InnerGlowSizeChanged)));

		// Token: 0x0400003F RID: 63
		public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(InnerGlowBorder), null);

		// Token: 0x04000040 RID: 64
		public static readonly DependencyProperty InnerGlowColorProperty = DependencyProperty.Register("InnerGlowColor", typeof(Color), typeof(InnerGlowBorder), new PropertyMetadata(Colors.Black, new PropertyChangedCallback(InnerGlowBorder.InnerGlowColorChanged)));

		// Token: 0x04000041 RID: 65
		public static readonly DependencyProperty ClipContentProperty = DependencyProperty.Register("ClipContent", typeof(bool), typeof(InnerGlowBorder), null);

		// Token: 0x04000042 RID: 66
		public static readonly DependencyProperty ContentZIndexProperty = DependencyProperty.Register("ContentZIndex", typeof(int), typeof(InnerGlowBorder), null);

		// Token: 0x04000043 RID: 67
		private Border _bottomGlow;

		// Token: 0x04000044 RID: 68
		private GradientStop _bottomGlowStop0;

		// Token: 0x04000045 RID: 69
		private GradientStop _bottomGlowStop1;

		// Token: 0x04000046 RID: 70
		private Border _leftGlow;

		// Token: 0x04000047 RID: 71
		private GradientStop _leftGlowStop0;

		// Token: 0x04000048 RID: 72
		private GradientStop _leftGlowStop1;

		// Token: 0x04000049 RID: 73
		private Border _rightGlow;

		// Token: 0x0400004A RID: 74
		private GradientStop _rightGlowStop0;

		// Token: 0x0400004B RID: 75
		private GradientStop _rightGlowStop1;

		// Token: 0x0400004C RID: 76
		private Border _topGlow;

		// Token: 0x0400004D RID: 77
		private GradientStop _topGlowStop0;

		// Token: 0x0400004E RID: 78
		private GradientStop _topGlowStop1;
	}
}
