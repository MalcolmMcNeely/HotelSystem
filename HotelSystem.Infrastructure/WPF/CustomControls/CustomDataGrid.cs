using HotelSystem.Infrastructure.WPF.Attributes;
using HotelSystem.Infrastructure.Helpers;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace HotelSystem.Infrastructure.WPF.CustomControls
{
    public class CustomDataGrid : DataGrid
    {
        #region Fields

        private Key _lastKeyPress = Key.None;
        private Dictionary<string, int> _defaultColumnDisplayOrder = new Dictionary<string, int>();
        private Dictionary<string, double> _defaultColumnWidths = new Dictionary<string, double>();
        private bool _isInitialized;

        #endregion

        #region Constructors

        static CustomDataGrid()
        {
            StyleProperty.OverrideMetadata(typeof(CustomDataGrid), new FrameworkPropertyMetadata(DefaultDataGridStyle));
        }

        public CustomDataGrid()
        {
            SetCommands();
        }

        #endregion

        #region Default Style

        private static Style _defaultDataGridStyle;
        public static Style DefaultDataGridStyle
        {
            get
            {
                if (_defaultDataGridStyle == null)
                {
                    Style style = new Style(typeof(CustomDataGrid));
                    style.Setters.Add(new Setter(DataGrid.CanUserAddRowsProperty, false));
                    style.Setters.Add(new Setter(DataGrid.CanUserDeleteRowsProperty, false));
                    style.Seal();

                    _defaultDataGridStyle = style;
                }

                return _defaultDataGridStyle;
            }
        }

        #endregion

        #region DependencyProperties

        public Button ShowAllColumnsButton
        {
            get { return (Button)GetValue(ShowAllColumnsButtonProperty); }
            set { SetValue(ShowAllColumnsButtonProperty, value); }
        }

        public static DependencyProperty ShowAllColumnsButtonProperty =
            DependencyProperty.Register("ShowAllColumnsButton", typeof(Button),
                typeof(CustomDataGrid),
                new PropertyMetadata(null, OnShowAllColumnsButtonPropertyChanged));

        private static void OnShowAllColumnsButtonPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as CustomDataGrid;

            if (dataGrid != null)
            {
                var button = e.NewValue as Button;

                if (button != null)
                {
                    button.Command = dataGrid.ShowAllColumnsCommand;
                }
            }
        }

        public Button ResetLayoutButton
        {
            get { return (Button)GetValue(ResetLayoutButtonProperty); }
            set { SetValue(ResetLayoutButtonProperty, value); }
        }

        public static DependencyProperty ResetLayoutButtonProperty =
            DependencyProperty.Register("ResetLayoutButton", typeof(Button),
                typeof(CustomDataGrid),
                new PropertyMetadata(null, OnResetLayoutButtonPropertyChanged));

        private static void OnResetLayoutButtonPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as CustomDataGrid;

            if (dataGrid != null)
            {
                var button = e.NewValue as Button;

                if (button != null)
                {
                    button.Command = dataGrid.ResetLayoutCommand;
                }
            }
        }

        public Button ResetSortButton
        {
            get { return (Button)GetValue(ResetSortButtonProperty); }
            set { SetValue(ResetSortButtonProperty, value); }

        }

        public static DependencyProperty ResetSortButtonProperty =
            DependencyProperty.Register("ResetSortButton", typeof(Button),
                typeof(CustomDataGrid),
                new PropertyMetadata(null, OnResetSortButtonPropertyChanged));

        private static void OnResetSortButtonPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as CustomDataGrid;

            if (dataGrid != null)
            {
                var button = e.NewValue as Button;

                if (button != null)
                {
                    button.Command = dataGrid.ResetSortCommand;
                }
            }
        }
        
        public ICommand DataGridDoubleClickCommand
        {
            get { return (ICommand)GetValue(DataGridDoubleClickCommandProperty); }
            set { SetValue(DataGridDoubleClickCommandProperty, value); }
        }

        public static readonly DependencyProperty DataGridDoubleClickCommandProperty =
            DependencyProperty.RegisterAttached("DataGridDoubleClickCommand", typeof(ICommand), typeof(CustomDataGrid),
                    new PropertyMetadata(new PropertyChangedCallback(AttachOrRemoveDataGridDoubleClickEvent)));

        public static void AttachOrRemoveDataGridDoubleClickEvent(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            DataGrid dataGrid = obj as DataGrid;
            if (dataGrid != null)
            {
                ICommand cmd = (ICommand)args.NewValue;

                if (args.OldValue == null && args.NewValue != null)
                {
                    dataGrid.MouseDoubleClick += ExecuteDataGridDoubleClick;
                }
                else if (args.OldValue != null && args.NewValue == null)
                {
                    dataGrid.MouseDoubleClick -= ExecuteDataGridDoubleClick;
                }
            }
        }

        private static void ExecuteDataGridDoubleClick(object sender, MouseButtonEventArgs args)
        {
            DependencyObject obj = sender as DependencyObject;
            ICommand cmd = (ICommand)obj.GetValue(DataGridDoubleClickCommandProperty);
            if (cmd != null)
            {
                if (cmd.CanExecute(obj))
                {
                    cmd.Execute(obj);
                }
            }
        }

        #endregion

        #region Events

        private void AttachEvents()
        {
            PreviewKeyDown += CallistoDataGrid_PreviewKeyDown;
            PreparingCellForEdit += CallistoDataGrid_PreparingCellForEdit;
        }

        private void CallistoDataGrid_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            // If Cell has gone into edit as a result of a keypress (which can only be numeric)
            // then we need to create a key press event for the underlying DecimalTextBox or IntegerTextBox
            // to handle and process the key press.
            if (_lastKeyPress != Key.None)
            {
                var decimalTextBox = e.EditingElement as DecimalTextBox;

                if (decimalTextBox != null)
                {
                    decimalTextBox.RaiseEvent(
                        new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice,
                                                              PresentationSource.FromVisual(decimalTextBox),
                                                              0,
                                                              _lastKeyPress)
                        { RoutedEvent = Keyboard.PreviewKeyDownEvent }
                        );
                    _lastKeyPress = Key.None;
                    return;
                }

                var integerTextBox = e.EditingElement as IntegerTextBox;

                if (integerTextBox != null)
                {
                    integerTextBox.RaiseEvent(
                    new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice,
                                                          PresentationSource.FromVisual(integerTextBox),
                                                          0,
                                                          _lastKeyPress)
                    { RoutedEvent = Keyboard.PreviewKeyDownEvent }
                    );

                    _lastKeyPress = Key.None;
                    return;
                }
            }
        }

        private void CallistoDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Navigation keys require any edit to be committed to allow them to
            // be handled by the default WPF navigation code
            if (InputHelper.IsKeyHandled(e.Key, InputConfiguration.GridNavigation))
            {
                // We can always call this function as, if nothing is currently
                // being editied, it will do nothing.
                CommitEdit();
            }
            else if (InputHelper.IsKeyNumeric(e.Key))
            {
                var cell = e.OriginalSource as DataGridCell;

                if (cell != null)
                {
                    var cellContent = CurrentCell.Column.GetCellContent(CurrentCell.Item);

                    if (cellContent is DecimalTextBox ||
                       cellContent is IntegerTextBox)
                    {
                        if (!cell.IsEditing && !cell.IsReadOnly)
                        {
                            // Cache last key press when going into edit mode
                            _lastKeyPress = e.Key;
                            BeginEdit();
                        }
                    }
                }
            }
        }

        #endregion

        #region Commands

        private void SetCommands()
        {
            ShowAllColumnsCommand = new DelegateCommand(ExecuteShowAllColumnsCommand);
            ResetLayoutCommand = new DelegateCommand(ExecuteResetLayoutCommand);
            ResetSortCommand = new DelegateCommand(ExecuteResetSortCommand);
        }

        public ICommand ShowAllColumnsCommand { get; private set; }

        private void ExecuteShowAllColumnsCommand()
        {
            foreach (var column in this.Columns)
            {
                column.Visibility = Visibility.Visible;
            }
        }

        public ICommand ResetLayoutCommand { get; private set; }

        private void ExecuteResetLayoutCommand()
        {
            foreach (var column in this.Columns)
            {
                column.Width = new DataGridLength(_defaultColumnWidths[(string)column.Header],
                    DataGridLengthUnitType.Star);
                column.Visibility = Visibility.Visible;
                column.DisplayIndex = _defaultColumnDisplayOrder[(string)column.Header];
            }
        }

        public ICommand ResetSortCommand { get; private set; }

        private void ExecuteResetSortCommand()
        {
            foreach (var column in this.Columns)
            {
                column.SortDirection = null;
            }

            Items.SortDescriptions.Clear();
            Items.Refresh();
        }

        #endregion

        #region Overrides

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            // If we are using autogeneration then the
            // columns will not be generated yet
            if (!AutoGenerateColumns)
            {
                if (!_isInitialized)
                {
                    CacheDefaultColumnDisplayOrder();
                    CacheDefaultColumnWidth();
                    _isInitialized = true;
                }
            }

            AttachEvents();
        }

        /// <summary>
        /// This is called after the DataGrid base has generated a column but before the column
        /// has been added to its DataGridColumns collection. We can use it to override column
        /// generation or make our own custom modifications here.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            base.OnAutoGeneratingColumn(e);

            Type propertyType = e.PropertyType;
            PropertyDescriptor pd = e.PropertyDescriptor as PropertyDescriptor;

            if (pd.Attributes.OfType<DoNotAutoGenerateAttribute>().Any())
            {
                e.Cancel = true;
            }
            else
            {
                // Replace the auto generated column with the column we generate ourselves
                if (typeof(decimal).IsAssignableFrom(propertyType))
                {
                    e.Column = GenerateDecimalTextBoxColumn(e);
                }
                else if (typeof(int).IsAssignableFrom(propertyType))
                {
                    e.Column = GenerateIntegerTextBoxColumn(e);
                }
                else
                {
                    // If we don't generate out own columns then configure according
                    // to any attributes specified
                    if (pd.Attributes.OfType<DisplayNameAttribute>().Any())
                    {
                        var boundColumn = e.Column as DataGridBoundColumn;

                        if (boundColumn != null)
                        {
                            var displayName = pd.Attributes.OfType<DisplayNameAttribute>().First();
                            boundColumn.Header = displayName.DisplayName;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This is called after autogenerated has happened where we can cache
        /// the widths and display order for the reset command functionality
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAutoGeneratedColumns(EventArgs e)
        {
            base.OnAutoGeneratedColumns(e);

            if (AutoGenerateColumns)
            {
                if (!_isInitialized)
                {
                    CacheDefaultColumnDisplayOrder();
                    CacheDefaultColumnWidth();
                    _isInitialized = true;
                }
            }
        }

        #endregion

        #region Autogeneration

        private DataGridColumn GenerateDecimalTextBoxColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridColumn column = null;
            column = new DataGridDecimalColumn();
            ConfigureFromAttributesAndBindColumn(column, e);
            return column;
        }

        private DataGridColumn GenerateIntegerTextBoxColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridColumn column = null;
            column = new DataGridIntegerColumn();
            ConfigureFromAttributesAndBindColumn(column, e);
            return column;
        }

        private void ConfigureFromAttributesAndBindColumn(DataGridColumn column,
                                                          DataGridAutoGeneratingColumnEventArgs e)
        {
            // Copy configuration of the column generated by default
            column.CanUserSort = e.Column.CanUserSort;
            column.Header = e.Column.Header;

            // Custom configuration of column
            var boundColumn = column as DataGridBoundColumn;

            if (boundColumn != null)
            {
                Binding binding = new Binding(e.PropertyName);
                PropertyDescriptor pd = e.PropertyDescriptor as PropertyDescriptor;

                if (pd != null)
                {
                    // Configure column based on attributes
                    var attributes = pd.Attributes;

                    if (pd.Attributes.OfType<DisplayNameAttribute>().Any())
                    {
                        var displayName = pd.Attributes.OfType<DisplayNameAttribute>().First();
                        boundColumn.Header = displayName.DisplayName;
                    }

                    if (pd.IsReadOnly)
                    {
                        binding.Mode = BindingMode.OneWay;
                        column.IsReadOnly = true;
                    }
                }
                else
                {
                    PropertyInfo pi = e.PropertyDescriptor as PropertyInfo;

                    if (pi != null)
                    {
                        if (!pi.CanWrite)
                        {
                            binding.Mode = BindingMode.OneWay;
                            column.IsReadOnly = true;
                        }
                    }
                }

                boundColumn.Binding = binding;
            }
        }

        #endregion

        /// <summary>
        /// Attempts to focus the DataGridCell specified by item and column name
        /// </summary>
        /// <param name="item"></param>
        /// <param name="columnName"></param>
        public void FocusCellByItem(object item, string columnName)
        {
            DataGridRow row = ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
            int columnIndex = -1;

            foreach (var column in Columns)
            {
                var header = column.Header as string;

                if (header != null)
                {
                    if (header == columnName)
                    {
                        columnIndex = column.DisplayIndex;
                    }
                }
            }

            if (columnIndex >= 0)
            {
                if (row == null)
                {
                    ScrollIntoView(item);
                    row = ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                }

                if (row != null)
                {
                    DataGridCell cell = GetCell(row, columnIndex);
                    if (cell != null)
                    {
                        cell.Focus();
                    }
                }
            }
        }

        /// <summary>
        /// Returns the cell in the specified row by column index
        /// </summary>
        /// <param name="rowContainer"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private DataGridCell GetCell(DataGridRow rowContainer, int column)
        {
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);

                if (presenter == null)
                {
                    // If the row has been virtualized away, call its ApplyTemplate() method
                    // to build its visual tree in order for the DataGridCellsPresenter
                    // and the DataGridCells to be created
                    rowContainer.ApplyTemplate();
                    presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);
                }

                if (presenter != null)
                {
                    DataGridCell cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
                    if (cell == null)
                    {
                        // bring the column into view
                        // in case it has been virtualized away
                        ScrollIntoView(rowContainer, Columns[column]);
                        cell = presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
                    }
                    return cell;
                }
            }
            return null;
        }

        /// <summary>
        /// Caches column display order as specified in the designer as the default column order
        /// </summary>
        private void CacheDefaultColumnDisplayOrder()
        {
            for (int i = 0; i < Columns.Count; i++)
            {
                if (Columns[i].Header != null)
                {
                    _defaultColumnDisplayOrder.Add((string)Columns[i].Header, i);
                }
            }
        }

        /// <summary>
        /// Caches column width display value as specified in the designer as the default column width
        /// </summary>
        private void CacheDefaultColumnWidth()
        {
            for (int i = 0; i < Columns.Count; i++)
            {
                if (Columns[i].Header != null)
                {
                    if (Columns[i].Header != null)
                    {
                        _defaultColumnWidths.Add((string)Columns[i].Header, Columns[i].Width.Value);
                    }
                }
            }
        }

        private static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);

                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }

            return null;
        }
    }
}
