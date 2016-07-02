namespace FMusic
{
    partial class ChangeFloppySettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_accept = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.numericUpDown_id = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_step = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_dir = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_id)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_step)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_dir)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Уникальный ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Номер вывода для шага:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(175, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Номер вывода для направления:";
            // 
            // button_accept
            // 
            this.button_accept.Location = new System.Drawing.Point(12, 99);
            this.button_accept.Name = "button_accept";
            this.button_accept.Size = new System.Drawing.Size(75, 23);
            this.button_accept.TabIndex = 2;
            this.button_accept.Text = "OK";
            this.button_accept.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(187, 99);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_id
            // 
            this.numericUpDown_id.Location = new System.Drawing.Point(193, 12);
            this.numericUpDown_id.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDown_id.Name = "numericUpDown_id";
            this.numericUpDown_id.Size = new System.Drawing.Size(64, 20);
            this.numericUpDown_id.TabIndex = 3;
            // 
            // numericUpDown_step
            // 
            this.numericUpDown_step.Location = new System.Drawing.Point(193, 38);
            this.numericUpDown_step.Name = "numericUpDown_step";
            this.numericUpDown_step.Size = new System.Drawing.Size(64, 20);
            this.numericUpDown_step.TabIndex = 3;
            // 
            // numericUpDown_dir
            // 
            this.numericUpDown_dir.Location = new System.Drawing.Point(193, 64);
            this.numericUpDown_dir.Name = "numericUpDown_dir";
            this.numericUpDown_dir.Size = new System.Drawing.Size(64, 20);
            this.numericUpDown_dir.TabIndex = 3;
            // 
            // ChangeFloppySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 134);
            this.Controls.Add(this.numericUpDown_dir);
            this.Controls.Add(this.numericUpDown_step);
            this.Controls.Add(this.numericUpDown_id);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_accept);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ChangeFloppySettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Изменить настройки";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_id)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_step)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_dir)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_accept;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.NumericUpDown numericUpDown_id;
        private System.Windows.Forms.NumericUpDown numericUpDown_step;
        private System.Windows.Forms.NumericUpDown numericUpDown_dir;
    }
}