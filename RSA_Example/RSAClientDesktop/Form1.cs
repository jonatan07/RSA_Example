using Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSAClientDesktop
{
    public partial class Form1 : Form
    {
        private string _xmlKeys = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (OFdialog.ShowDialog() == DialogResult.OK)
                {
                    TextReader read = new StreamReader(OFdialog.FileName);
                    _xmlKeys = read.ReadToEnd();
                    lbStatus.Text = "Status Correcto";
                    btnSend.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lbStatus.Text = $"Status incorrecto: {ex.Message}";
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var security = new RSACryptoService();
            security.LoadKeys(_xmlKeys);
            var encryptMessage = security.Encrypt(txtMessage.Text,security.ConvertPublicKey(security.PublicKeyString()));

            txtMessageEncrypt.Text = encryptMessage;
        }
    }
}
