using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEppAssignment3
{
    class Tile : Button
    {
        
        public Tile(int height, int width, int top, int left)
        {
            this.Height = height;
            this.Width = width;
            this.Top = top;
            this.Left = left;
        }
    }
}
