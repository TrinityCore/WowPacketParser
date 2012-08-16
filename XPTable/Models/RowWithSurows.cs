using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPTable.Models
{
    public class RowWithSubrows : Row
    {
        public RowWithSubrows()
            : base()
        {
            InitSubRows();
        }
        private RowCollectionForParentRow _subrows;
        protected override RowCollectionForParentRow subrows
        {
            get
            {
                return _subrows;
            }
            set
            {
                _subrows = value;
            }
        }

        object _tag;
        protected override object tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }
    }
}
