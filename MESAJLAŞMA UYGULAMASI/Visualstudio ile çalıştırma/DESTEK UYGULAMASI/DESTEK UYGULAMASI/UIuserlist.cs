using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DESTEK_UYGULAMASI.Properties;

namespace DESTEK_UYGULAMASI
{
    internal class UIuserlist
    {
        public static int GetTextHeight(Label lbl)
        {
            using(Graphics g = lbl.CreateGraphics())
            {
                SizeF size = g.MeasureString(lbl.Text, lbl.Font, 495);
                return(int)Math.Ceiling(size.Height);
            }
        }
    }
}
