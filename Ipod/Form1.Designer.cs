using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Linq;
namespace Ipod
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 900);
            this.Text = "Ipod Nikolai";
            this.BackColor = Color.White;
            if (File.Exists("music.txt"))
                PathToFile.AddRange(File.ReadAllLines("music.txt"));

            this.disk.Image = Image.FromFile("disk.png");
            this.disk.BackColor = Color.White;
            this.disk.Location = new Point(320, 60);
            this.disk.Size = new Size(274, 274);
            this.disk.SizeMode = PictureBoxSizeMode.Zoom;
            this.Controls.Add(disk);

            this.left_button.Size = new Size(80, 80);
            this.left_button.Location = new Point(200, 350);
            this.left_button.Image = Image.FromFile("left.png");
            this.left_button.SizeMode = PictureBoxSizeMode.Zoom;
            this.left_button.Click += Left_button_Click;
            this.Controls.Add(left_button);

            this.right_button.Size = new Size(80, 80);
            this.right_button.Location = new Point(630, 350);
            this.right_button.Image = Image.FromFile("right.png");
            this.right_button.SizeMode = PictureBoxSizeMode.Zoom;
            this.right_button.Click += Right_button_Click;
            this.Controls.Add(right_button);

            this.play_button.Size = new Size(80, 80);
            this.play_button.Location = new Point(415, 350);
            this.play_button.Image = Image.FromFile("play.png");
            this.play_button.SizeMode = PictureBoxSizeMode.Zoom;
            this.play_button.Click += Play_button_Click;
            this.Controls.Add(play_button);

            this.add_button.Size = new Size(80, 80);
            this.add_button.Location = new Point(700, 500);
            this.add_button.Image = Image.FromFile("down.png");
            this.add_button.SizeMode = PictureBoxSizeMode.Zoom;
            this.add_button.Click += Add_button_Click;
            this.Controls.Add(add_button);

            this.delte_button.Size = new Size(80, 80);
            this.delte_button.Location = new Point(700, 700);
            this.delte_button.Image = Image.FromFile("del.png");
            this.delte_button.SizeMode = PictureBoxSizeMode.Zoom;
            this.delte_button.Click += Delte_button_Click;
            this.Controls.Add(delte_button);


            this.list_music.Size = new Size(400, 400);
            this.list_music.Location = new Point(250, 460);
            this.Controls.Add(list_music);

            var flowPanel = new FlowLayoutPanel();
            flowPanel.FlowDirection = FlowDirection.LeftToRight;
            flowPanel.Margin = new Padding(10);

            this.Controls.Add(flowPanel);

            this.FormClosing += Form1_FormClosing;

            backup_music(PathToFile);

            


        }


        private void Delte_button_Click(object sender, EventArgs e)
        {
            int indx = list_music.SelectedIndex;
            
            if (indx == num) 
            {
                MessageBox.Show("Поставтьте на паузу или выберите другой трек для удалениее этого!");
            }
            else
            {
                list_music.Items.RemoveAt(list_music.SelectedIndex);
                PathToFile.Remove(PathToFile[indx]);

            }
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllLines("music.txt",PathToFile);
            
            File.WriteAllText("last_music.txt", num.ToString());
        }
        void backup_music(List<string> pathToFiles)
        {
            this.amount.Text = $"{num + 1}/{PathToFile.Count.ToString()}";
            pathToFiles.Distinct();
            pathToFiles = File.ReadAllLines("music.txt").ToList();
            foreach (var item in pathToFiles)
            {
                list_music.Items.Add(count + "." + Path.GetFileNameWithoutExtension(item));
                count++;
            }
            
        }
        private void Left_button_Click(object sender, EventArgs e)
        {
            
            if (btn_enable == true)
            {
                string message = "Включите аудиопроигрыватель для переключения";
                string caption = "Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);

            }
            else
            {
                if (num == 0)
                {
                    this.left_button.Enabled = false;
                }
                else
                {
                    num--;
                    if (check == false)
                    {
                        outputDevice.Dispose();
                        outputDevice = null;
                        audioFile.Dispose();
                        audioFile = null;
                        GC.Collect();
                        outputDevice?.Stop();

                        if (outputDevice == null)
                        {
                            outputDevice = new WaveOutEvent();
                            outputDevice?.Stop();
                        }
                        if (audioFile == null)
                        {
                            audioFile = new AudioFileReader(PathToFile[num]);
                            //this.list_music.Enabled
                            outputDevice.Init(audioFile);
                            this.play_button.Image = Image.FromFile("pause.png");
                        }
                        outputDevice.Play();
                    }
                    
                }
            }
        }

        private void Play_button_Click(object sender, EventArgs e)
        {
            if (PathToFile.Count == 0)
            {
                string message = "Ваш музыкальный файл не найден(Загрузите ещё раз)";
                string caption = "Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);

            }
            else
            {
                if (check == true)
                {
                    if (outputDevice == null)
                    {
                        outputDevice = new WaveOutEvent();
                        outputDevice?.Stop();
                    }
                    if (audioFile == null)
                    {
                        audioFile = new AudioFileReader(PathToFile[num]);
                        outputDevice.Init(audioFile);
                        this.play_button.Image = Image.FromFile("pause.png");
                    }
                    outputDevice.Play();
                    check = false;
                    btn_enable = false;
                }
                else
                {
                    outputDevice.Dispose();
                    outputDevice = null;
                    audioFile.Dispose();
                    audioFile = null;
                    GC.Collect();
                    outputDevice?.Stop();
                    this.play_button.Image = Image.FromFile("play.png");
                    check = true;
                    btn_enable = true;
                }
            }

        }

        private void Right_button_Click(object sender, EventArgs e)
        {
            if (btn_enable == true)
            {
                string message = "Включите аудиопроигрыватель для переключения";
                string caption = "Error";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);

            }
            else
            {
                if (PathToFile.Count == num + 1)
                {
                    string message = "Добавьте ещё одну песню для продолжения";
                    string caption = "Error";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons);

                }
                else
                {
                    num++;
                    if (check == false)
                    {
                        outputDevice.Dispose();
                        outputDevice = null;
                        audioFile.Dispose();
                        audioFile = null;
                        GC.Collect();
                        outputDevice?.Stop();

                        if (outputDevice == null)
                        {
                            outputDevice = new WaveOutEvent();
                            outputDevice?.Stop();
                        }
                        if (audioFile == null)
                        {
                            audioFile = new AudioFileReader(PathToFile[num]);
                            outputDevice.Init(audioFile);
                            this.play_button.Image = Image.FromFile("pause.png");
                        }
                        outputDevice.Play();
                    }
                    this.left_button.Enabled = true;
                }
            }
        }



        private void Add_button_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "*.mp3|*.mp3";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    PathToFile.Add(dialog.FileName);
                    list_music.Items.Add(count+"."+Path.GetFileNameWithoutExtension(dialog.FileName));
                    count++;
                }
            }
        }
        PictureBox left_button = new PictureBox();
        PictureBox play_button = new PictureBox();
        PictureBox right_button = new PictureBox();
        PictureBox add_button = new PictureBox();
        PictureBox delte_button = new PictureBox();
        PictureBox disk = new PictureBox();
        ListBox list_music = new ListBox();
        Label amount = new Label();
        List<string> PathToFile = new List<string>();
        int count = 1;
        bool check = true;
        bool btn_enable = true;
        static int numVal = Int32.Parse(File.ReadAllText("last_music.txt"));
        int num = numVal;
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;

        #endregion
    }
}

