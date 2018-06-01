using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace business
{
	public static class Serializator
	{

		public static void WriteFile<T>(string fileName, List<T> data)
		{
			using (var file = File.Create(fileName))
			{
				new XmlSerializer(typeof(List<T>)).Serialize(file, data);
			}
		}

		public static void ReadFile<T>(string fileName, ref List<T> data)
		{
			using (var file = File.OpenRead(fileName))
			{
				data = (List<T>) new XmlSerializer(typeof(List<T>)).Deserialize(file);
			}
		}

	}
}
