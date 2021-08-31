namespace Test_task_from_Saber_Interactive
{
    public class RequestFile
    {
        public static bool Save(string title, string type, out string fileName)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog { Title = title, Filter = type };
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
                fileName = dialog.FileName;
            else
                fileName = string.Empty;
            return result.Value;
        }
        public static bool RequestOpenFile(string filter, out string fileName)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog { Filter = filter, FilterIndex = 2, RestoreDirectory = true };
            bool? result = dialog.ShowDialog();
            if (result == true)
                fileName = dialog.FileName;
            else
                fileName = string.Empty;
            return result.Value;
        }
    }
}
