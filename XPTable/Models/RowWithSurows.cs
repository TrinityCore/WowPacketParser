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
        private RowCollection _subrows;
        protected override RowCollection subrows
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
    }
}
