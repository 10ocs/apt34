using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ToggleSwitch.Borders
{
	// Token: 0x02000006 RID: 6
	public class ClippingBorder : ContentControl
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00003341 File Offset: 0x00001541
		public ClippingBorder()
		{
			base.DefaultStyleKey = typeof(ClippingBorder);
			base.SizeChanged += this.ClippingBorderSizeChanged;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000336B File Offset: 0x0000156B
		// (set) Token: 0x0600006B RID: 107 RVA: 0x0000337D File Offset: 0x0000157D
		[Category("Appearance")]
		[Description("Sets the corner radius on the border.")]
		public CornerRadius CornerRadius
		{
			get
			{
				return (CornerRadius)base.GetValue(ClippingBorder.CornerRadiusProperty);
			}
			set
			{
				base.SetValue(ClippingBorder.CornerRadiusProperty, value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003390 File Offset: 0x00001590
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000033A2 File Offset: 0x000015A2
		[Category("Appearance")]
		[Description("Sets whether the content is clipped or not.")]
		public bool ClipContent
		{
			get
			{
				return (bool)base.GetValue(ClippingBorder.ClipContentProperty);
			}
			set
			{
				base.SetValue(ClippingBorder.ClipContentProperty, value);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000033B8 File Offset: 0x000015B8
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this._border = (base.GetTemplateChild("PART_Border") as Border);
			this._topLeftContentControl = (base.GetTemplateChild("PART_TopLeftContentControl") as ContentControl);
			this._topRightContentControl = (base.GetTemplateChild("PART_TopRightContentControl") as ContentControl);
			this._bottomRightContentControl = (base.GetTemplateChild("PART_BottomRightContentControl") as ContentControl);
			this._bottomLeftContentControl = (base.GetTemplateChild("PART_BottomLeftContentControl") as ContentControl);
			if (this._topLeftContentControl != null)
			{
				this._topLeftContentControl.SizeChanged += this.ContentControlSizeChanged;
			}
			this._topLeftClip = (base.GetTemplateChild("PART_TopLeftClip") as RectangleGeometry);
			this._topRightClip = (base.GetTemplateChild("PART_TopRightClip") as RectangleGeometry);
			this._bottomRightClip = (base.GetTemplateChild("PART_BottomRightClip") as RectangleGeometry);
			this._bottomLeftClip = (base.GetTemplateChild("PART_BottomLeftClip") as RectangleGeometry);
			this.UpdateClipContent(this.ClipContent);
			this.UpdateCornerRadius(this.CornerRadius);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000034C8 File Offset: 0x000016C8
		internal void UpdateCornerRadius(CornerRadius newCornerRadius)
		{
			if (this._border != null)
			{
				this._border.CornerRadius = newCornerRadius;
			}
			if (this._topLeftClip != null)
			{
				this._topLeftClip.RadiusX = (this._topLeftClip.RadiusY = newCornerRadius.TopLeft - Math.Min(base.BorderThickness.Left, base.BorderThickness.Top) / 2.0);
			}
			if (this._topRightClip != null)
			{
				this._topRightClip.RadiusX = (this._topRightClip.RadiusY = newCornerRadius.TopRight - Math.Min(base.BorderThickness.Top, base.BorderThickness.Right) / 2.0);
			}
			if (this._bottomRightClip != null)
			{
				this._bottomRightClip.RadiusX = (this._bottomRightClip.RadiusY = newCornerRadius.BottomRight - Math.Min(base.BorderThickness.Right, base.BorderThickness.Bottom) / 2.0);
			}
			if (this._bottomLeftClip != null)
			{
				this._bottomLeftClip.RadiusX = (this._bottomLeftClip.RadiusY = newCornerRadius.BottomLeft - Math.Min(base.BorderThickness.Bottom, base.BorderThickness.Left) / 2.0);
			}
			this.UpdateClipSize(new Size(base.ActualWidth, base.ActualHeight));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003650 File Offset: 0x00001850
		internal void UpdateClipContent(bool clipContent)
		{
			if (clipContent)
			{
				if (this._topLeftContentControl != null)
				{
					this._topLeftContentControl.Clip = this._topLeftClip;
				}
				if (this._topRightContentControl != null)
				{
					this._topRightContentControl.Clip = this._topRightClip;
				}
				if (this._bottomRightContentControl != null)
				{
					this._bottomRightContentControl.Clip = this._bottomRightClip;
				}
				if (this._bottomLeftContentControl != null)
				{
					this._bottomLeftContentControl.Clip = this._bottomLeftClip;
				}
				this.UpdateClipSize(new Size(base.ActualWidth, base.ActualHeight));
				return;
			}
			if (this._topLeftContentControl != null)
			{
				this._topLeftContentControl.Clip = null;
			}
			if (this._topRightContentControl != null)
			{
				this._topRightContentControl.Clip = null;
			}
			if (this._bottomRightContentControl != null)
			{
				this._bottomRightContentControl.Clip = null;
			}
			if (this._bottomLeftContentControl != null)
			{
				this._bottomLeftContentControl.Clip = null;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000372C File Offset: 0x0000192C
		private static void CornerRadiusChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			((ClippingBorder)dependencyObject).UpdateCornerRadius((CornerRadius)eventArgs.NewValue);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003745 File Offset: 0x00001945
		private static void ClipContentChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			((ClippingBorder)dependencyObject).UpdateClipContent((bool)eventArgs.NewValue);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000375E File Offset: 0x0000195E
		private void ClippingBorderSizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (this.ClipContent)
			{
				this.UpdateClipSize(e.NewSize);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003774 File Offset: 0x00001974
		private void ContentControlSizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (this.ClipContent)
			{
				this.UpdateClipSize(new Size(base.ActualWidth, base.ActualHeight));
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003798 File Offset: 0x00001998
		private void UpdateClipSize(Size size)
		{
			if (size.Width > 0.0 || size.Height > 0.0)
			{
				double num = Math.Max(0.0, size.Width - base.BorderThickness.Left - base.BorderThickness.Right);
				double num2 = Math.Max(0.0, size.Height - base.BorderThickness.Top - base.BorderThickness.Bottom);
				if (this._topLeftClip != null)
				{
					this._topLeftClip.Rect = new Rect(0.0, 0.0, num + this.CornerRadius.TopLeft * 2.0, num2 + this.CornerRadius.TopLeft * 2.0);
				}
				if (this._topRightClip != null)
				{
					this._topRightClip.Rect = new Rect(0.0 - this.CornerRadius.TopRight, 0.0, num + this.CornerRadius.TopRight, num2 + this.CornerRadius.TopRight);
				}
				if (this._bottomRightClip != null)
				{
					this._bottomRightClip.Rect = new Rect(0.0 - this.CornerRadius.BottomRight, 0.0 - this.CornerRadius.BottomRight, num + this.CornerRadius.BottomRight, num2 + this.CornerRadius.BottomRight);
				}
				if (this._bottomLeftClip != null)
				{
					this._bottomLeftClip.Rect = new Rect(0.0, 0.0 - this.CornerRadius.BottomLeft, num + this.CornerRadius.BottomLeft, num2 + this.CornerRadius.BottomLeft);
				}
			}
		}

		// Token: 0x04000032 RID: 50
		public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ClippingBorder), new PropertyMetadata(new PropertyChangedCallback(ClippingBorder.CornerRadiusChanged)));

		// Token: 0x04000033 RID: 51
		public static readonly DependencyProperty ClipContentProperty = DependencyProperty.Register("ClipContent", typeof(bool), typeof(ClippingBorder), new PropertyMetadata(new PropertyChangedCallback(ClippingBorder.ClipContentChanged)));

		// Token: 0x04000034 RID: 52
		private Border _border;

		// Token: 0x04000035 RID: 53
		private RectangleGeometry _bottomLeftClip;

		// Token: 0x04000036 RID: 54
		private ContentControl _bottomLeftContentControl;

		// Token: 0x04000037 RID: 55
		private RectangleGeometry _bottomRightClip;

		// Token: 0x04000038 RID: 56
		private ContentControl _bottomRightContentControl;

		// Token: 0x04000039 RID: 57
		private RectangleGeometry _topLeftClip;

		// Token: 0x0400003A RID: 58
		private ContentControl _topLeftContentControl;

		// Token: 0x0400003B RID: 59
		private RectangleGeometry _topRightClip;

		// Token: 0x0400003C RID: 60
		private ContentControl _topRightContentControl;
	}
}
