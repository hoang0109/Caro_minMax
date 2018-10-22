namespace WindowsFormsApplication1
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Bt_2man = new System.Windows.Forms.Button();
            this.Bt_undo = new System.Windows.Forms.Button();
            this.bt_ManvsCOm = new System.Windows.Forms.Button();
            this.Redo = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Location = new System.Drawing.Point(110, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 485);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // Bt_2man
            // 
            this.Bt_2man.Location = new System.Drawing.Point(12, 42);
            this.Bt_2man.Name = "Bt_2man";
            this.Bt_2man.Size = new System.Drawing.Size(75, 23);
            this.Bt_2man.TabIndex = 2;
            this.Bt_2man.Text = "Man vs Man";
            this.Bt_2man.UseVisualStyleBackColor = true;
            this.Bt_2man.Click += new System.EventHandler(this.Bt_2man_Click);
            // 
            // Bt_undo
            // 
            this.Bt_undo.Location = new System.Drawing.Point(12, 137);
            this.Bt_undo.Name = "Bt_undo";
            this.Bt_undo.Size = new System.Drawing.Size(75, 23);
            this.Bt_undo.TabIndex = 3;
            this.Bt_undo.Text = "Undo";
            this.Bt_undo.UseVisualStyleBackColor = true;
            this.Bt_undo.Click += new System.EventHandler(this.Bt_undo_Click);
            // 
            // bt_ManvsCOm
            // 
            this.bt_ManvsCOm.Location = new System.Drawing.Point(12, 71);
            this.bt_ManvsCOm.Name = "bt_ManvsCOm";
            this.bt_ManvsCOm.Size = new System.Drawing.Size(75, 23);
            this.bt_ManvsCOm.TabIndex = 4;
            this.bt_ManvsCOm.Text = "Man vs Com";
            this.bt_ManvsCOm.UseVisualStyleBackColor = true;
            this.bt_ManvsCOm.Click += new System.EventHandler(this.bt_ManvsCOm_Click);
            // 
            // Redo
            // 
            this.Redo.Location = new System.Drawing.Point(13, 167);
            this.Redo.Name = "Redo";
            this.Redo.Size = new System.Drawing.Size(75, 23);
            this.Redo.TabIndex = 5;
            this.Redo.Text = "Redo";
            this.Redo.UseVisualStyleBackColor = true;
            this.Redo.Click += new System.EventHandler(this.Redo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 509);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Redo);
            this.Controls.Add(this.bt_ManvsCOm);
            this.Controls.Add(this.Bt_undo);
            this.Controls.Add(this.Bt_2man);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Bt_2man;
        private System.Windows.Forms.Button Bt_undo;
        private System.Windows.Forms.Button bt_ManvsCOm;
        private System.Windows.Forms.Button Redo;
        public System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Label label1;
    }
}

