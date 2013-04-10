using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.ViewModel
{
    public class FilteredListViewModel : INotifyPropertyChanged
    {
        private int _selectedFilter = 0;
        private readonly string[] _filters;
        private Model.EntrepriseEntities1 _context;
        public FilteredListViewModel(Model.EntrepriseEntities1 context)
        {
            _context = context;
            _filters = "Tout le staff,10$ et moins,anniversaire".Split(',');
        }
        public IEnumerable<object> FilteredList
        {
            get
            {
                switch (this._selectedFilter)
                {
                    case 0:
                        return from employee in _context.Employees
                               select new
                               {
                                   Prénom = employee.First_Name,
                                   Nom = employee.Last_Name
                               };
                    case 1:
                        return from product in _context.Products
                               where product.Unit_Price < 10.0m
                               select new
                               {
                                   Produit = product.Product_Name,
                                   Prix = product.Unit_Price
                               };
                    case 2:
                        var today = DateTime.Today;
                        return from employee in _context.Employees
                               let Age = today - employee.Birth_Date.Value
                               where employee.Birth_Date.Value.Month == '1'
                               select new
                               {
                                   Prénom = employee.First_Name,
                                   Nom = employee.Last_Name,
                                   Jour = employee.Birth_Date.Value.Day
                               };
                    default:
                        return new string[] { "(Not implemented filter)" };
                }
            }
        }
        public IEnumerable<String> Filters
        {
            get { return _filters; }
        }
        public int SelectedFilter
        {
            get { return this._selectedFilter; }
            set
            {
                this._selectedFilter = value;
                if (PropertyChanged != null)
                    PropertyChanged(this,
                    new PropertyChangedEventArgs("FilteredList")
                    );
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
