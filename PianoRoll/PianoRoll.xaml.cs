using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.Generic;
using System.IO;

namespace Piano
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class PianoRoll : INotifyPropertyChanged
    {
        class XyidiEvent
        {
            public int note;
            public int start;
            public int duration;
        }
        List<XyidiEvent> xyidiEvents;
        
        private ObservableCollection<RowData> _rows;

        bool isDrawing;
        int drowStartX;
        double drowStartY;


        public ObservableCollection<RowData> Rows
        {
            get => _rows;
            set
            {
                _rows = value;
                OnPropertyChanged();
            }
        }

        public PianoRoll()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            Rows = new ObservableCollection<RowData>();

            for (int i = 0; i < 500; i++)
                 
                Rows.Add(new RowData()
                {
                    Title = i + "",
                    Sounds = new ObservableCollection<SoundSquare>()/*(Enumerable.Range(0, 10).Select(j => new SoundSquare()
                    {
                        Start = j * 80 + (i % 2 * 40), Length = rnd.Next(80)
                    })) */
                }); ;
        }


        void SoundMouseDown(object sender, MouseButtonEventArgs e)
        {
            var Y = (int)(e.GetPosition(this).Y + SV.ContentVerticalOffset * 0.963);
            var X = (int)e.GetPosition(this).X;
            //var sounds = Rows[Y / 25].Sounds;
            foreach (var sound in Rows[Y / 25].Sounds)
            {
                if ((X > sound.Start) && (X < sound.Start + 50 + sound.Length))
                {
                    
                    foreach (var xe in xyidiEvents )
                    {
                        if ((xe.start == sound.Start) && ( xe.duration == (sound.Start + 50 + sound.Length) )&& (xe.note == Y / 25) )
                        {
                            xyidiEvents.Remove(xe);
                        }
                    }
                    Rows[Y / 25].Sounds.Remove(sound);
                    SaveSounds();
                    break;
                }
            }
        }
        void DrawingMouseDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;

            drowStartY = e.GetPosition(this).Y + SV.ContentVerticalOffset*0.963;
            drowStartX = (int)e.GetPosition(this).X;
        }

        void DrawingMouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
            if (drowStartX == (int)e.GetPosition(this).X) { return; }
            Rows[(int)(drowStartY / 25)].Sounds.Add(new SoundSquare() { Start = drowStartX - 50, Length = (int)e.GetPosition(this).X - drowStartX });
            if (xyidiEvents == null)
            {
                xyidiEvents = new List<XyidiEvent>();
                xyidiEvents.Add(new XyidiEvent
                {
                    note = (int)(drowStartY / 25),
                    start = drowStartX * 2,
                    duration = Math.Abs((int)e.GetPosition(this).X - drowStartX) * 7
                });
            }
            else
            {
                for (int i = 0; i < xyidiEvents.Count(); i++)
                {
                    if (drowStartX * 2 > xyidiEvents[i].start)
                    {
                        xyidiEvents.Insert(i, new XyidiEvent
                        {
                            note = (int)(drowStartY / 25),
                            start = drowStartX * 2,
                            duration = Math.Abs((int)e.GetPosition(this).X - drowStartX )*7
                        }); break;
                    }
                }
            }
            SaveSounds();
            Mouse.Capture(null);

        }
        private void SaveSounds ()
        {
            using (StreamWriter sw = new StreamWriter("1", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(xyidiEvents.Count);
                foreach (var xe in xyidiEvents)
                {
                    sw.WriteLine(xe.note);
                    sw.WriteLine(xe.start);
                    sw.WriteLine(xe.duration);
                }
                sw.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private void MousePosition()
        {
            Label label = new Label();
            label.Width = 100;
            label.Height = 30;

            this.Content = label;

            this.Loaded += delegate
            {
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Elapsed += delegate
                {
                    this.Dispatcher.Invoke(new Action(delegate
                    {
                        Mouse.Capture(this);
                        Point pointToWindow = Mouse.GetPosition(this);
                        Point pointToScreen = PointToScreen(pointToWindow);
                        label.Content = pointToScreen.ToString();
                        Mouse.Capture(null);
                    }));
                };
                timer.Interval = 1;
                timer.Start();
            };
        }
    }
    public class RowData
    {
        public string Title { get; set; }
        public ObservableCollection<SoundSquare> Sounds { get; set; }
    }

    public class SoundSquare
    {
        public int Start { get; set; }
        public int Length { get; set; }
    }
}

/*      Brush color;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddRemoveRect(object sender, MouseButtonEventArgs e)
        {
            List<Canvas> canvasHolder = new List<Canvas>();

            if(e.OriginalSource is Rectangle)
            {
                Rectangle activeRectange = (Rectangle)e.OriginalSource;

                MyCanvas.Children.Remove(activeRectange);

            }
            else
            {
                color = new SolidColorBrush(Color.FromArgb(100, 100, 100, 100));

                Rectangle newRectangle = new Rectangle
                {
                    Width = 50,
                    Height = 50,
                    Fill = color,
                    StrokeThickness = 3,
                    Stroke = Brushes.Black
                };

                Canvas.SetLeft(newRectangle, Mouse.GetPosition(MyCanvas).X-(int)(newRectangle.Width)/2);
                Canvas.SetTop(newRectangle, Mouse.GetPosition(MyCanvas).Y- (int)(newRectangle.Height)/2);

                MyCanvas.Children.Add(newRectangle);
            }

        }
*/