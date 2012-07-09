using System;
using System.Collections;
using System.Text;

namespace XPTable.Sorting
{
    /// <summary>
    /// Represents a collection of SortColumn objects
    /// </summary>
    public class SortColumnCollection : CollectionBase
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the SortColumnCollection class 
        /// </summary>
        public SortColumnCollection()
            : base()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the specified SortColumn to the end of the collection
        /// </summary>
        /// <param name="sortColumn">The SortColumn to add</param>
        public int Add(SortColumn sortColumn)
        {
            if (sortColumn == null)
            {
                throw new System.ArgumentNullException("SortColumn is null");
            }

            int index = this.List.Add(sortColumn);

            return index;
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets the Cell at the specified index
        /// </summary>
        public SortColumn this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                {
                    return null;
                }

                return this.List[index] as SortColumn;
            }
        }

        #endregion
    }
}
