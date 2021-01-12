// 第二章栈，队列和链表的实现
using System;

namespace QueueStackLinkedList
{
    [Serializable]
    public class ListNode<T>
    {
        public static ListNode<T> NextNNode(int index, ListNode<T> currentNode)
        {
            if (index == 0)
            {
                return currentNode;
            }
            return NextNNode(--index, currentNode.Next); //? 递归调用的try catch逻辑是什么呢？能不能不一层层传递异常呢？感觉效率不高啊
        }
        public ListNode(T data)
        {
            m_data = data;
        }

        T m_data;
        ListNode<T> m_next;

        public T Data
        {
            set { m_data = value; }
            get { return m_data; }
        }
        public ListNode<T> Next
        {
            set { m_next = value; }
            get { return m_next; }
        }
        public ListNode<T> NextN(Int32 n)
        {
            return NextNNode(n, this);
        }
    }
    //todo 加入线程安全逻辑

    /// <summary>
    /// 栈实现
    /// </summary>
    [Serializable]
    public class Stack<T> //todo 考虑下线程安全
    {
        /// <summary>
        /// Reference of the item on top of the stack
        /// </summary>
        private ListNode<T> m_top;
        /// <summary>
        /// The number of items stored in this stack
        /// </summary>
        private Int32 m_size;
        /// <summary>
        /// Construct an empty stack
        /// </summary>
        public Stack()
        {
            m_top = null; m_size = 0;
        }
        /// <summary>
        /// Push an item to the stack top
        /// </summary>
        /// <param name="data">The data to push</param>
        public void Push(T data)
        {
            ListNode<T> n = new ListNode<T>(data);
            n.Next = m_top;
            m_top = n;
            m_size++;
        }
        /// <summary>
        /// Push an item to the stack top
        /// </summary>
        /// <param name="node">A ListNode containing data</param>
        // public void Push(ListNode<T> node) //? should I copy the data? // 似乎我应该隐藏实现细节，这个部分不应该存在。
        // {
        //     throw new NotImplementedException("I don't want to implement this method");
        // }
        /// <summary>
        /// Pop an item from the stack top
        /// </summary>
        /// <returns>The item on stack top</returns>
        public T Pop()
        {
            if (m_top == null)
            {
                throw new IndexOutOfRangeException("Can not pop, the stack is empty");
            }
            T t = m_top.Data;
            m_top = m_top.Next;
            m_size--;
            return t;
        }
        /// <summary>
        /// glance the index-th item from the stack top, the perfromance is not so good
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T Top(int index = 0)
        {
            if (index > m_size)
            {
                throw new ArgumentOutOfRangeException("The index must smaller than the size of the stack");
            }
            return m_top.NextN(index).Data;
        }
        /// <summary>
        /// The number of items stored in the stack
        /// </summary>
        /// <value></value>
        public Int32 Size
        {
            get { return m_size; }
        }
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
    /// 链表实现，有问题，并不安全。1. 需要check是否为本object之中的item 2.需要保证链表之间的关系不会被修改，也就是Next不能暴露给用户。
    /// 啊好难啊
    /// </summary>
    [Serializable]
    public class LinkedList<T> where T : IComparable
    {
        public LinkedList() : this(new T[] { })
        { }
        public LinkedList(T[] array)
        {
            if (array.Length == 0)
            {
                m_head = null;
                m_tail = null;
                return;
            }
            m_head = new ListNode<T>(array[0]);
            ListNode<T> temp = m_head;
            for (int i = 1; i < array.Length; i++)
            {
                temp.Next = new ListNode<T>(array[i]);
                temp = temp.Next;
            }
            m_tail = temp;
            m_size = array.Length;
        }
        private ListNode<T> m_head;
        private ListNode<T> m_tail;
        private Int32 m_size;

        public void Insert(ListNode<T> foreNode, T data) //! It is not right. you must ensure the foreNode is in this List
        {
            if (foreNode == null)
            {
                throw new ArgumentNullException("The foreNode should't be null");
            }
            //! Check if this node belongs to this List (An iterator is requested)
            ListNode<T> temp = new ListNode<T>(data);
            temp.Next = foreNode.Next;
            foreNode.Next = temp;
            m_size++;
        }
        public void Delete(ListNode<T> node)
        {
            //! Check if node blongs to this List
            if (node == null || m_size == 0)
            {
                return;
            }
            if (Object.ReferenceEquals(node, m_tail)) // if the node to be deleted is the tail, find the fore node.
            {
                if (m_size == 1)
                {
                    m_head = m_tail = null;
                }
                // 双向链表简单一些...
                ListNode<T> i1 = m_head;
                while (i1.Next.Next != null)
                {
                    i1 = i1.Next;
                }
                i1.Next = null;
            }
            node.Data = node.Next.Data; // interesting
            node.Next = node.Next.Next;
            m_size--;
        }
        public ListNode<T> this[Int32 index] // It shouldn't be used like this.
        {
            get
            {
                return m_head.NextN(index);
            }
            set
            {
                m_head.NextN(index).Data = value.Data;
            }
        }

        public ListNode<T> Find(T value)
        {
            if (m_size == 0)
            {
                return null;
            }
            ListNode<T> temp = m_head;
            while (!(temp.Data.Equals(value)))
            {
                temp = temp.Next;
                if (temp == null)
                {
                    return null;
                }
            }
            return temp;
        }

        public ListNode<T> Find(Func<T, bool> condition)
        {
            if (m_size == 0)
            {
                return null;
            }
            ListNode<T> temp = m_head;
            while (!(condition(temp.Data)))
            {
                temp = temp.Next;
                if (temp == null)
                {
                    return null;
                }
            }
            return temp;
        }

        public ListNode<T> Find(Func<ListNode<T>, bool> condition)
        {
            if (m_size == 0)
            {
                return null;
            }
            ListNode<T> temp = m_head;
            while (!(condition(temp)))
            {
                temp = temp.Next;
                if (temp == null)
                {
                    return null;
                }
            }
            return temp;
        }
        public Int32 Length
        {
            get { return m_size; }
        }

        public ListNode<T> Tail
        {
            get { return m_tail; }
        }
        public ListNode<T> Head
        {
            get { return m_head; }
        }
    }
}