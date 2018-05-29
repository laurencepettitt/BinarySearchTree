using static System.Math;
//using System.Diagnostics;

namespace Trees {

  class BinarySearchTree<T> : ISet<T> where T : System.IComparable<T>
  {
    public class Node : System.IComparable<Node>
    {
      public T Val;
      public Node Left;
      public Node Right;
      public int Height;
      
      public Node(T val, int height) {
        this.Val = val;
        this.Height = height;
      }
      
      public int CompareTo(Node v) => this.Val.CompareTo(v.Val);
      
      string ToStringUtil(Node t, int height)
      {
        if ( t == null ) return "";
        string s = "";
        s += ToStringUtil(t.Right, height + 2);
        s += new string (' ', height) + t.Val;
        //s += t.Val + " ";
        //s += " (" + BalanceFactor(t) + ")";
        s += "\n";
        s += ToStringUtil( t.Left, height + 2);
        return s;
      }
      
      public override string ToString()
        => ToStringUtil(this, 0);
    }
    
    Node Root;
    public int Count = 0;
    
    public BinarySearchTree() { Root = null; }
    
    // Recursive utility to insert val into the Binary Search Tree.
    // Once finished, ref Node currRoot will be the root of BST.
    bool AddTo(ref Node root, T val)
    {
      bool result = true;
      if ( root == null ) {
        root = new Node(val, 1);
        Count++;
        return true;
      }
      else if ( val.CompareTo(root.Val) < 0 )
        result = AddTo(ref root.Left, val);
      else if ( val.CompareTo(root.Val) > 0 )
        result = AddTo(ref root.Right, val);
      else
        return false; // duplicates not allowed
      return result;
    }

    public bool Add(T theVal) => AddTo(ref Root, theVal);
    
    // delete and return min val in tree rooted at root
    T RemoveMin(ref Node root) {
      if (root == null) throw new System.NullReferenceException();
      if (root.Left == null) {
        T min = root.Val;
        root = root.Right;
        return min;
      } else
        return RemoveMin(ref root.Left);
    }
    
    bool RemoveFrom(ref Node root, T val) {
      if ( root == null ) return false; // not found
      bool result = false; // arbitrary initialisation
      if ( val.CompareTo(root.Val) < 0 )
        result = RemoveFrom(ref root.Left, val); // recurse left
      else if ( val.CompareTo(root.Val) > 0 )
        result = RemoveFrom(ref root.Right, val); // recurse right
      else if ( val.CompareTo(root.Val) == 0 ) {
        if (root.Left != null && root.Right != null) {
          root.Val = RemoveMin(ref root.Right); // two children
        } else root = root.Left ?? root.Right; // at most one child
        result = true;
        Count--;
      }
      if (root == null) return true; // deleted node had no children
      return result;
    }
    
    public bool Remove(T val) => RemoveFrom(ref Root, val);
    
    bool ContainsIn(Node root, T val, out Node v) {
      v = root;
      if ( root == null ) 
          return false;
      if ( val.CompareTo(root.Val) == 0 )
          return true;
      if ( val.CompareTo(root.Val) < 0 )
          return ContainsIn(root.Left, val, out v);
      if ( val.CompareTo(root.Val) > 0 )
          return ContainsIn(root.Right, val, out v);
      return false;
    }
    
    public bool Contains(T val) => ContainsIn(Root, val, out Node v);
    
    public override string ToString()
      => (Root == null) ? "" : Root.ToString();
  }

}
