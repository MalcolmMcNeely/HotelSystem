using HotelSystem.Business;
using HotelSystem.Infrastructure.Common;
using System.ComponentModel;

namespace People.ViewModels
{
   public class PeopleViewModel : BindableBase, IPeopleViewModel
   {
      private Person _model;

      public PeopleViewModel()
      {
         _model = new Person();
         _model.PropertyChanged += OnModelPropertyChanged;
      }

      public void ShutDown()
      {
         if(_model != null)
         {
            _model.PropertyChanged -= OnModelPropertyChanged;
         }
      }

      #region Model Accessors

      public string Name
      {
         get => _model.Name;
         set => _model.Name = value;
      }

      public int Age
      {
         get => _model.Age;
         set => _model.Age = value;
      }

      public string AddressLineOne
      {
         get => _model.AddressLineOne;
         set => _model.AddressLineOne = value;
      }

      public string AddressLineTwo
      {
         get => _model.AddressLineTwo;
         set => _model.AddressLineTwo = value;
      }

      public string City
      {
         get => _model.City;
         set => _model.City = value;
      }

      public string PostCode
      {
         get => _model.PostCode;
         set => _model.PostCode = value;
      }

      public string PhoneNumber
      {
         get => _model.PhoneNumber;
         set => _model.PhoneNumber = value;
      }

      public string CreditCardNumber
      {
         get => _model.CreditCardNumber;
         set => _model.CreditCardNumber = value;
      }

      public decimal AmountOwed
      {
         get => _model.AmountOwed;
         set => _model.AmountOwed = value;
      }

      public decimal AmountPaid
      {
         get => _model.AmountPaid;
         set => _model.AmountPaid = value;
      }

      #endregion

      #region Events

      private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
      {
         RaisePropertyChanged(e.PropertyName);
      }

      #endregion
   }
}
