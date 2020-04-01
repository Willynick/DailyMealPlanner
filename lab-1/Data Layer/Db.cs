using DailyMealPlanner.Business_Layer.CategoryData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DailyMealPlanner.Data_Layer
{
    [Serializable]
    public class Db
    {
        [XmlElement]
        public CategoryInfo[] Category;

        [XmlIgnore]
        public ObservableCollection<CategoryClass> categories { get; set; } = new ObservableCollection<CategoryClass>();

        public Db()
        {

        }
    }
}
