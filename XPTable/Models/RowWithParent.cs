using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPTable.Models
{
    public class RowWithParent : Row
    {

        public RowWithParent()
            : base()
        {
        }
        private int _childindex;
        protected override int childindex
        {
            get
            {
                return _childindex;
            }
            set
            {
                _childindex = value;
            }
        }

        private Row _parentrow;
        protected override Row parentrow
        {
            get
            {
                return _parentrow;
            }
            set
            {
                _parentrow = value;
            }
        }
    }
}
