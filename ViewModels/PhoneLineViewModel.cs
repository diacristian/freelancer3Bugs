using System;
using System.Linq;

using System.Windows;
using System.Windows.Media;

namespace FL1.ViewModels
{
    public class PhoneLineViewModel //: PropertyChangedBase
    {
        #region Constructor and initialization

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parent"></param>
        public PhoneLineViewModel()
        {

            this.PhoneLineColor = (Color)ColorConverter.ConvertFromString("Green");
        }

        // maybe null for a non existant button, but the UI binding still has to work
        public void Initialize(/*PhoneLine phoneLine*/)
        {
            _Name = "Support";
            this.PhoneLineStatus = "In Use";
        }

        #endregion Constructor

        #region Private backing Fields and Properties

        private Color _phoneLineColor;
        
        #endregion Private backing Fields and Properties

        #region Public Properties with INotifyPropertyChange
        
        /// <summary>
        ///
        /// </summary>
        //public long RID
        //{
        //    get { return this.PhoneLine == null ? -1 : this.PhoneLine.PhoneId; }
        //}

        /// <summary>
        ///
        /// </summary>
        public Color PhoneLineColor
        {
            get { return _phoneLineColor; }
            set
            {
                this._phoneLineColor = value;
                //this.BackgroundBrush = new SolidColorBrush(value);
            //    NotifyOfPropertyChange(() => PhoneLineColor);
            //    NotifyOfPropertyChange(() => BackgroundBrush);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public Brush BackgroundBrush { get; set; }

        /// <summary>
        /// Notifable name field
        /// </summary>
        public string PhoneLineName
        {
            get { return _Name; }
        }

        /// <summary>
        ///
        /// </summary>
        public Visibility PhoneLineButtonIsVisible
        {
            get { return Visibility.Visible; }
        }

        /// <summary>
        ///
        /// </summary>
        public string PhoneLineStatus
        {
            get { return _phoneLineStatus; }
            set
            {
                _phoneLineStatus = value;
                //NotifyOfPropertyChange(() => PhoneLineStatus);
            }
        }

        #endregion Public Properties with INotifyPropertyChange

        #region ICommands

        /// <summary>
        ///
        /// </summary>
        public void PhoneLineButton()
        {
            try
            {
                // proxy back to main view model

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion ICommands

        /// <summary>
        /// Alter the state of button based on timer refresh
        /// </summary>
        private bool _toggleState;

        //private bool _isHold;

        private string _phoneLineStatus;
        private string _Name;

        public void RefreshViewModel()
        {
            this._toggleState = !this._toggleState;

            this.PhoneLineColor = _toggleState
                ? (Color)ColorConverter.ConvertFromString("#ffffd800")
                : (Color)ColorConverter.ConvertFromString("#ffef9b0f");
        }
    }
}