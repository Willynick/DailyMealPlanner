using DailyMealPlanner;
using DailyMealPlanner.Business_Layer.ProductData;
using DailyMealPlanner.Business_Layer.Validator;
using DailyMealPlanner.Data_Layer;
using DailyMealPlanner.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyMealPlanner.Service_Layer
{
    public class ProductVM : OnPropertyChangedClass
    {
        private DataAccess dataAccess;

        private static ProductVM thisInstance;

        public static ProductVM getInstance()
        {
            return thisInstance;
        }

        private bool editMode = false;
        public bool EditMode
        {
            get
            {
                return editMode;
            }
            set
            {
                editMode = value;
                if (editMode)
                {
                    VisibilityAddButton = Visibility.Collapsed;
                    VisibilityEditButton = Visibility.Visible;

                    if (!isProductFromMealTime)
                    {
                        Product.FillInfo(MainWindow.selectedProduct);
                    }
                    else
                    {
                        Product.FillInfo(MainWindow.selectedProductFromMealTime);
                    }
                    OnPropertyChanged("Product");
                }
                else
                {
                    VisibilityAddButton = Visibility.Visible;
                    VisibilityEditButton = Visibility.Collapsed;
                }
            }
        }

        public bool isProductFromMealTime = false;

        public ProductVM()
        {
            dataAccess = new DataAccess();

            thisInstance = this;
        }

        public static AddProductWindow addProductWindow = new AddProductWindow();

        public ProductInfo _Product = new ProductInfo();
        public ProductInfo Product
        {
            get { return _Product; }
            set
            {
                _Product = value;
                OnPropertyChanged("Product");
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      //ProductClass product = new ProductClass(Product, new ProductValidator());
                      dataAccess.AddProduct(MainWindow.selectedCategory, Product);
                      MessageBox.Show("Product added successfull");
                      addProductWindow.Hide();

                      EditEvent();
                  }));
            }
        }

        public Visibility _VisibilityAddButton = Visibility.Visible;
        public Visibility VisibilityAddButton
        {
            get { return _VisibilityAddButton; }
            set
            {
                _VisibilityAddButton = value;
                OnPropertyChanged("VisibilityAddButton");
            }
        }

        public Visibility _VisibilityEditButton = Visibility.Collapsed;
        public Visibility VisibilityEditButton
        {
            get { return _VisibilityEditButton; }
            set
            {
                _VisibilityEditButton = value;
                OnPropertyChanged("VisibilityEditButton");
            }
        }

        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand(obj =>
                  {
                      bool noErrors = false;

                      if (!isProductFromMealTime)
                      {
                          noErrors = dataAccess.EditProduct(MainWindow.selectedProduct, Product);
                      }
                      else
                      {
                          noErrors = dataAccess.EditProductFromMealTime(MainWindow.selectedProductFromMealTime, Product);
                      }

                      if (noErrors == true)
                      {
                          MessageBox.Show("Product edit successfull");
                      }
                      addProductWindow.Hide();

                      EditEvent();
                  }));
            }
        }

        public event EventHandler EditEventHandler;
        protected virtual void EditEvent()
        {
            EditEventHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}
