using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BsTree
{
    class BsTreeLink 
    {
        public class Node
        {
            public int val;
            public Link left;
            public Link right;
            public Node(int val)
            {
                this.val = val;
            }
        }

        public class Link
        { 
            public Node next;
            public Node prev;
        }

        public Node root = null;
      
        public void Init(int[] ini)
        {
            Clear();
            for (int i = 0; i < ini.Length; i++)
            {
                Add(ini[i]);
            }
        }

        public void Add(int val)
        {
            if (root == null)
            {
                root = new Node(val);
                return;
            }
            AddNode(root, val);
        }

        private void AddNode(Node p, int val)
        {
            if (val < p.val)
            {
                if (p.left == null)
                {
                    p.left = new Link();
                    p.left.next = new Node(val);
                    p.left.prev = p;
                }
                else
                {
                    AddNode(p.left.next, val);
                }
            }
            else
            {
                if (p.right == null)
                {
                    p.right = new Link();
                    p.right.next = new Node(val);
                    p.right.prev = p;
                }
                else
                {
                    AddNode(p.right.next, val);
                }
            }
        }

  
        public void Clear()
        {
            root = null;
        }

        public int Size()
        {
            int size = 0;
            Size(root, ref size);
            return size;
        }

        private void Size(Node p, ref int size)
        {
            if (p == null)
                return;

            Size(p.left.next, ref size);
            ++size;
            Size(p.right.next, ref size);

        }

        public int Leaves()
        {
            int n = 0;
            CountLeaves(root, ref n);
            return n;
        }

        private void CountLeaves(Node p, ref int n)
        {
            if (p == null)
            {
                return;
            }

            if (p.right.next == null && p.left.next == null)
            {
                ++n;
            }
            CountLeaves(p.left.next, ref n);
            CountLeaves(p.right.next, ref n);
        }

        public int Nodes()
        {
            return NodeNodes(root);
        }

        private int NodeNodes(Node p)
        {
            if (p == null)
                return 0;

            int ret = 0;
            ret += NodeNodes(p.left.next);
            if (p.left.next != null || p.right.next != null)
                ret++;
            ret += NodeNodes(p.right.next);
            return ret;
        }

  
   
        public void Del(int val)
        {
            if (root == null)
                return;

            Console.WriteLine(root.val);
            root = Del(root, val);
        }
        private Node Del(Node r, int v)
        {
            if (r == null)
                return r;

            if (v < r.val)
            {
                r.left.next = Del(r.left.next, v);
            }
            else if (v > r.val)
            {
                r.right.next = Del(r.right.next, v);
            }
            else
            {
                if (r.left.next == null && r.right.next == null)
                {
                    r = null;
                }
                else if (r.left.next == null)
                {
                    Node tmp = r;
                    r = r.right.next;
                    tmp = null;
                }
                else if (r.right.next == null)
                {
                    Node tmp = r;
                    r = r.left.next;
                    tmp = null;
                }
                else
                {
                    Node tmp = MinValue(ref r.right.next);
                    r.val = tmp.val;
                    r.right.next = Del(r.right.next, tmp.val);
                }
            }
            return r;
        }
        private Node MinValue(ref Node p)
        {
            if (p.left.next != null)
            {
                return MinValue(ref p.left.next);
            }
            else
            {
                return p;
            }
        }
    }
}
