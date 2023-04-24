using DevExpress.XtraEditors;
using DTO;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;

namespace GUI
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
       
        public Main(Account account,Employee emp)
        {
            InitializeComponent();
        }

        void LoadDataGridViewEmployee()
        {
            using (AirportManager db = new AirportManager())
            {
                dgvEmployee.DataSource = db.Employees.ToList<Employee>();
            }
        }
        void LoadDataGridViewJob()
        {
            using (AirportManager db = new AirportManager())
            {
                dgvJob.DataSource= db.Jobs.ToList<Job>();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            Account account = new Account();
            BUS_Employee busEmp = new BUS_Employee();
            BUS_Account busAcc = new BUS_Account();
            DAL_Account dalAccount = new DAL_Account();
            DAL_Employee dalEmployee = new DAL_Employee();

            if (String.IsNullOrEmpty(txtNameEmployee.Text) || String.IsNullOrEmpty(txtAddressEmployee.Text) || String.IsNullOrEmpty(txtNationalityEmployee.Text) || String.IsNullOrEmpty(txtEmailEmployee.Text) || String.IsNullOrEmpty(txtNationalIDEmployee.Text) || String.IsNullOrEmpty(txtPhoneEmployee.Text) || String.IsNullOrEmpty(txtPhoneEmployee.Text))
            {
                MessageBox.Show("Please enter all the information!");
            }
            else
            {
                employee.Name = txtNameEmployee.Text.Trim();
                employee.Address = txtAddressEmployee.Text.Trim();
                employee.Nationality = txtNationalityEmployee.Text.Trim();
                if (rbMale.Checked)
                    employee.Sex = true;
                else
                    employee.Sex = false;
                employee.DateOfBirth = dtpBirthdayEmployee.Value.Date;
                employee.Email = txtEmailEmployee.Text.Trim();
                employee.NationID = txtNationalIDEmployee.Text.Trim();
                employee.TeleNumber = txtPhoneEmployee.Text.Trim();
                employee.Position = txtPositionEmployee.Text.Trim();




                if (busEmp.AddEmployee(employee) == 1)
                {
                    account.EmployeeID = dalAccount.takeEmployeeIDbyEmployeeNationalID(txtNationalIDEmployee.Text.Trim());
                    account.Username = txtUsernameEmployee.Text.Trim();
                    account.Password = txtPasswordEmployee.Text.Trim();
                    account.AccessLevel = false;

                    if (busAcc.AddAccount(account) == 2)
                    {
                        MessageBox.Show("Username was exited");
                        LoadDataGridViewEmployee();
                    }
                    else
                    {
                        if (busAcc.AddAccount(account) == 1)
                        {
                            MessageBox.Show("Add Employee Successfully");
                            LoadDataGridViewEmployee();
                        }
                        else
                            MessageBox.Show("Add  Employee Successfully!");
                    }
                }
                else
                {
                        if (busEmp.AddEmployee(employee) == 0)
                            MessageBox.Show("Add Employee Fail");
                        else
                            if (busEmp.AddEmployee(employee) == 2)
                            MessageBox.Show("Exited Phone");
                        else
                                if (busEmp.AddEmployee(employee) == 3)
                            MessageBox.Show("Exited NationalID");
                        else
                                    if (busEmp.AddEmployee(employee) == 4)
                            MessageBox.Show("Exited NationalID and Phone");
                        else
                            MessageBox.Show("Something wrong");
                }
                
            }
        }

        private void gbEmployee_Paint(object sender, PaintEventArgs e)
        {
            txtEmployeeID.ReadOnly = true;
            LoadDataGridViewEmployee();
        }
        private void gbJob_Paint(object sender, PaintEventArgs e)
        {
            LoadDataGridViewJob();
        }

        private void gvEmployee_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gvEmploy.GetRow(gvEmploy.FocusedRowHandle) != null)
            {
                Employee cur_emp = (Employee)gvEmploy.GetRow(gvEmploy.FocusedRowHandle);
                txtEmployeeID.Text = cur_emp.EmployeeID.ToString();
                txtNameEmployee.Text = cur_emp.Name.ToString();
                txtAddressEmployee.Text = cur_emp.Address.ToString();
                txtNationalityEmployee.Text = cur_emp.Nationality.ToString();
            //    dtpBirthday.Value.Date = cur_emp.DateOfBirth.;
                txtNationalIDEmployee.Text = cur_emp.NationID.ToString();
                txtEmailEmployee.Text = cur_emp.Email.ToString(); 
                txtNationalIDEmployee.Text = cur_emp.NationID.ToString();
                txtPhoneEmployee.Text = cur_emp.TeleNumber.ToString();
                txtPositionEmployee.Text = cur_emp.Position.ToString();
                if (cur_emp.Sex == true)
                    rbMale.Checked= true;
                else
                    rbFelmale.Checked= true;
            }    
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            Account account = new Account();
            BUS_Employee busEmp = new BUS_Employee();
            BUS_Account busAcc = new BUS_Account();
            DAL_Account dalAccount = new DAL_Account();
            DAL_Employee dalEmployee = new DAL_Employee();

            if (String.IsNullOrEmpty(txtEmployeeID.Text))
            {
                MessageBox.Show("Please select Employee you want to delete");
            }
            else
            {
                if (MessageBox.Show("Do you want to delete this employee?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (dalEmployee.CheckExitEmployee(Int32.Parse(txtEmployeeID.Text))== false)
                    {
                        MessageBox.Show("This employee not exit!");
                    }    
                    else
                    {
                        if (busAcc.DeleteAccount(Int32.Parse(txtEmployeeID.Text)) == 1)
                        {
                            if (busEmp.DeleteEmployee(Int32.Parse(txtEmployeeID.Text)) == 1)
                                MessageBox.Show("Delete Employee Successfully");
                            else
                                MessageBox.Show("Delete Employee Fail");
                        }
                        else
                        {
                            MessageBox.Show("Fail, Something wrong");
                        }    

                    }
                }
            }
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            Account account = new Account();
            BUS_Employee busEmp = new BUS_Employee();
            BUS_Account busAcc = new BUS_Account();
            DAL_Account dalAccount = new DAL_Account();
            DAL_Employee dalEmployee = new DAL_Employee();

            if (String.IsNullOrEmpty(txtEmployeeID.Text)|| String.IsNullOrEmpty(txtNameEmployee.Text) || String.IsNullOrEmpty(txtAddressEmployee.Text) || String.IsNullOrEmpty(txtNationalityEmployee.Text) || String.IsNullOrEmpty(txtEmailEmployee.Text) || String.IsNullOrEmpty(txtNationalIDEmployee.Text) || String.IsNullOrEmpty(txtPhoneEmployee.Text) || String.IsNullOrEmpty(txtUsernameEmployee.Text) || String.IsNullOrEmpty(txtPasswordEmployee.Text))
            {
                MessageBox.Show("Please enter all the information!");
            }
            else
            {
                employee.EmployeeID = Int32.Parse(txtEmployeeID.Text.Trim());
                employee.Name = txtNameEmployee.Text.Trim();
                employee.Address = txtAddressEmployee.Text.Trim();
                employee.Nationality = txtNationalityEmployee.Text.Trim();
                if (rbMale.Checked)
                    employee.Sex = true;
                else
                    employee.Sex = false;
                employee.DateOfBirth = dtpBirthdayEmployee.Value.Date;
                employee.Email = txtEmailEmployee.Text.Trim();
                employee.NationID = txtNationalIDEmployee.Text.Trim();
                employee.TeleNumber = txtPhoneEmployee.Text.Trim();
                employee.Position = txtPositionEmployee.Text.Trim();


                if (busEmp.UpdateEmployee(employee) == 2)
                {
                    MessageBox.Show("EmployeeID not exit");
                } 
                else
                {
                    if (busEmp.UpdateEmployee(employee) == 1)
                    {
                        account.EmployeeID = dalAccount.takeEmployeeIDbyEmployeeNationalID(txtNationalIDEmployee.Text.Trim());
                        account.Username = txtUsernameEmployee.Text.Trim();
                        account.Password = txtPasswordEmployee.Text.Trim();
                        account.AccessLevel = false;
                        dalAccount.UpdateAccount(account);

                        MessageBox.Show("Update Employee successfully");
                    }    
                    else
                        MessageBox.Show("Something Wrong");
                }    
                    
            }
        }

        private void txtPasswordEmployee_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddJob_Click(object sender, EventArgs e)
        {
            BUS_Job busJob = new BUS_Job();
            Job job = new Job();
            if (String.IsNullOrEmpty(txtEmpIDJob.Text) || String.IsNullOrEmpty(txtFightIDJob.Text) || String.IsNullOrEmpty(txtJobDescription.Text) || cbStateJob.SelectedItem == null)
            {
                MessageBox.Show("Please enter all the information!");
            }
            else
            {
                job.AssignedDate = dtpAssignedDateJob.Value.Date;
                job.EmployeeID = Int32.Parse(txtEmpIDJob.Text.Trim());
                job.FlightID = Int32.Parse(txtFightIDJob.Text.Trim());
                job.JobDescription = txtJobDescription.Text.Trim();
                job.JobState = cbStateJob.SelectedIndex.ToString();
                if (busJob.AddJob(job) == 1)
                {
                    MessageBox.Show(" Add job successfull");
                }
                else
                {
                    if (busJob.AddJob(job) == 2)
                    {
                        MessageBox.Show("NOT EXIT FLIGHTID");
                    }
                    else
                    {
                        if (busJob.AddJob(job) == 3)
                        {

                        }
                    }

                }
            }
                
        }
    }
}