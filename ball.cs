using System;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace topSektirme
{
    [Serializable]
    public class ball
    {
        public int size { get; set; }
        public int hizx { get; set; }
        public int hizy { get; set; }
        public int colorR { get; set; }
        public int colorG { get; set; }
        public int colorB { get; set; }
        public int locationX { get; set; }
        public int locationY { get; set; }

        [JsonIgnore]
        public PictureBox box = new PictureBox();

        public ball()
        {
            size = 30;
            hizx = 10;
            hizy = 10;
            colorR = 0;
            colorG = 0;
            colorB = 0;
            locationX = 0;
            locationY = 0;
        }
        public void Picture_paint(object sender, PaintEventArgs e)
        {
            Graphics g1 = e.Graphics;

            Brush brush = new SolidBrush(Color.FromArgb(colorR, colorG, colorB));

            g1.FillEllipse(brush, 0, 0, 28, 28);
        }
    }

    public class ballBackup
    {
        public int size { get; set; }
        public int hizx { get; set; }
        public int hizy { get; set; }
        public int colorR { get; set; }
        public int colorG { get; set; }
        public int colorB { get; set; }
        public int locationX { get; set; }
        public int locationY { get; set; }
        public int skor { get; set; }
        public ballBackup()
        {
            size = 30;
            hizx = 10;
            hizy = 10;
            colorR = 0;
            colorG = 0;
            colorB = 0;
            locationX = 0;
            locationY = 0;
            skor = 0;
        }
    }
}
