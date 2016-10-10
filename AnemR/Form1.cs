using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//edited
//fuck
//
namespace AnemR
{
    public partial class Form1 : Form
    {
        //контроллеры для рисования 
        public Font font = new Font("Arial", 9);

        public string topic = "";
        public PictureBox[] PictureBoxes;
        public Bitmap[] bmp;
        public Graphics[] g;
        private int myWidth = 470;
        private int myHeight = 134;
        //
        public List<Diagnosis> diagnoses=new List<Diagnosis>();
        public string[,] dict = new string[,]
        {
            { "Концентрация эритроцитов", "RBC", " х 10^12/л." },
            { "Средний объем эритроцита", "MCV", " мкм3." },
            { "Концентрация тромбоцитов", "PLT", " х 10^9/л." },
            { "Концентрация лейкоцитов", "WBC", " х 10^9/л." },
            { "Концентрация ретикулоцитов", "RET", " %." },
            { "Общий уровень гемоглобина", "HGB", " г/л." },
            { "Среднее содержание гемоглобина \nв одном эритроците", "MCH", "пг." },
            { "Гематокрит", "Hct", " %." },
            { "Цветовой показатель", "CP", " ОЕ." },
            { "Скорость оседания эритроцитов", "COE", " мм/час." }
        };
        double[] vals ;//тут хранятся текущие значения 
        public bool sex = false;//0-м 1-ж
        private TextBox[] tt; //массив текстбоксов
        public double[] tbval;
        public string[] names;
        public Form1()
        {
            diagnoses = template.templates;
            InitializeComponent();
            int co = dict.GetLength(0);
            Label[] l1 = new Label[co];
            TextBox[] t = new TextBox[co];
            Label[] l2=new Label[co];

            tbval=new double[co];
            names=new string[co];

            for (int i = 0; i < t.Length; i++)
            // проходим по элементам масива
            {
                t[i] = new TextBox(); // для каждого элемента массива создаем текстбокс
                l1[i]=new Label();
                l2[i] = new Label();

                l2[i]=new Label();
                l2[i].Text = dict[i, 2];
                l2[i].Location = new Point(270, i * 30 + 20);


                l1[i].Location = new Point(10, i * 30 + 20);
                l1[i].Text = dict[i, 0];

                t[i].Name = dict[i, 1];
                t[i].KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
                names[i] = dict[i, 1];
                    
                t[i].Location = new Point(200, i * 30 + 20); // привязываем расположение тексбоксов к индексам масива, чтоб они не налаживались друг на друга
                t[i].Size = new Size(70, 20); // размеры текстбокса
                l1[i].Size = new Size(200, 30); 
                this.Controls.Add(t[i]); // добавляем текстбоксы на панель
                this.Controls.Add(l1[i]);
                this.Controls.Add(l2[i]);
            }
            tt = t;
            dataGridView1.RowCount = 20;
            dataGridView1.ColumnCount = 20;
            for (int i = 0; i < names.Length; i++)
                dataGridView1.Rows[i + 1].Cells[0].Value = names[i];
            for (int i = 0; i < diagnoses.Count; i++)
                dataGridView1.Rows[0].Cells[i + 1].Value = diagnoses[i].Title;
            
        }
        private void keyPress(object sender, System.EventArgs e)
        {
            // Add event handler code here.
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tt.Length; i++)
            {
                if (tt[i].Text == "")
                {
                    MessageBox.Show("заполните все поля");
                    return;
                }
            }
            vals=new double[tt.Length];
            for (int i = 0; i < tt.Length; i++)
            {
                vals[i] = Convert.ToDouble(tt[i].Text);
            }

