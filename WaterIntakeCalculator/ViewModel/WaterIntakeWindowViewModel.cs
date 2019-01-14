using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace WaterIntakeCalculator.ViewModel
{
    public class WaterIntakeWindowViewModel : INotifyPropertyChanged
    {
        #region Constructor
        public WaterIntakeWindowViewModel()
        {
            ProgressBarVisible = false ;
            SubmitVisibility = Visibility.Collapsed;
            IntakeCapacityVisibilty = Visibility.Collapsed;

        }
        #endregion

        #region Properties
        private Visibility _submitVisibility;
        private string _name;
        private string _weight;
        private string _validationError;
        private string _waterConsumedPerHour;
        private Visibility _intakeCapacityVisibilty;
        private double _waterNeedToConsume;
        private bool _progressBarVisible;
        private DispatcherTimer _timer;
        private double _countglss = .5;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public string Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                _weight = value;
                NotifyPropertyChanged("Weight");
            }
        }
        public string ValidationError
        {
            get
            {
                return _validationError;
            }
            set
            {
                _validationError = value;
                NotifyPropertyChanged("ValidationError");
            }
        }

        public string WaterConsumedPerHour
        {
            get
            {
                return _waterConsumedPerHour;
            }
            set
            {
                _waterConsumedPerHour = value;
                NotifyPropertyChanged("WaterConsumedPerHour");
            }
        }
        

        public double WaterNeedToConsume
        {
            get
            {
                return _waterNeedToConsume;
            }
            set
            {
                _waterNeedToConsume = value;
                NotifyPropertyChanged("WaterNeedToConsume");
            }
        }



        public Visibility SubmitVisibility
        {
            get
            {
                return _submitVisibility;
            }
            set
            {
                _submitVisibility = value;
                NotifyPropertyChanged("SubmitVisibility");
            }
        }
        public Visibility IntakeCapacityVisibilty
        {
            get
            {
                return _intakeCapacityVisibilty;
            }
            set
            {
                _intakeCapacityVisibilty = value;
                NotifyPropertyChanged("IntakeCapacityVisibilty");
            }
        }

        public bool ProgressBarVisible
        {
            get
            {
                return _progressBarVisible;
            }

            set
            {
                _progressBarVisible = value;
                NotifyPropertyChanged("ProgressBarVisible");
            }
        }
        #endregion

        #region Command
        public ICommand WaterIntakeCapacityCommand
        {
            get { return new DelegateCommand(WaterIntakeCapacity); }
        }

        public ICommand SubmitDataCommand
        {
            get { return new DelegateCommand(SubmitData); }
        }

        #endregion

        #region PrivateMethods

        /// <summary>
        /// Calculate Water Intake Capacity of a person according to weight
        /// </summary>
        private void WaterIntakeCapacity()
        {
            ProgressBarVisible = true;
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Weight))
            {
                ValidationError = "Mandatory Fields (*) ";
                ProgressBarVisible = false;
            }

            else
            {
               
                try
                {
                    
                    WaterNeedToConsume = WaterCapacity(Convert.ToDouble(Weight));
                    if ( WaterNeedToConsume != null)
                    {
                        IntakeCapacityVisibilty = Visibility.Visible;
                        SubmitVisibility = Visibility.Visible;
                        ProgressBarVisible = false;
                    }
                }
                catch (Exception)
                {
                    ValidationError = "Wrong Data Format ";
                    ProgressBarVisible = false;
                    SubmitVisibility = Visibility.Collapsed;
                    IntakeCapacityVisibilty = Visibility.Collapsed;
                }
                
            }
        }

        /// <summary>
        /// Save The Water intake capacity of a person  in-memory database
        /// </summary>
        private void SubmitData()
        {
            WaterConsumedPerHour = "Water Consumed by " + Name + " :   " + _countglss + "Ltr";
            SubmitVisibility = Visibility.Collapsed;
            _timer = new DispatcherTimer();

            // For testing  purpose i set the timer for 5 sec interval (Actual should be  1800 sec ) 
            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Start();
            _timer.Tick += Timer_Tick;

        }

        /// <summary>
        /// Alert the user to take water in every 30 min  interval
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("It's  time to take 1/2 Glass water ! Be Healty");
            _countglss = _countglss + .5;
            if (_countglss == WaterNeedToConsume || _countglss > WaterNeedToConsume)
            {
                _timer.Stop();
                MessageBox.Show(Name+ " Consumed Maximum Water per day as per your weight");
                WaterConsumedPerHour = "Water Consumed by " + Name + " :   " + WaterNeedToConsume + "Ltr";

            }
            if (_countglss < WaterNeedToConsume)
                WaterConsumedPerHour = "Water Consumed by " + Name + " :   " + _countglss + "Ltr";
        }

        /// <summary>
        /// Calculate Water Capacity per Ltr according to weight 
        /// </summary>
        /// <param name="weight">person weigt</param>
        /// <returns>capacity to consume water</returns>
        private double WaterCapacity(double weight)
        {
            return (weight / 30);
        }

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string value)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(value));
        }

        #endregion
    }
}
