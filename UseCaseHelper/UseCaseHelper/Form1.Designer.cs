﻿namespace UseCaseHelper
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbLine = new System.Windows.Forms.RadioButton();
            this.rbUseCase = new System.Windows.Forms.RadioButton();
            this.rbActor = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbSelect = new System.Windows.Forms.RadioButton();
            this.rbCreate = new System.Windows.Forms.RadioButton();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsBottomBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.drawCanvas = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbLine);
            this.groupBox1.Controls.Add(this.rbUseCase);
            this.groupBox1.Controls.Add(this.rbActor);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(137, 95);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Elementen";
            // 
            // rbLine
            // 
            this.rbLine.AutoSize = true;
            this.rbLine.Location = new System.Drawing.Point(7, 66);
            this.rbLine.Name = "rbLine";
            this.rbLine.Size = new System.Drawing.Size(45, 17);
            this.rbLine.TabIndex = 2;
            this.rbLine.TabStop = true;
            this.rbLine.Text = "Line";
            this.rbLine.UseVisualStyleBackColor = true;
            // 
            // rbUseCase
            // 
            this.rbUseCase.AutoSize = true;
            this.rbUseCase.Location = new System.Drawing.Point(7, 43);
            this.rbUseCase.Name = "rbUseCase";
            this.rbUseCase.Size = new System.Drawing.Size(71, 17);
            this.rbUseCase.TabIndex = 1;
            this.rbUseCase.TabStop = true;
            this.rbUseCase.Text = "Use Case";
            this.rbUseCase.UseVisualStyleBackColor = true;
            // 
            // rbActor
            // 
            this.rbActor.AutoSize = true;
            this.rbActor.Location = new System.Drawing.Point(7, 20);
            this.rbActor.Name = "rbActor";
            this.rbActor.Size = new System.Drawing.Size(50, 17);
            this.rbActor.TabIndex = 0;
            this.rbActor.TabStop = true;
            this.rbActor.Text = "Actor";
            this.rbActor.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbSelect);
            this.groupBox2.Controls.Add(this.rbCreate);
            this.groupBox2.Location = new System.Drawing.Point(175, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(137, 95);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modes";
            // 
            // rbSelect
            // 
            this.rbSelect.AutoSize = true;
            this.rbSelect.Location = new System.Drawing.Point(7, 43);
            this.rbSelect.Name = "rbSelect";
            this.rbSelect.Size = new System.Drawing.Size(55, 17);
            this.rbSelect.TabIndex = 1;
            this.rbSelect.TabStop = true;
            this.rbSelect.Text = "Select";
            this.rbSelect.UseVisualStyleBackColor = true;
            // 
            // rbCreate
            // 
            this.rbCreate.AutoSize = true;
            this.rbCreate.Location = new System.Drawing.Point(7, 20);
            this.rbCreate.Name = "rbCreate";
            this.rbCreate.Size = new System.Drawing.Size(56, 17);
            this.rbCreate.TabIndex = 0;
            this.rbCreate.TabStop = true;
            this.rbCreate.Text = "Create";
            this.rbCreate.UseVisualStyleBackColor = true;
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(962, 12);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(115, 23);
            this.btnClearAll.TabIndex = 2;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(962, 49);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(115, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBottomBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 622);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1087, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsBottomBar
            // 
            this.tsBottomBar.Name = "tsBottomBar";
            this.tsBottomBar.Size = new System.Drawing.Size(0, 17);
            // 
            // drawCanvas
            // 
            this.drawCanvas.BackColor = System.Drawing.SystemColors.Window;
            this.drawCanvas.Location = new System.Drawing.Point(12, 131);
            this.drawCanvas.Name = "drawCanvas";
            this.drawCanvas.Size = new System.Drawing.Size(1063, 456);
            this.drawCanvas.TabIndex = 5;
            this.drawCanvas.TabStop = false;
            this.drawCanvas.Click += new System.EventHandler(this.canvasClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 644);
            this.Controls.Add(this.drawCanvas);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "UseCaseHelper";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbLine;
        private System.Windows.Forms.RadioButton rbUseCase;
        private System.Windows.Forms.RadioButton rbActor;
        private System.Windows.Forms.RadioButton rbSelect;
        private System.Windows.Forms.RadioButton rbCreate;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsBottomBar;
        private System.Windows.Forms.PictureBox drawCanvas;
    }
}

