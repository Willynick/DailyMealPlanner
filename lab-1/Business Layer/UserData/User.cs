using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyMealPlanner.Utility;
using DailyMealPlanner.Business_Layer.Validator;
using DailyMealPlanner.Data_Layer;

namespace DailyMealPlanner.Business_Layer.UserData
{
    public class User
    {
        private UserValidator userValidator = new UserValidator();

        private double weight;
        public double Weight
        {
            get { return weight; }
            set
            {
                userValidator.Errors.Clear();
                bool isValid = userValidator.ValidateWeight(value);

                if (isValid)
                {
                    weight = value;
                }
            }
        }

        private double height;
        public double Height
        {
            get { return height; }
            set
            {
                userValidator.Errors.Clear();
                bool isValid = userValidator.ValidateHeight(value);

                if (isValid)
                {
                    height = value;
                }
            }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                userValidator.Errors.Clear();
                bool isValid = userValidator.ValidateAge(value);

                if (isValid)
                {
                    age = value;
                }
            }
        }

        private DailyActivity _dailyActivity;
        public DailyActivity dailyActivity
        {
            get { return _dailyActivity; }
            set { _dailyActivity = value; }
        }

        public void Update(UserInfo user)
        {
            this.weight = user.Weight;
            this.height = user.Height;
            this.age = user.Age;
            this.dailyActivity = user.dailyActivity;
        }


        public void UpdateUserCalculations()
        {
            userCalculations.Update(this);
        }

        public User()
        {
            
        }

        public User(double weight, double height, int age, DailyActivity dailyActivity)
        {
            UserInfo user = new UserInfo(weight, height, age, dailyActivity);
            ValidateAllInformation(user);
        }

        public User(UserInfo user)
        {

            ValidateAllInformation(user);
        }

        public void ValidateAllInformation(UserInfo user)
        {
            bool isParamsValid = userValidator.Validate(user);

            Update(user);
        }

        public UserCalculations userCalculations = new UserCalculations();

        public void ChangeWeight(double weight)
        {
            bool isParamsValid = userValidator.ValidateWeight(weight);

            if (isParamsValid != false)
            {
                this.weight = weight;
            }
        }

        public void ChangeHeight(double height)
        {
            bool isParamsValid = userValidator.ValidateWeight(height);

            if (isParamsValid != false)
            {
                this.height = height;
            }
        }

        public void ChangeAge(int age)
        {
            bool isParamsValid = userValidator.ValidateAge(age);

            if (isParamsValid != false)
            {
                this.age = age;
            }
        }

        public void ChangeDailyActivity(DailyActivity dailyActivity)
        {
            this.dailyActivity = dailyActivity;
        }

    }
}
