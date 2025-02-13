namespace ANK20Sozluk
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtKelime = new TextBox();
            txtAciklama = new TextBox();
            txtAnlam = new TextBox();
            label2 = new Label();
            btnEkle = new Button();
            chcKriter = new CheckBox();
            btnSil = new Button();
            btnGuncelle = new Button();
            chcGuncelle = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(26, 16);
            label1.Name = "label1";
            label1.Size = new Size(100, 41);
            label1.TabIndex = 0;
            label1.Text = "KELİME";
            // 
            // txtKelime
            // 
            txtKelime.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            txtKelime.Location = new Point(132, 16);
            txtKelime.Multiline = true;
            txtKelime.Name = "txtKelime";
            txtKelime.Size = new Size(294, 41);
            txtKelime.TabIndex = 1;
            txtKelime.TextChanged += txtKelime_TextChanged;
            // 
            // txtAciklama
            // 
            txtAciklama.Enabled = false;
            txtAciklama.Location = new Point(26, 198);
            txtAciklama.Multiline = true;
            txtAciklama.Name = "txtAciklama";
            txtAciklama.Size = new Size(869, 218);
            txtAciklama.TabIndex = 2;
            // 
            // txtAnlam
            // 
            txtAnlam.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            txtAnlam.Location = new Point(132, 63);
            txtAnlam.Multiline = true;
            txtAnlam.Name = "txtAnlam";
            txtAnlam.Size = new Size(294, 74);
            txtAnlam.TabIndex = 5;
            // 
            // label2
            // 
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(26, 79);
            label2.Name = "label2";
            label2.Size = new Size(100, 39);
            label2.TabIndex = 4;
            label2.Text = "ANLAMI:";
            // 
            // btnEkle
            // 
            btnEkle.BackColor = SystemColors.Info;
            btnEkle.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnEkle.ForeColor = SystemColors.ControlText;
            btnEkle.Location = new Point(453, 16);
            btnEkle.Name = "btnEkle";
            btnEkle.Size = new Size(95, 121);
            btnEkle.TabIndex = 6;
            btnEkle.Text = "EKLE";
            btnEkle.UseVisualStyleBackColor = false;
            btnEkle.Click += btnEkle_Click;
            // 
            // chcKriter
            // 
            chcKriter.AutoSize = true;
            chcKriter.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 162);
            chcKriter.Location = new Point(26, 142);
            chcKriter.Name = "chcKriter";
            chcKriter.Size = new Size(146, 36);
            chcKriter.TabIndex = 8;
            chcKriter.Text = "Tam Metin";
            chcKriter.UseVisualStyleBackColor = true;
            chcKriter.CheckedChanged += chcKriter_CheckedChanged;
            // 
            // btnSil
            // 
            btnSil.BackColor = Color.Firebrick;
            btnSil.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnSil.ForeColor = SystemColors.ControlText;
            btnSil.Location = new Point(569, 16);
            btnSil.Name = "btnSil";
            btnSil.Size = new Size(95, 121);
            btnSil.TabIndex = 9;
            btnSil.Text = "SİL";
            btnSil.UseVisualStyleBackColor = false;
            btnSil.Click += btnSil_Click;
            // 
            // btnGuncelle
            // 
            btnGuncelle.BackColor = Color.NavajoWhite;
            btnGuncelle.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnGuncelle.ForeColor = SystemColors.ControlText;
            btnGuncelle.Location = new Point(688, 16);
            btnGuncelle.Name = "btnGuncelle";
            btnGuncelle.Size = new Size(131, 121);
            btnGuncelle.TabIndex = 10;
            btnGuncelle.Text = "GÜNCELLE";
            btnGuncelle.UseVisualStyleBackColor = false;
            btnGuncelle.Click += btnGuncelle_Click;
            // 
            // chcGuncelle
            // 
            chcGuncelle.AutoSize = true;
            chcGuncelle.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 162);
            chcGuncelle.Location = new Point(178, 143);
            chcGuncelle.Name = "chcGuncelle";
            chcGuncelle.Size = new Size(203, 36);
            chcGuncelle.TabIndex = 11;
            chcGuncelle.Text = "Baştan Güncelle";
            chcGuncelle.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(925, 450);
            Controls.Add(chcGuncelle);
            Controls.Add(btnGuncelle);
            Controls.Add(btnSil);
            Controls.Add(chcKriter);
            Controls.Add(btnEkle);
            Controls.Add(txtAnlam);
            Controls.Add(label2);
            Controls.Add(txtAciklama);
            Controls.Add(txtKelime);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtKelime;
        private TextBox txtAciklama;
        private TextBox txtAnlam;
        private Label label2;
        private Button btnEkle;
        private CheckBox chcKriter;
        private Button btnSil;
        private Button btnGuncelle;
        private CheckBox chcGuncelle;
    }
}
