﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace HotelSystem.Infrastructure.Common
{
   public abstract class ValidatableBindableBase : BindableBase, INotifyDataErrorInfo
   {
      private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

      /// <summary>
      /// Adds error to the dictionary, then raises the changed property event.
      /// </summary>
      public void AddError(string propertyName, string errorMessage)
      {
         if (!_errors.ContainsKey(propertyName))
         {
            _errors.Add(propertyName, new List<string> { errorMessage });
         }

         RaiseErrorsChanged(propertyName);
      }

      /// <summary>
      /// Clears the error for a property, then raises the changed property event.
      /// </summary>
      protected void ClearError(string propertyName)
      {
         if (_errors.ContainsKey(propertyName))
         {
            _errors.Remove(propertyName);
         }

         RaiseErrorsChanged(propertyName);
      }

      protected void ClearAllErrors()
      {
         var errors = _errors.Select(error => error.Key).ToList();

         foreach (var propertyName in errors)
         {
            ClearError(propertyName);
         }
      }

      #region Implementation of INotifyDataErrorInfo

      public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { return; };

      public IEnumerable GetErrors(string propertyName)
      {
         if (String.IsNullOrEmpty(propertyName) || !_errors.ContainsKey(propertyName))
         {
            return null;
         }

         return _errors[propertyName];
      }

      public bool HasErrors
      {
         get { return _errors.Any(x => x.Value != null && x.Value.Count > 0); }
      }

      protected void RaiseErrorsChanged(string propertyName)
      {
         var handler = ErrorsChanged;
         handler?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
      }

      #endregion
   }
}