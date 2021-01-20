using System;

namespace Tree
{
    public class BinaryTreeNode<T>
    {
        BinaryTreeNode(T data)
        {
            Data = data;
        }
        public T Data;
        public BinaryTreeNode<T> Left = null;
        public BinaryTreeNode<T> Right = null;
    }
    public class BinaryHeap<T> where T : IComparable<T>
    {
        //! only when the index starts from 1 the index/2 relation is true
        private Func<T, T, int> m_comparator;
        private T[] m_data;
        private int m_count;
        private bool m_isMax;
        public BinaryHeap(int capacity, bool isMax = true)
        {
            m_data = new T[capacity];
            m_count = 0;
            m_isMax = isMax;
            if (isMax)
            {
                m_comparator = (T a, T b) => { return -(a.CompareTo(b)); };
            }
            else
            {
                m_comparator = (T a, T b) => { return a.CompareTo(b); };
            }
        }
        public BinaryHeap(T[] array, bool isMax = true) : this(array.Length * 2, isMax)
        {
            for (int i = 0; i < array.Length; i++) //! not the optimal implementation.
            {
                Push(array[i]);
            }
        }
        public BinaryHeap() : this(50) { }
        public void Push(T item)
        {
            if (UnusedCount < 2) // ensure the array has 3 more space
            {
                Enlarge();
            }
            m_data[m_count] = item;
            int count = m_count;
            //todo adjust the tree
            while (FatherIndex(count) != -1 && m_comparator(m_data[count], m_data[FatherIndex(count)]) < 0)
            {
                T temp = m_data[count];
                m_data[count] = m_data[(count + 1) / 2 - 1];
                m_data[FatherIndex(count)] = temp;
                count = FatherIndex(count);
            }
            ++m_count;
        }
        public T Pop()
        {
            if (m_count < 1)
            {
                throw new IndexOutOfRangeException("Pop() can not apply on an empty heap");
            }
            m_data[m_count] = m_data[0];
            --m_count;

            m_data[0] = m_data[m_count];
            //todo adjust
            int count = 0;
            while (LeftChildIndex(count) < m_count)
            {
                int exchangeIndex;
                if (RightChildIndex(count) < m_count)
                {
                    if (m_comparator(m_data[LeftChildIndex(count)], m_data[RightChildIndex(count)]) > 0)
                    {
                        exchangeIndex = LeftChildIndex(count);
                    }
                    else
                    {
                        exchangeIndex = RightChildIndex(count);
                    }
                }
                else
                {
                    exchangeIndex = LeftChildIndex(count);
                }
                if (m_comparator(m_data[count], m_data[exchangeIndex]) > 0)
                {
                    m_data[m_count + 2] = m_data[count];
                    m_data[count] = m_data[exchangeIndex];
                    m_data[exchangeIndex] = m_data[m_count + 2];
                }
                else
                {
                    break;
                }
            }
            return m_data[m_count + 1];
        }
        public int Count
        {
            get { return m_count; }
        }
        public bool isMax
        {
            get { return m_isMax; }
        }
        protected int UnusedCount
        {
            get { return m_data.Length - m_count; }
        }
        protected void Enlarge()
        {
            T[] newData = new T[m_data.Length * 2];
            Array.Copy(m_data, newData, m_data.Length);
        }
        protected int FatherIndex(int index)
        {
            return (index + 1) / 2 - 1;
        }
        protected int LeftChildIndex(int index)
        {
            return index * 2 + 1;
        }
        protected int RightChildIndex(int index)
        {
            return index * 2 + 2;
        }
    }
    public class Tree
    {

    }
}