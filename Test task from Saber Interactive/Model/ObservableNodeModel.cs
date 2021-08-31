using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Test_task_from_Saber_Interactive
{
    public class ObservableNode : INotifyPropertyChanged
    {
        private string name;
        private string randomElement;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string RandomElement
        {
            get { return randomElement; }
            set
            {
                randomElement = value;
                OnPropertyChanged("RandomElement");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
