// 第二章栈，队列和链表的实现
using System;

namespace QueueStackLinkedList
{
    /// <summary>
    /// 栈实现
    /// </summary>
    [Serializable]
    public class Stack
    {
        //todo 实现之
    }

    /// <summary>
    /// 队列实现
    /// </summary>
    /// <typeparam name="T">type of items in the queue</typeparam>
    [Serializable]
    public class Queue<T>
    {
        /// <summary>
        /// the core data contained by queue
        /// </summary>
        private T[] m_data;
        /// <summary>
        /// the index pointing to the next position of the last item
        /// </summary>
        private int tail;
        /// <summary>
        /// the index pointing to the first item
        /// </summary>
        private int head;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="capacity">the maximum capacity of the queue</param>
        public Queue(int capacity)
        {
            capacity = capacity < 2 ? 2 : capacity;
            m_data = new T[capacity];
            head = tail = 0;
        }
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="array">the initial array of the queue</param>
        /// <param name="capacity">the maximum capacity of the queue</param>
        /// <returns>an initialized queue</returns>
        public Queue(int[] array, int capacity) : this(capacity)
        {
            if (array.Length > capacity)
            {
                throw new ArgumentException("The array's length must smaller than the capacity");
            }
            Array.Copy(array, m_data, array.Length);
            tail = array.Length;
            return;
        }
        /// <summary>
        /// pop an item from queue head
        /// </summary>
        /// <returns>the item poped</returns>
        public T Pop()
        {
            T temp = m_data[head];
            if (head == tail)
            {
                throw new IndexOutOfRangeException("The Queue is empty");
            }
            else
            {
                temp = m_data[head % m_data.Length];
                head++;
                if (head >= m_data.Length)
                {
                    head = head % m_data.Length;
                    tail = tail % m_data.Length;
                }
                return temp;
            }
        }
        /// <summary>
        /// push an item to the tail
        /// </summary>
        /// <param name="value">the item to be pushed</param>
        public void Push(T value)
        {
            if (tail - head >= m_data.Length)
            {
                throw new IndexOutOfRangeException("The Queue is full");
            }
            m_data[tail % m_data.Length] = value;
            tail++;
        }
        /// <summary>
        /// get the maximum capacity
        /// </summary>
        /// <value>maximum capacity</value>
        public int capacity
        {
            get
            {
                return m_data.Length;
            }
        }
        /// <summary>
        /// the number of items it contains
        /// </summary>
        /// <value>the number of items it contains</value>
        public int Length
        {
            get
            {
                return tail - head;
            }
        }

        /// <summary>
        /// check if the queue is empty
        /// </summary>
        /// <value>if the queue is empty, return true, otherwise, false</value>
        public bool Empty
        {
            get
            {
                return Length == 0 ? true : false;
            }
        }
    }
    /// <summary>
    /// 链表实现
    /// </summary>
    [Serializable]
    public class LinkedList
    {
        //todo 实现之
    }
}