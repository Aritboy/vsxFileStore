namespace vsxFileStore
{
    partial class Form_Wizard2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox2_Results = new System.Windows.Forms.RichTextBox();
            this.labelTime = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(10, 4);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(592, 63);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "На данном шаге осуществляется поиск оптимальной стратегии замен файлов данных на " +
    "файлы, проецируемые в память с учетом верхней  границы доступного объема операти" +
    "вной памяти клиентской ЭВМ.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Верхняя граница доступного объёма ОП: ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 129);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(337, 29);
            this.button1.TabIndex = 4;
            this.button1.Text = "Найти оптимальную стратегию замен";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.CheckBoxes = true;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(10, 200);
            this.listView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(592, 171);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(343, 75);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(115, 22);
            this.textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(343, 103);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(115, 22);
            this.textBox2.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 103);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(246, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Количество Монте-Карло итераций: ";
            // 
            // richTextBox2_Results
            // 
            this.richTextBox2_Results.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2_Results.Location = new System.Drawing.Point(10, 375);
            this.richTextBox2_Results.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.richTextBox2_Results.Name = "richTextBox2_Results";
            this.richTextBox2_Results.ReadOnly = true;
            this.richTextBox2_Results.Size = new System.Drawing.Size(592, 49);
            this.richTextBox2_Results.TabIndex = 9;
            this.richTextBox2_Results.Text = "";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(357, 136);
            this.labelTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(0, 16);
            this.labelTime.TabIndex = 10;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 164);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(337, 31);
            this.button2.TabIndex = 11;
            this.button2.Text = "Создать файлы";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form_Wizard2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 429);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.richTextBox2_Results);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form_Wizard2";
            this.Text = "Мастер оптимизации.  Шаг2 - Оптимальная стратегия замен.";
            this.Load += new System.EventHandler(this.Form_Wizard2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox2_Results;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Button button2;
    }
}