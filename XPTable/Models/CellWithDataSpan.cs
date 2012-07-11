using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPTable.Models
{
    public class CellWithDataSpan : CellWithData
    {
        public CellWithDataSpan(object data)
            : base(data)
        {
        }

        private short _colspan;

        protected override int colspan
        {
            get
            {
                return (int)_colspan;
            }
            set
            {
                _colspan = (short)value;
            }
        }
    }
}
