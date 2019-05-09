using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ToggleSwitch.Utils;

namespace ToggleSwitch
{
	// Token: 0x02000003 RID: 3
	[TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
	[TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
	[TemplateVisualState(Name = "MouseOver", GroupName = "CommonStates")]
	[TemplateVisualState(Name = "Focused", GroupName = "FocusStates")]
	[TemplateVisualState(Name = "Unfocused", GroupName = "FocusStates")]
	[TemplateVisualState(Name = "Checked", GroupName = "CheckStates")]
	[TemplateVisualState(Name = "Unchecked", GroupName = "CheckStates")]
	[TemplateVisualState(Name = "DraggingChecked", GroupName = "CheckStates")]
	[TemplateVisualState(Name = "DraggingUnchecked", GroupName = "CheckStates")]
	[TemplatePart(Name = "SwitchChecked", Type = typeof(Control))]
	[TemplatePart(Name = "SwitchUnchecked", Type = typeof(Control))]
	[TemplatePart(Name = "SwitchThumb", Type = typeof(Thumb))]
	[TemplatePart(Name = "SwitchRoot", Type = typeof(FrameworkElement))]
	[TemplatePart(Name = "SwitchTrack", Type = typeof(FrameworkElement))]
	[Description("A control which when clicked or dragged toggles between on and off states.")]
	public abstract class ToggleSwitchBase : Control
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002404 File Offset: 0x00000604
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000240C File Offset: 0x0000060C
		protected Thumb SwitchThumb { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002415 File Offset: 0x00000615
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000241D File Offset: 0x0000061D
		protected Control SwitchChecked { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002426 File Offset: 0x00000626
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000242E File Offset: 0x0000062E
		protected Control SwitchUnchecked { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002437 File Offset: 0x00000637
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000243F File Offset: 0x0000063F
		protected FrameworkElement SwitchRoot { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002448 File Offset: 0x00000648
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002450 File Offset: 0x00000650
		protected FrameworkElement SwitchTrack { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18
		// (set) Token: 0x06000013 RID: 19
		protected abstract double Offset { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002459 File Offset: 0x00000659
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002461 File Offset: 0x00000661
		protected double CheckedOffset { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000246A File Offset: 0x0000066A
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002472 File Offset: 0x00000672
		protected double UncheckedOffset { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000247B File Offset: 0x0000067B
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002483 File Offset: 0x00000683
		protected double DragOffset { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000248C File Offset: 0x0000068C
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002494 File Offset: 0x00000694
		protected bool IsDragging { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000249D File Offset: 0x0000069D
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000024A5 File Offset: 0x000006A5
		public bool IsPressed { get; protected set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30
		protected abstract PropertyPath SlidePropertyPath { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000024AE File Offset: 0x000006AE
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000024C0 File Offset: 0x000006C0
		[Description("The template applied to the Checked and Unchecked content properties.")]
		public ControlTemplate ContentTemplate
		{
			get
			{
				return (ControlTemplate)base.GetValue(ToggleSwitchBase.ContentTemplateProperty);
			}
			set
			{
				base.SetValue(ToggleSwitchBase.ContentTemplateProperty, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000024CE File Offset: 0x000006CE
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000024DB File Offset: 0x000006DB
		[Category("Common Properties")]
		[Description("The content shown on the checked side of the toggle switch")]
		public object CheckedContent
		{
			get
			{
				return base.GetValue(ToggleSwitchBase.CheckedContentProperty);
			}
			set
			{
				base.SetValue(ToggleSwitchBase.CheckedContentProperty, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000024E9 File Offset: 0x000006E9
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000024FB File Offset: 0x000006FB
		[Description("The brush used for the foreground of the checked side of the toggle switch.")]
		public Brush CheckedForeground
		{
			get
			{
				return (Brush)base.GetValue(ToggleSwitchBase.CheckedForegroundProperty);
			}
			set
			{
				base.SetValue(ToggleSwitchBase.CheckedForegroundProperty, value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002509 File Offset: 0x00000709
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000251B File Offset: 0x0000071B
		[Description("The brush used for the background of the checked side of the toggle switch.")]
		public Brush CheckedBackground
		{
			get
			{
				return (Brush)base.GetValue(ToggleSwitchBase.CheckedBackgroundProperty);
			}
			set
			{
				base.SetValue(ToggleSwitchBase.CheckedBackgroundProperty, value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002529 File Offset: 0x00000729
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002536 File Offset: 0x00000736
		[Category("Common Properties")]
		[Description("The content shown on the unchecked side of the toggle switch.")]
		public object UncheckedContent
		{
			get
			{
				return base.GetValue(ToggleSwitchBase.UncheckedContentProperty);
			}
			set
			{
				base.SetValue(ToggleSwitchBase.UncheckedContentProperty, value);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002544 File Offset: 0x00000744
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002556 File Offset: 0x00000756
		[Description("The brush used for the foreground of the Unchecked side of the toggle switch.")]
		public Brush UncheckedForeground
		{
			get
			{
				return (Brush)base.GetValue(ToggleSwitchBase.UncheckedForegroundProperty);
			}
			set
			{
				base.SetValue(ToggleSwitchBase.UncheckedForegroundProperty, value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002564 File Offset: 0x00000764
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002576 File Offset: 0x00000776
		[Description("The brush used for the background of the Unchecked side of the toggle switch.")]
		public Brush UncheckedBackground
		{
			get
			{
				return (Brush)base.GetValue(ToggleSwitchBase.UncheckedBackgroundProperty);
			}
			set
			{
				base.SetValue(ToggleSwitchBase.UncheckedBackgroundProperty, value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002584 File Offset: 0x00000784
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000025AD File Offset: 0x000007AD
		[Category("Common Properties")]
		[Description("Determines the percentage of the way the thumb must be dragged before the switch changes it's IsChecked state.")]
		public double Elasticity
		{
			get
			{
				return ((double)base.GetValue(ToggleSwitchBase.ElasticityProperty)).Clamp(0.0, 1.0);
			}
			set
			{
				base.SetValue(ToggleSwitchBase.ElasticityProperty, value.Clamp(0.0, 1.0));
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025D7 File Offset: 0x000007D7
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000025E9 File Offset: 0x000007E9
		[Description("The thumb's control template.")]
		public ControlTemplate ThumbTemplate
		{
			get
			{
				return (ControlTemplate)base.GetValue(ToggleSwitchBase.ThumbTemplateProperty);
			}
			set
			{
				base.SetValue(ToggleSwitchBase.ThumbTemplateProperty, value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000025F7 File Offset: 0x000007F7
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002609 File Offset: 0x00000809
		[Description("The brush used to fill the thumb.")]
		public Brush ThumbBrush
		{
			get
			{
				return (Brush)base.GetValue(ToggleSwitchBase.ThumbBrushProperty);
			}
			set
			{
				base.SetValue(ToggleSwitchBase.ThumbBrushProperty, value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002617 File Offset: 0x00000817
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002629 File Offset: 0x00000829
		[Category("Appearance")]
		[Description("The size of the toggle switch's thumb.")]
		public double ThumbSize
		{
			get
			{
				return (double)base.GetValue(ToggleSwitchBase.ThumbSizeProperty);
			}
			set
			{
				base.SetValue(ToggleSwitchBase.ThumbSizeProperty, value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000263C File Offset: 0x0000083C
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000264E File Offset: 0x0000084E
		[Category("Common Properties")]
		[Description("Gets or sets whether the control is in the checked state.")]
		public bool IsChecked
		{
			get
			{
				return (bool)base.GetValue(ToggleSwitchBase.IsCheckedProperty);
			}
			set
			{
				base.SetValue(ToggleSwitchBase.IsCheckedProperty, value);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002664 File Offset: 0x00000864
		private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ToggleSwitchBase toggleSwitchBase = (ToggleSwitchBase)d;
			if (e.NewValue != e.OldValue)
			{
				if ((bool)e.NewValue)
				{
					toggleSwitchBase.InvokeChecked(new RoutedEventArgs());
				}
				else
				{
					toggleSwitchBase.InvokeUnchecked(new RoutedEventArgs());
				}
			}
			toggleSwitchBase.ChangeCheckStates(true);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000038 RID: 56 RVA: 0x000026B8 File Offset: 0x000008B8
		// (remove) Token: 0x06000039 RID: 57 RVA: 0x000026F0 File Offset: 0x000008F0
		public event RoutedEventHandler Unchecked;

		// Token: 0x0600003A RID: 58 RVA: 0x00002728 File Offset: 0x00000928
		protected void InvokeUnchecked(RoutedEventArgs e)
		{
			RoutedEventHandler @unchecked = this.Unchecked;
			if (@unchecked != null)
			{
				@unchecked(this, e);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600003B RID: 59 RVA: 0x00002748 File Offset: 0x00000948
		// (remove) Token: 0x0600003C RID: 60 RVA: 0x00002780 File Offset: 0x00000980
		public event RoutedEventHandler Checked;

		// Token: 0x0600003D RID: 61 RVA: 0x000027B8 File Offset: 0x000009B8
		protected void InvokeChecked(RoutedEventArgs e)
		{
			RoutedEventHandler @checked = this.Checked;
			if (@checked != null)
			{
				@checked(this, e);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000027D7 File Offset: 0x000009D7
		protected ToggleSwitchBase()
		{
			base.Loaded += delegate(object A_1, RoutedEventArgs A_2)
			{
				this.UpdateVisualState(false);
			};
			base.IsEnabledChanged += this.OnIsEnabledChanged;
		}

		// Token: 0x0600003F RID: 63
		protected abstract void OnDragDelta(object sender, DragDeltaEventArgs e);

		// Token: 0x06000040 RID: 64
		protected abstract void OnDragCompleted(object sender, DragCompletedEventArgs e);

		// Token: 0x06000041 RID: 65
		protected abstract void LayoutControls();

		// Token: 0x06000042 RID: 66 RVA: 0x00002804 File Offset: 0x00000A04
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this.RemoveEventHandlers();
			this.GetTemplateChildren();
			this.AddEventHandlers();
			this.LayoutControls();
			VisualStateManager.GoToState(this, base.IsEnabled ? "Normal" : "Disabled", false);
			this.ChangeCheckStates(false);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002854 File Offset: 0x00000A54
		protected virtual void GetTemplateChildren()
		{
			this.SwitchRoot = (base.GetTemplateChild("SwitchRoot") as FrameworkElement);
			this.SwitchThumb = (base.GetTemplateChild("SwitchThumb") as Thumb);
			this.SwitchChecked = (base.GetTemplateChild("SwitchChecked") as Control);
			this.SwitchUnchecked = (base.GetTemplateChild("SwitchUnchecked") as Control);
			this.SwitchTrack = (base.GetTemplateChild("SwitchTrack") as FrameworkElement);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000028D0 File Offset: 0x00000AD0
		protected virtual void AddEventHandlers()
		{
			if (this.SwitchThumb != null)
			{
				this.SwitchThumb.DragStarted += this.OnDragStarted;
				this.SwitchThumb.DragDelta += this.OnDragDelta;
				this.SwitchThumb.DragCompleted += this.OnDragCompleted;
			}
			base.SizeChanged += this.OnSizeChanged;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002940 File Offset: 0x00000B40
		protected virtual void RemoveEventHandlers()
		{
			if (this.SwitchThumb != null)
			{
				this.SwitchThumb.DragStarted -= this.OnDragStarted;
				this.SwitchThumb.DragDelta -= this.OnDragDelta;
				this.SwitchThumb.DragCompleted -= this.OnDragCompleted;
			}
			base.SizeChanged -= this.OnSizeChanged;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000029B0 File Offset: 0x00000BB0
		protected virtual void OnDragStarted(object sender, DragStartedEventArgs e)
		{
			this.IsDragging = true;
			this.DragOffset = this.Offset;
			this.ChangeCheckStates(false);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000029CC File Offset: 0x00000BCC
		protected void OnClick()
		{
			base.SetCurrentValue(ToggleSwitchBase.IsCheckedProperty, !this.IsChecked);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029E7 File Offset: 0x00000BE7
		protected virtual void OnSizeChanged(object sender, SizeChangedEventArgs e)
		{
			this.LayoutControls();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029EF File Offset: 0x00000BEF
		internal void CaptureMouseInternal()
		{
			if (!this._isMouseCaptured)
			{
				this._isMouseCaptured = base.CaptureMouse();
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002A05 File Offset: 0x00000C05
		protected internal void ReleaseMouseCaptureInternal()
		{
			base.ReleaseMouseCapture();
			this._isMouseCaptured = false;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A14 File Offset: 0x00000C14
		private static void OnLayoutDependancyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue != e.OldValue)
			{
				((ToggleSwitchBase)d).LayoutControls();
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A31 File Offset: 0x00000C31
		private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this._suspendStateChanges = true;
			if (!base.IsEnabled)
			{
				this.IsPressed = false;
				this._isMouseCaptured = false;
				this._isSpaceKeyDown = false;
				this._isMouseLeftButtonDown = false;
			}
			this._suspendStateChanges = false;
			this.UpdateVisualState(true);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A6C File Offset: 0x00000C6C
		protected override void OnLostFocus(RoutedEventArgs e)
		{
			base.OnLostFocus(e);
			this.IsPressed = false;
			this.ReleaseMouseCaptureInternal();
			this._isSpaceKeyDown = false;
			this._suspendStateChanges = false;
			this.UpdateVisualState(true);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A97 File Offset: 0x00000C97
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.Handled)
			{
				return;
			}
			if (this.OnKeyDownInternal(e.Key))
			{
				e.Handled = true;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002AC0 File Offset: 0x00000CC0
		private bool OnKeyDownInternal(Key key)
		{
			bool result = false;
			if (base.IsEnabled)
			{
				if (key == Key.Space)
				{
					if (!this._isMouseCaptured && !this._isSpaceKeyDown)
					{
						this._isSpaceKeyDown = true;
						this.IsPressed = true;
						this.CaptureMouseInternal();
						result = true;
					}
				}
				else if (key == Key.Return)
				{
					this._isSpaceKeyDown = false;
					this.IsPressed = false;
					this.ReleaseMouseCaptureInternal();
					this.OnClick();
					result = true;
				}
				else if (this._isSpaceKeyDown)
				{
					this.IsPressed = false;
					this._isSpaceKeyDown = false;
					this.ReleaseMouseCaptureInternal();
				}
			}
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B43 File Offset: 0x00000D43
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (e.Handled)
			{
				return;
			}
			if (this.OnKeyUpInternal(e.Key))
			{
				e.Handled = true;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B6C File Offset: 0x00000D6C
		private bool OnKeyUpInternal(Key key)
		{
			bool result = false;
			if (base.IsEnabled && key == Key.Space)
			{
				this._isSpaceKeyDown = false;
				if (!this._isMouseLeftButtonDown)
				{
					this.ReleaseMouseCaptureInternal();
					if (this.IsPressed)
					{
						this.OnClick();
					}
					this.IsPressed = false;
				}
				else if (this._isMouseCaptured)
				{
					bool flag = this.IsValidMousePosition();
					this.IsPressed = flag;
					if (!flag)
					{
						this.ReleaseMouseCaptureInternal();
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002BD8 File Offset: 0x00000DD8
		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			base.OnMouseLeftButtonDown(e);
			if (e.Handled)
			{
				return;
			}
			this._isMouseLeftButtonDown = true;
			if (!base.IsEnabled)
			{
				return;
			}
			e.Handled = true;
			this._suspendStateChanges = true;
			base.Focus();
			this.CaptureMouseInternal();
			if (this._isMouseCaptured)
			{
				this.IsPressed = true;
			}
			this._suspendStateChanges = false;
			this.UpdateVisualState(true);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C40 File Offset: 0x00000E40
		protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
		{
			base.OnMouseLeftButtonUp(e);
			if (e.Handled)
			{
				return;
			}
			this._isMouseLeftButtonDown = false;
			if (!base.IsEnabled)
			{
				return;
			}
			e.Handled = true;
			if (!this._isSpaceKeyDown && this.IsPressed)
			{
				this.OnClick();
			}
			if (!this._isSpaceKeyDown)
			{
				this.ReleaseMouseCaptureInternal();
				this.IsPressed = false;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002CA0 File Offset: 0x00000EA0
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this._mousePosition = e.GetPosition(this);
			if (this._isMouseLeftButtonDown && base.IsEnabled && this._isMouseCaptured && !this._isSpaceKeyDown)
			{
				this.IsPressed = this.IsValidMousePosition();
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002CF0 File Offset: 0x00000EF0
		private bool IsValidMousePosition()
		{
			return this._mousePosition.X >= 0.0 && this._mousePosition.X <= base.ActualWidth && this._mousePosition.Y >= 0.0 && this._mousePosition.Y <= base.ActualHeight;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002D54 File Offset: 0x00000F54
		protected bool GoToState(bool useTransitions, string stateName)
		{
			return VisualStateManager.GoToState(this, stateName, useTransitions);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002D60 File Offset: 0x00000F60
		protected virtual void ChangeVisualState(bool useTransitions)
		{
			if (!base.IsEnabled)
			{
				this.GoToState(useTransitions, "Disabled");
			}
			else
			{
				this.GoToState(useTransitions, base.IsMouseOver ? "MouseOver" : "Normal");
			}
			if (base.IsFocused && base.IsEnabled)
			{
				this.GoToState(useTransitions, "Focused");
				return;
			}
			this.GoToState(useTransitions, "Unfocused");
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002DCB File Offset: 0x00000FCB
		protected void UpdateVisualState(bool useTransitions = true)
		{
			if (!this._suspendStateChanges)
			{
				this.ChangeVisualState(useTransitions);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002DDC File Offset: 0x00000FDC
		protected virtual void ChangeCheckStates(bool useTransitions)
		{
			string text = this.IsChecked ? "Checked" : "Unchecked";
			if (this.IsDragging)
			{
				VisualStateManager.GoToState(this, "Dragging" + text, useTransitions);
			}
			else
			{
				VisualStateManager.GoToState(this, text, useTransitions);
				if (this.SwitchThumb != null)
				{
					VisualStateManager.GoToState(this.SwitchThumb, text, useTransitions);
				}
			}
			if (this.SwitchThumb == null || this.SwitchTrack == null)
			{
				return;
			}
			Storyboard storyboard = new Storyboard();
			Duration duration = new Duration(useTransitions ? TimeSpan.FromMilliseconds(100.0) : TimeSpan.Zero);
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			DoubleAnimation doubleAnimation2 = new DoubleAnimation();
			doubleAnimation.Duration = duration;
			doubleAnimation2.Duration = duration;
			double value = this.IsChecked ? this.CheckedOffset : this.UncheckedOffset;
			doubleAnimation.To = new double?(value);
			doubleAnimation2.To = new double?(value);
			storyboard.Children.Add(doubleAnimation);
			storyboard.Children.Add(doubleAnimation2);
			Storyboard.SetTarget(doubleAnimation, this.SwitchTrack);
			Storyboard.SetTarget(doubleAnimation2, this.SwitchThumb);
			Storyboard.SetTargetProperty(doubleAnimation, this.SlidePropertyPath);
			Storyboard.SetTargetProperty(doubleAnimation2, this.SlidePropertyPath);
			storyboard.Begin();
		}

		// Token: 0x04000001 RID: 1
		private const string CommonStates = "CommonStates";

		// Token: 0x04000002 RID: 2
		private const string NormalState = "Normal";

		// Token: 0x04000003 RID: 3
		private const string DisabledState = "Disabled";

		// Token: 0x04000004 RID: 4
		private const string MouseOverState = "MouseOver";

		// Token: 0x04000005 RID: 5
		private const string CheckStates = "CheckStates";

		// Token: 0x04000006 RID: 6
		private const string CheckedState = "Checked";

		// Token: 0x04000007 RID: 7
		private const string DraggingState = "Dragging";

		// Token: 0x04000008 RID: 8
		private const string UncheckedState = "Unchecked";

		// Token: 0x04000009 RID: 9
		private const string FocusStates = "FocusStates";

		// Token: 0x0400000A RID: 10
		private const string FocusedState = "Focused";

		// Token: 0x0400000B RID: 11
		private const string UnfocusedState = "Unfocused";

		// Token: 0x0400000C RID: 12
		private const string SwitchRootPart = "SwitchRoot";

		// Token: 0x0400000D RID: 13
		private const string SwitchCheckedPart = "SwitchChecked";

		// Token: 0x0400000E RID: 14
		private const string SwitchUncheckedPart = "SwitchUnchecked";

		// Token: 0x0400000F RID: 15
		private const string SwitchThumbPart = "SwitchThumb";

		// Token: 0x04000010 RID: 16
		private const string SwitchTrackPart = "SwitchTrack";

		// Token: 0x04000011 RID: 17
		private const string CommonPropertiesCategory = "Common Properties";

		// Token: 0x04000012 RID: 18
		private const string AppearanceCategory = "Appearance";

		// Token: 0x04000013 RID: 19
		private bool _isMouseCaptured;

		// Token: 0x04000014 RID: 20
		private bool _isSpaceKeyDown;

		// Token: 0x04000015 RID: 21
		private bool _isMouseLeftButtonDown;

		// Token: 0x04000016 RID: 22
		private Point _mousePosition;

		// Token: 0x04000017 RID: 23
		private bool _suspendStateChanges;

		// Token: 0x04000022 RID: 34
		public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register("ContentTemplate", typeof(ControlTemplate), typeof(ToggleSwitchBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange, new PropertyChangedCallback(ToggleSwitchBase.OnLayoutDependancyPropertyChanged)));

		// Token: 0x04000023 RID: 35
		public static readonly DependencyProperty CheckedContentProperty = DependencyProperty.Register("CheckedContent", typeof(object), typeof(ToggleSwitchBase), new FrameworkPropertyMetadata("ON", FrameworkPropertyMetadataOptions.AffectsArrange, new PropertyChangedCallback(ToggleSwitchBase.OnLayoutDependancyPropertyChanged)));

		// Token: 0x04000024 RID: 36
		public static readonly DependencyProperty CheckedForegroundProperty = DependencyProperty.Register("CheckedForeground", typeof(Brush), typeof(ToggleSwitchBase), null);

		// Token: 0x04000025 RID: 37
		public static readonly DependencyProperty CheckedBackgroundProperty = DependencyProperty.Register("CheckedBackground", typeof(Brush), typeof(ToggleSwitchBase), null);

		// Token: 0x04000026 RID: 38
		public static readonly DependencyProperty UncheckedContentProperty = DependencyProperty.Register("UncheckedContent", typeof(object), typeof(ToggleSwitchBase), new FrameworkPropertyMetadata("OFF", FrameworkPropertyMetadataOptions.AffectsArrange, new PropertyChangedCallback(ToggleSwitchBase.OnLayoutDependancyPropertyChanged)));

		// Token: 0x04000027 RID: 39
		public static readonly DependencyProperty UncheckedForegroundProperty = DependencyProperty.Register("UncheckedForeground", typeof(Brush), typeof(ToggleSwitchBase), null);

		// Token: 0x04000028 RID: 40
		public static readonly DependencyProperty UncheckedBackgroundProperty = DependencyProperty.Register("UncheckedBackground", typeof(Brush), typeof(ToggleSwitchBase), null);

		// Token: 0x04000029 RID: 41
		public static readonly DependencyProperty ElasticityProperty = DependencyProperty.Register("Elasticity", typeof(double), typeof(ToggleSwitchBase), new PropertyMetadata(0.5));

		// Token: 0x0400002A RID: 42
		public static readonly DependencyProperty ThumbTemplateProperty = DependencyProperty.Register("ThumbTemplate", typeof(ControlTemplate), typeof(ToggleSwitchBase), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange, new PropertyChangedCallback(ToggleSwitchBase.OnLayoutDependancyPropertyChanged)));

		// Token: 0x0400002B RID: 43
		public static readonly DependencyProperty ThumbBrushProperty = DependencyProperty.Register("ThumbBrush", typeof(Brush), typeof(ToggleSwitchBase), null);

		// Token: 0x0400002C RID: 44
		public static readonly DependencyProperty ThumbSizeProperty = DependencyProperty.Register("ThumbSize", typeof(double), typeof(ToggleSwitchBase), new FrameworkPropertyMetadata(40.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange, new PropertyChangedCallback(ToggleSwitchBase.OnLayoutDependancyPropertyChanged)));

		// Token: 0x0400002D RID: 45
		public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register("IsChecked", typeof(bool), typeof(ToggleSwitchBase), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsArrange, new PropertyChangedCallback(ToggleSwitchBase.OnIsCheckedChanged)));
	}
}
