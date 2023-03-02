using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Dataplace_Descompact
{
	public class Form1 : Form
	{
		private IContainer components;

		private FolderBrowserDialog folderBrowserDialog1;

		private TabPage tabDescompacta;

		private CheckBox chk_deleteCompactados;

		private Button obtemDiretorios;

		private ListBox listagemDiretorios;

		private Button processa;

		private ProgressBar progressBar1;

		private Panel panel1;

		private TextBox currentDirectory;
        private CheckBox chk_rename_default;
        private TabControl tab3;

		public Form1()
		{
			InitializeComponent();
			tabDescompacta.Visible = false;
		}

		private void obtemDiretorios_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				listagemDiretorios.Items.Add(folderBrowserDialog1.SelectedPath);
				if (listagemDiretorios.Items.Count > 0)
				{
					processa.Enabled = true;
				}
			}
		}

		private void processa_Click(object sender, EventArgs e)
		{
			if (listagemDiretorios.Items.Count == 0)
			{
                MessageBox.Show("Selecione ao menos um diretório para continuar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                panel1.Hide();
			}
			progressBar1.Value = 0;
			int deletaCompactados = (chk_deleteCompactados.Checked ? 1 : 0);
			bool varreSubDiretorios = true;
            bool renomearDefault = (chk_rename_default.Checked ? true : false);

            new ProcessaDiretorios((byte)deletaCompactados != 0, varreSubDiretorios, renomearDefault).CarregaDiretorios(panel1, listagemDiretorios, currentDirectory, progressBar1);
		}

		private void listagemDiretorios_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listagemDiretorios.SelectedItem != null)
			{
				listagemDiretorios.Items.Remove(listagemDiretorios.SelectedItem);
			}
			if (listagemDiretorios.Items.Count == 0)
			{
				processa.Enabled = false;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tab3 = new System.Windows.Forms.TabControl();
            this.tabDescompacta = new System.Windows.Forms.TabPage();
            this.currentDirectory = new System.Windows.Forms.TextBox();
            this.chk_rename_default = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.chk_deleteCompactados = new System.Windows.Forms.CheckBox();
            this.obtemDiretorios = new System.Windows.Forms.Button();
            this.listagemDiretorios = new System.Windows.Forms.ListBox();
            this.processa = new System.Windows.Forms.Button();
            this.tab3.SuspendLayout();
            this.tabDescompacta.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Diretório Atual";
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog1.SelectedPath = "C:\\\\";
            // 
            // tab3
            // 
            this.tab3.Controls.Add(this.tabDescompacta);
            this.tab3.Location = new System.Drawing.Point(3, 4);
            this.tab3.Margin = new System.Windows.Forms.Padding(4);
            this.tab3.Name = "tab3";
            this.tab3.SelectedIndex = 0;
            this.tab3.Size = new System.Drawing.Size(640, 434);
            this.tab3.TabIndex = 3;
            // 
            // tabDescompacta
            // 
            this.tabDescompacta.Controls.Add(this.currentDirectory);
            this.tabDescompacta.Controls.Add(this.chk_rename_default);
            this.tabDescompacta.Controls.Add(this.panel1);
            this.tabDescompacta.Controls.Add(this.chk_deleteCompactados);
            this.tabDescompacta.Controls.Add(this.obtemDiretorios);
            this.tabDescompacta.Controls.Add(this.listagemDiretorios);
            this.tabDescompacta.Controls.Add(this.processa);
            this.tabDescompacta.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabDescompacta.Location = new System.Drawing.Point(4, 25);
            this.tabDescompacta.Margin = new System.Windows.Forms.Padding(4);
            this.tabDescompacta.Name = "tabDescompacta";
            this.tabDescompacta.Padding = new System.Windows.Forms.Padding(4);
            this.tabDescompacta.Size = new System.Drawing.Size(632, 405);
            this.tabDescompacta.TabIndex = 1;
            this.tabDescompacta.Text = "Descompactar";
            this.tabDescompacta.UseVisualStyleBackColor = true;
            // 
            // currentDirectory
            // 
            this.currentDirectory.Location = new System.Drawing.Point(12, 273);
            this.currentDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.currentDirectory.Multiline = true;
            this.currentDirectory.Name = "currentDirectory";
            this.currentDirectory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.currentDirectory.Size = new System.Drawing.Size(607, 80);
            this.currentDirectory.TabIndex = 14;
            this.currentDirectory.Visible = false;
            // 
            // chk_rename_default
            // 
            this.chk_rename_default.AutoSize = true;
            this.chk_rename_default.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chk_rename_default.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.chk_rename_default.Location = new System.Drawing.Point(9, 196);
            this.chk_rename_default.Margin = new System.Windows.Forms.Padding(4);
            this.chk_rename_default.Name = "chk_rename_default";
            this.chk_rename_default.Size = new System.Drawing.Size(461, 21);
            this.chk_rename_default.TabIndex = 12;
            this.chk_rename_default.Text = "Selecione para renomear os arquivos default (config.ini e painel01_nfe.ini)";
            this.chk_rename_default.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Location = new System.Drawing.Point(9, 375);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 20);
            this.panel1.TabIndex = 10;
            this.panel1.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.AccessibleRole = System.Windows.Forms.AccessibleRole.Alert;
            this.progressBar1.Location = new System.Drawing.Point(4, 4);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(505, 12);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 9;
            // 
            // chk_deleteCompactados
            // 
            this.chk_deleteCompactados.AutoSize = true;
            this.chk_deleteCompactados.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chk_deleteCompactados.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.chk_deleteCompactados.Location = new System.Drawing.Point(8, 164);
            this.chk_deleteCompactados.Margin = new System.Windows.Forms.Padding(4);
            this.chk_deleteCompactados.Name = "chk_deleteCompactados";
            this.chk_deleteCompactados.Size = new System.Drawing.Size(450, 21);
            this.chk_deleteCompactados.TabIndex = 8;
            this.chk_deleteCompactados.Text = "Selecione para deletar os arquivos compactados ao finalizar a extração";
            this.chk_deleteCompactados.UseVisualStyleBackColor = true;
            // 
            // obtemDiretorios
            // 
            this.obtemDiretorios.Location = new System.Drawing.Point(521, 7);
            this.obtemDiretorios.Margin = new System.Windows.Forms.Padding(4);
            this.obtemDiretorios.Name = "obtemDiretorios";
            this.obtemDiretorios.Size = new System.Drawing.Size(100, 28);
            this.obtemDiretorios.TabIndex = 7;
            this.obtemDiretorios.Text = "Procurar";
            this.obtemDiretorios.UseVisualStyleBackColor = true;
            this.obtemDiretorios.Click += new System.EventHandler(this.obtemDiretorios_Click);
            // 
            // listagemDiretorios
            // 
            this.listagemDiretorios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listagemDiretorios.FormattingEnabled = true;
            this.listagemDiretorios.ItemHeight = 16;
            this.listagemDiretorios.Location = new System.Drawing.Point(9, 7);
            this.listagemDiretorios.Margin = new System.Windows.Forms.Padding(4);
            this.listagemDiretorios.MultiColumn = true;
            this.listagemDiretorios.Name = "listagemDiretorios";
            this.listagemDiretorios.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listagemDiretorios.Size = new System.Drawing.Size(485, 148);
            this.listagemDiretorios.TabIndex = 6;
            this.listagemDiretorios.SelectedIndexChanged += new System.EventHandler(this.listagemDiretorios_SelectedIndexChanged);
            // 
            // processa
            // 
            this.processa.Location = new System.Drawing.Point(525, 367);
            this.processa.Margin = new System.Windows.Forms.Padding(4);
            this.processa.Name = "processa";
            this.processa.Size = new System.Drawing.Size(96, 28);
            this.processa.TabIndex = 5;
            this.processa.Text = "Processar";
            this.processa.UseVisualStyleBackColor = true;
            this.processa.Click += new System.EventHandler(this.processa_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 431);
            this.Controls.Add(this.tab3);
            this.DoubleBuffered = true;
            this.ImeMode = System.Windows.Forms.ImeMode.Close;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(661, 478);
            this.MinimumSize = new System.Drawing.Size(661, 478);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Descompactador - Custom";
            this.tab3.ResumeLayout(false);
            this.tabDescompacta.ResumeLayout(false);
            this.tabDescompacta.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
	}
}
