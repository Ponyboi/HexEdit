using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestingProject.Models
{
    public class HexNode
    {
        public string hex;
        public string propName;
        public string fileName;
        public int lineNumber;


        public HexNode(string hex, string propName, string fileName, int lineNumber)
        {
            this.hex = hex;
            this.propName = propName;
            this.fileName = fileName;
            this.lineNumber = lineNumber;
        }

        public string ToString() {
            return String.Format("{0}, {1}, {2}", fileName, lineNumber, hex); 
        }
    }
}