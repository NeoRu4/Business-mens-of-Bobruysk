using System;
using System.Collections.Generic;
using System.Text;

namespace business
{
	public class Subject
	{
		//ИНН
		public int ITaxNum { get; set; }

		//ФИО предпренимателя
		public string Name { get; set; }

		//дата регестрации
		public DateTime StartDate { get; set; }

		//ОКВЭД
		public List<int> Activities { get; set; }
		//тип налогообложения
		public TaxTypes TaxType { get; set; }

		public Subject()
		{
			ITaxNum = 0;
			Name = "";
			StartDate = DateTime.Now;
			Activities = new List<int>();
			TaxType = new TaxTypes();
		}


		public string ActivitiesToString()
		{
			return String.Join(",",Activities);
		}

		public void ActivitiesFromString(string str)
		{
			string[] nubmers = str.Split(',');

			int num;
			Activities.Clear();
			foreach (var n in nubmers)
				if ((num = Convert.ToInt32(n))!=0)
					Activities.Add(num);
		}

		public static bool DateCorrect(string dateString)
		{
			bool dateCorrect = true;
			if (dateString == "") { return dateCorrect; }
			try
			{
				Convert.ToDateTime(dateString);
			}
			catch
			{
				dateCorrect = false;
			}
			return dateCorrect;
		}

	}

	public enum TaxTypes
	{
		УСН,
		ОСНО,
		ПСН,
		ЕНВД
	}


	public class ActivitiesClass //отдельная база
	{
		public int id { get; set; }
		public string name { get; set; }
	}
}
