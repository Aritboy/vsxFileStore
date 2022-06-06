using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vsxFileStore
{
    public partial class Form_FileSize : Form
    {
        public int m_DataFileIndex;
        public int m_Size = int.MaxValue;
        public int m_Calls = 1;
        public string m_FileName = "";
        public Form_FileSize(int DataFileIndex)
        {
            m_DataFileIndex = DataFileIndex;
            m_Size = Form_file_picker.m_DataFileList[m_DataFileIndex].m_size;
            m_Calls = Form_file_picker.m_DataFileList[m_DataFileIndex].m_calls;
            m_FileName = Form_file_picker.m_DataFileList[m_DataFileIndex].m_FileName;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                m_Size = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                m_Size = int.MaxValue;
            }

            try
            {
                m_Calls = Convert.ToInt32(textBox2.Text);
            }
            catch
            {
                m_Calls = 1;
            }
            Form_file_picker.m_DataFileList[m_DataFileIndex].m_size = m_Size;
            Form_file_picker.m_DataFileList[m_DataFileIndex].m_calls = m_Calls;
        }

        private void Form_FileSize_Load(object sender, EventArgs e)
        {
            label2.Text = "Изменить число обращений к файлу " + m_FileName;
            label1.Text = "Введите размер файла " + m_FileName + "(в байтах)";
            textBox1.Text = (m_Size == int.MaxValue) ? "Не определён" : m_Size.ToString();
            textBox2.Text = m_Calls.ToString();
        }
    }
}
