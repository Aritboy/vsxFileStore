using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace vsxFileStore
{
    public partial class Form_Wizard2 : Form
    {
        public Form_Wizard2()
        {
            InitializeComponent();
        }

        int m_V = 100;
        int m_N = 1000;
        int[] best_strategy = new int[0];
        int checkenFilesCount = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                m_V = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Введите корректное значение верхней границы ОП (целое > 0)");
                return;
            }

            try
            {
                m_N = Convert.ToInt32(textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Введите корректное значение числа итераций (целое > 0)");
                return;
            }

            //-----------------------------------------------------------------
            DataFile[] allFiles = Form_file_picker.m_DataFileList.ToArray();
            DataFile[] files = new DataFile[checkenFilesCount];
            int filesIndex = 0;
            for (int i = 0; i < Form_file_picker.m_DataFileList.Count; i++)
            {
                if (Form_file_picker.m_DataFileList[i].m_size != int.MaxValue)
                {
                    files[filesIndex] = Form_file_picker.m_DataFileList[i];
                    filesIndex++;
                }
            }
                
            int cnt = files.Count();
            best_strategy = new int[cnt];
            int[] current_strategy = new int[cnt];
            int F_best = int.MaxValue, V_best = int.MaxValue;
            int F_current, V_current;
            Random rnd = new Random(DateTime.Now.Second);

            for (int iteration = 0; iteration < m_N; iteration++)
            {
                // --- генерируем стратегию и проверяем на соотвествие ограничению памяти 
                V_current = 0;
                for (int i = 0; i < cnt; i++)
                {
                    current_strategy[i] = (int)Math.Round(rnd.NextDouble());
                    V_current += (1 - current_strategy[i]) * files[i].m_size;
                }
                //---- если суммарный объем включенных в текущую стретегию файлов больше
                //---- доступного объема, то переходим к следующей итерации Монте-Карло
                if (V_current > m_V) continue;

                //---- расчитываем значение целевой функции как суммы числа обращений к файлам
                F_current = 0;
                for (int i = 0; i < cnt; i++)
                {
                    F_current += current_strategy[i] * files[i].m_calls;
                }
                //---- сравниваем с рекордом - если текущее значение целевой функции лучше, то обновляем..
                //---- ..рекорд и сохраняем стратегию как лучшую
                if (F_current < F_best)
                {
                    F_best = F_current;
                    V_best = V_current;
                    for (int i = 0; i < cnt; i++) best_strategy[i] = current_strategy[i];
                }
            }
            //------------ отображаем лучшую стратегию на экране ------------------------------------
            richTextBox2_Results.Text = "Число обращений к внешним носителям: " + (F_best == int.MaxValue ? "Не определно" : F_best.ToString());
            richTextBox2_Results.Text = "Используемый объем ОП: " + (V_best == int.MaxValue ? "Не определно" : V_best.ToString());
            stopwatch.Stop();
            long elapsed_time = stopwatch.ElapsedMilliseconds;
            labelTime.Text = "Время поиска решения: " + elapsed_time.ToString();

            if (F_best != int.MaxValue)
            {  // если найдено допустимое значение, то отметить файлы, которые необходимо заменить
                listView1.CheckBoxes = true;
                for (int i = 0; i < cnt; i++)
                {
                    listView1.Items[i].Checked = (best_strategy[i] == 0);
                    listView1.Items[i].Text = (best_strategy[i] == 0 ? "Да" : "Нет");
                }
            }
        }

        private void Form_Wizard2_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Заменять?", 100);
            listView1.Columns.Add("Файл данных", 100);
            listView1.Columns.Add("Обращений к файлу", 120);
            listView1.Columns.Add("Размер (байт)", 100);
            listView1.CheckBoxes = false;

            m_V = 0;
            checkenFilesCount = 0;
            foreach (DataFile obj in Form_file_picker.m_DataFileList)
            {
                if (obj.m_size == int.MaxValue) continue;
                ListViewItem item = listView1.Items.Add("Не определено");
                item.SubItems.Add(obj.m_FileName);
                item.SubItems.Add(obj.m_calls.ToString());
                item.SubItems.Add(obj.m_size.ToString());

                m_V += obj.m_size;
                checkenFilesCount++;
            }
            textBox1.Text = m_V.ToString();
            textBox2.Text = m_N.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Vals = "";
            string initVals1 = "";
            string closeVals = "";
            List<string> filesList = new List<string>();
            foreach (DataFile obj in Form_file_picker.m_DataFileList)
            {
                Vals += "HANDLE " + obj.m_FileName.Split('.')[0] + "Mappling;" +
                    "LPVOID " + obj.m_FileName.Split('.')[0] + "MapplingPOINT;\n\r";
                initVals1 += (string)obj.m_FileName.Split('.')[0] + "Mappling = CreateFileMappingA(" +
                    "INVALID_HANDLE_VALUE, // не связывать с файлом на диске" +
                    "NULL,// неограничен по пользователям" +
                    "PAGE_READWRITE,// вид доступа" +
                    "0,// верхние 4 байта размера файла" +
                    "1000,// нижние 4 байта размера" +
                    "'Example MMF Object');// name" +
                    obj.m_FileName.Split('.')[0] + "ViewOfFile = MapViewOfFile(" +
                        obj.m_FileName.Split('.')[0] + "Mappling, // handle to file-mapping object" +
                        "FILE_MAP_ALL_ACCESS,// desired access" +
                        "0," +
                        "0," +
                        "0);// map all file";
                closeVals += "UnmapViewOfFile("+ obj.m_FileName.Split('.')[0] + "Mappling);CloseHandle("+ obj.m_FileName.Split('.')[0] + "MapplingPOINT); ";
                foreach (string fileCall in obj.m_SourceFilePathList)
                {
                    if(!filesList.Contains(fileCall))
                    {
                        filesList.Add(fileCall);
                    }
                }
            }

            string header = "#pragma once" +
                "#include <windows.h>" +
                "#include <iostream>" +
                "class MapplingTestClass" +
                "        {" +
                "            public:";

            string footer = "};};";

            // Try to create the directory.
            DirectoryInfo di = Directory.CreateDirectory(Form_file_picker.doc.Path + "Test");

            string path = (string)Form_file_picker.doc.Path + "Test/test.txt";
            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(header + Vals + "MapplingTestClass() {" + initVals1 + "}; void MapplingClassOffFile() {" + closeVals + footer);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            foreach (string fileCall in filesList)
            {
                string data = File.ReadAllText(fileCall);
                data = "#include \"MapplingTestClass.h\"\r\n" + data;
                int pos = data.IndexOf("fopen(");
                while (pos != -1)
                {
                    int p1 = data.IndexOf('\"', pos);
                    if (p1 == -1) break;
                    int p2 = data.IndexOf('\"', p1 + 1);
                    if (p2 == -1) break;

                    string DataFileName = data.Substring(p1 + 1, p2 - p1 - 1);
                    if (DataFileName.Length > 0)
                    {
                        string fileName = DataFileName.Split('.')[0];

                    }
                }
            }
        }
    }
}

