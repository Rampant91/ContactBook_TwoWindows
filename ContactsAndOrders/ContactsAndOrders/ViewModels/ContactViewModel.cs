using ContactsAndOrders.Commands;
using ContactsAndOrders.DbRealization;
using ContactsAndOrders.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ContactsAndOrders.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {
        #region Commands
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand OpenOrdersCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        #endregion

        #region ObservableCollections
        private ObservableCollection<Contact>? _contacts;
        public ObservableCollection<Contact>? Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value;
                OnPropertyChanged(nameof(Contacts));
            }
        }

        private ObservableCollection<Order>? _orders;
        public ObservableCollection<Order>? Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }
        #endregion

        #region SelectedContact
        private Contact? _selectedContact;
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                OnPropertyChanged(nameof(SelectedContact));
            }
        }
        #endregion

        #region ContactProperties
        private string? _firstName;
        public string? FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string? _lastName;
        public string? LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string? _patronymic;
        public string? Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                OnPropertyChanged(nameof(Patronymic));
            }
        }

        private string? _city;
        public string? City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private string? _address;
        public string? Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string? _phone;
        public string? Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private string? _email;
        public string? Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private bool _editable = false;
        public bool Editable
        {
            get => _editable;
            set
            {
                _editable = value;
                OnPropertyChanged(nameof(Editable));
            }
        }
        #endregion

        #region Constructor
        public ContactViewModel()
        {
            AddCommand = new AddContactCommand(this);
            DeleteCommand = new DeleteContactCommand(this);
            EditCommand = new EditContactCommand(this);
            OpenOrdersCommand = new OpenOrdersWindowCommand(this);
            SaveCommand = new SaveContactCommand(this);
            using (DataContext db = new DataContext())
            {
                if (db.Contacts != null)
                    Contacts = new ObservableCollection<Contact>(db.Contacts);
                else
                    Contacts = new ObservableCollection<Contact>();
            }
        }
        #endregion
    }
}