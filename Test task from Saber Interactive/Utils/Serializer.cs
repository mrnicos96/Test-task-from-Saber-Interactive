using System;
using System.Xml;

namespace Test_task_from_Saber_Interactive
{
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
                element.SetAttribute("Name", current.Name.ToString());
                element.SetAttribute("Random", current.Random.Name.ToString());

                rootNode.AppendChild(element);
                current = current.Next;
                rootNode.AppendChild(element);
            }


            rootNode.SetAttribute("RandomElemenName", linkedList.GetRandomElement());

            doc.AppendChild(rootNode);

            return doc;
        }

        public static void Deserialize(ListRandom<T> list, string fileName)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
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
