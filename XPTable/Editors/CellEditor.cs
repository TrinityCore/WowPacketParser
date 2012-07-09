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
using System.Windows.Forms.Design;

using XPTable.Events;
using XPTable.Models;
using XPTable.Win32;


namespace XPTable.Editors
{
	/// <summary>
	/// Base class for Cell editors
	/// </summary>
	public abstract class CellEditor : ICellEditor, IMouseMessageFilterClient, IKeyMessageFilterClient
	{
		#region Event Handlers

		/// <summary>
		/// Occurs when the CellEditor begins editing a Cell
		/// </summary>
		public event CellEditEventHandler BeginEdit;

		/// <summary>
		/// Occurs when the CellEditor stops editing a Cell
		/// </summary>
		public event CellEditEventHandler EndEdit;

		/// <summary>
		/// Occurs when the editing of a Cell is cancelled
		/// </summary>
		public event CellEditEventHandler CancelEdit;

		#endregion


		#region Class Data

		/// <summary>
		/// The Control that is performing the editing
		/// </summary>
		private Control control;

		/// <summary>
		/// The Cell that is being edited
		/// </summary>
		internal Cell cell;

		/// <summary>
		/// The Table that contains the Cell being edited
		/// </summary>
		protected Table table;

		/// <summary>
		/// A CellPos that represents the position of the Cell being edited
		/// </summary>
		protected CellPos cellPos;

		/// <summary>
		/// The Rectangle that represents the Cells location and size
		/// </summary>
		protected Rectangle cellRect;

		/// <summary>
		/// A MouseMessageFilter that receives mouse messages before they 
		/// are dispatched to their destination
		/// </summary>
		private MouseMessageFilter mouseMessageFilter;

		/// <summary>
		/// A KeyMessageFilter that receives key messages before they 
		/// are dispatched to their destination
		/// </summary>
		private KeyMessageFilter keyMessageFilter;

		#endregion
		
		
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the CellEditor class with default settings
		/// </summary>
		protected CellEditor()
		{
			this.control = null;
			this.cell = null;
			this.table = null;
			this.cellPos = CellPos.Empty;
			this.cellRect = Rectangle.Empty;

			this.mouseMessageFilter = new MouseMessageFilter(this);
			this.keyMessageFilter = new KeyMessageFilter(this);
		}

		#endregion


		#region Methods

		/// <summary>
		/// Prepares the CellEditor to edit the specified Cell
		/// </summary>
		/// <param name="cell">The Cell to be edited</param>
		/// <param name="table">The Table that contains the Cell</param>
		/// <param name="cellPos">A CellPos representing the position of the Cell</param>
		/// <param name="cellRect">The Rectangle that represents the Cells location and size</param>
		/// <param name="userSetEditorValues">Specifies whether the ICellEditors 
		/// starting value has already been set by the user</param>
		/// <returns>true if the ICellEditor can continue editing the Cell, false otherwise</returns>
		public virtual bool PrepareForEditing(Cell cell, Table table, CellPos cellPos, Rectangle cellRect, bool userSetEditorValues)
		{
			this.cell = cell;
			this.table = table;
			this.cellPos = cellPos;
			this.cellRect = cellRect;

			// check if the user has already set the editors value for us
			if (!userSetEditorValues)
				this.SetEditValue();

			this.SetEditLocation(cellRect);

			// raise the BeginEdit event
			CellEditEventArgs e = new CellEditEventArgs(cell, this, table, cellPos.Row, cellPos.Column, cellRect);
			e.Handled = userSetEditorValues;

			this.OnBeginEdit(e);
			
			// if the edit has been canceled, remove the editor and return false
            if (e.Cancel)
            {
                this.RemoveEditControl();
                return false;
            }
            else
            {
                return true;
            }
		}

		/// <summary>
		/// Sets the location and size of the CellEditor
		/// </summary>
		/// <param name="cellRect">A Rectangle that represents the size and location 
		/// of the Cell being edited</param>
		protected abstract void SetEditLocation(Rectangle cellRect);


		/// <summary>
		/// Sets the initial value of the editor based on the contents of 
		/// the Cell being edited
		/// </summary>
		protected abstract void SetEditValue();


		/// <summary>
		/// Sets the contents of the Cell being edited based on the value 
		/// in the editor
		/// </summary>
		protected abstract void SetCellValue();


		/// <summary>
		/// Displays the editor to the user and adds it to the Table's Control
		/// collection
		/// </summary>
		protected virtual void ShowEditControl()
		{
			this.control.Parent = this.table;

			this.control.Visible = true;
		}


		/// <summary>
		/// Conceals the editor from the user, but does not remove it from the 
		/// Table's Control collection
		/// </summary>
		protected virtual void HideEditControl()
		{
			this.control.Visible = false;
		}


		/// <summary>
		/// Conceals the editor from the user and removes it from the Table's 
		/// Control collection
		/// </summary>
		protected virtual void RemoveEditControl()
		{
			this.control.Visible = false;
			this.control.Parent = null;

			this.table.Focus();

			this.cell = null;
			this.table = null;
			this.cellPos = CellPos.Empty;
			this.cellRect = Rectangle.Empty;
		}


