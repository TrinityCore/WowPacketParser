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
using System.Windows.Forms;

using XPTable.Models;
using XPTable.Renderers;


namespace XPTable.Editors
{
	/// <summary>
	/// A class for editing Cells that contain DateTimes
	/// </summary>
	public class DateTimeCellEditor : DropDownCellEditor
	{
		#region EventHandlers

		/// <summary>
		/// Occurs when the user makes an explicit date selection using the mouse
		/// </summary>
		public event DateRangeEventHandler DateSelected;

		#endregion
		
		
		#region Class Data

		/// <summary>
		/// The MonthCalendar that will be shown in the drop-down portion of the 
		/// DateTimeCellEditor
		/// </summary>
		private MonthCalendar calendar;

		#endregion


		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the DateTimeCellEditor class with default settings
		/// </summary>
		public DateTimeCellEditor() : base()
		{
			this.calendar = new MonthCalendar();
			this.calendar.Location = new System.Drawing.Point(0, 0);
			this.calendar.MaxSelectionCount = 1;

			this.DropDown.Width = this.calendar.Width + 2;
			this.DropDown.Height = this.calendar.Height + 2;
			this.DropDown.Control = this.calendar;

			base.DropDownStyle = DropDownStyle.DropDownList;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Sets the location and size of the CellEditor
		/// </summary>
		/// <param name="cellRect">A Rectangle that represents the size and location 
		/// of the Cell being edited</param>
		protected override void SetEditLocation(Rectangle cellRect)
		{
			// calc the size of the textbox
			ICellRenderer renderer = this.EditingTable.ColumnModel.GetCellRenderer(this.EditingCellPos.Column);
			int buttonWidth = ((DateTimeCellRenderer) renderer).ButtonWidth;

			this.TextBox.Size = new Size(cellRect.Width - 1 - buttonWidth, cellRect.Height-1);
			this.TextBox.Location = cellRect.Location;
		}


		/// <summary>
		/// Sets the initial value of the editor based on the contents of 
		/// the Cell being edited
		/// </summary>
		protected override void SetEditValue()
		{
			// set default values incase we can't find what we're looking for
			DateTime date = DateTime.Now;
			String format = DateTimeColumn.LongDateFormat;
			
			if (this.EditingCell.Data != null && this.EditingCell.Data is DateTime)
			{
				date = (DateTime) this.EditingCell.Data;

				if (this.EditingTable.ColumnModel.Columns[this.EditingCellPos.Column] is DateTimeColumn)
				{
					DateTimeColumn dtCol = (DateTimeColumn) this.EditingTable.ColumnModel.Columns[this.EditingCellPos.Column];
					
					switch (dtCol.DateTimeFormat)
					{
						case DateTimePickerFormat.Short:	
							format = DateTimeColumn.ShortDateFormat;
							break;

						case DateTimePickerFormat.Time:	
							format = DateTimeColumn.TimeFormat;
							break;

						case DateTimePickerFormat.Custom:	
							format = dtCol.CustomDateTimeFormat;
							break;
					}
				}
			}
				
			this.calendar.SelectionStart = date;
			this.TextBox.Text = date.ToString(format);
		}


		/// <summary>
		/// Sets the contents of the Cell being edited based on the value 
		/// in the editor
		/// </summary>
		protected override void SetCellValue()
		{
			this.EditingCell.Data = this.calendar.SelectionStart;
		}


		/// <summary>
		/// Starts editing the Cell
		/// </summary>
		public override void StartEditing()
		{
			this.calendar.DateSelected += new DateRangeEventHandler(calendar_DateSelected);

			this.TextBox.SelectionLength = 0;
			
			base.StartEditing();
		}


		/// <summary>
		/// Stops editing the Cell and commits any changes
		/// </summary>
		public override void StopEditing()
		{
			this.calendar.DateSelected -= new DateRangeEventHandler(calendar_DateSelected);
			
			base.StopEditing();
		}


		/// <summary>
		/// Stops editing the Cell and ignores any changes
		/// </summary>
		public override void CancelEditing()
		{
			this.calendar.DateSelected -= new DateRangeEventHandler(calendar_DateSelected);
			
			base.CancelEditing();
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets a value specifying the style of the drop down editor
		/// </summary>
		public new DropDownStyle DropDownStyle
		{
			get
			{
				return base.DropDownStyle;
			}

			set
			{
				throw new NotSupportedException();
			}
		}

		#endregion


		#region Events

		/// <summary>
		/// Raises the DateSelected event
		/// </summary>
		/// <param name="e">A DateRangeEventArgs that contains the event data</param>
		protected virtual void OnDateSelected(DateRangeEventArgs e)
		{
			if (DateSelected != null)
			{
				DateSelected(this, e);
			}
		}


		/// <summary>
		/// Handler for the editors MonthCalendar.DateSelected events
		/// </summary>
		/// <param name="sender">The object that raised the event</param>
		/// <param name="e">A DateRangeEventArgs that contains the event data</param>
		private void calendar_DateSelected(object sender, DateRangeEventArgs e)
		{
			this.DroppedDown = false;

			this.OnDateSelected(e);

			this.EditingTable.StopEditing();
		}

		#endregion
	}
}
