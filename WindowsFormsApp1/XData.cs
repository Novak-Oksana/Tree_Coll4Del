using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree_Coll;

namespace WindowsFormsApp1
{
    public class XData : BsTree
    {
        public int[] Tree = new int[] { 50, 25, 70, 11, 34, 18, 6, 99, 120, 81};

        public int left = 0;
        public int right = 0;
        public int dy = 0;
        public int level = 0;
        public int xp = 0; 
        public int yp = 0; 
    }
}
