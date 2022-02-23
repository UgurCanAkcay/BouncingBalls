using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Security.Cryptography;

namespace topSektirme
{

    public partial class Form1 : Form
    {

        public int i = 0;
        public int first = 1;
        public int sayac = 1;

        int skor = 0;

        public int k = 0;

        public ball[] arrayBall = new ball[10];
        public ballBackup[] ballBackups = new ballBackup[10];

        public int l = 0;
  
        public Form1()
        {
            InitializeComponent();
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (k < 5)
            {
                newball();
            }
            else if(k == 9)
            {
               this.Close();
            }
            if(timer1.Interval == 2000)
            {
                timer1.Interval = 10000;
            }
        }

        private void newball()
        {
            Random rastgele = new Random();
            int xLocation = rastgele.Next(40, 760);
            int yLocation = rastgele.Next(40, 300);
            int colorR = rastgele.Next(0, 256);
            int colorG = rastgele.Next(0, 256);
            int colorB = rastgele.Next(0, 256);

            ball newball = new ball();
            ballBackup newballbackup = new ballBackup();
            newball.locationX = xLocation;
            newball.locationY = yLocation;
            newball.colorR = colorR;
            newball.colorG = colorG;
            newball.colorB = colorB;
            newballbackup.locationX = xLocation;
            newballbackup.locationY = yLocation;
            newballbackup.colorR = colorR;
            newballbackup.colorG = colorG;
            newballbackup.colorB = colorB;
            newball.box.Size = new Size(30, 30);
            newball.box.Location = new Point(xLocation, yLocation);

            newball.box.Paint += new PaintEventHandler(newball.Picture_paint); 

            for (int z = 0; z<10; z++)
            {
                    if (arrayBall[z] == null)
                    {
                        arrayBall[z] = newball;
                        break;
                    }
            }
            for (int z = 0; z <10; z++)
            {
                if (ballBackups[z] == null)
                {
                    ballBackups[z] = newballbackup;
                    break;
                }
            }
            k++;
            i++;
            if (first < 11)
            {
                first++;
            }
            this.Controls.Add(newball.box);
        }

