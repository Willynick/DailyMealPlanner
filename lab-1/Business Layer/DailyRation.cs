using DailyMealPlanner.Data_Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DailyMealPlanner.Business_Layer
{
    [Serializable]
    public class DailyRation
    {
        [XmlElement]
        public ObservableCollection<MealTime> mealTimes { get; set; } = new ObservableCollection<MealTime>();

        public DailyRation()
        {
            mealTimes.Add(new MealTime("Breakfast"));
            mealTimes.Add(new MealTime("Lunch"));
            mealTimes.Add(new MealTime("Dinner"));
        }
    }
}
