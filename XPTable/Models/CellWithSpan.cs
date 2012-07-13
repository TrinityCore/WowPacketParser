using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPTable.Models
{
    public class CellWithSpan : Cell
    {
        public CellWithSpan()
            : base()
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
