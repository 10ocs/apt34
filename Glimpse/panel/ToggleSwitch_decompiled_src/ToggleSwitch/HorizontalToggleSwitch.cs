using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ToggleSwitch
{
	// Token: 0x02000002 RID: 2
	public class HorizontalToggleSwitch : ToggleSwitchBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public HorizontalToggleSwitch()
		{
			base.DefaultStyleKey = typeof(HorizontalToggleSwitch);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002075 File Offset: 0x00000275
		protected override double Offset
		{
			get
			{
				return Canvas.GetLeft(base.SwitchThumb);
			}
			set
			{
				base.SwitchTrack.BeginAnimation(Canvas.LeftProperty, null);
				base.SwitchThumb.BeginAnimation(Canvas.LeftProperty, null);
				Canvas.SetLeft(base.SwitchTrack, value);
				Canvas.SetLeft(base.SwitchThumb, value);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020B1 File Offset: 0x000002B1
		protected override PropertyPath SlidePropertyPath
		{
			get
			{
				return new PropertyPath("(Canvas.Left)", new object[0]);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020C3 File Offset: 0x000002C3
		protected override void OnDragDelta(object sender, DragDeltaEventArgs e)
		{
			base.DragOffset += e.HorizontalChange;
			this.Offset = Math.Max(base.UncheckedOffset, Math.Min(base.CheckedOffset, base.DragOffset));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020FC File Offset: 0x000002FC
		protected override void LayoutControls()
		{
			if (base.SwitchThumb == null || base.SwitchRoot == null)
			{
				return;
			}
			double num = base.SwitchThumb.ActualWidth + base.SwitchThumb.BorderThickness.Left + base.SwitchThumb.BorderThickness.Right;
			if (base.SwitchChecked != null && base.SwitchUnchecked != null)
			{
				base.SwitchChecked.Width = (base.SwitchUnchecked.Width = Math.Max(0.0, base.SwitchRoot.ActualWidth - num / 2.0));
				base.SwitchChecked.Padding = new Thickness(0.0, 0.0, (base.SwitchThumb.ActualWidth + base.SwitchThumb.BorderThickness.Left) / 2.0, 0.0);
				base.SwitchUnchecked.Padding = new Thickness((base.SwitchThumb.ActualWidth + base.SwitchThumb.BorderThickness.Right) / 2.0, 0.0, 0.0, 0.0);
			}
			base.SwitchThumb.Margin = new Thickness(base.SwitchRoot.ActualWidth - num, base.SwitchThumb.Margin.Top, 0.0, base.SwitchThumb.Margin.Bottom);
			base.UncheckedOffset = -base.SwitchRoot.ActualWidth + num - base.SwitchThumb.BorderThickness.Left;
			base.CheckedOffset = base.SwitchThumb.BorderThickness.Right;
			if (!base.IsDragging)
			{
				this.Offset = (base.IsChecked ? base.CheckedOffset : base.UncheckedOffset);
				this.ChangeCheckStates(false);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002304 File Offset: 0x00000504
		protected override void OnDragCompleted(object sender, DragCompletedEventArgs e)
		{
			base.IsDragging = false;
			bool flag = false;
			double num = base.SwitchThumb.ActualWidth + base.SwitchThumb.BorderThickness.Left + base.SwitchThumb.BorderThickness.Right;
			if ((!base.IsChecked && base.DragOffset > (base.SwitchRoot.ActualWidth - num) * (base.Elasticity - 1.0)) || (base.IsChecked && base.DragOffset < (base.SwitchRoot.ActualWidth - num) * -base.Elasticity))
			{
				double num2 = base.IsChecked ? base.CheckedOffset : base.UncheckedOffset;
				if (this.Offset != num2)
				{
					flag = true;
				}
			}
			else if (base.DragOffset == base.CheckedOffset || base.DragOffset == base.UncheckedOffset)
			{
				flag = true;
			}
			else
			{
				this.ChangeCheckStates(true);
			}
			if (flag)
			{
				base.OnClick();
			}
			base.DragOffset = 0.0;
		}
	}
}
