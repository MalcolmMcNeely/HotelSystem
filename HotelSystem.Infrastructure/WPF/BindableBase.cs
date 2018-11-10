using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HotelSystem.Infrastructure.WPF
{
    public class BindableBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Attempts to set <paramref name="fieldReference"/> to the specified <paramref name="newValue"/>.
        /// If setting is successful then an event is raised using the relevant <paramref name="propertyName"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldReference"></param>
        /// <param name="newValue"></param>
        /// <param name="propertyName"></param>
        /// <returns>True if successfully set</returns>
        protected bool SetProperty<T>(ref T fieldReference, T newValue, [CallerMemberName] string propertyName = null)
        {
            bool bIsSameValue = Equals(fieldReference, newValue);

            if (!bIsSameValue)
            {
                fieldReference = newValue;

                if (propertyName != null)
                {
                    RaisePropertyChanged(propertyName);
                }
            }

            return !bIsSameValue;
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
