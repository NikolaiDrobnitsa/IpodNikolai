using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

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
            if (File.Exists("music.txt"))
                PathToFile.AddRange(File.ReadAllLines("music.txt"));

            this.disk.BackgroundImage = Image.FromFile("CD.jpg");
            this.disk.BackColor = Color.White;
            this.disk.Location = new Point(320, 60);
            this.disk.Size = new Size(274, 274);
            this.disk.SizeMode = PictureBoxSizeMode.StretchImage;
            this.disk.Click += Disk_Click;
            this.Controls.Add(disk);

            this.left_button.Size = new Size(80, 80);
            this.left_button.Location = new Point(200, 350);
            this.left_button.Text = "<";
            this.left_button.Font = new Font("Times New Roman", 50, FontStyle.Bold);
            this.Controls.Add(left_button);

            this.right_button.Size = new Size(80, 80);
            this.right_button.Location = new Point(630, 350);
            this.right_button.Text = ">";
            this.right_button.Font = new Font("Times New Roman", 50, FontStyle.Bold);
            this.Controls.Add(right_button);

            this.play_button.Size = new Size(80, 80);
            this.play_button.Location = new Point(415, 350);
            this.play_button.Text = "<>";
            this.play_button.Font = new Font("Times New Roman", 30, FontStyle.Bold);
            this.Controls.Add(play_button);

            this.add_button.Size = new Size(80, 80);
            this.add_button.Location = new Point(700, 500);
            this.add_button.Text = "+";
            this.add_button.Font = new Font("Times New Roman", 30, FontStyle.Bold);
            this.add_button.Click += Add_button_Click;
            this.Controls.Add(add_button);

            this.delte_button.Size = new Size(80, 80);
            this.delte_button.Location = new Point(700, 700);
            this.delte_button.Text = "-";
            this.delte_button.Font = new Font("Times New Roman", 30, FontStyle.Bold);
            this.Controls.Add(delte_button);

            this.list_music.Size = new Size(400, 400);
            this.list_music.Location = new Point(250, 450);
            this.Controls.Add(list_music);




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

        private void Disk_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        Button left_button = new Button();
        Button play_button = new Button();
        Button right_button = new Button();
        Button add_button = new Button();
        Button delte_button = new Button();
        PictureBox disk = new PictureBox();
        ListBox list_music = new ListBox();
        //OpenFileDialog openFileDialog1 = new OpenFileDialog();
        List<string> PathToFile = new List<string>();
        int count = 1;
        #endregion
    }
}

