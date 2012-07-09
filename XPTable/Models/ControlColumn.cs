using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using XPTable.Editors;
using XPTable.Events;
using XPTable.Models.Design;
using XPTable.Renderers;
using XPTable.Sorting;

namespace XPTable.Models
{

	/// <summary>
	/// Represents a Column whose Cells are displayed as a Control
	/// </summary>
	[DesignTimeVisible(false),
	ToolboxItem(false)]
	public class ControlColumn : Column
	{

        #region Class Data
        /// <summary>
        /// The factory class for creating the controls.
        /// </summary>
		private ControlFactory factory = null;

		/// <summary>
		/// The size of the control
		/// </summary>
		private Size controlSize;

		#endregion
		
        
   		#region Constructor
		
		/// <summary>
		/// Creates a new ControlColumn with default values
		/// </summary>
		public ControlColumn() : base()
		{
			this.Init();
		}

        /// <summary>
        /// Creates a new ControlColumn with the specified header text and width
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="width">The column's width</param>
        public ControlColumn(string text, int width)
            : base(text, width)
        {
            this.Init();
        }

        /// <summary>
        /// Creates a new ControlColumn with the specified header text, image and width
        /// </summary>
        /// <param name="text">The text displayed in the column's header</param>
        /// <param name="image">The image displayed on the column's header</param>
        /// <param name="width">The column's width</param>
        public ControlColumn(string text, Image image, int width)
            : base(text, image, width)
        {
            this.Init();
        }

		/// <summary>
		/// Creates a new ControlColumn with the specified header width
		/// </summary>
		/// <param name="width">The column's width</param>
		public ControlColumn(int width) : base("", width)
		{
			this.Init();
		}

		/// <summary>
		/// Initializes the ControlColumn with default values
		/// </summary>
		private void Init()
		{
			this.controlSize = new Size(this.Width, 13);
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
			return "CONTROL";
		}


		/// <summary>
		/// Gets the Column's default CellRenderer
		/// </summary>
		/// <returns>The Column's default CellRenderer</returns>
		public override ICellRenderer CreateDefaultRenderer()
		{
			return new ControlCellRenderer();
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
		/// Gets or sets the size of the controls
		/// </summary>
		[Category("Appearance"),
		Description("Specifies the size of the controls")]
		public Size ControlSize
		{
			get
			{
				return this.controlSize;
			}

			set
			{
				if (value.Width < 0 || value.Height < 0)
				{
					value = new Size(13, 13);
				}
				
				if (this.controlSize != value)
				{
					this.controlSize = value;

					this.OnPropertyChanged(new ColumnEventArgs(this, ColumnEventType.RendererChanged, null));
				}
			}
		}


		/// <summary>
		/// Specifies whether the ControlSize property should be serialized at 
		/// design time
		/// </summary>
		/// <returns>true if the ControlSize property should be serialized, 
		/// false otherwise</returns>
		private bool ShouldSerializeControlSize()
		{
			return (this.controlSize.Width != 13 || this.controlSize.Height != 13);
		}

		/// <summary>
		/// Gets the Type of the Comparer used to compare the Column's Cells when 
		/// the Column is sorting
		/// </summary>
		public override Type DefaultComparerType
		{
			get
			{
				return null;
			}
		}

        /// <summary>
        /// Gets or sets the factory class for creating the controls.
        /// </summary>
        public ControlFactory ControlFactory
        {
            get { return this.factory; }
            set { this.factory = value; }
        }
		#endregion

	}
}
