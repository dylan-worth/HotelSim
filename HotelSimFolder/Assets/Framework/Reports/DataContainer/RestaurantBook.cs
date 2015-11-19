using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("RestaurantBooks")]
[XmlInclude(typeof(RestaurantBook))] // include type class restaurantbook.
public class RestaurantBookList       //Class holding a list of restaurantbook. Used to storing and loading purposes.
{
    [XmlArray("RestaurantBookArray")]
    public List<RestaurantBook> restaurantBooklist = new List<RestaurantBook>();

    [XmlElement("Listname")]
    public string Listname { get; set; }

    // Constructor
    public RestaurantBookList() { }

    public RestaurantBookList(string name)
    {
        this.Listname = name;
    }

    public void Add(RestaurantBook restaurantBook)
    {
        restaurantBooklist.Add(restaurantBook);
    }

}
[System.Serializable]
[XmlRoot("RestaurantBook")]
public class RestaurantBook
{
    public date startDate;
    public int numbOfDays;
    public int restaurantLevel;
    public int barLevel;

    public int numberOfStaff;
    public float staffProductivity;
    public float productivityNeeded;

    public float totalFoodSales;
    public float totalBeverageSales;
    //prices at start
    public float foodPrice;
    public float beveragePrice;

    public float conditionStart;
    public float conditionEnd;

    public int lostCustomers;
    public float staffCost;
    public float GetRevenue()
    {
        float total = totalFoodSales + totalBeverageSales;
        return total;
    }

   

    public RestaurantBook() { }

    public RestaurantBook(date sD, int nOD, int rL, int bL, int nOS, float fP, float bP, float cnd)
    {
        this.startDate = sD;
        this.numbOfDays = nOD;
        this.restaurantLevel = rL;
        this.barLevel = bL;
        this.numberOfStaff = nOS;
        this.foodPrice = fP;
        this.beveragePrice = bP;
        this.conditionStart = cnd;
        //will be assign later outside of constructor.
        this.staffProductivity = 0;
        this.productivityNeeded = 0;
        this.totalFoodSales = 0;
        this.totalBeverageSales = 0;
        this.conditionEnd = 0;
        this.lostCustomers = 0;
        this.staffCost = 0;
    }

}

