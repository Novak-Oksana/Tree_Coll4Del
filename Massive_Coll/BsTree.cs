using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_Coll
{
    public class BsTree : ITree
    {

        public class Node
        {
            public int val;
            public Node left;
            public Node right;
            public Node(int val)
            {
                this.val = val;
            }
        }

        public Node root = null;

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

        #region Add
        public void Add(int val)
        {
            if (root == null)
                root = new Node(val);
            else
                AddNode(root, val);
        }
        private void AddNode(Node node, int val)
        {
            if (val < node.val)
            {
                if (node.left == null)
                    node.left = new Node(val);
                else
                    AddNode(node.left, val);
            }
            else if (val > node.val)
            {
                if (node.right == null)
                    node.right = new Node(val);
                else
                    AddNode(node.right, val);
            }
        }
        #endregion

        #region Del
        public void DelUpRight(int val)
        {
            if (root == null)
                throw new EmptyTreeEx();
            if (FindNode(root, val) == null)
                throw new ValueNotFoundEx();
            if (Size() == 1)
                root = null;

            DeleteNodeUpRight(root, val);
        }

        public void DelUpLeft(int val)
        {
            if (root == null)
                throw new EmptyTreeEx();
            if (FindNode(root, val) == null)
                throw new ValueNotFoundEx();
            if (Size() == 1)
                root = null;

            DeleteNodeUpLeft(root, val);
        }

        private Node FindNode(Node node, int val)
        {
            if (node == null || val == node.val)
                return node;
            if (val < node.val)
                return FindNode(node.left, val);
            else
                return FindNode(node.right, val);
        }
        private Node DeleteNodeUpRight(Node node, int val)//подъём справа
        {
            if (node == null)
                return node;
            if (val == node.val)
            {
                if (node.left != null && node.right != null)
                //  if ( node.right != null)
                {
                    node.val = Min(node.right).val;
                    node.right = DeleteNodeUpRight(node.right, node.val);
                }
                else
                {
                    if (node.left != null)
                    {
                        node = node.left;

                    }
                    else
                    {
                    //    node.val = Min(node.right).val;
                     //   node.right = DeleteNodeUpRight(node.right, node.val);
                        node = node.right;
                    }
                }
                return node;
            }
            if (val < node.val)
                node.left = DeleteNodeUpRight(node.left, val);
            else
                node.right = DeleteNodeUpRight(node.right, val);
            return node;

        }
        private Node DeleteNodeUpLeft(Node node, int val)//подъём слева
        {
            if (node == null)
                return node;
            if (val == node.val)
            {
                if (node.left != null && node.right != null)
                {
                    node.val = Max(node.left).val;
                    node.left = DeleteNodeUpLeft(node.left, node.val);
                }
                else
                {
                    if (node.right != null)
                        node = node.right;
                    else
                        node = node.left;
                }
                return node;
            }
            if (val < node.val)
                node.left = DeleteNodeUpLeft(node.left, val);
            else
                node.right = DeleteNodeUpLeft(node.right, val);
            return node;
        }


        public void DelRotRight(int val)
        {
            if (root == null)
                throw new EmptyTreeEx();
            /*  if (FindNode(root, val) == null)
                  throw new ValueNotFoundEx();
              if (Size() == 1)
                  root = null;*/

            root = DeleteNodeRotRight(root, val);
        }

        public void DelRotLeft(int val)
        {
            if (root == null)
                throw new EmptyTreeEx();

            root = DeleteNodeRotLeft(root, val);
        }

        private Node DeleteNodeRotRight(Node node, int val)//вращение справа
        {
            if (node == null)
                return node;
            if (val == node.val)
            {
                if (node.left != null && node.right != null)
                {
                    Node p = node.left;
                    node.left = null;
                    node = node.right;
                    Min(node).left = p;
                }
                else
                {
                    if (node.left != null)
                        node = node.left;
                    else
                        node = node.right;
                }
                return node;
            }
            if (val < node.val)
                node.left = DeleteNodeRotRight(node.left, val);
            else
                node.right = DeleteNodeRotRight(node.right, val);
            return node;
        }

        private Node DeleteNodeRotLeft(Node node, int val)//вращение слева
        {
            if (node == null)
                return node;
            if (val == node.val)
            {
                if (node.left != null && node.right != null)
                {
                    Node p = node.right;
                    node.right = null;
                    node = node.left;
                    Max(node).right = p;
                }
                else
                {
                    if (node.right != null)
                        node = node.right;
                    else
                        node = node.left;
                }
                return node;
            }
            if (val < node.val)
                node.left = DeleteNodeRotLeft(node.left, val);
            else
                node.right = DeleteNodeRotLeft(node.right, val);
            return node;
        }

        private Node Min(Node node)
        {
            if (node.left == null)
                return node;

            return Min(node.left);
        }
        private Node Max(Node node)
        {
            if (node.right == null)
                return node;

            return Min(node.right);
        }
        #endregion

        #region Height
        public int Height()
        {
            return GetHeight(root);
        }
        private int GetHeight(Node node)
        {
            if (node == null)
                return 0;

            return Math.Max(GetHeight(node.left), GetHeight(node.right)) + 1;
        }
        #endregion

        #region Width
        public int Width()
        {
            if (root == null)
                return 0;

            int[] ret = new int[Height()];
            GetWidth(root, ret, 0);
            return ret.Max();
        }
        private void GetWidth(Node node, int[] levels, int level)
        {
            if (node == null)
                return;

            GetWidth(node.left, levels, level + 1);
            levels[level]++;
            GetWidth(node.right, levels, level + 1);
        }
        #endregion

        #region Leaves
        public int Leaves()
        {
            return GetLeaves(root);
        }
        private int GetLeaves(Node node)
        {
            if (node == null)
                return 0;

            int leaves = 0;
            leaves += GetLeaves(node.left);
            if (node.left == null && node.right == null)
                leaves++;
            leaves += GetLeaves(node.right);
            return leaves;
        }
        #endregion

        #region Nodes
        public int Nodes()
        {
            return GetNodes(root);
        }
        private int GetNodes(Node node)
        {
            if (node == null)
                return 0;

            int nodes = 0;
            nodes += GetNodes(node.left);
            if (node.left != null || node.right != null)
                nodes++;
            nodes += GetNodes(node.right);
            return nodes;
        }
        #endregion

        #region Reverse
        public void Reverse()
        {
            SwapSides(root);
        }
        private void SwapSides(Node node)
        {
            if (node == null)
                return;

            SwapSides(node.left);
            Node temp = node.right;
            node.right = node.left;
            node.left = temp;
            SwapSides(node.left);
        }
        #endregion

        #region Size
        public int Size()
        {
            return GetSize(root);
        }
        private int GetSize(Node node)
        {
            if (node == null)
                return 0;

            int count = 0;
            count += GetSize(node.left);
            count++;
            count += GetSize(node.right);
            return count;
        }
        #endregion

        #region ToArray
        public int[] ToArray()
        {
            if (root == null)
                return new int[] { };

            int[] ret = new int[Size()];
            int i = 0;
            NodeToArray(root, ret, ref i);
            return ret;


        }
        private void NodeToArray(Node node, int[] ini, ref int n)
        {
            if (node == null)
                return;

            NodeToArray(node.left, ini, ref n);
            ini[n++] = node.val;
            NodeToArray(node.right, ini, ref n);

        }
        #endregion

        #region ToString
        public override String ToString()
        {
            return NodeToString(root).TrimEnd(new char[] { ',', ' ' });
        }

        private String NodeToString(Node node)
        {
            if (node == null)
                return "";

            String str = "";
            str += NodeToString(node.left);
            str += node.val + ", ";
            str += NodeToString(node.right);
            return str;
        }

        public void Clear()
        {
            root = null;
        }
        #endregion

        #region Equal

        public bool Equal(ITree tree)
        {
            //  return CompareNodes(root, tree.root);
            return CompareNodes(root, (tree as BsTree).root);
        }

        private bool CompareNodes(Node curTree, Node tree)
        {
            if (curTree == null && tree == null)
                return true;
            if (curTree == null || tree == null)
                return false;

            if (curTree.val != tree.val)
                return false;

            bool equal = true;
            equal = CompareNodes(curTree.left, tree.left) && CompareNodes(curTree.right, tree.right);
            return equal;
        }

        #endregion
    }
}
