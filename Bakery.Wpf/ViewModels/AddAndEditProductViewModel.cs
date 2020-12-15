using Bakery.Core.DTOs;
using Bakery.Core.Entities;
using Bakery.Persistence;
using Bakery.Wpf.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bakery.Wpf.ViewModels
{
    public class AddAndEditProductViewModel : BaseViewModel
    {
        private string _titleString;
        private ProductDto _originalProduct;
        private string _productNr;
        private string _name;
        private double _price;
        public ICommand CmdSave { get; set; }
        public ICommand CmdUndo { get; set; }

        public string TitleString
        {
            get => _titleString;
            set
            {
                _titleString = value;
                OnPropertyChanged();
            }
        }

        [Required(ErrorMessage = "Produktnummer muss angegeben werden")]
        public string ProductNr
        {
            get => _productNr;
            set
            {
                _productNr = value;
                OnPropertyChanged();
            }
        }

        [Required(ErrorMessage = "Produktname muss angegeben werden")]
        [MinLength(1, ErrorMessage = "Produktname muss mindestens 1 Zeichen lang sein")]
        [MaxLength(20, ErrorMessage = "Produktname darf maximal 20 Zeichen lang sein")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public AddAndEditProductViewModel(IWindowController controller, ProductDto product) : base(controller)
        {
            if(product != null)
            {
                _originalProduct = product;
                ProductNr = _originalProduct.ProductNr;
                Name = _originalProduct.Name;
                Price = _originalProduct.Price;
                TitleString = "Produkt bearbeiten";
            }
            else
            {
                TitleString = "Produkt anlegen";
            }

            LoadCommands();
        }

        private void LoadCommands()
        {
            CmdSave = new RelayCommand(
                async c => await SaveProduct(),
                c => _originalProduct == null || (HasChanged() && IsValid)
            );

            CmdUndo = new RelayCommand(
                c => UndoChanges(),
                c => _originalProduct != null && HasChanged()
            );
        }

        private bool HasChanged()
        {
            if(ProductNr != _originalProduct.ProductNr ||
                Name != _originalProduct.Name ||
                Price != _originalProduct.Price)
            {
                return true;
            }

            return false;
        }

        private async Task SaveProduct()
        {
            await using var uow = new UnitOfWork();
            Product dbProduct = null;
            if (_originalProduct == null)
            {
                dbProduct = new Product
                {
                    ProductNr = ProductNr,
                    Name = Name,
                    Price = Price
                };
                await uow.Products.AddAsync(dbProduct);

            }
            else
            {
                dbProduct = await uow.Products.GetAsync(_originalProduct.Id);
                if (dbProduct == null)
                {
                    DbError = "Produkt existiert nicht";
                    return;
                }
                dbProduct.ProductNr = ProductNr;
                dbProduct.Name = Name;
                dbProduct.Price = Price;
            }

            try
            {
                await uow.SaveChangesAsync();
                _originalProduct = new ProductDto(dbProduct);
                Controller.CloseWindow(this);
            }
            catch (ValidationException e)
            {
                DbError = e.Message;
            }
        }

        private void UndoChanges()
        {
            ProductNr = _originalProduct.ProductNr;
            Name = _originalProduct.Name;
            Price = _originalProduct.Price;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return ValidationResult.Success;
        }
    }
}
