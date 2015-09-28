/*using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

[System.Serializable]
public class Serializable_BalanceSheet {

        [System.Xml.Serialization.XmlElement("StockNumber")]
        public string StockNumber { get; set; }

        [System.Xml.Serialization.XmlElement("Make")]
        public string Make { get; set; }

        [System.Xml.Serialization.XmlElement("Model")]
        public string Model { get; set; }

}


/*[System.Serializable]
[System.Xml.Serialization.XmlRoot("BalanceSheet_Collection")]
public class BalanceSheet_Collection
{
    [XmlArray("BalanceSheets")]
    [XmlArrayItem("BalanceSheet", typeof(Serializable_BalanceSheet))]
    public Serializable_BalanceSheet[] balanceSheet { get; set; }
}*/
/*CarCollection cars = null;
string path = "cars.xml";

XmlSerializer serializer = new XmlSerializer(typeof(CarCollection));

StreamReader reader = new StreamReader(path);
cars = (CarCollection)serializer.Deserialize(reader);
reader.Close();*/