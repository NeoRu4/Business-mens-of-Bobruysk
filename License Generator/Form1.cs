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

namespace License_Generator
{
	public partial class Form1 : Form
	{
		private LicenseLib License { get; set; }
		SaveFileDialog saveFileDialog1;
		OpenFileDialog openFileDialog1;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			saveFileDialog1 = new SaveFileDialog();
			saveFileDialog1.Filter = "Key files(*.xml)|*.xml";
			saveFileDialog1.RestoreDirectory = true;
			saveFileDialog1.FileName = "bonjour";

			openFileDialog1 = new OpenFileDialog();
			openFileDialog1.Filter = "Key files(*.xml)|*.xml|All files (*.*)|*.*";
			openFileDialog1.RestoreDirectory = true;


			button2.Enabled = false;

			label4.Text = "приватный ключ не выбран";

		}

		private void button1_Click(object sender, EventArgs e)
		{
			License = new LicenseLib();
			label4.Text = "private key:"+License.secretKey;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (License != null)
			{
				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					var dto = new LicenseDto();
					dto.LicenseeName = textBox2.Text;
					dto.ValidUntil = dateTimePicker1.Value;
					dto.AllowedFeatures = new List<string>();
					foreach (var val in textBox1.Text.Split(','))
						dto.AllowedFeatures.Add(val.Trim());

					License.CreateLicenseFile(dto, saveFileDialog1.FileName);

					MessageBox.Show("Лицензия создана:"+ saveFileDialog1.FileName);

				}
			}
		}

		private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
		{

		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			if (textBox1.Text.Length > 1 && textBox2.Text.Length > 1 && License != null) 
					button2.Enabled = true;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			if (textBox1.Text.Length > 1 && textBox2.Text.Length > 1 && License != null)
				button2.Enabled = true;
		}

		private void label4_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				
				string key = System.IO.File.ReadAllText(openFileDialog1.FileName);						
				License = new LicenseLib(key);
				label4.Text = "private key:" + key;						
			}
		}
	}
}
