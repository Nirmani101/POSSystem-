using POSSystem.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace POSSystem.ViewModels
{ 
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Product> _products;
        private ObservableCollection<CartItem> _cartItems;
        private string _productCode;
        private decimal _totalPrice;

        public MainViewModel()
        {
            // Initialize collections
            Products = new ObservableCollection<Product>();
            CartItems = new ObservableCollection<CartItem>();

            // Sample products
            InitializeSampleProducts();

            // Initialize commands
            AddToCartCommand = new RelayCommand(AddToCart);
        }

        private void InitializeSampleProducts()
        {
            Products.Add(new Product { Code = "1001", Name = "Laptop", Price = 999.99m, QuantityInStock = 10 });
            Products.Add(new Product { Code = "1002", Name = "Mouse", Price = 19.99m, QuantityInStock = 50 });
            Products.Add(new Product { Code = "1003", Name = "Keyboard", Price = 49.99m, QuantityInStock = 30 });
            Products.Add(new Product { Code = "1004", Name = "Monitor", Price = 199.99m, QuantityInStock = 15 });
        }

        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CartItem> CartItems
        {
            get => _cartItems;
            set
            {
                _cartItems = value;
                OnPropertyChanged();
            }
        }

        public string ProductCode
        {
            get => _productCode;
            set
            {
                _productCode = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalPrice
        {
            get => _totalPrice;
            set
            {
                _totalPrice = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddToCartCommand { get; }

        private void AddToCart(object parameter)
        {
            if (string.IsNullOrWhiteSpace(ProductCode))
                return;

            

            var existingItem = CartItems.FirstOrDefault(item => item.Product.Code == ProductCode);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                CartItems.Add(new CartItem { Product = product, Quantity = 1 });
            }

            UpdateTotalPrice();
            ProductCode = string.Empty; // Clear the input after adding
        }

        private void UpdateTotalPrice()
        {
            TotalPrice = CartItems.Sum(item => item.TotalPrice);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}