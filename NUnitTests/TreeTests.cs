using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Tree_Coll;

namespace NUnitTests
{
    [TestFixture(typeof(BsTree))]//падает 1 тест на TestDelFullBsTree_UpRight на del 7
    [TestFixture(typeof(BsTreeR))]//падает 1 тест на TestDelFullBsTree_UpRight на del 3
    [TestFixture(typeof(BsTreeLink))]


    public class NUnitTests<TTree> where TTree : ITree, new()
    {
        ITree lst = null;
        //  ITree lst = new TTree();

        [SetUp]
        public void SetUp()
        {
            lst = new TTree();
        }

        [Test]
        [TestCase(null, new int[] { })]
        [TestCase(new int[] { }, new int[] { })]
        [TestCase(new int[] { 2 }, new int[] { 2 })]
        [TestCase(new int[] { 5, 6 }, new int[] { 5, 6 })]
        [TestCase(new int[] { 3, 7, 4, 9, 1 }, new int[] { 1, 3, 4, 7, 9 })]
        public void TestToArray(int[] ini, int[] exp)
        {
            lst.Init(ini);
            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        [Test]
        [TestCase(null, new int[] { })]
        [TestCase(new int[] { }, new int[] { })]
        [TestCase(new int[] { 2 }, new int[] { 2 })]
        [TestCase(new int[] { 5, 6 }, new int[] { 5, 6 })]
        [TestCase(new int[] { 3, 7, 4, 9, 1 }, new int[] { 1, 3, 4, 7, 9 })]
        public void TestInit(int[] ini, int[] exp)
        {
            lst.Init(ini);
            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        [Test]
        [TestCase(null, "")]
        [TestCase(new int[] { }, "")]
        [TestCase(new int[] { 2 }, "2")]
        [TestCase(new int[] { 5, 6 }, "5, 6")]
        [TestCase(new int[] { 3, 7, 4, 9, 1 }, "1, 3, 4, 7, 9")]
        public void TestToString(int[] ini, string exp)
        {
            lst.Init(ini);
            Assert.AreEqual(exp, lst.ToString());
        }

        [Test]
        [TestCase(null, 0)]
        [TestCase(new int[] { }, 0)]
        [TestCase(new int[] { 2 }, 1)]
        [TestCase(new int[] { 5, 6 }, 2)]
        [TestCase(new int[] { 3, 7, 4, 9, 1 }, 5)]
        public void TestSize(int[] ini, int exp)
        {
            lst.Init(ini);
            Assert.AreEqual(exp, lst.Size());
        }

        [Test]
        [TestCase(null, 0)]
        [TestCase(new int[] { }, 0)]
        [TestCase(new int[] { 2 }, 1)]
        [TestCase(new int[] { 5, 6 }, 2)]
        [TestCase(new int[] { 3, 7, 4, 9, 1 }, 3)]
        [TestCase(new int[] { 3, 7, 4, 9, 1, 12, 2, -5, 5 }, 4)]
        public void TestHeight(int[] ini, int exp)
        {
            lst.Init(ini);
            Assert.AreEqual(exp, lst.Height());
        }

        [Test]
        [TestCase(null, 0)]
        [TestCase(new int[] { }, 0)]
        [TestCase(new int[] { 2 }, 1)]
        [TestCase(new int[] { 5, 6 }, 1)]
        [TestCase(new int[] { 3, 7, 4, 9, 1 }, 2)]
        [TestCase(new int[] { 3, 7, 4, 9, 1, 12, 2, -5, 5 }, 4)]
        public void TestWidth(int[] ini, int exp)
        {
            lst.Init(ini);
            Assert.AreEqual(exp, lst.Width());
        }

        [Test]
        [TestCase(null, 0)]
        [TestCase(new int[] { }, 0)]
        [TestCase(new int[] { 2 }, 0)]
        [TestCase(new int[] { 5, 6 }, 1)]
        [TestCase(new int[] { 3, 7, 4, 9, 1 }, 2)]
        [TestCase(new int[] { 3, 7, 4, 9, 1, 12, 2, -5, 5 }, 5)]
        public void TestNodes(int[] ini, int exp)
        {
            lst.Init(ini);
            Assert.AreEqual(exp, lst.Nodes());
        }

        [Test]
        [TestCase(null, 0)]
        [TestCase(new int[] { }, 0)]
        [TestCase(new int[] { 2 }, 1)]
        [TestCase(new int[] { 5, 6 }, 1)]
        [TestCase(new int[] { 3, 7, 4, 9, 1 }, 3)]
        public void TestLeaves(int[] ini, int exp)
        {
            lst.Init(ini);
            Assert.AreEqual(exp, lst.Leaves());
        }

        [Test]
        [TestCase(null, new int[] { 1 }, 1)]
        [TestCase(new int[] { }, new int[] { 2 }, 2)]
        [TestCase(new int[] { 2 }, new int[] { 0, 2 }, 0)]
        [TestCase(new int[] { 5, 8 }, new int[] { 5, 6, 8 }, 6)]
        [TestCase(new int[] { 3, 7, 4, 9, 1 }, new int[] { 0, 1, 3, 4, 7, 9 }, 0)]
        public void TestAdd(int[] ini, int[] exp, int val)
        {
            lst.Init(ini);
            lst.Add(val);
            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

        [TestCase(new int[] { 2 }, new int[] { }, 2)]
        [TestCase(new int[] { 5, 8 }, new int[] { 5 }, 8)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 9, 8 }, 2)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 2, 8 }, 9)]
        //  [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 1, 8, 0, 2, 9 }, 7)]
        //  [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 7, 1, 9, 0, 2, 8 }, 3)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 2, 7, 0, 9, 8 }, 1)]
        [TestCase(new int[] { 3, 7, 1, 0, 8, 2, 9 }, new int[] { 3, 1, 8, 0, 2, 9 }, 7)]
        public void TestDelFullBsTree_UpRight(int[] ini, int[] exp, int val)
        {
            TTree treeExp = new TTree();
            TTree treeAct = new TTree();
            treeExp.Init(exp);
            treeAct.Init(ini);
            treeAct.DelUpRight(val);
            Assert.IsTrue(treeAct.Equal(treeExp));
            Assert.AreEqual(treeExp.Size(), treeAct.Size());
            /*
                        TTree compare = new TTree();
                        compare.Init(exp);
                        lst.Init(ini);
                        lst.DelUpRight(val);
                        Assert.IsTrue(lst.Equal(compare));*/
        }

