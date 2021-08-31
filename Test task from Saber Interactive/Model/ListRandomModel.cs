using System;
using System.Collections;
using System.Collections.Generic;


namespace Test_task_from_Saber_Interactive
{
    public class ListRandom<T> : IEnumerable<T>  // двусвязный список
    {
        ListNode<T> head; // головной/первый элемент
        ListNode<T> tail; // последний/хвостовой элемент
        ListNode<T> random; // произвольный элемент внутри списка
        int count;  // количество элементов в списке
        public int Count { get { return count; } }
        public void Add(T data, ListRandom<T> list, bool NewList)
        {
            ListNode<T> node = new ListNode<T>(data);
            Random rnd = new Random();

            if (head == null)
            {
                head = node;
                node.Random = node;
                random = node;
            }
            else
            {
                tail.Next = node;
                node.Previous = tail;
                if (NewList == true)
                {
                    node.Random = list.GetNode(rnd.Next(count), list);
                    random = list.GetNode(rnd.Next(count), list);
                }
            }
            tail = node;
            count++;

        }

        // очистка списка
        public void Clear()
        {
            head = null;
            tail = null;
            random = null;
            count = 0;
        }

        public bool Contains(T data)
        {
            ListNode<T> current = head;
            while (current != null)
            {
                if (current.Name.Equals(data))
                    return true;
                current = current.Next;
            }
            return false;
        }

        // возвращает  головной элемент
        public ListNode<T> GetHeadNode()
        {
            return head;
        }

        // возвращает произвольный элемент внутри списка
        public string GetRandomElement()
        {
            return random.Name.ToString();
        }

        // возвращает  головной элемент
        public ListNode<T> GetTailNode()
        {
            return tail;
        }

        // возращает элемент списка по его номеру в списке 
        public ListNode<T> GetNode(int value, ListRandom<T> list)
        {
            int counter = 0;
            ListNode<T> current = head;

            while (current != null)
            {
                if (counter == value)
                {
                    break;
                }
                current = current.Next;
                counter++;
            }
            return current;
        }

        // возвращает элемент списка по его имени
        public ListNode<T> GetNode(string value, ListRandom<T> list)
        {
            ListNode<T> current = head;

            while (current != null)
            {
                if (current.Name.ToString().Contains(value))
                {
                    break;
                }
                current = current.Next;
            }
            return current;
        }

        // устанавливает случайный элемент по имени
        public void SetRandomElement(string value, ListRandom<T> list)
        {
            random = GetNode(value, list);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            ListNode<T> current = head;
            while (current != null)
            {
                yield return current.Name;
                current = current.Next;
            }
        }
    }
}
