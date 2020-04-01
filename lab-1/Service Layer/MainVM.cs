using DailyMealPlanner.Business_Layer.UserData;
using DailyMealPlanner.Data_Layer;
using DailyMealPlanner.Utility;
using DailyMealPlanner.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DailyMealPlanner.Business_Layer.CategoryData;
using DailyMealPlanner.Business_Layer.ProductData;
using System.Windows;
using DailyMealPlanner.Business_Layer.Validator;
using System.Windows.Media;

namespace DailyMealPlanner.Service_Layer
{
    public class MainVM : OnPropertyChangedClass
    {
        private DataAccess dataAccess;

        public MainVM()
        {
            dataAccess = new DataAccess();

            _user = dataAccess.GetUser();

            _MealTimes = dataAccess.GetMealTimes();
            _Categories = dataAccess.GetCategories();
        }

        private User _user;
        public User user
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged("user");
            }
        }

        public double getWeight
        {
            get
            {
                return user.Weight;
            }
            set
            {
                dataAccess.SetWeight(value);
                OnPropertyChanged("getARM");
                OnPropertyChanged("getBMR");
                OnPropertyChanged("getDailyCaloriesRate");
                OnPropertyChanged("NumberOfCalories");
                OnPropertyChanged("Overspending");
            }
        }

        public double getHeight
        {
            get
            {
                return user.Height;
            }
            set
            {
                dataAccess.SetHeight(value);
                OnPropertyChanged("getARM");
                OnPropertyChanged("getBMR");
                OnPropertyChanged("getDailyCaloriesRate");
                OnPropertyChanged("NumberOfCalories");
                OnPropertyChanged("Overspending");
            }
        }

        public int getAge
        {
            get
            {
                return user.Age;
            }
            set
            {
                dataAccess.SetAge(value);
                OnPropertyChanged("getARM");
                OnPropertyChanged("getBMR");
                OnPropertyChanged("getDailyCaloriesRate");
                OnPropertyChanged("NumberOfCalories");
                OnPropertyChanged("Overspending");
            }
        }

        public DailyActivity getDailyActivity
        {
            get
            {
                return user.dailyActivity;
            }
            set
            {
                dataAccess.SetDailyActivity(value);
                OnPropertyChanged("getARM");
                OnPropertyChanged("getBMR");
                OnPropertyChanged("getDailyCaloriesRate");
                OnPropertyChanged("NumberOfCalories");
                OnPropertyChanged("Overspending");
            }
        }

        public double getARM
        {
            get
            {
                return user.userCalculations.ARM;
            }
        }
        public double getBMR
        {
            get
            {
                return user.userCalculations.BMR;
            }
        }
        public double getDailyCaloriesRate
        {
            get
            {
                return Math.Round(user.userCalculations.DailyCaloriesRate, 3);
            }
        }

        public double getProtein
        {
            get
            {
                if (MainWindow.selectedProductFromMealTime != null)
                {
                    return MainWindow.selectedProductFromMealTime.Protein;
                }
                else
                {
                    return 0.0;
                }
            }
        }

        public double getFats
        {
            get
            {
                if (MainWindow.selectedProductFromMealTime != null)
                {
                    return MainWindow.selectedProductFromMealTime.Fats;
                }
                else
                {
                    return 0.0;
                }
            }
        }

        public double getCarbs
        {
            get
            {
                if (MainWindow.selectedProductFromMealTime != null)
                {
                    return MainWindow.selectedProductFromMealTime.Carbs;
                }
                else
                {
                    return 0.0;
                }
            }
        }

        public double getCalories
        {
            get
            {
                if (MainWindow.selectedProductFromMealTime != null)
                {
                    return MainWindow.selectedProductFromMealTime.Calories;
                }
                else
                {
                    return 0.0;
                }
            }
        }

        private string searchCondition;
        public string SearchCondition
        {
            get
            {
                return searchCondition;
            }
            set
            {
                searchCondition = value;

                if (searchCondition != "")
                {
                    Categories = dataAccess.SearchInfo(searchCondition);
                }
                else
                {
                    Categories = dataAccess.GetCategories();
                }
            }
        }

        private bool enabledTreeViewCategories = true;
        public bool EnabledTreeViewCategories
        {
            get
            {
                return enabledTreeViewCategories;
            }
            set
            {
                enabledTreeViewCategories = value;
                OnPropertyChanged("EnabledTreeViewCategories");
            }
        }

        private bool enabledTreeViewMealTimes = true;
        public bool EnabledTreeViewMealTimes
        {
            get
            {
                return enabledTreeViewMealTimes;
            }
            set
            {
                enabledTreeViewMealTimes = value;
                OnPropertyChanged("EnabledTreeViewMealTimes");
            }
        }

        public double WeightSelectedProduct
        {
            get
            {
                if (MainWindow.selectedProductFromMealTime != null)
                {
                    MainWindow.selectedProductFromMealTime.Gramms = Math.Round(MainWindow.selectedProductFromMealTime.Gramms, 3);
                    return MainWindow.selectedProductFromMealTime.Gramms;
                }
                else
                {
                    return 100;
                }
            }
            set
            {
                MainWindow.selectedProductFromMealTime.Gramms = value;
                MainWindow.selectedProductFromMealTime.Calories = MainWindow.selectedProductFromMealTime.CalloriesForOneGramm * value;

                OnPropertyChanged("WeightSelectedProduct");
                OnPropertyChanged("NumberOfCalories");
                OnPropertyChanged("Overspending");
            }
        }

        public double Overspending
        {
            get
            {
                double overspending = dataAccess.CalculateNumberOfCalories() - getDailyCaloriesRate;
                if (overspending > 0.0)
                {
                    ForegroundOfProgressBar = Brushes.DarkRed;
                    return Math.Round(overspending, 3);
                }
                ForegroundOfProgressBar = Brushes.Green;
                return 0;
            }
            set
            {
                OnPropertyChanged("Overspending");
            }
        }

        private Brush foregroundOfProgressBar = Brushes.Green;
        public Brush ForegroundOfProgressBar
        {
            get
            {
                return foregroundOfProgressBar;
            }
            set
            {
                foregroundOfProgressBar = value;
                OnPropertyChanged("ForegroundOfProgressBar");
            }
        }

        public double NumberOfCalories
        {
            get
            {
                if (user.userCalculations.DailyCaloriesRate != 0)
                {
                    return (dataAccess.CalculateNumberOfCalories() / user.userCalculations.DailyCaloriesRate) * 100;
                }
                else
                {
                    return 0.0;
                }
            }
            set
            {
                OnPropertyChanged("NumberOfCalories");
                OnPropertyChanged("Overspending");
            }
        }

        public ObservableCollection<CategoryClass> _Categories;
        public ObservableCollection<CategoryClass> Categories
        {
            get
            {
                return _Categories;
            }
            set
            {
                _Categories = value;
                OnPropertyChanged("Categories");
            }
        }

        private ObservableCollection<MealTime> _MealTimes;
        public ObservableCollection<MealTime> MealTimes
        {
            get
            {
                return _MealTimes;
            }
            set
            {
                _MealTimes = value;
                OnPropertyChanged("MealTimes");
            }
        }

        private RelayCommand addCategoryCommand;
        public RelayCommand AddCategoryCommand
        {
            get
            {
                return addCategoryCommand ??
                  (addCategoryCommand = new RelayCommand(obj =>
                  {
                      CategoryVM.addCategoryWindow.Show();
                      CategoryVM.getInstance().EditMode = false;

                      EnabledTreeViewCategories = false;

                      CategoryVM.getInstance().EditEventHandler += CategoriesShowChangesEvent;

                  }));
            }
        }

        private RelayCommand removeCategoryCommand;
        public RelayCommand RemoveCategoryCommand
        {
            get
            {
                return removeCategoryCommand ??
                  (removeCategoryCommand = new RelayCommand(obj =>
                  {
                      if (MainWindow.selectedCategory == null)
                      {
                          MessageBox.Show("Select a category to delete");
                      }
                      else
                      {
                          dataAccess.RemoveCategory(MainWindow.selectedCategory);
                          OnPropertyChanged("Categories");
                      }
                  }));
            }
        }
       //-----------------------------------------------------------------------------------------------------
        private RelayCommand addProductCommand;
        public RelayCommand AddProductCommand
        {
            get
            {
                return addProductCommand ??
                  (addProductCommand = new RelayCommand(obj =>
                  {

                      if (MainWindow.selectedCategory == null)
                      {
                          MessageBox.Show("Select a category to add product");
                      }
                      else
                      {
                          ProductVM.addProductWindow.Show();
                          ProductVM.getInstance().EditMode = false;

                          EnabledTreeViewCategories = false;

                          ProductVM.getInstance().EditEventHandler += CategoriesShowChangesEvent;
                      }
                  }));
            }
        }

        private RelayCommand removeProductCommand;
        public RelayCommand RemoveProductCommand
        {
            get
            {
                return removeProductCommand ??
                  (removeProductCommand = new RelayCommand(obj =>
                  {
                      if (MainWindow.selectedProduct == null)
                      {
                          MessageBox.Show("Select a product to delete");
                      }
                      else
                      {
                          dataAccess.RemoveProduct(MainWindow.selectedProduct);
                          OnPropertyChanged("Categories");
                      }
                  }));
            }
        }

        private RelayCommand addProductToMealtime;
        public RelayCommand AddProductToMealtime
        {
            get
            {
                return addProductToMealtime ??
                  (addProductToMealtime = new RelayCommand(obj =>
                  {
                      if (MainWindow.selectedMealTime == null || MainWindow.selectedProduct == null)
                      {
                          MessageBox.Show("Select a product and mealtime to add to mealtime");
                      }
                      else
                      {
                          dataAccess.AddProductToMealTime(MainWindow.selectedMealTime, MainWindow.selectedProduct);
                          OnPropertyChanged("NumberOfCalories");
                          OnPropertyChanged("Overspending");
                          OnPropertyChanged("MealTimes");

                          OnPropertyChanged("getProtein");
                          OnPropertyChanged("getFats");
                          OnPropertyChanged("getCarbs");
                          OnPropertyChanged("getCalories");
                      }
                  }));
            }
        }

        private RelayCommand removeProductFromMealTime;
        public RelayCommand RemoveProductFromMealTime
        {
            get
            {
                return removeProductFromMealTime ??
                  (removeProductFromMealTime = new RelayCommand(obj =>
                  {
                      if (MainWindow.selectedProductFromMealTime == null)
                      {
                          MessageBox.Show("Select a product to delete");
                      }
                      else
                      {
                          dataAccess.RemoveProductFromMealTime(MainWindow.selectedProductFromMealTime);
                          OnPropertyChanged("NumberOfCalories");
                          OnPropertyChanged("Overspending");
                          OnPropertyChanged("MealTimes");

                          OnPropertyChanged("getProtein");
                          OnPropertyChanged("getFats");
                          OnPropertyChanged("getCarbs");
                          OnPropertyChanged("getCalories");
                      }
                  }));
            }
        }

        private RelayCommand addMealTimeCommand;
        public RelayCommand AddMealTimeCommand
        {
            get
            {
                return addMealTimeCommand ??
                  (addMealTimeCommand = new RelayCommand(obj =>
                  {
                      MealTimeVM.getInstance().EditMode = false;
                      MealTimeVM.addMealTimeWindow.Show();

                      EnabledTreeViewMealTimes = false;

                      MealTimeVM.getInstance().EditEventHandler += MealTimesShowChangesEvent;
                  }));
            }
        }

        private RelayCommand removeMealTimeCommand;
        public RelayCommand RemoveMealTimeCommand
        {
            get
            {
                return removeMealTimeCommand ??
                  (removeMealTimeCommand = new RelayCommand(obj =>
                  {
                      if (MainWindow.selectedMealTime == null)
                      {
                          MessageBox.Show("Select a MealTime to delete");
                      }
                      else
                      {
                          dataAccess.RemoveMealTime(MainWindow.selectedMealTime);
                          OnPropertyChanged("NumberOfCalories");
                          OnPropertyChanged("Overspending");
                          OnPropertyChanged("MealTimes");
                      }
                  }));
            }
        }

        private RelayCommand editMealTimeCommand;
        public RelayCommand EditMealTimeCommand
        {
            get
            {
                return editMealTimeCommand ??
                  (editMealTimeCommand = new RelayCommand(obj =>
                  {
                      if (SelectedIndexClass.IsSelectedMealTimeItem())
                      {
                          MessageBox.Show("Select an item to edit");
                      }
                      else
                      {
                          EnabledTreeViewMealTimes = false;

                          if (SelectedIndexClass.IsSelectedProductFromMealTime())
                          {
                              ProductVM.getInstance().isProductFromMealTime = true;
                              ProductVM.getInstance().EditMode = true;

                              ProductVM.addProductWindow.Show();

                              ProductVM.getInstance().EditEventHandler += MealTimesShowChangesEvent;
                          }
                          else
                          {
                              MealTimeVM.getInstance().EditMode = true;
                              MealTimeVM.addMealTimeWindow.Show();

                              MealTimeVM.getInstance().EditEventHandler += MealTimesShowChangesEvent;
                          }
                      }
                  }));
            }
        }

        private RelayCommand editItemCommand;
        public RelayCommand EditItemCommand
        {
            get
            {
                return editItemCommand ??
                  (editItemCommand = new RelayCommand(obj =>
                  {
                      if (SelectedIndexClass.IsSelectedItem())
                      {
                          MessageBox.Show("Select an item to edit");
                      }
                      else
                      {
                          EnabledTreeViewCategories = false;

                          if (SelectedIndexClass.IsSelectedProduct())
                          {                             
                              ProductVM.getInstance().isProductFromMealTime = false;
                              ProductVM.getInstance().EditMode = true;

                              ProductVM.addProductWindow.Show();

                              ProductVM.getInstance().EditEventHandler += CategoriesShowChangesEvent;

                          }
                          else
                          {                            
                               CategoryVM.getInstance().EditMode = true;
                               CategoryVM.addCategoryWindow.Show();

                               CategoryVM.getInstance().EditEventHandler += CategoriesShowChangesEvent;
                          }                        
                      }
                  }));
            }
        }

        ////
        private RelayCommand closingCommand;
        public RelayCommand ClosingCommand
        {
            get
            {
                return closingCommand ??
                  (closingCommand = new RelayCommand(obj =>
                  {
                      CategoryVM.addCategoryWindow.Close();
                      ProductVM.addProductWindow.Close();
                      MealTimeVM.addMealTimeWindow.Close();
                  }));
            }
        }

        private RelayCommand exportProducts;
        public RelayCommand ExportProducts
        {
            get
            {
                return exportProducts ??
                  (exportProducts = new RelayCommand(obj =>
                  {
                      dataAccess.GetProductsFile();
                  }));
            }
        }

        private RelayCommand exportMealTimes;
        public RelayCommand ExportMealTimes
        {
            get
            {
                return exportMealTimes ??
                  (exportMealTimes = new RelayCommand(obj =>
                  {
                      dataAccess.GetMealtimesFile();
                  }));
            }
        }

        private RelayCommand exportPDF;
        public RelayCommand ExportPDF
        {
            get
            {
                return exportPDF ??
                  (exportPDF = new RelayCommand(obj =>
                  {
                        dataAccess.GetPDFFile();
                  }));
            }
        }

        public void CategoriesShowChangesEvent(object sender, EventArgs e)
        {
            MainWindow.previousSelectedCategory = MainWindow.selectedCategory;
            MainWindow.selectedCategory = null;
            MainWindow.previousSelectedProduct = MainWindow.selectedProduct;
            MainWindow.selectedProduct = null;

            OnPropertyChanged("Categories");
            EnabledTreeViewCategories = true;
        }

        public void MealTimesShowChangesEvent(object sender, EventArgs e)
        {
            MainWindow.previousSelectedMealTime = MainWindow.selectedMealTime;
            MainWindow.selectedMealTime = null;
            MainWindow.previousSelectedProductFromMealTime = MainWindow.selectedProductFromMealTime;
            MainWindow.selectedProductFromMealTime = null;

            OnPropertyChanged("MealTimes");
            OnPropertyChanged("NumberOfCalories");

            EnabledTreeViewMealTimes = true;
        }

        public void UpdateCharacteristicsOfProduct()
        {
            OnPropertyChanged("getProtein");
            OnPropertyChanged("getFats");
            OnPropertyChanged("getCarbs");
            OnPropertyChanged("getCalories");
        }
    }
}
