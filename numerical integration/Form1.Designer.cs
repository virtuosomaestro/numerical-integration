namespace numerical_integration
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
            this.components = new System.ComponentModel.Container();
            this.zedGraphControl = new ZedGraph.ZedGraphControl();
            this.problem = new System.Windows.Forms.ComboBox();
            this.method = new System.Windows.Forms.ComboBox();
            this.Start = new System.Windows.Forms.Button();
            this.number_of_nodes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.integral = new System.Windows.Forms.Label();
            this.error = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // zedGraphControl
            // 
            this.zedGraphControl.Location = new System.Drawing.Point(372, 12);
            this.zedGraphControl.Name = "zedGraphControl";
            this.zedGraphControl.ScrollGrace = 0D;
            this.zedGraphControl.ScrollMaxX = 0D;
            this.zedGraphControl.ScrollMaxY = 0D;
            this.zedGraphControl.ScrollMaxY2 = 0D;
            this.zedGraphControl.ScrollMinX = 0D;
            this.zedGraphControl.ScrollMinY = 0D;
            this.zedGraphControl.ScrollMinY2 = 0D;
            this.zedGraphControl.Size = new System.Drawing.Size(1064, 562);
            this.zedGraphControl.TabIndex = 0;
            // 
            // problem
            // 
            this.problem.AccessibleDescription = "";
            this.problem.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.problem.FormattingEnabled = true;
            this.problem.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.problem.Location = new System.Drawing.Point(26, 60);
            this.problem.Name = "problem";
            this.problem.Size = new System.Drawing.Size(309, 39);
            this.problem.TabIndex = 1;
            // 
            // method
            // 
            this.method.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.method.FormattingEnabled = true;
            this.method.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.method.Location = new System.Drawing.Point(26, 220);
            this.method.Name = "method";
            this.method.Size = new System.Drawing.Size(309, 39);
            this.method.TabIndex = 2;
            // 
            // Start
            // 
            this.Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.Start.Location = new System.Drawing.Point(26, 489);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(309, 85);
            this.Start.TabIndex = 3;
            this.Start.Text = "Начать";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.button1_Click);
            // 
            // number_of_nodes
            // 
            this.number_of_nodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.number_of_nodes.Location = new System.Drawing.Point(26, 368);
            this.number_of_nodes.Name = "number_of_nodes";
            this.number_of_nodes.Size = new System.Drawing.Size(309, 38);
            this.number_of_nodes.TabIndex = 4;
            this.number_of_nodes.Tag = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label1.Location = new System.Drawing.Point(20, 316);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 31);
            this.label1.TabIndex = 5;
            this.label1.Text = "Количество Узлов:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label2.Location = new System.Drawing.Point(20, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 31);
            this.label2.TabIndex = 6;
            this.label2.Text = "Метод:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label3.Location = new System.Drawing.Point(20, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 31);
            this.label3.TabIndex = 7;
            this.label3.Text = "Задача:";
            // 
            // integral
            // 
            this.integral.AutoSize = true;
            this.integral.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.integral.Location = new System.Drawing.Point(20, 602);
            this.integral.Name = "integral";
            this.integral.Size = new System.Drawing.Size(0, 31);
            this.integral.TabIndex = 8;
            // 
            // error
            // 
            this.error.AutoSize = true;
            this.error.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.error.Location = new System.Drawing.Point(20, 656);
            this.error.Name = "error";
            this.error.Size = new System.Drawing.Size(0, 31);
            this.error.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1457, 709);
            this.Controls.Add(this.error);
            this.Controls.Add(this.integral);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.number_of_nodes);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.method);
            this.Controls.Add(this.problem);
            this.Controls.Add(this.zedGraphControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl;
        private System.Windows.Forms.ComboBox problem;
        private System.Windows.Forms.ComboBox method;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.TextBox number_of_nodes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label integral;
        private System.Windows.Forms.Label error;
    }
}

