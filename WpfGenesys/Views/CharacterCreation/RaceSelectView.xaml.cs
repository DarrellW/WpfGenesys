using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfGenesys.Controls;
using WpfGenesys.Data;
using WpfGenesys.Models;

namespace WpfGenesys.Views.CharacterCreation
{
    /// <summary>
    /// Interaction logic for RaceSelectView.xaml
    /// </summary>
    public partial class RaceSelectView : Page
    {
        //private static readonly SolidColorBrush _hoverColor = new(Color.FromArgb(255, 200, 255, 255));
        //private static readonly SolidColorBrush _selectedColour = new(Color.FromArgb(255, 180, 235, 245));
        //private static readonly SolidColorBrush _baseColor = new(Color.FromArgb(0,0,0,0));
        //private static readonly SolidColorBrush _transparent = Brushes.Transparent;

        public RaceBlockControl SelectedBlock { get; private set; }

        public RaceSelectView()
        {
            InitializeComponent();

            RaceData raceData = new();

            if (raceData.Count <= 0)
            {
                return;
            }

            InitializeCollection(raceData, SpeciesStackPanel);
            

            //KeyDown += RaceSelectView_KeyDown;
        }

        /// <summary>
        /// Called in the constructor (ObservableCollection) and when generating the subspecies stackpanel (List)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">List of Race objects</param>
        /// <param name="panel">Any Panel UIElement</param>
        private static void InitializeCollection<T>(T data, Panel panel) where T:IList<Race>
        {
            // First
            {
                RaceBlockControl raceBlock = new();
                raceBlock.DataContext = data[0];
                panel.Children.Insert(0,raceBlock);
                AttachRaceBlockEvents(raceBlock);
                //raceBlock.KeyDown += LeftMostRaceBlock_KeyDown;
            }
            // Last
            if (data.Count > 1)
            {
                RaceBlockControl raceBlock = new();
                raceBlock.DataContext = data[^1];//equivalent to count-1
                panel.Children.Insert(1,raceBlock);
                AttachRaceBlockEvents(raceBlock);
                //raceBlock.KeyDown += RightMostRaceBlock_KeyDown;
            }
            // 2nd - 2nd last
            for (int i = 1, last = data.Count - 1; i < last; i++)
            {
                RaceBlockControl raceBlock = new();
                raceBlock.DataContext = data[i];
                panel.Children.Insert(i,raceBlock);
                AttachRaceBlockEvents(raceBlock);
                //raceBlock.KeyDown += RaceBlock_KeyDown;
            }

        }

        #region RaceBlock selection events
        public static void AttachRaceBlockEvents(RaceBlockControl block)
        {
            block.MouseLeftButtonDown += RaceBlockControl_MouseLeftButtonDown;
            block.MouseLeftButtonUp += RaceBlockControl_MouseLeftButtonUp;
        }

        public static void DetachRaceBlockEvents(RaceBlockControl block)
        {
            block.MouseLeftButtonDown -= RaceBlockControl_MouseLeftButtonDown;
            block.MouseLeftButtonUp -= RaceBlockControl_MouseLeftButtonUp;
        }

        private static RaceBlockControl _mouseDraggedBlock;
        private static DateTime _timeMouseDown;
        private static void RaceBlockControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not RaceBlockControl block)
                return;

