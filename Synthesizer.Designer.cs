﻿
namespace Synthesizer
{
    partial class Synthesizer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.oscillator1 = new Oscillator();
            this.oscillator2 = new Oscillator();
            this.oscillator3 = new Oscillator ();
            this.SuspendLayout();
            // 
            // oscillator1
            // 
            this.oscillator1.Location = new System.Drawing.Point(13, 13);
            this.oscillator1.Name = "oscillator1";
            this.oscillator1.Size = new System.Drawing.Size(459, 120);
            this.oscillator1.TabIndex = 0;
            this.oscillator1.TabStop = false;
            this.oscillator1.Text = "Oschillator 1";
            // 
            // oscillator2
            // 
            this.oscillator2.Location = new System.Drawing.Point(13, 140);
            this.oscillator2.Name = "oscillator2";
            this.oscillator2.Size = new System.Drawing.Size(459, 120);
            this.oscillator2.TabIndex = 1;
            this.oscillator2.TabStop = false;
            this.oscillator2.Text = "Oscillator 2";
            // 
            // oscillator3
            // 
            this.oscillator3.Location = new System.Drawing.Point(13, 267);
            this.oscillator3.Name = "oscillator3";
            this.oscillator3.Size = new System.Drawing.Size(459, 120);
            this.oscillator3.TabIndex = 2;
            this.oscillator3.TabStop = false;
            this.oscillator3.Text = "Oscillator 3";
            // 
            // Synthesizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 401);
            this.Controls.Add(this.oscillator3);
            this.Controls.Add(this.oscillator2);
            this.Controls.Add(this.oscillator1);
            this.KeyPreview = true;
            this.Name = "Synthesizer";
            this.Text = "Synthesizer";
            this.ResumeLayout(false);

        }

        #endregion

        private Oscillator oscillator1;
        private Oscillator oscillator2;
        private Oscillator oscillator3;
    }
}

