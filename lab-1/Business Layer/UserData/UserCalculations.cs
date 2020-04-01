using DailyMealPlanner.Business_Layer.UserData;
using DailyMealPlanner.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMealPlanner.Business_Layer.UserData
{
    public class UserCalculations
    {
        private double _ARM;
        public double ARM
        {
            get { return _ARM; }
            set { _ARM = value; }
        }

        private double _BMR;
        public double BMR
        {
            get { return _BMR; }
            set { _BMR = value; }
        }

        private double _DailyCaloriesRate;
        public double DailyCaloriesRate
        {
            get { return _DailyCaloriesRate; }
            set { _DailyCaloriesRate = value; }
        }

        public UserCalculations()
        {

        }

        public void Update(User user)
        {
            switch (user.dailyActivity)
            {
                case DailyActivity.Low:
                    _ARM = 1.2; break;
                case DailyActivity.Normal:
                    _ARM = 1.375; break;
                case DailyActivity.Average:
                    _ARM = 1.55; break;
                case DailyActivity.High:
                    _ARM = 1.725; break;
            }
            _BMR = 447.593 + 9.247 * user.Weight + 3.098 * user.Height - 4.330 * user.Age;
            _DailyCaloriesRate = _BMR + _ARM;
        }
    }
}
