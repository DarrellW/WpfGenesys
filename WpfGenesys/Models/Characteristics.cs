using System.ComponentModel;

namespace WpfGenesys.Models
{
    [System.Serializable]
    public class Characteristics : INotifyPropertyChanged, System.ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string sProp)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(sProp));
        }

        private int _brawn = 2;
        private int _agility = 2;
        private int _intellect = 2;
        private int _cunning = 2;
        private int _willpower = 2;
        private int _presence = 2;

        public int Brawn
        {
            get => _brawn;
            set
            {
                _brawn = value;
                NotifyPropertyChanged("Brawn");
            }
        }
        public int Agility
        {
            get => _agility;
            set
            {
                _agility = value;
                NotifyPropertyChanged("Agility");
            }
        }
        public int Intellect
        {
            get => _intellect;
            set
            {
                _intellect = value;
                NotifyPropertyChanged("Intellect");
            }
        }
        public int Cunning
        {
            get => _cunning;
            set
            {
                _cunning = value;
                NotifyPropertyChanged("Cunning");
            }
        }
        public int Willpower
        {
            get => _willpower;
            set
            {
                _willpower = value;
                NotifyPropertyChanged("Willpower");
            }
        }
        public int Presence
        {
            get => _presence;
            set
            {
                _presence = value;
                NotifyPropertyChanged("Presence");
            }
        }

        public static bool operator ==(Characteristics left, Characteristics right)
        {
            if (right is null)
            {
                if (left is null)
                    return true;
                return false;
            }

            return left.GetHashCode() == right.GetHashCode();
        }
        public static bool operator !=(Characteristics left, Characteristics right)
        {
            if (right is null)
            {
                if (left is null)
                    return false;
                return true;
            }
            return left.GetHashCode() != right.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Characteristics other && other == this;
        }

        public override int GetHashCode()
        {
            return Brawn * 100000
                + Agility * 10000
                + Intellect * 1000
                + Cunning * 100
                + Willpower * 10
                + Presence;
        }

        public object Clone()
        {
            return new Characteristics
            {
                _brawn = _brawn,
                _agility = _agility,
                _intellect = _intellect,
                _cunning = _cunning,
                _willpower = _willpower,
                _presence = _presence
            };
        }
    }
}
