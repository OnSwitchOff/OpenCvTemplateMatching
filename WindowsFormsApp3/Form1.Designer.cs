namespace WindowsFormsApp3
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button1 = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
			this.openFileDialog3 = new System.Windows.Forms.OpenFileDialog();
			this.nextBtn = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.labelCurPos = new System.Windows.Forms.Label();
			this.labelTargetPos = new System.Windows.Forms.Label();
			this.btnFindSteps = new System.Windows.Forms.Button();
			this.labelStepsCur = new System.Windows.Forms.Label();
			this.labelResultDep = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			resources.ApplyResources(this.pictureBox1, "pictureBox1");
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.TabStop = false;
			// 
			// button1
			// 
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// openFileDialog2
			// 
			this.openFileDialog2.FileName = "openFileDialog2";
			// 
			// openFileDialog3
			// 
			this.openFileDialog3.FileName = "openFileDialog3";
			// 
			// nextBtn
			// 
			resources.ApplyResources(this.nextBtn, "nextBtn");
			this.nextBtn.Name = "nextBtn";
			this.nextBtn.UseVisualStyleBackColor = true;
			this.nextBtn.Click += new System.EventHandler(this.nextBtn_click);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// numericUpDown1
			// 
			resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// labelCurPos
			// 
			resources.ApplyResources(this.labelCurPos, "labelCurPos");
			this.labelCurPos.Name = "labelCurPos";
			this.labelCurPos.Click += new System.EventHandler(this.labelCurPos_Click);
			// 
			// labelTargetPos
			// 
			resources.ApplyResources(this.labelTargetPos, "labelTargetPos");
			this.labelTargetPos.Name = "labelTargetPos";
			// 
			// btnFindSteps
			// 
			resources.ApplyResources(this.btnFindSteps, "btnFindSteps");
			this.btnFindSteps.Name = "btnFindSteps";
			this.btnFindSteps.UseVisualStyleBackColor = true;
			this.btnFindSteps.Click += new System.EventHandler(this.btnFindSteps_Click);
			// 
			// labelStepsCur
			// 
			resources.ApplyResources(this.labelStepsCur, "labelStepsCur");
			this.labelStepsCur.Name = "labelStepsCur";
			// 
			// labelResultDep
			// 
			resources.ApplyResources(this.labelResultDep, "labelResultDep");
			this.labelResultDep.Name = "labelResultDep";
			// 
			// Form1
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.labelResultDep);
			this.Controls.Add(this.labelStepsCur);
			this.Controls.Add(this.btnFindSteps);
			this.Controls.Add(this.labelTargetPos);
			this.Controls.Add(this.labelCurPos);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.nextBtn);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog2;
		private System.Windows.Forms.OpenFileDialog openFileDialog3;
		private System.Windows.Forms.Button nextBtn;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label labelCurPos;
		private System.Windows.Forms.Label labelTargetPos;
		private System.Windows.Forms.Button btnFindSteps;
		private System.Windows.Forms.Label labelStepsCur;
		private System.Windows.Forms.Label labelResultDep;
	}
}

