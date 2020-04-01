using DailyMealPlanner.Business_Layer.UserData;
using DailyMealPlanner.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMealPlanner.Data_Layer
{
    interface IUserDao
    {
        void SetWeight(double weight);
        void SetHeight(double height);
        void SetAge(int age);
        void SetDailyActivity(DailyActivity dailyActivity);
        User GetUser();
    }
}
