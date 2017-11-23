namespace ClientWindowsForms
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.picSelectColor = new System.Windows.Forms.PictureBox();
            this.lblSelectColor = new System.Windows.Forms.Label();
            this.picArea = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSelectColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picArea)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(434, 510);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtMessage);
            this.tabPage1.Controls.Add(this.txtMessages);
            this.tabPage1.Controls.Add(this.btnSendMessage);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(426, 484);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Чат";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Сообщения";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(6, 6);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(274, 20);
            this.txtMessage.TabIndex = 6;
            // 
            // txtMessages
            // 
            this.txtMessages.Location = new System.Drawing.Point(6, 70);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.Size = new System.Drawing.Size(412, 406);
            this.txtMessages.TabIndex = 5;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(286, 6);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(132, 53);
            this.btnSendMessage.TabIndex = 4;
            this.btnSendMessage.Text = "Отправить";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.picSelectColor);
            this.tabPage2.Controls.Add(this.lblSelectColor);
            this.tabPage2.Controls.Add(this.picArea);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(426, 484);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Рисование";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // picSelectColor
            // 
            this.picSelectColor.BackColor = System.Drawing.Color.Black;
            this.picSelectColor.Location = new System.Drawing.Point(180, 451);
            this.picSelectColor.Name = "picSelectColor";
            this.picSelectColor.Size = new System.Drawing.Size(52, 26);
            this.picSelectColor.TabIndex = 11;
            this.picSelectColor.TabStop = false;
            this.picSelectColor.Click += new System.EventHandler(this.picSelectColor_Click);
            // 
            // lblSelectColor
            // 
            this.lblSelectColor.AutoSize = true;
            this.lblSelectColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSelectColor.Location = new System.Drawing.Point(18, 451);
            this.lblSelectColor.Name = "lblSelectColor";
            this.lblSelectColor.Size = new System.Drawing.Size(156, 20);
            this.lblSelectColor.TabIndex = 10;
            this.lblSelectColor.Text = "Выбрать цвет пера";
            this.lblSelectColor.Click += new System.EventHandler(this.lblSelectColor_Click);
            // 
            // picArea
            // 
            this.picArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
            this.picArea.Location = new System.Drawing.Point(12, 36);
            this.picArea.Name = "picArea";
            this.picArea.Size = new System.Drawing.Size(400, 400);
            this.picArea.TabIndex = 9;
            this.picArea.TabStop = false;
            this.picArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picArea_MouseDown);
            this.picArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picArea_MouseMove);
            this.picArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picArea_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(8, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Область для рисования";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 510);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSelectColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picArea;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.PictureBox picSelectColor;
        private System.Windows.Forms.Label lblSelectColor;
    }
}

