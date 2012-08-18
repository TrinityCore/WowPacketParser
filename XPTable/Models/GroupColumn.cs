using System;
using System.ComponentModel;
using System.Drawing;

using XPTable.Editors;
using XPTable.Events;
using XPTable.Models.Design;
using XPTable.Renderers;
using XPTable.Sorting;


namespace XPTable.Models
{
	/// <summary>
	/// Represents a Column whose Cells are displayed as a collapse/expand icon.
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public class GroupColumn : Column
	{

		#region Class Data

		/// <summary>
		/// Specifies whether any text contained in the Cell should be drawn
		/// </summary>
		private bool drawText;

		/// <summary>
		/// Specifies the colour of the box and connecting lines
		/// </summary>
		private Color lineColor;

		/// <summary>
		/// Determies whether the collapse/expand is performed on the Click event. If false then Double Click toggles the state.
		/// </summary>
		private bool toggleOnSingleClick = false;

		#endregion


		#region Constructor

		/// <summary>
		/// Creates a new GroupColumn with default values
		/// </summary>
		public GroupColumn() : base()
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new GroupColumn with the specified header text
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		public GroupColumn(string text) : base(text)
		{
			this.Init();
		}


		/// <summary>
		/// Creates a new GroupColumn with the specified header text and width
		/// </summary>
		/// <param name="text">The text displayed in the column's header</param>
		/// <param name="width">The column's width</param>
		public GroupColumn(string text, int width) : base(text, width)
		{
			this.Init();
		}

		/// <summary>
		/// Initializes the GroupColumn with default values
		/// </summary>
		private void Init()
		{
			this.drawText = true;
			this.Editable = false;
			this.lineColor = Color.FromArgb(195, 218, 249);
		}

		#endregion


		#region Methods

		/// <summary>
		/// Gets a string that specifies the name of the Column's default CellRenderer
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellRenderer</returns>
		public override string GetDefaultRendererName()
		{
			return "GROUP";
		}


		/// <summary>
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public override ICellRenderer CreateDefaultRenderer()
		{
			return new GroupCellRenderer();
		}


		/// <summary>
		/// Gets a string that specifies the name of the Column's default CellEditor
		/// </summary>
		/// <returns>A string that specifies the name of the Column's default 
		/// CellEditor</returns>
		public override string GetDefaultEditorName()
		{
			return null;
		}


		/// <summary>
		/// Gets the Column's default CellEditor
		/// </summary>
		/// <returns>The Column's default CellEditor</returns>
		public override ICellEditor CreateDefaultEditor()
		{
			return null;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets whether any text contained in the Column's Cells should be drawn
		/// </summary>
		[Category("Appearance"),
		DefaultValue(true),
		Description("Determines whether any text contained in the Column's Cells should be drawn")]
		public bool DrawText
		{
			get
			{
				return this.drawText;
			}

			set
			{
				if (this.drawText != value)
				{
					this.drawText = value;

					this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.RendererChanged, null));
				}
			}
		}

		/// <summary>
		/// Gets the Type of the Comparer used to compare the Column's Cells when 
		/// the Column is sorting
		/// </summary>
		public override Type DefaultComparerType
		{
			get
			{
				return typeof(TextComparer);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the Column's Cells contents 
		/// are able to be edited
		/// </summary>
		[Category("Appearance"),
		DefaultValue(false),
		Description("Controls whether the column's cell contents are able to be changed by the user")]
		public new bool Editable
		{
			get
			{
				return base.Editable;
			}

			set
			{
				base.Editable = value;
			}
		}

		/// <summary>
		/// Specifies the colour of the box and connecting lines.
		/// </summary>
		[Category("Appearance"),
		Description("Specifies the colour of the box and connecting lines")]
		public Color LineColor
		{
			get { return lineColor; }
			set { lineColor = value; }
		}

		/// <summary>
		/// Gets or sets whether the collapse/expand is performed on the Click event. If false then Double Click toggles the state.
		/// </summary>
		[Category("Behaviour"),
		DefaultValue(false),
		Description("Controls whether the collapse/expand is performed on the Click event. If false then Double Click toggles the state")]
		public bool ToggleOnSingleClick
		{
			get { return toggleOnSingleClick; }
			set { toggleOnSingleClick = value; }
		}

		#endregion


	}
}
