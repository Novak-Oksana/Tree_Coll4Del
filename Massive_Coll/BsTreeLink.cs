using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_Coll
{
    public class BsTreeLink : ITree
    {
        public class Node
        {
            public int val;
            public Link left = new Link();
            public Link right = new Link();
            public Node(int val)
            {
                this.val = val;
            }
        }

        public class Link
        {
            public Node nod;
        }

        public Link root = new Link();

        public void Init(int[] ini)
        {

            if (ini == null)
                return;
            Clear();
            for (int i = 0; i < ini.Length; i++)
            {
                Add(ini[i]);
            }
        }

        public void Clear()
        {
            root = new Link();
        }

        #region Add
        public void Add(int val)
        {
            if (root.nod == null)
            {
                root.nod = new Node(val);
            }
            else
                AddNode(root, val);
        }

        private void AddNode(Link lnk, int val)
        {
            if (lnk.nod == null)
            {
                lnk.nod = new Node(val);
                return;
            }

            if (val < lnk.nod.val)
            {
                if (lnk.nod.left.nod == null)
                    lnk.nod.left.nod = new Node(val);
                else
                    AddNode(lnk.nod.left, val);
            }
            else if (val > lnk.nod.val)
            {
                if (lnk.nod.right.nod == null)
                    lnk.nod.right.nod = new Node(val);
                else
                    AddNode(lnk.nod.right, val);
            }

        }
        #endregion

        #region Size
        public int Size()
        {
            return GetSize(root);
        }
        private int GetSize(Link lnk)
        {
            if (lnk.nod == null)
                return 0;
            int size = 0;
            size += GetSize(lnk.nod.left);
            size++;
            size += GetSize(lnk.nod.right);
            return size;
        }
        #endregion

        #region Del
        public void DelUpRight(int val)
        {
            Link ff = FindNode(root, val);
            if (ff.nod == null)
                throw new ValueNotFoundEx();
            
            DeleteNodeUpRight(ff);
        }

        public void DelUpLeft(int val)
        {
            Link ff = FindNode(root, val);
            if (ff.nod == null)
                throw new ValueNotFoundEx();
            
            DeleteNodeUpLeft(ff);
        }

        private Link FindNode(Link lnk, int val)
        {
            if (lnk.nod == null)
                throw new EmptyTreeEx();
//
            if (lnk.nod == null || val == lnk.nod.val)
                return lnk;
            if (val == lnk.nod.val)
                return lnk;
            if (val < lnk.nod.val)
            {
                return FindNode(lnk.nod.left, val);
            }
            else
            {
                return FindNode(lnk.nod.right, val);
            }
        }
        private void DeleteNodeUpRight(Link lnk)//подъём справа
        {
            if (lnk.nod.right.nod == null)
            {
                lnk.nod = lnk.nod.left.nod;
                return;
            }
            else
            {
                Link rmin = FindMinRight(lnk.nod.right);
                Node p = rmin.nod;
                rmin.nod = rmin.nod.right.nod;
                p.left.nod = lnk.nod.left.nod;
                p.right.nod = lnk.nod.right.nod;
                lnk.nod = p;
            }
            /*
            Link rmin = FindMinRight(lnk.nod.right);
            if (rmin.nod == null)
            {
                lnk.nod = lnk.nod.left.nod;
                return;
            }
            Node p = rmin.nod;
            rmin.nod = rmin.nod.right.nod;
            p.left.nod = lnk.nod.left.nod;
            p.right.nod = lnk.nod.right.nod;
            lnk.nod = p;*/
        }

        private void DeleteNodeUpLeft(Link lnk)//подъём слева
        {
            if (lnk.nod.left.nod == null)
            {
                lnk.nod = lnk.nod.right.nod;
                return;
            }
            else
            {
                Link rmax = FindMaxLeft(lnk.nod.left);
                Node p = rmax.nod;
                rmax.nod = rmax.nod.left.nod;
                p.left.nod = lnk.nod.left.nod;
                p.right.nod = lnk.nod.right.nod;
                lnk.nod = p;
            }
            /*
            Link rmax = FindMaxLeft(lnk.nod.left);
            if (rmax.nod == null)
            {
                lnk.nod = lnk.nod.right.nod;
                return;
            }
            Node p = rmax.nod;
            rmax.nod = rmax.nod.left.nod;
            p.left.nod = lnk.nod.left.nod;
            p.right.nod = lnk.nod.right.nod;
            lnk.nod = p;*/
        }

        public void DelRotRight(int val)//вращение справа
        {
            Link lnk = FindNode(root, val);
            if (lnk.nod == null)
                return;
            DeleteNodeRotRight(lnk);
        }

        public void DelRotLeft(int val)//вращенение слева
        {
            Link lnk = FindNode(root, val);
            if (lnk.nod == null)
                return;
            DeleteNodeRotLeft(lnk);
        }

        private void DeleteNodeRotRight(Link lnk)//вращение справа
        {
            if (lnk.nod.right.nod == null)
            {
                lnk.nod = lnk.nod.left.nod;
                return;
            }
            Node p = lnk.nod.left.nod;
            lnk.nod = lnk.nod.right.nod;
            Link rmin = FindMinRight(lnk);
            rmin.nod.left.nod = p;
        }

        private void DeleteNodeRotLeft(Link lnk)//вращение слева
        {
            if (lnk.nod.left.nod == null)
            {
                lnk.nod = lnk.nod.right.nod;
                return;
            }
            Node p = lnk.nod.right.nod;
            lnk.nod = lnk.nod.left.nod;
            Link rmax = FindMaxLeft(lnk);
            rmax.nod.right.nod = p;
        }

        private Link FindMinRight(Link lnk)
        {
            if (lnk.nod.left.nod == null)
                return lnk;

            return FindMinRight(lnk.nod.left);

        }

        private Link FindMaxLeft(Link lnk)
        {
            if (lnk.nod.right.nod == null)
                return lnk;

            return FindMaxLeft(lnk.nod.right);
        }
        #endregion

        #region ToArray
        public int[] ToArray()
        {
            if (root.nod == null)
                return new int[] { };

            int[] ret = new int[Size()];
            int i = 0;
            NodeToArray(root, ret, ref i);
            return ret;
        }
        private void NodeToArray(Link lnk, int[] ini, ref int n)
        {
            if (lnk.nod == null)
                return;

            NodeToArray(lnk.nod.left, ini, ref n);
            ini[n++] = lnk.nod.val;
            NodeToArray(lnk.nod.right, ini, ref n);

        }
        #endregion

        #region Height
        public int Height()
        {
            return GetHeight(root);
        }
        private int GetHeight(Link lnk)
        {
            if (lnk.nod == null)
                return 0;

            return Math.Max(GetHeight(lnk.nod.left), GetHeight(lnk.nod.right)) + 1;
        }
        #endregion

        #region Leaves
        public int Leaves()
        {
            return GetLeaves(root);
        }
        private int GetLeaves(Link lnk)
        {
            if (lnk.nod == null)
                return 0;

            int leaves = 0;
            leaves += GetLeaves(lnk.nod.left);
            if (lnk.nod.left.nod == null && lnk.nod.right.nod == null)
                leaves++;
            leaves += GetLeaves(lnk.nod.right);
            return leaves;
        }
        #endregion

        #region Nodes
        public int Nodes()
        {
            return GetNodes(root);
        }
        private int GetNodes(Link lnk)
        {
            if (lnk.nod == null)
                return 0;

            int nods = 0;
            nods += GetNodes(lnk.nod.left);
            if (lnk.nod.left.nod != null || lnk.nod.right.nod != null)
                nods++;
            nods += GetNodes(lnk.nod.right);
            return nods;
        }
        #endregion

        #region Reverse
        public void Reverse()
        {
            SwapSides(root);
        }
        private void SwapSides(Link lnk)
        {
            if (lnk.nod == null)
                return;

            SwapSides(lnk.nod.left);
            Link temp = lnk.nod.right;
            lnk.nod.right = lnk.nod.left;
            lnk.nod.left = temp;
            SwapSides(lnk.nod.left);
        }
        #endregion

        #region Width
        public int Width()
        {
            if (root.nod == null)
                return 0;

            int[] ret = new int[Height()];
            GetWidth(root, ret, 0);
            return ret.Max();
        }
        private void GetWidth(Link lnk, int[] levels, int level)
        {
            if (lnk.nod == null)
                return;

            GetWidth(lnk.nod.left, levels, level + 1);
            levels[level]++;
            GetWidth(lnk.nod.right, levels, level + 1);
        }
        #endregion

        #region ToString
        public override String ToString()
        {
            return NodeToString(root).TrimEnd(new char[] { ',', ' ' });
        }

        private String NodeToString(Link lnk)
        {
            if (lnk.nod == null)
                return "";

            String str = "";
            str += NodeToString(lnk.nod.left);
            str += lnk.nod.val + ", ";
            str += NodeToString(lnk.nod.right);
            return str;
        }
        #endregion

        #region Equal

        public bool Equal(ITree tree)
        {
            return CompareNodes(root, (tree as BsTreeLink).root);
        }


        private bool CompareNodes(Link curTree, Link tree)
        {/*
            if (curTree.nod == null && tree.nod == null)
                return true;
            if (curTree.nod == null || tree.nod == null)
                return false;

            bool equal = true;
            equal &= CompareNodes(curTree.nod.left, tree.nod.left);
            equal &= (curTree.nod.val == tree.nod.val);
            equal &= CompareNodes(curTree.nod.right, tree.nod.right);
            return equal;*/

            if (curTree.nod == null && tree.nod == null)
                return true;
            if (curTree.nod == null || tree.nod == null)
                return false;

            if (curTree.nod.val != tree.nod.val)
                return false;

            bool equal = true;
            equal = CompareNodes(curTree.nod.left, tree.nod.left) && CompareNodes(curTree.nod.right, tree.nod.right);
            return equal;
        }

        #endregion
    }
}
