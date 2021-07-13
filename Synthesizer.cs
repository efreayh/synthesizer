using System;
using System.Linq;
using System.Windows.Forms;
using NAudio.Wave;

namespace Synthesizer
{
    public partial class Synthesizer : Form
    {
        public Synthesizer()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MultiWaveProvider sineWaveProvider = new MultiWaveProvider(this.Controls.OfType<Oscillator>().ToArray());
            WaveOut waveOut = new WaveOut();
            waveOut.Init(sineWaveProvider);
            waveOut.Play();
        }
    }

    public class MultiWaveProvider : IWaveProvider
    {
        private int _sample;
        private WaveFormat _waveFormat;
        private Oscillator[] _oscillators;

        public MultiWaveProvider(Oscillator[] oscillators) : this(oscillators, 44100, 1) { }

        public MultiWaveProvider(Oscillator[] oscillators, int sampleRate, int channels)
        {
            if(oscillators == null)
            {
                throw new ArgumentNullException();
            }

            if(oscillators.Length < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            SetWaveFormat(sampleRate, channels);
            this._sample = 0;
            this._oscillators = oscillators;
        }

        public void SetWaveFormat(int sampleRate, int channels)
        {
            this._waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            WaveBuffer waveBuffer = new WaveBuffer(buffer);
            int samplesRequired = count / 4;
            int samplesRead = Read(waveBuffer.FloatBuffer, offset / 4, samplesRequired);
            return samplesRead * 4;
        }

        private int Read(float[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;
            for (int n = 0; n < sampleCount; n++)
            {
                float waveSum = 0f;
                int on = 0;

                foreach (Oscillator oscillator in this._oscillators)
                {
                    float wave = CalculateWave(oscillator, sampleRate);
                    if(wave != 0f)
                    {
                        on++;
                    }
                    waveSum += wave;
                }

                if(on > 0)
                {
                    buffer[n + offset] = waveSum / on;
                }
                else
                {
                    buffer[n + offset] = 0f;
                }
                
                this._sample++;

                if(this._sample >= int.MaxValue)
                {
                    this._sample = 0;
                }
            }
            return sampleCount;
        }

        private float CalculateWave(Oscillator oscillator, int sampleRate)
        {
            if(oscillator == null)
            {
                throw new ArgumentNullException();
            }

            if(oscillator.On == true)
            {
                switch (oscillator.WaveForm)
                {
                    case WaveForm.Sine:
                        return (float)(oscillator.Amplitude * Math.Sin((2 * Math.PI * this._sample * oscillator.Frequency) / sampleRate));
                    case WaveForm.Square:
                        return (float)(oscillator.Amplitude * Math.Sign(Math.Sin((2 * Math.PI * this._sample * oscillator.Frequency) / sampleRate)));
                    case WaveForm.Saw:
                        return (float)((oscillator.Amplitude * 2 * ((this._sample * oscillator.Frequency / sampleRate) - Math.Floor(this._sample * oscillator.Frequency / sampleRate))) - 1);
                    case WaveForm.Triangle:
                        return (float)(oscillator.Amplitude * 2 / Math.PI * Math.Asin(Math.Sin((2 * Math.PI * this._sample * oscillator.Frequency) / sampleRate)));
                    case WaveForm.SemiSine:
                        return (float)((2 * Math.Abs(oscillator.Amplitude * Math.Sin((Math.PI * this._sample * oscillator.Frequency) / sampleRate))) - 1);
                    default:
                        return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public WaveFormat WaveFormat
        {
            get
            {
                return this._waveFormat;
            }
        }
    }

    
}
