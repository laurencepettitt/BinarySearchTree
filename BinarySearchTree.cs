using System;

interface ISet<T>
{
    bool Add(T val);  // Add a value.  Returns true if added, false if already present.
    bool Remove(T Val);  // Remove a value.  Returns true if removed, false if not present.
    bool Contains(T Val);  // Return true if a value is present in the set.
}

class SortedSet<T> : ISet<T> where T : IComparable<T>
{
    public class Node : IComparable<Node>
    {
        public T Val;
        public Node Parent;
        public Node Left;
        public Node Right;
        
        public Node(T val, Node parent)
        {
            this.Val = val;
            this.Parent = parent;
        }
        
        public int CompareTo(Node v) => this.Val.CompareTo(v.Val);
        
        string ToStringUtil(Node t, int height)
        {
            if ( t == null ) return "";
            string s = "";
            s += ToStringUtil(t.Right, height + 2);
            s += new string (' ', height) + t.Val + '\n';
            s += ToStringUtil( t.Left, height + 2);
            return s;
        }
        
        public override string ToString()
            => ToStringUtil(this, 0);
    }
    
    Node Root;
    
    public SortedSet() { Root = null; }
    
    // Recursive utility to insert val into the Binary Search Tree.
    // Once finished, out Node v will be the new root of BST.
    bool AddUtil(ref Node currRoot, T val, Node parent = null)
    {
        if ( currRoot == null ) 
        {
            currRoot = new Node(val, parent);
            return true;
        }
        if ( val.CompareTo(currRoot.Val) < 0 )
            return AddUtil(ref currRoot.Left, val, currRoot);
        if ( val.CompareTo(currRoot.Val) > 0 ) 
            return AddUtil(ref currRoot.Right, val, currRoot);
        return false;
    }
    
    // recursively looks for Node root where T root.Val == val
    // if node found, out Node v equals it, else v = null
    bool ContainsUtil(Node currRoot, T val, out Node v)
    {
        v = currRoot;
        if ( currRoot == null ) 
            return false;
        if ( val.CompareTo(currRoot.Val) == 0 )
            return true;
        if ( val.CompareTo(currRoot.Val) < 0 )
            return ContainsUtil(currRoot.Left, val, out v);
        if ( val.CompareTo(currRoot.Val) > 0 )
            return ContainsUtil(currRoot.Right, val, out v);
        return false;
    }
    
    // recursively looks for Node min where min has minimum value in tree
    // if tree has at least one element then min exists,
    // then out Node min equals it, else min = null
    bool MinUtil(Node currRoot, out Node min)
    {
        min = currRoot;
        if ( currRoot == null ) return false;
        if ( currRoot.Left == null ) return true;
        return MinUtil(currRoot.Left, out min);
    }
    
    public bool Remove(T val, Node v = null)
    {
        bool found = true;
        // need to find node v where v.Val == val
        if ( v == null )
            found = ContainsUtil(Root, val, out v);
        
        if ( !found ) return false;
            
        if ( v.Left == null || v.Right == null ) // v has 1 or no children
        {
            
            // onlyChild is null if v has no children
            // or the only child who is not null, if v has a child
            Node onlyChild = v.Left ?? v.Right;
            
            if ( onlyChild != null ) // v has 1 child
                onlyChild.Parent = v.Parent; // give child to grandparent
                
            if ( v == Root )
            {
                Root = onlyChild; // n.b. onlyChild may be null
                return true; // we are done here
            }
            
            // v is left child of it's parent
            if ( v.Parent.Left.CompareTo(v) == 0 )
                v.Parent.Left = onlyChild;
            else // v is right child of it's parent
                v.Parent.Right = onlyChild;
        }
        else // v has 2 children
        {
            MinUtil(v.Right, out Node min); // find min node on right
            T minVal = min.Val; // let v = min
            Remove(min); // recursion is ok as min has at most 1 child
            v.Val = minVal;
        }
        return true;
    }
    
    public bool Add(T val) => AddUtil(ref Root, val);
    
    public bool Remove(Node n) => Remove(n.Val, n);
    
    public bool Remove(T val) => Remove(val, null);
    
    public bool Contains(T val)
        => ContainsUtil(Root, val, out Node v);
    
    public override string ToString()
        => (Root == null) ? "" : Root.ToString();
}
