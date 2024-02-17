using static System.Console;

namespace CalendarLab
{
    public partial class Form1 : Form
    {

        string selectedDate = "";
        string selectedTime = "";
        string eventDescription = "";
        string filepath = "events.txt";
        int counter = 0;
        DateTime targetTime;

        List<System.Windows.Forms.Timer> timers = new();

        public Form1()
        {
            InitializeComponent();
            ImportLabels(filepath);
        }


        // ���� ����
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            selectedTime = dateTimePicker1.Value.ToString("HH:mm:ss");
            targetTime = dateTimePicker1.Value;

        }

        // ���� ���� 
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            // ��� ������� - ��� �� ������� ���� �� ���������, ���� � �� ������ ����� ���� ����.  
            
            monthCalendar1.AddBoldedDate(monthCalendar1.SelectionStart);
            monthCalendar1.UpdateBoldedDates();
            selectedDate = monthCalendar1.SelectionStart.ToShortDateString();
            selectedTime = dateTimePicker1.Value.ToString("HH:mm:ss");
            DateTime tempDate = monthCalendar1.SelectionStart.Date;
            DateTime tempTime = dateTimePicker1.Value;
            targetTime = new DateTime(
                tempDate.Year,
                tempDate.Month,
                tempDate.Day,
                tempTime.Hour,
                tempTime.Minute,
                tempTime.Second);
        }
        // ���� ��䳿
        private void textBox1_Description(object sender, EventArgs e)
        {
            eventDescription = textBox1.Text;
        }


        //��������� ������ ����������� ������� ADD
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(eventDescription))
            {
                MessageBox.Show("Add event description!");
                return; //��� ����� ��䳿 �� �����������!
            }
           /* if (string.IsNullOrEmpty(selectedDate))
            {
                MessageBox.Show("Select date!");
                return; //��� ��������� ���� �� �����������!
            }*/
            var lbl = new Label()
            {
                AutoSize = true,
                Text = $"{selectedDate} {selectedTime} \n {eventDescription}",
                BorderStyle = BorderStyle.FixedSingle,
                Tag = counter

            };
            counter++;
            flowLayoutPanel1.Controls.Add(lbl);
            DeleteEventHandler(lbl); // ��������� - leftclick
            InfoEventHandler(lbl); // ���� ���������� - rightclick
            AddTimer(lbl, targetTime); // ������
            ExportLabels(filepath); // ������ � ����
        }

        //�� ���� �� ��� ������� � �����
        private void AddLabel(string labelText)
        {
            var lbl = new Label()
            {
                AutoSize = true,
                Text = labelText,
                BorderStyle = BorderStyle.FixedSingle,
                Tag = counter
            };
            counter++;
            flowLayoutPanel1.Controls.Add(lbl);
            DeleteEventHandler(lbl); // ���������
            InfoEventHandler(lbl); // ����������
            DateTime tempTargetTime = GetTimeFromText(labelText);
           
            AddTimer(lbl, tempTargetTime); // ������
            if (tempTargetTime < DateTime.Now) lbl.BackColor = Color.LightGray;
        }

        private static DateTime GetTimeFromText(string labelText)
        {
            DateTime tempTargetTime = new();
            foreach (string line in labelText.Split('\n'))
            {
                try
                {
                    tempTargetTime = DateTime.Parse(line);
                }
                catch { }
            }

            return tempTargetTime;
        }




        // ������
        private void AddTimer(Label lbl, DateTime tempTargetTime)
        {
            System.Windows.Forms.Timer timer = new();
            
            //���� �������
            timer.Tick += (sender, e) =>
            {
                Beep(440, 1000);                
                lbl.BackColor = Color.LightGray;
                timer.Stop();
            };

            // ��������� ������

            TimeSpan timeDifference = tempTargetTime - DateTime.Now;

            try { 
            if (timeDifference.TotalMilliseconds > 0) // ��������, �� �������� ��� � �����������
            {
                timer.Interval = (int)timeDifference.TotalMilliseconds; // ������������ �������� �������
             }
            else
            {
                timer.Interval = 1;
            }
            
            timers.Add(timer);
            if (timer.Interval > 1)
                timer.Start(); // ��������� ������
            }
            catch (Exception ex) { 
                
                // ������ ��� ���� ������ �������, 
                MessageBox.Show(ex.Message); 
                lbl.BackColor = Color.Red;
                timer.Interval = 1;
            
            }


        }



        // ������� 
        private void ExportLabels(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {

                foreach (Label label in flowLayoutPanel1.Controls.OfType<Label>())
                {
                    writer.WriteLine(label.Text + "\n===ENDLABEL===");
                }
            }
        }

        // ������
        private void ImportLabels(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string fileContent = reader.ReadToEnd();

                string[] labels = fileContent.Split("\n===ENDLABEL===" + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                foreach (string label in labels)
                {
                 
                    if (label != "")
                        AddLabel(label);
                }
            }
        }



        // ���� ��� ���� (right click)
        private void InfoEventHandler(Label label)
        {
            label.Click += (sender, e) =>
            {
                if (e is MouseEventArgs mouseEventArgs && mouseEventArgs.Button == MouseButtons.Right)
                {
                    Label clickedLabel = (Label)sender;

                    string[] labelInfo = clickedLabel.Text.Split('\n');
                    string time = labelInfo[0];
                    string description = labelInfo[1];
                    int index = Convert.ToInt32(label.Tag);
                    string intervalinfo = GetIntervalInfo(index);
                    string timeleft = GetTimeLeft(label, index);

                    MessageBox.Show($"Event #: {index}\nTime: {time}\nDescr: {description}\nInterval set: {intervalinfo}\nTime left: {timeleft}");
                }
            };
        }

        private string GetIntervalInfo(int index)
        {
            string waitinfo = "";
            int waitseconds = timers[index].Interval / 1000;
            if (timers[index].Interval / 1000 == 0) waitinfo = "event passed";
            else waitinfo = waitseconds.ToString() + " sec";
            return waitinfo;
        }

        private string GetTimeLeft(Label label, int index)
        {
            int seconds = 0;
            DateTime now = DateTime.Now;
            DateTime eventTime = GetTimeFromText(label.Text);
            TimeSpan timeSpan = eventTime - now;
            if (timeSpan.Milliseconds < 0) return "event passed";
            else seconds = (int)timeSpan.TotalSeconds;
            return seconds.ToString() + " sec";
        }


        // ��������� ��䳿 (leftclick)


        private void DeleteEventHandler(Label label)
        {
            label.Click += (sender, e) =>
            {
                if (e is MouseEventArgs mouseEventArgs && mouseEventArgs.Button == MouseButtons.Left)
                {
                    DialogResult result = MessageBox.Show("Delete Event?", "", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        RemoveLabel((Label)sender);
                    }
                }
            };
        }

        private void RemoveLabel(Label label)
        {
            flowLayoutPanel1.Controls.Remove(label);
            label.Dispose();
            ExportLabels(filepath);
        }


        //������ ���������
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Color.AliceBlue);
            e.Graphics.FillEllipse(brush, 0, 0, flowLayoutPanel1.Width, flowLayoutPanel1.Height);
        }

       
    }
}
