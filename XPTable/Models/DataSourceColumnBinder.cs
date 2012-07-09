using System;
using System.ComponentModel;
using System.Text;

namespace XPTable.Models
{
    /// <summary>
    /// Binder that creates the appropriate type of Column for a given column in a DataSource.
    /// </summary>
    public class DataSourceColumnBinder
    {
        /// <summary>
        /// Creates a DataSourceColumnBinder with default values.
        /// </summary>
        public DataSourceColumnBinder()
        {
        }

        /// <summary>
        /// Returns the ColumnModel to use for the given fields from the datasource.
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public virtual ColumnModel GetColumnModel(PropertyDescriptorCollection properties)
        {
            ColumnModel columns = new ColumnModel();
            int index = 0;
            foreach (PropertyDescriptor prop in properties)
            {
                Column column = GetColumn(prop, index);
                columns.Columns.Add(column);
                index++;
            }
            return columns;
        }

        /// <summary>
        /// Returns the type of column that is appropriate for the given property of the data source.
        /// Numbers, DateTime, Color and Boolean columns are mapped to NumberColumn, DateTimeColumn, ColorColumn and CheckBoxColumn respectively. The default
        /// is just a TextColumn.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual Column GetColumn(PropertyDescriptor prop, int index)
        {
            NumberColumn numCol = null;
            Column column = null;
            switch (prop.PropertyType.Name)
            {
                case "Int32":
                    numCol = new NumberColumn(prop.Name);
                    numCol.Maximum = Int32.MaxValue;
                    numCol.Minimum = Int32.MinValue;
                    column = numCol;
                    break;
                case "Double":
                    numCol = new NumberColumn(prop.Name);
                    numCol.Maximum = Decimal.MaxValue;
                    numCol.Minimum = Decimal.MinValue;
                    column = numCol;
                    break;
                case "Float":
                    numCol = new NumberColumn(prop.Name);
                    numCol.Maximum = Convert.ToDecimal(float.MaxValue);
                    numCol.Minimum = Convert.ToDecimal(float.MinValue);
                    column = numCol;
                    break;
                case "Int16":
                    numCol = new NumberColumn(prop.Name);
                    numCol.Maximum = Int16.MaxValue;
                    numCol.Minimum = Int16.MinValue;
                    column = numCol;
                    break;
                case "Int64":
                    numCol = new NumberColumn(prop.Name);
                    numCol.Maximum = Int64.MaxValue;
                    numCol.Minimum = Int64.MinValue;
                    column = numCol;
                    break;
                case "Decimal":
                    numCol = new NumberColumn(prop.Name);
                    numCol.Maximum = Decimal.MaxValue;
                    numCol.Minimum = Decimal.MinValue;
                    column = numCol;
                    break;

                case "DateTime":
                    column = new DateTimeColumn(prop.Name);
                    break;

                case "Color":
                    column = new ColorColumn(prop.Name);
                    break;

                case "Boolean":
                    column = new CheckBoxColumn(prop.Name);
                    break;

                default:
                    column = new TextColumn(prop.Name);
                    break;
            }
            return column;
        }

		/// <summary>
		/// Returns the cell to add to a row for the given value, depending on the type of column it will be 
		/// shown in.
		/// If the column is a TextColumn then just the Text property is set. For all other
		/// column types just the Data value is set.
		/// </summary>
		/// <param name="column"></param>
		/// <param name="val"></param>
		/// <returns></returns>
		public virtual Cell GetCell(Column column, object val)
		{
			Cell cell = null;

			switch (column.GetType().Name)
			{
				case "TextColumn":
					cell = new Cell(val.ToString());
					break;

				case "CheckBoxColumn":
					bool check = false;
					if (val is Boolean)
						check = (bool)val;
					cell = new Cell("", check);
					break;

				default:
					cell = new Cell(val);
					break;
			}

			return cell;
		}
    }
}