            int[][] res;
            // 0- не соответствует
            // 1- соответсвтвует
            // 2- ни то ни то, скорее всего нет такого ключа
            res = new int[diagnoses.Count][];
            int tmp = 0;
            foreach (var v in diagnoses)
            {
                res[tmp] = v.getResult(names, vals,sex);
                tmp++;
            }
            tmp = 0;
            /*
            Diagnosis d1=new Diagnosis();
            d1.Title = "norma";
            double [,] tmp=new double[2,2]{{3,4},{3,4}};
            diagnoses = template.templates;
            d1.Params["ECB"]=tmp;
            string s = "name and surname";
            string[] all=s.Split(' ');
            //this.MediaTypeNames.Text = "lalal";

            */
            for (int i = 0; i < names.Length; i++)
                dataGridView1.Rows[i+1].Cells[0].Value = names[i];
            for (int i = 0; i < diagnoses.Count; i++)
                dataGridView1.Rows[0].Cells[i+1].Value = diagnoses[i].Title;
            for (int i = 0; i < res.GetLength(0); i++)
            {
                for (int j = 0; j < res[0].Length; j++)
                {
                    dataGridView1.Rows[j+1].Cells[i + 1].Value = res[i][j];
                }
            }
            draw();
            check(res);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar>='0' && e.KeyChar<='9')
                return;
            if (e.KeyChar == '.')
                e.KeyChar = ',';
            if (e.KeyChar == ',')
            {
                if (this.Text.IndexOf(',') != -1 || this.Text.Length == 0)
                    e.Handled = true;
                return;
            }
            if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char) Keys.Enter)
                {
                    if (sender.Equals(textBox1))
                        button1.Focus();
                    else
                    {
                        button1.Focus();
                    }
                }
                return;
            }
            e.Handled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            sex = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            sex = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int x = 400;
            int y = 0;
            int co = dict.GetLength(0);
            PictureBoxes = new PictureBox[co];
            bmp = new Bitmap[co];
            g = new Graphics[co];
            //создание
            for (int i = 0; i < co; i++)
            {
                if (i%(co/2) == 0 && i > 2)
                {
                    x = x + myWidth + 10;
                    y = 0;
                }
                PictureBoxes[i] = new PictureBox();
                PictureBoxes[i].Location = new Point(x, 10 + y*(myHeight + 5));
                PictureBoxes[i].Size = new Size(myWidth, myHeight);
                PictureBoxes[i].Visible = true;
                PictureBoxes[i].BackColor = Color.Aqua;
                PictureBoxes[i].BorderStyle = BorderStyle.FixedSingle;
                this.Controls.Add(PictureBoxes[i]);
                y++;
            }
            //рисование
            Diagnosis norma = new Diagnosis();
            foreach (var v in diagnoses)
            {
                if (v.Title == "Norma")
                {
                    norma = v;
                }
            }
            double[,] limits = new double[norma.Params.Count, 2];
            for (int i = 0; i < norma.Params.Count; i++)
            {
                if (sex == false)
                {
                    limits[i, 0] = norma.Params[dict[i, 1]][0, 0];
                    limits[i, 1] = norma.Params[dict[i, 1]][0, 1];
                }
                else
                {
                    limits[i, 0] = norma.Params[dict[i, 1]][1, 0];
                    limits[i, 1] = norma.Params[dict[i, 1]][1, 1];
                }
            }

            for (int i = 0; i < co; i++)
                {
                    
                    bmp[i] = new Bitmap(myWidth, myHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    g[i] = Graphics.FromImage(bmp[i]);
                    g[i].FillRectangle(Brushes.White, 0, 0, myWidth, myHeight);

                    g[i].FillRectangle(Brushes.LightGreen,MyRectangle(myHeight, myWidth, limits[i, 1], limits[i, 0]));
                    g[i].DrawString(dict[i, 0], font, Brushes.Red, 5, 5);
                    PictureBoxes[i].Image = bmp[i];
                }
           }
        
        public Rectangle MyRectangle(double H,double W,double limit,double border)
        {
            //double MAX = (H/1.5/limit)*limit;
            double y = H - (H / 1.5 / limit) * limit;
            double h = (H - y) - (H/1.5/limit)*border;
            //Rec
            return new Rectangle(0, Convert.ToInt16(y), Convert.ToInt16(W), Convert.ToInt16(h));
        }
        public Rectangle MyCircle(double H, double W, double limit, double border, double val)
        {
            //double rel = (H / 1.5 / limit) * val / limit;
            //double MAX = (H / 1.5 / limit) * limit;
            double y = H - (H / 1.5 / limit) * val;
            y = y - 3;
            double h = (H - y) - (H / 1.5 / limit) * border;
            //double res = (H - rel) - (H / 1.5 / limit) * border;
            //Rec
            return new Rectangle(10, Convert.ToInt16(y), Convert.ToInt16(6), Convert.ToInt16(6));
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Font font=new Font("Arial",12);
            
            e.Graphics.DrawString(this.Text,font,Brushes.Black, 3,3);
            e.Graphics.FillRectangle(Brushes.LawnGreen,30,30,400,400);
        }

        public void check(int[][] res)
        {
            for (int i = 0; i < res.Length; i++)
            {
                for (int j = 0; j < res[0].Length; j++)
                {
                    if (res[i][j] == 1)
                    {
                        if (j == res[0].Length - 1)
                        {
                            MessageBox.Show(diagnoses[i].Title);
                        }
                        continue;
                    }
                    else
                        break;
                }
            }
        }
        public void draw()
        {
            for (int i = 0; i < tt.Length; i++)
            {
                vals[i] = Convert.ToDouble(tt[i].Text);
            }
            int co = dict.GetLength(0);
            Diagnosis norma = new Diagnosis();
            foreach (var v in diagnoses)
            {
                if (v.Title == "Norma")
                {
                    norma = v;
                }
            }
            double[,] limits = new double[norma.Params.Count, 2];
            for (int i = 0; i < norma.Params.Count; i++)
            {
                if (sex == false)
                {
                    limits[i, 0] = norma.Params[dict[i, 1]][0, 0];
                    limits[i, 1] = norma.Params[dict[i, 1]][0, 1];
                }
                else
                {
                    limits[i, 0] = norma.Params[dict[i, 1]][1, 0];
                    limits[i, 1] = norma.Params[dict[i, 1]][1, 1];
                }
            }

            for (int i = 0; i < co; i++)
            {

                bmp[i] = new Bitmap(myWidth, myHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                g[i] = Graphics.FromImage(bmp[i]);
                g[i].FillRectangle(Brushes.White, 0, 0, myWidth, myHeight);
               
                g[i].FillRectangle(Brushes.LightGreen, MyRectangle(myHeight, myWidth, limits[i, 1], limits[i, 0]));
                g[i].FillPie(Brushes.DarkBlue, MyCircle(myHeight, myWidth, limits[i, 1], limits[i, 0], vals[i]), 0, 360);
                g[i].DrawString(dict[i, 0], font, Brushes.Red, 5, 5);
                PictureBoxes[i].Image = bmp[i];
            }
        }
       
    }
    
   
}
