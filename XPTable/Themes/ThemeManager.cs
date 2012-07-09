/*
 * Copyright © 2005, Mathew Hall
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 *
 *    - Redistributions of source code must retain the above copyright notice, 
 *      this list of conditions and the following disclaimer.
 * 
 *    - Redistributions in binary form must reproduce the above copyright notice, 
 *      this list of conditions and the following disclaimer in the documentation 
 *      and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
 * OF SUCH DAMAGE.
 */

using System;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using XPTable.Win32;


namespace XPTable.Themes
{
	/// <summary>
	/// A class that contains methods for drawing Windows XP themed Control parts
	/// </summary>
	public abstract class ThemeManager
	{
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the ThemeManager class with default settings
		/// </summary>
		protected ThemeManager()
		{
			
		}

		#endregion


		#region Methods

		#region Painting

		#region Button

		/// <summary>
		/// Draws a push button in the specified state, on the specified graphics 
		/// surface, and within the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="buttonRect">The Rectangle that represents the dimensions 
		/// of the button</param>
		/// <param name="state">A PushButtonState value that specifies the 
		/// state to draw the button in</param>
        /// <param name="flatStyle">If true, then the button is drawn in flat style, but only if VisualStyles are not being used.</param>
		public static void DrawButton(Graphics g, Rectangle buttonRect, PushButtonState state, bool flatStyle)
		{
			ThemeManager.DrawButton(g, buttonRect, buttonRect, state, flatStyle);
		}


