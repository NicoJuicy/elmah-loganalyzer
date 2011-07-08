﻿using ElmahLogAnalyzer.UI.Controls;

namespace ElmahLogAnalyzer.UI.Views.Partials
{
	partial class ReportSelectionView
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._reportsComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this._showButton = new System.Windows.Forms.Button();
			this._dateIntervalPicker = new ElmahLogAnalyzer.UI.Controls.DateIntervalPicker();
			this._numberOfResultsComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _reportsComboBox
			// 
			this._reportsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._reportsComboBox.FormattingEnabled = true;
			this._reportsComboBox.Location = new System.Drawing.Point(357, 16);
			this._reportsComboBox.Name = "_reportsComboBox";
			this._reportsComboBox.Size = new System.Drawing.Size(383, 21);
			this._reportsComboBox.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(309, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Report:";
			// 
			// _showButton
			// 
			this._showButton.Location = new System.Drawing.Point(990, 14);
			this._showButton.Name = "_showButton";
			this._showButton.Size = new System.Drawing.Size(75, 23);
			this._showButton.TabIndex = 3;
			this._showButton.Text = "Show";
			this._showButton.UseVisualStyleBackColor = true;
			this._showButton.Click += new System.EventHandler(this.ShowButtonClick);
			// 
			// _dateIntervalPicker
			// 
			this._dateIntervalPicker.Location = new System.Drawing.Point(3, 3);
			this._dateIntervalPicker.Name = "_dateIntervalPicker";
			this._dateIntervalPicker.Size = new System.Drawing.Size(291, 66);
			this._dateIntervalPicker.TabIndex = 0;
			// 
			// _numberOfResultsComboBox
			// 
			this._numberOfResultsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._numberOfResultsComboBox.FormattingEnabled = true;
			this._numberOfResultsComboBox.Location = new System.Drawing.Point(854, 16);
			this._numberOfResultsComboBox.Name = "_numberOfResultsComboBox";
			this._numberOfResultsComboBox.Size = new System.Drawing.Size(121, 21);
			this._numberOfResultsComboBox.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(754, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Number of results:";
			// 
			// ReportSelectionView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label2);
			this.Controls.Add(this._numberOfResultsComboBox);
			this.Controls.Add(this._dateIntervalPicker);
			this.Controls.Add(this._showButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this._reportsComboBox);
			this.Name = "ReportSelectionView";
			this.Size = new System.Drawing.Size(1088, 49);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox _reportsComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button _showButton;
		private DateIntervalPicker _dateIntervalPicker;
		private System.Windows.Forms.ComboBox _numberOfResultsComboBox;
		private System.Windows.Forms.Label label2;
	}
}