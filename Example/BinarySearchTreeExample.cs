using System;
using static System.Console;
using Trees;

class BinarySearchTreeTest
{
    public static void Main()
    {
        ISet<int> sortedSet = new BinarySearchTree<int>();
        
        while (true)
        {
            string s = ReadLine();
            WriteLine(s);
            if ( string.IsNullOrEmpty(s) ) break;
            char command = s[0];
            if ( !"IPQD".Contains(command.ToString()) ) continue; // invalid command
            if ( command == 'P' )
            {
                WriteLine(sortedSet);
                continue;
            }
            string argString;
            try {
                argString = s.Substring(2);
            } catch (ArgumentOutOfRangeException) {
                continue; // invalid argument
            }
            if ( !int.TryParse(argString, out int arg) )
                continue; // invalid argument
            switch (command)
            {
                case 'I':
                    if (sortedSet.Add(arg) )
                        WriteLine("Element inserted");
                    else
                        WriteLine("Element was already in the tree");
                    break;
                case 'D':
                    if ( sortedSet.Remove(arg) )
                        WriteLine("Element deleted");
                    else
                        WriteLine("Element was not in the tree");
                    break;
                case 'Q':
                    if ( sortedSet.Contains(arg) )
                        WriteLine("Present");
                    else
                        WriteLine("Absent");
                    break;
            }
        }
    }
}