        [Test]
        [TestCase(new int[] { 2 }, new int[] { }, 2)]
        [TestCase(new int[] { 5, 8 }, new int[] { 5 }, 8)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 1, 7, 0, 9, 8 }, 2)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 1, 7, 0, 2, 8 }, 9)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 1, 9, 0, 2, 8 }, 7)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 2, 1, 7, 0, 9, 8 }, 3)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 0, 7, 2, 9, 8 }, 1)]
        public void TestDelFullBsTree_UpLeft(int[] ini, int[] exp, int val)
        {
            TTree treeExp = new TTree();
            TTree treeAct = new TTree();
            treeExp.Init(exp);
            treeAct.Init(ini);
            treeAct.DelUpLeft(val);
            Assert.IsTrue(treeAct.Equal(treeExp));
            Assert.AreEqual(treeExp.Size(), treeAct.Size());
        }

        [TestCase(new int[] { 2 }, new int[] { }, 2)]
        [TestCase(new int[] { 5, 8 }, new int[] { 5 }, 8)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 9, 8 }, 2)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 2, 8 }, 9)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 9, 8, 1, 0, 2 }, 7)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 7, 1, 2, 9, 0, 8 }, 3)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 2, 7, 0, 9, 8 }, 1)]
        public void TestDelFullBsTree_RotRight(int[] ini, int[] exp, int val)
        {
            TTree treeExp = new TTree();
            TTree treeAct = new TTree();
            treeExp.Init(exp);
            treeAct.Init(ini);
            treeAct.DelRotRight(val);
            Assert.IsTrue(treeAct.Equal(treeExp));
            Assert.AreEqual(treeExp.Size(), treeAct.Size());

        }

        [TestCase(new int[] { 2 }, new int[] { }, 2)]
        [TestCase(new int[] { 5, 8 }, new int[] { 5 }, 8)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 9, 8 }, 2)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 7, 1, 0, 2, 8 }, 9)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 9, 8, 1, 0, 2 }, 7)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 1, 2, 7, 9, 0, 8 }, 3)]
        [TestCase(new int[] { 3, 7, 1, 0, 9, 2, 8 }, new int[] { 3, 0, 7, 2, 9, 8 }, 1)]
        public void TestDelFullBsTree_RotLeft(int[] ini, int[] exp, int val)
        {
            TTree treeExp = new TTree();
            TTree treeAct = new TTree();
            treeExp.Init(exp);
            treeAct.Init(ini);
            treeAct.DelRotLeft(val);
            Assert.IsTrue(treeAct.Equal(treeExp));
            Assert.AreEqual(treeExp.Size(), treeAct.Size());
        }

        [Test]
        [TestCase(null, 5)]
        [TestCase(new int[] { }, 2)]
        public void TestDelEx(int[] ini, int val)
        {
            lst.Init(ini);
            var ex = Assert.Throws<EmptyTreeEx>(() => lst.DelUpRight(val));
            Assert.AreEqual(typeof(EmptyTreeEx), ex.GetType());
        }

        [Test]
        [TestCase(null, 5)]
        [TestCase(new int[] { }, 2)]
        public void TestDelExEmppty(int[] ini, int val)
        {
            lst.Init(ini);
            var ex = Assert.Throws<EmptyTreeEx>(() => lst.DelUpRight(val));
            Assert.AreEqual(typeof(EmptyTreeEx), ex.GetType());
        }


        /*     [Test]
             [TestCase(new int[] { 3, 7, 4, 9, 1 }, 5)]
             public void TestDelExNull(int[] ini, int val)
             {
                 lst.Init(ini);
                 var ex = Assert.Throws<ValueNotFoundEx>(() => lst.DelUpRight(val));
                 Assert.AreEqual(typeof(ValueNotFoundEx), ex.GetType());
             }*/

        [Test]
        [TestCase(null, new int[] { })]
        [TestCase(new int[] { }, new int[] { })]
        [TestCase(new int[] { 2 }, new int[] { 2 })]
        [TestCase(new int[] { 5, 8 }, new int[] { 8, 5 })]
        [TestCase(new int[] { 3, 7, 4, 9, 1 }, new int[] { 9, 7, 4, 3, 1 })]
        [TestCase(new int[] { 3, 7, 4, 9, 1, 12, 2, -5, 5 }, new int[] { 12, 9, 7, 5, 4, 3, 2, 1, -5 })]
        public void TestReverse(int[] ini, int[] exp)
        {
            lst.Init(ini);
            lst.Reverse();
            CollectionAssert.AreEqual(exp, lst.ToArray());
        }

    }
}
