
using UnityEngine.Events;

public class ObservableUnityEvent<T, U> : UnityEvent<T, U>
    {
        private Observable<T> _observable;
        public ObservableUnityEvent(Observable<T> observable) : base()
        {
            _observable = observable;
        }
        
        
        public new void AddListener(UnityAction<T, U> call)
        {
            AddListener(call, false);
        }
        
        
        public void AddListener(UnityAction<T, U> call, bool willRefreshImmediately )
        {
            base.AddListener(call);
            if (willRefreshImmediately)
            {
                _observable.OnValueChangedRefresh();
            }
        }
    }
    
    
    public class Observable<TValue> 
    {
        
        public readonly ObservableUnityEvent<TValue, TValue> OnValueChanged;
        
        
        public TValue Value
        {
            set
            {
                _currentValue = OnValueChanging(_currentValue, value);
                OnValueChanged.Invoke(_previousValue, _currentValue);
            }
            get
            {
                return _currentValue;
            }
        }

        
        private TValue _currentValue;
        private TValue _previousValue;

        

        public Observable()
        {
            OnValueChanged = new ObservableUnityEvent<TValue,TValue>(this);
        }

        
        public void OnValueChangedRefresh()
        {
            OnValueChanged.Invoke(_previousValue, _currentValue);
        }
        
        protected virtual TValue OnValueChanging(TValue previousValue, TValue newValue)
        {
            
            _previousValue = previousValue;
            return newValue;
        }

    }
