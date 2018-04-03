using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;


namespace business.ui
{

	public partial class addInfo : Form
	{
		public Subject mainSubject;

		private InfoPlate INN;
		private InfoPlate FIO;
		private InfoPlate DATEREGIST;
		private InfoPlate OECVED;
		private ComboBox TAX;
		private Button buttonOK;

		private void addFunc(Subject info)
		{
			InitializeComponent();

			int posy = 10;
			Controls.Add(INN = new InfoPlate(posy, "ИНН", info.ITaxNum, "0000"));
			Controls.Add(FIO = new InfoPlate(posy += 35, "ФИО", info.Name));
			Controls.Add(DATEREGIST = new InfoPlate(posy += 35, "дата регестрации", info.StartDate.ToString("dd.MM.yyyy"), "00/00/0000"));
			Controls.Add(OECVED = new InfoPlate(posy += 35, "коды ОКВЭД( 21,... )", info.ActivitiesToString()));

			Controls.Add(TAX =  new ComboBox
			{
				SelectedText = "Тип налогообложения",
				Location = new Point(10, posy += 35),
				Size = new Size(300,10),
				Items =  {
					(TaxTypes.УСН).ToString(),
					(TaxTypes.ОСНО).ToString(),
					(TaxTypes.ПСН).ToString(),
					(TaxTypes.ЕНВД).ToString(),
				},
				SelectedIndex = (object)info.TaxType != null ? (int)info.TaxType : -1,
			});
			mainSubject = info;

			buttonOK = new Button()
			{
				Size = new Size(360, 25),
				Location = new Point(Width/2 - Size.Width/3, Height - Height/5),
				Anchor = AnchorStyles.Bottom,
				Text = "продолжить",
			};
			buttonOK.Click += buttonOK_Click;
			Controls.Add(buttonOK);
		}

		public addInfo(Subject info)
		{
			addFunc(info);
		}

		public addInfo()
		{
			Subject info = new Subject();
			addFunc(info);
		}

		private void addInfo_Load(object sender, EventArgs e)
		{
		}

		private int delay = 17;
		private DateTime time = DateTime.Now;
		private void buttonOK_Click(object sender, EventArgs e)
		{
			//задержка нажатия
			if (DateTime.Now > time)
				time = DateTime.Now.AddSeconds(2.5);
			else
				return;

			if (FIO.textBox.Text.Length < 3 || INN.textBox.Text.Length < 1 || !Subject.IntCorrect(INN.textBox.Text) || OECVED.textBox.Text.Length < 1 || !Subject.DateCorrect(DATEREGIST.textBox.Text))
			{
				for (int X = 1; X <= 24; X++)
				{
					var l = Math.Sin(X * 1.1) * Math.Pow(Math.E, -X / 6);
					buttonOK.Location = new Point(buttonOK.Location.X + Convert.ToInt32(l * 25), buttonOK.Location.Y);
					buttonOK.Update();
					Thread.Sleep(delay);

					buttonOK.Location = new Point(buttonOK.Location.X - Convert.ToInt32(l * 25), buttonOK.Location.Y);
					buttonOK.Update();
				}
			}
			else
			{
				mainSubject.Name = FIO.textBox.Text;
				mainSubject.ITaxNum = Convert.ToInt32(INN.textBox.Text);
				mainSubject.StartDate = Convert.ToDateTime(DATEREGIST.textBox.Text);
				mainSubject.ActivitiesFromString(OECVED.textBox.Text);
				mainSubject.TaxType = (TaxTypes)TAX.SelectedIndex;

				this.DialogResult = DialogResult.OK;
				this.Dispose();

			}

		}
	}

	public class InfoPlate : Panel
	{
		public MaskedTextBox textBox;
		public Label label;
		public InfoPlate(int y, string name, object text, string mask = "")
		{
			Location = new Point(10, y);
			AutoSize = true;
			AutoSizeMode = AutoSizeMode.GrowAndShrink;
			Margin = new Padding(10);
			Height = 20;
			BorderStyle = BorderStyle.None;

			label = new Label
			{
				Text = name,
				Location = new Point(0, 5),
				TextAlign = ContentAlignment.MiddleCenter,
				AutoSize = true,
			};

			textBox = new MaskedTextBox
			{
				Mask = mask,
				Text = text.ToString(),
				Size = new Size(320, 10),
				Location = new Point(130, 0),
			};

			Controls.Add(label);
			Controls.Add(textBox);
		}
	}
}
