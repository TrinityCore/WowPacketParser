using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPTable.Models
{
    public class CellData : Cell
    {
        object _data;
        public CellData(object data)
            : base(data)
        {
        }
        protected override object data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }
    }
}
