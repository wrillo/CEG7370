using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    internal class quickSortAlgo
    {
        public static void Quicksort<T>(T[] data, int left, int right) where T :
            IComparable<T> // The Quicksort method uses the IComparable<T> implementation to sort the list entries.
        {
            if (right > left)
            {
                int pivot = Partition(data, left, right); 
                Quicksort(data, left, pivot - 1);
                Quicksort(data, pivot + 1, right);
            }
        }
        public static void QuickSortParallel<T>(T[] data, int left, int right) where T : IComparable<T>
        {
            const int SEQUENTIAL_THRESHOLD = 2000;
            if (right > left)
            {
                if ((right - left) < SEQUENTIAL_THRESHOLD)
                    Quicksort(data, left, right);// do sequential sort  
                else
                {
                    int pivot = Partition(data, left, right);
                    Parallel.Invoke(() => QuickSortParallel(data, left, pivot - 1),
                        () => QuickSortParallel(data, pivot + 1, right));
                }
            }
        }

        private static int Partition<T>(T[] data, int low, int high) where T : IComparable<T>
        {
            int left, right; 
            T pivotltem;
            pivotltem = data[low];
            int pivot = left = low; right = high;

            while (left < right)
            {
                while (data[left].CompareTo(pivotltem) <= 0)
                {
                    if (left < data.Length - 1)
                        left++;
                    else
                        break;
                }

                while (data[right].CompareTo(pivotltem) > 0)
                {
                    if (right > 0)
                        right--;
                    else
                        break;
                }

                if (left < right)
                    Swap(data, left, right);
            }

            data[low] = data[right];
            data[right] = pivotltem;
            return right;

        }

        private static void Swap<T>(T[] data, int i, int j)
        {
            T temp = data[i];
            data[i] = data[j];
            data[j] = temp;
        }
    }
}