using Bakery.Core.DTOs;
using Bakery.Persistence;
using Bakery.Wpf.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bakery.Wpf.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<ProductDto> Products { get; }
        private IEnumerable<ProductDto> _products;
        private ProductDto _selectedProduct;
        private string _avgPrice;
        private string _filterPriceFrom;
        private string _filterPriceTo;

        public ProductDto SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }

        public string AvgPrice
        {
            get => _avgPrice;
            set
            {
                _avgPrice = value;
                OnPropertyChanged();
            }
        }

        public string FilterPriceFrom
        {
            get => _filterPriceFrom;
            set
            {
                _filterPriceFrom = value;
                OnPropertyChanged();
            }
        }

        public string FilterPriceTo
        {
            get => _filterPriceTo;
            set
            {
                _filterPriceTo = value;
                OnPropertyChanged();
            }
        }

        public ICommand CmdFilterProducts { get; set; }
        public ICommand CmdNewProduct { get; set; }
        public ICommand CmdEditProduct { get; set; }
        public ICommand CmdDeleteProduct { get; set; }


        public MainWindowViewModel(IWindowController controller) : base(controller)
        {
            Products = new ObservableCollection<ProductDto>();
            LoadCommands();
        }

        private void LoadCommands()
        {
            CmdFilterProducts = new RelayCommand(
                c => FilterProducts(),
                c => AllowFilter()
            );

            CmdNewProduct = new RelayCommand(
                async c => await CreateProduct(),
                c => true
            );

            CmdEditProduct = new RelayCommand(
                async c => await EditProduct(),
                c => SelectedProduct != null
            );

            CmdDeleteProduct = new RelayCommand(
                async c => await DeleteProduct(),
                c => true
            );
        }

        private void FilterProducts()
        {
            var from = double.Parse(FilterPriceFrom);
            var to = double.Parse(FilterPriceTo);
            var productsFiltered = _products
                .Where(product => product.Price >= from && product.Price <= to)
                .ToList();
            Products.Clear();
            productsFiltered.ForEach(Products.Add);
            AvgPrice = $"{Products.Select(product => (double?)product.Price).Average() ?? 0:f2}";
        }

        private bool AllowFilter()
        {
            if(string.IsNullOrEmpty(FilterPriceFrom) || string.IsNullOrEmpty(FilterPriceTo))
            {
                return false;
            }

            try
            {
                double from = double.Parse(FilterPriceFrom);
                double to = double.Parse(FilterPriceTo);
                return from < to;
            }
            catch(Exception)
            {
                return false;
            }
        }

        private async Task CreateProduct()
        {
            AddAndEditProductViewModel model = new AddAndEditProductViewModel(Controller, null);
            Controller.ShowWindow(model, true);
            await LoadProducts();
        }

        private async Task EditProduct()
        {
            AddAndEditProductViewModel model = new AddAndEditProductViewModel(Controller, SelectedProduct);
            Controller.ShowWindow(model, true);
            await LoadProducts();
        }

        private async Task DeleteProduct()
        {
            await using var uow = new UnitOfWork();
            await uow.Products.DeleteAsync(SelectedProduct);
            await uow.SaveChangesAsync();
            await LoadProducts();
        }

        public static async Task<MainWindowViewModel> Create(IWindowController controller)
        {
            var model = new MainWindowViewModel(controller);
            await model.LoadProducts();
            return model;
        }

        /// <summary>
        /// Produkte laden. Produkte können nach Preis gefiltert werden. 
        /// Preis liegt zwischen from und to
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private async Task LoadProducts()
        {
            await using UnitOfWork uow = new UnitOfWork();
            _products = await uow.Products.GetAllAsync();
            Products.Clear();
            _products
                .ToList()
                .ForEach(Products.Add);

            AvgPrice = $"{Products.Select(product => (double?)product.Price).Average() ?? 0:f2}";
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return ValidationResult.Success;
        }
    }
}
