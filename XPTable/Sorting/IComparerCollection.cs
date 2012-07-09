using System;
using System.Collections;
using System.Text;

namespace XPTable.Sorting
{
    /// <summary>
    /// Represents a collection of IComparer objects
    /// </summary>
    public class IComparerCollection : CollectionBase
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the IComparerCollection class 
        /// </summary>
        public IComparerCollection()
            : base()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified IComparer to the end of the collection
        /// </summary>
        /// <param name="comparer">The IComparer to add</param>
        public int Add(IComparer comparer)
        {
            if (comparer == null)
            {
                throw new System.ArgumentNullException("comparer is null");
            }

            int index = this.List.Add(comparer);

            return index;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the IComparer at the specified index
        /// </summary>
        public IComparer this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                {
                    return null;
                }

                return this.List[index] as IComparer;
            }
        }
        #endregion
    }
}
