using System;
using System.Collections.Generic;
using System.Text;

namespace lab2_Schrage
{
    /// <summary>
    /// Kolejka priorytetowa, której priorytetem jest najmniejsze r
    /// </summary>
    public class MinPriorityQueue
    {
        public MinPriorityQueue(Task[] tasks)
        {
            items = tasks;
            capacity = tasks.Length;
            size = tasks.Length;
        }

        private static int capacity = 10;
        private int size = 0;

        Task[] items = new Task[capacity];


        private int getLeftChildIndex(int parentindex) { return 2 * parentindex + 1; }
        private int getRightChildIndex(int parentindex) { return 2 * parentindex + 2; }
        private int getParentIndex(int childIndex) { return (childIndex - 1) / 2; }

        private bool HasLeftChild(int index) { return getLeftChildIndex(index) < size; }
        private bool HasRightChild(int index) { return getRightChildIndex(index) < size; }
        private bool HasParent(int index) { return getParentIndex(index) >= 0; }

        private Task LeftChild(int index) { return items[getLeftChildIndex(index)]; }
        private Task RightChild(int index) { return items[getRightChildIndex(index)]; }
        private Task Parent(int index) { return items[getParentIndex(index)]; }


        private void Swap(int indexOne, int indexTwo)
        {
            var temp = items[indexOne];
            items[indexOne] = items[indexTwo];
            items[indexTwo] = temp;
        }

        private void RemoveAt(int index)
        {
            if (size == 0) Console.WriteLine("MinQueue: RemoveAt: Pusta kolejka");

            var item = items[index];
            Swap(index, size - 1);
            size--;

            var ele = items[index];

            HeapifyDown();

            if (items[index].Equals(ele))
                HeapifyUp();
        }

        private void EnsureExtraCapacity()
        {
            if (size == capacity)
            {
                Array.Resize(ref items, capacity * 2);
                capacity *= 2;
            }
        }

        private void HeapifyUp()
        {
            int index = size - 1;
            while (HasParent(index) && Parent(index).r > items[index].r)
            {
                Swap(getParentIndex(index), index);
                index = getParentIndex(index);
            }
        }

        private void HeapifyDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                int smallerChildIndex = getLeftChildIndex(index);
                if (HasRightChild(index) && RightChild((index)).r < LeftChild(index).r)
                {
                    smallerChildIndex = getRightChildIndex(index);
                }

                if (items[index].r < items[smallerChildIndex].r)
                {
                    break;
                }
                else
                {
                    Swap(index, smallerChildIndex);
                }
                index = smallerChildIndex;

            }
        }

        /// <summary>
        /// Returns true if is empty, otherwise false
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if(size == 0)
                return true;

            return false;
        }


        /// <summary>
        /// Returns min element but doesn't delete them
        /// </summary>
        /// <returns></returns>
        public Task Peek()
        {
            if (size == 0)
                if (size == 0) Console.WriteLine("MinQueue: Peek: Pusta kolejka");

            return items[0];
        }

        /// <summary>
        /// Return min element, delete it, and heapify
        /// </summary>
        /// <returns></returns>
        public Task Poll()
        {
            var item = items[0];
            RemoveAt(0);
            return item;
        }

        /// <summary>
        /// Add element to heap, and heapify
        /// </summary>
        /// <param name="item"></param>
        public void Add(Task item)
        {
            EnsureExtraCapacity();
            items[size] = item;
            size++;
            HeapifyUp();
        }

        /// <summary>
        /// Remove item with current value, and heapify
        /// </summary>
        /// <param name="item"></param>
        public bool Remove(Task element)
        {
            var index = 0;

            for (int i = 0; i < size; i++)
            {
                if (element.Equals(items[i]))
                {
                    RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        

    }
}
