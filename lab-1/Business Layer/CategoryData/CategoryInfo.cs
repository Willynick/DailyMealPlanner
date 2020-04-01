using DailyMealPlanner.Business_Layer.ProductData;
using DailyMealPlanner.Business_Layer.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DailyMealPlanner.Business_Layer.CategoryData
{
    [Serializable]
    public class CategoryInfo
    {
        [XmlAttribute]
        public string name { get; set; }

        [XmlAttribute]
        public string description { get; set; }

        [XmlAttribute]
        public bool hidden { get; set; }

        [XmlIgnore]
        public string IconUri { get; set; } = "/Images/mealtime.png";

        public CategoryInfo()
        {

        }

        public CategoryInfo(string name, string description, bool hidden)
        {
            this.name = name;
            this.description = description;
            this.hidden = hidden;
        }

        public void FillInfo(CategoryClass category)
        {
            this.name = category.name;
            this.description = category.description;
            this.hidden = category.hidden;
            this.IconUri = category.IconUri;
        }

        [XmlElement]
        public ProductInfo[] Product;
    }
}
