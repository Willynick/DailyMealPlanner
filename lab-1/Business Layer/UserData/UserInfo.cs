using DailyMealPlanner.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMealPlanner.Business_Layer.UserData
{
    public class UserInfo
    {
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Age { get; set; }
        public DailyActivity dailyActivity { get; set; }

        public UserInfo()
        {

        }

        public UserInfo(double weight, double height, int age, DailyActivity _dailyActivity)
        {
            this.Weight = weight;
            this.Height = height;
            this.Age = age;
            this.dailyActivity = _dailyActivity;
        }
    }
}
