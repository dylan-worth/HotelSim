using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Data_Save_BalanceSheet_Collection")]
public class Data_Save_CTNBalanceSheet
{


	//public BalanceSheet[] Data_Save_BalanceSheets;
	[XmlArray("Data_Save_BalanceSheets"),XmlArrayItem("Data_Save_BalanceSheet")]
	public List<BalanceSheet> Data_Save_BalanceSheets = Reception.balanceSheets;
	
	public void Save(string path)
	{

		var serializer = new XmlSerializer(typeof(Data_Save_CTNBalanceSheet));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, this);
		}
	}
	
	public static Data_Save_CTNBalanceSheet Load(string path)
	{
		var serializer = new XmlSerializer(typeof(Data_Save_CTNBalanceSheet));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as Data_Save_CTNBalanceSheet;
		}
	}
	
	//Loads the xml directly from the given string. Useful in combination with www.text.
	public static Data_Save_CTNBalanceSheet LoadFromText(string text) 
	{
		var serializer = new XmlSerializer(typeof(Data_Save_CTNBalanceSheet));
		return serializer.Deserialize(new StringReader(text)) as Data_Save_CTNBalanceSheet;
	}


}