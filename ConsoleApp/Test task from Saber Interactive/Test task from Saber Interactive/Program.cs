using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;


namespace Test_task_from_Saber_Interactive
{
    class Program
    {
        static void Main(string[] args)
        {
            ListRandom<string> linkedList = new ListRandom<string>();
            // добавление элементов
            linkedList.Add("Bob", linkedList, true);
            linkedList.Add("Bill", linkedList, true);
            linkedList.Add("Tom", linkedList, true);
            linkedList.Add("Kate", linkedList, true);
            // вывод списка на экран
            Console.WriteLine("Создан двух связный список:");
            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Cлучайный элемент списка {linkedList.GetRandomElement()}");
            // сериализация списка в XML файл
            Console.WriteLine("");
            Console.WriteLine("Запуск сериализации в файл ...");
            Console.WriteLine("");
            XmlDocument document = Serializer<string>.Serialize(linkedList);
            document.Save("C:\\Users\\User\\Desktop\\test.xml");
            Console.WriteLine("Сериализация завершена!");
            // десериализация списка из XML файла
            Console.WriteLine("");
            Console.WriteLine("Запуск десериализации ...");
            Console.WriteLine("");
            Serializer<string>.Deserialize(linkedList);
            Console.WriteLine("Десериализация завершена, востоновлен двух связный список:");
            foreach (var item in linkedList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Cлучайный элемент списка {linkedList.GetRandomElement()}");

        }
    }

    public class ListNode<T>
    {
        public ListNode(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
        public ListNode<T> Previous { get; set; }
        public ListNode<T> Next { get; set; }
        public ListNode<T> Random { get; set; }
    }

    public class ListRandom<T> : IEnumerable<T>  // двусвязный список
    {
        ListNode<T> head; // головной/первый элемент
        ListNode<T> tail; // последний/хвостовой элемент
        ListNode<T> random; // произвольный элемент внутри списка
        int count;  // количество элементов в списке
        Random rnd = new Random();
        public int Count { get { return count; } }

        // добавление элемента
        public void Add(T data, ListRandom<T> list, bool NewList)
        {
            ListNode<T> node = new ListNode<T>(data);

            if (head == null)
                head = node;
            else
            {
                tail.Next = node;
                node.Previous = tail;
            }
            tail = node;
            count++;
            if (NewList == true)
            {
                node.Random = list.GetNode(rnd.Next(count), list);
                random = list.GetNode(rnd.Next(count), list);
            }
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
                if (current.Data.Equals(data))
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
            return random.Data.ToString();
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
                if (current.Data.ToString().Contains(value))
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
                yield return current.Data;
                current = current.Next;
            }
        }
    }

    public class Serializer<T>
    {
        public static XmlDocument Serialize(ListRandom<T> linkedList)
        {
            var doc = new XmlDocument();
            var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            var rootNode = doc.CreateElement("List_of_values");

            ListNode<T> current = linkedList.GetHeadNode();
            while (current != null)
            {
                var element = doc.CreateElement("Element");
                element.SetAttribute("Name", current.Data.ToString());
                element.SetAttribute("Random", current.Random.Data.ToString());

                rootNode.AppendChild(element);
                current = current.Next;
                rootNode.AppendChild(element);
            }


            rootNode.SetAttribute("RandomElemenName", linkedList.GetRandomElement());

            doc.AppendChild(rootNode);

            return doc;
        }

        public static void Deserialize(ListRandom<T> list)
        {
            list.Clear();

            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\Users\\User\\Desktop\\test.xml");
            XmlElement rootNode = doc.DocumentElement;

            foreach (XmlNode childNode in rootNode.ChildNodes)
            {
                list.Add(GetT(childNode.Attributes["Name"].Value.ToString()), list, false);
            }
            ListNode<T> current = list.GetHeadNode();

            while (current != null)
            {
                foreach (XmlNode childNode in rootNode.ChildNodes)
                {
                    current.Random = list.GetNode(childNode.Attributes["Random"].Value.ToString(), list);
                    current = current.Next;
                }
            }
            list.SetRandomElement(rootNode.Attributes["RandomElemenName"].Value.ToString(), list);

        }
        public static T GetT(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
