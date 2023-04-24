using DevExpress.XtraEditors;
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
using DevExpress.Mvvm.Native;
using DTO;
using System.Windows.Controls;
using System.Runtime.Serialization;
using System.Globalization;
using DevExpress.Charts.Native;
using DevExpress.CodeParser;
using LiveCharts;
using LiveCharts.Wpf;
using Separator = LiveCharts.Wpf.Separator;

namespace GUI
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        BUS_Customer bCustomer = new BUS_Customer();
        BUS_Plane planebus = new BUS_Plane();
        BUS_Flight flightbus = new BUS_Flight();
        BUS_Ticket bTicket = new BUS_Ticket();
        Account current_account;
        Employee current_employee;

        public Main(Account current_account, Employee current_employee)
        {
            InitializeComponent();
            this.current_account = current_account;
            this.current_employee = current_employee;
            tabControls.SelectedPageIndex = 1;
        }

        void Main_Load(object sender, EventArgs e)
        {
        }
        // Customer Controller
        void tpCustomer_Paint(object sender, PaintEventArgs e)
        {
            gcCustomer.DataSource = bCustomer.getCustomerList();
        }

        void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if (txtCustomerID.Text != null && !txtCustomerID.Text.IsEmptyOrSingle()
               && txtCustomerPhone.Text != null && !txtCustomerPhone.Text.IsEmptyOrSingle()
               && txtCustomerName.Text != null && !txtCustomerName.Text.IsEmptyOrSingle()
               && txtCustomerEmail.Text != null && !txtCustomerEmail.Text.IsEmptyOrSingle()
               && txtCustomerNationality.Text != null && !txtCustomerNationality.Text.IsEmptyOrSingle()
               && txtCustomerAddress.Text != null && !txtCustomerAddress.Text.IsEmptyOrSingle()
               && dtpCustomerDate.Text != null && (rbCustomerMale.Checked == true || rbCustomerFemale.Checked == true))
            {
                string customerID = txtCustomerID.Text.Trim();
                string customerName = txtCustomerName.Text.Trim();
                string customerEmail = txtCustomerEmail.Text.Trim();
                string customerAddress = txtCustomerAddress.Text.Trim();
                string customerPhone = txtCustomerPhone.Text.Trim();
                String customerDate = dtpCustomerDate.Value.ToString("dd/MM/yyyy");
                string customerNationality = txtCustomerNationality.Text.Trim();
                bool? customerSex;
                if (rbCustomerFemale.Checked)
                {
                    customerSex = true;
                }
                else
                {
                    customerSex = false;
                }

                Customer customer = new Customer();
                customer.NationalID = customerID;
                customer.Name = customerName;
                customer.Email = customerEmail;
                customer.Address = customerAddress;
                customer.TeleNumber = customerPhone;
                customer.DateOfBirth = DateTime.ParseExact(customerDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                customer.Nationality = customerNationality;
                customer.Sex = customerSex;

                try
                {
                    if (bCustomer.addCustomer(customer))
                    {
                        MessageBox.Show("Add customer successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gcCustomer.DataSource = bCustomer.getCustomerList();
                    }
                    else
                    {
                        MessageBox.Show("Add customer failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("Not enough infomation about customer", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gvCustomer_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            if (e.Clicks == 2) //Send customer to ticket tab
            {
                tabControls.SelectedPageIndex = 0;
                Customer order_customer = (Customer)gvCustomer.GetRow(gvCustomer.FocusedRowHandle);
                lbTicketsCustomerID.Text = order_customer.CustomerID.ToString();
                lbTicketCustomerName.Text = order_customer.Name;
                lbTicketCustomerAddress.Text = order_customer.Address;
                lbTicketCustomerPhone.Text = order_customer.TeleNumber;
                lbTicketCustomerNid.Text = order_customer.NationalID;
                if (order_customer.Sex == true)
                {
                    lbTicketCustomerSex.Text = "Nữ";
                }
                else
                {
                    lbTicketCustomerSex.Text = "Nam";
                }
                lbTicketCustomerDoB.Text = order_customer.DateOfBirth.Value.ToString("dd/MM/yyyy");
            }
            else if (e.Clicks == 1) //fetch data to textfield
            {

                if (gvCustomer.GetRow(gvCustomer.FocusedRowHandle) != null)
                {
                    Customer cur_customer = (Customer)gvCustomer.GetRow(gvCustomer.FocusedRowHandle);
                    txtCustomerID.Text = cur_customer.NationalID;
                    txtCustomerName.Text = cur_customer.Name;
                    txtCustomerAddress.Text = cur_customer.Address;
                    txtCustomerPhone.Text = cur_customer.TeleNumber;
                    txtCustomerEmail.Text = cur_customer.Email;
                    txtCustomerNationality.Text = cur_customer.Nationality;
                    dtpCustomerDate.Value = cur_customer.DateOfBirth.Value;
                    if (cur_customer.Sex == true)
                    {
                        rbCustomerFemale.Checked = true;
                    }
                    else
                    {
                        rbCustomerMale.Checked = true;
                    }
                }

            }
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            if (gvCustomer.GetRow(gvCustomer.FocusedRowHandle) != null)
            {
                if (txtCustomerID.Text != null && !txtCustomerID.Text.IsEmptyOrSingle()
               && txtCustomerPhone.Text != null && !txtCustomerPhone.Text.IsEmptyOrSingle()
               && txtCustomerName.Text != null && !txtCustomerName.Text.IsEmptyOrSingle()
               && txtCustomerEmail.Text != null && !txtCustomerEmail.Text.IsEmptyOrSingle()
               && txtCustomerNationality.Text != null && !txtCustomerNationality.Text.IsEmptyOrSingle()
               && txtCustomerAddress.Text != null && !txtCustomerAddress.Text.IsEmptyOrSingle()
               && dtpCustomerDate.Text != null && (rbCustomerMale.Checked == true || rbCustomerFemale.Checked == true))
                {
                    Customer current_customer = (Customer)gvCustomer.GetRow(gvCustomer.FocusedRowHandle);
                    Customer updated_customer = new Customer();
                    updated_customer.CustomerID = current_customer.CustomerID;
                    updated_customer.Name = txtCustomerName.Text.Trim();
                    updated_customer.NationalID = txtCustomerID.Text.Trim();
                    updated_customer.Email = txtCustomerEmail.Text.Trim();
                    updated_customer.Address = txtCustomerAddress.Text.Trim();
                    updated_customer.TeleNumber = txtCustomerPhone.Text.Trim();
                    updated_customer.DateOfBirth = DateTime.ParseExact(dtpCustomerDate.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    updated_customer.Nationality = txtCustomerNationality.Text.Trim();

                    if (rbCustomerFemale.Checked)
                    {
                        updated_customer.Sex = true;
                    }
                    else
                    {
                        updated_customer.Sex = false;
                    }

                    try
                    {
                        if (bCustomer.updateCustomer(updated_customer))
                        {
                            MessageBox.Show("Update customer successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gcCustomer.DataSource = bCustomer.getCustomerList();
                        }
                        else
                        {
                            MessageBox.Show("Update customer failure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }

            }
        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this customer?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (gvCustomer.GetRow(gvCustomer.FocusedRowHandle) != null)
                {
                    Customer current_customer = (Customer)gvCustomer.GetRow(gvCustomer.FocusedRowHandle);
                    int id = current_customer.CustomerID;

                    if (bCustomer.deleteCustomer(id))
                    {
                        MessageBox.Show("Delete customer successfully", "Success");
                        gcCustomer.DataSource = bCustomer.getCustomerList();
                    }
                    else
                    {
                        MessageBox.Show("Delete customer failure", "Error");
                    }
                }
            }
        }

        // PlaneController  =========>

        void gridControlPlane_Load(object sender, EventArgs e)
        {
            gridControlPlane.DataSource = planebus.GetListPlanes();

            if (!comboBoxPlaneState.Items.Contains("Free"))
            {
                comboBoxPlaneState.Items.Add("Free");
                comboBoxPlaneState.Items.Add("Busy");
            }

            comboBoxPlaneState.Text = "Free";
        }

        void GridPlaneRowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridPlane.GetRow(gridPlane.FocusedRowHandle) != null)
            {
                var pickedPlane = (Plane)gridPlane.GetRow(gridPlane.FocusedRowHandle);
                txtPlaneModel.Text = pickedPlane.Model;
                txtManufactor.Text = pickedPlane.Manufacturer;
                txtPlaneSeat.Text = pickedPlane.TotalSeat.ToString();
                txtRegistration.Text = pickedPlane.Registration;
                comboBoxPlaneState.SelectedIndex = pickedPlane.State > 0 ? 1 : 0;
            }
        }

        void ButtonAddPlaneClick(object sender, EventArgs e)
        {
            if (txtPlaneModel.Text != null && !txtPlaneModel.Text.IsEmptyOrSingle()
                                           && txtManufactor.Text != null && !txtManufactor.Text.IsEmptyOrSingle()
                                           && txtPlaneSeat.Text != null && !txtPlaneSeat.Text.IsEmptyOrSingle()
                                           && txtRegistration.Text != null && !txtRegistration.Text.IsEmptyOrSingle()
                                           && comboBoxPlaneState != null)
            {
                var obj = new Plane
                {
                    Model = txtPlaneModel.Text.Trim(),
                    Manufacturer = txtManufactor.Text.Trim(),
                    Registration = txtRegistration.Text.Trim(),
                    TotalSeat = int.Parse(txtPlaneSeat.Text.Trim()),
                    State = comboBoxPlaneState.SelectedIndex
                };

                try
                {
                    if (planebus.AddPlane(obj))
                    {
                        MessageBox.Show(@"Add plane successful!!!", @"SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gcCustomer.DataSource = bCustomer.getCustomerList();
                    }
                    else
                    {
                        MessageBox.Show(@"Add plane failed!!!", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(@"Missing plane data!!!", @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void ButtonUpdatePlaneClick(object sender, EventArgs e)
        {
            if (gridPlane.GetRow(gridPlane.FocusedRowHandle) != null)
            {
                if (txtPlaneModel.Text != null && !txtPlaneModel.Text.IsEmptyOrSingle()
                                               && txtManufactor.Text != null && !txtManufactor.Text.IsEmptyOrSingle()
                                               && txtPlaneSeat.Text != null && !txtPlaneSeat.Text.IsEmptyOrSingle()
                                               && txtRegistration.Text != null && !txtRegistration.Text.IsEmptyOrSingle()
                                               && comboBoxPlaneState != null)
                {
                    var pickedPlane = (Plane)gridPlane.GetRow(gridPlane.FocusedRowHandle);
                    var updatePlane = new Plane();
                    updatePlane.PlaneID = pickedPlane.PlaneID;
                    updatePlane.Model = txtPlaneModel.Text.Trim();
                    updatePlane.Manufacturer = txtManufactor.Text.Trim();
                    updatePlane.TotalSeat = int.Parse(txtPlaneSeat.Text.Trim());
                    updatePlane.Registration = txtRegistration.Text.Trim();
                    updatePlane.State = comboBoxPlaneState.SelectedIndex;

                    try
                    {
                        if (planebus.UpdatePlane(updatePlane))
                        {
                            MessageBox.Show(@"Update plane successful", @"SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gcCustomer.DataSource = bCustomer.getCustomerList();
                        }
                        else
                        {
                            MessageBox.Show(@"Update plane failed", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(@"Missing plane data!!!", @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(@"Missing plane data!!!", @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void ButtonDeletePlaneClick(object sender, EventArgs e)
        {
            if (txtPlaneModel.Text != null && !txtPlaneModel.Text.IsEmptyOrSingle()
                                           && txtManufactor.Text != null && !txtManufactor.Text.IsEmptyOrSingle()
                                           && txtPlaneSeat.Text != null && !txtPlaneSeat.Text.IsEmptyOrSingle()
                                           && txtRegistration.Text != null && !txtRegistration.Text.IsEmptyOrSingle()
                                           && comboBoxPlaneState != null)
            {
                var pickedPlane = (Plane)gridPlane.GetRow(gridPlane.FocusedRowHandle);

                try
                {
                    if (planebus.RemovePlane(pickedPlane))
                    {
                        MessageBox.Show(@"Remove plane successful", @"SUCCESS", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        gcCustomer.DataSource = bCustomer.getCustomerList();
                    }
                    else
                    {
                        MessageBox.Show(@"Remove plane failed", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // <=========

        // FlightController  =========>

        void GridControlFlightLoad(object sender, EventArgs e)
        {
            comboBoxDepart.DataSource = flightbus.GetLocations();
            comboBoxDesti.DataSource = flightbus.GetLocations();
            comboBoxDepart.DisplayMember = comboBoxDesti.DisplayMember = "LocationName";
            comboBoxPlane.DataSource = planebus.GetListPlanes();
            comboBoxPlane.DisplayMember = "Registration";
        }

        void gridViewFlight_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (gridViewFlight.GetRow(gridViewFlight.FocusedRowHandle) == null) return;
            var pickedFlight = (Flight)gridViewFlight.GetRow(gridViewFlight.FocusedRowHandle);
            var planeID = (Plane)comboBoxPlane.SelectedItem;
            var loDes = (Location)comboBoxDepart.SelectedItem;
            var loDep = (Location)comboBoxDesti.SelectedItem;
            textFlightID.Text = pickedFlight.FlightID.ToString();
            planeID.PlaneID = pickedFlight.PlaneID;
            textAirline.Text = pickedFlight.Airline;
            loDes.LocationID = (int)pickedFlight.Departure;
            loDep.LocationID = (int)pickedFlight.Destination;
            dateDepartPicker.Value = pickedFlight.DateOfDeparture.Date;
            timeDepartPicker.Value = DateTime.Parse(pickedFlight.DateOfDeparture.TimeOfDay.ToString());
        }

        void buttonAddFlight_Click(object sender, EventArgs e)
        {
            if (textAirline.Text != null && !textAirline.Text.IsEmptyOrSingle()
                                           && comboBoxDepart.SelectedItem != comboBoxDesti.SelectedItem)
            {
                var daTime = dateDepartPicker.Value.ToString("yyyy/MM/dd") + " " + timeDepartPicker.Value.ToString("HH:mm:ss");
                var planeFl = (Plane)comboBoxPlane.SelectedItem;
                var loDes = (Location)comboBoxDepart.SelectedItem;
                var loDep = (Location)comboBoxDesti.SelectedItem;

                var obj = new Flight()
                {
                    PlaneID = planeFl.PlaneID,
                    Airline = textAirline.Text.Trim(),
                    Departure = loDep.LocationID,
                    Destination = loDes.LocationID,
                    DateOfDeparture = DateTime.Parse(daTime)
                };

                try
                {
                    if (flightbus.AddFlights(obj))
                    {
                        MessageBox.Show(@"Add flight successful!!!", @"SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gcCustomer.DataSource = bCustomer.getCustomerList();
                    }
                    else
                    {
                        MessageBox.Show(@"Add flight failed!!!", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(@"Missing flight data!!!", @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void buttonUpdateFlight_Click(object sender, EventArgs e)
        {
            if (gridViewFlight.GetRow(gridViewFlight.FocusedRowHandle) != null)
            {
                if (textAirline.Text != null && !textAirline.Text.IsEmptyOrSingle()
                                                && comboBoxDepart.SelectedItem != comboBoxDesti.SelectedItem)
                {
                    var pickedFlight = (Flight)gridViewFlight.GetRow(gridViewFlight.FocusedRowHandle);
                    var updatedFlight = new Flight();
                    var daTime = dateDepartPicker.Value.ToString("yyyy/MM/dd") + " " + timeDepartPicker.Value.ToString("HH:mm:ss");
                    var planeFl = (Plane)comboBoxDepart.SelectedItem;
                    var loDes = (Location)comboBoxDepart.SelectedItem;
                    var loDep = (Location)comboBoxDesti.SelectedItem;
                    updatedFlight.FlightID = pickedFlight.FlightID;
                    updatedFlight.PlaneID = planeFl.PlaneID;
                    updatedFlight.Airline = textAirline.Text.Trim();
                    updatedFlight.Departure = loDep.LocationID;
                    updatedFlight.Destination = loDes.LocationID;
                    updatedFlight.DateOfDeparture = DateTime.Parse(daTime);

                    try
                    {
                        if (flightbus.UpdateFlights(updatedFlight))
                        {
                            MessageBox.Show(@"Update flight successful", @"SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gcCustomer.DataSource = bCustomer.getCustomerList();
                        }
                        else
                        {
                            MessageBox.Show(@"Update flight failed", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(@"Missing flight data!!!", @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(@"Missing flight data!!!", @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void buttonDeleteFlight_Click(object sender, EventArgs e)
        {
            var pickedFlight = (Flight)gridViewFlight.GetRow(gridViewFlight.FocusedRowHandle);

            try
            {
                if (flightbus.DeleteFlights(pickedFlight))
                {
                    MessageBox.Show(@"Remove flight successful", @"SUCCESS", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    gcCustomer.DataSource = bCustomer.getCustomerList();
                }
                else
                {
                    MessageBox.Show(@"Remove flight failed", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void TabNavigationFlightPaint(object sender, PaintEventArgs e)
        {
            gridControlFlight.DataSource = flightbus.GetListFlights();
        }

        private void tpStatistics_Paint(object sender, PaintEventArgs e)
        {
            //Pie chart - ticket
            Func<ChartPoint, String> lablePoint = chartpoint => string.Format("{0} ({1:P})", chartpoint.Y, chartpoint.Participation);
            DateTime date = new DateTime(2023, 04, 23);
            List<Bill_Detail> list = bTicket.getTicketListByDate(date);
            SeriesCollection series = new SeriesCollection();
            series.Add(new PieSeries() { Title = "Normal Tickets", Values = new ChartValues<int> { list.Count(t => t.SeatClass == false) }, DataLabels = true, LabelPoint = lablePoint });
            series.Add(new PieSeries() { Title = "VIP Tickets", Values = new ChartValues<int> { list.Count(t => t.SeatClass == true) }, DataLabels = true, LabelPoint = lablePoint });
            pieTicket.Series = series;
            pieTicket.LegendLocation = LegendLocation.Bottom;

            //Total Price - Chart
            var total = list.GroupBy(t => t.BookingDate).Select(t => new
            {
                BookingDate = t.Key,
                Total = t.Sum(ta => ta.TotalPrice),
            }).ToList();

            ColumnSeries col = new ColumnSeries() { DataLabels = true, Values = new ChartValues<decimal>(), LabelPoint = point => point.Y.ToString() };
            Axis ax = new Axis() { Separator = new Separator() { Step = 1, IsEnabled = true } };
            ax.Labels = new List<string>();
            foreach (var x in total) {
                col.Values.Add(x.Total);
                ax.Labels.Add(x.BookingDate.Value.Day.ToString());
            }

            cartesianTicket.Series.Add(col);
            cartesianTicket.AxisX.Add(ax);
            cartesianTicket.AxisY.Add(new Axis
            {
                LabelFormatter = value => value.ToString(),
                Separator = new Separator() { }
            });


        }
    }

    //private void btnUpdateTicket_Click(object sender, EventArgs e)
    //{
    //    if (gvTicket.GetRow(gvTicket.FocusedRowHandle) != null)
    //    {
    //        Bill_Detail updated_ticket = (Bill_Detail) gvTicket.GetRow(gvTicket.FocusedRowHandle);
    //        //Get flightID to update flight
    //        //Get Seat and SeatClass to update seat, seatclass and total price
    //        //Get Date to update booking date
    //        String current_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    //        updated_ticket.BookingDate = DateTime.Parse(current_date);
    //        MessageBox.Show(updated_ticket.BookingDate.ToString());
    //        //Get Employee to update Employee in ticket

    //        bTicket.updateTicket(updated_ticket);
    //        gcTicket.DataSource = bTicket.getTicketsList();
    //    }
    //}
}
