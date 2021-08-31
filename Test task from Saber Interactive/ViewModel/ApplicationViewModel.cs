using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Xml;

namespace Test_task_from_Saber_Interactive
{
    class ApplicationViewModel : INotifyPropertyChanged
    {
        // свойство для приема имени элемента списка
        private string elementName;
        public string ElementName
        {
            get { return elementName; }
            set
            {
                elementName = value;
                OnPropertyChanged("ElementName");
            }
        }

        // свойство для управления доступом к кнопке добавления новых элементов в список
        private bool buttoneAvailible;
        public bool ButtoneAvailible
        {
            get { return buttoneAvailible; }
            set
            {
                buttoneAvailible = value;
                OnPropertyChanged("ButtoneAvailible");
            }
        }


        // команда добавления нового элемента списка 
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      linkedList.Add(ElementName, linkedList, true);
                      ObservableNode observableNode = new ObservableNode();
                      observableNode.Name = ElementName;
                      observableNode.RandomElement = linkedList.GetTailNode().Random.Name;
                      ObservableList.Insert(linkedList.Count - 1, observableNode);
                      ButtoneAvailible = true;
                  }));
            }
        }

        // команда очистки списка //
        private RelayCommand clearCommand;
        public RelayCommand ClearCommand
        {
            get
            {
                return clearCommand ??
                  (clearCommand = new RelayCommand(obj =>
                  {
                      linkedList.Clear();
                      ObservableList.Clear();
                      ButtoneAvailible = false;
                  }));
            }
        }

        // команда открытия списка из файла
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      linkedList.Clear();
                      ObservableList.Clear();
                      if (RequestFile.RequestOpenFile("xml files (*.xml)|*.xml", out string fileName))
                      {
                          Serializer<string>.Deserialize(linkedList, fileName);
                          ListNode<string> current = linkedList.GetTailNode();
                          while (current != null)
                          {
                              ObservableNode observableNode = new ObservableNode();
                              observableNode.Name = current.Name;
                              observableNode.RandomElement = current.Random.Name;
                              ObservableList.Insert(0, observableNode);
                              current = current.Previous;
                          }
                          ButtoneAvailible = true;
                      }
                  }));
            }
        }

        // команда сохранения списка в файл
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      XmlDocument document = Serializer<string>.Serialize(linkedList);
                      string newFileName = null;
                      if (RequestFile.Save("Сохранение конфигурации", "xml files (*.xml)|*.xml", out string fileName) &&
                          fileName != null && fileName.Length > 0)
                          newFileName = fileName;
                      document.Save(fileName);
                  }));
            }
        }


        public ObservableCollection<ObservableNode> ObservableList { get; set; }
        public ListRandom<string> linkedList { get; set; }

        public ApplicationViewModel()
        {
            linkedList = new ListRandom<string>();
            ObservableList = new ObservableCollection<ObservableNode>();              
            ButtoneAvailible = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
