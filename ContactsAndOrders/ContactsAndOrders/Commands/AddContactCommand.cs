﻿using ContactBook_TwoWindows.DbRealization;
using ContactBook_TwoWindows.Models;
using ContactBook_TwoWindows.ViewModels;
using System.ComponentModel;

namespace ContactBook_TwoWindows.Commands
{
    public class AddContactCommand : BaseCommand
    {
        private readonly ContactViewModel _contactViewModel;

        public AddContactCommand(ContactViewModel contactsViewModel)
        {
            _contactViewModel = contactsViewModel;
            _contactViewModel.PropertyChanged += ContactViewModelPropertyChanged;
        }

        private void ContactViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ContactViewModel.SelectedContact))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            using (DataContext db = new DataContext())
            {
                Contact contact = new Contact();
                db.Contacts.Add(contact);
                _contactViewModel.SelectedContact = contact;
                _contactViewModel.Editable = true;
                _contactViewModel.Contacts.Add(contact);
                db.SaveChanges();
            }
        }
    }
}