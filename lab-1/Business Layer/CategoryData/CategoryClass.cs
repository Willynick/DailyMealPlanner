using DailyMealPlanner.Business_Layer.ProductData;
using DailyMealPlanner.Business_Layer.Validator;
using DailyMealPlanner.Data_Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace DailyMealPlanner.Business_Layer.CategoryData
{
    public class CategoryClass : ICloneable
    {
       public CategoryValidator categoryValidator { get; set; } = new CategoryValidator();

        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description;
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }

        private bool _hidden = true;
        public bool hidden
        {
            get { return _hidden; }
            set { _hidden = value; }
        }

        public string IconUri { get; set; } = "/Images/mealtime.png";

        private CategoryClass()
        {

        }

        public object Clone()
        {
            ObservableCollection<ProductClass> copyProducts = new ObservableCollection<ProductClass>();

            for (int i = 0; i < this.products.Count; i++)
            {
                copyProducts.Add((ProductClass)this.products[i].Clone());
            }
            return new CategoryClass
            {
                name = this.name,
                description = this.description,
                hidden = this.hidden,
                products = copyProducts,
                IconUri = this.IconUri
            };
        }

        public CategoryClass(string name, string description, bool hidden)
        {
            CategoryInfo category = new CategoryInfo(name, description, hidden);

            ValidateAllInformation(category);
        }

        public CategoryClass(CategoryInfo category)
        {
            ValidateAllInformation(category);
        }

        public void ChangeName(string name)
        {
            this.name = name;
        }
        public void ChangeDescription(string description)
        {
            this.description = description;
        }

        private void Update(CategoryInfo category)
        {
            this.name = category.name;
            this.description = category.description;
            this.hidden = category.hidden;
            this.IconUri = category.IconUri;
        }

        public void ValidateAllInformation(CategoryInfo category)
        {

            bool isParamsValid = categoryValidator.Validate(category);

            Update(category);
        }


        public void ChangeInfo(string info, string nameofCharacteristic)
        {
            string error = "";

            bool isParamsValid = categoryValidator.ValidateInfo(info, nameofCharacteristic, out error);

            if (isParamsValid == false)
            {
                MessageBox.Show(categoryValidator.Errors[0].Error);
            }
            else
            {
                Update(info, nameofCharacteristic);
            }
        }

        public void Update(string info, string nameofCharacteristic)
        {
            switch (nameofCharacteristic)
            {
                case "Name":
                    this.name = name; break;
                case "Description":
                    this.description = description; break;
            }
        }


        public ObservableCollection<ProductClass> products { get; set; } = new ObservableCollection<ProductClass>();

        //-----------------------------------------------------------------------------------------------
    }
}
