
namespace CalendarLab
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Add_btn = new Button();
            textBox1 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            monthCalendar1 = new MonthCalendar();
            flowLayoutPanel1 = new FlowLayoutPanel();
            timerGeneral = new System.Windows.Forms.Timer(components);
            CurrentTime = new Label();
            SuspendLayout();
            // 
            // Add_btn
            // 
            Add_btn.BackColor = SystemColors.ButtonHighlight;
            Add_btn.Location = new Point(502, 255);
            Add_btn.Name = "Add_btn";
            Add_btn.Size = new Size(150, 38);
            Add_btn.TabIndex = 0;
            Add_btn.Text = "Add";
            Add_btn.UseVisualStyleBackColor = false;
            Add_btn.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(393, 80);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(380, 151);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_Description;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Time;
            dateTimePicker1.Location = new Point(623, 24);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.ShowUpDown = true;
            dateTimePicker1.Size = new Size(150, 27);
            dateTimePicker1.TabIndex = 2;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(49, 24);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 4;
            monthCalendar1.DateChanged += monthCalendar1_DateChanged;
            
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = SystemColors.ButtonFace;
            flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel1.Location = new Point(49, 313);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(724, 325);
            flowLayoutPanel1.TabIndex = 6;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // timerGeneral
            // 
            timerGeneral.Enabled = true;
            timerGeneral.Tick += timer1_Tick;
            // 
            // CurrentTime
            // 
            CurrentTime.AutoSize = true;
            CurrentTime.Location = new Point(393, 31);
            CurrentTime.Name = "CurrentTime";
            CurrentTime.Size = new Size(137, 20);
            CurrentTime.TabIndex = 7;
            CurrentTime.Text = "16.02.2024 14:24:36";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(821, 666);
            Controls.Add(CurrentTime);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(monthCalendar1);
            Controls.Add(dateTimePicker1);
            Controls.Add(textBox1);
            Controls.Add(Add_btn);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            CurrentTime.Text = DateTime.Now.ToString();

        }

        #endregion

        private Button Add_btn;
        private TextBox textBox1;
        private DateTimePicker dateTimePicker1;
        private MonthCalendar monthCalendar1;
        private FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Timer timerGeneral;
        private Label CurrentTime;
    }
}