        private void moveball(ball[] arrayBall)
        {
            textBox1.Text = skor.ToString();
            for (int b = 0; b < 10; b++)
            {
                    if (arrayBall[b] != null && ballBackups[b] != null)
                    {
                        if (arrayBall[b].box.Right >= this.ClientSize.Width - 10 && arrayBall[b].box != null)
                        {
                            arrayBall[b].hizx = -arrayBall[b].hizx;
                            ballBackups[b].hizx = -ballBackups[b].hizx;
                            arrayBall[b].box.Left += arrayBall[b].hizx - 20;
                            arrayBall[b].locationX = arrayBall[b].box.Left;
                            ballBackups[b].locationX = arrayBall[b].locationX;
                    }
                        else if (((arrayBall[b].box.Bottom >= panel1.Location.Y && arrayBall[b].box.Left >= panel1.Left) && (arrayBall[b].box.Bottom >= panel1.Location.Y && (arrayBall[b].box.Right <= panel1.Right))) && arrayBall[b].box != null)
                        {
                        arrayBall[b].hizy = -arrayBall[b].hizy;
                        ballBackups[b].hizy = -ballBackups[b].hizy;
                        arrayBall[b].box.Top += arrayBall[b].hizy - 20;
                        arrayBall[b].locationY = arrayBall[b].box.Top;
                        ballBackups[b].locationY = arrayBall[b].locationY;
                        skor += 1;
                            textBox1.Text = skor.ToString();
                        }
                        else if (arrayBall[b].box.Left <= 10 && arrayBall[b].box != null)
                        {
                            arrayBall[b].hizx = -arrayBall[b].hizx;
                            ballBackups[b].hizx = -ballBackups[b].hizx;
                            arrayBall[b].box.Left += arrayBall[b].hizx + 20;
                            arrayBall[b].locationX = arrayBall[b].box.Left;
                            ballBackups[b].locationX = arrayBall[b].locationX;
                    }
                        else if (arrayBall[b].box.Top <= 15 && arrayBall[b].box.Left >= panel3.Right && arrayBall[b].box.Right <= panel4.Left && arrayBall[b].box != null)
                        {
                            arrayBall[b].box.Top += arrayBall[b].hizy - 50;
                            arrayBall[b].locationY = arrayBall[b].box.Top;
                            this.Controls.Remove(arrayBall[b].box);
                            arrayBall[b] = null;
                            ballBackups[b] = null;
                            skor += 10;
                            k--;
                            textBox1.Text = skor.ToString();
                            newball();
                            newball();
                        }
                        else if (arrayBall[b].box.Top <= 15 && arrayBall[b].box != null)
                        {
                            arrayBall[b].hizy = -arrayBall[b].hizy;
                            ballBackups[b].hizy = -ballBackups[b].hizy;
                            arrayBall[b].box.Top += arrayBall[b].hizy + 20;
                            arrayBall[b].locationY = arrayBall[b].box.Top;
                            ballBackups[b].locationY = arrayBall[b].locationY;
                    }
                        else if (arrayBall[b].box.Bottom >= this.ClientSize.Height && arrayBall[b].box != null)
                        {
                            arrayBall[b].box.Top += arrayBall[b].hizy + 100;
                            arrayBall[b].locationY = arrayBall[b].box.Top;
                            this.Controls.Remove(arrayBall[b].box);
                            arrayBall[b]= null;
                            ballBackups[b] = null;
                            k--;
                            skor -= 20;
                            textBox1.Text = skor.ToString();
                        }
                        else if (arrayBall[b].box != null)
                        {
                            arrayBall[b].box.Left += arrayBall[b].hizx;
                            arrayBall[b].box.Top += arrayBall[b].hizy;
                            arrayBall[b].locationX = arrayBall[b].box.Left;
                            arrayBall[b].locationY = arrayBall[b].box.Top;
                            ballBackups[b].locationX = arrayBall[b].locationX;
                            ballBackups[b].locationY = arrayBall[b].locationY;
                    }
                    }
                
            }  
        }

