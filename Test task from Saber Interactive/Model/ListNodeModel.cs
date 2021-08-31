using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Test_task_from_Saber_Interactive
{
    public class ListNode<T> : INotifyPropertyChanged
    {
        public ListNode(T data)
        {
            Name = data;
        }
        private T name;
        private ListNode<T> previous;
        private ListNode<T> next;
        private ListNode<T> random;

        public T Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public ListNode<T> Previous
        {
            get { return previous; }
            set
            {
                previous = value;
                OnPropertyChanged("Previous");
            }
        }
        public ListNode<T> Next
        {
            get { return next; }
            set
            {
                next = value;
                OnPropertyChanged("Next");
            }
        }
        public ListNode<T> Random
        {
            get { return random; }
            set
            {
                random = value;
                OnPropertyChanged("Random");
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
