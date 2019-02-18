// C# program for 3-way quick sort 
using System;
using System.Collections;
using System.Collections.Generic;

class GFG
{
    private static NumberComparer _comparer = new NumberComparer();
    // A function which is used to swap values 
    static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }
    /* This function partitions a[] in three parts 
	a) a[l..i] contains all elements smaller than pivot 
	b) a[i+1..j-1] contains all occurrences of pivot 
	c) a[j..r] contains all elements greater than pivot */
    public static void Partition(int[] a, int l, int r, ref int i, ref int j)
    {
        i = l - 1; j = r;
        int p = l - 1, q = r;
        int v = a[r];

        while (true)
        {
            // From left, find the first element greater than 
            // or equal to v. This loop will definitely terminate 
            // as v is last element 
            while (_comparer.Compare(a[++i], v) < 0) ;

            // From right, find the first element smaller than or 
            // equal to v 
            while (_comparer.Compare(v, a[--j]) < 0)
                if (_comparer.Compare(j, l) == 0)
                    break;

            // If i and j cross, then we are done 
            if (_comparer.Compare(i, j) >= 0) break;

            // Swap, so that smaller goes on left greater goes on right 
            Swap(ref a[i], ref a[j]);

            // Move all same left occurrence of pivot to beginning of 
            // array and keep count using p 
            if (_comparer.Compare(a[i], v) == 0)
            {
                p++;
                Swap(ref a[p], ref a[i]);
            }

            // Move all same right occurrence of pivot to end of array 
            // and keep count using q 
            if (_comparer.Compare(a[j], v) == 0)
            {
                q--;
                Swap(ref a[j], ref a[q]);
            }
        }

        // Move pivot element to its correct index 
        Swap(ref a[i], ref a[r]);

        // Move all left same occurrences from beginning 
        // to adjacent to arr[i] 
        j = i - 1;
        for (int k = l; k < p; k++, j--)
            Swap(ref a[k], ref a[j]);

        // Move all right same occurrences from end 
        // to adjacent to arr[i] 
        i = i + 1;
        for (int k = r - 1; k > q; k--, i++)
            Swap(ref a[i], ref a[k]);
    }

    // 3-way partition based quick sort 
    public static void QuickSort3Way(int[] a, int l, int r)
    {
        if (_comparer.Compare(l, r) >= 0) return;

        int i = 0, j = 0;

        // Note that i and j are passed as reference 
        Partition(a, l, r, ref i, ref j);

        // Recur 
        QuickSort3Way(a, l, j);
        QuickSort3Way(a, i, r);
    }

    // A utility function to print an array 
    public static void PrintArray(int[] a, int n)
    {
        for (int i = 0; i < n; ++i)
            Console.Write(a[i] + " ");
        Console.Write("\n");
    }

    // Driver program 
    static void Main()
    {
        int[] a = { 4, 9, 4, 4, 1, 9, 4, 4, 9, 4, 4, 1, 4 };
        int[] a2 = { 4, 9, 4, 4, 1, 9, 4, 4, 9, 4, 4, 1, 4 };
        int[] a3 = { 4, 9, 4, 4, 1, 9, 4, 4, 9, 4, 4, 1, 4 };
        int size = a.Length;

        System.Console.WriteLine(nameof(QuickSort3Way));
        PrintArray(a, size);
        QuickSort3Way(a, 0, size - 1);
        PrintArray(a, size);
        System.Console.WriteLine(_comparer.GetNumberOfComparisons);

        _comparer.ResetCounter();
        System.Console.WriteLine(nameof(Array.Sort));
        PrintArray(a2, size);
        Array.Sort<int>(a2, _comparer);
        PrintArray(a2, size);
        System.Console.WriteLine(_comparer.GetNumberOfComparisons);

        _comparer.ResetCounter();
        System.Console.WriteLine(nameof(QuickSort));
        PrintArray(a3, size);
        QuickSort(a3, 0, size - 1);
        PrintArray(a3, size);
        System.Console.WriteLine(_comparer.GetNumberOfComparisons);

    }
    /* This function takes last element as pivot, 
    places the pivot element at its correct 
    position in sorted array, and places all 
    smaller (smaller than pivot) to left of 
    pivot and all greater elements to right 
    of pivot */
    static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];

        // index of smaller element 
        int i = (low - 1);
        for (int j = low; j < high; j++)
        {
            // If current element is smaller  
            // than or equal to pivot 
            if (_comparer.Compare(arr[j], pivot) <= 0)
            {
                i++;
                // swap arr[i] and arr[j] 
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        // swap arr[i+1] and arr[high] (or pivot) 
        int temp1 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp1;

        return i + 1;
    }


    /* The main function that implements QuickSort() 
    arr[] --> Array to be sorted, 
    low --> Starting index, 
    high --> Ending index */
    static void QuickSort(int[] arr, int low, int high)
    {
        if (_comparer.Compare(low, high) < 0)
        {
            /* pi is partitioning index, arr[pi] is  
            now at right place */
            int pi = Partition(arr, low, high);

            // Recursively sort elements before 
            // partition and after partition 
            QuickSort(arr, low, pi - 1);
            QuickSort(arr, pi + 1, high);
        }
    }
}

public class NumberComparer : IComparer<int>
{
    public int GetNumberOfComparisons => _counter;
    public void ResetCounter()
    {
        _counter = 0;
    }
    private int _counter = 0;
    public int Compare(int x, int y)
    {
        _counter++;
        return x.CompareTo(y);
    }
}

public class CustomIterator : IEnumerator<long?>
{
    private readonly Node x;
    private readonly long? lo;
    private readonly long? hi;

    public CustomIterator(Node x, long? lo, long? hi)
    {
        if (lo == null) throw new Exception("first argument to keys() is null");
        if (hi == null) throw new Exception("second argument to keys() is null");
				
        this.hi = hi;
        this.lo = lo;
        this.x = x;
    }
    public long? Current { get; set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
        if (x == null) return false;
        int cmplo = lo.Value.CompareTo(x.key);
        int cmphi = hi.Value.CompareTo(x.key);
        if (cmplo < 0) keys(x.left, lo, hi);
        if (cmplo <= 0 && cmphi >= 0) Current = x.key;
        if (cmphi > 0) keys(x.right, lo, hi);

        return true;
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    ///////////////////////
    // BST helper node data type
    public class Node
    {
        public long? key;           // key
        public string val;         // associated data
        public Node left, right;  // links to left and right subtrees
        public bool color;     // color of parent link
        public int size;          // subtree count

        public Node(long? key, string val, bool color, int size)
        {
            this.key = key;
            this.val = val;
            this.color = color;
            this.size = size;
        }
    }

    // add the keys between lo and hi in the subtree rooted at x
    // to the queue
    private IEnumerable<long?> keys(Node x, long? lo, long? hi)
    {
        if (lo == null) throw new Exception("first argument to keys() is null");
        if (hi == null) throw new Exception("second argument to keys() is null");

        if (x == null) yield break;
        int cmplo = lo.Value.CompareTo(x.key);
        int cmphi = hi.Value.CompareTo(x.key);
        if (cmplo < 0) keys(x.left, lo, hi);
        if (cmplo <= 0 && cmphi >= 0) yield return x.key;
        if (cmphi > 0) keys(x.right, lo, hi);
    }
}