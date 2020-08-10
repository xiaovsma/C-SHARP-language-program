﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 自动进入钉钉直播间
{
    public partial class FrmSetOCRKey : Form
    {
        private Point initLocal;
        private string apikeyPath;
        public FrmSetOCRKey(int x, int y)
        {
            InitializeComponent();

            // 在父窗口剧中显示
            initLocal.X = x - this.Width / 2;
            initLocal.Y = y - this.Height / 2;
        }

        private void button1_ApplicationKey_Click(object sender, EventArgs e)
        {
            Process.Start("https://ai.baidu.com/tech/ocr/general");
        }

        private void button2_TestKey_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1_APIKey.Text))
                    throw new Exception("APIKey不能为空！");

                if (string.IsNullOrEmpty(textBox2_SecretKey.Text))
                    throw new Exception("SecretKey不能为空！");

                string tmp;
                OCR.GetAccessToken(out tmp, textBox1_APIKey.Text, textBox2_SecretKey.Text);
                if (!string.IsNullOrEmpty(tmp))
                {
                    File.WriteAllText(apikeyPath, textBox1_APIKey.Text + Environment.NewLine + textBox2_SecretKey.Text);
                    MessageBox.Show("自定义文字识别Key成功！", "自动进入钉钉直播间", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                    throw new Exception("自定义文字识别Key失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "自动进入钉钉直播间", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmSetOCRKey_Load(object sender, EventArgs e)
        {
            this.Location = initLocal;

            // 加载ApiKey文件
            apikeyPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "apikey.txt");
            if (!File.Exists(apikeyPath))
                return;

            string ak, sk;
            OCR.ReadApiKeyFile(apikeyPath, out ak, out sk);
            textBox1_APIKey.Text = ak;
            textBox2_SecretKey.Text = sk;
        }
    }
}