		/// <summary>
		/// Draws a push button in the specified state, on the specified graphics 
		/// surface, and within the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="buttonRect">The Rectangle that represents the dimensions 
		/// of the button</param>
		/// <param name="clipRect">The Rectangle that represents the clipping area</param>
		/// <param name="state">A PushButtonState value that specifies the 
		/// state to draw the button in</param>
        /// <param name="flatStyle">If true, then the button is drawn in flat style, but only if VisualStyles are not being used.</param>
        public static void DrawButton(Graphics g, Rectangle buttonRect, Rectangle clipRect, PushButtonState state, bool flatStyle)
		{
			if (g == null || buttonRect.Width <= 0 || buttonRect.Height <= 0 || clipRect.Width <= 0 || clipRect.Height <= 0)
			{
				return;
			}

			if (ThemeManager.VisualStylesEnabled)
			{
				//ThemeManager.DrawThemeBackground(g, ThemeClasses.Button, (int) ButtonParts.PushButton, (int) state, buttonRect, clipRect);
				VisualStyleRenderer renderer;
				switch (state)
				{
					case PushButtonState.Disabled:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Disabled);
						break;
					case PushButtonState.Hot:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Hot);
						break;
					case PushButtonState.Normal:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Normal);
						break;
					case PushButtonState.Pressed:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Pressed);
						break;
					default:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.PushButton.Default);
						break;
				}
				renderer.DrawBackground(g, buttonRect, clipRect);
			}
			else
			{
                ButtonState newState = ThemeManager.ConvertPushButtonStateToButtonState(state);
                if (flatStyle)
                    newState = newState | ButtonState.Flat;
				ControlPaint.DrawButton(g, buttonRect, newState);
			}
		}


		/// <summary>
		/// Converts the specified PushButtonState value to a ButtonState value
		/// </summary>
		/// <param name="state">The PushButtonState value to be converted</param>
		/// <returns>A ButtonState value that represents the specified PushButtonState 
		/// value</returns>
		private static ButtonState ConvertPushButtonStateToButtonState(PushButtonState state)
		{
			switch (state)
			{
				case PushButtonState.Pressed:
					return ButtonState.Pushed;

				case PushButtonState.Disabled:
					return ButtonState.Inactive;
            }

			return ButtonState.Normal;
		}

		#endregion

		#region CheckBox

		/// <summary>
		/// Draws a check box in the specified state, on the specified graphics 
		/// surface, and within the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="checkRect">The Rectangle that represents the dimensions 
		/// of the check box</param>
		/// <param name="state">A CheckBoxState value that specifies the 
		/// state to draw the check box in</param>
		public static void DrawCheck(Graphics g, Rectangle checkRect, CheckBoxState state)
		{
			ThemeManager.DrawCheck(g, checkRect, checkRect, state);
		}


		/// <summary>
		/// Draws a check box in the specified state, on the specified graphics 
		/// surface, and within the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="checkRect">The Rectangle that represents the dimensions 
		/// of the check box</param>
		/// <param name="clipRect">The Rectangle that represents the clipping area</param>
		/// <param name="state">A CheckBoxState value that specifies the 
		/// state to draw the check box in</param>
		public static void DrawCheck(Graphics g, Rectangle checkRect, Rectangle clipRect, CheckBoxState state)
		{
			if (g == null || checkRect.Width <= 0 || checkRect.Height <= 0)
			{
				return;
			}

			if (ThemeManager.VisualStylesEnabled)
			{
				//ThemeManager.DrawThemeBackground(g, ThemeClasses.Button, (int) ButtonParts.CheckBox, (int) state, checkRect, clipRect);
				VisualStyleRenderer renderer;
				switch (state)
				{
					case CheckBoxState.CheckedDisabled:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.CheckedDisabled);
						break;
					case CheckBoxState.CheckedHot:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.CheckedHot);
						break;
					case CheckBoxState.CheckedNormal:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.CheckedNormal);
						break;
					case CheckBoxState.CheckedPressed:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.CheckedPressed);
						break;
					case CheckBoxState.MixedDisabled:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.MixedDisabled);
						break;
					case CheckBoxState.MixedHot:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.MixedHot);
						break;
					case CheckBoxState.MixedNormal:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.MixedNormal);
						break;
					case CheckBoxState.MixedPressed:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.MixedPressed);
						break;
					case CheckBoxState.UncheckedDisabled:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.UncheckedDisabled);
						break;
					case CheckBoxState.UncheckedHot:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.UncheckedHot);
						break;
					case CheckBoxState.UncheckedPressed:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.UncheckedPressed);
						break;
					case CheckBoxState.UncheckedNormal:
					default:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.CheckBox.UncheckedNormal);
						break;
				}
				renderer.DrawBackground(g, checkRect, clipRect);
			}
			else
			{
				if (IsMixed(state))
				{
					ControlPaint.DrawMixedCheckBox(g, checkRect, ThemeManager.ConvertCheckBoxStateToButtonState(state));
				}
				else
				{
					ControlPaint.DrawCheckBox(g, checkRect, ThemeManager.ConvertCheckBoxStateToButtonState(state));
				}
			}
		}


		/// <summary>
		/// Converts the specified CheckBoxState value to a ButtonState value
		/// </summary>
		/// <param name="state">The CheckBoxState value to be converted</param>
		/// <returns>A ButtonState value that represents the specified CheckBoxState 
		/// value</returns>
		private static ButtonState ConvertCheckBoxStateToButtonState(CheckBoxState state)
		{
			switch (state)
			{
				case CheckBoxState.UncheckedPressed:
				{
					return ButtonState.Pushed;
				}

				case CheckBoxState.UncheckedDisabled:
				{
					return ButtonState.Inactive;
				}

				case CheckBoxState.CheckedNormal:
				case CheckBoxState.CheckedHot:
				{
					return ButtonState.Checked;
				}

				case CheckBoxState.CheckedPressed:
				{
					return (ButtonState.Checked | ButtonState.Pushed);
				}

				case CheckBoxState.CheckedDisabled:
				{
					return (ButtonState.Checked | ButtonState.Inactive);
				}

				case CheckBoxState.MixedNormal:
				case CheckBoxState.MixedHot:
				{
					return ButtonState.Checked;
				}

				case CheckBoxState.MixedPressed:
				{
					return (ButtonState.Checked | ButtonState.Pushed);
				}

				case CheckBoxState.MixedDisabled:
				{
					return (ButtonState.Checked | ButtonState.Inactive);
				}
			}

			return ButtonState.Normal;
		}


		/// <summary>
		/// Returns whether the specified CheckBoxState value is in an 
		/// indeterminate state
		/// </summary>
		/// <param name="state">The CheckBoxState value to be checked</param>
		/// <returns>true if the specified CheckBoxState value is in an 
		/// indeterminate state, false otherwise</returns>
		private static bool IsMixed(CheckBoxState state)
		{
			switch (state)
			{
				case CheckBoxState.MixedNormal:
				case CheckBoxState.MixedHot:
				case CheckBoxState.MixedPressed:
				case CheckBoxState.MixedDisabled:
				{
					return true;
				}
			}

			return false;
		}

		#endregion

		#region ColumnHeader

		/// <summary>
		/// Draws a column header in the specified state, on the specified graphics 
		/// surface, and within the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="headerRect">The Rectangle that represents the dimensions 
		/// of the column header</param>
		/// <param name="state">A ColumnHeaderState value that specifies the 
		/// state to draw the column header in</param>
		public static void DrawColumnHeader(Graphics g, Rectangle headerRect, ColumnHeaderState state)
		{
			ThemeManager.DrawColumnHeader(g, headerRect, headerRect, state);
		}


		/// <summary>
		/// Draws a column header in the specified state, on the specified graphics 
		/// surface, and within the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="headerRect">The Rectangle that represents the dimensions 
		/// of the column header</param>
		/// <param name="clipRect">The Rectangle that represents the clipping area</param>
		/// <param name="state">A ColumnHeaderState value that specifies the 
		/// state to draw the column header in</param>
		public static void DrawColumnHeader(Graphics g, Rectangle headerRect, Rectangle clipRect, ColumnHeaderState state)
		{
			if (g == null || headerRect.Width <= 0 || headerRect.Height <= 0 || clipRect.Width <= 0 || clipRect.Height <= 0)
			{
				return;
			}

			if (ThemeManager.VisualStylesEnabled)
			{
				//ThemeManager.DrawThemeBackground(g, ThemeClasses.ColumnHeader, (int) ColumnHeaderParts.HeaderItem, (int) state, headerRect, clipRect);
				VisualStyleRenderer renderer;
				switch (state)
				{
					case ColumnHeaderState.Hot:
						renderer = new VisualStyleRenderer(VisualStyleElement.Header.Item.Hot);
						break;
					case ColumnHeaderState.Pressed:
						renderer = new VisualStyleRenderer(VisualStyleElement.Header.Item.Pressed);
						break;
					case ColumnHeaderState.Normal:
					default:
						renderer = new VisualStyleRenderer(VisualStyleElement.Header.Item.Normal);
						break;
				}
				renderer.DrawBackground(g, headerRect, clipRect);
			}
			else
			{
				g.FillRectangle(SystemBrushes.Control, headerRect);

				if (state == ColumnHeaderState.Pressed)
				{
					g.DrawRectangle(SystemPens.ControlDark, headerRect.X, headerRect.Y, headerRect.Width-1, headerRect.Height-1);
				}
				else
				{
					ControlPaint.DrawBorder3D(g, headerRect.X, headerRect.Y, headerRect.Width, headerRect.Height, Border3DStyle.RaisedInner);
				}
			}
		}

		#endregion

		#region ComboBoxButton

		/// <summary>
		/// Draws a combobox button in the specified state, on the specified graphics 
		/// surface, and within the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="buttonRect">The Rectangle that represents the dimensions 
		/// of the combobox button</param>
		/// <param name="state">A ComboBoxState value that specifies the 
		/// state to draw the combobox button in</param>
		public static void DrawComboBoxButton(Graphics g, Rectangle buttonRect, ComboBoxState state)
		{
			ThemeManager.DrawComboBoxButton(g, buttonRect, buttonRect, state);
		}


		/// <summary>
		/// Draws a combobox button in the specified state, on the specified graphics 
		/// surface, and within the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="buttonRect">The Rectangle that represents the dimensions 
		/// of the button</param>
		/// <param name="clipRect">The Rectangle that represents the clipping area</param>
		/// <param name="state">A ComboBoxState value that specifies the 
		/// state to draw the combobox button in</param>
		public static void DrawComboBoxButton(Graphics g, Rectangle buttonRect, Rectangle clipRect, ComboBoxState state)
		{
			if (g == null || buttonRect.Width <= 0 || buttonRect.Height <= 0 || clipRect.Width <= 0 || clipRect.Height <= 0)
			{
				return;
			}

			if (ThemeManager.VisualStylesEnabled)
			{
				//ThemeManager.DrawThemeBackground(g, ThemeClasses.ComboBox, (int) ComboBoxParts.DropDownButton, (int) state, buttonRect, clipRect);
				VisualStyleRenderer renderer;
				switch (state)
				{
					case ComboBoxState.Disabled:
						renderer = new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Disabled);
						break;
					case ComboBoxState.Hot:
						renderer = new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Hot);
						break;
					case ComboBoxState.Pressed:
						renderer = new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Pressed);
						break;
					case ComboBoxState.Normal:
					default:
						renderer = new VisualStyleRenderer(VisualStyleElement.ComboBox.DropDownButton.Normal);
						break;
				}
				renderer.DrawBackground(g, buttonRect, clipRect);
			}
			else
			{
				ControlPaint.DrawComboButton(g, buttonRect, ThemeManager.ConvertComboBoxStateToButtonState(state));
			}
		}


		/// <summary>
		/// Converts the specified ComboBoxState value to a ButtonState value
		/// </summary>
		/// <param name="state">The ComboBoxState value to be converted</param>
		/// <returns>A ButtonState value that represents the specified ComboBoxState 
		/// value</returns>
		private static ButtonState ConvertComboBoxStateToButtonState(ComboBoxState state)
		{
			switch (state)
			{
				case ComboBoxState.Pressed:
				{
					return ButtonState.Pushed;
				}

				case ComboBoxState.Disabled:
				{
					return ButtonState.Inactive;
				}
			}

			return ButtonState.Normal;
		}

		#endregion

		#region ProgressBar

		/// <summary>
		/// Draws a ProgressBar on the specified graphics surface, and within 
		/// the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="drawRect">The Rectangle that represents the dimensions 
		/// of the ProgressBar</param>
		public static void DrawProgressBar(Graphics g, Rectangle drawRect)
		{
			ThemeManager.DrawProgressBar(g, drawRect, drawRect);
		}


		/// <summary>
		/// Draws a ProgressBar on the specified graphics surface, and within 
		/// the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="drawRect">The Rectangle that represents the dimensions 
		/// of the ProgressBar</param>
		/// <param name="clipRect">The Rectangle that represents the clipping area</param>
		public static void DrawProgressBar(Graphics g, Rectangle drawRect, Rectangle clipRect)
		{
			if (g == null || drawRect.Width <= 0 || drawRect.Height <= 0 || clipRect.Width <= 0 || clipRect.Height <= 0)
			{
				return;
			}

			if (ThemeManager.VisualStylesEnabled)
			{
				//ThemeManager.DrawThemeBackground(g, ThemeClasses.ProgressBar, (int) ProgressBarParts.Bar, 0, drawRect, clipRect);
				VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.ProgressBar.Bar.Normal);
				renderer.DrawBackground(g, drawRect, clipRect);
			}
			else
			{
				// background
				g.FillRectangle(Brushes.White, drawRect);

				// 3d border
				//ControlPaint.DrawBorder3D(g, drawRect, Border3DStyle.SunkenInner);
				
				// flat border
				g.DrawRectangle(SystemPens.ControlDark, drawRect.Left, drawRect.Top, drawRect.Width-1, drawRect.Height-1);
			}
		}


		/// <summary>
		/// Draws the ProgressBar's chunks on the specified graphics surface, and within 
		/// the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="drawRect">The Rectangle that represents the dimensions 
		/// of the ProgressBar</param>
		public static void DrawProgressBarChunks(Graphics g, Rectangle drawRect)
		{
			ThemeManager.DrawProgressBarChunks(g, drawRect, drawRect);
		}


		/// <summary>
		/// Draws the ProgressBar's chunks on the specified graphics surface, and within 
		/// the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="drawRect">The Rectangle that represents the dimensions 
		/// of the ProgressBar</param>
		/// <param name="clipRect">The Rectangle that represents the clipping area</param>
		public static void DrawProgressBarChunks(Graphics g, Rectangle drawRect, Rectangle clipRect)
		{
			if (g == null || drawRect.Width <= 0 || drawRect.Height <= 0 || clipRect.Width <= 0 || clipRect.Height <= 0)
			{
				return;
			}

			if (ThemeManager.VisualStylesEnabled)
			{
				//ThemeManager.DrawThemeBackground(g, ThemeClasses.ProgressBar, (int) ProgressBarParts.Chunk, 0, drawRect, clipRect);
				VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.ProgressBar.Chunk.Normal);
				renderer.DrawBackground(g, drawRect, clipRect);
			}
			else
			{
				g.FillRectangle(SystemBrushes.Highlight, drawRect);
			}
		}

		#endregion

		#region RadioButton

		/// <summary>
		/// Draws a RadioButton in the specified state, on the specified graphics 
		/// surface, and within the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="checkRect">The Rectangle that represents the dimensions 
		/// of the RadioButton</param>
		/// <param name="state">A RadioButtonState value that specifies the 
		/// state to draw the RadioButton in</param>
		public static void DrawRadioButton(Graphics g, Rectangle checkRect, RadioButtonState state)
		{
			ThemeManager.DrawRadioButton(g, checkRect, checkRect, state);
		}


		/// <summary>
		/// Draws a RadioButton in the specified state, on the specified graphics 
		/// surface, and within the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="checkRect">The Rectangle that represents the dimensions 
		/// of the RadioButton</param>
		/// <param name="clipRect">The Rectangle that represents the clipping area</param>
		/// <param name="state">A RadioButtonState value that specifies the 
		/// state to draw the RadioButton in</param>
		public static void DrawRadioButton(Graphics g, Rectangle checkRect, Rectangle clipRect, RadioButtonState state)
		{
			if (g == null || checkRect.Width <= 0 || checkRect.Height <= 0)
			{
				return;
			}

			if (ThemeManager.VisualStylesEnabled)
			{
				//ThemeManager.DrawThemeBackground(g, ThemeClasses.Button, (int) ButtonParts.RadioButton, (int) state, checkRect, clipRect);
				VisualStyleRenderer renderer;
				switch (state)
				{
					case RadioButtonState.CheckedDisabled:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.CheckedDisabled);
						break;
					case RadioButtonState.CheckedHot:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.CheckedHot);
						break;
					case RadioButtonState.CheckedNormal:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.CheckedNormal);
						break;
					case RadioButtonState.CheckedPressed:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.CheckedPressed);
						break;
					case RadioButtonState.UncheckedDisabled:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.UncheckedDisabled);
						break;
					case RadioButtonState.UncheckedHot:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.UncheckedHot);
						break;
					case RadioButtonState.UncheckedPressed:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.UncheckedPressed);
						break;
					case RadioButtonState.UncheckedNormal:
					default:
						renderer = new VisualStyleRenderer(VisualStyleElement.Button.RadioButton.UncheckedNormal);
						break;
				}
				renderer.DrawBackground(g, checkRect, clipRect);
			}
			else
			{
				ControlPaint.DrawRadioButton(g, checkRect, ThemeManager.ConvertRadioButtonStateToButtonState(state));
			}
		}


		/// <summary>
		/// Converts the specified RadioButtonState value to a ButtonState value
		/// </summary>
		/// <param name="state">The RadioButtonState value to be converted</param>
		/// <returns>A ButtonState value that represents the specified RadioButtonState 
		/// value</returns>
		private static ButtonState ConvertRadioButtonStateToButtonState(RadioButtonState state)
		{
			switch (state)
			{
				case RadioButtonState.UncheckedPressed:
				{
					return ButtonState.Pushed;
				}

				case RadioButtonState.UncheckedDisabled:
				{
					return ButtonState.Inactive;
				}

				case RadioButtonState.CheckedNormal:
				case RadioButtonState.CheckedHot:
				{
					return ButtonState.Checked;
				}

				case RadioButtonState.CheckedPressed:
				{
					return (ButtonState.Checked | ButtonState.Pushed);
				}

				case RadioButtonState.CheckedDisabled:
				{
					return (ButtonState.Checked | ButtonState.Inactive);
				}
			}

			return ButtonState.Normal;
		}

		#endregion

		#region TabPage

		/// <summary>
		/// Draws a TabPage body on the specified graphics surface, and within the 
		/// specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="tabRect">The Rectangle that represents the dimensions 
		/// of the TabPage body</param>
		internal static void DrawTabPageBody(Graphics g, Rectangle tabRect)
		{
			ThemeManager.DrawTabPageBody(g, tabRect, tabRect);
		}

		
		/// <summary>
		/// Draws a TabPage body on the specified graphics surface, and within the 
		/// specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="tabRect">The Rectangle that represents the dimensions 
		/// of the TabPage body</param>
		/// <param name="clipRect">The Rectangle that represents the clipping area</param>
		internal static void DrawTabPageBody(Graphics g, Rectangle tabRect, Rectangle clipRect)
		{
			if (g == null || tabRect.Width <= 0 || tabRect.Height <= 0 || clipRect.Width <= 0 || clipRect.Height <= 0)
			{
				return;
			}

			if (ThemeManager.VisualStylesEnabled)
			{
				//ThemeManager.DrawThemeBackground(g, ThemeClasses.TabControl, (int) TabParts.Body, 0, tabRect, clipRect);
				VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.Tab.Body.Normal);
				renderer.DrawBackground(g, tabRect, clipRect);
			}
			else
			{
				g.FillRectangle(SystemBrushes.Control, Rectangle.Intersect(clipRect, tabRect));
			}
		}

		#endregion

		#region TextBox

		/// <summary>
		/// Draws a TextBox in the specified state, on the specified graphics 
		/// surface, and within the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="textRect">The Rectangle that represents the dimensions 
		/// of the TextBox</param>
		/// <param name="state">A TextBoxState value that specifies the 
		/// state to draw the TextBox in</param>
		public static void DrawTextBox(Graphics g, Rectangle textRect, TextBoxState state)
		{
			ThemeManager.DrawTextBox(g, textRect, textRect, state);
		}


		/// <summary>
		/// Draws a TextBox in the specified state, on the specified graphics 
		/// surface, and within the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="textRect">The Rectangle that represents the dimensions 
		/// of the TextBox</param>
		/// <param name="clipRect">The Rectangle that represents the clipping area</param>
		/// <param name="state">A TextBoxState value that specifies the 
		/// state to draw the TextBox in</param>
		public static void DrawTextBox(Graphics g, Rectangle textRect, Rectangle clipRect, TextBoxState state)
		{
			if (g == null || textRect.Width <= 0 || textRect.Height <= 0 || clipRect.Width <= 0 || clipRect.Height <= 0)
			{
				return;
			}

			if (ThemeManager.VisualStylesEnabled)
			{
				//ThemeManager.DrawThemeBackground(g, ThemeClasses.TextBox, (int) TextBoxParts.EditText, (int) state, textRect, clipRect);
				VisualStyleRenderer renderer;
				switch (state)
				{
					case TextBoxState.Disabled:
						renderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Disabled);
						break;
					case TextBoxState.Hot:
						renderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Hot);
						break;
					case TextBoxState.Readonly:
						renderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.ReadOnly);
						break;
					case TextBoxState.Selected:
						renderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Selected);
						break;
					case TextBoxState.Normal:
						renderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Normal);
						break;
					default:
						renderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Focused);
						break;
				}
				renderer.DrawBackground(g, textRect, clipRect);
			}
			else
			{
				ControlPaint.DrawBorder3D(g, textRect, Border3DStyle.Sunken);
			}
		}

		#endregion

		#region UpDown

		/// <summary>
		/// Draws an UpDown's up and down buttons in the specified state, on the specified 
		/// graphics surface, and within the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="upButtonRect">The Rectangle that represents the dimensions 
		/// of the up button</param>
		/// <param name="upButtonState">An UpDownState value that specifies the 
		/// state to draw the up button in</param>
		/// <param name="downButtonRect">The Rectangle that represents the dimensions 
		/// of the down button</param>
		/// <param name="downButtonState">An UpDownState value that specifies the 
		/// state to draw the down button in</param>
		public static void DrawUpDownButtons(Graphics g, Rectangle upButtonRect, UpDownState upButtonState, Rectangle downButtonRect, UpDownState downButtonState)
		{
			ThemeManager.DrawUpDownButtons(g, upButtonRect, upButtonRect, upButtonState, downButtonRect, downButtonRect, downButtonState);
		}


		/// <summary>
		/// Draws an UpDown's up and down buttons in the specified state, on the specified 
		/// graphics surface, and within the specified bounds
		/// </summary>
		/// <param name="g">The Graphics to draw on</param>
		/// <param name="upButtonRect">The Rectangle that represents the dimensions 
		/// of the up button</param>
		/// <param name="upButtonClipRect">The Rectangle that represents the clipping area
		/// for the up button</param>
		/// <param name="upButtonState">An UpDownState value that specifies the 
		/// state to draw the up button in</param>
		/// <param name="downButtonRect">The Rectangle that represents the dimensions 
		/// of the down button</param>
		/// <param name="downButtonClipRect">The Rectangle that represents the clipping area
		/// for the down button</param>
		/// <param name="downButtonState">An UpDownState value that specifies the 
		/// state to draw the down button in</param>
		public static void DrawUpDownButtons(Graphics g, Rectangle upButtonRect, Rectangle upButtonClipRect, UpDownState upButtonState, Rectangle downButtonRect, Rectangle downButtonClipRect, UpDownState downButtonState)
		{
			if (g == null)
			{
				return;
			}

			if (upButtonRect.Width > 0 && upButtonRect.Height > 0 && upButtonClipRect.Width > 0 && upButtonClipRect.Height > 0)
			{
				if (ThemeManager.VisualStylesEnabled)
				{
					//ThemeManager.DrawThemeBackground(g, ThemeClasses.UpDown, (int) UpDownParts.Up, (int) upButtonState, upButtonRect, upButtonClipRect);
					VisualStyleRenderer renderer;
					switch (upButtonState)
					{
						case UpDownState.Disabled:
							renderer = new VisualStyleRenderer(VisualStyleElement.Spin.Up.Disabled);
							break;
						case UpDownState.Hot:
							renderer = new VisualStyleRenderer(VisualStyleElement.Spin.Up.Hot);
							break;
						case UpDownState.Pressed:
							renderer = new VisualStyleRenderer(VisualStyleElement.Spin.Up.Pressed);
							break;
						case UpDownState.Normal:
						default:
							renderer = new VisualStyleRenderer(VisualStyleElement.Spin.Up.Normal);
							break;
					}
					renderer.DrawBackground(g, upButtonRect, upButtonClipRect);
				}
				else
				{
					ControlPaint.DrawScrollButton(g, upButtonRect, ScrollButton.Up, ThemeManager.ConvertUpDownStateToButtonState(upButtonState));
				}
			}

			if (downButtonRect.Width > 0 && downButtonRect.Height > 0 && downButtonClipRect.Width > 0 && downButtonClipRect.Height > 0)
			{
				if (ThemeManager.VisualStylesEnabled)
				{
					//ThemeManager.DrawThemeBackground(g, ThemeClasses.UpDown, (int) UpDownParts.Down, (int) downButtonState, downButtonRect, downButtonClipRect);
					VisualStyleRenderer renderer;
					switch (downButtonState)
					{
						case UpDownState.Disabled:
							renderer = new VisualStyleRenderer(VisualStyleElement.Spin.Down.Disabled);
							break;
						case UpDownState.Hot:
							renderer = new VisualStyleRenderer(VisualStyleElement.Spin.Down.Hot);
							break;
						case UpDownState.Pressed:
							renderer = new VisualStyleRenderer(VisualStyleElement.Spin.Down.Pressed);
							break;
						case UpDownState.Normal:
						default:
							renderer = new VisualStyleRenderer(VisualStyleElement.Spin.Down.Normal);
							break;
					}
					renderer.DrawBackground(g, downButtonRect, downButtonClipRect);
				}
				else
				{
					ControlPaint.DrawScrollButton(g, downButtonRect, ScrollButton.Down, ThemeManager.ConvertUpDownStateToButtonState(downButtonState));
				}
			}
		}


		/// <summary>
		/// Converts the specified UpDownState value to a ButtonState value
		/// </summary>
		/// <param name="state">The UpDownState value to be converted</param>
		/// <returns>A ButtonState value that represents the specified UpDownState 
		/// value</returns>
		private static ButtonState ConvertUpDownStateToButtonState(UpDownState state)
		{
			switch (state)
			{
				case UpDownState.Pressed:
				{
					return ButtonState.Pushed;
				}

				case UpDownState.Disabled:
				{
					return ButtonState.Inactive;
				}
			}

			return ButtonState.Normal;
		}

		#endregion

		#endregion

		#endregion


		#region Properties
		/// <summary>
		/// Gets whether Visual Styles are supported by the system
		/// </summary>
		public static bool VisualStylesSupported
		{
			get
			{
				return OSFeature.Feature.IsPresent(OSFeature.Themes);
			}
		}


		/// <summary>
		/// Gets whether Visual Styles are enabled for the application
		/// </summary>
		public static bool VisualStylesEnabled
		{
			get
			{
				if (VisualStylesSupported)
				{
					// are themes enabled
					if (VisualStyleInformation.IsSupportedByOS && VisualStyleInformation.IsEnabledByUser)
					{
						return Application.RenderWithVisualStyles;
					}
				}

				return false;
			}
		}

		#endregion
	}
}
