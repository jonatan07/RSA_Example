using Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

        private async void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                var security = new RSACryptoService();
                security.LoadKeys(_xmlKeys);
                var encryptMessage = security.Encrypt(txtMessage.Text, security.ConvertPublicKey(security.PublicKeyString()));
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://axcomapi.azurewebsites.net");
                //httpClient.BaseAddress = new Uri("https://localhost:44348");
                HttpRequestMessage req = new HttpRequestMessage();
                req.Method = HttpMethod.Post;
                var dataRequest = new RequestDTO
                {
                    DateCreated = DateTime.Now,
                    Data = encryptMessage,
                    IdSecret = txtSecret.Text
                };
                string content = JsonSerializer.Serialize(dataRequest);
                req.Content = new StringContent(content);
                req.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                req.RequestUri = new Uri("api/Monitor/StatusCard",UriKind.Relative);
                var result = await httpClient.SendAsync(req);
                txtMessageEncrypt.Text = encryptMessage;
                txtDescriptMessage.Text = await result.Content.ReadAsStringAsync();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
        }
    }
}
