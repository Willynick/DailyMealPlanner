using DailyMealPlanner.Business_Layer.CategoryData;
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
    public class CategoryVM : OnPropertyChangedClass
    {
        private DataAccess dataAccess;

        private static CategoryVM thisInstance;

        public static CategoryVM getInstance()
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

                    Category.FillInfo(MainWindow.selectedCategory);
                    OnPropertyChanged("Category");
                }
                else
                {
                    VisibilityAddButton = Visibility.Visible;
                    VisibilityEditButton = Visibility.Collapsed;
                }
            }
        }

        public CategoryVM()
        {
            dataAccess = new DataAccess();

            thisInstance = this;         
        }

        public static AddCategoryWindow addCategoryWindow = new AddCategoryWindow();

        public CategoryInfo _Category = new CategoryInfo();
        public CategoryInfo Category
        {
            get { return _Category; }
            set
            {
                _Category = value;
                OnPropertyChanged("Category");
            }
        }

        private RelayCommand browseCommand;
        public RelayCommand BrowseCommand
        {
            get
            {
                return browseCommand ??
                  (browseCommand = new RelayCommand(obj =>
                  {
                      string IconUri = "";

                      //dataAccess.AddCategory()
                      Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                      dlg.FileName = ""; // Default file name
                                         // dlg.DefaultExt = ".png"; // Default file extension
                      dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                   "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                   "Portable Network Graphic (*.png)|*.png";   // Filter files by extension

                      // Show open file dialog box
                      Nullable<bool> result = dlg.ShowDialog();

                      // Process open file dialog box results
                      if (result == true)
                      {
                          // Open document
                          IconUri = dlg.FileName;
                      }

                      if (IconUri != "")
                      {
                          Category.IconUri = IconUri;
                      }
                      dlg = null;
                  }));
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

                      dataAccess.AddCategory(Category);
                      MessageBox.Show("Category added successfull");

                      addCategoryWindow.Hide();

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
                      
                      if (dataAccess.EditCategory(MainWindow.selectedCategory, Category))
                      {
                          MessageBox.Show("Category edit successfull");
                      }

                      addCategoryWindow.Hide();

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
