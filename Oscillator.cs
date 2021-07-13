using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Synthesizer
{
    public enum WaveForm
    {
        Sine, Square, Saw, Triangle, SemiSine
    }

    public class Oscillator : GroupBox
    {
        public bool On;
        public float Frequency;
        public float Amplitude;

        public WaveForm WaveForm { get; private set; }

        public Oscillator()
        {
            this.On = false;
            this.Frequency = 0f;
            this.Amplitude = 0f;

            this.Controls.Add(new Button() 
            { 
                Name = "OnOff",
                Location = new Point(10, 20),
                Text = "Off",
                BackColor = Color.PaleVioletRed
            });

            this.Controls.Add(new Button()
            {
                Name = "Sine",
                Location = new Point(70, 20),
                Text = "Sine",
                BackColor = Color.Yellow
            });
            this.Controls.Add(new Button()
            {
                Name = "Square",
                Location = new Point(130, 20),
                Text = "Square"
            });
            this.Controls.Add(new Button()
            {
                Name = "Saw",
                Location = new Point(10, 65),
                Text = "Saw"
            });
            this.Controls.Add(new Button()
            {
                Name = "Triangle",
                Location = new Point(70, 65),
                Text = "Triangle"
            });
            this.Controls.Add(new Button()
            {
                Name = "SemiSine",
                Location = new Point(130, 65),
                Text = "Semi Sine"
            });

            foreach(Control control in this.Controls.OfType<Button>())
            {
                control.Size = new Size(55, 40);
                control.Font = new Font("Microsoft Sans Serif", 8.25f);
                control.Click += WaveButton_Click;
            }

            this.Controls.Add(new Label() 
            { 
                Name = "Frequency",
                Location = new Point(190, 30),
                Text = "Frequency:",
                Size = new Size(65, 30)
            });

            TrackBar frequencySlider = new TrackBar();
            frequencySlider.Name = "FrequencySlider";
            frequencySlider.Location = new Point(260, 25);
            frequencySlider.Size = new Size(120, 30);
            frequencySlider.TickStyle = 0;
            frequencySlider.Minimum = 0;
            frequencySlider.Maximum = 4187000;
            frequencySlider.ValueChanged += FrequencySlider_ValueChanged;
            this.Controls.Add(frequencySlider);

            NumericUpDown frequencyUpDown = new NumericUpDown();
            frequencyUpDown.Name = "FrequencyUpDown";
            frequencyUpDown.Location = new Point(385, 30);
            frequencyUpDown.Size = new Size(65, 30);
            frequencyUpDown.Minimum = 0;
            frequencyUpDown.Maximum = 4187;
            frequencyUpDown.DecimalPlaces = 3;
            frequencyUpDown.Increment = 0.001m;
            frequencyUpDown.ValueChanged += FrequencyUpDown_ValueChanged;
            this.Controls.Add(frequencyUpDown);

            this.Controls.Add(new Label()
            {
                Name = "Amplitude",
                Location = new Point(190, 75),
                Text = "Amplitude:",
                Size = new Size(65, 30)
            });

            TrackBar amplitudeSlider = new TrackBar();
            amplitudeSlider.Name = "AmplitudeSlider";
            amplitudeSlider.Location = new Point(260, 70);
            amplitudeSlider.Size = new Size(120, 30);
            amplitudeSlider.TickStyle = 0;
            amplitudeSlider.Minimum = 0;
            amplitudeSlider.Maximum = 2000;
            amplitudeSlider.ValueChanged += AmplitudeSlider_ValueChanged;
            this.Controls.Add(amplitudeSlider);

            NumericUpDown amplitudeUpDown = new NumericUpDown();
            amplitudeUpDown.Name = "AmplitudeUpDown";
            amplitudeUpDown.Location = new Point(385, 75);
            amplitudeUpDown.Size = new Size(65, 30);
            amplitudeUpDown.Minimum = 0;
            amplitudeUpDown.Maximum = 2;
            amplitudeUpDown.DecimalPlaces = 3;
            amplitudeUpDown.Increment = 0.001m;
            amplitudeUpDown.ValueChanged += AmplitudeUpDown_ValueChanged;
            this.Controls.Add(amplitudeUpDown);
        }

        private Control GetControlByName(string Name)
        {
            if(Name == null)
            {
                throw new ArgumentNullException();
            }

            foreach(Control control in this.Controls)
            {
                if(control.Name.Equals(Name))
                {
                    return control;
                }
            }

            throw new InvalidOperationException();
        }

        private void FrequencySlider_ValueChanged(object sender, EventArgs e)
        {
            if(sender.GetType() != typeof(TrackBar))
            {
                throw new InvalidCastException();
            }

            TrackBar changed = (TrackBar)sender;
            object control = GetControlByName("FrequencyUpDown");

            if(control.GetType() != typeof(NumericUpDown))
            {
                throw new InvalidCastException();
            }

            NumericUpDown frequencyUpDown = (NumericUpDown)control;
            frequencyUpDown.Value = (decimal)changed.Value / 1000;
            this.Frequency = (float)changed.Value / 1000;
        }

        private void FrequencyUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(NumericUpDown))
            {
                throw new InvalidCastException();
            }

            NumericUpDown changed = (NumericUpDown)sender;
            object control = GetControlByName("FrequencySlider");

            if (control.GetType() != typeof(TrackBar))
            {
                throw new InvalidCastException();
            }

            TrackBar frequencySlider = (TrackBar)control;
            frequencySlider.Value = (int)(changed.Value * 1000);
            this.Frequency = (float)changed.Value;
        }

        private void AmplitudeSlider_ValueChanged(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(TrackBar))
            {
                throw new InvalidCastException();
            }

            TrackBar changed = (TrackBar)sender;
            object control = GetControlByName("AmplitudeUpDown");

            if (control.GetType() != typeof(NumericUpDown))
            {
                throw new InvalidCastException();
            }

            NumericUpDown amplitudeUpDown = (NumericUpDown)control;
            amplitudeUpDown.Value = (decimal)changed.Value / 1000;
            this.Amplitude = (float)changed.Value / 1000;
        }

        private void AmplitudeUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(NumericUpDown))
            {
                throw new InvalidCastException();
            }

            NumericUpDown changed = (NumericUpDown)sender;
            object control = GetControlByName("AmplitudeSlider");

            if (control.GetType() != typeof(TrackBar))
            {
                throw new InvalidCastException();
            }

            TrackBar amplitudeSlider = (TrackBar)control;
            amplitudeSlider.Value = (int)(changed.Value * 1000);
            this.Amplitude = (float)changed.Value;
        }

        private void WaveButton_Click(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(Button))
            {
                throw new InvalidCastException();
            }

            Button selected = (Button)sender;
            
            if(selected.Name.Equals("OnOff"))
            {
                if(this.On == true)
                {
                    this.On = false;
                    selected.BackColor = Color.PaleVioletRed;
                    selected.Text = "Off";
                }
                else
                {
                    this.On = true;
                    selected.BackColor = Color.LightSeaGreen;
                    selected.Text = "On";
                }
            }
            else
            {
                this.WaveForm = (WaveForm)Enum.Parse(typeof(WaveForm), selected.Name);

                foreach (Button notSelected in this.Controls.OfType<Button>())
                {
                    if(!notSelected.Name.Equals("OnOff"))
                    {
                        notSelected.UseVisualStyleBackColor = true;
                    }
                }

                selected.BackColor = Color.Yellow;
            }
        }

        
    }
}
