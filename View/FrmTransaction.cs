// using System;
// using System.Data;
// using System.Windows.Forms;
// using MoneyFlow.Service;

// namespace MoneyFlow
// {
//     public static class UserSession
//         {
//             public static int UserId = 1;
//             public static string Username = "admin";
//         }
//     public partial class FrmTransaction : Form
//     {
//         private readonly TransactionService _transactionService = new TransactionService();
//         public FrmTransaction()
//         {
//             InitializeComponent();
//             LoadIncomeCategories();
//             LoadExpenseCategories();
//         }

// private void LoadExpenseCategories()
// {
//     try
//     {
//         DataTable dt = _transactionService.GetCategories(
//             UserSession.UserId,
//             "Expense");

//         DataRow otherRow = dt.NewRow();

//         otherRow["c_category_id"] = 0;
//         otherRow["c_category_name"] = "Other";
//         otherRow["c_category_type"] = "Expense";
//         otherRow["c_user_id"] = UserSession.UserId;

//         dt.Rows.Add(otherRow);

//         lstboxTransactionExpenseCategory.DataSource = dt;
//         lstboxTransactionExpenseCategory.DisplayMember =
//             "c_category_name";
//         lstboxTransactionExpenseCategory.ValueMember =
//             "c_category_id";

//         lstboxTransactionExpenseCategory.SelectedIndex = -1;
//     }
//     catch (Exception ex)
//     {
//         MessageBox.Show(
//             ex.Message,
//             "Error",
//             MessageBoxButtons.OK,
//             MessageBoxIcon.Error);
//     }
// }
//         private void LoadIncomeCategories()
// {
//     try
//     {
//         DataTable dt = _transactionService.GetCategories(
//             UserSession.UserId,
//             "Income");

//         DataRow otherRow = dt.NewRow();

//         otherRow["c_category_id"] = 0;
//         otherRow["c_category_name"] = "Other";
//         otherRow["c_category_type"] = "Income";
//         otherRow["c_user_id"] = UserSession.UserId;

//         dt.Rows.Add(otherRow);

//         cmbIncomeCategory.DataSource = dt;
//         cmbIncomeCategory.DisplayMember = "c_category_name";
//         cmbIncomeCategory.ValueMember = "c_category_id";

//         cmbIncomeCategory.SelectedIndex = -1;
//     }
//     catch (Exception ex)
//     {
//         MessageBox.Show(
//             ex.Message,
//             "Error",
//             MessageBoxButtons.OK,
//             MessageBoxIcon.Error);
//     }
// }
//         private void ClearValidationMessages()
//         {
//             lblTransactionDateError.Text = "";
//             lblTransactionDescError.Text = "";
//             lblTransactionTypeError.Text = "";
//             lblTransactionAmountError.Text = "";
//             lblTransactionCategoryError.Text = "";

//             lblTransactionDateError.Visible = false;
//             lblTransactionDescError.Visible = false;
//             lblTransactionTypeError.Visible = false;
//             lblTransactionAmountError.Visible = false;
//             lblTransactionCategoryError.Visible = false;
//         }

//         private void RbTransactionIncome_CheckedChanged(object sender, System.EventArgs e)
//         {
//                  if (rbTransactionIncome.Checked)
//                     {
//                         LoadIncomeCategories();

//                         cmbIncomeCategory.Visible = true;
//                         lblTransactionIncomeCategory.Visible = true;

//                         lstboxTransactionExpenseCategory.Visible = false;
//                         lblTransactionExpenseCategory.Visible = false;
//                     }
//         }

//         private void RbTransactionExpense_CheckedChanged(object sender, System.EventArgs e)
//         {
//             if (rbTransactionExpense.Checked)
//             {
//                 LoadExpenseCategories();

//                 cmbIncomeCategory.Visible = false;
//                 lblTransactionIncomeCategory.Visible = false;

//                 lstboxTransactionExpenseCategory.Visible = true;
//                 lblTransactionExpenseCategory.Visible = true;
//             }
//         }
//     }
// }

using System;
using System.Data;
using System.Windows.Forms;
using MoneyFlow.Service;

namespace MoneyFlow
{
    public static class UserSession
    {
        public static int UserId = 1;
        public static string Username = "admin";
    }

    public partial class FrmTransaction : Form
    {
        private readonly TransactionService _transactionService =
            new TransactionService();
        private readonly TransactionCategoryService _transactionCategoryService =
            new TransactionCategoryService();

