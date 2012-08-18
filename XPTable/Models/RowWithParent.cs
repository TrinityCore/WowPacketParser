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
        //private sbyte _childindex;
        protected override int childindex
        {
            get
            {
                //HACK
                return 1;//_childindex;
            }
            set
            {
                //_childindex = value;
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
