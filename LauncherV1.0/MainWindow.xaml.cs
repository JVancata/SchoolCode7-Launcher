using FileHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LauncherV1._0
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AllFilesLabel.Content = "";
            test();

        }
        private List<String> filesPaths = new List<string>();
        public void test()
        {
            //var engine = new FileHelperEngine<Projects>();
            var dir = new DirectoryInfo(@"C:\Users\vanca\source\repos");
            FileInfo[] files = dir.GetFiles();
            var engine = new FileHelperEngine<Projects>();

            DirectoryInfo[] dirs = dir.GetDirectories();
            ObservableCollection<Projects> filesCol = new ObservableCollection<Projects>();
            foreach (var item in dir.GetFiles("*.sln", SearchOption.AllDirectories))
            {

                string projectPath = item.DirectoryName;
                var dir2 = new DirectoryInfo(@item.DirectoryName);
                //Debug.WriteLine(item.DirectoryName);
                Projects project = new Projects("sad", "asd", "sad", "das");
                string path = projectPath + "\\data.csv";
                //Debug.WriteLine(projectPath);
                if (!System.IO.File.Exists(@path))
                {
                    using (System.IO.FileStream fs = System.IO.File.Create(@path))
                    {
                        fs.WriteByte(0);
                    }
                }
                foreach (var item2 in dir2.GetFiles("*.exe", SearchOption.AllDirectories))
                {
                    if (System.IO.Path.GetFileNameWithoutExtension(item.Name) == System.IO.Path.GetFileNameWithoutExtension(item2.Name))
                    {
                        string smtn = Directory.GetParent(item2.DirectoryName).ToString();
                        string fldr = smtn.Substring(smtn.Length - 3);
                        if (!(fldr == "obj"))
                        {

                            string prettyPath = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(item2.FullName)).ToString() + "/" + item2.Name.ToString();
                            string fullName = item2.FullName.ToString();//System.IO.Path.GetFileNameWithoutExtension(item2.Name) + ".sln"
                            string desc = "none";
                            try
                            {
                                desc = ReadDescription(path);
                            }
                            catch
                            {

                            }
                            Projects projekt = new Projects(prettyPath, fullName, item2.Name, projectPath, desc);
                            filesCol.Add(projekt);
                            List<Projects> listProject = new List<Projects>() { projekt };
                            engine.WriteFile(projectPath + "\\data.csv", listProject);

                            //filesCol.Add(System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(item2.FullName)) + "/" + item2.Name);
                            //filesCol.Add(System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(item2.FullName)) + "/" + item2.Name);
                        }
                    }
                }
            }
            VypisVsech.ItemsSource = filesCol;

            /*var carMake = files
            .Where(item => item.Extension == ".exe")
            .Select(item => item);
            foreach (var item in carMake)
            {
            }*/
        }
        private string ReadDescription(string path)
        {
            var engine = new FileHelperEngine<Projects>();

            // To Read Use:
            var result = engine.ReadFile(path);
            Debug.WriteLine(result);
            /*List<Projects> list = new List<Projects>()
            {
                result
            };*/

            // result is now an array of Customer

            // To Write Use:
            //engine.WriteFile(path, result);

            return result[0].Description;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Projects projekt = (Projects)VypisVsech.SelectedItems[0];
            AllFilesLabel.Content = VypisVsech.SelectedItems[0];
            NameLabel.Content = projekt.Name;
            Description.Text = projekt.Description;
            Process.Start(projekt.FullPath);
            //AllFilesLabel.Content = projekt.FullPath;
        }

        void PrintText(object sender, SelectionChangedEventArgs args)
        {
            Projects projekt = (Projects)VypisVsech.SelectedItems[0];
            NameLabel.Content = projekt.Name;
            AllFilesLabel.Content = projekt.FullPath;
            Debug.WriteLine("Mrdka: "+projekt.Description);
            //var engine = new FileHelperEngine<Projects>();
            //var result = engine.ReadFile(projekt.ProjectPath + "\\data.csv");
            //List<string> descript = ReadDescription(projekt.ProjectPath + "\\data.csv");
            
            //Debug.WriteLine(result);
            Description.Text = projekt.Description;
            Debug.WriteLine(projekt.ProjectPath);
            //ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
            //tb.Text = "   You selected " + lbi.Content.ToString() + ".";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = Description.Text;
            try
            {
                Projects projekt = (Projects)VypisVsech.SelectedItems[0];
                projekt.Description = text;
                var engine = new FileHelperEngine<Projects>();

                List<Projects> listProject = new List<Projects>()
                {
                    projekt
                };
                engine.WriteFile(projekt.ProjectPath + "\\data.csv", listProject);

            }
            catch { }
        }
        //AllFilesLabel.Content = VypisVsech.SelectedItems[0];
        //Process.Start(VypisVsech.SelectedItems[0].ToString());

    }
}
