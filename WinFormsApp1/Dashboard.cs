using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;

namespace WinFormsApp1
{
    public partial class Dashboard : Form
    {
        private string username, role;
        string? query, customerID;
        int rid;
        function fn = new function();

        public Dashboard(string username, string role)
        {
            InitializeComponent();
            this.username = username;
            this.role = role;

            if (role != "admin")
            {
                tabControl1.SelectedTab = tabControl1.TabPages[1];
                roomRegistration(username);
            }
            else if (role == "admin")
            {
                tabControl1.SelectedTab = tabControl1.TabPages[0];
                addRoom(username);
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (role != "admin" && (e.TabPageIndex == 0 || e.TabPageIndex == 3 || e.TabPageIndex == 4))
            {
                e.Cancel = true;
                MessageBox.Show("Bạn không có quyền truy cập vào chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages[0])
            {
                addRoom(username);
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[1])
            {
                roomRegistration(username);
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[2])
            {
                checkOut(username);
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[3])
            {
                customerManagement(username);
            }
            else if (tabControl1.SelectedTab == tabControl1.TabPages[4])
            {
                staffManagement(username);
            }
        }

        public string welcomeString(string username)
        {
            string welcome;
            string timeString = DateTime.Now.TimeOfDay.ToString("hh\\:mm");
            TimeSpan currentTime = TimeSpan.Parse(timeString);

            TimeSpan morning = new TimeSpan(4, 0, 0);
            TimeSpan noon = new TimeSpan(11, 0, 0);
            TimeSpan afternoon = new TimeSpan(14, 0, 0);
            TimeSpan evening = new TimeSpan(18, 0, 0);
            TimeSpan night = new TimeSpan(23, 0, 0);

            if (currentTime >= morning && currentTime < noon)
            {
                welcome = "Buổi sáng tốt lành nha " + username + "!";
            }
            else if (currentTime >= noon && currentTime < afternoon)
            {
                welcome = "Buổi trưa vui vẻ nhé " + username + "!";
            }
            else if (currentTime >= afternoon && currentTime < evening)
            {
                welcome = "Gửi đến bạn một buổi chiều hạnh phúc, " + username + "!";
            }
            else if (currentTime >= evening && currentTime < night)
            {
                welcome = "Chào buổi tối " + username + "!";
            }
            else
            {
                welcome = "Chúng tôi vẫn thức cùng bạn, " + username + "!";
            }
            return welcome;
        }

        //TAB THEM PHONG
        public void addRoom(string username)
        {
            lbAddRoomWelcome.Text = welcomeString(username);
            btnRoomEdit.Enabled = false;
            btnRoomDelete.Enabled = false;
            query = "SELECT * FROM rooms";
            DataSet ds = fn.getData(query);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            if (tbRoomNo.Text != "" && cbRoomType.Text != "" && cbBedNum.Text != "" && tbRoomPrice.Text != "")
            {
                string roomNo = tbRoomNo.Text;
                string roomType = cbRoomType.Text;
                string bedNum = cbBedNum.Text;
                Int64 price;
                bool isNumPrice = Int64.TryParse(tbRoomPrice.Text, out price);
                if (isNumPrice)
                {
                    query = "INSERT INTO rooms (roomNo, roomType, bedNum, price) VALUES ('" + roomNo + "', N'" + roomType + "', '" + bedNum + "', " + price + ")";
                    fn.setData(query, "Đã thêm phòng!");
                    addRoomClearAll();
                    addRoom(username);
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng giá phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            tbRoomNo.Text = row.Cells["roomNo"].Value.ToString();
            cbRoomType.Text = row.Cells["roomType"].Value.ToString();
            cbBedNum.Text = row.Cells["bedNum"].Value.ToString();
            tbRoomPrice.Text = row.Cells["price"].Value.ToString();
            btnRoomDelete.Enabled = true;
            btnRoomEdit.Enabled = true;
        }

        private void btnRoomEdit_Click(object sender, EventArgs e)
        {
            if (tbRoomNo.Text != "" && cbRoomType.Text != "" && cbBedNum.Text != "" && tbRoomPrice.Text != "")
            {
                string roomNo = tbRoomNo.Text;
                string roomType = cbRoomType.Text;
                string bedNum = cbBedNum.Text;
                Int64 price;
                bool isNumPrice = Int64.TryParse(tbRoomPrice.Text, out price);
                if (isNumPrice)
                {
                    query = "UPDATE rooms SET roomType = N'" + roomType + "', bedNum = '" + bedNum + "', price = " + price + " WHERE roomNo = '" + roomNo + "'";
                    fn.setData(query, "Đã cập nhật thông tin!");
                    addRoomClearAll();
                    addRoom(username);
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập đúng giá phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRoomDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xoá phòng này?", "Xác nhận!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                query = $"DELETE FROM rooms WHERE roomNo = '{tbRoomNo.Text}'";
                fn.setData(query, "Phòng đã được xoá!");
            }
            addRoom(username);
        }

        public void addRoomClearAll()
        {
            tbRoomNo.Clear();
            cbRoomType.SelectedIndex = -1;
            cbBedNum.SelectedIndex = -1;
            tbRoomPrice.Clear();
        }

        //TAB DANG KY THUE PHONG
        public void roomRegistration(string username)
        {
            lbRoomRegistrationWelcome.Text = welcomeString(username);
            query = "SELECT COUNT(*) FROM rooms WHERE booked != 'YES'";
            int availableRoom = fn.getNumberData(query);
            lbAvailableRoom.Text = "Số phòng còn trống: " + availableRoom;
            datetimeRegister.Value = DateTime.Now;
        }

        public void getComboBoxOption(string query, ComboBox combo)
        {
            SqlDataReader sdr = fn.getForCombo(query);
            combo.Items.Clear();
            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    combo.Items.Add(sdr.GetString(i));
                }
            }
            sdr.Close();
        }

        private void cbRoomTypeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbBedNumSelect.SelectedIndex = -1;
            cbRoomNoSelect.Items.Clear();
            tbRoomPriceShow.Clear();
        }

        private void cbBedNumSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbRoomNoSelect.Items.Clear();
            query = $"SELECT roomNo FROM rooms WHERE roomType = N'{cbRoomTypeSelect.Text}' and bedNum = '{cbBedNumSelect.Text}' and booked = 'NO'";
            getComboBoxOption(query, cbRoomNoSelect);
        }

        private void cbRoomNoSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = $"SELECT price, roomid FROM rooms WHERE roomNo = '{cbRoomNoSelect.Text}'";
            DataSet ds = fn.getData(query);
            tbRoomPriceShow.Text = ds.Tables[0].Rows[0][0].ToString();
            rid = int.Parse(ds.Tables[0].Rows[0][1].ToString());
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            if (tbTen.Text != "" && tbPhoneNo.Text != "" && tbNationality.Text != "" && cbGender.SelectedIndex != -1 && tbID.Text != "" && tbAddress.Text != "" && tbRoomPriceShow.Text != "")
            {
                string name = tbTen.Text;
                string phoneNumber = tbPhoneNo.Text;
                string country = tbNationality.Text;
                string gender = cbGender.Text;
                string dob = datetimeDOB.Text;
                string id = tbID.Text;
                string address = tbAddress.Text;
                string checkIn = datetimeRegister.Text;
                string roomNumber = cbRoomNoSelect.Text;

                query = $"INSERT INTO roomRegistration (cname, mobile, nationality, gender, dob, idproof, address, checkin, roomid) VALUES (N'{name}', '{phoneNumber}', N'{country}', N'{gender}', '{dob}', '{id}', '{address}', '{checkIn}', '{rid}') UPDATE rooms SET booked = 'YES' WHERE roomNo = '{roomNumber}'";
                fn.setData(query, "Đăng ký thuê thành công!");
                roomRegistrationClearAll();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnReType_Click(object sender, EventArgs e)
        {
            roomRegistrationClearAll();
        }

        public void roomRegistrationClearAll()
        {
            tbTen.Clear();
            tbPhoneNo.Clear();
            tbNationality.Clear();
            cbGender.SelectedIndex = -1;
            datetimeDOB.ResetText();
            tbID.Clear();
            tbAddress.Clear();
            datetimeRegister.Value = DateTime.Now;
            cbRoomTypeSelect.SelectedIndex = -1;
            cbBedNumSelect.SelectedIndex = -1;
            cbRoomNoSelect.Items.Clear();
            tbRoomPriceShow.Clear();
        }

        //TAB THANH TOAN
        public void checkOut(string username)
        {
            lbCheckOutWelcome.Text = welcomeString(username);
            tbCheckOutDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            btnCheckOut.Enabled = false;
            query = "SELECT roomRegistration.cid, roomRegistration.cname, roomRegistration.mobile, rooms.roomNo, roomRegistration.idproof, roomRegistration.checkin, roomRegistration.checkout, rooms.price, roomRegistration.nationality, roomRegistration.gender, roomRegistration.dob, roomRegistration.address FROM roomRegistration INNER JOIN rooms ON roomRegistration.roomid = rooms.roomid WHERE checkoutstate = 'NO'";
            DataSet ds = fn.getData(query);
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void btnSearchByName_Click(object sender, EventArgs e)
        {
            query = $"SELECT roomRegistration.cid, roomRegistration.cname, roomRegistration.mobile, rooms.roomNo, roomRegistration.idproof, roomRegistration.checkin, roomRegistration.checkout, rooms.price, roomRegistration.nationality, roomRegistration.gender, roomRegistration.dob, roomRegistration.address FROM roomRegistration INNER JOIN rooms ON roomRegistration.roomid = rooms.roomid WHERE checkoutstate = 'NO' AND cname = N'{tbSearch.Text}'";
            DataSet ds = fn.getData(query);
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void btnSearchByRoomNo_Click(object sender, EventArgs e)
        {
            query = $"SELECT roomRegistration.cid, roomRegistration.cname, roomRegistration.mobile, rooms.roomNo, roomRegistration.idproof, roomRegistration.checkin, roomRegistration.checkout, rooms.price, roomRegistration.nationality, roomRegistration.gender, roomRegistration.dob, roomRegistration.address FROM roomRegistration INNER JOIN rooms ON roomRegistration.roomid = rooms.roomid WHERE checkoutstate = 'NO' AND roomNo = '{tbSearch.Text}'";
            DataSet ds = fn.getData(query);
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
            tbCustomerName.Text = row.Cells["cname"].Value.ToString();
            tbCustomerRoomNo.Text = row.Cells["roomNo"].Value.ToString();
            customerID = row.Cells["cid"].Value.ToString();

            if (tbCustomerName.Text != "")
            {
                btnCheckOut.Enabled = true;
                string checkInDate = row.Cells["checkin"].Value.ToString();
                string checkOutDate = DateTime.Now.ToString();
                DateTime date1;
                DateTime date2;

                if (DateTime.TryParse(checkInDate, out date1) && DateTime.TryParse(checkOutDate, out date2))
                {
                    TimeSpan difference = date2 - date1;
                    int days = difference.Days;

                    if (difference.Hours > 12)
                    {
                        days += 1;
                    }
                    Int64 roomPrice = Convert.ToInt64(row.Cells["price"].Value);
                    lbPayment.Text = "Số tiền cần thanh toán: " + days * roomPrice;
                }
            }
            else
            {
                btnCheckOut.Enabled = false;
                lbPayment.Text = "Số tiền cần thanh toán: 0";
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (tbCustomerName.Text != "")
            {
                if (MessageBox.Show("Bạn có chắc chắn thao tác trả phòng không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    query = $"UPDATE roomRegistration SET checkoutstate = 'YES', checkout = '{tbCheckOutDate.Text}' WHERE cid = '{customerID}' UPDATE rooms SET booked = 'NO' WHERE roomNo = '{tbCustomerRoomNo.Text}'";
                    fn.setData(query, "Thanh toán thành công!");
                    checkOut(username);
                    checkOutClearAll();
                }
            }
        }

        public void checkOutClearAll()
        {
            tbSearch.Clear();
            tbCustomerName.Clear();
            tbCustomerRoomNo.Clear();
            lbPayment.Text = "Số tiền cần thanh toán: 0";
        }

        //TAB QUAN LY KHACH HANG
        public void customerManagement(string username)
        {
            lbCustomerManagementWelcome.Text = welcomeString(username);

        }

        private void cbChooseCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbChooseCriteria.SelectedIndex == 0)
            {
                query = "SELECT roomRegistration.cid, roomRegistration.cname, roomRegistration.mobile, rooms.roomNo, roomRegistration.idproof, roomRegistration.checkin, roomRegistration.checkout, roomRegistration.checkoutstate, rooms.price, roomRegistration.nationality, roomRegistration.gender, roomRegistration.dob, roomRegistration.address FROM roomRegistration INNER JOIN rooms ON roomRegistration.roomid = rooms.roomid";
                loadRecord(query);
            }
            else if (cbChooseCriteria.SelectedIndex == 1)
            {
                query = "SELECT roomRegistration.cid, roomRegistration.cname, roomRegistration.mobile, rooms.roomNo, roomRegistration.idproof, roomRegistration.checkin, roomRegistration.checkout, roomRegistration.checkoutstate, rooms.price, roomRegistration.nationality, roomRegistration.gender, roomRegistration.dob, roomRegistration.address FROM roomRegistration INNER JOIN rooms ON roomRegistration.roomid = rooms.roomid WHERE checkoutstate = 'NO'";
                loadRecord(query);
            }
            else if (cbChooseCriteria.SelectedIndex == 2)
            {
                query = "SELECT roomRegistration.cid, roomRegistration.cname, roomRegistration.mobile, rooms.roomNo, roomRegistration.idproof, roomRegistration.checkin, roomRegistration.checkout, roomRegistration.checkoutstate, rooms.price, roomRegistration.nationality, roomRegistration.gender, roomRegistration.dob, roomRegistration.address FROM roomRegistration INNER JOIN rooms ON roomRegistration.roomid = rooms.roomid WHERE checkoutstate = 'YES'";
                loadRecord(query);
            }
        }

        public void loadRecord(string query)
        {
            DataSet ds = fn.getData(query);
            dataGridView3.DataSource = ds.Tables[0];
        }

        //TAB QUAN LY NHAN VIEN
        public void staffManagement(string username)
        {
            lbStaffManagementWelcome.Text = welcomeString(username);
        }

        private void tabStaffManagement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabStaffManagement.SelectedTab == tabStaffManagement.TabPages[1])
            {
                getStaffList();
            }
            else if (tabStaffManagement.SelectedTab == tabStaffManagement.TabPages[2])
            {
                getStaffListForDelete();
            }
        }

        //TAB QUAN LY NHAN VIEN > DANG KY NHAN VIEN
        private void btnStaffRegister_Click(object sender, EventArgs e)
        {
            if (tbStaffName.Text != "" && tbStaffContact.Text != "" && tbStaffUsername.Text != "" && tbStaffPassword.Text != "" && isValidEmail(tbStaffEmail.Text) && cbStaffGender.SelectedIndex != -1 && cbStaffRole.SelectedIndex != -1)
            {
                string name = tbStaffName.Text;
                string email = tbStaffEmail.Text;
                string contact = tbStaffContact.Text;
                string username = tbStaffUsername.Text;
                string password = tbStaffPassword.Text;
                string gender = cbStaffGender.Text;
                string role = cbStaffRole.Text;

                query = $"INSERT INTO usersList (name, contact, gender, username, password, email, role) VALUES (N'{name}', '{contact}', N'{gender}', '{username}', '{password}', '{email}', '{role}')";
                fn.setData(query, "Đăng ký nhân viên thành công!");
                staffRegisterClearAll();
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void staffRegisterClearAll()
        {
            tbStaffName.Clear();
            tbStaffContact.Clear();
            tbStaffUsername.Clear();
            tbStaffPassword.Clear();
            tbStaffEmail.Clear();
            cbStaffGender.SelectedIndex = -1;
            cbStaffRole.SelectedIndex = -1;
        }

        public bool isValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        //TAB QUAN LY NHAN VIEN > DANH SACH NHAN VIEN
        public void getStaffList()
        {
            query = "SELECT * FROM usersList";
            DataSet ds = fn.getData(query);
            dataGridView4.DataSource = ds.Tables[0];
        }

        //TAB QUAN LY NHAN VIEN > XOA NHAN VIEN
        public void getStaffListForDelete()
        {
            query = "SELECT * FROM usersList";
            DataSet ds = fn.getData(query);
            dataGridView5.DataSource = ds.Tables[0];
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView5.Rows[e.RowIndex];
            tbStaffDelete.Text = row.Cells["username"].Value.ToString();
        }

        private void tbStaffDelete_TextChanged(object sender, EventArgs e)
        {
            query = $"SELECT * FROM usersList WHERE username LIKE '{tbStaffDelete.Text}%'";
            DataSet ds = fn.getData(query);
            dataGridView5.DataSource = ds.Tables[0];
        }

        private void btnStaffDelete_Click(object sender, EventArgs e)
        {
            if (fn.getUserRole(tbStaffDelete.Text) == "admin")
            {
                query = "SELECT COUNT(*) FROM usersList WHERE role = 'admin'";
                int adminNum = fn.getNumberData(query);
                if (tbStaffDelete.Text == username)
                {
                    MessageBox.Show("Bạn không thể xoá chính mình!", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (adminNum == 1)
                {
                    MessageBox.Show("Bạn không thể xoá user admin cuối cùng!", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (MessageBox.Show("Bạn chắc chắn muốn xoá?", "Xác nhận!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                query = $"DELETE FROM usersList WHERE username = '{tbStaffDelete.Text}'";
                fn.setData(query, "Xoá thành công!");
            }

            getStaffListForDelete();
            tbStaffDelete.Clear();
        }
    }
}