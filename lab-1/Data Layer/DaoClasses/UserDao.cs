using DailyMealPlanner.Business_Layer.UserData;
using DailyMealPlanner.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMealPlanner.Data_Layer
{
    public class UserDao : IUserDao
    {
        DataBase dataBase = new DataBase();

        public void SetWeight(double weight)
        {
            dataBase.getUser().ChangeWeight(weight);
            dataBase.getUser().UpdateUserCalculations();
        }

        public void SetHeight(double height)
        {
            dataBase.getUser().ChangeHeight(height);
            dataBase.getUser().UpdateUserCalculations();
        }

        public void SetAge(int age)
        {
            dataBase.getUser().ChangeAge(age);
            dataBase.getUser().UpdateUserCalculations();
        }

        public void SetDailyActivity(DailyActivity dailyActivity)
        {
            dataBase.getUser().ChangeDailyActivity(dailyActivity);
            dataBase.getUser().UpdateUserCalculations();
        }

        public User GetUser()
        {
            return dataBase.getUser();
        }
    }
}
