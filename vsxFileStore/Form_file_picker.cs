using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System.Diagnostics;

namespace vsxFileStore
{
    public partial class Form_file_picker : Form
    {
        public List<string> m_SourceFilePathList = new List<string>();
        static public List<DataFile> m_DataFileList = new List<DataFile>();
        static public EnvDTE.Document doc;

        internal SVsServiceProvider ServiceProvider = null;

        public Form_file_picker()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            long asd = proc.PrivateMemorySize64;
            
            EnvDTE.DTE dte = Package.GetGlobalService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
            doc = dte.ActiveDocument;
            string docpath = "";
            if (!(doc == null))
            {
                docpath = doc.Path + doc.Name;
                if (docpath.IndexOf(' ') > 0)
                {
                    docpath = '"' + docpath + '"';
                }

                // чтобы получить содержимое...
                EnvDTE.TextDocument txt = doc.Object() as EnvDTE.TextDocument;
            }
          
            string[] filesGroup = Directory.GetFiles(doc.Path);
            foreach (string filePath in filesGroup)
            {
                bool bAlreadyExists = false;
                foreach (string fp in m_SourceFilePathList)
                {
                    if (fp == filePath)
                    {
                        bAlreadyExists = true;
                        break;
                    }
                }
                if (bAlreadyExists) continue;
                m_SourceFilePathList.Add(filePath);
            }

            listView2.View = View.Details;
            listView2.Columns.Add("Файл", -1);
            listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            foreach (string obj in m_SourceFilePathList)
            {
                ListViewItem item = listView2.Items.Add(obj);
                item.Checked = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.DefaultExt = "";
            openFileDialog1.FileName = "";
            openFileDialog1.Multiselect = true;
            openFileDialog1.Title = "Выберите файлы с исходным кодом на C/C++";
            openFileDialog1.Filter = "Файлы C++ (*.cpp)|*.cpp|Файлы C (*.c)|*.c|Заголочные (*.h)|*.h|Любые файлы (*.*)|*.*";
            openFileDialog1.ShowDialog(this);

            foreach (String filePath in openFileDialog1.FileNames)
            {
                bool bAlreadyExists = false;
                foreach (string fp in m_SourceFilePathList)
                {
                    if (fp == filePath)
                    {
                        bAlreadyExists = true;
                        break;
                    }
                }
                if (bAlreadyExists) continue;

                m_SourceFilePathList.Add(filePath);
                // listBox1.Items.Add(Path.GetFileName(filePath));
            }
        }

      /*  private void button3_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                m_SourceFilePathList.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }
*/
        private void button2_Click(object sender, EventArgs e)
        {
            if (m_SourceFilePathList.Count() == 0)
            {
                MessageBox.Show("Список файлов исходных кодов пуст! Запуск оптимизации невозможен.");
                return;
            }

            /*Form_Wizard1 frm1 = new Form_Wizard1();
            if (frm1.ShowDialog() == DialogResult.Cancel) return;
            */


            for (int i = 0; i < listView1.CheckedItems.Count; i++)
            {
                if (listView1.CheckedItems[i].Text == "Не определен")
                {
                    MessageBox.Show("ВВедите размер всех выбранных файлов");
                    return;
                }
            }

            Form_Wizard2 frm2 = new Form_Wizard2();
            frm2.ShowDialog();

        }

        public void FindAllDataFiles(List<string> m_SourceFilePathList)
        {
            m_DataFileList.Clear();

            foreach (string FilePath in m_SourceFilePathList)
            {
                try
                {
                    string data = File.ReadAllText(FilePath);
                    FindDataFiles(FilePath, ref data);
                }
                catch
                {
                }
            }
        }
        void FindDataFiles(string SourceFilePath, ref string data)
        {
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
                    bool bFind = false;
                    foreach (DataFile obj in m_DataFileList)
                    {
                        if (obj.m_FileName == DataFileName)
                        {
                            obj.m_SourceFilePathList.Add(SourceFilePath);
                            obj.m_calls++;
                            bFind = true;
                            break;
                        }
                    }
                    if (!bFind)
                    {
                        m_DataFileList.Add(new DataFile(DataFileName, SourceFilePath));
                    }
                }
                //-----------------------------------------------
                pos = data.IndexOf("fopen(", pos + 1);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            listView1.Clear();   
            listView1.View = View.Details;
            listView1.Columns.Add("Файл данных", 100);
            listView1.Columns.Add("Файлы, в которых используется", 200);
            listView1.Columns.Add("Обращений", 80);
            listView1.Columns.Add("Размер (байт)", 100);

            if (listView2.CheckedItems != null)
            {
                List<string> m_SelectedSourceFilePathList = new List<string>();
                for (int i = 0; i < listView2.CheckedItems.Count; i++)
                {
                   m_SelectedSourceFilePathList.Add(listView2.CheckedItems[i].Text);
                }
                
                FindAllDataFiles(m_SelectedSourceFilePathList);
                foreach (DataFile obj in m_DataFileList)
                {
                    ListViewItem item = listView1.Items.Add(obj.m_FileName);
                    string files = obj.m_SourceFilePathList.Count().ToString() + ": ";
                    foreach (string fn in obj.m_SourceFilePathList) files += Path.GetFileName(fn) + ";";
                    item.SubItems.Add(files);
                    item.SubItems.Add(obj.m_calls.ToString());
                    item.SubItems.Add("Не определен");

                    if (obj.m_SourceFilePathList.Count() > 1) item.Checked = true;
                }
            } else
            {
                MessageBox.Show("Выберите один из файлов");
            }
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.CheckedItems.Count; i++)
            {
                // listView1.CheckedItems[0].SubItems[1].Text = "asdad";
                // if (listView1.CheckedItems[i].SubItems[3].Text == "Не определен")
                {
                    int ind = listView1.CheckedItems[i].Index;
                    Form_FileSize frm = new Form_FileSize(ind);
                    if (DialogResult.OK == frm.ShowDialog())
                    {
                        int calls = m_DataFileList[ind].m_calls;
                        listView1.Items[ind].SubItems[2].Text = calls.ToString();

                        int sz = m_DataFileList[ind].m_size;
                        string val = (sz != int.MaxValue ? sz.ToString() : "Не определён");
                        listView1.Items[ind].SubItems[3].Text = val;
                    }
                }
            }
        }
    }
}