            _mouseDraggedBlock = block;
            _timeMouseDown = DateTime.Now;
        }

        private static void RaceBlockControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is not RaceBlockControl block)
                return;

            // Only register a click for selection if the mouse is over the same block when released and if less than a second has elapsed.
            if (block != _mouseDraggedBlock || (DateTime.Now - _timeMouseDown).Ticks > TimeSpan.TicksPerSecond)
            {
                _mouseDraggedBlock = null;
                _timeMouseDown = default;
                return;
            }

            // Get view
            RaceSelectView raceSelectView = GetParentRaceSelectView(block);// Could also store a reference to the view.

            // Stop here if the selection hasn't actually changed
            if (raceSelectView.SelectedBlock == block)
                return;

            // Different logic depending on the block's container.
            string panelName = (string)block.Parent.GetValue(NameProperty);
            if (panelName == "SpeciesStackPanel")
            {
                // Get the RaceBlockControl's Race data
                Race selected = block.Race;

                raceSelectView.ClearSubSpeciesPanel();

                //Select this Species only if it has no subraces. If it does have some, display those instead.
                if (selected.HasNoSubRaces)
                {
                    raceSelectView.SelectBlock(block);
                }
                else
                {
                    InitializeCollection(selected.SubRaces, raceSelectView.SubSpeciesStackPanel);
                    // Shift the focus/scroll down so that the subraces are completely visible.
                }
            }
            else if (panelName == "SubSpeciesStackPanel")
            {
                raceSelectView.SelectBlock(block);
            }
        }

        private static RaceSelectView GetParentRaceSelectView(RaceBlockControl control)
        {
            var parentView = control.Parent;
            while (parentView != null && parentView is not RaceSelectView)
            {
                parentView = VisualTreeHelper.GetParent(parentView);
            }
            if (parentView is not RaceSelectView raceSelectView)
                throw new Exception("Could not find RaceSelectView. " + parentView.ToString());

            return raceSelectView;
        }

        private void SelectBlock(RaceBlockControl block)
        {
            if (SelectedBlock != null) SelectedBlock.Unselect();
            SelectedBlock = block;
            SelectedBlock.Select();
        }

        private void ClearSubSpeciesPanel()
        {
            var children = SubSpeciesStackPanel.Children;
            for (int i = children.Count - 1; i >= 0; i--)
            {
                UIElement child = children[i];
                if (child is RaceBlockControl block)
                {
                    DetachRaceBlockEvents(block);
                    block.Dispose();
                }
                children.Remove(child);
            }
        }
        #endregion

        private void RaceSelectView_KeyDown(object sender, KeyEventArgs e)
        {
            DebugTextBox.Text = e.Key.ToString();
        }

        private void LeftMostRaceBlock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D || e.Key == Key.Right)
            {
                //right
            }
        }
        private void RightMostRaceBlock_KeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void RaceBlock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A || e.Key == Key.Left)
            {
                //go left
            }
            else if (e.Key == Key.D || e.Key == Key.Right)
            {
                //go right
            }
            else if (e.Key == Key.Down)
            {
                
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not being called on middle mouse tilt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
                e.Handled = true;
            //if (!e.Handled)
            //{
            //    e.Handled = true;
            //    MouseWheelEventArgs args = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            //    args.RoutedEvent = UIElement.MouseWheelEvent;
            //    args.Source = sender;
            //    var parent = (DependencyObject)sender;
            //    ScrollViewer scrollViewer = sender as ScrollViewer;
            //    //do
            //    //{
            //    //    parent = VisualTreeHelper.GetParent(parent);
            //    //    scrollViewer = parent as ScrollViewer;
            //    //} while (parent != null && scrollViewer == null);
                
            //    scrollViewer.RaiseEvent(args);
            //}
        }

        private void NewSpeciesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CustomRaceView());
        }

        #region InnerScrollViewer scrolling
        private double _originalX;
        private ScrollViewer _viewerScrolling;

        private void InnerScrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            var newX = e.GetPosition(_viewerScrolling).X;
            _viewerScrolling.ScrollToHorizontalOffset(_originalX - newX);

            e.Handled = true;
        }

        private void InnerScrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not ScrollViewer sv)
                return;

            _viewerScrolling = sv;

            _originalX = e.GetPosition(sv).X + sv.HorizontalOffset;

            // Start panning
            this.MouseMove += InnerScrollViewer_MouseMove;
        }

        private void InnerScrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _viewerScrolling = null;

            // Stop panning
            this.MouseMove -= InnerScrollViewer_MouseMove;
        }
        #endregion

    }
}
