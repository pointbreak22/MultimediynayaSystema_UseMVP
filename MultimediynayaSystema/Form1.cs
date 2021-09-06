using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultimediynayaSystema
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private readonly List<string> patchs = new List<string>();          //список типо путей доступа к нужному объекту там где класс InitPlugin
        private readonly string PathToFolder = "Music_Plugins";           //название папки где находятся плагины
        private readonly List<Assembly> assemblies = new List<Assembly>();      //по идеи список сборок

        private void ComboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();  //очистка элементов комбобокса
            assemblies.Clear();       //очистка списка сборок
            patchs.Clear();
            var dir = new DirectoryInfo(PathToFolder); // папка с файлами
            foreach (FileInfo file in dir.GetFiles())
            {
                if (Path.GetExtension(file.FullName) == ".dll")
                {
                    Assembly asm = Assembly.LoadFrom(file.FullName);  //получение сборки из файла через рефлексию
                    string patch = Path.GetFileNameWithoutExtension(file.FullName) + ".InitPlugin"; //получение доступа к классу

                    Type t = asm.GetType(patch, false, true);    //получение типа
                    if (t != null)
                    {
                        patchs.Add(patch);
                        object obj = Activator.CreateInstance(t); //получение типа как объект
                        comboBox1.Items.Add(t.GetProperty("PluginName").GetValue(obj).ToString()); //находит свойство PluginName, получает значение, добавляет в комбобокс
                        assemblies.Add(asm);
                    }
                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Assembly asm = assemblies[comboBox1.SelectedIndex];  //выбирается сборка по выбранному индексу
            Type t = asm.GetType(patchs[comboBox1.SelectedIndex], true, true);
            object obj = Activator.CreateInstance(t);
            // получаем метод Show
            MethodInfo method = t.GetMethod("Show");
            // вызываем метод c помощью которого открывается Form1 из библиотеки классов
            _ = method.Invoke(obj, new object[] { });
        }
    }
}