using System.Collections.Generic;

namespace WpfGenesys
{
    public interface PageCollection
    {
        public int Count { get; }
        //public object[] Pages;
    }

    public class FormNavigation
    {
        private int _currentWindow;
        //private List _pages;
        
        public FormNavigation(int initialWindow = 0)
        {
            //_pages = new List<T>();



            _currentWindow = initialWindow;
        }


    }
}
