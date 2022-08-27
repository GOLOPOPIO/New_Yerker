using System;
using System.Collections.Generic;
using System.Text;

namespace New_Yerker.Model.DataModel
{
    public class Size
    {
        public string SizeName { get; set; }
        public string sizeCode = "";
        public string code = "";
        public string BaraCode
        {
            get { return code + sizeCode; }
            set
            {
                var c = value.ToCharArray();
                for (int i = 0; i < 16; i++)
                {
                    if (i < 12)
                        code += c[i];
                    if (i >= 12)
                        sizeCode += c[i];
                }
            }
        }
    }
}
