using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPTable.Models
{
    public class CellText : Cell
    {
        string _text;
        public CellText(string text)
            : base(text)
        {
        }
        protected override string text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }
    }
}