		/// <summary>
		/// Starts editing the Cell
		/// </summary>
		public virtual void StartEditing()
		{
			this.ShowEditControl();

			Application.AddMessageFilter(this.keyMessageFilter);
			Application.AddMessageFilter(this.mouseMessageFilter);
		}


		/// <summary>
		/// Stops editing the Cell and commits any changes
		/// </summary>
		public virtual void StopEditing()
		{
			Application.RemoveMessageFilter(this.keyMessageFilter);
			Application.RemoveMessageFilter(this.mouseMessageFilter);
			
			//
			CellEditEventArgs e = new CellEditEventArgs(this.cell, this, this.table, this.cellPos.Row, this.cellPos.Column, this.cellRect);

			this.table.OnEditingStopped(e);
			this.OnEndEdit(e);
			
			if (!e.Cancel && !e.Handled)
			{
				this.SetCellValue();
			}

			this.RemoveEditControl();
		}


		/// <summary>
		/// Stops editing the Cell and ignores any changes
		/// </summary>
		public virtual void CancelEditing()
		{
			Application.RemoveMessageFilter(this.keyMessageFilter);
			Application.RemoveMessageFilter(this.mouseMessageFilter);
			
			//
			CellEditEventArgs e = new CellEditEventArgs(this.cell, this, this.table, this.cellPos.Row, this.cellPos.Column, this.cellRect);

			this.table.OnEditingCancelled(e);
			this.OnCancelEdit(e);
			
			this.RemoveEditControl();
		}


		/// <summary>
		/// Filters out a mouse message before it is dispatched
		/// </summary>
		/// <param name="target">The Control that will receive the message</param>
		/// <param name="msg">A WindowMessage that represents the message to process</param>
		/// <param name="wParam">Specifies the WParam field of the message</param>
		/// <param name="lParam">Specifies the LParam field of the message</param>
		/// <returns>true to filter the message and prevent it from being dispatched; 
		/// false to allow the message to continue to the next filter or control</returns>
		public virtual bool ProcessMouseMessage(Control target, WindowMessage msg, int wParam, int lParam)
		{
			if (msg == WindowMessage.WM_LBUTTONDOWN || msg == WindowMessage.WM_RBUTTONDOWN || 
				msg == WindowMessage.WM_MBUTTONDOWN || msg == WindowMessage.WM_XBUTTONDOWN || 
				msg == WindowMessage.WM_NCLBUTTONDOWN || msg == WindowMessage.WM_NCRBUTTONDOWN || 
				msg == WindowMessage.WM_NCMBUTTONDOWN || msg == WindowMessage.WM_NCXBUTTONDOWN)
			{	
				Point cursorPos = Cursor.Position;
				
				if (target != this.EditingTable && target != this.Control)
				{
					this.EditingTable.StopEditing();
				}
			}
			
			return false;
		}


		/// <summary>
		/// Filters out a key message before it is dispatched
		/// </summary>
		/// <param name="target">The Control that will receive the message</param>
		/// <param name="msg">A WindowMessage that represents the message to process</param>
		/// <param name="wParam">Specifies the WParam field of the message</param>
		/// <param name="lParam">Specifies the LParam field of the message</param>
		/// <returns>true to filter the message and prevent it from being dispatched; 
		/// false to allow the message to continue to the next filter or control</returns>
		public virtual bool ProcessKeyMessage(Control target, WindowMessage msg, long wParam, long lParam)
		{
			return false;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the Control that is being used to edit the Cell
		/// </summary>
		protected Control Control
		{
			get
			{
				return this.control;
			}

			set
			{
				this.control = value;
			}
		}


		/// <summary>
		/// Gets the Cell that is being edited
		/// </summary>
		public Cell EditingCell
		{
			get
			{
				return this.cell;
			}
		}


		/// <summary>
		/// Gets the Table that contains the Cell being edited
		/// </summary>
		public Table EditingTable
		{
			get
			{
				return this.table;
			}
		}


		/// <summary>
		/// Gets a CellPos that represents the position of the Cell being edited
		/// </summary>
		public CellPos EditingCellPos
		{
			get
			{
				return this.cellPos;
			}
		}


		/// <summary>
		/// Gets whether the CellEditor is currently editing a Cell
		/// </summary>
		public bool IsEditing
		{
			get
			{
				return this.cell != null;
			}
		}

		#endregion


		#region Events

		/// <summary>
		/// Raises the BeginEdit event
		/// </summary>
		/// <param name="e">A CellEditEventArgs that contains the event data</param>
		protected virtual void OnBeginEdit(CellEditEventArgs e)
		{
			if (this.BeginEdit != null)
			{
				this.BeginEdit(this, e);
			}
		}


		/// <summary>
		/// Raises the EndEdit event
		/// </summary>
		/// <param name="e">A CellEditEventArgs that contains the event data</param>
		protected virtual void OnEndEdit(CellEditEventArgs e)
		{
			if (this.EndEdit != null)
			{
				this.EndEdit(this, e);
			}
		}


		/// <summary>
		/// Raises the CancelEdit event
		/// </summary>
		/// <param name="e">A CellEditEventArgs that contains the event data</param>
		protected virtual void OnCancelEdit(CellEditEventArgs e)
		{
			if (this.CancelEdit != null)
			{
				this.CancelEdit(this, e);
			}
		}

		#endregion
	}
}
