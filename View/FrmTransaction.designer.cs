namespace MoneyFlow
{
    public partial class FrmTransaction : System.Windows.Forms.Form
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.grpboxTransaction = new System.Windows.Forms.GroupBox();
            this.txtTransactionDescription = new System.Windows.Forms.TextBox();
            this.grpboxTransactionLookup = new System.Windows.Forms.GroupBox();
            this.btnTransactionBind = new System.Windows.Forms.Button();
            this.txtTransactionLookup = new System.Windows.Forms.TextBox();
            this.lblTransactionLookup = new System.Windows.Forms.Label();
            this.dtpTransactionDate = new System.Windows.Forms.DateTimePicker();
            this.txtTransactionId = new System.Windows.Forms.TextBox();
            this.btnTransactionClear = new System.Windows.Forms.Button();
            this.btnTransactionDelete = new System.Windows.Forms.Button();
            this.btnTransactionEdit = new System.Windows.Forms.Button();
            this.btnTransactionAdd = new System.Windows.Forms.Button();
            this.grpboxTransactionCategory = new System.Windows.Forms.GroupBox();
            this.btnTransactionAddNewCat = new System.Windows.Forms.Button();
            this.txtTransactionNewCat = new System.Windows.Forms.TextBox();
            this.lblTransactionNewCat = new System.Windows.Forms.Label();
            this.lstboxTransactionExpenseCategory = new System.Windows.Forms.ListBox();
            this.lblTransactionExpenseCategory = new System.Windows.Forms.Label();
            this.cmbIncomeCategory = new System.Windows.Forms.ComboBox();
            this.lblTransactionIncomeCategory = new System.Windows.Forms.Label();
            this.numupdTransactionAmount = new System.Windows.Forms.NumericUpDown();
            this.lblTransactionAmount = new System.Windows.Forms.Label();
            this.grpboxTransactionType = new System.Windows.Forms.GroupBox();
            this.rbTransactionExpense = new System.Windows.Forms.RadioButton();
            this.rbTransactionIncome = new System.Windows.Forms.RadioButton();
            this.lblTransactionDesc = new System.Windows.Forms.Label();
            this.lblTransactionDate = new System.Windows.Forms.Label();
            this.lblTransactionId = new System.Windows.Forms.Label();
            this.lblTransactionCategoryError = new System.Windows.Forms.Label();
            this.lblTransactionAmountError = new System.Windows.Forms.Label();
            this.lblTransactionTypeError = new System.Windows.Forms.Label();
            this.lblTransactionDescError = new System.Windows.Forms.Label();
            this.lblTransactionDateError = new System.Windows.Forms.Label();
            this.grpboxTransaction.SuspendLayout();
            this.grpboxTransactionLookup.SuspendLayout();
            this.grpboxTransactionCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numupdTransactionAmount)).BeginInit();
            this.grpboxTransactionType.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpboxTransaction
            // 
            this.grpboxTransaction.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grpboxTransaction.Controls.Add(this.txtTransactionDescription);
            this.grpboxTransaction.Controls.Add(this.grpboxTransactionLookup);
            this.grpboxTransaction.Controls.Add(this.dtpTransactionDate);
            this.grpboxTransaction.Controls.Add(this.txtTransactionId);
            this.grpboxTransaction.Controls.Add(this.btnTransactionClear);
            this.grpboxTransaction.Controls.Add(this.btnTransactionDelete);
            this.grpboxTransaction.Controls.Add(this.btnTransactionEdit);
            this.grpboxTransaction.Controls.Add(this.btnTransactionAdd);
            this.grpboxTransaction.Controls.Add(this.grpboxTransactionCategory);
            this.grpboxTransaction.Controls.Add(this.numupdTransactionAmount);
            this.grpboxTransaction.Controls.Add(this.lblTransactionAmount);
            this.grpboxTransaction.Controls.Add(this.grpboxTransactionType);
            this.grpboxTransaction.Controls.Add(this.lblTransactionDesc);
            this.grpboxTransaction.Controls.Add(this.lblTransactionDate);
            this.grpboxTransaction.Controls.Add(this.lblTransactionId);
            this.grpboxTransaction.Controls.Add(this.lblTransactionCategoryError);
            this.grpboxTransaction.Controls.Add(this.lblTransactionAmountError);
            this.grpboxTransaction.Controls.Add(this.lblTransactionTypeError);
            this.grpboxTransaction.Controls.Add(this.lblTransactionDescError);
            this.grpboxTransaction.Controls.Add(this.lblTransactionDateError);
            this.grpboxTransaction.Location = new System.Drawing.Point(0, 0);
            this.grpboxTransaction.Name = "grpboxTransaction";
            this.grpboxTransaction.Padding = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.grpboxTransaction.Size = new System.Drawing.Size(738, 680);
            this.grpboxTransaction.TabIndex = 0;
            this.grpboxTransaction.TabStop = false;
            this.grpboxTransaction.Text = "Transaction Details";
            // 
            // txtTransactionDescription
            // 
            this.txtTransactionDescription.Location = new System.Drawing.Point(177, 175);
            this.txtTransactionDescription.Multiline = true;
            this.txtTransactionDescription.Name = "txtTransactionDescription";
            this.txtTransactionDescription.Size = new System.Drawing.Size(400, 45);
            this.txtTransactionDescription.TabIndex = 5;
            this.txtTransactionDescription.TextChanged += new System.EventHandler(this.TxtTransactionDescription_TextChanged);
            // 
            // grpboxTransactionLookup
            // 
            this.grpboxTransactionLookup.Controls.Add(this.btnTransactionBind);
            this.grpboxTransactionLookup.Controls.Add(this.txtTransactionLookup);
            this.grpboxTransactionLookup.Controls.Add(this.lblTransactionLookup);
            this.grpboxTransactionLookup.Location = new System.Drawing.Point(32, 20);
            this.grpboxTransactionLookup.Name = "grpboxTransactionLookup";
            this.grpboxTransactionLookup.Size = new System.Drawing.Size(626, 55);
            this.grpboxTransactionLookup.TabIndex = 21;
            this.grpboxTransactionLookup.TabStop = false;
            this.grpboxTransactionLookup.Text = "Load Transaction for update and delete";
            // 
            // btnTransactionBind
            // 
            this.btnTransactionBind.Location = new System.Drawing.Point(320, 18);
            this.btnTransactionBind.Name = "btnTransactionBind";
            this.btnTransactionBind.Size = new System.Drawing.Size(139, 27);
            this.btnTransactionBind.TabIndex = 2;
            this.btnTransactionBind.Text = "Bind Transaction";
            this.btnTransactionBind.UseVisualStyleBackColor = true;
            this.btnTransactionBind.Click += new System.EventHandler(this.BtnTransactionBind_Click);
            // 
            // txtTransactionLookup
            // 
            this.txtTransactionLookup.Location = new System.Drawing.Point(158, 20);
            this.txtTransactionLookup.Name = "txtTransactionLookup";
            this.txtTransactionLookup.Size = new System.Drawing.Size(150, 22);
            this.txtTransactionLookup.TabIndex = 1;
            // 
            // lblTransactionLookup
            // 
            this.lblTransactionLookup.AutoSize = true;
            this.lblTransactionLookup.Location = new System.Drawing.Point(18, 23);
            this.lblTransactionLookup.Name = "lblTransactionLookup";
            this.lblTransactionLookup.Size = new System.Drawing.Size(95, 16);
            this.lblTransactionLookup.TabIndex = 0;
            this.lblTransactionLookup.Text = "Transaction Id:";
            // 
            // dtpTransactionDate
            // 
            this.dtpTransactionDate.CustomFormat = "dd-MM-yyyy";
            this.dtpTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTransactionDate.Location = new System.Drawing.Point(177, 141);
            this.dtpTransactionDate.Name = "dtpTransactionDate";
            this.dtpTransactionDate.Size = new System.Drawing.Size(200, 22);
            this.dtpTransactionDate.TabIndex = 3;
            this.dtpTransactionDate.ValueChanged += new System.EventHandler(this.DtpTransactionDate_ValueChanged);
            // 
            // txtTransactionId
            // 
            this.txtTransactionId.Location = new System.Drawing.Point(177, 105);
            this.txtTransactionId.Name = "txtTransactionId";
            this.txtTransactionId.Size = new System.Drawing.Size(200, 22);
            this.txtTransactionId.TabIndex = 1;
            this.txtTransactionId.Visible = false;
            // 
            // btnTransactionClear
            // 
            this.btnTransactionClear.Location = new System.Drawing.Point(416, 620);
            this.btnTransactionClear.Name = "btnTransactionClear";
            this.btnTransactionClear.Size = new System.Drawing.Size(110, 35);
            this.btnTransactionClear.TabIndex = 15;
            this.btnTransactionClear.Text = "Clear";
            this.btnTransactionClear.UseVisualStyleBackColor = true;
            this.btnTransactionClear.Click += new System.EventHandler(this.BtnTransactionClear_Click);
            // 
            // btnTransactionDelete
            // 
            this.btnTransactionDelete.Location = new System.Drawing.Point(288, 620);
            this.btnTransactionDelete.Name = "btnTransactionDelete";
            this.btnTransactionDelete.Size = new System.Drawing.Size(110, 35);
            this.btnTransactionDelete.TabIndex = 14;
            this.btnTransactionDelete.Text = "Delete";
            this.btnTransactionDelete.UseVisualStyleBackColor = true;
            this.btnTransactionDelete.Click += new System.EventHandler(this.BtnTransactionDelete_Click);
            // 
            // btnTransactionEdit
            // 
            this.btnTransactionEdit.Location = new System.Drawing.Point(160, 620);
            this.btnTransactionEdit.Name = "btnTransactionEdit";
            this.btnTransactionEdit.Size = new System.Drawing.Size(110, 35);
            this.btnTransactionEdit.TabIndex = 13;
            this.btnTransactionEdit.Text = "Update";
            this.btnTransactionEdit.UseVisualStyleBackColor = true;
            this.btnTransactionEdit.Click += new System.EventHandler(this.BtnTransactionEdit_Click);
            // 
            // btnTransactionAdd
            // 
            this.btnTransactionAdd.Location = new System.Drawing.Point(32, 620);
            this.btnTransactionAdd.Name = "btnTransactionAdd";
            this.btnTransactionAdd.Size = new System.Drawing.Size(110, 35);
            this.btnTransactionAdd.TabIndex = 12;
            this.btnTransactionAdd.Text = "Add";
            this.btnTransactionAdd.UseVisualStyleBackColor = true;
            this.btnTransactionAdd.Click += new System.EventHandler(this.BtnTransactionAdd_Click);
            // 
            // grpboxTransactionCategory
            // 
            this.grpboxTransactionCategory.Controls.Add(this.btnTransactionAddNewCat);
            this.grpboxTransactionCategory.Controls.Add(this.txtTransactionNewCat);
            this.grpboxTransactionCategory.Controls.Add(this.lblTransactionNewCat);
            this.grpboxTransactionCategory.Controls.Add(this.lstboxTransactionExpenseCategory);
            this.grpboxTransactionCategory.Controls.Add(this.lblTransactionExpenseCategory);
            this.grpboxTransactionCategory.Controls.Add(this.cmbIncomeCategory);
            this.grpboxTransactionCategory.Controls.Add(this.lblTransactionIncomeCategory);
            this.grpboxTransactionCategory.Location = new System.Drawing.Point(32, 395);
            this.grpboxTransactionCategory.Name = "grpboxTransactionCategory";
            this.grpboxTransactionCategory.Size = new System.Drawing.Size(626, 180);
            this.grpboxTransactionCategory.TabIndex = 9;
            this.grpboxTransactionCategory.TabStop = false;
            this.grpboxTransactionCategory.Text = "Category";
            // 
            // btnTransactionAddNewCat
            // 
            this.btnTransactionAddNewCat.Location = new System.Drawing.Point(320, 140);
            this.btnTransactionAddNewCat.Name = "btnTransactionAddNewCat";
            this.btnTransactionAddNewCat.Size = new System.Drawing.Size(70, 27);
            this.btnTransactionAddNewCat.TabIndex = 11;
            this.btnTransactionAddNewCat.Text = "Add";
            this.btnTransactionAddNewCat.UseVisualStyleBackColor = true;
            this.btnTransactionAddNewCat.Visible = false;
            this.btnTransactionAddNewCat.Click += new System.EventHandler(this.BtnTransactionAddNewCat_Click);
            // 
            // txtTransactionNewCat
            // 
            this.txtTransactionNewCat.Location = new System.Drawing.Point(158, 142);
            this.txtTransactionNewCat.Name = "txtTransactionNewCat";
            this.txtTransactionNewCat.Size = new System.Drawing.Size(150, 22);
            this.txtTransactionNewCat.TabIndex = 10;
            this.txtTransactionNewCat.Visible = false;
            // 
            // lblTransactionNewCat
            // 
            this.lblTransactionNewCat.AutoSize = true;
            this.lblTransactionNewCat.Location = new System.Drawing.Point(18, 145);
            this.lblTransactionNewCat.Name = "lblTransactionNewCat";
            this.lblTransactionNewCat.Size = new System.Drawing.Size(95, 16);
            this.lblTransactionNewCat.TabIndex = 4;
            this.lblTransactionNewCat.Text = "New Category:";
            this.lblTransactionNewCat.Visible = false;
            // 
            // lstboxTransactionExpenseCategory
            // 
            this.lstboxTransactionExpenseCategory.FormattingEnabled = true;
            this.lstboxTransactionExpenseCategory.ItemHeight = 16;
            this.lstboxTransactionExpenseCategory.Location = new System.Drawing.Point(158, 67);
            this.lstboxTransactionExpenseCategory.Name = "lstboxTransactionExpenseCategory";
            this.lstboxTransactionExpenseCategory.Size = new System.Drawing.Size(150, 68);
            this.lstboxTransactionExpenseCategory.TabIndex = 3;
            this.lstboxTransactionExpenseCategory.SelectedIndexChanged += new System.EventHandler(this.LstboxTransactionExpenseCategory_SelectedIndexChanged);
            // 
            // lblTransactionExpenseCategory
            // 
            this.lblTransactionExpenseCategory.AutoSize = true;
            this.lblTransactionExpenseCategory.Location = new System.Drawing.Point(18, 70);
            this.lblTransactionExpenseCategory.Name = "lblTransactionExpenseCategory";
            this.lblTransactionExpenseCategory.Size = new System.Drawing.Size(121, 16);
            this.lblTransactionExpenseCategory.TabIndex = 2;
            this.lblTransactionExpenseCategory.Text = "Expense Category:";
            // 
            // cmbIncomeCategory
            // 
            this.cmbIncomeCategory.FormattingEnabled = true;
            this.cmbIncomeCategory.Location = new System.Drawing.Point(158, 28);
            this.cmbIncomeCategory.Name = "cmbIncomeCategory";
            this.cmbIncomeCategory.Size = new System.Drawing.Size(150, 24);
            this.cmbIncomeCategory.TabIndex = 1;
            this.cmbIncomeCategory.SelectedIndexChanged += new System.EventHandler(this.CmbIncomeCategory_SelectedIndexChanged);
            // 
            // lblTransactionIncomeCategory
            // 
            this.lblTransactionIncomeCategory.AutoSize = true;
            this.lblTransactionIncomeCategory.Location = new System.Drawing.Point(18, 32);
            this.lblTransactionIncomeCategory.Name = "lblTransactionIncomeCategory";
            this.lblTransactionIncomeCategory.Size = new System.Drawing.Size(112, 16);
            this.lblTransactionIncomeCategory.TabIndex = 0;
            this.lblTransactionIncomeCategory.Text = "Income Category:";
            // 
            // numupdTransactionAmount
            // 
            this.numupdTransactionAmount.DecimalPlaces = 2;
            this.numupdTransactionAmount.Maximum = decimal.MaxValue;
            this.numupdTransactionAmount.Location = new System.Drawing.Point(177, 350);
            this.numupdTransactionAmount.Name = "numupdTransactionAmount";
            this.numupdTransactionAmount.Size = new System.Drawing.Size(150, 22);
            this.numupdTransactionAmount.TabIndex = 8;
            this.numupdTransactionAmount.ThousandsSeparator = true;
            this.numupdTransactionAmount.TextChanged += new System.EventHandler(this.NumupdTransactionAmount_TextChanged);
            this.numupdTransactionAmount.ValueChanged += new System.EventHandler(this.NumupdTransactionAmount_ValueChanged);
            // 
            // lblTransactionAmount
            // 
            this.lblTransactionAmount.AutoSize = true;
            this.lblTransactionAmount.Location = new System.Drawing.Point(34, 356);
            this.lblTransactionAmount.Name = "lblTransactionAmount";
            this.lblTransactionAmount.Size = new System.Drawing.Size(55, 16);
            this.lblTransactionAmount.TabIndex = 7;
            this.lblTransactionAmount.Text = "Amount:";
            // 
            // grpboxTransactionType
            // 
            this.grpboxTransactionType.Controls.Add(this.rbTransactionExpense);
            this.grpboxTransactionType.Controls.Add(this.rbTransactionIncome);
            this.grpboxTransactionType.Location = new System.Drawing.Point(32, 245);
            this.grpboxTransactionType.Name = "grpboxTransactionType";
            this.grpboxTransactionType.Size = new System.Drawing.Size(364, 80);
            this.grpboxTransactionType.TabIndex = 6;
            this.grpboxTransactionType.TabStop = false;
            this.grpboxTransactionType.Text = "Transaction Type";
            // 
            // rbTransactionExpense
            // 
            this.rbTransactionExpense.Location = new System.Drawing.Point(158, 34);
            this.rbTransactionExpense.Name = "rbTransactionExpense";
            this.rbTransactionExpense.Size = new System.Drawing.Size(104, 24);
            this.rbTransactionExpense.TabIndex = 1;
            this.rbTransactionExpense.Text = "Expense";
            this.rbTransactionExpense.UseVisualStyleBackColor = true;
            this.rbTransactionExpense.CheckedChanged += new System.EventHandler(this.RbTransactionExpense_CheckedChanged);
            // 
            // rbTransactionIncome
            // 
            this.rbTransactionIncome.Checked = true;
            this.rbTransactionIncome.Location = new System.Drawing.Point(18, 34);
            this.rbTransactionIncome.Name = "rbTransactionIncome";
            this.rbTransactionIncome.Size = new System.Drawing.Size(104, 24);
            this.rbTransactionIncome.TabIndex = 0;
            this.rbTransactionIncome.TabStop = true;
            this.rbTransactionIncome.Text = "Income";
            this.rbTransactionIncome.UseVisualStyleBackColor = true;
            this.rbTransactionIncome.CheckedChanged += new System.EventHandler(this.RbTransactionIncome_CheckedChanged);
            // 
            // lblTransactionDesc
            // 
            this.lblTransactionDesc.AutoSize = true;
            this.lblTransactionDesc.Location = new System.Drawing.Point(34, 178);
            this.lblTransactionDesc.Name = "lblTransactionDesc";
            this.lblTransactionDesc.Size = new System.Drawing.Size(78, 16);
            this.lblTransactionDesc.TabIndex = 4;
            this.lblTransactionDesc.Text = "Description:";
            // 
            // lblTransactionDate
            // 
            this.lblTransactionDate.AutoSize = true;
            this.lblTransactionDate.Location = new System.Drawing.Point(34, 144);
            this.lblTransactionDate.Name = "lblTransactionDate";
            this.lblTransactionDate.Size = new System.Drawing.Size(113, 16);
            this.lblTransactionDate.TabIndex = 2;
            this.lblTransactionDate.Text = "Transaction Date:";
            // 
            // lblTransactionId
            // 
            this.lblTransactionId.AutoSize = true;
            this.lblTransactionId.Location = new System.Drawing.Point(34, 109);
            this.lblTransactionId.Name = "lblTransactionId";
            this.lblTransactionId.Size = new System.Drawing.Size(95, 16);
            this.lblTransactionId.TabIndex = 0;
            this.lblTransactionId.Text = "Transaction Id:";
            this.lblTransactionId.Visible = false;
            // 
            // lblTransactionCategoryError
            // 
            this.lblTransactionCategoryError.AutoSize = true;
            this.lblTransactionCategoryError.ForeColor = System.Drawing.Color.Red;
            this.lblTransactionCategoryError.Location = new System.Drawing.Point(34, 585);
            this.lblTransactionCategoryError.Name = "lblTransactionCategoryError";
            this.lblTransactionCategoryError.Size = new System.Drawing.Size(0, 16);
            this.lblTransactionCategoryError.TabIndex = 20;
            this.lblTransactionCategoryError.Visible = false;
            // 
            // lblTransactionAmountError
            // 
            this.lblTransactionAmountError.AutoSize = true;
            this.lblTransactionAmountError.ForeColor = System.Drawing.Color.Red;
            this.lblTransactionAmountError.Location = new System.Drawing.Point(179, 374);
            this.lblTransactionAmountError.Name = "lblTransactionAmountError";
            this.lblTransactionAmountError.Size = new System.Drawing.Size(0, 16);
            this.lblTransactionAmountError.TabIndex = 19;
            this.lblTransactionAmountError.Visible = false;
            // 
            // lblTransactionTypeError
            // 
            this.lblTransactionTypeError.AutoSize = true;
            this.lblTransactionTypeError.ForeColor = System.Drawing.Color.Red;
            this.lblTransactionTypeError.Location = new System.Drawing.Point(34, 328);
            this.lblTransactionTypeError.Name = "lblTransactionTypeError";
            this.lblTransactionTypeError.Size = new System.Drawing.Size(0, 16);
            this.lblTransactionTypeError.TabIndex = 18;
            this.lblTransactionTypeError.Visible = false;
            // 
            // lblTransactionDescError
            // 
            this.lblTransactionDescError.AutoSize = true;
            this.lblTransactionDescError.ForeColor = System.Drawing.Color.Red;
            this.lblTransactionDescError.Location = new System.Drawing.Point(179, 223);
            this.lblTransactionDescError.Name = "lblTransactionDescError";
            this.lblTransactionDescError.Size = new System.Drawing.Size(0, 16);
            this.lblTransactionDescError.TabIndex = 17;
            this.lblTransactionDescError.Visible = false;
            // 
            // lblTransactionDateError
            // 
            this.lblTransactionDateError.AutoSize = true;
            this.lblTransactionDateError.ForeColor = System.Drawing.Color.Red;
            this.lblTransactionDateError.Location = new System.Drawing.Point(387, 144);
            this.lblTransactionDateError.Name = "lblTransactionDateError";
            this.lblTransactionDateError.Size = new System.Drawing.Size(0, 16);
            this.lblTransactionDateError.TabIndex = 16;
            this.lblTransactionDateError.Visible = false;
            // 
            // FrmTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(238, 246, 247);
            this.ForeColor = System.Drawing.Color.FromArgb(27, 49, 58);
            this.ClientSize = new System.Drawing.Size(850, 785);
            this.Controls.Add(this.grpboxTransaction);
            this.Name = "FrmTransaction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transaction Management";
            this.Resize += new System.EventHandler(this.FrmTransaction_Resize);
            this.grpboxTransaction.ResumeLayout(false);
            this.grpboxTransaction.PerformLayout();
            this.grpboxTransactionLookup.ResumeLayout(false);
            this.grpboxTransactionLookup.PerformLayout();
            this.grpboxTransactionCategory.ResumeLayout(false);
            this.grpboxTransactionCategory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numupdTransactionAmount)).EndInit();
            this.grpboxTransactionType.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion


        private void FrmTransaction_Resize(object sender, System.EventArgs e)
        {
            this.grpboxTransaction.Left = ((this.ClientSize.Width - this.grpboxTransaction.Width)
                        / 2);
            this.grpboxTransaction.Top = ((this.ClientSize.Height - this.grpboxTransaction.Height)
                        / 2);
        }
        private System.Windows.Forms.GroupBox grpboxTransaction;
        private System.Windows.Forms.TextBox txtTransactionDescription;
        private System.Windows.Forms.GroupBox grpboxTransactionLookup;
        private System.Windows.Forms.Button btnTransactionBind;
        private System.Windows.Forms.TextBox txtTransactionLookup;
        private System.Windows.Forms.Label lblTransactionLookup;
        private System.Windows.Forms.DateTimePicker dtpTransactionDate;
        private System.Windows.Forms.TextBox txtTransactionId;
        private System.Windows.Forms.Button btnTransactionClear;
        private System.Windows.Forms.Button btnTransactionDelete;
        private System.Windows.Forms.Button btnTransactionEdit;
        private System.Windows.Forms.Button btnTransactionAdd;
        private System.Windows.Forms.GroupBox grpboxTransactionCategory;
        private System.Windows.Forms.Button btnTransactionAddNewCat;
        private System.Windows.Forms.TextBox txtTransactionNewCat;
        private System.Windows.Forms.Label lblTransactionNewCat;
        private System.Windows.Forms.ListBox lstboxTransactionExpenseCategory;
        private System.Windows.Forms.Label lblTransactionExpenseCategory;
        private System.Windows.Forms.ComboBox cmbIncomeCategory;
        private System.Windows.Forms.Label lblTransactionIncomeCategory;
        private System.Windows.Forms.NumericUpDown numupdTransactionAmount;
        private System.Windows.Forms.Label lblTransactionAmount;
        private System.Windows.Forms.GroupBox grpboxTransactionType;
        private System.Windows.Forms.RadioButton rbTransactionExpense;
        private System.Windows.Forms.RadioButton rbTransactionIncome;
        private System.Windows.Forms.Label lblTransactionDesc;
        private System.Windows.Forms.Label lblTransactionDate;
        private System.Windows.Forms.Label lblTransactionId;
        private System.Windows.Forms.Label lblTransactionCategoryError;
        private System.Windows.Forms.Label lblTransactionAmountError;
        private System.Windows.Forms.Label lblTransactionTypeError;
        private System.Windows.Forms.Label lblTransactionDescError;
        private System.Windows.Forms.Label lblTransactionDateError;
    }
}