        private void MoveTimer(object sender, EventArgs e)
        {
            if (k == 9)
            {
                this.Close();
            }
            moveball(arrayBall);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int offset = 20;
            switch (keyData)
            {
                case Keys.Left:
                    panel1.Location = new Point(panel1.Location.X - offset, panel1.Location.Y);
                    return true;
                case Keys.Right:
                    panel1.Location = new Point(panel1.Location.X + offset, panel1.Location.Y);
                    return true;
                case Keys.A:
                    panel1.Location = new Point(panel1.Location.X - offset, panel1.Location.Y);
                    return true;
                case Keys.D:
                    panel1.Location = new Point(panel1.Location.X + offset, panel1.Location.Y);
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            button2.Enabled = false;
            button1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataContractJsonSerializer serObj = new DataContractJsonSerializer(typeof(ballBackup[]));
            MemoryStream stream = new MemoryStream();
            serObj.WriteObject(stream, ballBackups);
            string s1 = Encoding.UTF8.GetString(stream.ToArray());
            File.WriteAllText(@"not_Encryptted.json", s1);

            const string message =
          "Alacağınız Yedek Encrypt Edilsin mi? Encryptlamak İstediğiniz Dosyayı Seçiniz!!!";
            const string caption = "Encryptlama Yapılıyor...";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {

            }
            else if (result == DialogResult.Yes)
            {
                try

                {

                    OpenFileDialog ofd = new OpenFileDialog();

                    ofd.ShowDialog();

                    string password = @"MyKey123";

                    UnicodeEncoding UE = new UnicodeEncoding();

                    byte[] key = UE.GetBytes(password);

                    string cryptFile = @"Encryptted.xyz";

                    FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                    RijndaelManaged RMCrypto = new RijndaelManaged();

                    CryptoStream cs = new CryptoStream(fsCrypt,

                        RMCrypto.CreateEncryptor(key, key),

                        CryptoStreamMode.Write);

                    FileStream fsIn = new FileStream(ofd.FileName, FileMode.Open);

                    int data;
                    while ((data = fsIn.ReadByte()) != -1)
                    {
                        cs.WriteByte((byte)data);
                    }

                    fsIn.Close();

                    cs.Close();

                    fsCrypt.Close();

                    MessageBox.Show("Encryptlamak Başarılı Oldu!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Hata Oluştu!");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (File.Exists("Encryptted.xyz"))
            {
                const string message =
             "Yedek Dosyayı Decryptlamak İster misiniz? Decryptlamak İstediğiniz Dosyayı Seçiniz!!!";
                const string caption = "Decryptasyon Gerçekleşiyor...";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {

                }
                else if (result == DialogResult.Yes)
                {
                    try
                    {
                        string password = @"MyKey123";

                        OpenFileDialog ofd = new OpenFileDialog();

                        ofd.ShowDialog();

                        UnicodeEncoding UE = new UnicodeEncoding();

                        byte[] key = UE.GetBytes(password);

                        FileStream fsCrypt = new FileStream(ofd.FileName, FileMode.Open);

                        RijndaelManaged RMCrypto = new RijndaelManaged();

                        CryptoStream cs = new CryptoStream(fsCrypt,

                            RMCrypto.CreateDecryptor(key, key),

                            CryptoStreamMode.Read);

                        string outputfile = @"gp_yedek.json";

                        FileStream fsOut = new FileStream(outputfile, FileMode.Create);

                        int data;

                        while ((data = cs.ReadByte()) != -1)
                        {
                            fsOut.WriteByte((byte)data);
                        }

                        fsOut.Close();

                        cs.Close();

                        fsCrypt.Close();

                        MessageBox.Show("File Decryplasyonu Başarıyla Gerçekleşti!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }

                DataContractJsonSerializer serObj = new DataContractJsonSerializer(typeof(ballBackup[]));
                ballBackup[] DeseriledObj = (ballBackup[])serObj.ReadObject(new MemoryStream(File.ReadAllBytes(@"gp_yedek.json")));
                ballBackups = DeseriledObj;

                for (int i = 0; i < 10; i++)
                {

                    if (ballBackups[i] != null)
                    {
                        arrayBall[i].locationX = ballBackups[i].locationX;
                        arrayBall[i].locationY = ballBackups[i].locationY;
                        arrayBall[i].colorR = ballBackups[i].colorR;
                        arrayBall[i].colorG = ballBackups[i].colorG;
                        arrayBall[i].colorB = ballBackups[i].colorB;
                        arrayBall[i].hizx = ballBackups[i].hizx;
                        arrayBall[i].hizy = ballBackups[i].hizy;
                        arrayBall[i].size = ballBackups[i].size;

                        arrayBall[i].box.Left = ballBackups[i].locationX;
                        arrayBall[i].box.Top = ballBackups[i].locationY;

                        arrayBall[i].box.Paint += new PaintEventHandler(arrayBall[i].Picture_paint);

                        this.Controls.Add(arrayBall[i].box);

                        k++;
                    }
                    else if (arrayBall[i] != null)
                    {
                        this.Controls.Remove(arrayBall[i].box);
                        arrayBall[i] = null;
                    }
                }
            }
        }

        private void Backup_Timer(object sender, EventArgs e)
        {
            DataContractJsonSerializer serObj = new DataContractJsonSerializer(typeof(ballBackup[]));
            MemoryStream stream = new MemoryStream();
            serObj.WriteObject(stream, ballBackups);
            string s1 = Encoding.UTF8.GetString(stream.ToArray());
            File.WriteAllText(@"not_Encryptted.xyz", s1);

            const string message =
        "Alacağınız Yedek Encrypt Edilsin mi? Encryptlamak İstediğiniz Dosyayı Seçiniz!!!";
            const string caption = "Encryptlama Yapılıyor...";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {

            }
            else if (result == DialogResult.Yes)
            {
                try

                {

                    OpenFileDialog ofd = new OpenFileDialog();

                    ofd.ShowDialog();

                    string password = @"MyKey123";

                    UnicodeEncoding UE = new UnicodeEncoding();

                    byte[] key = UE.GetBytes(password);

                    string cryptFile = @"Encryptted.xyz";

                    FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                    RijndaelManaged RMCrypto = new RijndaelManaged();

                    CryptoStream cs = new CryptoStream(fsCrypt,

                        RMCrypto.CreateEncryptor(key, key),

                        CryptoStreamMode.Write);

                    FileStream fsIn = new FileStream(ofd.FileName, FileMode.Open);

                    int data;
                    while ((data = fsIn.ReadByte()) != -1)
                    {
                        cs.WriteByte((byte)data);
                    }

                    fsIn.Close();

                    cs.Close();

                    fsCrypt.Close();

                    MessageBox.Show("Encryptlamak Başarılı Oldu!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Hata Oluştu!");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (File.Exists("Encryptted.xyz"))
            {
                const string message =
             "Yedek üzerinden yükleme yapılsın mı? Decryptlamak İstediğiniz Dosyayı Seçiniz!!!";
                const string caption = "Form'a Yükleme yapılıyor...";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                  
                }
                else if (result == DialogResult.Yes)
                {
                    for(int i= 0; i<10; i++)
                    {
                        if(arrayBall[i] != null)
                        {
                            this.Controls.Remove(arrayBall[i].box);
                            arrayBall[i] = null;
                        }
                        ball newball = new ball();
                        if(arrayBall[i] == null)
                        {
                            arrayBall[i] = newball;
                        }
                    }

                    try
                    {
                        string password = @"MyKey123";

                        OpenFileDialog ofd = new OpenFileDialog();

                        ofd.ShowDialog();

                        UnicodeEncoding UE = new UnicodeEncoding();

                        byte[] key = UE.GetBytes(password);

                        FileStream fsCrypt = new FileStream(ofd.FileName, FileMode.Open);

                        RijndaelManaged RMCrypto = new RijndaelManaged();

                        CryptoStream cs = new CryptoStream(fsCrypt,

                            RMCrypto.CreateDecryptor(key, key),

                            CryptoStreamMode.Read);

                        string outputfile = @"gp_yedek.json";

                        FileStream fsOut = new FileStream(outputfile, FileMode.Create);

                        int data;

                        while ((data = cs.ReadByte()) != -1)
                        {
                            fsOut.WriteByte((byte)data);
                        }

                        fsOut.Close();

                        cs.Close();

                        fsCrypt.Close();

                        MessageBox.Show("File Decryplasyonu Başarıyla Gerçekleşti!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    DataContractJsonSerializer serObj = new DataContractJsonSerializer(typeof(ballBackup[]));
                    ballBackup[] DeseriledObj = (ballBackup[])serObj.ReadObject(new MemoryStream(File.ReadAllBytes(@"gp_yedek.json")));
                    ballBackups = DeseriledObj;

                    for (int i = 0; i < 10; i++)
                    {
                        if (ballBackups[i] != null)
                        {
                            arrayBall[i].locationX = ballBackups[i].locationX;
                            arrayBall[i].locationY = ballBackups[i].locationY;
                            arrayBall[i].colorR = ballBackups[i].colorR;
                            arrayBall[i].colorG = ballBackups[i].colorG;
                            arrayBall[i].colorB = ballBackups[i].colorB;
                            arrayBall[i].hizx = ballBackups[i].hizx;
                            arrayBall[i].hizy = ballBackups[i].hizy;
                            arrayBall[i].size = ballBackups[i].size;

                            arrayBall[i].box.Left = ballBackups[i].locationX;
                            arrayBall[i].box.Top = ballBackups[i].locationY;

                            arrayBall[i].box.Paint += new PaintEventHandler(arrayBall[i].Picture_paint);

                            this.Controls.Add(arrayBall[i].box);

                            k++;
                        }
                        else
                        {
                            this.Controls.Remove(arrayBall[i].box);
                            arrayBall[i] = null;
                        }
                    }
                }
            }
        }
    } 
}
