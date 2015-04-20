using ExCSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HexEdit.Models
{
    public class HexNode
    {
        public string hex;
        public string propName;
        public string fileName;
        public int lineNumber;
        public Property property;

        public HexNode(string hex, string propName, string fileName, int lineNumber, Property property)
        {
            this.hex = hex;
            this.propName = propName;
            this.fileName = fileName;
            this.lineNumber = lineNumber;
            this.property = property;
        }

        public string ToString() {
            return String.Format("{0}, {1}, {2}", fileName, lineNumber, hex); 
        }
    }
}