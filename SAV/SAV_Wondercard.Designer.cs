﻿namespace PKHeX
{
    partial class SAV_Wondercard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SAV_Wondercard));
            this.B_Save = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.B_Output = new System.Windows.Forms.Button();
            this.B_Import = new System.Windows.Forms.Button();
            this.LB_WCs = new System.Windows.Forms.ListBox();
            this.LB_Received = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.B_WCSlottoDisplay = new System.Windows.Forms.Button();
            this.B_DisplaytoWCSlot = new System.Windows.Forms.Button();
            this.RTB = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.B_DeleteWC = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // B_Save
            // 
            this.B_Save.Location = new System.Drawing.Point(197, 227);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(75, 23);
            this.B_Save.TabIndex = 0;
            this.B_Save.Text = "Save";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.Location = new System.Drawing.Point(120, 227);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(71, 23);
            this.B_Cancel.TabIndex = 1;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // B_Output
            // 
            this.B_Output.Location = new System.Drawing.Point(197, 12);
            this.B_Output.Name = "B_Output";
            this.B_Output.Size = new System.Drawing.Size(75, 23);
            this.B_Output.TabIndex = 2;
            this.B_Output.Text = "Output .wc6";
            this.B_Output.UseVisualStyleBackColor = true;
            this.B_Output.Click += new System.EventHandler(this.B_Output_Click);
            // 
            // B_Import
            // 
            this.B_Import.Location = new System.Drawing.Point(120, 12);
            this.B_Import.Name = "B_Import";
            this.B_Import.Size = new System.Drawing.Size(71, 23);
            this.B_Import.TabIndex = 3;
            this.B_Import.Text = "Import .wc6";
            this.B_Import.UseVisualStyleBackColor = true;
            this.B_Import.Click += new System.EventHandler(this.B_Import_Click);
            // 
            // LB_WCs
            // 
            this.LB_WCs.FormattingEnabled = true;
            this.LB_WCs.Location = new System.Drawing.Point(12, 25);
            this.LB_WCs.Name = "LB_WCs";
            this.LB_WCs.Size = new System.Drawing.Size(78, 95);
            this.LB_WCs.TabIndex = 4;
            // 
            // LB_Received
            // 
            this.LB_Received.FormattingEnabled = true;
            this.LB_Received.Location = new System.Drawing.Point(12, 155);
            this.LB_Received.Name = "LB_Received";
            this.LB_Received.Size = new System.Drawing.Size(78, 95);
            this.LB_Received.Sorted = true;
            this.LB_Received.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Wondercards:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Received List:";
            // 
            // B_WCSlottoDisplay
            // 
            this.B_WCSlottoDisplay.Location = new System.Drawing.Point(96, 59);
            this.B_WCSlottoDisplay.Name = "B_WCSlottoDisplay";
            this.B_WCSlottoDisplay.Size = new System.Drawing.Size(18, 24);
            this.B_WCSlottoDisplay.TabIndex = 8;
            this.B_WCSlottoDisplay.Text = ">";
            this.B_WCSlottoDisplay.UseVisualStyleBackColor = true;
            this.B_WCSlottoDisplay.Click += new System.EventHandler(this.B_SAV2WC);
            // 
            // B_DisplaytoWCSlot
            // 
            this.B_DisplaytoWCSlot.Location = new System.Drawing.Point(96, 89);
            this.B_DisplaytoWCSlot.Name = "B_DisplaytoWCSlot";
            this.B_DisplaytoWCSlot.Size = new System.Drawing.Size(18, 24);
            this.B_DisplaytoWCSlot.TabIndex = 9;
            this.B_DisplaytoWCSlot.Text = "<";
            this.B_DisplaytoWCSlot.UseVisualStyleBackColor = true;
            this.B_DisplaytoWCSlot.Click += new System.EventHandler(this.B_WC2SAV);
            // 
            // RTB
            // 
            this.RTB.Location = new System.Drawing.Point(120, 59);
            this.RTB.Name = "RTB";
            this.RTB.ReadOnly = true;
            this.RTB.Size = new System.Drawing.Size(151, 162);
            this.RTB.TabIndex = 10;
            this.RTB.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Details:";
            // 
            // B_DeleteWC
            // 
            this.B_DeleteWC.Location = new System.Drawing.Point(96, 29);
            this.B_DeleteWC.Name = "B_DeleteWC";
            this.B_DeleteWC.Size = new System.Drawing.Size(18, 24);
            this.B_DeleteWC.TabIndex = 12;
            this.B_DeleteWC.Text = "X";
            this.B_DeleteWC.UseVisualStyleBackColor = true;
            this.B_DeleteWC.Click += new System.EventHandler(this.B_DeleteWC_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(96, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(18, 24);
            this.button1.TabIndex = 13;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.B_DeleteReceived_Click);
            // 
            // SAV_Wondercard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.B_DeleteWC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RTB);
            this.Controls.Add(this.B_DisplaytoWCSlot);
            this.Controls.Add(this.B_WCSlottoDisplay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LB_Received);
            this.Controls.Add(this.LB_WCs);
            this.Controls.Add(this.B_Import);
            this.Controls.Add(this.B_Output);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_Save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SAV_Wondercard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wondercard I/O";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Button B_Output;
        private System.Windows.Forms.Button B_Import;
        private System.Windows.Forms.ListBox LB_WCs;
        private System.Windows.Forms.ListBox LB_Received;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button B_WCSlottoDisplay;
        private System.Windows.Forms.Button B_DisplaytoWCSlot;
        private System.Windows.Forms.RichTextBox RTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button B_DeleteWC;
        private System.Windows.Forms.Button button1;
    }
}