        public FrmTransaction()
        {
            InitializeComponent();

            // Default = Income
            rbTransactionIncome.Checked = true;

            // Show income controls
            cmbIncomeCategory.Visible = true;
            lblTransactionIncomeCategory.Visible = true;

            // Hide expense controls
            lstboxTransactionExpenseCategory.Visible = false;
            lblTransactionExpenseCategory.Visible = false;

            // Hide new category controls
            HideNewCategoryControls();

            ClearValidationMessages();

            // Load income categories
            LoadIncomeCategories();
        }

        // =========================================================
        // LOAD INCOME CATEGORIES
        // =========================================================
        private void LoadIncomeCategories()
        {
            try
            {
                DataTable dt =
                    _transactionCategoryService.GetCategories(
                        UserSession.UserId,
                        "Income");

                AddOtherOption(dt, "Income");

                cmbIncomeCategory.DataSource = null;
                cmbIncomeCategory.DataSource = dt;
                cmbIncomeCategory.DisplayMember =
                    "c_category_name";
                cmbIncomeCategory.ValueMember =
                    "c_category_id";

                cmbIncomeCategory.SelectedIndex = -1;

                HideNewCategoryControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // =========================================================
        // LOAD EXPENSE CATEGORIES
        // =========================================================
        private void LoadExpenseCategories()
        {
            try
            {
                DataTable dt =
                    _transactionCategoryService.GetCategories(
                        UserSession.UserId,
                        "Expense");

                AddOtherOption(dt, "Expense");

                lstboxTransactionExpenseCategory.DataSource = null;
                lstboxTransactionExpenseCategory.DataSource = dt;
                lstboxTransactionExpenseCategory.DisplayMember =
                    "c_category_name";
                lstboxTransactionExpenseCategory.ValueMember =
                    "c_category_id";

                lstboxTransactionExpenseCategory.SelectedIndex = -1;

                HideNewCategoryControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // =========================================================
        // ADD "OTHER" TO DATATABLE
        // =========================================================
        private void AddOtherOption(
            DataTable dt,
            string categoryType)
        {
            DataRow otherRow = dt.NewRow();

            otherRow["c_category_id"] = 0;
            otherRow["c_category_name"] = "Other";
            otherRow["c_category_type"] = categoryType;
            otherRow["c_user_id"] = UserSession.UserId;

            dt.Rows.Add(otherRow);
        }


        // =========================================================
        // INCOME RADIO BUTTON
        // =========================================================
        private void RbTransactionIncome_CheckedChanged(
            object sender,
            EventArgs e)
        {
           if (!rbTransactionIncome.Checked)
        return;

            // Show income
            cmbIncomeCategory.Visible = true;
            lblTransactionIncomeCategory.Visible = true;

            // Hide expense
            lstboxTransactionExpenseCategory.Visible = false;
            lblTransactionExpenseCategory.Visible = false;

            // Hide new category controls
            HideNewCategoryControls();

            // Load income categories
            LoadIncomeCategories();
            
        }


        // =========================================================
        // EXPENSE RADIO BUTTON
        // =========================================================
        private void RbTransactionExpense_CheckedChanged(
            object sender,
            EventArgs e)
        {
             if (!rbTransactionExpense.Checked)
                return;

            // Hide income
            cmbIncomeCategory.Visible = false;
            lblTransactionIncomeCategory.Visible = false;

            // Show expense
            lstboxTransactionExpenseCategory.Visible = true;
            lblTransactionExpenseCategory.Visible = true;

            // Hide new category controls
            HideNewCategoryControls();

            // Load expense categories
            LoadExpenseCategories();
        }


        // =========================================================
        // INCOME CATEGORY SELECTION
        // =========================================================
        private void CmbIncomeCategory_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (cmbIncomeCategory.SelectedValue == null)
                return;

            if (cmbIncomeCategory.SelectedValue is DataRowView)
                return;

            int categoryId;

            if (!int.TryParse(
                cmbIncomeCategory.SelectedValue.ToString(),
                out categoryId))
            {
                return;
            }

            if (categoryId == 0)
            {
                ShowNewCategoryControls();
            }
            else
            {
                HideNewCategoryControls();
                ClearValidationMessage(lblTransactionCategoryError);
            }
        }


        // =========================================================
        // EXPENSE CATEGORY SELECTION
        // =========================================================
        private void LstboxTransactionExpenseCategory_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            if (lstboxTransactionExpenseCategory.SelectedValue == null)
                return;

            if (lstboxTransactionExpenseCategory.SelectedValue
                is DataRowView)
                return;

            int categoryId;

            if (!int.TryParse(
                lstboxTransactionExpenseCategory.SelectedValue.ToString(),
                out categoryId))
            {
                return;
            }

            if (categoryId == 0)
            {
                ShowNewCategoryControls();
            }
            else
            {
                HideNewCategoryControls();
                ClearValidationMessage(lblTransactionCategoryError);
            }
        }

        private void DtpTransactionDate_ValueChanged(
            object sender,
            EventArgs e)
        {
            if (dtpTransactionDate.Value.Date > DateTime.Today)
            {
                lblTransactionDateError.Text =
                    "Transaction date cannot be in the future.";
                lblTransactionDateError.Visible = true;
            }
            else
            {
                ClearValidationMessage(lblTransactionDateError);
            }
        }

        private void TxtTransactionDescription_TextChanged(
            object sender,
            EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(
                txtTransactionDescription.Text))
            {
                ClearValidationMessage(lblTransactionDescError);
            }
        }

        private void NumupdTransactionAmount_ValueChanged(
            object sender,
            EventArgs e)
        {
            if (numupdTransactionAmount.Value > 0)
            {
                ClearValidationMessage(lblTransactionAmountError);
            }
        }

        private void NumupdTransactionAmount_TextChanged(
            object sender,
            EventArgs e)
        {
            if (decimal.TryParse(
                    numupdTransactionAmount.Text,
                    out decimal amount) &&
                amount > 0)
            {
                ClearValidationMessage(lblTransactionAmountError);
            }
        }

        private void ClearValidationMessage(Label errorLabel)
        {
            errorLabel.Text = string.Empty;
            errorLabel.Visible = false;
        }


        // =========================================================
        // SHOW NEW CATEGORY CONTROLS
        // =========================================================
        private void ShowNewCategoryControls()
        {
            lblTransactionNewCat.Visible = true;
            txtTransactionNewCat.Visible = true;
            btnTransactionAddNewCat.Visible = true;

            txtTransactionNewCat.Focus();
        }


        // =========================================================
        // HIDE NEW CATEGORY CONTROLS
        // =========================================================
        private void HideNewCategoryControls()
        {
            lblTransactionNewCat.Visible = false;
            txtTransactionNewCat.Visible = false;
            btnTransactionAddNewCat.Visible = false;

            txtTransactionNewCat.Clear();
        }


        // =========================================================
        // ADD NEW CATEGORY BUTTON
        // =========================================================
        private void BtnTransactionAddNewCat_Click(
            object sender,
            EventArgs e)
        {
            string categoryName =
                txtTransactionNewCat.Text.Trim();

            // ---------------------------------------------
            // VALIDATION
            // ---------------------------------------------
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                MessageBox.Show(
                    "Please enter category name.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtTransactionNewCat.Focus();
                return;
            }

            if (categoryName.Length > 100)
            {
                MessageBox.Show(
                    "Category name cannot exceed 100 characters.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtTransactionNewCat.Focus();
                return;
            }

            // Optional: prevent only spaces/special characters
            bool hasLetterOrDigit = false;

            foreach (char c in categoryName)
            {
                if (char.IsLetterOrDigit(c))
                {
                    hasLetterOrDigit = true;
                    break;
                }
            }

            if (!hasLetterOrDigit)
            {
                MessageBox.Show(
                    "Please enter a valid category name.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtTransactionNewCat.Focus();
                return;
            }


            // ---------------------------------------------
            // DETERMINE CATEGORY TYPE
            // ---------------------------------------------
            string categoryType;

            if (rbTransactionIncome.Checked)
            {
                categoryType = "Income";
            }
            else if (rbTransactionExpense.Checked)
            {
                categoryType = "Expense";
            }
            else
            {
                MessageBox.Show(
                    "Please select transaction type.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            // ---------------------------------------------
            // CHECK DUPLICATE
            // ---------------------------------------------
            try
            {
                bool exists =
                    _transactionCategoryService.CategoryExists(
                        UserSession.UserId,
                        categoryName,
                        categoryType);

                if (exists)
                {
                    MessageBox.Show(
                        "This category already exists.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtTransactionNewCat.Focus();
                    return;
                }


                // ---------------------------------------------
                // INSERT CATEGORY
                // ---------------------------------------------
                int newCategoryId =
                    _transactionCategoryService.AddCategory(
                        categoryName,
                        categoryType,
                        UserSession.UserId);


                MessageBox.Show(
                    "Category added successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);


                // ---------------------------------------------
                // RELOAD CATEGORY LIST
                // ---------------------------------------------
                if (categoryType == "Income")
                {
                    LoadIncomeCategories();

                    SelectIncomeCategory(newCategoryId);
                }
                else
                {
                    LoadExpenseCategories();

                    SelectExpenseCategory(newCategoryId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // =========================================================
        // SELECT NEW INCOME CATEGORY
        // =========================================================
        private void SelectIncomeCategory(int categoryId)
        {
            for (int i = 0;
                 i < cmbIncomeCategory.Items.Count;
                 i++)
            {
                DataRowView row =
                    cmbIncomeCategory.Items[i] as DataRowView;

                if (row != null &&
                    Convert.ToInt32(
                        row["c_category_id"]) == categoryId)
                {
                    cmbIncomeCategory.SelectedIndex = i;
                    break;
                }
            }
        }


        // =========================================================
        // SELECT NEW EXPENSE CATEGORY
        // =========================================================
        private void SelectExpenseCategory(int categoryId)
        {
            for (int i = 0;
                 i < lstboxTransactionExpenseCategory.Items.Count;
                 i++)
            {
                DataRowView row =
                    lstboxTransactionExpenseCategory.Items[i]
                    as DataRowView;

                if (row != null &&
                    Convert.ToInt32(
                        row["c_category_id"]) == categoryId)
                {
                    lstboxTransactionExpenseCategory.SelectedIndex = i;
                    break;
                }
            }
        }


        // =========================================================
        // CLEAR VALIDATION MESSAGES
        // =========================================================
        private void ClearValidationMessages()
        {
            lblTransactionDateError.Text = "";
            lblTransactionDescError.Text = "";
            lblTransactionTypeError.Text = "";
            lblTransactionAmountError.Text = "";
            lblTransactionCategoryError.Text = "";

            lblTransactionDateError.Visible = false;
            lblTransactionDescError.Visible = false;
            lblTransactionTypeError.Visible = false;
            lblTransactionAmountError.Visible = false;
            lblTransactionCategoryError.Visible = false;
        }

        private bool ValidateTransaction(
            out string transactionType,
            out int categoryId)
        {
            ClearValidationMessages();

            bool isValid = true;
            transactionType = string.Empty;
            categoryId = 0;

            if (!rbTransactionIncome.Checked &&
                !rbTransactionExpense.Checked)
            {
                lblTransactionTypeError.Text =
                    "Select transaction type.";
                lblTransactionTypeError.Visible = true;
                isValid = false;
            }
            else
            {
                transactionType = rbTransactionIncome.Checked
                    ? "Income"
                    : "Expense";
                ClearValidationMessage(lblTransactionTypeError);
            }

            if (dtpTransactionDate.Value.Date > DateTime.Today)
            {
                lblTransactionDateError.Text =
                    "Transaction date cannot be in the future.";
                lblTransactionDateError.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(
                txtTransactionDescription.Text))
            {
                lblTransactionDescError.Text =
                    "Description is required.";
                lblTransactionDescError.Visible = true;
                isValid = false;
            }

            if (numupdTransactionAmount.Value <= 0)
            {
                lblTransactionAmountError.Text =
                    "Amount must be greater than zero.";
                lblTransactionAmountError.Visible = true;
                isValid = false;
            }

            object? selectedValue = transactionType == "Income"
                ? cmbIncomeCategory.SelectedValue
                : lstboxTransactionExpenseCategory.SelectedValue;

            if (selectedValue == null ||
                selectedValue is DataRowView ||
                !int.TryParse(selectedValue.ToString(), out categoryId) ||
                categoryId == 0)
            {
                lblTransactionCategoryError.Text =
                    "Select a category.";
                lblTransactionCategoryError.Visible = true;
                isValid = false;
            }

            return isValid;
        }

        private void BtnTransactionAdd_Click(
            object sender,
            EventArgs e)
        {
            if (!ValidateTransaction(
                out string transactionType,
                out int categoryId))
            {
                return;
            }

            try
            {
                _transactionService.AddTransaction(
                    transactionType,
                    categoryId,
                    numupdTransactionAmount.Value,
                    dtpTransactionDate.Value.Date,
                    UserSession.UserId,
                    txtTransactionDescription.Text.Trim());

                MessageBox.Show(
                    "Transaction added successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtTransactionDescription.Clear();
                numupdTransactionAmount.Value = 0;
                ClearValidationMessages();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}