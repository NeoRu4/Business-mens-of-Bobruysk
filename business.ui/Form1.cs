using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace business.ui
{
	public partial class Form1 : Form
	{
		public string CurFileName = "base.bu";
		public List<Subject> businesMens;
		public int countElements { get; private set; }

		private void addToViewList(Subject sbj)
		{
			businesMens.Add(sbj);
			listView1.Items.Add(new ListViewItem(new string[] {
				sbj.ITaxNum.ToString(),
				sbj.Name,
				sbj.StartDate.ToString("dd.MM.yyyy"),
				sbj.TaxType.ToString()
			}));
			countElements++;
		}

		public Form1()
		{
			businesMens = new List<Subject>();
			countElements = 0;
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			openFileDialog1.FileName = CurFileName;
			openFileDialog1.Filter = "Файл базы (*.bu)|*.bu";
			openFileDialog1.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


			listView1.Columns.Add("1", "ИНН", 100);
			listView1.Columns.Add("2", "ФИО",200);
			listView1.Columns.Add("3", "дата регестрации", 130);
			listView1.Columns.Add("4", "налог", 130);
			button3.Enabled = false;

		}

		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			this.Activate();

			string fileName = openFileDialog1.FileNames[0];
			List<Subject> buffer = new List<Subject>();
			Serializator.ReadFile(fileName, ref buffer);

			// reload list
			listView1.Items.Clear();
			businesMens = new List<Subject>();
			countElements = 0;

			foreach (var v in buffer)
				addToViewList(v);


			CurFileName = fileName;
			openFileDialog1.FileName = CurFileName;
			
		}


		private void button1_Click(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();

		}

		private void button2_Click(object sender, EventArgs e)
		{
			var inforow = new addInfo();
			var dialog = inforow.ShowDialog(this);
			if (dialog == DialogResult.OK)
			{
				addToViewList(inforow.mainSubject);
			}

		}


		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0]!= null)
				button3.Enabled = true;
			else
				button3.Enabled = false;
		}

		private void button3_Click(object sender, EventArgs e)
		{
				var id = listView1.SelectedItems[0].Index;
				var inforow = new addInfo(businesMens[id]);
				var dialog = inforow.ShowDialog(this);
				if (dialog == DialogResult.OK)
				{
					businesMens[id] = inforow.mainSubject;
					listView1.Items[id] = new ListViewItem(new string[] {
						businesMens[id].ITaxNum.ToString(),
						businesMens[id].Name,
						businesMens[id].StartDate.ToString("dd.MM.yyyy"),
						businesMens[id].TaxType.ToString()
					});
				}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			Serializator.WriteFile(CurFileName, businesMens);
			MessageBox.Show(CurFileName+" сохранён", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}


	}
}
