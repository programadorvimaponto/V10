
namespace Vatbook
{
    partial class FrmVatbookView
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButtonBlocco = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lookUpEditMese = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditAnno = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButtonRicalcolare = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditMese.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAnno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.simpleButtonBlocco);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.lookUpEditMese);
            this.groupControl1.Controls.Add(this.lookUpEditAnno);
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(197, 130);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Vatbook Bloccato";
            // 
            // simpleButtonBlocco
            // 
            this.simpleButtonBlocco.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonBlocco.Appearance.Options.UseFont = true;
            this.simpleButtonBlocco.Location = new System.Drawing.Point(56, 95);
            this.simpleButtonBlocco.Name = "simpleButtonBlocco";
            this.simpleButtonBlocco.Size = new System.Drawing.Size(87, 26);
            this.simpleButtonBlocco.TabIndex = 4;
            this.simpleButtonBlocco.Text = "Blocco";
            this.simpleButtonBlocco.Click += new System.EventHandler(this.simpleButtonBlocco_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mese";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Anno";
            // 
            // lookUpEditMese
            // 
            this.lookUpEditMese.Location = new System.Drawing.Point(75, 52);
            this.lookUpEditMese.Name = "lookUpEditMese";
            this.lookUpEditMese.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditMese.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditMese.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditMese.Properties.NullText = "";
            this.lookUpEditMese.Size = new System.Drawing.Size(100, 22);
            this.lookUpEditMese.TabIndex = 1;
            // 
            // lookUpEditAnno
            // 
            this.lookUpEditAnno.Location = new System.Drawing.Point(75, 21);
            this.lookUpEditAnno.Name = "lookUpEditAnno";
            this.lookUpEditAnno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lookUpEditAnno.Properties.Appearance.Options.UseFont = true;
            this.lookUpEditAnno.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditAnno.Properties.NullText = "";
            this.lookUpEditAnno.Size = new System.Drawing.Size(100, 22);
            this.lookUpEditAnno.TabIndex = 0;
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(0, 0);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Size = new System.Drawing.Size(100, 20);
            this.lookUpEdit1.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.simpleButtonRicalcolare);
            this.groupControl2.Controls.Add(this.label4);
            this.groupControl2.Location = new System.Drawing.Point(206, 3);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(197, 130);
            this.groupControl2.TabIndex = 4;
            this.groupControl2.Text = "Vatbook Bloccato";
            // 
            // simpleButtonRicalcolare
            // 
            this.simpleButtonRicalcolare.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButtonRicalcolare.Appearance.Options.UseFont = true;
            this.simpleButtonRicalcolare.Location = new System.Drawing.Point(56, 95);
            this.simpleButtonRicalcolare.Name = "simpleButtonRicalcolare";
            this.simpleButtonRicalcolare.Size = new System.Drawing.Size(87, 26);
            this.simpleButtonRicalcolare.TabIndex = 3;
            this.simpleButtonRicalcolare.Text = "Ricalcolare";
            this.simpleButtonRicalcolare.Click += new System.EventHandler(this.simpleButtonRicalcolare_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 53);
            this.label4.TabIndex = 2;
            this.label4.Text = "Ricalcola il numero della fattura solo nei documenti aperti!";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmVatbookView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVatbookView";
            this.Size = new System.Drawing.Size(408, 136);
            this.Text = "Vatbook";
            this.Load += new System.EventHandler(this.FrmVatbookView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditMese.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditAnno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditMese;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditAnno;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButtonRicalcolare;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SimpleButton simpleButtonBlocco;
    }
}