using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace HotelSystem.Infrastructure.WPF
{
    public class CollectionItemPropertyModifiedEventArgs : EventArgs
    {
        public CollectionItemPropertyModifiedEventArgs(object item, string propertyName)
        {
            Item = item;
            PropertyName = propertyName;
        }

        public object Item { get; private set; }
        public string PropertyName { get; private set; }
    }

    /// <summary>
    /// An Observable Collection that can add ranges of items and suppress notifications so that
    /// an event is not fired off for each individual element added to the underlying collection.
    /// Will also raise a property changed event if one its items has been modified and it implements
    /// INotifyPropertyChanged.
    /// </summary>
    public class SmartCollection<T> : ObservableCollection<T>
    {
        private bool _suppressNotification = false;
        private bool _hasAddedItems = false;
        private bool _hasRemovedItems = false;
        private List<T> _modifiedItems = new List<T>();

        public SmartCollection() : base() { }
        public SmartCollection(IEnumerable<T> collection) : base(collection) { }

        // TODO: I think this can be deleted
        //public void RaisePropertyChangedEvent()
        //{
        //    OnPropertyChanged(new PropertyChangedEventArgs("Count"));
        //    OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
        //    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        //}

        /// <summary>
        /// Takes an IEnumerable of items and adds them to this object's underlying
        /// collection and fires off an event once the operation is complete
        /// </summary>
        /// <param name="range">Items to add</param>
        public void AddRange(IEnumerable<T> range)
        {
            if (range == null)
            {
                throw new ArgumentNullException("range is null");
            }

            BeginEdit();

            _hasAddedItems = true;

            foreach (var item in range)
            {
                _modifiedItems.Add(item);
                Items.Add(item);
            }

            EndEdit();
        }

        /// <summary>
        /// Removes N items from the underlying collection and fires off an event
        /// once the operatio is complete
        /// </summary>
        /// <param name="numItems">Number of items to remove</param>
        public void DownsizeCollection(int numItems)
        {
            if (numItems <= 0)
            {
                throw new ArgumentException("Can not remove 0 or less items from SmartCollection<T>");
            }

            BeginEdit();

            _hasRemovedItems = true;
            for (int i = 0; i < numItems; i++)
            {
                _modifiedItems.Add(this[this.Count - 1]);
                RemoveAt(this.Count - 1);
            }

            EndEdit();
        }

        /// <summary>
        /// This will clear the underlying collection using DownsizeCollection()
        /// which also ensures correct detaching of event listening if T implements
        /// INotifyPropertyChanged. Calling Reset with no arguments is the preferred
        /// way of clearing a SmartCollection
        /// </summary>
        /// <param name="range">Items to add</param>
        public void Reset(IEnumerable<T> range = null)
        {
            if (this.Count > 0)
            {
                DownsizeCollection(this.Count);
            }

            if (range != null)
            {
                AddRange(range);
            }
        }

        /// <summary>
        /// BeginEdit marks the start of a modification of the underlying collection
        /// and the suppression of events.
        /// </summary>
        private void BeginEdit()
        {
            _suppressNotification = true;
            _modifiedItems.Clear();
            _hasAddedItems = false;
            _hasRemovedItems = false;
        }

        /// <summary>
        /// EndEdit marks the end of a modification to the underlying collection which
        /// disables the suppression of events and fires off the appropriate event
        /// as dictated by the type of modification done
        /// </summary>
        private void EndEdit()
        {
            _suppressNotification = false;
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));

            NotifyCollectionChangedEventArgs eventArgs = null;

            // Fire appropriate modification event; reset by default
            if (_hasAddedItems)
            {
                eventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,
                    _modifiedItems);
            }
            else if (_hasRemovedItems)
            {
                eventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,
                    _modifiedItems);
            }

            if (eventArgs != null)
            {
                OnCollectionChanged(eventArgs);
            }
            else
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_suppressNotification)
            {
                try
                {
                    if (e.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach (var item in e.NewItems)
                        {
                            var notifyItem = item as INotifyPropertyChanged;
                            if (notifyItem != null)
                            {
                                notifyItem.PropertyChanged += OnItemPropertyChanged;
                            }
                        }
                    }
                    else if (e.Action == NotifyCollectionChangedAction.Remove)
                    {
                        foreach (var item in e.OldItems)
                        {
                            var notifyItem = item as INotifyPropertyChanged;
                            if (notifyItem != null)
                            {
                                notifyItem.PropertyChanged -= OnItemPropertyChanged;
                            }
                        }
                    }

                    base.OnCollectionChanged(e);
                }
                catch (NotSupportedException)
                {
                    // Workaround: this is the default behaviour of observable collection being
                    // unable to deal with ranges. Problem resides in WPF framework.
                    NotifyCollectionChangedEventArgs alternativeEventArgs =
                       new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
                    OnCollectionChanged(alternativeEventArgs);
                }
            }
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaiseCollectionItemModified(new CollectionItemPropertyModifiedEventArgs(sender, e.PropertyName));
        }

        public delegate void CollectionItemPropertyModifiedEventHandler(object sender, CollectionItemPropertyModifiedEventArgs e);

        public event CollectionItemPropertyModifiedEventHandler CollectionItemPropertyModified;

        public void RaiseCollectionItemModified(CollectionItemPropertyModifiedEventArgs eventArgs)
        {
            if (CollectionItemPropertyModified != null) CollectionItemPropertyModified(this, eventArgs);
        }
    }
}
